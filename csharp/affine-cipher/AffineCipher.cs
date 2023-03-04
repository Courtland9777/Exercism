using System;
using System.Collections.Generic;
using System.Linq;

public static class AffineCipher
{
    private const int M = 26;

    public static string Encode(string plainText, int a, int b) =>
        !IsCoPrime(a, M)
            ? throw new ArgumentException($"{nameof(a)} ({a}) is not CoPrime to m ({M})", nameof(a))
            : new string(plainText
                .ToLowerInvariant()
                .Where(char.IsLetterOrDigit)
                .Select(c => char.IsLetter(c) ? CharAt(Mod(a * ValueOf(c) + b, M)) : c)
                .InsertAtInterval(' ', 5)
                .ToArray());

    public static string Decode(string cipheredText, int a, int b)
    {
        if (!IsCoPrime(a, M))
        {
            throw new ArgumentException($"{nameof(a)} ({a}) is not CoPrime to m ({M})", nameof(a));
        }

        var aInverse = ModInverse(a, M);
        return new string(cipheredText
            .Replace(" ", "")
            .Select(c => char.IsLetter(c) ? CharAt(Mod(aInverse * (ValueOf(c) - b), M)) : c)
            .ToArray());
    }

    private static int ValueOf(char c) => c - 'a';

    private static char CharAt(int nr) => (char)('a' + nr);
    private static bool IsCoPrime(int a, int b) => Gcd(a, b) == 1;

    private static int Gcd(int a, int b)
    {
        while (a != 0 && b != 0)
        {
            if (a > b)
            {
                a %= b;
            }
            else
            {
                b %= a;
            }
        }

        return Math.Max(a, b);
    }

    private static int Mod(int n, int m) => (n %= m) < 0 ? n + m : n;

    private static int ModInverse(int a, int n)
    {
        var i = n;
        var v = 0;
        var d = 1;
        while (a > 0)
        {
            var t = i / a;
            var x = a;
            a = i % x;
            i = x;
            x = d;
            d = v - t * x;
            v = x;
        }

        return Mod(v, n);
    }
}

public static class EnumerableExtensions
{
    public static IEnumerable<T> InsertAtInterval<T>(this IEnumerable<T> list, T separator, int interval)
    {
        var i = 0;
        foreach (var item in list)
        {
            if (i > 0 && i % interval == 0)
            {
                yield return separator;
            }

            yield return item;
            i++;
        }
    }
}