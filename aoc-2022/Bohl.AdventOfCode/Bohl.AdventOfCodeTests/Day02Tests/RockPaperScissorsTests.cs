using Bohl.AdventOfCode;
using Bohl.AdventOfCode.Day02;

namespace Bohl.AdventOfCodeTests.Day02Tests;

public class RockPaperScissorsTests
{
    [Test]
    public void Day2_Challenge1_Test()
    {
        var input = Inputs.Day2Tests.Replace("\r", "");

        var rockPaperScissor = RockPaperScissors.ParseFromAnswers(input);
        var score = rockPaperScissor.Score();
        Assert.That(score == 15);
    }

    [Test]
    public void Day2_Challenge1()
    {
        var input = Inputs.Day2Challenge.Replace("\r", "");

        var rockPaperScissor = RockPaperScissors.ParseFromAnswers(input);
        var score = rockPaperScissor.Score();
        Assert.That(score == 10404);
    }

    [Test]
    public void Day2_Challenge2_Test()
    {
        var input = Inputs.Day2Tests.Replace("\r", "");

        var rockPaperScissor = RockPaperScissors.ParseFromOutcome(input);
        var score = rockPaperScissor.Score();
        Assert.That(score == 12);
    }

    [Test]
    public void Day2_Challenge2()
    {
        var input = Inputs.Day2Challenge.Replace("\r", "");

        var rockPaperScissor = RockPaperScissors.ParseFromOutcome(input);
        var score = rockPaperScissor.Score();
        Assert.That(score == 10334);
    }
}