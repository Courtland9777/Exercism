using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Proverb
{
    private const string ForWantOfA = "For want of a ";
    private const string WasLost = " was lost.";
    private const string AndAllFor = "And all for the want of a ";
    public static string[] Recite(string[] subjects)
    {
        List<string> proverb = new List<string>();

        if (subjects.Length <= 0)
        {
            return proverb.ToArray();
        }

        string ending = $"And all for the want of a {subjects[0]}.";

        for (int i = 1; i < subjects.Length; i++)
            proverb.Add($"For want of a {subjects[i - 1]} the {subjects[i]} was lost.");
        proverb.Add(ending);

        return proverb.ToArray();
    }
}