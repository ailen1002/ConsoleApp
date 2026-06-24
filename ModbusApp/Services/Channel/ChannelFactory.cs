// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 06月16日 10:06
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Services\Channel\ChannelFactory.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System.Net.Sockets;
using Serilog;

namespace ModbusApp.Services.Channel;

public static class ChannelFactory
{
    private static readonly (string Name, string Ip, int Port)[] TcpConfigs =
    [
        ("测试设备通讯卡", "192.168.1.100", 9000)
    ];
    
    public static async Task<IReadOnlyList<IChannel>> CreateChannelAsync()
    {
        var tasks = TcpConfigs.Select(async config =>
        {
            var client = new TcpClient();
            await client.ConnectAsync(config.Ip, config.Port);

            return new TcpChannel(config.Name, client);
        });
        
        var channels = await Task.WhenAll(tasks);
        
        foreach (var c in channels)
        {
            Log.Information($"名称: {c.Name} | 类型: {c.Type}");
        }
        
        return channels.AsReadOnly();
    }
}