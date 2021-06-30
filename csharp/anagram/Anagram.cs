using System;
using System.Linq;

public class Anagram
{
    private readonly string _baseWord;
    private readonly string _sortedBaseWordLetters;

    public Anagram(string baseWord)
    {
        _baseWord = baseWord;
        _sortedBaseWordLetters = SortStringAsc(baseWord);
    }

    private static string SortStringAsc(string stringToSort) =>
        string.Concat(stringToSort.ToLower().ToCharArray().OrderBy(x => x));

    public string[] FindAnagrams(string[] potentialMatches) =>
        potentialMatches.Where(potentialMatch =>
            SortStringAsc(potentialMatch).Equals(_sortedBaseWordLetters, StringComparison.OrdinalIgnoreCase) &&
            !_baseWord.Equals(potentialMatch,StringComparison.OrdinalIgnoreCase)).ToArray();
}