// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 06月16日 08:06
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Devices\VoltageBoard\VoltageBoard.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using Modbus.Device;
using ModbusApp.Services.Channel;
using Serilog;

namespace ModbusApp.Devices;

public class VoltageBoard(IEnumerable<IModbusChannel> channels, string channelName)
{
    private readonly IModbusMaster _master = channels.First(c => c.Name == channelName).Master;
    private readonly SemaphoreSlim _lock = new(1, 1);
    private const int ChannelCount = 16;
    private readonly double[] _voltages = new double[ChannelCount];
    private const byte SlaveId = 1;
    private const ushort StartAddress = 48;
    private const ushort Quantity = 16;
    
    public double this[int index] => Get(index);
    
    private double Get(int index)
        => index >= 0 && index < _voltages.Length ? _voltages[index] : 0.0f;
    
    public async Task ReadStateAsync()
    {
        await _lock.WaitAsync();

        try
        {
            var response = await _master.ReadHoldingRegistersAsync(SlaveId, StartAddress, Quantity);
            
            for (var i = 0; i < response.Length; i++)
            {
                _voltages[i] = Math.Round(response[i] / 10.0f, 1);
            }
            
            Log.Debug("{BoardName}: {@Values}", channelName, _voltages);
        }
        finally
        {
            _lock.Release();
        }
    }
}