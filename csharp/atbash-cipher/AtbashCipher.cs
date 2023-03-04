using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class AtbashCipher
{
    public static IEnumerable<string> Split(this string str, int n) => Enumerable
        .Range(0, str.Length % 5 != 0 ? str.Length / n + 1 : str.Length / n).Select(i =>
            str.Length < i * n + 5 ? str[(i * n)..] : str.Substring(i * n, n));

    public static string Encode(string plainValue)
    {
        var encodedString = Decode(plainValue);
        return string.Join(" ", encodedString.Split(5)).Trim();
    }

    public static string Decode(string encodedValue)
    {
        var sb = new StringBuilder();
        foreach (var c in encodedValue.ToLower())
        {
            sb.Append(c switch
            {
                _ when char.IsDigit(c) => c,
                _ when char.IsLetter(c) => (char)('z' - c + 'a'),
                _ => string.Empty
            });
        }

        return sb.ToString().Trim();
    }
}