using Bohl.AdventOfCode;
using Bohl.AdventOfCode.Day6;

namespace Bohl.AdventOfCodeTests.Day6Tests;

public class TuningTroubleTests
{
    [TestCase(Inputs.Day6Test1, Inputs.Day6Test1ExpectedPart1)]
    [TestCase(Inputs.Day6Test2, Inputs.Day6Test2ExpectedPart1)]
    [TestCase(Inputs.Day6Test3, Inputs.Day6Test3ExpectedPart1)]
    [TestCase(Inputs.Day6Test4, Inputs.Day6Test4ExpectedPart1)]
    [TestCase(Inputs.Day6Test5, Inputs.Day6Test5ExpectedPart1)]
    public void Day6_Challenge1_Test(string input, int expected)
    {
        var startOfPacketMarker = TuningTrouble.StartOfPacketMarker(input, 4);

        Assert.That(startOfPacketMarker == expected);
    }

    [Test]
    public void Day6_Challenge1()
    {
        var startOfPacketMarker = TuningTrouble.StartOfPacketMarker(Inputs.Day6Challenge, 4);

        Assert.That(startOfPacketMarker == 1238);
    }

    [TestCase(Inputs.Day6Test1, Inputs.Day6Test1ExpectedPart2)]
    [TestCase(Inputs.Day6Test2, Inputs.Day6Test2ExpectedPart2)]
    [TestCase(Inputs.Day6Test3, Inputs.Day6Test3ExpectedPart2)]
    [TestCase(Inputs.Day6Test4, Inputs.Day6Test4ExpectedPart2)]
    [TestCase(Inputs.Day6Test5, Inputs.Day6Test5ExpectedPart2)]
    public void Day6_Challenge2_Test(string input, int expected)
    {
        var startOfPacketMarker = TuningTrouble.StartOfPacketMarker(input, 14);

        Assert.That(startOfPacketMarker == expected);
    }

    [Test]
    public void Day6_Challenge2()
    {
        var startOfPacketMarker = TuningTrouble.StartOfPacketMarker(Inputs.Day6Challenge, 14);

        Assert.That(startOfPacketMarker == 3037);
    }
}