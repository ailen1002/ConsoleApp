// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 04月09日 12:04
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\Day6\Dog.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

namespace Day6;

public class Dog(string name) : Animal
{
    public override void Speak()
    {
        Console.WriteLine($"{name} : 汪汪汪！");
    }
    
    public override void Eat()
    {
        Console.WriteLine($"{name} : 吃骨头");
    }
}