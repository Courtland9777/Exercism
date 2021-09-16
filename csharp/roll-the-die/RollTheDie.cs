using System;

public class Player
{
    private readonly Random _rnd;

    public Player()
    {
        _rnd = new Random();
    }

    public int RollDie() =>
        _rnd.Next(1,19);

    public double GenerateSpellStrength() =>
        _rnd.Next(0,101);
}
