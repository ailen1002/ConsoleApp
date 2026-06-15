// See https://aka.ms/new-console-template for more information

using ModbusApp.Devices.InputBoard;
using ModbusApp.Devices.OutputBoard;
using ModbusApp.Devices.ResBoard;
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
        
        try
        {
            channels = await ModbusChannelFactory.CreateChannelsAsync();
        }
        catch (Exception ex)
        {
            Log.Information($"Modbus 初始化失败: {ex.Message}");
            return;
        }

        var switchInputBoard = new InputBoard(channels, "数字量输入板卡");
        var acInputBoard = new InputBoard(channels, "继电器检测板卡");
        var dcInputBoard = new InputBoard(channels, "膨胀阀检测板卡");
        var outputBoard = new OutputBoard(channels, "数字量输出板卡");
        var resBoard = new ResBoard(channels, "电阻板检测板卡");

        while (true)
        {
            var task1 = switchInputBoard.ReadStateAsync();
            var task2 = switchInputBoard.ReadStateAsync();
            await Task.WhenAll(task1, task2);
            
            await acInputBoard.ReadStateAsync();
            await dcInputBoard.ReadStateAsync();
            var a = switchInputBoard[11];
            await outputBoard.ApSwitch.On();

            await Task.Delay(1000);
            
            await outputBoard.ApSwitch.Off();

            Log.Information("a: {@Values}", a);
            
            await Task.Delay(1000);
        }
    }
}