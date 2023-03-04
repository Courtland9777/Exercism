using System;
using System.Collections.Generic;
using System.Linq;

internal class Frame
{
    private const int AllPins = 10;
    private int _rollsAllowed = 2;

    public bool InProgress => Rolled < 2 && Score < AllPins;
    public bool Complete => Rolled >= _rollsAllowed;
    public int Rolled { get; private set; }
    public int Score { get; private set; }

    public void Add(int pins)
    {
        if (Complete)
        {
            throw new ArgumentException("Cannot add pins to a complete frame");
        }

        if (pins is < 0 or > AllPins)
        {
            throw new ArgumentException("Illegal number of pins");
        }

        if (_rollsAllowed == 2 && Score + pins > AllPins)
        {
            throw new ArgumentException($"Cannot hit more than {AllPins} pins in a frame");
        }

        Rolled++;
        Score += pins;
        if (Rolled < 3 && Score == AllPins)
        {
            _rollsAllowed = 3;
        }
    }
}

public class BowlingGame
{
    private const int FramesInGame = 10;
    private readonly List<Frame> _frames = new();

    public void Roll(int pins)
    {
        if (_frames.Count >= FramesInGame && _frames[FramesInGame - 1].Complete)
        {
            throw new ArgumentException("Cannot add rolls to a complete game");
        }

        foreach (var frame in IncompleteFrames())
        {
            frame.Add(pins);
        }
    }

    private IEnumerable<Frame> IncompleteFrames()
    {
        var incompleteFrames = _frames.Where(_ => !_.Complete);
        if (!incompleteFrames.Any(_ => _.InProgress))
        {
            _frames.Add(new Frame());
        }

        return incompleteFrames;
    }

    public int? Score()
    {
        var completeFrames = _frames.Where(_ => _.Complete).Take(FramesInGame);
        if (completeFrames.Count() != FramesInGame)
        {
            throw new ArgumentException("Game incomplete");
        }

        return completeFrames.Sum(f => f.Score);
    }
}