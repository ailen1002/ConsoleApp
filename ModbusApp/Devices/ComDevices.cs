// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 06月24日 14:06
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Devices\ComDevices.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

namespace ModbusApp.Devices;

public class ComDevices
{
    public required Controller Controller { get; init; }

    public required Voltmeter AcVoltmeter { get; init; }

    public required Voltmeter DcVoltmeter { get; init; }

    public required Ammeter Fan1Ammeter { get; init; }

    public required Ammeter Fan2Ammeter { get; init; }
}