// See https://aka.ms/new-console-template for more information

using ModbusApp.Devices.InputBoard;
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
        var acInputClient = new InputBoard(channels, "继电器检测板卡");
        //var outputBoard = channels.First(c => c.Name == "数字量输出板卡").Master;
        //var sp2 = channels.First(c => c.Name == "COM2").Master;

        while (true)
        {
            await switchInputBoard.ReadStateAsync();
            await acInputClient.ReadStateAsync();
            //var b = await outputBoard.ReadHoldingRegistersAsync(1, 0,10);
            //var c = await sp2.ReadHoldingRegistersAsync(1, 0,10);
            //var d = await sp2.ReadHoldingRegistersAsync(2, 0,10);
            
            //Log.Information("b: [{Values}]", string.Join(", ", b));
            //Log.Information("c: [{Values}]", string.Join(", ", c));
            //Log.Information("d: [{Values}]", string.Join(", ", d));
            // Log.Information("a: {@Values}", a);
            // Log.Information("b: {@Values}", b);
            // Log.Information("c: {@Values}", c);
            // Log.Information("d: {@Values}", d);
            
            await Task.Delay(1000);
        }
    }
}