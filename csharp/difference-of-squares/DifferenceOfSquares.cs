using System;
using System.Linq;

public static class DifferenceOfSquares
{
    public static int CalculateSquareOfSum(int max) =>
        (int)Math.Pow(Enumerable.Range(1, max).Sum(), 2);

    public static int CalculateSumOfSquares(int max) =>
        (max * (max + 1) * (2 * max + 1)) / 6;

    public static int CalculateDifferenceOfSquares(int max) =>
        Math.Abs(CalculateSquareOfSum(max) - CalculateSumOfSquares(max));
}