// See https://aka.ms/new-console-template for more information

using Day11.Services;
using Day11.Strategies;
using Day11.Utils;

namespace Day11;

internal abstract class ContractBook
{
    private static readonly ContactService ContactService = new(AppConfig.SaveStrategy);
    private static async Task Main()
    {
        await ContactService.LoadFromFileAsync();
        AppLogger.Log("程序启动: " + AppConfig.AppName);
        
        while (true)
        {
            try
            {
                Console.WriteLine("\n===== 通讯录菜单 =====");
                Console.WriteLine("1. 添加联系人; 2. 查看所有联系人; 3. 按索姓名查找联系人; 4. 退出");
                Console.Write("请选择操作: ");

                if (!ConsoleHelper.TryReadNumber(out var choice))
                {
                    ConsoleHelper.PrintLine("请输入合法数字");
                    continue;
                }
            
                switch (choice)
                {
                    case 1:
                        await AddContact();
                        break;
                    case 2:
                        ShowContacts();
                        break;
                    case 3:
                        FindContactByIndex();
                        break;
                    case 4:
                        await ContactService.SaveToFileAsync();
                        Console.WriteLine("已保存, 退出程序!");
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

    private static async Task AddContact()
    {
        Console.Write("请输入联系人姓名: ");
        var name = ConsoleHelper.ReadInput();
        
        Console.Write("请输入联系人电话: ");
        var phone = ConsoleHelper.ReadInput();
        
        ContactService.AddContact(name, phone);
        await ContactService.SaveToFileAsync();
        ConsoleHelper.PrintLine("添加并自动保存成功");
    }

    private static void ShowContacts()
    {
        var list = ContactService.GetAllContacts();
        
        if (list.Count == 0)
        {
            Console.WriteLine("通讯录为空!");
            return;
        }

        Console.WriteLine("\n----- 联系人列表 -----");
        for (var i = 0; i < list.Count; i++)
        {
            Console.WriteLine($"{i}: 姓名:{list[i].Name} 电话:{list[i].Phone}");
        }
    }

    private static void FindContactByIndex()
    {
        Console.Write("请输入联系人姓名: ");
        var name = ConsoleHelper.ReadInput();

        var findContact = ContactService.GetByName(name);
        
        if (findContact == null)
        {
            ConsoleHelper.PrintLine("未找到该用户!"); 
        }
        else
        {
            Console.WriteLine($"姓名: {findContact.Name} 电话: {findContact.Phone}");
        }
    }
}