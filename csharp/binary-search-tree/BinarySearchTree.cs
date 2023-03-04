using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BinarySearchTree : IEnumerable<int>
{
    public BinarySearchTree(int value) => Value = value;

    public BinarySearchTree(IEnumerable<int> values)
    {
        var enumerable = values as int[] ?? values.ToArray();
        Value = enumerable.First();
        foreach (var value in enumerable.Skip(1))
        {
            Add(value);
        }
    }

    public int Value { get; }
    public BinarySearchTree Left { get; private set; }
    public BinarySearchTree Right { get; private set; }

    public IEnumerator<int> GetEnumerator()
    {
        if (Left != null)
        {
            foreach (var leftValue in Left)
            {
                yield return leftValue;
            }
        }

        yield return Value;
        if (Right == null)
        {
            yield break;
        }

        foreach (var rightValue in Right)
        {
            yield return rightValue;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public BinarySearchTree Add(int value)
    {
        if (value <= Value)
        {
            Left = Left?.Add(value) ?? new BinarySearchTree(value);
        }
        else
        {
            Right = Right?.Add(value) ?? new BinarySearchTree(value);
        }

        return this;
    }
}