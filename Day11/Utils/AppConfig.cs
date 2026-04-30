// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 04月30日 11:04
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\Day11\Utils\AppConfig.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

namespace Day11.Utils;

public class AppConfig
{
    public static AppConfig Instance { get; } = new();
    private AppConfig() { }
    public static string DataFilePath => "contactData.txt";
    public static string AppName => "高级通讯录";
}