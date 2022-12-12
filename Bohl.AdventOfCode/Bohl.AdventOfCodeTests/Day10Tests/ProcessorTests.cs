using Bohl.AdventOfCode;
using Bohl.AdventOfCode.Day10;

namespace Bohl.AdventOfCodeTests.Day10Tests;

public class ProcessorTests
{
    [Test]
    public void Day10_Challenge1_Test()
    {
        var program = new Processor();
        program.LoadProgram(Inputs.Day10Test2);
        var targets = new[] { 20, 60, 100, 140, 180, 220 };
        var signalStrength = 0;
        while (program.ClockCycle <= 220)
        {
            program.Run(20);
            if (targets.Contains(program.ClockCycle))
            {
                var x = program.RegisterX;
                var clock = program.ClockCycle;
                var signal = program.SignalStrength;
                signalStrength += program.SignalStrength;
            }
        }
        Assert.That(signalStrength == 13140);
    }

    [Test]
    public void Day10_Challenge1()
    {
        var program = new Processor();
        program.LoadProgram(Inputs.Day10Challenge);
        var targets = new[] { 20, 60, 100, 140, 180, 220 };
        var signalStrength = 0;
        while (program.ClockCycle <= 220)
        {
            program.Run(20);
            if (targets.Contains(program.ClockCycle))
            {
                var x = program.RegisterX;
                var clock = program.ClockCycle;
                var signal = program.SignalStrength;
                signalStrength += program.SignalStrength;
            }
        }
        Assert.That(signalStrength == 14520);
    }

    [Test]
    public void Day4_Challenge2_Test()
    {

        //Assert.That(containedRanges == 4);
    }

    [Test]
    public void Day4_Challenge2()
    {

        //Assert.That(containedRanges == 938);
    }
}