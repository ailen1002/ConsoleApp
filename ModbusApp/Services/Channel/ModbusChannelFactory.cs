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
    public static async Task<IReadOnlyList<IModbusChannel>> CreateChannelsAsync()
    {
        var channels = new List<IModbusChannel>();

        var tcpTask1 = Task.Run(async () =>
        {
            var client = new TcpClient();
            await client.ConnectAsync("192.168.1.105", 502);
            channels.Add(new ModbusTcpChannel("数字量输入板", client));
        });
        
        var tcpTask2 = Task.Run(async () =>
        {
            var client = new TcpClient();
            await client.ConnectAsync("192.168.1.151", 502);
            channels.Add(new ModbusTcpChannel("数字量输出板", client));
        });

        var rtuTask = Task.Run(() =>
        {
            var sp = new SerialPort("COM2", 19200);
            sp.Open();
            channels.Add(new ModbusRtuChannel("COM2", sp));
        });

        foreach (var c in channels)
        {
            Log.Information($"名称:{c.Name} | 类型:{c.Type}");
        }
        
        await Task.WhenAll(tcpTask1, tcpTask2, rtuTask);
        return channels.AsReadOnly();
    }
}