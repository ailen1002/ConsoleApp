// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 06月24日 14:06
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Devices\IoDevices.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

namespace ModbusApp.Devices;

public sealed class IoDevices
{
    public required InputBoard SwitchInputBoard { get; init; }

    public required InputBoard AcInputBoard { get; init; }

    public required InputBoard DcInputBoard { get; init; }

    public required OutputBoard OutputBoard { get; init; }

    public required VoltageBoard VoltageBoard { get; init; }

    public required ResBoard ResBoard { get; init; }
}