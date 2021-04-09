using System;
using System.Linq;

public static class Series
{
    public static string[] Slices(string numbers, int sliceLength) =>
        numbers.Length < sliceLength || sliceLength < 1 ? throw new ArgumentException(nameof(sliceLength)) :
            numbers.Length == 0 ? throw new ArgumentException(nameof(numbers)) :
            Enumerable.Range(0, numbers.Length - sliceLength + 1).Select(x => numbers.Substring(x, sliceLength))
                .ToArray();
}