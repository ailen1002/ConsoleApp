// See https://aka.ms/new-console-template for more information

using System.IO.Ports;
using System.Net.Sockets;
using Modbus.Device;

namespace ModbusMultiApp;

class Program
{
    static async Task Main(string[] args)
    {
        var slaveA = new ModbusSlaveInfo
        {
            Name = "数字量输入板",
            Ip = "192.168.1.105",
            Port = 502,
            SlaveId = 1
        };
        var slaveB = new ModbusSlaveInfo
        {
            Name = "数字量输出板",
            Ip = "192.168.1.151",
            Port = 502,
            SlaveId = 1
        };
        var taskA = PollTcpAsync(slaveA);
        var taskB = PollTcpAsync(slaveB);
        var rtuTask = PollRtuAsync();

        await Task.WhenAll(taskA, taskB, rtuTask);
    }

    static async Task PollTcpAsync(ModbusSlaveInfo slave)
    {
        while (true)
        {
            TcpClient tcpClient = null;

            try
            {
                tcpClient = new TcpClient();
                await tcpClient.ConnectAsync(slave.Ip, slave.Port);

                var master = ModbusIpMaster.CreateIp(tcpClient);
                
                master.Transport.ReadTimeout = 2000;
                var data = await master.ReadHoldingRegistersAsync(slave.SlaveId, 0, 10);
                
                PrintRegisters("TCP", slave.Name, data);
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"{slave.Name} 网络连接失败: {ex.Message}");
            }
            catch (Exception  ex)
            {
                Console.WriteLine($"{slave.Name} Modbus异常: {ex.Message}");
            }
            finally
            {
                tcpClient?.Dispose();
            }
            
            await Task.Delay(1000); 
        }
    }
    
    static async Task PollRtuAsync()
    {
        SerialPort? serialPort = null;

        try
        {
            serialPort = new SerialPort
            {
                PortName = "COM2",
                BaudRate = 19200,
                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One,
                ReadTimeout = 200,
                WriteTimeout = 200
            };

            serialPort.Open();
            
            var master = ModbusSerialMaster.CreateRtu(serialPort);

            master.Transport.ReadTimeout = 200;
            master.Transport.WriteTimeout = 200;

            Console.WriteLine("\n[RTU-COM2] Serial port opened");

            while (true)
            {
                try
                {
                    var registers = await master.ReadHoldingRegistersAsync(
                        slaveAddress: 1,
                        startAddress: 0,
                        numberOfPoints: 10
                    );

                    PrintRegisters("RTU", "COM2", registers);

                    await Task.Delay(50);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[RTU-COM2] READ ERROR: " + ex.Message);
                    await Task.Delay(500);
                }
                
                await Task.Delay(1000);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("[RTU-COM2] OPEN ERROR: " + ex.Message);
        }
        finally
        {
            serialPort?.Close();
        }
    }

    public class ModbusSlaveInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Ip { get; set; } = string.Empty;
        public int Port { get; set; }
        public byte SlaveId { get; set; }
    }
    
    static void PrintRegisters(string protocol, string deviceName, IEnumerable<ushort> data)
    {
        var time = DateTime.Now.ToString("HH:mm:ss");
        var values = string.Join(", ", data);
        Console.WriteLine($"[{time}] [{protocol}] {deviceName} 寄存器数据: {values}");
    }
}