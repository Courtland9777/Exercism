using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SimpleLinkedList<T> : IEnumerable<T>
{
    private List<T> List { get; }
    private int _index;

    public SimpleLinkedList(T value) => List = new List<T>() { value };

    public SimpleLinkedList(IEnumerable<T> values) => List = values.ToList();

    public T Value
    {
        get
        {
            var val = List[_index];
            _index = 0;
            return val;
        }
    }

    public SimpleLinkedList<T> Next
    {
        get
        {
            _index++;
            return _index >= List.Count ? null : this;
        }
    }

    public SimpleLinkedList<T> Add(T value)
    {
        List.Add(value);
        return this;
    }

    public IEnumerator<T> GetEnumerator() => List.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}