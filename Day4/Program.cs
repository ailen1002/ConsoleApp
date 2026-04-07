// See https://aka.ms/new-console-template for more information

namespace Day4;

class Program
{
    private static void Main()
    {
        Console.WriteLine("《计算器》");
        Console.WriteLine("1:阶乘计算;2:斐波那契计算;0:退出");
        while (true)
        {
            var input = Console.ReadLine();

            switch (input)
            {
                case "1": FactorialCalculation();
                    break;
                case "2": FibonacciCalculation();
                    break;
                case "0": return;
            }
        }
    }

    private static void FactorialCalculation()
    {
        Console.WriteLine("请输入一个整数:");
        var input = Convert.ToInt16(Console.ReadLine());
        var result = Factorial(input);
        Console.WriteLine($"计算结果: {result}");
    }

    private static int Factorial(int a)
    {
        if (a == 0)
        {
            return 1;
        }

        return a*Factorial(a-1);
    }
    
    private static void FibonacciCalculation()
    {
        Console.WriteLine("请输入一个整数:");
        var input = Convert.ToInt16(Console.ReadLine());
        var result = Fibonacci(input);
        Console.WriteLine($"计算结果: {result}");
    }
    
    private static int Fibonacci(int a)
    {
        return a switch
        {
            1 => 1,
            2 => 1,
            _ => Fibonacci(a - 1) + Fibonacci(a - 2)
        };
    }
}