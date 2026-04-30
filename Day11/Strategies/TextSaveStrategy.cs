// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 04月30日 13:04
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\Day11\Strategies\TextSaveStrategy.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using Day11.Factories;
using Day11.Models;

namespace Day11.Strategies;

public class TextSaveStrategy : ISaveStrategy
{
    public async Task SaveAsync(string path, List<Contact> contacts)
    {
        await using var sw = new StreamWriter(path);
        foreach (var c in contacts)
        {
            await sw.WriteLineAsync($"{c.Name}|{c.Phone}");
        }
    }

    public async Task<List<Contact>> LoadAsync(string path)
    {
        var list = new List<Contact>();
        if (!File.Exists(path)) return list;

        using var sr = new StreamReader(path);
        while (await sr.ReadLineAsync() is { } line)
        {
            var arr = line.Split('|');
            if (arr.Length >= 2)
            {
                list.Add(ContactFactory.CreateContact(arr[0], arr[1]));
            }
        }
        return list;
    }
}