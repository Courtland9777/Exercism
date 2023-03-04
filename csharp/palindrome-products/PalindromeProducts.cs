using System;
using System.Collections.Generic;
using System.Linq;

public static class PalindromeProducts
{
    public static (int, IEnumerable<(int, int)>) Largest(int minFactor, int maxFactor) =>
        Products(minFactor, maxFactor).LastOrDefault();

    public static (int, IEnumerable<(int, int)>) Smallest(int minFactor, int maxFactor) =>
        Products(minFactor, maxFactor).FirstOrDefault();

    private static IEnumerable<(int product, IEnumerable<(int, int)> factors)> Products(int minFactor, int maxFactor)
    {
        if (minFactor > maxFactor)
        {
            throw new ArgumentException("minFactor cannot be larger than maxFactor", nameof(minFactor));
        }

        var products = new Dictionary<int, HashSet<(int, int)>>();
        for (var i = minFactor; i <= maxFactor; i++)
        {
            for (var j = minFactor; j <= maxFactor; j++)
            {
                if (!IsPalindrome(i * j))
                {
                    continue;
                }

                if (!products.ContainsKey(i * j))
                {
                    products.Add(i * j, new HashSet<(int, int)>());
                }

                if (!(products[i * j].Contains((i, j)) || products[i * j].Contains((j, i))))
                {
                    products[i * j].Add((i, j));
                }
            }
        }

        return products.Count == 0
            ? throw new ArgumentException("No palindrome in this range")
            : products.OrderBy(x => x.Key).Select(x => (x.Key, x.Value.Select(valueTuple => valueTuple)));
    }

    private static bool IsPalindrome(int number) => number == ReverseInt(number);

    private static int ReverseInt(int number)
    {
        var reversed = 0;
        while (number > 0)
        {
            reversed = reversed * 10 + number % 10;
            number /= 10;
        }

        return reversed;
    }
}