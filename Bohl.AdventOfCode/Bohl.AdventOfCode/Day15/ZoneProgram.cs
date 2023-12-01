using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCode.Day15;

public static class ZoneProgram
{
    public static void Run()
    {
        Console.WriteLine("Day 15");
        var monitor = new ConsoleMonitor.Monitor();

        var zone = Inputs.Day15Challenge.Parse<Zone>();
        //var map = zone.GetMap();

        var emptyItems =
            zone.Items.Count(i => i.Position.Y == 2000000 && i.ZoneType is ZoneType.Empty or ZoneType.Sensor);

        Console.WriteLine($"Answer: {emptyItems}");
        //monitor.Initialize(map);
        //monitor.Render2D(map);
    }
}