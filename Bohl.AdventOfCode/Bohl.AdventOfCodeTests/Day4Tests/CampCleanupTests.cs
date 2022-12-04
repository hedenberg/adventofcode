using Bohl.AdventOfCode.Day4;
using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCodeTests.Day4Tests;

public class CampCleanupTests
{
    [Test]
    public void Day3_Challenge1_Test()
    {
        var campCleanup = Inputs.Day4Tests.Parse<CampCleanup>();

        var containedRanges = campCleanup.ContainedPairs();

        Assert.That(containedRanges == 2);
    }

    [Test]
    public void Day3_Challenge1()
    {
        var campCleanup = Inputs.Day4Challenge.Parse<CampCleanup>();

        var containedRanges = campCleanup.ContainedPairs();

        Assert.That(containedRanges == 657);
    }

    [Test]
    public void Day3_Challenge2_Test()
    {
        var campCleanup = Inputs.Day4Tests.Parse<CampCleanup>();

        var containedRanges = campCleanup.OverlapCount();

        Assert.That(containedRanges == 4);
    }

    [Test]
    public void Day3_Challenge2()
    {
        var campCleanup = Inputs.Day4Challenge.Parse<CampCleanup>();

        var containedRanges = campCleanup.OverlapCount();

        Assert.That(containedRanges == 938);
    }
}