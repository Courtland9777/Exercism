using System;
using System.Collections.Generic;

public static class PrimeFactors
{
    public static long[] Factors(long number)
    {
        var factorList = new List<long>();
        while (number % 2 == 0)
        {
            factorList.Add(2);
            number /= 2;
        }

        for (int i = 3; i <= Math.Sqrt(number); i += 2)
        {
            while (number % i == 0)
            {
                factorList.Add(i);
                number /= i;
            }
        }

        if (number > 2) factorList.Add(number);

        return factorList.ToArray();
    }
}
