// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 04月08日 13:04
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\Day5\Model\Person.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

namespace Day5.Model;

public class Person
{
    public string Name { get; init; } = "";
    public string Gender { get; init; } = "";
    public int Age { get; init; }

    public void SelfIntroduction()
    {
        Console.WriteLine($"你好, 我的名字叫{Name}, 我是一个{Gender}性, 我今年{Age}岁。");
    }

    public void Eating()
    {
        Console.WriteLine($"{Name}一天吃了4顿饭。");
    }
}