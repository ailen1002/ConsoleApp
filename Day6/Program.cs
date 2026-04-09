// See https://aka.ms/new-console-template for more information

using Day6;

class Program
{
    private static void Main()
    {
        var a = new Cat("小白");
        var b = new Dog("小黑");
        
        a.Speak();
        b.Speak();
        a.Eat();
        b.Eat();
    }
}