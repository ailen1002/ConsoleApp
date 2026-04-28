// See https://aka.ms/new-console-template for more information

namespace Day10;

public static class GenericTool
{
    public static bool IsEqual<T>(T a, T b) where T : IComparable<T>
    {
        return a.CompareTo(b) == 0;
    }
    
    public static int FindIndex<T>(T[] array, T target)
    {
        if (array.Length == 0)
            return -1;

        for (var i = 0; i < array.Length; i++)
        {
            if (array[i]!.Equals(target))
                return i;
        }
        return -1;
    }
}
public class Student : IComparable<Student>
{
    public int Id { get; set; }
    public string Name { get; set; } = "";

    // 实现比较规则：按 Id 比较
    public int CompareTo(Student other)
    {
        return this.Id.CompareTo(other.Id);
    }

    // 重写相等判断
    public override bool Equals(object obj)
    {
        if (obj is Student student)
            return Id == student.Id;
        return false;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}

internal static class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("===== 泛型工具类测试 =====");

        // 1. 测试 int 比较
        var intEqual = GenericTool.IsEqual(10, 10);
        Console.WriteLine("10 和 10 相等？" + intEqual);

        // 2. 测试 string 比较
        var strEqual = GenericTool.IsEqual("Apple", "Banana");
        Console.WriteLine("Apple 和 Banana 相等？" + strEqual);

        // 3. 测试数组查找 int
        int[] numbers = [1, 3, 5, 7, 9];
        var index = GenericTool.FindIndex(numbers, 5);
        Console.WriteLine("数字 5 的索引：" + index);

        // 4. 测试自定义类 Student（泛型真正威力）
        var s1 = new Student { Id = 1, Name = "张三" };
        var s2 = new Student { Id = 1, Name = "张三" };
        var s3 = new Student { Id = 2, Name = "李四" };

        var stuEqual = GenericTool.IsEqual(s1, s2);
        Console.WriteLine("s1 和 s2 相等？" + stuEqual);

        Student[] students = [s1, s3];
        var stuIndex = GenericTool.FindIndex(students, s2);
        Console.WriteLine("s2 在数组中的索引：" + stuIndex);
    }
}