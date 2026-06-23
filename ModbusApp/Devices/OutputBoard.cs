// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 06月15日 08:06
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Devices\OutputBoard\OutputBoard.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using Modbus.Device;
using ModbusApp.Services.Channel;

namespace ModbusApp.Devices;

public class OutputBoard
{
    private readonly IModbusMaster _master;
    public OutputPoint ApSwitch { get; }
    public OutputPoint TestSwitch { get; }
    public OutputPoint SnowSwitch { get; }
    public OutputPoint SilentSwitch { get; }
    public OutputPoint Drm1Switch { get; }
    public OutputPoint Drm2Switch { get; }
    public OutputPoint ForcedStopSwitch { get; }
    public OutputPoint NumCompSwitch { get; }
    public OutputPoint Boot { get; }
    public OutputPoint HicPower { get; }
    public OutputPoint MiconResetSwitch { get; }
    public OutputPoint HicSwitch { get; }
    public OutputPoint HighPressureSwitch { get; }
    public OutputPoint CompTripSwitch { get; }
    public OutputPoint ConstantSpeedMotor { get; }
    public OutputPoint AcVoltageDetectionSwitch { get; }
    
    public OutputBoard(IEnumerable<IModbusChannel> channels, string channelName)
    {
        _master = channels.First(c => c.Name == channelName).Master;

        ApSwitch = new OutputPoint(WriteAsync, 0);
        TestSwitch = new OutputPoint(WriteAsync, 1);
        SnowSwitch = new OutputPoint(WriteAsync, 2);
        SilentSwitch = new OutputPoint(WriteAsync, 3);
        Drm1Switch = new OutputPoint(WriteAsync, 4);
        Drm2Switch = new OutputPoint(WriteAsync, 5);
        ForcedStopSwitch = new OutputPoint(WriteAsync, 6);
        NumCompSwitch = new OutputPoint(WriteAsync, 7);
        Boot = new OutputPoint(WriteAsync, 8);
        HicPower = new OutputPoint(WriteAsync, 9);
        MiconResetSwitch = new OutputPoint(WriteAsync, 10);
        HicSwitch = new OutputPoint(WriteAsync, 11);
        HighPressureSwitch = new OutputPoint(WriteAsync, 12);
        CompTripSwitch = new OutputPoint(WriteAsync, 13);
        ConstantSpeedMotor = new OutputPoint(WriteAsync, 14);
        AcVoltageDetectionSwitch = new OutputPoint(WriteAsync, 15);
    }
    
    private Task WriteAsync(ushort address, ushort value)
    {
        return _master.WriteSingleRegisterAsync(1, address, value);
    }
}