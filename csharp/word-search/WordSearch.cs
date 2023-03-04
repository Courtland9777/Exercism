using System.Collections.Generic;
using System.Linq;

public class WordSearch
{
    private static readonly (int, int) NotFound = (-1, -1);

    public WordSearch(string grid)
    {
        Puzzle = grid.Split('\n');
        Rows = Puzzle.Length;
        Columns = Puzzle[0].Length;
    }

    public static string[] Puzzle { get; set; }
    public static int Rows { get; set; }
    public static int Columns { get; set; }

    public Dictionary<string, ((int, int), (int, int))?> Search(string[] wordsToSearchFor) =>
        wordsToSearchFor.ToDictionary(word => word, word => SearchWord(word));

    private static ((int, int), (int, int))? SearchWord(string word)
    {
        for (var row = 0; row < Rows; row++)
        {
            var matches = FindMatchesInRow(Puzzle[row], word[0]);

            foreach (var column in matches)
            {
                var beginPosition = (row, column);
                var endPosition = FindEndPosition(beginPosition, word);

                if (endPosition != NotFound)
                {
                    return ReformatizePositions(beginPosition, endPosition);
                }
            }
        }

        return null;
    }

    private static IEnumerable<int> FindMatchesInRow(string row, char firstLetter) =>
        Enumerable.Range(0, Columns).Where(i => row[i] == firstLetter);

    private static (int, int) FindEndPosition((int, int) beginPosition, string word)
    {
        foreach (var direction in new[] { "U", "UR", "R", "RD", "D", "DL", "L", "UL" })
        {
            var endPosition = CheckLine(beginPosition, direction, word);

            if (endPosition != NotFound)
            {
                return endPosition;
            }
        }

        return NotFound;
    }

    private static (int, int) CheckLine((int, int) currentPosition, string direction, string word)
    {
        for (var index = 1; index < word.Length; index++)
        {
            currentPosition = Move(currentPosition, direction);

            if (IsOutsideOfPuzzle(currentPosition) ||
                Puzzle[currentPosition.Item1][currentPosition.Item2] != word[index])
            {
                return NotFound;
            }
        }

        return currentPosition;
    }

    private static (int, int) Move((int, int) current, string direction)
    {
        if (direction.Contains('R')) { current.Item2 += 1; }

        if (direction.Contains('D')) { current.Item1 += 1; }

        if (direction.Contains('L')) { current.Item2 -= 1; }

        if (direction.Contains('U')) { current.Item1 -= 1; }

        return current;
    }

    private static ((int, int), (int, int))? ReformatizePositions((int, int) beginPosition, (int, int) endPosition)
    {
        var beginFormatted = (beginPosition.Item2 + 1, beginPosition.Item1 + 1);
        var endFormatted = (endPosition.Item2 + 1, endPosition.Item1 + 1);

        return (beginFormatted, endFormatted);
    }

    private static bool IsOutsideOfPuzzle((int row, int col) currentPosition)
    {
        var isRowInside = Enumerable.Range(0, Rows).Contains(currentPosition.row);
        var isColInside = Enumerable.Range(0, Columns).Contains(currentPosition.col);

        return !(isRowInside && isColInside);
    }
}