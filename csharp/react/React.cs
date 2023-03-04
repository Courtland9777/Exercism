using System;
using System.Collections.Generic;
using System.Linq;

public class Reactor
{
    public InputCell CreateInputCell(int value) => new(value);

    public ComputeCell CreateComputeCell(IEnumerable<Cell> producers, Func<int[], int> compute) =>
        new(producers, compute);
}

public abstract class Cell
{
    protected int _value;

    public virtual int Value
    {
        get => _value;
        set
        {
            _value = value;
            NotifyTheOtherClass?.Invoke(this, EventArgs.Empty);
        }
    }

    public event EventHandler NotifyTheOtherClass;
    public event EventHandler<int> Changed;

    protected void ValueChanged(object sender, EventArgs eventArgs)
    {
        if (_value != Value)
        {
            Changed?.Invoke(this, Value);
            _value = Value;
        }

        NotifyTheOtherClass?.Invoke(this, EventArgs.Empty);
    }
}

public class InputCell : Cell
{
    public InputCell(int value) => _value = value;
}

public class ComputeCell : Cell
{
    private readonly IEnumerable<Cell> _cells;
    private readonly Func<int[], int> _compute;

    public ComputeCell(IEnumerable<Cell> cells, Func<int[], int> compute)
    {
        _cells = cells;
        _compute = compute;
        _value = Value;
        SubscribeToNotifications();
    }

    public override int Value => _compute.Invoke(_cells.Select(i => i.Value).ToArray());

    private void SubscribeToNotifications()
    {
        foreach (var cell in _cells)
        {
            cell.NotifyTheOtherClass += ValueChanged;
        }
    }
}