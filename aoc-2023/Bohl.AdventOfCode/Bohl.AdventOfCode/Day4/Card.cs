namespace Bohl.AdventOfCode.Day4;

internal class Card
{
    public int Id { get; set; }
    public List<int> WinningNumbers { get; set; }
    public List<int> Numbers { get; set; }
    public int Copies { get; set; } = 1;

    public int Winnings
    {
        get
        {
            if (Numbers.Count == 0)
                return 0;
            if (WinningNumbers.Count == 0)
                return 0;
            var winners = Numbers.Where(n => WinningNumbers.Contains(n));

            var wins = winners.Count();

            return wins;
        }
    }

    public int Score
    {
        get
        {
            var score = Math.Pow(2, Winnings - 1);

            return (int)Math.Floor(score);
        }
    }

    public static int ScratchCards(List<Card> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            var card = cards[i];

            var wins = card.Winnings;
            var copies = card.Copies;

            var upTo = i + wins + 1 < cards.Count ? i + wins + 1 : cards.Count;

            for (int j = i + 1; j < upTo; j++)
            {
                cards[j].Copies += copies;
            }
        }

        return cards.Sum(c => c.Copies);
    }

    public static int Sum(List<Card> cards)
    {
        var sum = cards.Sum(c => c.Score);

        return sum;
    }

    public static List<Card> Parse(List<string> rows)
    {
        var cards = new List<Card>();
        foreach (var row in rows)
        {
            var card = Parse(row);
            cards.Add(card);
        }

        var test = cards.First().Score;

        return cards;
    }

    public static Card Parse(string row)
    {
        // Worst parsing ever!
        // row: "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53"
        // p1: "Card 1" -> "1"
        var p1 = row.Split(':')[0].Replace("Card", "").Replace(" ", "");
        // p2: " 41 48 83 86 17 | 83 86  6 31 17  9 48 53" -> " 41 48 83 86 17 "
        var p2 = row.Split(':')[1].Split('|')[0].Split(" ");
        // p3: " 83 86  6 31 17  9 48 53"
        var p3 = row.Split('|')[1].Split(" ");

        var id = int.Parse(p1.Replace(" ", ""));
        var winningNumbers = new List<int>();
        foreach (var wn in p2)
        {
            if (string.IsNullOrEmpty(wn)) continue;

            var winningNumber = int.Parse(wn);
            winningNumbers.Add(winningNumber);
        }

        var numbers = new List<int>();
        foreach (var n in p3)
        {
            if (string.IsNullOrEmpty(n)) continue;

            var number = int.Parse(n);
            numbers.Add(number);
        }

        return new Card
        {
            Id = id,
            WinningNumbers = winningNumbers,
            Numbers = numbers
        };
    }
}