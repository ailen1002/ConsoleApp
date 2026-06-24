// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 06月24日 09:06
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Devices\Ammeter.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using Modbus.Device;
using Serilog;

namespace ModbusApp.Devices;

public class Ammeter(IModbusMaster channel, string channelName, byte slaveId)
{
    private readonly SemaphoreSlim _lock = new(1, 1);
    private const int ChannelCount = 3;
    public readonly ushort[] Current = new ushort[ChannelCount];

    public async Task ReadCurrentAsync()
    {
        await _lock.WaitAsync();

        try
        {
            var value = await channel.ReadHoldingRegistersAsync(slaveId, 43, 3);

            for (var i = 0; i < value.Length; i++)
            {
                Current[i] = value[i];
                
                Log.Debug("{BoardName}: {@Values}",channelName, Current[i]);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "{BoardName} 读取状态失败", channelName);

            throw;
        }
        finally
        {
            _lock.Release();
        }
    }
}