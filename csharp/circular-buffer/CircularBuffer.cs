using System;

public class CircularBuffer<T>
{
    private readonly T[] _buffer;
    private int _count;
    private int _readPosition;
    private int _writePosition;

    public CircularBuffer(int capacity)
    {
        if (capacity <= 0)
        {
            throw new ArgumentException("Capacity must be greater than zero.", nameof(capacity));
        }

        _buffer = new T[capacity];
    }

    public T Read()
    {
        if (_count == 0)
        {
            throw new InvalidOperationException("Buffer is empty.");
        }

        var value = _buffer[_readPosition];
        _buffer[_readPosition] = default(T);
        _readPosition = (_readPosition + 1) % _buffer.Length;
        _count--;
        return value;
    }

    public void Write(T value)
    {
        if (_count == _buffer.Length)
        {
            throw new InvalidOperationException("Buffer is full.");
        }

        _buffer[_writePosition] = value;
        _writePosition = (_writePosition + 1) % _buffer.Length;
        _count++;
    }

    public void Overwrite(T value)
    {
        if (_count < _buffer.Length)
        {
            Write(value);
        }
        else
        {
            _buffer[_writePosition] = value;
            _writePosition = (_writePosition + 1) % _buffer.Length;
            _readPosition = (_readPosition + 1) % _buffer.Length;
        }
    }

    public void Clear()
    {
        Array.Clear(_buffer, 0, _buffer.Length);
        _readPosition = 0;
        _writePosition = 0;
        _count = 0;
    }
}