// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 06月05日 12:06
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Models\DeviceConfig.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System.IO.Ports;

namespace ModbusApp.Models;

public class DeviceConfig
{
    public List<ModbusTcpConfig> ModbusTcp { get; set; } = [];
    public List<ModbusRtuConfig> ModbusRtu { get; set; } = [];
    public TargetConfig Target { get; set; } = new();
}

public abstract class ModbusTcpConfig
{
    public string DeviceName { get; set; } = string.Empty;
    public string Ip { get; set; } = string.Empty;
    public int Port { get; set; } = 502;
    public byte SlaveId { get; set; } = 1;
    public int PollInterval { get; set; } = 1000;
    public int ReadTimeout { get; set; } = 1000;
    public int WriteTimeout { get; set; } = 1000;
}

public abstract class ModbusRtuConfig
{
    public string Port { get; set; } = string.Empty;
    public int BaudRate { get; set; } = 9600;
    public int DataBits { get; set; } = 8;
    public Parity Parity { get; set; } = Parity.None;
    public StopBits StopBits { get; set; } = StopBits.One;
    public int ReadTimeout { get; set; } = 1000;
    public int WriteTimeout { get; set; } = 1000;
    public List<SlaveDeviceConfig> Slaves { get; set; } = [];
}

public abstract class SlaveDeviceConfig
{
    public string DeviceName { get; set; } = string.Empty;
    public byte SlaveId { get; set; } = 1;
    public int PollInterval { get; set; } = 1000;
}

public class TargetConfig
{
    public string Ip { get; set; } = string.Empty;
    public int Port { get; set; } = 9000;
}