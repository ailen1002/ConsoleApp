// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 04月29日 15:04
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\Day11\Utils\ConsoleHelper.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

namespace Day11.Utils;

public class ConsoleHelper
{
    public static string ReadInput()
    {
        return Console.ReadLine()?.Trim() ?? string.Empty;
    }

    public static bool TryReadNumber(out int num)
    {
        var input = ReadInput();
        return int.TryParse(input, out num);
    }

    public static void PrintLine(string msg)
    {
        Console.WriteLine(msg);
    }
}