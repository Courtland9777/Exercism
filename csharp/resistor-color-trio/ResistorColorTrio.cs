using System;
using System.Linq;
using System.Text;

public enum Colors
{
    Black = 0,

    Brown = 1,

    Red = 2,

    Orange = 3,

    Yellow = 4,

    Green = 5,

    Blue = 6,

    Violet = 7,

    Grey = 8,

    White = 9,
}

public static class ResistorColorTrio
{
    public static string Label(string[] colors)
    {
        var colorEnum = colors.Select(c => (Colors)Enum.Parse(typeof(Colors), c, true)).ToArray();
        var sb = new StringBuilder();
        sb.Append((int)colorEnum[0]).Append((int)colorEnum[1]);
        if ((int)colorEnum[2] > 0)
        {
            for (int i = 0; i < (int)colorEnum[2]; i++)
            {
                sb.Append('0');
            }
        }

        var colorInt = int.Parse(sb.ToString());
        bool isKilo = colorInt % 1000 == 0;
        string suffix = isKilo ? "kiloohms" : "ohms";
        var result = isKilo ? (colorInt / 1000).ToString() : colorInt.ToString();
        return $"{result} {suffix}";
    }
}
