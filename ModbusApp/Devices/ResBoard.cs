// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 06月15日 13:06
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Devices\ResBoard\ResBoard.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using Modbus.Device;
using ModbusApp.Services.Channel;
using Serilog;

namespace ModbusApp.Devices;

public class ResBoard(IEnumerable<IModbusChannel> channels, string channelName)
{
    private readonly IModbusMaster _master = channels.First(c => c.Name == channelName).Master;
    private readonly SemaphoreSlim _lock = new(1, 1);
    private const byte SlaveId = 1;
    private const ushort StartAddress = 0;
    private const int Quantity = 16;
    
    public Task OpenAll()
        => WriteAll(1);

    public Task CloseAll()
        => WriteAll(0);

    public Task CloseOddChannels()
        => WritePattern(i => i % 2 == 0);

    public Task CloseEvenChannels()
        => WritePattern(i => i % 2 == 1);
    
    private async Task WriteAll(ushort value)
    {
        await _lock.WaitAsync();
        
        try
        {
            for (var i = 0; i < Quantity; i++)
            {
                await Task.Delay(500);
                await _master.WriteSingleRegisterAsync(SlaveId, (ushort)(StartAddress + i), value);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "{BoardName} WriteAll写入失败", channelName);
            throw;
        }
        finally
        {
            _lock.Release();
        }
    }
    
    private async Task WritePattern(Func<int, bool> selector)
    {
        await _lock.WaitAsync();
        try
        {
            for (var i = 0; i < Quantity; i++)
            {
                var value = selector(i) ? (ushort)1 : (ushort)0;
                await Task.Delay(500);
                await _master.WriteSingleRegisterAsync(SlaveId, (ushort)(StartAddress + i), value);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "{BoardName} WritePattern写入失败", channelName);
            throw;
        }
        finally
        {
            _lock.Release();
        }
    }
}