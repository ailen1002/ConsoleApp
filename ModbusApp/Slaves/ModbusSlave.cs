// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 05月15日 13:05
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Slaves\ModbusSlave.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

namespace ModbusApp.Slaves;

public abstract class ModbusSlave(string name)
{
    public string Name { get; set; } = name;
    public abstract Task<ushort[]> ReadHoldingRegistersAsync(ushort startAddress, ushort quantity);
    public abstract Task<ushort[]> ReadInputRegistersAsync(ushort startAddress, ushort quantity);
    public abstract Task WriteSingleCoilAsync(ushort coilAddress, bool value);
    public abstract Task WriteMultipleCoilsAsync(ushort startCoilAddress, bool[] values);
    public abstract Task WriteSingleRegisterAsync(ushort startAddress, ushort value);
    public abstract Task WriteMultipleRegistersAsync(ushort startAddress, ushort[] values);
}