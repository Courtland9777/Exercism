using System.Collections.Generic;
using System.Linq;

public static class ProteinTranslation
{
    public static string[] Proteins(string strand)
    {
        var dict = GenerateDict();
        var list = new List<string>();
        foreach (var codon in CodonList(strand, 3))
        {
            var temp = dict[codon];
            if (temp.Equals("STOP")) return list.ToArray();
            list.Add(temp);
        }
        return list.ToArray();
    }

    private static IEnumerable<string> CodonList(string str, int chunkSize) =>
        Enumerable.Range(0, str.Length / chunkSize)
            .Select(i => str.Substring(i * chunkSize, chunkSize));

    private static Dictionary<string, string> GenerateDict() =>
        new Dictionary<string, string>
        {
            { "AUG", "Methionine" },
            { "UUU", "Phenylalanine" },
            { "UUC", "Phenylalanine" },
            { "UUA", "Leucine" },
            { "UUG", "Leucine" },
            { "UCU", "Serine" },
            { "UCC", "Serine" },
            { "UCA", "Serine" },
            { "UCG", "Serine" },
            { "UAU", "Tyrosine" },
            { "UAC", "Tyrosine" },
            { "UGU", "Cysteine" },
            { "UGC", "Cysteine" },
            { "UGG", "Tryptophan" },
            { "UAA", "STOP" },
            { "UAG", "STOP" },
            { "UGA", "STOP" }
        };
}