// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 06月05日 12:06
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Services\Connections\RtuConnection.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System.IO.Ports;
using Modbus.Device;

namespace ModbusApp.Services.Channel;

public sealed class ModbusRtuChannel(string name, SerialPort serialPort) : IModbusChannel
{
    public string Name { get; } = name;
    public string Type => "RTU";
    public IModbusMaster Master { get; } = ModbusSerialMaster.CreateRtu(serialPort);

    public void Dispose()
    {
        Master.Dispose();
        serialPort.Dispose();
    }
}