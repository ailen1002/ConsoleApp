// /*
//  * ================================================================================
//  * @Author       : Andrew
//  * @Date         : 04月03日 09:04
//  * @FilePath     : D:\works\MFCProject\RiderProjects\ConsoleApp1\Day3\Models\Student.cs
//  * @Description  :
//  * @Copyright    : Copyright 2015 zhang xu, All rights reserved.
//  * ================================================================================
//  */

using Day3.Enums;

namespace Day3.Models;

public class Student
{
    public string Name { get; set; } = "";
    public string Gender { get; set; } = "";
    public int Age { get; set; }
    public Dictionary<Subject, double> Scores { get; set; } = new();
    public double TotalScore => Scores.Values.Sum();
    public int Rank { get; set; }
}