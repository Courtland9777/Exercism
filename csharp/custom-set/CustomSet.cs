using System;
using System.Collections.Immutable;
using System.Linq;

public class CustomSet : IEquatable<CustomSet>
{
    public static readonly CustomSet EmptySet = new(ImmutableList<int>.Empty);

    //values in list are sorted
    private readonly ImmutableList<int> _values;
    private CustomSet(ImmutableList<int> values) => _values = values;

    public CustomSet(params int[] values) =>
        _values = values == null || values.Length == 0
            ? ImmutableList<int>.Empty
            : ImmutableList.CreateRange(values).Sort();

    public bool Equals(CustomSet other) =>
        (other == null || _values.Count == other._values.Count) &&
        !_values.Where((t, i) => other != null && t != other._values[i]).Any();

    public CustomSet Add(int value)
    {
        var index = _values.BinarySearch(value);
        return index < 0
            ? new CustomSet(_values.Insert(~index, value))
            : this;
    }

    public bool Empty() => _values.IsEmpty;
    public bool Contains(int value) => _values.BinarySearch(value) >= 0;

    public bool Subset(CustomSet right)
    {
        if (Empty() || ReferenceEquals(right, this))
        {
            return true;
        }

        if (right.Empty())
        {
            return false;
        }

        int leftIndex = 0, rightIndex = 0;
        do
        {
            var leftValue = _values[leftIndex];
            var rightValue = right._values[rightIndex];
            if (leftValue < rightValue)
            {
                return false;
            }

            if (leftValue > rightValue)
            {
                ++rightIndex;
            }
            else
            {
                ++leftIndex;
                ++rightIndex;
            }
        } while (leftIndex < _values.Count &&
                 rightIndex < right._values.Count);

        return leftIndex >= _values.Count;
    }

    public bool Disjoint(CustomSet right)
    {
        if (Empty() || right.Empty())
        {
            return true;
        }

        if (ReferenceEquals(right, this))
        {
            return false;
        }

        int leftIndex = 0, rightIndex = 0;
        do
        {
            var leftValue = _values[leftIndex];
            var rightValue = right._values[rightIndex];
            if (leftValue < rightValue)
            {
                ++leftIndex;
            }
            else if (leftValue > rightValue)
            {
                ++rightIndex;
            }
            else
            {
                return false;
            }
        } while (leftIndex < _values.Count &&
                 rightIndex < right._values.Count);

        return true;
    }

    public CustomSet Intersection(CustomSet right)
    {
        if (Empty() || right.Empty())
        {
            return EmptySet;
        }

        if (ReferenceEquals(right, this))
        {
            return this;
        }

        int leftIndex = 0, rightIndex = 0;
        var intersectionValues = ImmutableList.CreateBuilder<int>();
        do
        {
            var leftValue = _values[leftIndex];
            var rightValue = right._values[rightIndex];
            if (leftValue < rightValue)
            {
                ++leftIndex;
            }
            else if (leftValue > rightValue)
            {
                ++rightIndex;
            }
            else //if (leftValue == rightValue)
            {
                intersectionValues.Add(leftValue);
                ++leftIndex;
                ++rightIndex;
            }
        } while (leftIndex < _values.Count &&
                 rightIndex < right._values.Count);

        return new CustomSet(intersectionValues.ToImmutable());
    }

    public CustomSet Difference(CustomSet right)
    {
        if (right.Empty())
        {
            return this;
        }

        if (Empty() || ReferenceEquals(right, this))
        {
            return EmptySet;
        }

        var nonSharedValues = ImmutableList.CreateBuilder<int>();
        var index = 0;
        foreach (var value in right._values)
        {
            while (_values[index] < value)
            {
                nonSharedValues.Add(_values[index]);
                if (++index >= _values.Count)
                {
                    return new CustomSet(nonSharedValues.ToImmutableList());
                }
            }

            if (_values[index] == value && ++index >= _values.Count)
            {
                return new CustomSet(nonSharedValues.ToImmutableList());
            }
        }

        while (index < _values.Count)
        {
            nonSharedValues.Add(_values[index++]);
        }

        return new CustomSet(nonSharedValues.ToImmutableList());
    }

    public CustomSet Union(CustomSet right)
    {
        if (right.Empty() || ReferenceEquals(right, this))
        {
            return this;
        }

        if (Empty())
        {
            return right;
        }

        int leftIndex = 0, rightIndex = 0;
        var unionValues = ImmutableList.CreateBuilder<int>();
        do
        {
            var leftValue = _values[leftIndex];
            var rightValue = right._values[rightIndex];
            if (leftValue < rightValue)
            {
                unionValues.Add(leftValue);
                ++leftIndex;
            }
            else if (leftValue > rightValue)
            {
                unionValues.Add(rightValue);
                ++rightIndex;
            }
            else //if (leftValue == rightValue)
            {
                unionValues.Add(leftValue);
                ++leftIndex;
                ++rightIndex;
            }
        } while (leftIndex < _values.Count &&
                 rightIndex < right._values.Count);

        while (leftIndex < _values.Count)
        {
            unionValues.Add(_values[leftIndex++]);
        }

        while (rightIndex < right._values.Count)
        {
            unionValues.Add(right._values[rightIndex++]);
        }

        return new CustomSet(unionValues.ToImmutable());
    }

    public override bool Equals(object obj) => obj is CustomSet set && Equals(set);
}