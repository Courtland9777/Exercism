using System;
using System.Linq;

public static class Isogram
{
    public static bool IsIsogram(string word)
    {
        var cleanCharArray = word.ToLower().Where(char.IsLetter).ToArray();
        return cleanCharArray.Length == cleanCharArray.Distinct().Count();
    }
}
