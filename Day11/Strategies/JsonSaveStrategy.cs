// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 04月30日 13:04
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\Day11\Strategies\JsonSaveStrategy.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using System.Text.Json;
using Day11.Models;

namespace Day11.Strategies;

public class JsonSaveStrategy : ISaveStrategy
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };
    
    public async Task SaveAsync(string path, List<Contact> contacts)
    {
        var json = JsonSerializer.Serialize(contacts, JsonOptions);
        await File.WriteAllTextAsync(path, json);
    }

    public async Task<List<Contact>> LoadAsync(string path)
    {
        if (!File.Exists(path)) return [];
        var json = await File.ReadAllTextAsync(path);
        return JsonSerializer.Deserialize<List<Contact>>(json) ?? [];
    }
}