using System;
using System.Collections;
using System.Linq;

public static class PigLatin
{
    public static string Translate(string word) =>
        string.IsNullOrWhiteSpace(word)
            ? string.Empty
            : word.Split(' ').Aggregate(string.Empty, (current, w) =>
                $"{current} {ConvertWord(w)}")[1..];

    private static string ConvertWord(string input)
    {
        var vowels = new[] { 'a', 'e', 'i', 'o', 'u' };
        var consonantSounds = new[]
        {
            "bl", "br", "ch", "cl", "cr", "dr", "fl", "fr", "gl", "gr", "pl",
            "pr", "qu", "rh", "sch", "sc", "sh", "sk", "sl", "sm", "sn", "sp", "st", "sw",
            "thr", "th", "tr", "tw", "wh", "wr", "x", "k", "p", "q", "f", "r"
        };


        if (((IList)vowels).Contains(input[0]) ||
            input.Length >= 2 && (input[..2] == "xr" || input[..2] == "yt"))
        {
            return input + "ay";
        }

        if (input[0] == 'y')
        {
            return input[1..] + "yay";
        }

        foreach (var consonantSound in consonantSounds)
        {
            if (input.Length >= consonantSound.Length + 1 &&
                input[..consonantSound.Length] == consonantSound)
            {
                return input[consonantSound.Length..] + consonantSound + "ay";
            }
        }

        if (input.Length >= 3 && input.Substring(1, 2) == "qu")
        {
            return input[3..] + input[..3] + "ay";
        }

        if (!input.Contains('y'))
        {
            return input + "ay";
        }

        var indexOfY = input.IndexOf("y", StringComparison.Ordinal);
        return indexOfY > 0 && !((IList)vowels).Contains(input[indexOfY - 1])
            ? input[indexOfY..] + input[..indexOfY] + "ay"
            : input + "ay";
    }
}