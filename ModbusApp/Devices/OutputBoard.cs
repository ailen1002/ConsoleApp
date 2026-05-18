// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 05月15日 16:05
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Devices\OutputBoard.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using ModbusApp.Slaves;

namespace ModbusApp.Devices;

public class OutputBoard(ModbusSlave slave)
{
    public string Name => slave.Name;

    public Task WriteSingleRegisterAsync(ushort address, ushort value)
        => slave.WriteSingleRegisterAsync(address, value);

    public Task<ushort[]> ReadRegistersAsync(ushort start, ushort count)
        => slave.ReadHoldingRegistersAsync(start, count);
}