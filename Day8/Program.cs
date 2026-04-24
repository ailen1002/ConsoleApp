// See https://aka.ms/new-console-template for more information

var r1 = Calc(1,2,(a,b)=>a+b);
Console.WriteLine(r1);


static int Calc(int a, int b, Func<int, int, int> func)
{
    return func(a, b);
}

var list = new List<int>{1,2,3,4,5};
var even = list.Where(x=>x%2 == 0);
foreach (var i in even)
{
    Console.WriteLine(i);
}