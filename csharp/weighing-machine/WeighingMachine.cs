using System;
using System.ComponentModel;

enum Units
{
    Pounds,
    Kilograms
}

class WeighingMachine
{
    private const decimal KgPerLb = 2.20462m;
    private decimal _inputWeight;
    private Units _unit;

    public WeighingMachine()
    {
        _unit = Units.Kilograms;
    }

    public decimal InputWeight
    {
        get => _inputWeight;
        set
        {
            _inputWeight = value switch
            {
                < 0 => throw new ArgumentOutOfRangeException(nameof(value), "Input can't be a negative value"),
                _ => value
            };
        }
    }

    public decimal DisplayWeight =>
        _inputWeight - TareAdjustment;

    public USWeight USDisplayWeight =>
        _unit == Units.Pounds ? new USWeight(_inputWeight) : new USWeight(_inputWeight * KgPerLb);

    public Units Units { set => _unit = value; }

    public decimal TareAdjustment { get; set; }
}

struct USWeight
{
    private const decimal OuncesPerLb = 16m;
    private readonly decimal _weightInPounds;

    public USWeight(decimal weightInPounds)
    {
        _weightInPounds = weightInPounds;
    }

    public decimal Pounds => Math.Floor(_weightInPounds);

    public decimal Ounces => Math.Floor((_weightInPounds - Pounds) * OuncesPerLb);
}
