using System;
using System.Globalization;
using System.Numerics;

public static class CentralBank
{
    private const string TooBig = "*** Too Big ***";
    public static string DisplayDenomination(long @base, long multiplier) =>
        (BigInteger)@base * multiplier >= long.MaxValue ? TooBig : (@base * multiplier).ToString();

    public static string DisplayGDP(float @base, float multiplier) =>
        (@base * multiplier).ToString(CultureInfo.InvariantCulture).Equals("Infinity")
            ? TooBig
            : (@base * multiplier).ToString(CultureInfo.InvariantCulture);

    public static string DisplayChiefEconomistSalary(decimal salaryBase, decimal multiplier)
    {
        try
        {
            return (salaryBase * multiplier).ToString();
        }
        catch
        {
            return "*** Much Too Big ***";
        }
    }
}
