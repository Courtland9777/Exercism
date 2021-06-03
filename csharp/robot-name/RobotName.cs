using System;
using System.Collections.Generic;

public class Robot
{
    private static readonly HashSet<string> RobotNameList = new HashSet<string>();
    private static readonly Random Rnd = new Random();

    public Robot() =>
        Name = GenerateRobotName();

    public string Name { get; private set; }

    private static string GenerateRobotName()
    {
        string newRobotName;
        do
        {
            newRobotName = NewName();
        } while (!RobotNameList.Add(newRobotName));
        return newRobotName;
    }

    private static string NewName() =>
        $"{TwoRandomLetters()}{ThreeDigitRandomNumber()}";

    private static string TwoRandomLetters() =>
        $"{(char)Rnd.Next(65, 91)}{(char)Rnd.Next(65, 91)}";

    private static string ThreeDigitRandomNumber() =>
        Rnd.Next(1000).ToString("000");

    public void Reset()
    {
        RobotNameList.Remove(Name);
        Name = GenerateRobotName();
    }
}