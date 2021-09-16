using System;
using System.Collections.Generic;
using System.Linq;

public static class ResistorColorDuo
{
    public static int Value(string[] colors)
    {
        var numberString = string.Empty;

        for (var i = 0; i < 2; i++)
        {
            if (Enum.TryParse(typeof(Color), colors[i], out object number))
            {
                numberString += (int)number;
            }
        }

        return int.Parse(numberString);
    }
}

public enum Color
{
    black = 0,
    brown = 1,
    red = 2,
    orange = 3,
    yellow = 4,
    green = 5,
    blue = 6,
    violet = 7,
    grey = 8,
    white = 9
}
