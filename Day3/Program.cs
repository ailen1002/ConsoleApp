// See https://aka.ms/new-console-template for more information

using Day3.Enums;
using Day3.Models;

namespace Day3;

class Program
{
    private static void Main()
    {
        Console.WriteLine("《学生管理程序》");
        Console.WriteLine("1:增加;2:删除;3:遍历;0:退出");
        while (true)
        {
            var input = Console.ReadLine();

            switch (input)
            {
                case "1": AddStudent();
                    break;
                case "2": DeleteStudent();
                    break;
                case "3": ShowAllStudent();
                    break;
                case "0": return;
            }
        }
    }

    private static void AddStudent()
    {
        Console.Write("姓名:");
        var name = Console.ReadLine() ?? "";
        
        if (StudentManager.Map.ContainsKey(name))
        {
            Console.WriteLine("学生已存在");
            return;
        }
        
        Console.Write("性别:");
        var gender = Console.ReadLine() ?? "";
        
        Console.Write("年龄:");
        var age = Convert.ToInt16(Console.ReadLine() ?? "");
        
        Console.Write("语文:");
        var chinese = Convert.ToDouble(Console.ReadLine() ?? "");
        
        Console.Write("数学:");
        var math = Convert.ToDouble(Console.ReadLine() ?? "");
        
        Console.Write("英语:");
        var english = Convert.ToDouble(Console.ReadLine() ?? "");
        
        var stu = new Student()
        {
            Name = name,
            Gender = gender,
            Age = age,
            Scores = new Dictionary<Subject, double>()
            {
                [Subject.语文] = chinese,
                [Subject.数学] = math,
                [Subject.英语] = english,
            }
        };
        
        StudentManager.AddOrUpdate(stu);
        StudentManager.Map[name] = stu;
    }
    
    private static void DeleteStudent()
    {
        Console.Write("姓名:");
        var name = Console.ReadLine() ?? "";
        
        if (StudentManager.Map.ContainsKey(name))
        {
            StudentManager.Delete(name);
            return;
        }
        Console.WriteLine("未查找到该学生");
    }
    
    private static void ShowAllStudent()
    {
        var students = StudentManager.GetAll();
        foreach (var s in students)
        {
            Console.WriteLine(
                $"{s.Name} | {s.Gender} | {s.Age} | 语文:{s.Scores.GetValueOrDefault(Subject.语文, 0)} | 数学:{s.Scores.GetValueOrDefault(Subject.数学)} | 语文:{s.Scores.GetValueOrDefault(Subject.英语)} |总分:{s.TotalScore} | 排名:{s.Rank}");
        }
    }
}