using System;
using System.Linq;

public enum Classification
{
    Perfect,
    Abundant,
    Deficient
}

public static class PerfectNumbers
{
    public static Classification Classify(int number)
    {
        var aliquotSum = number > 0 ? GetAliquotSum(number) : throw new ArgumentOutOfRangeException(nameof(number));
        return aliquotSum == number ? Classification.Perfect :
            aliquotSum > number ? Classification.Abundant : Classification.Deficient;
    }

    private static int GetAliquotSum(int n) =>
        Enumerable.Range(1, (int)Math.Sqrt(n))
            .Where(i => n % i == 0 && i != n)
            .Select(j => (j != n / j && j != 1) ? j + (n / j) : j)
            .Sum();
}
