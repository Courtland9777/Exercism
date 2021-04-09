using System;

public static class Grains
{
    public static ulong Square(int n) =>
        n > 0 && n < 65 ?
            (ulong)Math.Pow(2, n - 1) :
            throw new ArgumentOutOfRangeException(nameof(n));

    public static ulong Total() =>
        18446744073709551615;
}