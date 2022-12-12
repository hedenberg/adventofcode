using System.Diagnostics;
using Bohl.AdventOfCode;
using Bohl.AdventOfCode.Day9.Part2;
using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCodeTests.Day9Tests.Part2;

public class RopeTests
{
    [Test]
    public void Day9_Challenge2_Test()
    {
        Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
        var rope = Inputs.Day9Test2.Parse<Rope>();

        rope.PerformMotions();

        var uniqueLocations = rope.GetVisits("9");

        Assert.That(uniqueLocations == 36);
    }

    [Test]
    public void Day9_Challenge2()
    {
        Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
        var rope = Inputs.Day9Challenge.Parse<Rope>();

        rope.PerformMotions();

        var uniqueLocations = rope.GetVisits("9");

        Assert.That(uniqueLocations == 2593);
    }
}