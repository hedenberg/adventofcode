using Bohl.AdventOfCode;
using Bohl.AdventOfCode.Day7;

namespace Bohl.AdventOfCodeTests.Day7Tests;

public class TerminalTests
{
    [Test]
    public void Day7_Challenge1_Test()
    {
        var terminal = new Terminal(Inputs.Day7Tests);

        var size = terminal.TotalSizeOfCandidates(100000);

        Assert.That(size == 95437);
    }

    [Test]
    public void Day7_Challenge1()
    {
        var terminal = new Terminal(Inputs.Day7Challenge);

        var size = terminal.TotalSizeOfCandidates(100000);

        Assert.That(size == 1086293);
    }

    [Test]
    public void Day7_Challenge2_Test()
    {
        var totalDiskSpace = 70000000;
        var requiredDiskSpace = 30000000;

        var terminal = new Terminal(Inputs.Day7Tests);

        var size = terminal.SmallestDirectoryMeetingRequirementSize(totalDiskSpace, requiredDiskSpace);

        Assert.That(size == 24933642);
    }

    [Test]
    public void Day7_Challenge2()
    {
        var totalDiskSpace = 70000000;
        var requiredDiskSpace = 30000000;

        var terminal = new Terminal(Inputs.Day7Challenge);

        var size = terminal.SmallestDirectoryMeetingRequirementSize(totalDiskSpace, requiredDiskSpace);

        Assert.That(size == 366028);
    }
}