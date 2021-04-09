using System;
using System.Collections.Generic;
using System.Linq;

public static class PythagoreanTriplet
{
    public static IEnumerable<(int a, int b, int c)> TripletsWithSum(int sum)
    {
        var list = new List<(int a, int b, int c)>();

        int mLimit = (int)Math.Sqrt(sum / 2);
        for (var m = 2; m <= mLimit; m++)
        {
            if ((sum / 2) % m != 0)
            {
                continue;
            }

            var k = m % 2 == 0 ? m + 1 : m + 2;
            while (k < 2 * m && k <= sum / (2 * m))
            {
                if (sum / (2 * m) % k == 0 && Gcd(k, m) == 1)
                {
                    var d = sum / 2 / (k * m);
                    var n = k - m;
                    var a = d * (m * m - n * n);
                    var b = 2 * d * n * m;
                    var c = d * (m * m + n * n);
                    if (a+b+c == sum) list.Add(a < b ? (a, b, c) : (b, a, c));
                }
                k += 2;
            }
        }

        return list.OrderBy(v => v.a);
    }

    private static int Gcd(int a, int b)
    {
        var y = 0;
        var x = 0;

        if (a > b)
        {
            x = a;
            y = b;
        }
        else
        {
            x = b;
            y = a;
        }

        while (x % y != 0)
        {
            int temp = x;
            x = y;
            y = temp % x;
        }
        return y;
    }
}