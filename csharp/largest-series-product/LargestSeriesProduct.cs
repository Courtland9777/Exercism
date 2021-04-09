using System;
using System.Linq;

public static class LargestSeriesProduct
{
    public static long GetLargestProduct(string digits, int span)
    {
        if (span == 0 && digits.Length > 0) return 1;
        if (span != 0 && digits.Length == 0) throw new ArgumentException(nameof(digits));
        if (span < 0) throw new ArgumentException(nameof(digits));
        if (span > digits.Length) throw new ArgumentException(nameof(digits));
        if (span == 0 && digits.Length == 0) return 1;
        if (digits.Any(c=>!char.IsDigit(c))) throw new ArgumentException(nameof(digits));

        var largestProduct = 0;
        for (int i = 0; i <= digits.Length-span; i++)
        {
            var thisProduct = digits.Substring(i, span).ToCharArray().Select(x => int.Parse(x.ToString()))
                .Aggregate((a, b) => a * b);
            if (thisProduct > largestProduct) largestProduct = thisProduct;
        }

        return largestProduct;
    }
}