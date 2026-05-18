// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 05月15日 16:05
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Services\PollingService.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using ModbusApp.Slaves;

namespace ModbusApp.Services;

public class PollingService(ModbusSlave slave, int intervalMs)
{
    public async Task StartAsync()
    {
        while (true)
        {
            try
            {
                var data = await slave.ReadHoldingRegistersAsync(0, 10);
                Console.WriteLine(
                    $"[{DateTime.Now:HH:mm:ss}] [{slave.Name}] {string.Join(", ", data)}"
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{slave.Name}] Poll Error: {ex.Message}");
            }

            await Task.Delay(intervalMs);
        }
    }
}