using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Diamond
{
    public static string Make(char target)
    {
        if (target is < 'A' or > 'Z')
        {
            throw new ArgumentOutOfRangeException(nameof(target));
        }

        var maxId = target - 'A';
        var size = maxId * 2 + 1;
        var lastRowId = size - 1;
        var field = Enumerable.Range(0, size)
            .Select(row => Enumerable.Range(0, size)
                .Select(col => '\0')
                .ToArray())
            .ToArray();

        Enumerable.Range(0, maxId + 1)
            .ToList()
            .ForEach(id =>
            {
                var letter = (char)('A' + id);
                var rowB = lastRowId - id;
                var colA = maxId - id;
                var colB = maxId + id;
                field[id][colA] = letter;
                field[id][colB] = letter;
                field[rowB][colA] = letter;
                field[rowB][colB] = letter;
            });

        return SerializeField(field, size);
    }

    private static string SerializeField(IReadOnlyList<char[]> field, int size)
    {
        var sb = new StringBuilder();
        Enumerable.Range(0, size)
            .ToList()
            .ForEach(row =>
            {
                Enumerable.Range(0, size)
                    .ToList()
                    .ForEach(column =>
                    {
                        var targetChar = field[row][column];
                        if (targetChar == '\0')
                        {
                            targetChar = ' ';
                        }

                        sb.Append(targetChar);
                    });
                sb.Append('\n');
            });

        sb.Length--;
        return sb.ToString();
    }
}