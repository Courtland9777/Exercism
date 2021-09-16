using System;
using System.Linq;
using System.Text;

public static class RunLengthEncoding
{
    public static string Encode(string input) =>
        string.IsNullOrEmpty(input) ? string.Empty : string.Concat(input.Skip(1)
        .Aggregate((t: input[0].ToString(), o: Enumerable.Empty<string>()),
           (a, c) => a.t[0] == c ? (a.t + c, a.o) : (c.ToString(), a.o.Append(a.t)),
           a => a.o.Append(a.t).Select(p => p.Length > 1 ? $"{p.Length}{p[0]}" : $"{p[0]}")));

    public static string Decode(string input)
    {
        var sb = new StringBuilder();
        var countBuilder = string.Empty;

        foreach (var c in input)
        {
            if (char.IsDigit(c))
            {
                countBuilder = $"{countBuilder}{c}";
                continue;
            }

            if(countBuilder.Length == 0)
            {
                sb.Append(c);
                continue;
            }
  
            for (int j = 0; j < int.Parse(countBuilder); j++)
            {
                sb.Append(c);
            }
            countBuilder = string.Empty;
        }

        return sb.ToString();
    }
}
