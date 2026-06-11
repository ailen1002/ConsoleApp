// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 06月05日 12:06
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Services\Connections\TcpConnection.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System.Net.Sockets;
using Modbus.Device;

namespace ModbusApp.Services.Channel;

public sealed class ModbusTcpChannel(string name, TcpClient client) : IModbusChannel
{
    public string Name { get; } = name;
    public string Type => "TCP";
    public IModbusMaster Master { get; } = ModbusIpMaster.CreateIp(client);

    public void Dispose()
    {
        Master.Dispose();
        client.Dispose();
    }
}