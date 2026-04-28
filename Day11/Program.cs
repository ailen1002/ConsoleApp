// See https://aka.ms/new-console-template for more information

namespace Day11;

internal abstract class ContractBook
{
    private static readonly List<string> Contacts = [];

    private static void Main()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("\n===== 通讯录菜单 =====");
                Console.WriteLine("1. 添加联系人; 2. 查看所有联系人; 3. 按索引查找联系人; 4. 退出");
                Console.Write("请选择操作: ");
            
                var choice = Convert.ToInt16(Console.ReadLine());
            
                switch (choice)
                {
                    case 1:
                        AddContact();
                        break;
                    case 2:
                        ShowContacts();
                        break;
                    case 3:
                        FindContactByIndex();
                        break;
                    case 4:
                        Console.WriteLine("退出程序!");
                        return;
                    default:
                        Console.WriteLine("输入无效, 请选1-4!");
                        break;
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine("【异常】请输入有效数字！错误: " + ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("【系统异常】: " + e);
                throw;
            }
        }
    }

    private static void AddContact()
    {
        Console.Write("请输入联系人姓名: ");
        var name = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.Write("姓名不能为空!");
        }
        else
        {
            Contacts.Add(name);
            Console.WriteLine("添加成功!");
        }
    }

    private static void ShowContacts()
    {
        if (Contacts.Count == 0)
        {
            Console.WriteLine("通讯录为空!");
            return;
        }

        Console.WriteLine("\n----- 联系人列表 -----");
        for (var i = 0; i < Contacts.Count; i++)
        {
            Console.WriteLine($"{i}：{Contacts[i]}");
        }
    }

    private static void FindContactByIndex()
    {
        try
        {
            Console.Write("请输入联系人编号: ");
            var index = Convert.ToInt16(Console.ReadLine());
        
            var findName = Contacts[index];
            Console.WriteLine($"找到联系人: {findName}");
        }
        catch (Exception)
        {
            Console.WriteLine("未找到联系人!");
        }
    }
}