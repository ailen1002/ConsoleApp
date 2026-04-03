// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

Console.WriteLine("请输入：");
var word = Console.ReadLine();

if (string.IsNullOrEmpty(word))
{
    Console.WriteLine("输入为空！");
    return;
}

var arr = word.ToCharArray();
Array.Reverse(arr);
var newWord = new string(arr);
var charCount = word.Length;
var wordCount = Regex.Count(word, @"\b\w+\b");

Console.WriteLine($"字符串颠倒为：{newWord}");
Console.WriteLine($"输入的字符数量：{charCount}");
Console.WriteLine($"输入的单词数量：{wordCount}");
