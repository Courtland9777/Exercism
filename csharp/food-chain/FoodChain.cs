using System.Collections.Generic;
using System.Linq;

public static class FoodChain
{
    private static readonly Dictionary<int, string[]> AnimalsAndReasons = new()
    {
        { 1, new[] { "fly", "I don't know why she swallowed the fly. Perhaps she'll die." } },
        { 2, new[] { "spider", "It wriggled and jiggled and tickled inside her." } },
        { 3, new[] { "bird", "How absurd to swallow a bird!" } },
        { 4, new[] { "cat", "Imagine that, to swallow a cat!" } },
        { 5, new[] { "dog", "What a hog, to swallow a dog!" } },
        { 6, new[] { "goat", "Just opened her throat and swallowed a goat!" } },
        { 7, new[] { "cow", "I don't know how she swallowed a cow!" } },
        { 8, new[] { "horse", "She's dead, of course!" } }
    };

    public static string Recite(int verseNumber)
    {
        var animalAndReason = AnimalsAndReasons[verseNumber];
        var animal = animalAndReason[0];
        var reason = animalAndReason[1];

        var result = $"I know an old lady who swallowed a {animal}.\n{reason}\n";

        if (verseNumber is 8 or 1)
        {
            return result[..^1];
        }

        for (var i = verseNumber; i > 1; i--)
        {
            var prey = AnimalsAndReasons[i - 1][0];
            var action = i == 3 ? " that wriggled and jiggled and tickled inside her" : string.Empty;

            result += $"She swallowed the {animal} to catch the {prey}{action}.\n";
            animal = prey;
        }

        result += $"{AnimalsAndReasons[1][1]}\n";
        return result[..^1];
    }

    public static string Recite(int startVerse, int endVerse) =>
        string.Join("\n",
            Enumerable.Range(startVerse, endVerse - startVerse + 1).Select(x => $"{Recite(x)}\n"))[..^1];
}