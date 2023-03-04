using System;
using System.Collections.Generic;
using System.Linq;

using E = System.Linq.Enumerable;

public static class Minesweeper
{
    public static TU Fwd<T, TU>(this T data, Func<T, TU> fun) => fun(data);

    private static IEnumerable<(int, int)> IndexMines(IEnumerable<string> input) =>
        input
            .SelectMany((row, r) =>
                row.Select((char m, int c) => (m, cell: (r, c))).Where(t => t.m == '*').Select(t => t.cell))
            .ToHashSet();

    private static IEnumerable<(int r, int c)> Cells(int row, int col) =>
        E.Range(row - 1, 3)
            .SelectMany(r => E.Range(col - 1, 3).Select(c => (r, c)))
            .ToHashSet();

    private static char ReportMines(char cell, IEnumerable<(int, int)> mines, IEnumerable<(int, int)> cells) =>
        cell == '*' ? '*' : cells.Intersect(mines).Count().Fwd(cnt => cnt == 0 ? ' ' : (char)('0' + cnt));

    public static string[] Annotate(string[] input)
    {
        var mines = IndexMines(input);
        return input
            .Select((row, r) => row.Select((cell, c) => ReportMines(cell, mines, Cells(r, c))).Fwd(string.Concat))
            .ToArray();
    }
}