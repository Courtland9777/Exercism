using System;
using System.Collections.Generic;
using System.Linq;

public static class AllYourBase
{
    public static int[] Rebase(int inputBase, int[] inputDigits, int outputBase) =>
        inputBase <= 1 || outputBase <= 1 || inputDigits.Any(i => i < 0) || inputDigits.Any(i => i >= inputBase)
            ? throw new ArgumentException()
            : inputDigits.All(d => d == 0)
            ? (new int[] { 0 })
            : FromBase10(outputBase, ToBase10(inputDigits, inputBase));

    private static int ToBase10(int[] digits, int inputB)
    {
        int power = 1;
        int num = 0;

        for (var i = digits.Length - 1; i >= 0; i--)
        {
            num += digits[i] * power;
            power *= inputB;
        }

        return num;
    }

    private static int[] FromBase10(int outputB, int inputNum)
    {
        var s = new List<int>();

        while (inputNum > 0)
        {
            s.Add(inputNum % outputB);
            inputNum /= outputB;
        }
        return s.ToArray().Reverse().ToArray();
    }
}