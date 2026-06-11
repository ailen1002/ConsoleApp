// See https://aka.ms/new-console-template for more information

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
            .Enrich.WithThreadId()
            .Enrich.FromLogContext()
            .WriteTo.Console(
                theme: AnsiConsoleTheme.Literate,
                outputTemplate:
                "[{Timestamp:HH:mm:ss.fff}] " +
                "[{Level:u3}] " +
                "[T:{ThreadId}] " +
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

        foreach (var ch in channels)
        {
            Log.Information($"{ch.Name} - {ch.Type}");
        }

        var inputBoard = channels.First(c => c.Name == "数字量输入板").Master;
        var outputBoard = channels.First(c => c.Name == "数字量输出板").Master;
        var sp2 = channels.First(c => c.Name == "COM2").Master;

        while (true)
        {
            var a = await inputBoard.ReadHoldingRegistersAsync(1, 0,10);
            var b = await outputBoard.ReadHoldingRegistersAsync(1, 0,10);
            var c = await sp2.ReadHoldingRegistersAsync(1, 0,10);
            var d = await sp2.ReadHoldingRegistersAsync(2, 0,10);
            
            Log.Information("a: [{Values}]", string.Join(", ", a));
            Log.Information("b: [{Values}]", string.Join(", ", b));
            Log.Information("c: [{Values}]", string.Join(", ", c));
            Log.Information("d: [{Values}]", string.Join(", ", d));
            // Log.Information("a: {@Values}", a);
            // Log.Information("b: {@Values}", b);
            // Log.Information("c: {@Values}", c);
            // Log.Information("d: {@Values}", d);
            
            await Task.Delay(1000);
        }
    }
}