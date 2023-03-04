using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class FlattenArray
{
    public static IEnumerable Flatten(IEnumerable input) =>
        Flatten(input.Cast<object>());

    private static IEnumerable<T> Flatten<T>(IEnumerable<T> input) =>
        input.SelectMany(o => o is IEnumerable<T> list ? Flatten(list) : new T[] { o }).Where(item => item != null);
}