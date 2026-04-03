// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 04月03日 11:04
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\Day3\StudentManager.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using Day3.Enums;
using Day3.Models;

namespace Day3;

public class StudentManager
{
    private static List<Student> Students = [];
    public static readonly Dictionary<string, Student> Map = new();

    public static void AddOrUpdate(Student s)
    {
        if (Map.TryGetValue(s.Name, out var value))
        {
            value.Gender = s.Gender;
            value.Age = s.Age;
            value.Scores = s.Scores;
        }
        else
        {
            Students.Add(s);
            Map[s.Name] = s;
        }
    }
    
    public static bool Delete(string name)
    {
        if (!Map.TryGetValue(name, out var value)) return false;
        Students.Remove(value);
        Map.Remove(name);
        return true;
    }
    
    public static Student? Get(string name)
    {
        return Map.GetValueOrDefault(name);
    }

    public static List<Student> GetAll()
    {
        return Students;
    }
    
    public static bool UpdateScore(string name, Subject subject, int score)
    {
        if (!Map.TryGetValue(name, out var s))
            return false;

        s.Scores[subject] = score;
        return true;
    }
    
    public static void UpdateRank()
    {
        var sorted = Students
            .OrderByDescending(s => s.TotalScore)
            .ToList();

        var rank = 1;

        for (var i = 0; i < sorted.Count; i++)
        {
            if (i > 0 && sorted[i].TotalScore < sorted[i - 1].TotalScore)
                rank = i + 1;

            sorted[i].Rank = rank;
        }
    }
    
    public static List<Student> GetSortedByTotal()
    {
        return Students
            .OrderByDescending(s => s.TotalScore)
            .ToList();
    }
    
    public static List<Student> GetSortedBySubject(Subject subject)
    {
        return Students
            .OrderByDescending(s => s.Scores.GetValueOrDefault(subject, 0))
            .ToList();
    }
}