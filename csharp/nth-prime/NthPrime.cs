using System.Linq;

public static class NthPrime
{
    public static bool IsPrime(int n)
    {
        switch (n)
        {
            case <= 1:
                return false;
            case <= 3:
                return true;
        }

        if (n % 2 == 0 || n % 3 == 0)
        {
            return false;
        }

        for (var i = 5; i * i <= n; i += 6)
        {
            if (n % i == 0 || n % (i + 2) == 0)
            {
                return false;
            }
        }

        return true;
    }

    public static int Prime(int nth) => Enumerable.Range(2, int.MaxValue - 2).Where(IsPrime).ElementAt(nth - 1);
}