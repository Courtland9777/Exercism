using System.Collections.Generic;
using System.Linq;

public static class PascalsTriangle
{
    public static IEnumerable<IEnumerable<int>> Calculate(int rows) =>
        Enumerable.Range(1, rows).Select(CreatePascalRow);

    private static IEnumerable<int> CreatePascalRow(int line)
    {
        int c = 1;
        var stack = new Stack<int>();
        for (int i = 1; i <= line; i++)
        {
            stack.Push(c);
            c = c * (line - i) / i;
        }

        return stack;
    }
}