using System;
using System.Collections.Generic;
using System.Linq;

public static class OcrNumbers
{
    private static readonly Dictionary<string, char> Map = new()
    {
        {
            "   " +
            "  |" +
            "  |",
            '1'
        },
        {
            " _ " +
            " _|" +
            "|_ ",
            '2'
        },
        {
            " _ " +
            " _|" +
            " _|",
            '3'
        },
        {
            "   " +
            "|_|" +
            "  |",
            '4'
        },
        {
            " _ " +
            "|_ " +
            " _|",
            '5'
        },
        {
            " _ " +
            "|_ " +
            "|_|",
            '6'
        },
        {
            " _ " +
            "  |" +
            "  |",
            '7'
        },
        {
            " _ " +
            "|_|" +
            "|_|",
            '8'
        },
        {
            " _ " +
            "|_|" +
            " _|",
            '9'
        },
        {
            " _ " +
            "| |" +
            "|_|",
            '0'
        }
    };

    public static string Convert(string input)
    {
        var array = input.Split("\n");
        if (array.Length % 4 != 0 || array.Any(s => s.Length % 3 != 0))
        {
            throw new ArgumentException();
        }

        var multiLine = array.Take(array.Length / 4)
            .Select((_, i) => array.Skip(i * 4).Take(4))
            .Select(rows =>
                Enumerable.Range(0, input.IndexOf('\n') / 3)
                    .Select(col => string.Concat(rows.Take(3).Select(s => s.Substring(col * 3, 3))))
                    .Select(s => Map.GetValueOrDefault(s, '?')))
            .Select(digits => string.Concat(digits));
        return string.Join(",", multiLine);
    }
}