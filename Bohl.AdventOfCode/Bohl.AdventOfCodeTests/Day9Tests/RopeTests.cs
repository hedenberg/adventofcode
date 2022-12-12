﻿using Bohl.AdventOfCode.Day9;
using Bohl.AdventOfCode.Input;
using System.Diagnostics;
using Bohl.AdventOfCode;

namespace Bohl.AdventOfCodeTests.Day9Tests;

public class RopeTests
{
    [Test]
    public void Day9_Challenge1_Test()
    {
        Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
        var rope = Inputs.Day9Tests.Parse<Rope>();

        rope.Move(false);

        var uniqueLocations = rope.UniqueTailLocations();

        Assert.That(uniqueLocations == 13);
    }

    [Test]
    public void Day9_Challenge1()
    {
        Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
        var rope = Inputs.Day9Challenge.Parse<Rope>();

        rope.Move(false);

        var uniqueLocations = rope.UniqueTailLocations();

        Assert.That(uniqueLocations == 6391);
    }
}