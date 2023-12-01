using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCode.Day14;

public static class SandProgram
{
    public static void Run(string part = "2")
    {
        if (part == "1")
        {
            var map = Inputs.Day14Challenge.Parse<Map>();

            var monitor = new Monitor(map.Pixels);
            monitor.RenderFull();

            var simulator = new SandSimulator
            {
                Monitor = monitor
            };

            var sandCreated = simulator.DropSand(map);
        }
        else
        {
            var map = Inputs.Day14Challenge.Parse<Map>();
            map.EnablePartTwo();

            var monitor = new Monitor(map.Pixels);
            //monitor.RenderFull();

            var simulator = new SandSimulator
            {
                Monitor = monitor
            };

            var sandCreated = simulator.DropSand(map);
        }
    }
}