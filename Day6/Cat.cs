// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 04月09日 13:04
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\Day6\Cat.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

namespace Day6;

public class Cat(string name) : Animal
{
    public override void Speak()
    {
        Console.WriteLine($"{name} : 喵喵喵！");
    }
    
    public override void Eat()
    {
        Console.WriteLine($"{name} : 吃小鱼");
    }
}