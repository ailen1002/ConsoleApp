// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 05月15日 14:05
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Models\RtuSlaveInfo.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System.IO.Ports;

namespace ModbusApp.Models;

public class RtuSlaveInfo
{   
    public string Name { get; init; } = string.Empty;
    public string PortName { get; init; } = string.Empty;
    public int BaudRate { get; init; } = 19200;
    public int DataBits { get; init; } = 8;
    public Parity Parity { get; init; } = Parity.None;
    public StopBits StopBits { get; init; } = StopBits.One;
    public int ReadTimeout { get; init; } = 200;
    public int WriteTimeout { get; init; } = 200;
    public byte SlaveId { get; init; } = 1;
}