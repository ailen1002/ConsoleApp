// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 04月10日 09:04
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\Day7\ContactsManager.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using Day7.Models;

namespace Day7;

public class ContactsManager
{
    private static readonly List<Contacts> Contacts = [];
    public static readonly Dictionary<string, Contacts> Map = new();

    public static void AddOrUpdate(Contacts c)
    {
        if (Map.TryGetValue(c.Name,out var exit))
        {
            exit.Phone = c.Phone;
        }
        else
        {
            Contacts.Add(c);
            Map[c.Name] = c;
        }
    }

    public static bool Delete(string name)
    {
        if (!Map.TryGetValue(name, out var exit))
        {
            Console.WriteLine("没有该联系人!");
            return false;
        }

        Contacts.Remove(exit);
        Map.Remove(name);
        return true;
    }

    public static List<Contacts> GetAll()
    {
        return Contacts;
    }
}