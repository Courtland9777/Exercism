using System;
using System.Linq;

public static class RnaTranscription
{
    private const char V = '\0';

    public static string ToRna(string nucleotide) =>
        string.Concat(nucleotide.Select(x => x switch
        {
            V => V,
            'A' => 'U',
            'C' => 'G',
            'G' => 'C',
            'T' => 'A',
            _ => throw new Exception()
        }));
}