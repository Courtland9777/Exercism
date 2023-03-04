using System;
using System.Globalization;
using System.Linq;
using System.Text;

public class LedgerEntry
{
    public LedgerEntry(DateTime date, string desc, decimal chg)
    {
        Date = date;
        Desc = desc;
        Chg = chg;
    }

    public DateTime Date { get; }
    public string Desc { get; }
    public decimal Chg { get; }
}

public static class Ledger
{
    public static LedgerEntry CreateEntry(string date, string desc, int change) =>
        new(DateTime.Parse(date, CultureInfo.InvariantCulture), desc, change / 100.0m);

    private static CultureInfo CreateCulture(string cur, string loc)
    {
        if (cur != "USD" && cur != "EUR")
        {
            throw new ArgumentException("Invalid currency");
        }

        var curSymb = cur switch
        {
            "USD" => "$",
            "EUR" => "€",
            _ => throw new ArgumentOutOfRangeException(nameof(cur), cur, null)
        };
        (int curNeg, string datPat) = loc switch
        {
            "en-US" => (0, "MM/dd/yyyy"),
            "nl-NL" => (12, "dd/MM/yyyy"),
            _ => throw new ArgumentOutOfRangeException(nameof(loc), loc, null)
        };
        var culture = new CultureInfo(loc)
        {
            NumberFormat =
            {
                CurrencySymbol = curSymb,
                CurrencyNegativePattern = curNeg
            },
            DateTimeFormat =
            {
                ShortDatePattern = datPat
            }
        };
        return culture;
    }

    private static string PrintHead(string loc) =>
        loc != "en-US" && loc != "nl-NL"
            ? throw new ArgumentException("Invalid locale")
            : loc switch
            {
                "en-US" => "Date       | Description               | Change       ",
                "nl-NL" => "Datum      | Omschrijving              | Verandering  ",
                _ => throw new ArgumentOutOfRangeException(nameof(loc), loc, null)
            };

    private static string Date(IFormatProvider culture, DateTime date) => date.ToString("d", culture);

    private static string Description(string desc) => desc.Length > 25 ? desc[..22].Insert(22, "...") : desc;

    private static string Change(IFormatProvider culture, decimal cgh) =>
        cgh < 0.0m ? cgh.ToString("C", culture) : cgh.ToString("C", culture) + " ";

    private static string PrintEntry(IFormatProvider culture, LedgerEntry entry)
    {
        const string stick = " | ";
        var formatted = new StringBuilder();
        return formatted.Append(Date(culture, entry.Date)).Append(stick)
            .Append($"{Description(entry.Desc),-25}").Append(stick)
            .Append($"{Change(culture, entry.Chg),13}").ToString();
    }

    public static string Format(string currency, string locale, LedgerEntry[] entries)
    {
        var culture = CreateCulture(currency, locale);
        var result = new StringBuilder();
        result.Append(PrintHead(locale));
        var entriesForOutput = entries.OrderBy(x => x.Date + "@" + x.Desc + "@" + x.Chg).ToList();
        for (var i = 0; i < entriesForOutput.Count; i++)
        {
            result.Append('\n').Append(PrintEntry(culture, entriesForOutput.Skip(i).First()));
        }

        return result.ToString();
    }
}