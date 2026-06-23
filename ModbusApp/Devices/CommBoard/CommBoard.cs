// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 06月16日 09:06
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\ModbusApp\Devices\CommBoard\CommBoard.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System.Net.Sockets;
using ModbusApp.Models;
using ModbusApp.Services.Channel;

namespace ModbusApp.Devices.CommBoard;

public class CommBoard
{
    private readonly TcpClient _client;
    private readonly NetworkStream _stream;
    private readonly SemaphoreSlim _lock = new(1, 1);
    
    public CommBoard(IEnumerable<IChannel> channels, string channelName)
    {
        var channel = channels.First(c => c.Name == channelName);

        _client = channel.Client;
        _stream = _client.GetStream();
    }
    
    public async Task<CommandResult> SetTxCommand(byte[] data, int expectedLength, string commandName = "")
    {
        await _lock.WaitAsync();
        try
        {
            if (!_client.Connected)
                throw new InvalidOperationException("TCP连接已断开");

            await ClearBufferAsync();
#if DEBUG
            Console.WriteLine();
            Console.WriteLine($"[{commandName}] TX:");
            PrintHex(data);
#endif
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(2));

            await _stream.WriteAsync(data, cts.Token);
            await _stream.FlushAsync(cts.Token);

            var response = await ReadExactAsync(expectedLength, cts.Token);
            var success = VerifyChecksum(response);
#if DEBUG
            Console.WriteLine($"[{commandName}] RX:");
            PrintHex(response);

            Console.WriteLine(
                $"Checksum={(success ? "OK" : "NG")}");
#endif
            return new CommandResult
            {
                CommandName = commandName,
                Success = success,
                Response = response,
                ActualLength = response.Length
            };
        }
        catch(OperationCanceledException)
        {
#if DEBUG
            Console.WriteLine($"[{commandName}] Timeout");
#endif
            return new CommandResult
            {
                CommandName = commandName,
                Success = false,
                Response = [],
                ActualLength = 0
            };
        }
        catch (Exception ex)
        {
#if DEBUG
            Console.WriteLine(
                $"[{commandName}] Exception: {ex.Message}");
#endif

            return new CommandResult
            {
                CommandName = commandName,
                Success = false,
                Response = [],
                ActualLength = 0
            };
        }
        finally
        {
            _lock.Release();
        }
    }
    
    private async Task ClearBufferAsync()
    {
        var buffer = new byte[1024];

        while (_stream.DataAvailable)
        {
            if (await _stream.ReadAsync(buffer) == 0)
                break;
        }
    }
    
    private async Task<byte[]> ReadExactAsync(
        int expectedLength,
        CancellationToken token)
    {
        var buffer = new byte[expectedLength];

        var totalRead = 0;

        while (totalRead < expectedLength)
        {
            var read = await _stream.ReadAsync(
                buffer.AsMemory(
                    totalRead,
                    expectedLength - totalRead),
                token);

            if (read == 0)
            {
                throw new IOException(
                    "远程连接已关闭");
            }

            totalRead += read;
        }

        return buffer;
    }
    
    private static bool VerifyChecksum(
        ReadOnlySpan<byte> data)
    {
        if (data.Length < 2)
            return false;

        byte checksum = 0;

        foreach (var b in data[..^1])
        {
            checksum ^= b;
        }

        return checksum == data[^1];
    }

#if DEBUG
    private static void PrintHex(
        byte[] data,
        int bytesPerLine = 16)
    {
        for (var i = 0; i < data.Length; i += bytesPerLine)
        {
            var length = Math.Min(
                bytesPerLine,
                data.Length - i);

            Console.WriteLine(
                BitConverter.ToString(
                    data,
                    i,
                    length));
        }
    }
#endif
}