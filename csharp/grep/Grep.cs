using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

public static class Grep
{
    public static string Match(string pattern, string flags, string[] files)
    {
        bool prefixNumber = flags.Contains("-n");
        bool ignoreCase = flags.Contains("-i");
        bool onlyFileName = flags.Contains("-l");
        bool invertResults = flags.Contains("-v");
        bool entireLine = flags.Contains("-x");
        bool onlyOneFile = files.Length == 1;

        var regexString = pattern;
        var regex = new ThreadLocal<Regex>(() =>
            new Regex(regexString, RegexOptions.Compiled | (ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None)));

        try
        {
            var matches = files.AsParallel()
                .AsOrdered()
                .WithMergeOptions(ParallelMergeOptions.NotBuffered)
                .SelectMany(
                    file => File.ReadLines(file)
                        .Zip(Enumerable.Range(1, int.MaxValue), (s, i) => new { Num = i, Text = s, File = file }),
                    (file, line) => new { file, line })
                .Where(@t => regex.Value != null && (entireLine ? invertResults ? !regex.Value.IsMatch(@t.line.Text) :
                    regex.Value.IsMatch(@t.line.Text) && pattern.Length == @t.line.Text.Length :
                    invertResults ? !regex.Value.IsMatch(@t.line.Text) : regex.Value.IsMatch(@t.line.Text)))
                .Select(@t => @t.line);


            var sb = new StringBuilder();
            foreach (var line in matches)
            {
                if (onlyFileName)
                {
                    if (!sb.ToString().Contains(line.File))
                    {
                        sb.Append($"{line.File}").Append('\n');
                    }
                }
                else
                {
                    if (onlyOneFile)
                    {
                        if (prefixNumber)
                        {
                            sb.Append($"{line.Num}:");
                        }
                        sb.Append($"{line.Text}").Append('\n');
                    }
                    else
                    {
                        if (prefixNumber)
                        {
                            sb.Append($"{line.File}:{line.Num}:{line.Text}").Append('\n');
                        }
                        else
                        {
                            sb.Append($"{line.File}:{line.Text}").Append('\n');
                        }
                    }
                }
            }
            return sb.ToString().TrimEnd('\r', '\n');
        }
        catch (AggregateException ae)
        {
            ae.Handle(e => { Console.WriteLine(e.Message); return true; });
        }
        return string.Empty;
    }
}
