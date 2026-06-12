// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 06月12日 12:06
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Devices\AcInputBoard\AcInputBoard.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using ModbusApp.Services.Channel;
using Serilog;

namespace ModbusApp.Devices.AcInputBoard;

public class AcInputBoard(IEnumerable<IModbusChannel> channels)
{
    public ushort Di1 => Get(0);
    public ushort Di2 => Get(1);
    public ushort Di3 => Get(2);
    public ushort Di4 => Get(3);
    public ushort Di5 => Get(4);
    public ushort Di6 => Get(5);
    public ushort Di7 => Get(6);
    public ushort Di8 => Get(7);
    public ushort Di9 => Get(8);
    public ushort Di10 => Get(9);
    public ushort Di11 => Get(10);
    public ushort Di12 => Get(11);
    public ushort Di13 => Get(12);
    public ushort Di14 => Get(13);
    public ushort Di15 => Get(14);
    public ushort Di16 => Get(15);
    
    private ushort[] _inputs = [];
    
    private ushort Get(int index)
        => index >= 0 && index < _inputs.Length ? _inputs[index] : (ushort)0;
    
    public async Task ReadStateAsync()
    {
        var acInputBoard = channels.First(c => c.Name == "继电器检测板卡").Master;
        _inputs = await acInputBoard.ReadHoldingRegistersAsync(1, 0, 16);
        
        Log.Debug("继电器检测板卡: {@Values}", _inputs);
    }
}