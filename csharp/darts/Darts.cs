using System;

public static class Darts
{
    //C# 9 makes this even more concise
    public static int Score(double x, double y)
    {
        var left = Math.Pow(x, 2) + Math.Pow(y, 2);
        return left switch
        {
            _ when left > 100 => 0,
            _ when left > 25 => 1,
            _ when left > 1 => 5,
            _ when left <= 1 => 10,
            _ => throw new ArgumentException(nameof(left))
        };
    }
}
