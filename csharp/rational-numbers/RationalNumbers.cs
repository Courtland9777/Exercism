using System;
using System.Diagnostics;

public static class RealNumberExtension
{
    public static double Expreal(this int realNumber, RationalNumber r) => r.Expreal(realNumber);
}

public struct RationalNumber
{
    private int numerator, denominator;
    private static int GCD(int a, int b)
    {
        while (a != 0 && b != 0)
        {
            if (a > b)
                a %= b;
            else
                b %= a;
        }
        return a == 0 ? b : a;
    }

    public double Expreal(int baseNumber) => Math.Pow(Math.Pow(baseNumber, numerator), 1 / (double)denominator);

    public RationalNumber(int numerator = 0, int denominator = 1)
    {
        this.numerator = numerator;
        this.denominator = denominator;
    }

    public static RationalNumber operator +(RationalNumber r1, RationalNumber r2)
    {
        var sum = new RationalNumber
        {
            numerator = (r1.numerator * r2.denominator) + (r2.numerator * r1.denominator),
            denominator = r1.denominator * r2.denominator
        };
        if (sum.numerator == 0)
            sum.denominator = 1;
        return sum.Reduce();
    }

    public static RationalNumber operator -(RationalNumber r1, RationalNumber r2)
    {
        var dif = new RationalNumber
        {
            numerator = (r1.numerator * r2.denominator) - (r2.numerator * r1.denominator),
            denominator = r1.denominator * r2.denominator
        };
        if (dif.numerator == 0)
            dif.denominator = 1;
        return dif.Reduce();
    }

    public static RationalNumber operator *(RationalNumber r1, RationalNumber r2)
    {
        var mul = new RationalNumber
        {
            numerator = r1.numerator * r2.numerator,
            denominator = r1.denominator * r2.denominator
        };
        return mul.Reduce();
    }

    public static RationalNumber operator /(RationalNumber r1, RationalNumber r2)
    {
        var div = new RationalNumber();
        if (r1.denominator * r2.denominator == 0)
            throw new ArgumentException();
        div.numerator = r1.numerator * r2.denominator;
        div.denominator = r2.numerator * r1.denominator;
        if (div.numerator < 0 && div.denominator < 0)
        {
            div.numerator = Math.Abs(div.numerator);
            div.denominator = Math.Abs(div.denominator);
        }

        if (div.numerator <= 0 || div.denominator >= 0)
        {
            return div.Reduce();
        }

        div.numerator -= 2 * div.numerator;
        div.denominator = Math.Abs(div.denominator);
        return div.Reduce();
    }

    public RationalNumber Abs()
    {
        numerator = Math.Abs(numerator);
        denominator = Math.Abs(denominator);
        return this;
    }

    public RationalNumber Reduce()
    {
        int gcd = GCD(Math.Abs(numerator), Math.Abs(denominator));
        int newNumerator = 0, newDenominator = 0;
        if (gcd == 1)
            return this;
        if (numerator < 0)
        {
            newNumerator = Math.Abs(numerator);
            newNumerator /= gcd;
            newNumerator -= 2 * newNumerator;
        }
        else
            newNumerator = numerator / gcd;
        if (denominator < 0 && newNumerator > 0)
        {
            newDenominator = Math.Abs(denominator);
            newDenominator /= gcd;
            newNumerator -= 2 * newNumerator;
        }
        else
        {
            newDenominator = denominator / gcd;
        }

        return new RationalNumber(newNumerator, newDenominator);
    }

    public RationalNumber Exprational(int power)
    {
        var powered = new RationalNumber();
        if (power >= 0)
        {
            powered.numerator = (int)Math.Pow(numerator, power);
            powered.denominator = (int)Math.Pow(denominator, power);
        }
        else
        {
            powered.numerator = (int)Math.Pow(denominator, Math.Abs(power));
            powered.denominator = (int)Math.Pow(numerator, Math.Abs(power));
        }
        return powered.Reduce();
    }
}