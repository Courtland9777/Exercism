using System;
using System.Linq;

public static class ArmstrongNumbers
{
    public static bool IsArmstrongNumber(int number) => 
        number < 10 || (number >= 100 && CalculateArmstrongOver99(number, number.ToString().Length));

    private static bool CalculateArmstrongOver99(int input, int power) =>
        input.ToString().ToCharArray().Select(c => int.Parse(c.ToString())).
            Sum(x => (int)Math.Pow(x, power)) == input;
}