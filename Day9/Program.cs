// See https://aka.ms/new-console-template for more information

using Day9.Models;

var students = new List<Student>
{
    new Student { Id = 1, Name = "张三", Age = 20, Score = 85, Class = "A班" },
    new Student { Id = 2, Name = "李四", Age = 22, Score = 76, Class = "B班" },
    new Student { Id = 3, Name = "王五", Age = 19, Score = 92, Class = "A班" },
    new Student { Id = 4, Name = "赵六", Age = 21, Score = 68, Class = "B班" },
    new Student { Id = 5, Name = "钱八", Age = 20, Score = 88, Class = "A班" }
};

var olderStudents = students.Where(s => s.Age > 20);
foreach (var s in olderStudents)
{
    Console.WriteLine($"大于20岁的学生名字是:{s.Name}。");
}
var highScoreStudents = students.Where(s=> s.Score >= 80);
foreach (var s in highScoreStudents)
{
    Console.WriteLine($"分数大于80分的学生名字是:{s.Name}。");
}
var classStudents = students.Where(s => s.Class == "A班");
foreach (var s in classStudents)
{
    Console.WriteLine($"A班级的学生名字是:{s.Name}。");
}
var result1 = students.Where(s => s is { Score: >= 90, Class: "A班" });
foreach (var s in result1)
{
    Console.WriteLine($"A班大于90的学生名字是:{s.Name}。");
}
var result2 = students.Where(s => s is { Age: >= 20 , Age: <=22 });
foreach (var s in students)
{
    Console.WriteLine($"年龄在20到22岁之间的学生名字是:{s.Name}。");
}
var result = students.Where(s => s is { Class: "A班", Age: > 20, Score: > 80 } && s.Name.Contains('张'));
foreach (var s in result)
{
    Console.WriteLine($"按照要求找到的学生是:{s.Name}。");
}
var sortedByAge = students.OrderBy(s => s.Age);
foreach (var s in sortedByAge)
{
    Console.WriteLine($"学生按照年龄排序: 姓名:{s.Name} 年龄:{s.Age}。");
}
var sortedByScoreDesc = students.OrderByDescending(s => s.Score);
foreach (var s in sortedByScoreDesc)
{
    Console.WriteLine($"学生按分数倒叙排序: 姓名:{s.Name} 分数:{s.Score}。");
}
var multiSorted = students.OrderBy(s => s.Class).ThenByDescending(s => s.Score);
foreach (var s in multiSorted)
{
    Console.WriteLine($"先按照班级排序，在按照分数从高到底排序: 姓名:{s.Name} 分数:{s.Score}。");
}