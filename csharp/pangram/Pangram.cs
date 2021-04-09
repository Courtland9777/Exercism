using System;
using System.Linq;

public static class Pangram
{
    private const string Alphabet = "abcdefghijklmnopqrstuvwxyz";

    public static bool IsPangram(string input) =>
        !string.IsNullOrEmpty(input) && Alphabet.All(input.ToLower().Contains);
}
