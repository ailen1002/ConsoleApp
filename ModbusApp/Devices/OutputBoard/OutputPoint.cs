// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 06月15日 11:06
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Devices\OutputBoard\OutputPoint.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using ModbusApp.Services.Channel;

namespace ModbusApp.Devices.OutputBoard;

public sealed class OutputPoint(Func<ushort, ushort, Task> writer, ushort address)
{
    public Task On() => writer(address, 1);

    public Task Off() => writer(address, 0);
}