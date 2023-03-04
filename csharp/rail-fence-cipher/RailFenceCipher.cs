using System;
using System.Linq;

public class RailFenceCipher
{
    public RailFenceCipher(int rails) => Rails = rails;
    public int Rails { get; }

    public string Encode(string input) =>
        string.Join("",
            input.Select((c, i) => (x: c, y: Bounce(i)))
                .OrderBy(a => a.y)
                .Select(b => b.x));

    public string Decode(string input) =>
        string.Join("",
            Enumerable.Range(0, input.Length)
                .Select(x => (a: x, b: Bounce(x)))
                .OrderBy(x => x.b)
                .Zip(input, (x, y) => (c: y, i: x.a))
                .OrderBy(x => x.i)
                .Select(x => x.c));

    private int Bounce(int i) =>
        Math.Abs((i + (Rails - 1)) % ((Rails - 1) * 2) - (Rails - 1));
}