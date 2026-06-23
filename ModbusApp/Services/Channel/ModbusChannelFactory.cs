// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 06月11日 12:06
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Services\Channel\aaa.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System.IO.Ports;
using System.Net.Sockets;
using Serilog;

namespace ModbusApp.Services.Channel;

public static class ModbusChannelFactory
{
    private static readonly (string Name, string Ip, int Port)[] TcpConfigs =
    [
        ("继电器检测板卡", "192.168.1.101", 502),
        ("膨胀阀检测板卡", "192.168.1.102", 502),
        ("电压板检测板卡", "192.168.1.103", 502),
        ("电阻板检测板卡", "192.168.1.104", 502),
        ("数字量输入板卡", "192.168.1.105", 502),
        ("数字量输出板卡", "192.168.1.106", 502)
    ];
    
    public static async Task<IReadOnlyList<IModbusChannel>> CreateChannelsAsync()
    {
        var tcpTasks = TcpConfigs.Select(async config =>
        {
            var client = new TcpClient();

            await client.ConnectAsync(config.Ip, config.Port);

            return (IModbusChannel)new ModbusTcpChannel(config.Name, client);
        });

        var rtuTask = Task.Run(() =>
        {
            var sp = new SerialPort("COM2", 9600, Parity.None, 8, StopBits.One);
            sp.Open();
            
            return (IModbusChannel)new ModbusRtuChannel("COM2", sp);
        });

        var channels = await Task.WhenAll(tcpTasks.Append(rtuTask));

        foreach (var c in channels)
        {
            Log.Information($"名称:{c.Name} | 类型:{c.Type}");
        }
        
        return channels.AsReadOnly();
    }
}