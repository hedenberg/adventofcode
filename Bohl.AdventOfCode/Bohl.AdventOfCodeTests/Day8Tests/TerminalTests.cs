using Bohl.AdventOfCode.Day8;
using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCodeTests.Day8Tests;

public class TreetopsTests
{
    [Test]
    public void Day8_Challenge1_Test()
    {
        var treeTops = Inputs.Day8Tests.Parse<Treetops>();

        var visibleTrees = treeTops.VisibleTrees();

        Assert.That(visibleTrees == 21);
    }

    [Test]
    public void Day8_Challenge1()
    {
        var treeTops = Inputs.Day8Challenge.Parse<Treetops>();

        var visibleTrees = treeTops.VisibleTrees();

        Assert.That(visibleTrees == 1814);
    }

    [Test]
    public void Day8_Challenge2_Test()
    {
        var treeTops = Inputs.Day8Challenge.Parse<Treetops>();
    }

    //[Test]
    //public void Day7_Challenge2()
    //{
    //    var totalDiskSpace = 70000000;
    //    var requiredDiskSpace = 30000000;

    //    var terminal = new Terminal(Inputs.Day7Challenge);

    //    var size = terminal.SmallestDirectoryMeetingRequirementSize(totalDiskSpace, requiredDiskSpace);

    //    Assert.That(size == 366028);
    //}
}