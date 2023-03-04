using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class Poker
{
    private static readonly Dictionary<string, int> combinations = new()
    {
        { "1,1", 0 },
        { "2,1", 1 },
        { "2,2", 2 },
        { "3,1", 3 },
        { "3,2", 6 },
        { "4,1", 7 }
    };

    public static IEnumerable<string> BestHands(IEnumerable<string> hands)
    {
        var rankingHands = hands.Select(card => new { Card = card, Rank = HandRank(card) })
            .ToArray();
        var maxRank = rankingHands.Max(hand => hand.Rank);
        return rankingHands.Where(pair => pair.Rank.Equals(maxRank))
            .Select(pair => pair.Card)
            .ToArray();
    }

    private static Tuple<int, int> HandRank(string hand)
    {
        var cards = hand.Split()
            .Select(card => new Card(card))
            .ToArray();
        var ranks = HandRanks(cards);
        var results = ranks
            .GroupBy(z => z)
            .Select(gr => new { Rank = gr.Key, Count = gr.Count() })
            .OrderByDescending(x => x.Count)
            .ThenByDescending(x => x.Rank)
            .ToArray();
        var combination = string.Join(",", results.Take(2).Select(x => x.Count));
        var rest = results.Select(x => x.Rank).Aggregate((acc, x) => acc * 14 + x);
        var first = combinations[combination];
        if (IsFlash(cards))
        {
            first += 5;
        }

        if (IsStraight(ranks))
        {
            first += 4;
        }

        return Tuple.Create(first, rest);
    }

    private static bool IsStraight(int[] ranks) => new HashSet<int>(ranks).Count == 5 && ranks.Max() - ranks.Min() == 4;

    private static bool IsFlash(IEnumerable<Card> cards) =>
        new HashSet<string>(cards.Select(card => card.Suit)).Count == 1;

    private static int[] HandRanks(IEnumerable<Card> cards)
    {
        var ranks = cards.Select(card => card.Rank)
            .OrderByDescending(x => x)
            .ToArray();
        return ranks.SequenceEqual(new[] { 14, 5, 4, 3, 2 }) ? new[] { 1, 2, 3, 4, 5 } : ranks;
    }

    private class Card
    {
        private static readonly Regex cardPattern;

        static Card() => cardPattern = new Regex(@"(?<rank>\d+|[JQKA])(?<suit>[HSCD])");

        public Card(string card)
        {
            var match = cardPattern.Match(card);
            if (!match.Success)
            {
                throw new ArgumentOutOfRangeException(nameof(card), card);
            }

            var rank = match.Groups["rank"].Value;
            Rank = int.TryParse(rank, out var result) ? result : "--23456789TJQKA".IndexOf(rank);
            Suit = match.Groups["suit"].Value;
        }

        public int Rank { get; }
        public string Suit { get; }
        public override string ToString() => $"Rank = {Rank}, Suit = {Suit}";
    }
}