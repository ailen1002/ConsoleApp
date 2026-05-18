// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 05月15日 16:05
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\ModbusHub.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using ModbusApp.Devices;
using ModbusApp.Slaves;

namespace ModbusApp;

public class ModbusHub
{
    private readonly Dictionary<string, ModbusSlave> _slaves = new();

    public void Register(ModbusSlave slave)
        => _slaves[slave.Name] = slave;

    public OutputBoard GetOutputBoard()
        => new OutputBoard(_slaves["数字量输出板"]);

    public ModbusSlave? Get(string name)
        => _slaves.GetValueOrDefault(name); 
}