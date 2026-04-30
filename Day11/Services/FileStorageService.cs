// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 04月29日 15:04
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\Day11\Services\FileStorageService.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using Day11.Models;

namespace Day11.Services;

public class FileStorageService(string filePath)
{
    public async Task SaveAllLineAsync(List<string> lines)
    {
        await using var sw = new StreamWriter(filePath);
        foreach (var line in lines)
        {
            await sw.WriteLineAsync(line);
        }
    }

    public async Task<List<string>> LoadAllLinesAsync()
    {
        var list = new List<string>();
        if (!File.Exists(filePath))
            return list;
        
        try
        {
            using var sr = new StreamReader(filePath);
            while (await sr.ReadLineAsync() is {} line)
            {
                list.Add(line);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return list;
    }
}