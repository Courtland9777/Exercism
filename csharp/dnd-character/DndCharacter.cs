using System;
using System.Linq;

public class DndCharacter
{
    public int Strength { get; }
    public int Dexterity { get; }
    public int Constitution { get; }
    public int Intelligence { get; }
    public int Wisdom { get; }
    public int Charisma { get; }
    public int Hitpoints { get; }

    private DndCharacter()
    {
        Strength = Ability();
        Dexterity = Ability();
        Constitution = Ability();
        Intelligence = Ability();
        Wisdom = Ability();
        Charisma = Ability();
        Hitpoints = 10 + Modifier(Constitution);
    }

    public static int Modifier(int score) => (int)Math.Floor((decimal)(score - 10)/2);

    public static int Ability()
    {
        Random random = new Random();
        return Enumerable.Range(0,4).Select(n => Roll(random)).OrderBy(x => x).Skip(1).Sum();
    }

    private static int Roll(Random random) => random.Next(1, 7);

    public static DndCharacter Generate() => new();
}
