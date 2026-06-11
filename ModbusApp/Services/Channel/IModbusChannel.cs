// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 06月11日 12:06
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Services\Channel\IModbusChannel.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using Modbus.Device;

namespace ModbusApp.Services.Channel;

public interface IModbusChannel : IDisposable
{
    string Name { get; }
    string Type { get; }   // "TCP" / "RTU"
    IModbusMaster Master { get; }
}