using Bohl.AdventOfCode.Day06;
using FluentAssertions;

namespace Bohl.AdventOfCode.Tests;

public class Day06
{
    [Fact]
    public void Day6_PartOne_Example()
    {
        var rows = Example1Input.Rows();

        var val = BoatRace.NumberOfWins(rows);

        val.Should().Be(288);
    }

    [Fact]
    public void Day6_PartOne_Test()
    {
        var rows = PuzzleInput.Rows();

        var val = BoatRace.NumberOfWins(rows);

        val.Should().Be(1660968);
    }

    [Fact]
    public void Day6_PartTwo_Example()
    {
        var rows = Example1Input.Rows();

        var val = BoatRace.NumberOfWins(rows, ignoreSpaces:true);

        val.Should().Be(71503);
    }

    [Fact]
    public void Day6_PartTwo_Test()
    {
        var rows = PuzzleInput.Rows();

        var val = BoatRace.NumberOfWins(rows, ignoreSpaces: true);

        val.Should().Be(26499773);
    }

    private const string Example1Input = @"Time:      7  15   30
Distance:  9  40  200";

    private const string PuzzleInput = @"Time:        47     98     66     98
Distance:   400   1213   1011   1540";
}