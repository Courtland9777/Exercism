using System;
using System.Collections.Generic;
using System.Linq;

using Sprache;

public class SgfTree : IEquatable<SgfTree>
{
    public SgfTree(IDictionary<string, string[]> data, params SgfTree[] children)
    {
        Data = data;
        Children = children;
    }

    public IDictionary<string, string[]> Data { get; }
    public SgfTree[] Children { get; }

    public bool Equals(SgfTree other) =>
        Data.All(d =>
            other.Data.TryGetValue(d.Key, out var otherValue) &&
            d.Value.SequenceEqual(otherValue))
        && Children?.SequenceEqual(other.Children) == true;
}

public static class SgfParser
{
    private static readonly Parser<char> NormalChar = Parse.Char(c => c != '\\' && c != ']', "Normal char");
    private static readonly Parser<char> EscapedChar = Parse.Char('\\').Then(_ => Parse.AnyChar.Select(UnEscape));

    private static readonly Parser<string> CValueType =
        EscapedChar.Or(NormalChar).Many().Select(chars => new string(chars.ToArray()));

    private static char UnEscape(char c) => c switch { 'n' => '\n', 'r' or 't' => ' ', ']' => ']', _ => c };

    private static Parser<string> Value() =>
        CValueType.Contained(Parse.Char('['), Parse.Char(']'));

    private static Parser<KeyValuePair<string, string[]>> Property() =>
        Parse.Upper.Select(c => c.ToString())
            .SelectMany(key => Value().AtLeastOnce().Select(values => values.ToArray()),
                (key, values) => new KeyValuePair<string, string[]>(key, values));

    private static Parser<Dictionary<string, string[]>> Properties() =>
        Property()
            .Many()
            .Select(properties => properties.ToDictionary(p => p.Key, p => p.Value))
            .Select(properties => properties);

    private static Parser<Dictionary<string, string[]>> Node() =>
        from _ in Parse.Char(';')
        from properties in Properties()
        select properties;

    private static Parser<SgfTree> Tree() =>
        Parse.Char('(')
            .SelectMany(open => Node().Many(), (open, nodes) => new { open, nodes })
            .SelectMany(t => Tree().Many(), (t, children) => new { t, children })
            .SelectMany(t => Parse.Char(')'), (t, close) => NodesToTree(t.t.nodes, t.children));

    public static SgfTree ParseTree(string input)
    {
        try
        {
            return Tree().Parse(input);
        }
        catch (Exception e)
        {
            throw new ArgumentException(nameof(input), e);
        }
    }

    private static SgfTree NodesToTree(IEnumerable<Dictionary<string, string[]>> properties, IEnumerable<SgfTree> trees)
    {
        var propertiesList = properties.ToList();
        var numberOfProperties = propertiesList.Count;
        return numberOfProperties switch
        {
            0 => throw new InvalidOperationException("Can only create tree from non-empty nodes list"),
            1 => new SgfTree(propertiesList.First(), trees.ToArray()),
            _ => new SgfTree(propertiesList.First(), NodesToTree(propertiesList.Skip(1), trees))
        };
    }
}