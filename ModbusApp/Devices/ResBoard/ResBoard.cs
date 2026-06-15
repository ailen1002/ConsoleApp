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

namespace ModbusApp.Devices.ResBoard;

public class ResBoard(IEnumerable<IModbusChannel> channels, string channelName)
{
    private readonly IModbusMaster _master = channels.First(c => c.Name == channelName).Master;
    private const byte SlaveId = 1;
    private const ushort StartAddress = 0;
    private const int ChannelCount = 16;

    
    public Task OpenAll()
        => WriteAll(1);

    public Task CloseAll()
        => WriteAll(0);

    public Task CloseOddChannels()
        => WritePattern(i => i % 2 == 0);

    public Task CloseEvenChannels()
        => WritePattern(i => i % 2 == 1);
    
    private Task WriteAll(ushort value)
    {
        var values = Enumerable
            .Repeat(value, ChannelCount)
            .ToArray();

        return _master.WriteMultipleRegistersAsync(SlaveId, StartAddress, values);
    }
    
    private Task WritePattern(Func<int, bool> selector)
    {
        var values = new ushort[ChannelCount];

        for (var i = 0; i < ChannelCount; i++)
        {
            values[i] = selector(i) ? (ushort)1 : (ushort)0;
        }

        return _master.WriteMultipleRegistersAsync(SlaveId, StartAddress, values);
    }
}