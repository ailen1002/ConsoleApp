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
    public required IoDevices Io { get; init; }

    public required CommDevices Comm { get; init; }

    public required ComDevices Com { get; init; }

    public static async Task<DeviceRegistry> CreateAsync()
    {
        var modbusChannels =
            await ModbusChannelFactory.CreateChannelsAsync();

        var tcpChannels =
            await ChannelFactory.CreateChannelAsync();

        var registry = new ChannelRegistry(
            modbusChannels,
            tcpChannels);
        
        var com2 = registry.GetMaster(DeviceNames.CommPort);

        return new DeviceRegistry
        {
            Io = new IoDevices
            {
                SwitchInputBoard = new InputBoard(modbusChannels, DeviceNames.SwitchInputBoard),

                AcInputBoard = new InputBoard(modbusChannels, DeviceNames.AcInputBoard),

                DcInputBoard = new InputBoard(modbusChannels, DeviceNames.DcInputBoard),

                OutputBoard = new OutputBoard(modbusChannels, DeviceNames.OutputBoard),

                ResBoard = new ResBoard(modbusChannels, DeviceNames.ResBoard),

                VoltageBoard = new VoltageBoard(modbusChannels, DeviceNames.VoltageBoard)
            },
            Comm = new CommDevices
            {
                CommBoard = new CommBoard(tcpChannels, DeviceNames.CommCard)
            },
            Com = new ComDevices
            {
                Controller = new Controller(com2, DeviceNames.Controller, 1),

                AcVoltmeter = new Voltmeter(com2, DeviceNames.AcVoltmeter, 2),

                DcVoltmeter = new Voltmeter(com2, DeviceNames.DcVoltmeter, 3),

                Fan1Ammeter = new Ammeter(com2, DeviceNames.Fan1Ammeter, 4),

                Fan2Ammeter = new Ammeter(com2, DeviceNames.Fan2Ammeter, 5)
            }
        };
    }
}