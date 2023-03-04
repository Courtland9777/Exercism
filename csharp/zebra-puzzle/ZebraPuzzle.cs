using System;
using System.Collections.Generic;
using System.Linq;

public enum Nationality { Norwegian, Japanese, Englishman, Ukrainian, Spaniard }

public enum Drink { Water, OrangeJuice, Tea, Milk, Coffee }

public enum Pet { Zebra, Dog, Snails, Horse, Fox }

public enum Smoke { OldGold, Parliaments, Kools, LuckyStrike, Chesterfields }

public enum Color { Green, Blue, Red, Ivory, Yellow }

public static class Position
{
    public static int First = 0;
    public static int Second = 1;
    public static int Middle = 2;
    public static int Fourth = 3;
    public static int Last = 4;
}

internal class Person
{
    public Color Color;
    public Drink Drink;
    public Nationality Nation;
    public Pet Pet;
    public Smoke Smoke;
}

public static class ZebraPuzzle
{
    private static readonly int[][] perms =
        Permutations.AllPermutations(new[] { 0, 1, 2, 3, 4 }).ToArray();

    private static IEnumerable<Person> Solution { get; } = Solve().FirstOrDefault();
    public static Nationality DrinksWater() => Solution.First(p => p.Drink == Drink.Water).Nation;
    public static Nationality OwnsZebra() => Solution.First(p => p.Pet == Pet.Zebra).Nation;

    private static Dictionary<T, int> AsDict<T>(int[] a) =>
        a.Zip(Enum.GetValues(typeof(T)).Cast<T>(), (v, k) => new { k, v })
            .ToDictionary(x => x.k, x => x.v);

    private static bool NextTo(int a, int b) => Math.Abs(a - b) == 1;

    internal static IEnumerable<IEnumerable<Person>> Solve() =>
        perms.Select(AsDict<Nationality>)
            .Where(nations => nations[Nationality.Norwegian] == Position.First)
            .Join(
                perms.Select(AsDict<Color>)
                    .Where(c => c[Color.Green] == c[Color.Ivory] + 1)
                    .Where(c => c[Color.Blue] == Position.Second), nations => nations[Nationality.Englishman],
                colors => colors[Color.Red],
                (nations, colors) => new { nations, colors })
            .Join(perms.Select(AsDict<Smoke>), t => (t.colors[Color.Yellow], t.nations[Nationality.Japanese]),
                smokes => (smokes[Smoke.Kools], smokes[Smoke.Parliaments]), (t, smokes) => new { t, smokes })
            .Join(perms.Select(AsDict<Pet>), t => (t.t.nations[Nationality.Spaniard], t.smokes[Smoke.OldGold]),
                pets => (pets[Pet.Dog], pets[Pet.Snails]), (t, pets) => new { t, pets })
            .Where(t =>
                NextTo(t.t.smokes[Smoke.Chesterfields], t.pets[Pet.Fox]) &&
                NextTo(t.t.smokes[Smoke.Kools], t.pets[Pet.Horse]))
            .Join(perms.Select(AsDict<Drink>),
                t => (Position.Middle, t.t.t.colors[Color.Green], t.t.t.nations[Nationality.Ukrainian],
                    t.t.smokes[Smoke.LuckyStrike]),
                drinks => (drinks[Drink.Milk], drinks[Drink.Coffee], drinks[Drink.Tea], drinks[Drink.OrangeJuice]),
                (t, drinks) => t.t.t.nations
                    .Join(t.t.t.colors, kn => kn.Value, kc => kc.Value, (kn, kc) => new { kn, kc })
                    .Join(drinks, t1 => t1.kn.Value, kd => kd.Value, (t1, kd) => new { t1, kd })
                    .Join(t.t.smokes, t1 => t1.t1.kn.Value, ks => ks.Value, (t1, ks) => new { t1, ks })
                    .Join(t.pets, t1 => t1.t1.t1.kn.Value, kp => kp.Value,
                        (t1, kp) => new Person
                        {
                            Nation = t1.t1.t1.kn.Key,
                            Color = t1.t1.t1.kc.Key,
                            Drink = t1.t1.kd.Key,
                            Smoke = t1.ks.Key,
                            Pet = kp.Key
                        }));
}

public static class Permutations
{
    public static IEnumerable<T[]> AllPermutations<T>(T[] set) where T : IComparable
    {
        set = set.ToArray();
        yield return set.ToArray();
        while (NextPermutation(set))
        {
            yield return set.ToArray();
        }
    }

    public static bool NextPermutation<T>(T[] set) where T : IComparable
    {
        var largestIndex = -1;
        for (var i = set.Length - 2; i >= 0; i--)
        {
            if (set[i].CompareTo(set[i + 1]) >= 0)
            {
                continue;
            }

            largestIndex = i;
            break;
        }

        if (largestIndex < 0)
        {
            return false;
        }

        var largestIndex2 = -1;
        for (var i = set.Length - 1; i >= 0; i--)
        {
            if (set[largestIndex].CompareTo(set[i]) >= 0)
            {
                continue;
            }

            largestIndex2 = i;
            break;
        }

        var tmp = set[largestIndex];
        set[largestIndex] = set[largestIndex2];
        set[largestIndex2] = tmp;
        for (int i = largestIndex + 1, j = set.Length - 1; i < j; i++, j--)
        {
            tmp = set[i];
            set[i] = set[j];
            set[j] = tmp;
        }

        return true;
    }
}