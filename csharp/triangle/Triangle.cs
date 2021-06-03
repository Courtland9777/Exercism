using System.Collections.Generic;
using System.Linq;

public static class Triangle
{
    public static bool IsScalene(double side1, double side2, double side3) =>
        !IsAnySideEqual(side1, side2, side3) &&
        IsSumOfTwoSidesGreaterThanThirdSide(new[] { side1, side2, side3 }.OrderByDescending(s=>s).ToArray());

    public static bool IsIsosceles(double side1, double side2, double side3) =>
        IsAnySideEqual(side1, side2, side3) &&
        IsSumOfTwoSidesGreaterThanThirdSide(new[] { side1, side2, side3 }.OrderByDescending(s => s).ToArray());

    public static bool IsEquilateral(double side1, double side2, double side3) =>
        side1.Equals(side2) && side1.Equals(side3) && !side1.Equals(0.0);

    private static bool IsAnySideEqual(double side1, double side2, double side3) =>
        side1.Equals(side2) || side1.Equals(side3) || side2.Equals(side3);

    private static bool IsSumOfTwoSidesGreaterThanThirdSide(IReadOnlyList<double> sides) =>
        sides[2] + sides[1] > sides[0];
}