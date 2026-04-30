// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 04月30日 13:04
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\Day11\Strategies\ISaveStrategy.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using Day11.Models;

namespace Day11.Strategies;

public interface ISaveStrategy
{
    Task SaveAsync(string path, List<Contact> contacts);

    Task<List<Contact>> LoadAsync(string path);
}