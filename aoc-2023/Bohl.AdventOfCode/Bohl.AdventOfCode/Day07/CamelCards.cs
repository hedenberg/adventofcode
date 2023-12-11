namespace Bohl.AdventOfCode.Day07;

internal class CamelCards
{
    public static int TotalWinnings(string[] input, bool joker = false)
    {
        var hands = input.Select(h => Hand.ParseHand(h, joker)).ToList();

        var sortedHands = hands.OrderBy(c => c).ToList();

        var multiplier = 1;
        var result = 0;
        foreach (var sortedHand in sortedHands)
        {
            result += multiplier * sortedHand.Bid;
            multiplier++;
        }

        return result;
    }
}

internal class Hand : IComparable<Hand>
{
    private Hand(string row, bool joker = false)
    {
        Cards = row.Split(' ')[0].ToCharArray();
        Bid = int.Parse(row.Split(' ')[1]);
        _joker = joker;
    }

    private readonly bool _joker;

    public char[] Cards { get; set; }
    public int Bid { get; set; }
    public HandType Type => GetHandType(Cards, _joker);

    public int CompareTo(Hand? other)
    {
        if (other == null) return 1;
        if (other.Type != Type)
            return other.Type > Type ? -1 : 1;

        for (var i = 0; i < 5; i++)
        {
            var c1 = Cards[i];
            var c2 = other.Cards[i];

            if (c1 == c2)
                continue;

            return Compare(c1, c2, _joker);
        }

        return 0;
    }

    public static Hand ParseHand(string row, bool joker = false)
    {
        return new Hand(row, joker);
    }

    public override string ToString()
    {
        return $"{new string(Cards)} {Bid} {Type} {_joker}";
    }

    private static char[] CardStrength = { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };

    private static int Compare(char c1, char c2, bool joker = false)
    {
        if (joker && c1 == 'J')
            return -1;
        if (joker && c2 == 'J')
            return 1;
        return Array.IndexOf(CardStrength, c1) > Array.IndexOf(CardStrength, c2) ? 1 : -1;
    }

    private static HandType GetHandType(char[] hand, bool joker = false)
    {
        if (FiveOfAKind(hand, joker))
            return HandType.FiveOfAKind;
        if (FourOfAKind(hand, joker))
            return HandType.FourOfAKind;
        if (FullHouse(hand, joker))
            return HandType.FullHouse;
        if (ThreeOfAKind(hand, joker))
            return HandType.ThreeOfAKind;
        if (TwoPair(hand, joker))
            return HandType.TwoPair;
        if (OnePair(hand, joker))
            return HandType.OnePair;
        return HandType.HighCard;
    }

    private static bool FiveOfAKind(char[] hand, bool joker = false)
    {
        return hand.Any(c => Occurrences(hand, c) + (joker && c != 'J' ? Occurrences(hand, 'J') : 0) == 5);
    }

    private static bool FourOfAKind(char[] hand, bool joker = false)
    {
        return hand.Any(c => Occurrences(hand, c) + (joker && c != 'J' ? Occurrences(hand, 'J') : 0) == 4);
    }

    private static bool FullHouse(char[] hand, bool joker = false)
    {
        if (joker)
        {
            var js = Occurrences(hand, 'J');
            var h2 = hand.Where(c => c != 'J').ToArray();
            return h2.Any(
                       c1 => Occurrences(h2, c1) + js == 3 && hand.Any(c2 => c2 != c1 && Occurrences(h2, c2) == 2))
                   || h2.Any(
                       c1 => Occurrences(h2, c1) == 3 && hand.Any(c2 => c2 != c1 && Occurrences(h2, c2) + js == 2));
        }
        return hand.Any(c1 => 
            Occurrences(hand, c1) == 3 && 
                hand.Any(c2 => c2 != c1 && Occurrences(hand, c2) == 2) ||
            Occurrences(hand, c1) == 3 && 
                hand.Any(c2 => c2 != c1 && Occurrences(hand, c2) == 2)
            );
    }

    private static bool ThreeOfAKind(char[] hand, bool joker = false)
    {
        return hand.Any(c => Occurrences(hand, c) + (joker && c != 'J' ? Occurrences(hand, 'J') : 0) == 3) && hand.Any(c => Occurrences(hand, c) == 1);
    }

    private static bool TwoPair(char[] hand, bool joker = false)
    {
        return hand.Any(c1 => 
            Occurrences(hand, c1) + (joker && c1 != 'J' ? Occurrences(hand, 'J') : 0) == 2 && hand.Any(c2 => c2 != c1 && Occurrences(hand, c2) == 2) ||
            Occurrences(hand, c1) == 2 && hand.Any(c2 => c2 != c1 && Occurrences(hand, c2) + (joker && c1 != 'J' && c2 != 'J' ? Occurrences(hand, 'J') : 0) == 2));
    }

    private static bool OnePair(char[] hand, bool joker = false)
    {
        return hand.Any(c => Occurrences(hand, c) + (joker && c != 'J' ? Occurrences(hand, 'J') : 0) == 2) && !TwoPair(hand);
    }

    private static int Occurrences(char[] hand, char card)
    {
        var cardCount = hand.GroupBy(c => c).ToDictionary(c => c.Key, c => c.Count());
        return cardCount.TryGetValue(card, out var occurrences) ? occurrences : 0;
    }
}

internal enum HandType
{
    HighCard = 0,
    OnePair = 1,
    TwoPair = 2,
    ThreeOfAKind = 3,
    FullHouse = 4,
    FourOfAKind = 5,
    FiveOfAKind = 6
}