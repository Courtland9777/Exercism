using System;

public class Queen
{
    public int Row { get; }
    public int Column { get; }

    public Queen(int row, int column)
    {
        Row = row;
        Column = column;
    }
}

public static class QueenAttack
{
    public static bool CanAttack(Queen white, Queen black) =>
        white.Row == black.Row || white.Column == black.Column || 
        Math.Abs(white.Column - black.Column) == Math.Abs(white.Row - black.Row);

    public static Queen Create(int row, int column) => row is < 0 or > 7 || column is < 0 or > 7
        ? throw new ArgumentOutOfRangeException()
        : new Queen(row, column);
}