// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 04月30日 11:04
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\Day11\Utils\AppLogger.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

namespace Day11.Utils;

public class AppLogger
{
    public static AppLogger Instance { get; } = new();
    private AppLogger(){ }
    public static void Log(string msg)
    {
        Console.WriteLine("【日志】: " + msg);
    }

    public static void Error(string msg)
    {
        Console.WriteLine("【错误】: " + msg);
    }
}