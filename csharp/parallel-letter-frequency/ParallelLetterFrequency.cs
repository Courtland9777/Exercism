using System;
using System.Collections.Generic;
using System.Linq;

public static class ParallelLetterFrequency
{
    public static Dictionary<char, int> Calculate(IEnumerable<string> texts) =>
        texts.AsParallel().SelectMany(c => c.ToLower().Where(char.IsLetter)).GroupBy(l => l)
            .ToDictionary(l => l.Key, l => l.Count());
}