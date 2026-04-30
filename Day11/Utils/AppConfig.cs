// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 04月30日 11:04
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\Day11\Utils\AppConfig.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using Day11.Strategies;

namespace Day11.Utils;

public class AppConfig
{
    public static AppConfig Instance { get; } = new();
    private AppConfig() { }
    public static string DataFilePath => "contactData.txt";
    public static string AppName => "高级通讯录";
    public static ISaveStrategy SaveStrategy => new JsonSaveStrategy();
    // public static ISaveStrategy SaveStrategy => new TextSaveStrategy();
}