using System.Drawing;
using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCode.Day12;

public static class Program
{
    public static void Run(string part = "2")
    {
        if (part == "1")
        {
            var heightMap = Inputs.Day12Challenge.Parse<HeightMap>();

            heightMap.PrintInitialState();

            var aStar = new SpatialAStar<Square, object>(heightMap.Squares);

            var path = aStar.Search(
                heightMap.Start,
                heightMap.End,
                null,
                heightMap);

            foreach (var square in path)
            {
                heightMap.PrintBox(square.X, square.Y, square.ToString(), ConsoleColor.DarkMagenta);
                Thread.Sleep(1);
            }

            var length = path.Count - 1;
            Console.WriteLine($"Length: {length}");
        }
        else
        {
            var print = true;
            var heightMap = Inputs.Day12Challenge.Parse<HeightMap>();

            var startingPoints = heightMap.Squares
                .Cast<Square>()
                .Where(square => square.Height == 0)
                .ToList();

            heightMap.PrintInitialState();

            var paths = new List<LinkedList<Square>>();
            foreach (var startingPoint in startingPoints)
            {
                if (print)
                {
                    heightMap.PrintInitialState();
                    heightMap.PrintBox(startingPoint.X, startingPoint.Y, "$", ConsoleColor.Yellow);
                }

                var aStar = new SpatialAStar<Square, object>(heightMap.Squares);

                var path = aStar.Search(
                    new Point
                    {
                        X = startingPoint.X,
                        Y = startingPoint.Y
                    },
                    heightMap.End,
                    null,
                    heightMap,
                    print);

                if (path != null)
                {
                    if (print)
                        foreach (var square in path)
                            heightMap.PrintBox(square.X, square.Y, square.ToString(), ConsoleColor.DarkMagenta);

                    paths.Add(path);
                }
            }

            heightMap.PrintInitialState();
            var shortestPath = paths.OrderBy(path => path.Count).First();
            foreach (var square in shortestPath)
                heightMap.PrintBox(square.X, square.Y, square.ToString(), ConsoleColor.DarkMagenta);

            Console.SetCursorPosition(0, 0);
            Console.WriteLine("##################################");
            var length = shortestPath.Count - 1;
            Console.WriteLine($"Length: {length}");
            Console.WriteLine("##################################");
        }
    }
}