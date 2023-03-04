using System;
using System.Linq;
using System.Text.RegularExpressions;

public static class Markdown
{
    private static string Wrap(string text, string tag) => $"<{tag}>{text}</{tag}>";

    private static string Parse(string markdown, string delimiter, string tag)
    {
        var pattern = $"{delimiter}(.+){delimiter}";
        var replacement = $"<{tag}>$1</{tag}>";
        return Regex.Replace(markdown, pattern, replacement);
    }

    private static string Parse__(string markdown) => Parse(markdown, "__", "strong");

    private static string Parse_(string markdown) => Parse(markdown, "_", "em");

    private static string ParseText(string markdown, bool list)
    {
        var parsedText = Parse_(Parse__(markdown));

        return list ? parsedText : Wrap(parsedText, "p");
    }

    private static string ParseHeader(string markdown, bool list, out bool inListAfter)
    {
        var count = 0;

        foreach (var c in markdown)
        {
            if (c == '#')
            {
                count++;
            }
            else
            {
                break;
            }
        }

        if (count is 0 or > 6)
        {
            inListAfter = list;
            return null;
        }

        var headerTag = $"h{count}";
        var headerHtml = Wrap(markdown[(count + 1)..], headerTag);

        if (list)
        {
            inListAfter = false;
            return $"</ul>{headerHtml}";
        }

        inListAfter = false;
        return headerHtml;
    }

    private static string ParseLineItem(string markdown, bool list, out bool inListAfter)
    {
        if (markdown.StartsWith("*"))
        {
            var innerHtml = Wrap(ParseText(markdown[2..], true), "li");

            if (list)
            {
                inListAfter = true;
                return innerHtml;
            }

            inListAfter = true;
            return $"<ul>{innerHtml}";
        }

        inListAfter = list;
        return null;
    }

    private static string ParseParagraph(string markdown, bool list, out bool inListAfter)
    {
        if (!list)
        {
            inListAfter = false;
            return ParseText(markdown, false);
        }

        inListAfter = false;
        return $"</ul>{ParseText(markdown, false)}";
    }

    private static string ParseLine(string markdown, bool list, out bool inListAfter)
    {
        var result = ParseHeader(markdown, list, out inListAfter)
                     ?? ParseLineItem(markdown, list, out inListAfter)
                     ?? ParseParagraph(markdown, list, out inListAfter);

        return result ?? throw new ArgumentException("Invalid markdown");
    }

    public static string Parse(string markdown)
    {
        var lines = markdown.Split('\n');
        var list = false;

        var result = lines.Aggregate("", (current, line) =>
            $"{current}{ParseLine(line, list, out list)}");

        return list ? $"{result}</ul>" : result;
    }
}