using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class CryptoSquare
{
    public static string NormalizedPlaintext(string plaintext)
    {
        var filter = new Regex("[a-zA-Z0-9]");
        var normalizedText = plaintext.Where(x => filter.IsMatch(x.ToString())).ToList();
        var text = string.Join("", normalizedText);
        return text.ToLower();
    }

    public static IEnumerable<string> PlaintextSegments(string plaintext)
    {
        var rows = (int)Math.Round(Math.Sqrt(plaintext.Length));
        int columns;
        if (Math.Sqrt(plaintext.Length) > rows)
        {
            columns = rows + 1;
        }
        else
        {
            columns = rows;
        }

        var encodedText = new List<string>();
        var paddedText = plaintext.PadRight(columns * rows);

        for (var i = 0; i < paddedText.Length; i += columns)
        {
            encodedText.Add(paddedText.Substring(i, columns));
        }

        return encodedText;
    }

    public static string Encoded(string plaintext) =>
        throw new NotImplementedException("You need to implement this function.");

    public static string Ciphertext(string plaintext)
    {
        var normalizedText = NormalizedPlaintext(plaintext);
        if (string.IsNullOrEmpty(normalizedText))
        {
            return "";
        }

        var textChunks = PlaintextSegments(normalizedText).ToList();
        var encodedText = new List<string>();
        for (var i = 0; i < textChunks[0].Length; i++)
        {
            var newWord = textChunks.Aggregate("", (current, t) => current + t[i]);

            encodedText.Add(newWord);
        }

        return string.Join(" ", encodedText);
    }
}