using System;
using System.Collections.Generic;
using System.Linq;

public enum Bucket
{
    One,
    Two
}

public class TwoBucketResult
{
    public int Moves { get; set; }
    public Bucket GoalBucket { get; set; }
    public int OtherBucket { get; set; }
}

public class TwoBucket
{
    private readonly int[] _sizes;
    private readonly int _startBucket;

    public TwoBucket(int bucketOne, int bucketTwo, Bucket startBucket)
    {
        _sizes = new[] { bucketOne, bucketTwo };
        _startBucket = (int)startBucket;
    }

    private static int[] Empty(IReadOnlyList<int> buckets, int i) =>
        i == 0 ? new[] { 0, buckets[1] } : new[] { buckets[0], 0 };

    private int[] Fill(IReadOnlyList<int> buckets, int i) =>
        i == 0 ? new[] { _sizes[0], buckets[1] } : new[] { buckets[0], _sizes[1] };

    private int[] Consolidate(IReadOnlyList<int> buckets, int i)
    {
        var amount = new[] { buckets[1 - i], _sizes[i] - buckets[i] }.Min();
        var target = buckets[i] + amount;
        var src = buckets[1 - i] - amount;
        return i == 0 ? new[] { target, src } : new[] { src, target };
    }

    public TwoBucketResult Measure(int goal)
    {
        var invalid = new[] { 0, 0 };
        invalid[1 - _startBucket] = _sizes[1 - _startBucket];
        var invalidStr = string.Join(",", invalid);
        var buckets = new[] { 0, 0 };
        buckets[_startBucket] = _sizes[_startBucket];
        var toVisit = new Queue<(int[], int)>();
        var visited = new HashSet<string>();
        var count = 1;
        var goalBucket = Array.IndexOf(buckets, goal);

        while (goalBucket < 0)
        {
            var key = string.Join(",", buckets);
            if (!visited.Contains(key) && !key.Equals(invalidStr))
            {
                visited.Add(key);
                var nc = count + 1;
                for (var i = 0; i < 2; i++)
                {
                    if (buckets[i] != 0)
                    {
                        toVisit.Enqueue((Empty(buckets, i), nc));
                    }

                    if (buckets[i] == _sizes[i])
                    {
                        continue;
                    }

                    toVisit.Enqueue((Fill(buckets, i), nc));
                    toVisit.Enqueue((Consolidate(buckets, i), nc));
                }
            }

            if (!toVisit.Any())
            {
                throw new ArgumentException("no more moves!");
            }

            (buckets, count) = toVisit.Dequeue();
            goalBucket = Array.IndexOf(buckets, goal);
        }

        return new TwoBucketResult
        {
            Moves = count,
            GoalBucket = (Bucket)goalBucket,
            OtherBucket = buckets[1 - goalBucket]
        };
    }
}