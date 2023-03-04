using System;
using System.Collections.Generic;
using System.Linq;

public enum ConnectWinner { None, Black, White }

public class Connect
{
    private readonly char[][] _board;

    public Connect(IEnumerable<string> board) =>
        _board = board.Select(CreateRow).ToArray();

    private static char[] CreateRow(string line) =>
        line.Replace(" ", "").ToCharArray();

    public ConnectWinner Result()
    {
        var visited = new HashSet<(int, int)>();
        var s = new Stack<(int, int, char)>(_board.Length * _board[0].Length);

        Enumerable.Range(0, _board.Length).ToList().ForEach(y => s.Push((0, y, 'X')));
        Enumerable.Range(0, _board[0].Length).ToList().ForEach(x => s.Push((x, 0, 'O')));

        while (s.Any())
        {
            var (x, y, token) = s.Pop();
            try
            {
                if (_board[y][x] != token || visited.Contains((x, y)))
                {
                    continue;
                }
            }
            catch (IndexOutOfRangeException) { continue; }

            switch (token)
            {
                case 'X' when x == _board[y].Length - 1:
                    return ConnectWinner.Black;
                case 'O' when y == _board.Length - 1:
                    return ConnectWinner.White;
            }

            visited.Add((x, y));
            Enumerable.Range(-1, 3).ToList().ForEach(m =>
                Enumerable.Range(-1, 3).ToList().ForEach(n =>
                {
                    if (m == n)
                    {
                        return;
                    }

                    s.Push((x + m, y + n, token));
                }));
        }

        return ConnectWinner.None;
    }
}