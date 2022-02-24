using System;

public enum Direction
{
    North,
    East,
    South,
    West
}

public class RobotSimulator
{
    public RobotSimulator(Direction direction, int x, int y)
    {
        Direction = direction;
        X = x;
        Y = y;
    }

    public Direction Direction { get; private set; }

    public int X { get; private set; }

    public int Y { get; private set; }

    public void Move(string instructions)
    {
        foreach (var t in instructions)
        {
            switch (t)
            {
                case 'A':
                    Advance();
                    break;
                case 'R':
                    TurnRight();
                    break;
                case 'L':
                    TurnLeft();
                    break;
                default: throw new ArgumentException();
            };
        }
    }

    private void TurnLeft() =>
    Direction = Direction switch
    {
        Direction.North => Direction.West,
        Direction.South => Direction.East,
        Direction.East => Direction.North,
        Direction.West => Direction.South,
        _ => throw new ArgumentException()
    };

    private void TurnRight() =>
            Direction = Direction switch
            {
                Direction.North => Direction.East,
                Direction.South => Direction.West,
                Direction.East => Direction.South,
                Direction.West => Direction.North,
                _ => throw new ArgumentException()
            };

    private void Advance() => _ = Direction switch
    {
        Direction.North => Y++,
        Direction.South => Y--,
        Direction.East => X++,
        Direction.West => X--,
        _ => throw new ArgumentException()
    };
}