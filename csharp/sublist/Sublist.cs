using System;
using System.Collections.Generic;
using System.Linq;

public enum SublistType
{
    Equal,
    Unequal,
    Superlist,
    Sublist
}

public static class Sublist
{
    public static SublistType Classify<T>(List<T> list1, List<T> list2)
        where T : IComparable
    {
        if (list1.Count == list2.Count)
        {
            foreach (var (x, y) in list1.Zip(list2))
            {
                if (x.CompareTo(y) != 0)
                {
                    return SublistType.Unequal;
                }
            }

            return SublistType.Equal;
        }

        if (list1.Count == 0)
        {
            return SublistType.Sublist;
        }

        if (list2.Count == 0)
        {
            return SublistType.Superlist;
        }

        if (list1.Count > list2.Count)
        {
            for (var i = 0; i <= list1.Count - list2.Count; i++)
            {
                if (list2.TakeWhile((t, j) => list1[i + j].CompareTo(t) == 0).Where((t, j) => j == list2.Count - 1)
                    .Any())
                {
                    return SublistType.Superlist;
                }
            }

            return SublistType.Unequal;
        }

        for (var i = 0; i <= list2.Count - list1.Count; i++)
        {
            if (list1.TakeWhile((t, j) => list2[i + j].CompareTo(t) == 0).Where((t, j) => j == list1.Count - 1).Any())
            {
                return SublistType.Sublist;
            }
        }

        return SublistType.Unequal;
    }
}