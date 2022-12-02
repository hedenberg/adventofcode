namespace Bohl.AdventOfCode.Day2;

public class RockPaperScissors
{
    public List<Round> Rounds { get; set; }

    public RockPaperScissors(string input)
    {
        Rounds = ParseInput(input);
    }

    public int Score()
    {
        return Rounds.Select(r => r.Score()).Sum();
    }

    private static List<Round> ParseInput(string input)
    {
        return input.Split('\n')
            .Select(round => new Round(round))
            .ToList();
    }
}

public enum HandShape
{
    Rock = 1,
    Paper = 2,
    Scissor = 3
}

public enum Result
{
    Lose = 1,
    Draw = 2,
    Win = 3
}

public class Round
{
    public HandShape OpponentMove { get; set; }
    public HandShape AnswerMove { get; set; }

    public Round(string input)
    {
        (OpponentMove, AnswerMove) = ParseMoves(input);
    }

    public int Score()
    {
        var resultScore =  Result() switch
        {
            Day2.Result.Lose => 0,
            Day2.Result.Draw => 3,
            Day2.Result.Win => 6,
            _ => throw new NotImplementedException()
        };
        var moveScore = (int)AnswerMove;
        return moveScore + resultScore;
    }

    public Result Result()
    {
        if (OpponentMove == AnswerMove)
        {
            return Day2.Result.Draw;
        }

        return (OpponentMove, AnswerMove) switch
        {
            (HandShape.Rock, HandShape.Paper) => Day2.Result.Win,
            (HandShape.Rock, HandShape.Scissor) => Day2.Result.Lose,
            (HandShape.Paper, HandShape.Rock) => Day2.Result.Lose,
            (HandShape.Paper, HandShape.Scissor) => Day2.Result.Win,
            (HandShape.Scissor, HandShape.Paper) => Day2.Result.Lose,
            (HandShape.Scissor, HandShape.Rock) => Day2.Result.Win,
            _ => throw new NotImplementedException()
        };
    }

    private static (HandShape, HandShape) ParseMoves(string input)
    {
        var (opponent, answer) = input.Split(' ').Select(ParseMove).ToArray();

        return (opponent, answer);
    }

    private static HandShape ParseMove(string input)
    {
        switch (input)
        {
            case "A":
            case "X":
                return HandShape.Rock;
            case "B":
            case "Y":
                return HandShape.Paper;
            case "C":
            case "Z":
                return HandShape.Scissor;
        }
        throw new NotImplementedException();
    }
}

public static class ArrayExtensions
{
    public static void Deconstruct<T>(this T[] srcArray, out T a0, out T a1)
    {
        if (srcArray == null || srcArray.Length < 2)
            throw new ArgumentException(nameof(srcArray));

        a0 = srcArray[0];
        a1 = srcArray[1];
    }
}