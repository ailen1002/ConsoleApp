// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 04月30日 13:04
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\Day11\Factories\ContactFactory.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using Day11.Models;

namespace Day11.Factories;

public static class ContactFactory
{
    public static Contact CreateContact(string name, string phone)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("姓名不能为空");
        
        return new Contact
        {
            Name = name.Trim(),
            Phone = phone.Trim()
        };
    }
    
    public static Contact CreateEmptyContact()
    {
        return new Contact();
    }
}