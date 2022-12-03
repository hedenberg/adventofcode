using Bohl.AdventOfCode.Day3;

namespace Bohl.AdventOfCodeTests.Day3Tests;

internal class RucksackReorganizationTests
{
    [Test]
    public void Day3_Challenge1_Test()
    {
        var input = Inputs.Day3Tests.Replace("\r", "");

        var elfRucksacks = ElfGroup.ParseInput(input);
        var priority = elfRucksacks.MisplacedPrioritySummary();

        Assert.That(priority == 157);
    }

    [Test]
    public void Day3_Challenge1()
    {
        var input = Inputs.Day3Challenge.Replace("\r", "");

        var elfRucksacks = ElfGroup.ParseInput(input);
        var priority = elfRucksacks.MisplacedPrioritySummary();

        Assert.That(priority == 7845);
    }

    [Test]
    public void Day3_Challenge2_Test()
    {
        var input = Inputs.Day3Tests.Replace("\r", "");

        var elfRucksacks = RucksackReorganization.ParseInput(input);
        var priority = elfRucksacks.BadgePrioritySummary();

        Assert.That(priority == 70);
    }

    [Test]
    public void Day3_Challenge2()
    {
        var input = Inputs.Day3Challenge.Replace("\r", "");

        var elfRucksacks = RucksackReorganization.ParseInput(input);
        var priority = elfRucksacks.BadgePrioritySummary();

        Assert.That(priority == 2790);
    }
}