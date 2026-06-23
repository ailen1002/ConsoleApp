// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 06月16日 15:06
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Devices\Controller\Controller.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using Modbus.Device;
using Serilog;

namespace ModbusApp.Devices;

public class Controller
{
    private readonly IModbusMaster _master;
    private readonly SemaphoreSlim _lock = new(1, 1);
    private readonly string _channelName;
    private const byte SlaveId = 1;
    private volatile ushort[] _inputs = [];
    
    public OutputPoint Forward { get; }
    public OutputPoint Reverse { get; }
    public OutputPoint VoltageSwitch { get; }
    public OutputPoint RemoteControlA { get; }
    public OutputPoint RemoteControlB { get; }
    public OutputPoint CapacitorDischargeA { get; }
    public OutputPoint CapacitorDischargeB { get; }
    public OutputPoint CapacitorDischargeC { get; }
    
    public ushort this[int index] => Get(index);
    
    private ushort Get(int index)
        => index >= 0 && index < _inputs.Length ? _inputs[index] : (ushort)0;
    
    public Controller(IModbusMaster channel, string channelName)
    {
        _master = channel;
        _channelName = channelName;

        Forward = new OutputPoint(WriteAsync, 0);
        Reverse = new OutputPoint(WriteAsync, 1);
        VoltageSwitch = new OutputPoint(WriteAsync, 2);
        RemoteControlA = new OutputPoint(WriteAsync, 3);
        RemoteControlB = new OutputPoint(WriteAsync, 4);
        CapacitorDischargeA = new OutputPoint(WriteAsync, 5);
        CapacitorDischargeB = new OutputPoint(WriteAsync, 6);
        CapacitorDischargeC = new OutputPoint(WriteAsync, 7);
    }
    
    public async Task ReadStateAsync()
    {
        await _lock.WaitAsync();

        try
        {
            _inputs = await _master.ReadHoldingRegistersAsync(SlaveId, 0, 2);
            
            Log.Debug("{BoardName}: {@Values}",_channelName, _inputs);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "{BoardName} 读取状态失败", _channelName);

            throw;
        }
        finally
        {
            _lock.Release();
        }
    }
    
    private async Task WriteAsync(ushort registerAddress, ushort value)
    {
        await _lock.WaitAsync();
        
        try
        {
            await _master.WriteSingleRegisterAsync(SlaveId, registerAddress, value);
            
            if (registerAddress < _inputs.Length)
            {
                _inputs[registerAddress] = value;
            }
            
            Log.Debug("{BoardName} 写入 Addr={Address}, Value={Value}", _channelName, registerAddress, value);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "{BoardName} 写入失败 Addr={Address}, Value={Value}", _channelName,registerAddress,value);
            throw;
        }
        finally
        {
            _lock.Release();
        }
    }
}