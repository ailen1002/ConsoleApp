// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 06月16日 11:06
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Services\Channel\IChannel.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System.Net.Sockets;

namespace ModbusApp.Services.Channel;

public interface IChannel : IDisposable
{
    string Name { get; }
    TcpClient Client { get; }
}