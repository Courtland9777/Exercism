using System;
using System.Collections.Generic;

public static class Raindrops
{
    private const string Pling = "Pling";
    private const string Plang = "Plang";
    private const string Plong = "Plong";

    public static string Convert(int number)
    {
        string result = string.Empty;
        if (number % 3 == 0) result = Pling;
        if (number % 5 == 0) result = $"{result}{Plang}";
        if (number % 7 == 0) result = $"{result}{Plong}";

        return string.IsNullOrEmpty(result)?number.ToString():result;
    }
}