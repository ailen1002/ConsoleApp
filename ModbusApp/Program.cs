// See https://aka.ms/new-console-template for more information

using ModbusApp.Devices.CommBoard;
using ModbusApp.Devices.Controller;
using ModbusApp.Devices.InputBoard;
using ModbusApp.Devices.OutputBoard;
using ModbusApp.Devices.ResBoard;
using ModbusApp.Devices.VoltageBoard;
using ModbusApp.Services.Channel;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace ModbusApp;

internal abstract class Program
{
    private static async Task Main()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .WriteTo.Console(
                theme: AnsiConsoleTheme.Literate,
                outputTemplate:
                "[{Timestamp:HH:mm:ss.fff}] " +
                "[{Level:u3}] " +
                "{Message:lj}{NewLine}{Exception}")
            .CreateLogger();
        
        IReadOnlyList<IModbusChannel> channels;
        IReadOnlyList<IChannel> tcpChannels;
        
        try
        {
            channels = await ModbusChannelFactory.CreateChannelsAsync();
            tcpChannels = await ChannelFactory.CreateChannelAsync();
        }
        catch (Exception ex)
        {
            Log.Information($"Modbus 初始化失败: {ex.Message}");
            return;
        }
        var sp2 = channels.First(c => c.Name == "COM2").Master;
        
        var switchInputBoard = new InputBoard(channels, "数字量输入板卡");
        var acInputBoard = new InputBoard(channels, "继电器检测板卡");
        var dcInputBoard = new InputBoard(channels, "膨胀阀检测板卡");
        var outputBoard = new OutputBoard(channels, "数字量输出板卡");
        var resBoard = new ResBoard(channels, "电阻板检测板卡");
        var voltageBoard = new VoltageBoard(channels, "电压板检测板卡");
        var commBoard = new CommBoard(tcpChannels, "测试设备通讯卡");
        var controller = new Controller(sp2,"主控制器");
        while (true)
        {
            await switchInputBoard.ReadStateAsync();
            await acInputBoard.ReadStateAsync();
            await dcInputBoard.ReadStateAsync();
            var a = switchInputBoard[11];
            await voltageBoard.ReadStateAsync();
            var b = voltageBoard[1];
            Log.Information("a: {@Values}", a);
            Log.Information("b: {@Values}", b);
            await outputBoard.ApSwitch.On();
            await Task.Delay(1000);
            await outputBoard.ApSwitch.Off();
            await Task.Delay(1000);
            await resBoard.CloseEvenChannels();
            await Task.Delay(1000);
            await resBoard.OpenAll();
            await Task.Delay(1000);
            await controller.Forward.On();
            await Task.Delay(5000);
            
            await commBoard.SetTxCommand("0xE5, 0xFE, 0x11, 0x03, 0x00, 0x61, 0x00", 15, "系统停止命令");
            await Task.Delay(5000);
            await controller.Forward.Off();
        }
    }
}