// See https://aka.ms/new-console-template for more information

using Day5.Model;

namespace Day5;

class Program
{
    private static void Main()
    {
        var people = new Person
        {
            Name = "张三",
            Gender = "男",
            Age = 18
        };

        people.SelfIntroduction();
        people.Eating();
    }
}