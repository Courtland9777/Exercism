using System;
using System.Linq;

public class Matrix
{
    private readonly int[,] _matrix;

    public Matrix(string input)
    {
        // Split the input string into rows
        var rows = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        // Determine the number of rows and columns in the matrix
        var numRows = rows.Length;
        var numCols = rows[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;

        // Initialize the matrix array
        _matrix = new int[numRows, numCols];

        // Populate the matrix array with the values from the input string
        for (var i = 0; i < numRows; i++)
        {
            var cols = rows[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (var j = 0; j < numCols; j++)
            {
                _matrix[i, j] = int.Parse(cols[j]);
            }
        }
    }

    public int[] Row(int row) =>
        // Get the specified row from the matrix and convert it to an array
        Enumerable.Range(0, _matrix.GetLength(1))
            .Select(col => _matrix[row - 1, col])
            .ToArray();

    public int[] Column(int col) =>
        // Get the specified column from the matrix and convert it to an array
        Enumerable.Range(0, _matrix.GetLength(0))
            .Select(row => _matrix[row, col - 1])
            .ToArray();
}