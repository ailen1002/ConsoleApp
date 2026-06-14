// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 06月11日 15:06
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Devices\SwitchInputBoard\SwitchInputBoard.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using ModbusApp.Services.Channel;
using Serilog;

namespace ModbusApp.Devices.InputBoard;

public class InputBoard(IEnumerable<IModbusChannel> channels, string channelName)
{
    private ushort[] _inputs = [];
    
    public ushort this[int index] => Get(index);
    
    private ushort Get(int index)
        => index >= 0 && index < _inputs.Length ? _inputs[index] : (ushort)0;
    
    public async Task ReadStateAsync()
    {
        var switchInputBoard = channels.First(c => c.Name == channelName).Master;
        _inputs = await switchInputBoard.ReadHoldingRegistersAsync(1, 0, 16);
        
        Log.Debug("{BoardName}: {@Values}",channelName, _inputs);
    }
}