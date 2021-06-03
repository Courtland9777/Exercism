using System;
using System.Collections.Generic;

public static class Etl
{
    public static Dictionary<string, int> Transform(Dictionary<int, string[]> old)
    {
        var dict = new Dictionary<string, int>();
        foreach (var (pointValue, letters) in old)
        {
            foreach (var letter in letters)
            {
                dict.Add(letter.ToLower(), pointValue);
            }
        }

        return dict;
    }
}