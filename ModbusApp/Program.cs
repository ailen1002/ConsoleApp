// See https://aka.ms/new-console-template for more information

using ModbusApp.Devices;
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
        
        DeviceRegistry devices;

        try
        {
            devices = await DeviceRegistry.CreateAsync();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "设备初始化失败");
            return;
        }
        
        while (true)
        {
            await devices.SwitchInputBoard.ReadStateAsync();
            await devices.AcInputBoard.ReadStateAsync();
            await devices.DcInputBoard.ReadStateAsync();
            var a = devices.SwitchInputBoard[11];
            await devices.VoltageBoard.ReadStateAsync();
            var b = devices.VoltageBoard[1];
            Log.Information("a: {@Values}", a);
            Log.Information("b: {@Values}", b);
            await devices.OutputBoard.ApSwitch.On();
            await Task.Delay(1000);
            await devices.OutputBoard.ApSwitch.Off();
            await Task.Delay(1000);
            await devices.ResBoard.CloseEvenChannels();
            await Task.Delay(1000);
            await devices.ResBoard.OpenAll();
            await Task.Delay(1000);
            await devices.Controller.Forward.On();
            await Task.Delay(5000);
            
            await devices.CommBoard.SetTxCommand("0xE5, 0xFE, 0x11, 0x03, 0x00, 0x61, 0x00", 15, "系统停止命令");
            await Task.Delay(5000);
            await devices.Controller.Forward.Off();
        }
    }
}