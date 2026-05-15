// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 05月15日 13:05
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Slaves\ModbusTcpSlave.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System.Net.Sockets;
using Modbus.Device;
using ModbusApp.Models;

namespace ModbusApp.Slaves;

public class ModbusTcpSlave(TcpSlaveInfo slaveInfo) : ModbusSlave(slaveInfo.Name)
{
    public override async Task<ushort[]> ReadHoldingRegistersAsync(ushort start, ushort count)
    {
        using var tcp = new TcpClient();
        await tcp.ConnectAsync(slaveInfo.Ip, slaveInfo.Port);

        var master = ModbusIpMaster.CreateIp(tcp);
        return await master.ReadHoldingRegistersAsync(
            slaveInfo.SlaveId,
            start,
            count
        );
    }
    
    public override async Task<ushort[]> ReadInputRegistersAsync(ushort start, ushort count)
    {
        using var tcp = new TcpClient();
        await tcp.ConnectAsync(slaveInfo.Ip, slaveInfo.Port);

        var master = ModbusIpMaster.CreateIp(tcp);
        return await master.ReadInputRegistersAsync(
            slaveInfo.SlaveId,
            start,
            count
        );
    }
    
    public override async Task WriteSingleRegisterAsync(ushort address, ushort value)
    {
        using var tcp = new TcpClient();
        await tcp.ConnectAsync(slaveInfo.Ip, slaveInfo.Port);

        var master = ModbusIpMaster.CreateIp(tcp);
        await master.WriteSingleRegisterAsync(slaveInfo.SlaveId, address, value);
    }
    
    public override async Task WriteMultipleRegistersAsync(ushort startAddress, ushort[] values)
    {
        using var tcp = new TcpClient();
        await tcp.ConnectAsync(slaveInfo.Ip, slaveInfo.Port);

        var master = ModbusIpMaster.CreateIp(tcp);
        await master.WriteMultipleRegistersAsync(slaveInfo.SlaveId, startAddress, values);
    }
    
    public override async Task WriteSingleCoilAsync(ushort coilAddress, bool value)
    {
        using var tcp = new TcpClient();
        await tcp.ConnectAsync(slaveInfo.Ip, slaveInfo.Port);

        var master = ModbusIpMaster.CreateIp(tcp);
        await master.WriteSingleCoilAsync(slaveInfo.SlaveId, coilAddress, value);
    }
    
    public override async Task WriteMultipleCoilsAsync(ushort startAddress, bool[] coils)
    {
        using var tcp = new TcpClient();
        await tcp.ConnectAsync(slaveInfo.Ip, slaveInfo.Port);

        var master = ModbusIpMaster.CreateIp(tcp);
        await master.WriteMultipleCoilsAsync(slaveInfo.SlaveId, startAddress, coils);
    }
}   