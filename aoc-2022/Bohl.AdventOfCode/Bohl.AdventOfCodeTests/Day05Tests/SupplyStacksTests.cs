using Bohl.AdventOfCode;
using Bohl.AdventOfCode.Day05;
using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCodeTests.Day05Tests;

public class SupplyStacksTests
{
    [Test]
    public void Day5_Challenge1_Test()
    {
        var supplyStacks = Inputs.Day5Tests.Parse<SupplyStacks>();

        supplyStacks.PerformMovesOneByOne();

        var topCrates = supplyStacks.GetTopCrates();

        Assert.That(topCrates == "CMZ");
    }

    [Test]
    public void Day5_Challenge1()
    {
        var supplyStacks = Inputs.Day5Challenge.Parse<SupplyStacks>();

        supplyStacks.PerformMovesOneByOne();

        var topCrates = supplyStacks.GetTopCrates();

        Assert.That(topCrates == "QMBMJDFTD");
    }

    [Test]
    public void Day5_Challenge2_Test()
    {
        var supplyStacks = Inputs.Day5Tests.Parse<SupplyStacks>();

        supplyStacks.PerformMoves();

        var topCrates = supplyStacks.GetTopCrates();

        Assert.That(topCrates == "MCD");
    }

    [Test]
    public void Day5_Challenge2()
    {
        var supplyStacks = Inputs.Day5Challenge.Parse<SupplyStacks>();

        supplyStacks.PerformMoves();

        var topCrates = supplyStacks.GetTopCrates();

        Assert.That(topCrates == "NBTVTJNFJ");
    }
}