using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCode.Day2;

public class RockPaperScissors
{
    public List<Round> Rounds { get; set; } = new();

    public static RockPaperScissors ParseFromAnswers(string input)
    {
        return new RockPaperScissors
        {
            Rounds = ParseRoundsFromAnswers(input)
        };
    }

    public static RockPaperScissors ParseFromOutcome(string input)
    {
        return new RockPaperScissors
        {
            Rounds = ParseRoundsFromOutcome(input)
        };
    }

    public int Score()
    {
        return Rounds
            .Select(r => r.Score())
            .Sum();
    }

    private static List<Round> ParseRoundsFromAnswers(string input)
    {
        return input
            .Rows()
            .Select(Round.ParseFromAnswers)
            .ToList();
    }

    private static List<Round> ParseRoundsFromOutcome(string input)
    {
        return input
            .Rows()
            .Select(Round.ParseFromOutcome)
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
    public Result Result { get; set; }

    public static Round ParseFromAnswers(string input)
    {
        var (opponentMove, answerMove) = ParseMoves(input);

        var round = new Round
        {
            OpponentMove = opponentMove,
            AnswerMove = answerMove
        };
        round.Result = round.CalculateResult();
        return round;
    }

    public static Round ParseFromOutcome(string input)
    {
        var (opponentMove, outcome) = ParseMoveAndOutcome(input);

        var round = new Round
        {
            OpponentMove = opponentMove,
            Result = outcome
        };
        round.AnswerMove = round.CalculateAnswer();
        return round;
    }

    public int Score()
    {
        var resultScore = CalculateResult() switch
        {
            Result.Lose => 0,
            Result.Draw => 3,
            Result.Win => 6
        };
        var moveScore = (int)AnswerMove;
        return moveScore + resultScore;
    }

    public Result CalculateResult()
    {
        if (OpponentMove == AnswerMove) return Result.Draw;

        return (OpponentMove, AnswerMove) switch
        {
            (HandShape.Rock, HandShape.Paper) => Result.Win,
            (HandShape.Rock, HandShape.Scissor) => Result.Lose,
            (HandShape.Paper, HandShape.Rock) => Result.Lose,
            (HandShape.Paper, HandShape.Scissor) => Result.Win,
            (HandShape.Scissor, HandShape.Paper) => Result.Lose,
            (HandShape.Scissor, HandShape.Rock) => Result.Win
        };
    }

    public HandShape CalculateAnswer()
    {
        if (Result is Result.Draw) return OpponentMove;

        return (OpponentMove, Result) switch
        {
            (HandShape.Rock, Result.Win) => HandShape.Paper,
            (HandShape.Rock, Result.Lose) => HandShape.Scissor,
            (HandShape.Paper, Result.Lose) => HandShape.Rock,
            (HandShape.Paper, Result.Win) => HandShape.Scissor,
            (HandShape.Scissor, Result.Lose) => HandShape.Paper,
            (HandShape.Scissor, Result.Win) => HandShape.Rock,
            _ => throw new ArgumentOutOfRangeException()
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
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static (HandShape, Result) ParseMoveAndOutcome(string input)
    {
        var (opponent, outcome) = input.Split(' ').ToArray();

        return (ParseMove(opponent), ParseResult(outcome));
    }

    private static Result ParseResult(string input)
    {
        switch (input)
        {
            case "X":
                return Result.Lose;
            case "Y":
                return Result.Draw;
            case "Z":
                return Result.Win;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}