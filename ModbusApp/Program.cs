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
            await devices.Io.SwitchInputBoard.ReadStateAsync();
            await Task.Delay(1000);
            await devices.Io.AcInputBoard.ReadStateAsync();
            await Task.Delay(1000);
            await devices.Io.DcInputBoard.ReadStateAsync();
            await Task.Delay(1000);
            var a = devices.Io.SwitchInputBoard[11];
            await devices.Io.VoltageBoard.ReadStateAsync();
            await Task.Delay(1000);
            var b = devices.Io.VoltageBoard[1];
            Log.Information("a: {@Values}", a);
            Log.Information("b: {@Values}", b);
            await devices.Com.AcVoltmeter.ReadVoltageAsync();
            var c = devices.Com.AcVoltmeter.Volt;
            Log.Information("c: {@Values}", c);
            await Task.Delay(1000);
            await devices.Com.DcVoltmeter.ReadVoltageAsync();
            var d = devices.Com.AcVoltmeter.Volt;
            Log.Information("d: {@Values}", d);
            await Task.Delay(1000);
            await devices.Com.Fan1Ammeter.ReadCurrentAsync();
            var e = devices.Com.Fan1Ammeter.Current[0];
            Log.Information("e: {@Values}", e);
            await Task.Delay(1000);
            await devices.Com.Fan2Ammeter.ReadCurrentAsync();
            var f = devices.Com.Fan2Ammeter.Current[0];
            Log.Information("f: {@Values}", f);
            await Task.Delay(1000);
            await devices.Io.OutputBoard.ApSwitch.On();
            await Task.Delay(1000);
            await devices.Io.OutputBoard.ApSwitch.Off();
            await Task.Delay(1000);
            await devices.Io.ResBoard.CloseEvenChannels();
            await Task.Delay(1000);
            await devices.Io.ResBoard.OpenAll();
            await Task.Delay(1000);
            await devices.Com.Controller.Forward.On();
            await Task.Delay(5000);
            await devices.Comm.CommBoard.SetTxCommand("0xE5, 0xFE, 0x11, 0x03, 0x00, 0x61, 0x00", 15, "系统停止命令");
            await Task.Delay(5000);
            await devices.Com.Controller.Forward.Off();
        }
    }
}