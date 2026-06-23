// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 06月23日 13:06
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Devices\ChannelRegistry.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using Modbus.Device;

namespace ModbusApp.Services.Channel;

public class ChannelRegistry
{
    private readonly Dictionary<string, IModbusChannel> _modbusChannels;

    private readonly Dictionary<string, IChannel> _tcpChannels;

    public ChannelRegistry(
        IEnumerable<IModbusChannel> modbusChannels,
        IEnumerable<IChannel> tcpChannels)
    {
        _modbusChannels = modbusChannels.ToDictionary(x => x.Name);

        _tcpChannels = tcpChannels.ToDictionary(x => x.Name);
    }

    public IModbusMaster GetMaster(string name)
    {
        return _modbusChannels.TryGetValue(name, out var channel)
            ? channel.Master
            : throw new InvalidOperationException(
                $"未找到 Modbus 通道: {name}");
    }

    public IChannel GetTcpChannel(string name)
    {
        return _tcpChannels.TryGetValue(name, out var channel)
            ? channel
            : throw new InvalidOperationException(
                $"未找到 TCP 通道: {name}");
    }
}