// See https://aka.ms/new-console-template for more information

using Day7.Models;

namespace Day7;

class Program()
{
    private static void Main()
    {
        Console.WriteLine("控制台通讯录");
        while (true)
        {
            Console.WriteLine("0:增加,1:删除,2:查询,3:退出");
            var a = Convert.ToInt16(Console.ReadLine());
            switch (a)
            {
                case 0: Add();
                    break;
                case 1: Delete();
                    break;
                case 2: Show();
                    break;
                case 3: return;
            }
        }
    }

    private static void Add()
    {
        Console.WriteLine("姓名:");
        var name = Console.ReadLine() ?? "";
        Console.WriteLine("电话:");
        var number = Console.ReadLine() ?? "";
        var c = new Contacts()
        {
            Name = name,
            Phone = number
        };
        ContactsManager.AddOrUpdate(c);
        ContactsManager.Map[name] = c;
    }

    private static void Delete()
    {
        Console.WriteLine("姓名:");
        var name = Console.ReadLine() ?? "";
        ContactsManager.Delete(name);
    }
    
    private static void Show()
    {
        var contacts = ContactsManager.GetAll();
        foreach (var c in contacts)
        {
            Console.WriteLine($"姓名:{c.Name} | 电话:{c.Phone}");
        }
    }
}