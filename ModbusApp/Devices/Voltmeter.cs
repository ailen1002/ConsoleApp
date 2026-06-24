// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 06月24日 09:06
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Devices\Voltmeter.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using Modbus.Device;
using Serilog;

namespace ModbusApp.Devices;

public class Voltmeter(IModbusMaster channel, string channelName, byte slaveId)
{
    private readonly SemaphoreSlim _lock = new(1, 1);
    public double  Volt;

    public async Task ReadVoltageAsync()
    {
        await _lock.WaitAsync();

        try
        {
            var value = await channel.ReadHoldingRegistersAsync(slaveId, 37, 1);

            Volt = Math.Round(value[0] / 10.0f, 1);
            
            Log.Debug("{BoardName}: {@Values}",channelName, Volt);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "{BoardName}: 读取状态失败", channelName);

            throw;
        }
        finally
        {
            _lock.Release();
        }
    }
}