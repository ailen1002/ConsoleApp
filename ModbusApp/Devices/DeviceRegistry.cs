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

    private DeviceRegistry(
        InputBoard switchInputBoard,
        InputBoard acInputBoard,
        InputBoard dcInputBoard,
        OutputBoard outputBoard,
        ResBoard resBoard,
        VoltageBoard voltageBoard,
        CommBoard commBoard,
        Controller controller)
    {
        SwitchInputBoard = switchInputBoard;
        AcInputBoard = acInputBoard;
        DcInputBoard = dcInputBoard;
        OutputBoard = outputBoard;
        ResBoard = resBoard;
        VoltageBoard = voltageBoard;
        CommBoard = commBoard;
        Controller = controller;
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

            new Controller(
                registry.GetMaster(DeviceNames.CommPort),
                "主控制器")
        );
    }
}