using System.Collections.Generic;

public class Deque<T>
{
    private readonly LinkedList<T> m_cList = new();
    public void Push(T value) => m_cList.AddLast(value);

    public T Pop()
    {
        var last = m_cList.Last;
        m_cList.RemoveLast();
        return last.Value;
    }

    public void Unshift(T value) => m_cList.AddFirst(value);

    public T Shift()
    {
        var first = m_cList.First;
        m_cList.RemoveFirst();
        return first.Value;
    }
}