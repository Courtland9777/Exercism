using System;
using System.Linq;

public static class IsbnVerifier
{
    public static bool IsValid(string number)
    {
        var isbn = string.Concat(number.Where(c => char.IsDigit(c) || c.Equals('X')));
        if (isbn.Length != 10 || isbn[..9].Contains('X')) return false;
        var range = Enumerable.Range(1, 10).OrderByDescending(i => i).ToArray();
        return Enumerable.Range(0, 10)
            .Select(n => range[n] * (
                char.IsDigit(isbn[n]) ? 
                    int.Parse(isbn[n].ToString()) : 10)
            ).Sum() % 11 == 0;
    }
}