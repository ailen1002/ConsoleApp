// See https://aka.ms/new-console-template for more information

using System.IO.Ports;
using ModbusApp.Models;
using ModbusApp.Services;
using ModbusApp.Slaves;

namespace ModbusApp;

internal abstract class Program
{
    private static async Task Main()
    {
        var hub = new ModbusHub();

        var a = new TcpSlaveInfo()
        {
            Name = "数字量输入板",
            Ip = "192.168.1.105"
        }; 
        
        var b = new TcpSlaveInfo()
        {
            Name = "数字量输出板",
            Ip = "192.168.1.151"
        }; 
            
        hub.Register(new ModbusTcpSlave(a));

        hub.Register(new ModbusTcpSlave(b));
        
        var rtuPort = new RtuSlaveInfo
        {
            Name = "COM2",
            PortName = "COM2",
            BaudRate = 19200,
            DataBits = 8,
            Parity = Parity.None,
            StopBits = StopBits.One,
            ReadTimeout = 500,
            WriteTimeout = 500,
            SlaveId = 1,
        };
        
        hub.Register(new ModbusRtuSlave(rtuPort));

        // 启动轮询
        _ = new PollingService(hub.Get("数字量输入板")!, 1000).StartAsync();
        _ = new PollingService(hub.Get("数字量输出板")!, 1000).StartAsync();
        _ = new PollingService(hub.Get("COM2")!, 500).StartAsync();

        // ✅ 直接拿设备
        var outputBoard = hub.GetOutputBoard();

        // ✅ 这就是你要的写法
        await outputBoard.WriteSingleRegisterAsync(2, 999);

        var data = await outputBoard.ReadRegistersAsync(0, 10); 
        Console.WriteLine(string.Join(", ", data));

        await Task.Delay(Timeout.Infinite);
    }
}