using System.Collections.Generic;
using System.Linq;

public static class SaddlePoints
{
    public static IEnumerable<(int, int)> Calculate(int[,] matrix)
    {
        var rows = Enumerable.Range(0, matrix.GetLength(0))
            .Select(i => Enumerable.Range(0, matrix.GetLength(1)).Select(j => matrix[i, j]));
        var cols = Enumerable.Range(0, matrix.GetLength(1))
            .Select(j => Enumerable.Range(0, matrix.GetLength(0)).Select(i => matrix[i, j]));

        return Enumerable.Range(0, matrix.GetLength(0))
            .SelectMany(i => Enumerable.Range(0, matrix.GetLength(1))
                .Where(j => rows.ElementAt(i).All(x => matrix[i, j] >= x))
                .Where(j => cols.ElementAt(j).All(x => matrix[i, j] <= x))
                .Select(j => (i + 1, j + 1)));
    }
}