using System.Diagnostics;
using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCode.Day9;

public class Rope : IParsable<Rope>
{
    public int Width { get; set; } = 1;
    public int Height { get; set; } = 1;
    public int OffsetX { get; set; } = 0;
    public int OffsetY { get; set; } = 0;
    public required int X { get; set; }
    public required int Y { get; set; }
    public required RopeEnd Head { get; set; }
    public required RopeEnd Tail { get; set;}
    public char[,] Map { get; set; } = new char[5000, 5000]; // Big value.. 

    public void Move(bool print = true)
    {
        if (print)
        {
            PrintHeader("Initial State");
            SetMapToInitialState();
            UpdateMap();

            PrintState();
        }


        Tail.Locations.Add((Tail.X, Tail.Y));
        foreach (var (vx, vy) in Head.Moves)
        {
            Head.Locations.Add((Head.X, Head.Y));
            if (print)
                PrintMove(vx, vy);
            Head.X += vx;
            Head.Y += vy;

            var diffX = Head.X - Tail.X;
            var diffY = Head.Y - Tail.Y;

            var tailVx = Math.Sign(diffX);
            var tailVy = Math.Sign(diffY);

            if (Math.Abs(diffX) > 1)
            {
                Tail.X += tailVx;
                if (Math.Abs(diffY) > 0)
                {
                    Tail.Y += tailVy;
                }
                Tail.Locations.Add((Tail.X, Tail.Y));
            }
            else if (Math.Abs(diffY) > 1)
            {
                Tail.Y += tailVy;
                if (Math.Abs(diffX) > 0)
                {
                    Tail.X += tailVx;
                }
                Tail.Locations.Add((Tail.X, Tail.Y));
            }

            if (print)
            {
                UpdateMap();
                PrintState();
            }
        }

        if (print)
        {
            TailPathToMap();
            PrintState();
        }
    }

    public int UniqueTailLocations()
    {
        var locations = Tail.Locations.Distinct().Count();
        return locations;
    }

    public void SetMapToInitialState()
    {
        for (var i = 0; i < Map.GetLength(0); i++)
        {
            for (var j = 0; j < Map.GetLength(1); j++)
            {
                Map[i, j] = '.';
            }
        }
    }

    public void UpdateMap()
    {
        if (Head.X + 1 > Width)
            Width = Head.X + 1;
        if (Head.Y + 1 > Height)
            Height = Head.Y + 1;

        if (Head.X < OffsetX)
            OffsetX -= 1;
        if (Head.Y + 1 > Height)
            OffsetX -= 1;

        SetMapToInitialState();

        for (var y = Height - 1; y >= 0; y--)
        {
            for (var x = 0; x < Width; x++)
            {
                if (x == X && y == Y)
                    Map[y, x] = 's';
                if (x == Tail.X && y == Tail.Y)
                    Map[y, x] = 'T';
                if (x == Head.X && y == Head.Y)
                    Map[y, x] = 'H';
            }
        }
    }

    public void TailPathToMap()
    {
        SetMapToInitialState();
        for (var y = Height - 1; y >= 0; y--)
        {
            for (var x = 0; x < Width; x++)
            {
                if (Tail.Locations.Any(l => l.Item1 == x && l.Item2 == y))
                {
                    Map[y, x] = '#';
                }
                if (x == X && y == Y)
                    Map[y, x] = 's';
            }
        }
    }

    public void PrintState()
    {
        for (var y = Height - 1; y >= 0; y--)
        {
            for (var x = 0; x < Width; x++)
            {
                Trace.Write(Map[y, x]);
            }
            Trace.Write("\n");
        }

        Trace.WriteLine("");
    }

    public void PrintMove(int x, int y)
    {
        if (x == 1)
        {
            PrintHeader("R");
        }
        else if (x == -1)
        {
            PrintHeader("L");
        }
        else if (y == 1)
        {
            PrintHeader("U");
        }
        else if (y == -1)
        {
            PrintHeader("D");
        }
    }

    public void PrintHeader(string header)
    {
        Trace.WriteLine($"== {header} ==");
        Trace.WriteLine("");
    }

    public static Rope Parse(string input, IFormatProvider? provider)
    {
        var rows = input.Rows();

        var moves = new List<(int, int)>();

        foreach (var row in rows)
        {
            var (directionString, countString) = row.Split(' ');
            var direction = Direction(directionString);
            var count = int.Parse(countString);

            while (count > 0)
            {
                moves.Add(direction);
                count--;
            }
        }

        return new Rope
        {
            X = 0,
            Y = 0,
            Head = new RopeEnd
            {
                X = 0,
                Y = 0,
                Moves = moves
            },
            Tail = new RopeEnd
            {
                X = 0,
                Y = 0,
                Moves = new List<(int, int)>()
            }
        };
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out Rope result)
    {
        throw new NotImplementedException();
    }

    public static (int, int) Direction(string input)
    {
        return input switch
        {
            "R" => (1, 0),
            "U" => (0, 1),
            "L" => (-1, 0),
            "D" => (0, -1),
            _ => throw new NotImplementedException()
        };
    }
}

public class RopeEnd
{
    public required int X { get; set; }
    public required int Y { get; set; }
    public required List<(int, int)> Moves { get; set; }
    public List<(int, int)> Locations { get; set; } = new();
}