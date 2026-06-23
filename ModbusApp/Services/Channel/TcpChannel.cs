// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 06月16日 11:06
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Services\Channel\TcpChannel.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System.Net.Sockets;

namespace ModbusApp.Services.Channel;

public sealed class TcpChannel(string name, TcpClient client) : IChannel
{
    public string Name { get; } = name;
    
    public string Type { get; } = "Test";
    public TcpClient Client { get; } = client;
    
    public void Dispose()
    {
        Client.Dispose();
    }
}