using System;

public static class RomanNumeralExtension
{
    private static readonly string[] ThouLetters = { "", "M", "MM", "MMM" };
    private static readonly string[] HundLetters =
        { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" };
    private static readonly string[] TensLetters =
        { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
    private static readonly string[] OnesLetters =
        { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" };

    public static string ToRoman(this int value)
    {
        string result = string.Empty;

        result += ThouLetters[value / 1000];
        value %= 1000;

        result += HundLetters[value / 100];
        value %= 100;

        result += TensLetters[value / 10];
        value %= 10;

        result += OnesLetters[value];

        return result;
    }
}