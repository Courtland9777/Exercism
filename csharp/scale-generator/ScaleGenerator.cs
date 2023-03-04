using System;
using System.Linq;

public static class ScaleGenerator
{
    private static readonly string[] ChromaticSharp = new[] { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };
    private static readonly string[] ChromaticFlat = new[] { "A", "Bb", "B", "C", "Db", "D", "Eb", "E", "F", "Gb", "G", "Ab" };

    public static string[] Chromatic(string tonic) =>
        Enumerable.Range(GetIndex(tonic), 12).Select(n => SharpOrFlat(tonic)[n % 12]).ToArray();

    public static string[] Interval(string tonic, string pattern)
    {
        var index = GetIndex(tonic);
        var result = new string[pattern.Length];
        for (int i = 0; i < pattern.Length; i++)
        {
            result[i] = SharpOrFlat(tonic)[index];
            index = pattern[i] switch
            {
                'm' => index + 1,
                'M' => index + 2,
                'A' => index + 3,
                _ => throw new ArgumentException()
            } % 12;
        }

        return result;
    }

    private static int GetIndex(string tonic) =>
        Array.FindIndex(SharpOrFlat(tonic), note => note.Equals(tonic, StringComparison.OrdinalIgnoreCase));

    private static string[] SharpOrFlat(string note) =>
        new[] { "C", "G", "D", "A", "E", "B", "F#", "a", "e", "b", "f#", "c#", "g#", "d#" }.Any(x => x.Equals(note)) ? ChromaticSharp : ChromaticFlat;
}