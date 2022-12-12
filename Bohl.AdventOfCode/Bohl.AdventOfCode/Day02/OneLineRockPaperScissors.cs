using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCode.Day02;

public static class OneLineRockPaperScissors
{
    public static int ScoreFromRecommendations(string input)
    {
        return input
            .Rows()
            .Select(r => (r.Split(' ').AsEnumerable().ElementAt(0), r.Split(' ').AsEnumerable().ElementAt(1)))
            .Select(r => (char.Parse(r.Item1), char.Parse(r.Item2)))
            .Select(r => (r.Item1 - 'A' + 1, r.Item2 - 'X' + 1))
            .Select(r => r.Item2 + (r.Item1 == r.Item2 ? 3 : (r.Item1 - r.Item2 + 3) % 3 == 2 ? 6 : 0))
            .Sum();
    }

    public static int ScoreFromOutcome(string input)
    {
        return input
            .Rows()
            .Select(r => (r.Split(' ').AsEnumerable().ElementAt(0), r.Split(' ').AsEnumerable().ElementAt(1)))
            .Select(r => (char.Parse(r.Item1), char.Parse(r.Item2)))
            .Select(r => (r.Item1 - 'A' + 1, r.Item2 - 'X' + 1))
            .Select(r => (r.Item1, r.Item2 switch
            {
                2 => r.Item1,
                3 => r.Item1 - 2 <= 0 ? r.Item1 - 2 + 3 : r.Item1 - 2,
                _ => r.Item1 + 2 > 3 ? r.Item1 + 2 - 3 : r.Item1 + 2
            }))
            .Select(r => r.Item2 + (r.Item1 == r.Item2 ? 3 : (r.Item1 - r.Item2 + 3) % 3 == 2 ? 6 : 0))
            .Sum();
    }
}