using Bohl.AdventOfCode;
using Bohl.AdventOfCode.Day1;

namespace Bohl.AdventOfCodeTests.Day1Tests;

public class CalorieCounterTests
{
    [Test]
    public void Day1_Challenge1_Test()
    {
        var input = Inputs.Day1Tests.Replace("\r", "");

        var topCalories = CalorieCounter.GetTopCalories(input);
        Assert.That(topCalories == 24000);
    }

    [Test]
    public void Day1_Challenge1()
    {
        var input = Inputs.Day1Challenge.Replace("\r", "");

        var topCalories = CalorieCounter.GetTopCalories(input);
        Assert.That(topCalories == 67622);
    }

    [Test]
    public void Day1_Challenge2_Test()
    {
        var input = Inputs.Day1Tests.Replace("\r", "");

        var topCalories = CalorieCounter.GetTopCalories(3, input);
        var sum = topCalories.Sum();
        Assert.That(sum == 45000);
    }

    [Test]
    public void Day1_Challenge2()
    {
        var input = Inputs.Day1Challenge.Replace("\r", "");

        var topCalories = CalorieCounter.GetTopCalories(3, input);
        var sum = topCalories.Sum();
        Assert.That(sum == 201491);
    }
}