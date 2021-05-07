using System;
using System.Linq;

public static class Hamming
{
    public static int Distance(string firstStrand, string secondStrand) =>
        firstStrand.Length != secondStrand.Length
            ? throw new ArgumentException($"{nameof(firstStrand)} {nameof(secondStrand)}")
            : firstStrand.Equals(secondStrand, StringComparison.Ordinal)
            ? 0
            : Enumerable.Range(0, firstStrand.Length).Count(i => firstStrand[i] != secondStrand[i]);
}