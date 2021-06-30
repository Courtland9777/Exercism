using System;
using System.Collections.Generic;
using System.Linq;

public static class Sieve
{
    public static int[] Primes(int limit)
    {
        if(limit < 2) throw new ArgumentOutOfRangeException(nameof(limit));

        bool[] isPrime  = Enumerable.Repeat(true, limit + 1).ToArray();

        var primeNumberList = new HashSet<int>();
        for (int i = 2; i <= limit; i++)
        {
            if (!isPrime[i])
            {
                continue;
            }

            primeNumberList.Add(i);
            for (int j = i * 2; j <= limit; j += i)
                isPrime[j] = false;
        }
        return primeNumberList.ToArray();
    }
}