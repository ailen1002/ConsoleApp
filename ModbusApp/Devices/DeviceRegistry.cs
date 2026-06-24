// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 06月23日 13:06
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Devices\DeviceRegistry.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using ModbusApp.Services.Channel;

namespace ModbusApp.Devices;

public class DeviceRegistry
{
    public InputBoard SwitchInputBoard { get; }

    public InputBoard AcInputBoard { get; }

    public InputBoard DcInputBoard { get; }

    public OutputBoard OutputBoard { get; }

    public ResBoard ResBoard { get; }

    public VoltageBoard VoltageBoard { get; }

    public CommBoard CommBoard { get; }

    public Controller Controller { get; }
    
    public Voltmeter AcVoltmeter { get; }
    
    public Voltmeter DcVoltmeter { get; }
    
    public Ammeter Fan1Ammeter { get; }
    
    public Ammeter Fan2Ammeter { get; }

    private DeviceRegistry(
        InputBoard switchInputBoard,
        InputBoard acInputBoard,
        InputBoard dcInputBoard,
        OutputBoard outputBoard,
        ResBoard resBoard,
        VoltageBoard voltageBoard,
        CommBoard commBoard,
        Controller controller,
        Voltmeter acVoltmeter,
        Voltmeter dcVoltmeter,
        Ammeter fan1Ammeter,
        Ammeter fan2Ammeter)
    {
        SwitchInputBoard = switchInputBoard;
        AcInputBoard = acInputBoard;
        DcInputBoard = dcInputBoard;
        OutputBoard = outputBoard;
        ResBoard = resBoard;
        VoltageBoard = voltageBoard;
        CommBoard = commBoard;
        Controller = controller;
        AcVoltmeter = acVoltmeter;
        DcVoltmeter = dcVoltmeter;
        Fan1Ammeter = fan1Ammeter;
        Fan2Ammeter = fan2Ammeter;
    }

    public static async Task<DeviceRegistry> CreateAsync()
    {
        var modbusChannels =
            await ModbusChannelFactory.CreateChannelsAsync();

        var tcpChannels =
            await ChannelFactory.CreateChannelAsync();

        var registry = new ChannelRegistry(
            modbusChannels,
            tcpChannels);
        
        var channel = registry.GetMaster(DeviceNames.CommPort);

        return new DeviceRegistry(
            new InputBoard(
                modbusChannels,
                DeviceNames.SwitchInputBoard),

            new InputBoard(
                modbusChannels,
                DeviceNames.AcInputBoard),

            new InputBoard(
                modbusChannels,
                DeviceNames.DcInputBoard),

            new OutputBoard(
                modbusChannels,
                DeviceNames.OutputBoard),

            new ResBoard(
                modbusChannels,
                DeviceNames.ResBoard),

            new VoltageBoard(
                modbusChannels,
                DeviceNames.VoltageBoard),

            new CommBoard(
                tcpChannels,
                DeviceNames.CommCard),
            new Controller(channel, DeviceNames.Controller,1),
            new Voltmeter(channel, DeviceNames.AcVoltmeter,2),
            new Voltmeter(channel, DeviceNames.DcVoltmeter,3),
            new Ammeter(channel, DeviceNames.Fan1Ammeter,4),
            new Ammeter(channel, DeviceNames.Fan2Ammeter,5)
        );
    }
}