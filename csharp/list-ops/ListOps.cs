using System;
using System.Collections.Generic;
using System.Linq;

public static class ListOps
{
    public static int Length<T>(List<T> input) => input.Count;

    public static List<T> Reverse<T>(List<T> input)
    {
        throw new NotImplementedException("You need to implement this function.");
    }

    public static List<TOut> Map<TIn, TOut>(List<TIn> input, Func<TIn, TOut> map) =>
        input.ConvertAll(x => map(x));

    public static List<T> Filter<T>(List<T> input, Func<T, bool> predicate) =>
        input.Where(x => predicate(x)).ToList();

    public static TOut Foldl<TIn, TOut>(List<TIn> input, TOut start, Func<TOut, TIn, TOut> func) =>
        input.Folder(start, func );

    public static TOut Folder<TIn, TOut>(this IEnumerable<TIn> list, TOut start, Func<TOut, TIn, TOut> func)
    {
        bool containsValue = false;
        var firstValue = start;
        bool firstValueSet = false;
        using IEnumerator<TIn> iEnumerator = list.GetEnumerator();
        while (iEnumerator.MoveNext())
        {
            containsValue = true;
            TOut tempValue = iEnumerator.Current;
            if (firstValueSet)
                tempValue = func(firstValue, tempValue);
            firstValueSet = true;
            firstValue = tempValue;
        }
        return containsValue ? firstValue : default;
    }

    public static TOut Foldr<TIn, TOut>(List<TIn> input, TOut start, Func<TIn, TOut, TOut> func)
    {
        var list = new List<TIn>();
        for (int i = input.Count - 1; i >= 0; i--)
        {
            list.Add(input[i]);
        }
        return list.Aggregate(start, func);
    }

    public static List<T> Concat<T>(List<List<T>> input) =>
        input.SelectMany(list => list).ToList();

    public static List<T> Append<T>(List<T> left, List<T> right) =>
        left == null
        ? right
        : right == null ? left : left.Concat(right).ToList();
}