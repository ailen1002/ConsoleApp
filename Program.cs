// See https://aka.ms/new-console-template for more information

Console.WriteLine("求1到100的和");
var sum = 0;
for (var i = 1; i <= 100; i++)
{
    sum = sum + i;
}
Console.WriteLine($"1到100的和是：{sum}");
Console.WriteLine("求1到100的奇数和");
var sum1 = 0;
for (var i = 1; i <= 100; i++)
{
    if (i % 2 == 1)
    {
        sum1 = sum1 + i;
    }
}
Console.WriteLine($"1到100的奇数和是：{sum1}");