using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class Alphametics
{
    public static IDictionary<char, int> Solve(string equation)
    {
        var parts = new Regex("\\+|==").Split(equation).Select(p => p.Trim());
        var (letters, counts, numLeaders) = Analyze(parts);
        var weights = FindSolution(counts, numLeaders);
        return MakeSolution(letters, weights);
    }

    private static (char[] letters, long[] counts, int numLeaders) Analyze(IEnumerable<string> parts)
    {
        var charCount = new ConcurrentDictionary<char, long>();
        var leading = new HashSet<char>();
        var scale = -1;

        foreach (var part in parts.Reverse())
        {
            foreach (var ch in part.Reverse())
            {
                charCount.AddOrUpdate(ch, scale, (_, count) => count + scale);
                scale *= 10;
            }

            scale = 1;
            leading.Add(part[0]);
        }

        return BuildResultWithLeadingFirst(charCount, leading);
    }

    private static (char[] chars, long[] counts, int numLeaders) BuildResultWithLeadingFirst(
        IDictionary<char, long> charCount, IReadOnlySet<char> leading)
    {
        var letters = charCount.Keys.ToArray();
        var counts = charCount.Values.ToArray();
        var numLeaders = leading.Count;
        var items = charCount.OrderBy(cc => leading.Contains(cc.Key) ? -1 : 1);
        var i = 0;

        foreach (var item in items)
        {
            letters[i] = item.Key;
            counts[i] = item.Value;
            i++;
        }

        return (letters, counts, numLeaders);
    }

    private static IEnumerable<int> FindSolution(IReadOnlyCollection<long> counts, int numLeaders)
    {
        var numbers = Enumerable.Range(0, 10).ToArray();

        foreach (var weights in Variations(numbers, counts.Count, numLeaders))
        {
            if (counts.Zip(weights, (c, w) => c * w).Sum() == 0)
            {
                return weights;
            }
        }

        throw new ArgumentException("No solution found.");
    }

    private static IEnumerable<T[]> Variations<T>(T[] based, int k, int avoidPlacingFirstElementBeforePosition)
    {
        var itemCount = 0;

        foreach (var item in based)
        {
            itemCount++;

            if (avoidPlacingFirstElementBeforePosition > 0 && itemCount == 1)
            {
                continue;
            }

            if (k <= 1)
            {
                yield return new[] { item };
            }
            else
            {
                var remaining = based.Where(v => !v.Equals(item)).ToArray();

                foreach (var variant in Variations(remaining, k - 1, avoidPlacingFirstElementBeforePosition - 1))
                {
                    yield return Prepend(item, variant).ToArray();
                }
            }
        }
    }

    private static IEnumerable<T> Prepend<T>(T first, IEnumerable<T> rest)
    {
        yield return first;

        foreach (var item in rest)
        {
            yield return item;
        }
    }

    private static IDictionary<char, int> MakeSolution(IEnumerable<char> letters, IEnumerable<int> weights) =>
        letters.Zip(weights).ToDictionary(x => x.First, x => x.Second);
}