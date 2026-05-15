// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 05月15日 14:05
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Slaves\ModbusRtuSlave.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System.IO.Ports;
using Modbus.Device;
using ModbusApp.Models;

namespace ModbusApp.Slaves;

public class ModbusRtuSlave(RtuSlaveInfo slaveInfo) : ModbusSlave(slaveInfo.Name)
{
    private SerialPort CreateAndOpenPort()
    {
        var port = new SerialPort
        {
            PortName = slaveInfo.PortName,
            BaudRate = slaveInfo.BaudRate,
            DataBits = slaveInfo.DataBits,
            Parity = slaveInfo.Parity,
            StopBits = slaveInfo.StopBits,
            ReadTimeout = slaveInfo.ReadTimeout,
            WriteTimeout = slaveInfo.WriteTimeout
        };

        port.Open();
        return port;
    }
    
    public override async Task<ushort[]> ReadHoldingRegistersAsync(ushort start, ushort count)
    {
        using var port = CreateAndOpenPort();
        var master = ModbusSerialMaster.CreateRtu(port);
        return await master.ReadHoldingRegistersAsync(
            slaveInfo.SlaveId,
            start,
            count
        );
    }
    
    public override async Task<ushort[]> ReadInputRegistersAsync(ushort start, ushort count)
    {
        using var port = CreateAndOpenPort();
        var master = ModbusSerialMaster.CreateRtu(port);
        return await master.ReadInputRegistersAsync(
            slaveInfo.SlaveId,
            start,
            count
        );
    }
    
    public override async Task WriteSingleRegisterAsync(ushort address, ushort value)
    {
        using var port = CreateAndOpenPort();
        var master = ModbusSerialMaster.CreateRtu(port);
        await master.WriteSingleRegisterAsync(slaveInfo.SlaveId, address, value);
    }
    
    public override async Task WriteMultipleRegistersAsync(ushort startAddress, ushort[] values)
    {
        using var port = CreateAndOpenPort();
        var master = ModbusSerialMaster.CreateRtu(port);
        await master.WriteMultipleRegistersAsync(slaveInfo.SlaveId, startAddress, values);
    }
    
    public override async Task WriteSingleCoilAsync(ushort coilAddress, bool value)
    {
        using var port = CreateAndOpenPort();
        var master = ModbusSerialMaster.CreateRtu(port);
        await master.WriteSingleCoilAsync(slaveInfo.SlaveId, coilAddress, value);
    }
    
    public override async Task WriteMultipleCoilsAsync(ushort startAddress, bool[] coils)
    {    
        using var port = CreateAndOpenPort();
        var master = ModbusSerialMaster.CreateRtu(port);
        await master.WriteMultipleCoilsAsync(slaveInfo.SlaveId, startAddress, coils);
    }
}