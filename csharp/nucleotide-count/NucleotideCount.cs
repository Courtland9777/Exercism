using System;
using System.Collections.Generic;
using System.Linq;

public static class NucleotideCount
{
    public static IDictionary<char, int> Count(string sequence) =>
        sequence.Any(c => !"ACGT".Contains(c))
        ? throw new ArgumentException(nameof(sequence))
        : new Dictionary<char, int>
        {
            { 'A', CharCount(sequence, 'A') },
            { 'C', CharCount(sequence, 'C') },
            { 'G', CharCount(sequence, 'G') },
            { 'T', CharCount(sequence, 'T') },
        };

    private static int CharCount(string sequence, char nucleotideLetter) =>
        sequence.Count(c => c == nucleotideLetter);
}