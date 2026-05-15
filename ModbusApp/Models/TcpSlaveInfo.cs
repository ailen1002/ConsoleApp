// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 05月15日 13:05
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Models\TcpSlaveInfo.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System.Net.Sockets;

namespace ModbusApp.Models;

public class TcpSlaveInfo
{
    public string Name { get; init; } = string.Empty;
    public string Ip { get; init; } = string.Empty;
    public int Port { get; init; } = 502;
    public byte SlaveId { get; init; } = 1;
    public int ReadTimeout { get; init; } = 2000;
    public int WriteTimeout { get; init; } = 2000;
}