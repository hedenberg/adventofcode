using System.Diagnostics;
using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCode.Day9.Part2;

public partial class Rope : IParsable<Rope>
{
    public Rope()
    {
        Start = new Position
        {
            X = 0,
            Y = 0,
        };
        Head = new Knot
        {
            Name = "H",
            Position = new Position
            {
                X = 0,
                Y = 0,
            },
            NextKnot = Knot.Initialize(1, 9)
        };
    }

    public required List<Motion> Motions { get; set; }
    public Position Start { get; set; }
    public Knot Head { get; set; }

    public void PerformMotions()
    {
        foreach (var motion in Motions)
        {
            //PrintMotion(motion);
            Head.PerformMotion(motion);

            //while (motion.Count != 0)
            //{
            //    Head.PerformMotion(new Motion
            //    {
            //        Direction = motion.Direction,
            //        Count = 1
            //    });

            //    motion.Count--;
            //    PrintMap();
            //}

        }
        //PrintPath("9");
    }

    public int GetVisits(string name)
    {
        var knots = GetKnots();
        var knot = knots.Single(k => k.Name == name);
        knot.Positions.Add((knot.Position.X, knot.Position.Y));
        var visits = knot.Positions.Distinct();

        return visits.Count();
    }

    public static Rope Parse(string input, IFormatProvider? provider)
    {
        var rows = input.Rows();

        var motions = rows.Select(row => row.Parse<Motion>()).ToList();

        return new Rope
        {
            Motions = motions
        };
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out Rope result)
    {
        throw new NotImplementedException();
    }
}

public class Knot
{
    public static Knot Initialize(int i, int max)
    {

        return new Knot
        {
            Name = i.ToString(),
            Position = new Position
            {
                X = 0,
                Y = 0
            },
            NextKnot = i < max ? Knot.Initialize(++i, max) : null
        };
    }

    public void PerformMotion(Motion motion)
    {
        while (motion.Count != 0)
        {
            // Perform motion
            var vx = motion.Direction switch
            {
                Direction.Right => 1,
                Direction.Left => -1,
                _ => 0
            };
            var vy = motion.Direction switch
            {
                Direction.Up => 1,
                Direction.Down => -1,
                _ => 0
            };

            Position.X += vx;
            Position.Y += vy;

            // perform motion on next knot
            NextKnot?.React(Position);

            motion.Count--;
        }
    }

    public void React(Position position)
    {
        if (Name == "1")
        {

        }
        Positions.Add((Position.X, Position.Y));
        var diffX = position.X - Position.X;
        var diffY = position.Y - Position.Y;

        var vx = Math.Sign(diffX);
        var vy = Math.Sign(diffY);

        if (Math.Abs(diffX) > 1)
        {
            Position.X += vx;
            if (Math.Abs(diffY) > 0)
            {
                Position.Y += vy;
            }
            NextKnot?.React(Position);
        }
        else if (Math.Abs(diffY) > 1)
        {
            Position.Y += vy;
            if (Math.Abs(diffX) > 0)
            {
                Position.X += vx;
            }
            NextKnot?.React(Position);
        }

    }

    public required string Name { get; set; }
    public required Position Position { get; set; }
    public Knot? NextKnot { get; set; }
    public List<(int,int)> Positions { get; set; } = new();
}

public class Position
{
    public int X { get; set; }
    public int Y { get; set; }
}

public partial class Rope
{
    public int MapWidth { get; set; } = 1;
    public int MapHeight { get; set; } = 1;
    public int MapMinX { get; set; } = 0;
    public int MapMinY { get; set; } = 0;

    public List<Knot> GetKnots()
    {
        var knots = new List<Knot>();
        var knot = Head;
        while (knot is not null)
        {
            knots.Add(knot);
            knot = knot.NextKnot;
        }

        return knots;
    }

    public void PrintMap()
    {
        var knots = GetKnots();

        var positions = knots.Select(k => k.Position).ToList();
        positions.Add(Start);
        var xs = positions.Select(p => p.X).ToArray();
        var minX = xs.Min();
        var maxX = xs.Max();
        var ys = positions.Select(p => p.Y).ToArray();
        var minY = ys.Min();
        var maxY = ys.Max();

        var width = maxX - minX + 1;
        var height = maxY - minY + 1;

        MapWidth = width > MapWidth ? width : MapWidth;
        MapHeight = height > MapHeight ? height : MapHeight;

        MapMinX = minX < MapMinX ? minX : MapMinX;
        MapMinY = minY < MapMinY ? minY : MapMinY;

        for (var y = MapHeight - 1; y >= MapMinY; y--)
        {
            for (var x = MapMinX; x < MapWidth; x++)
            {
                var character = ".";
                if (Start.X == x && Start.Y == y)
                    character = "s";
                foreach (var k in knots.Where(k => k.Name != "H").OrderByDescending(k => k.Name))
                {
                    if (k.Position.X == x && k.Position.Y == y)
                        character = k.Name;
                }

                var h = knots.Single(k => k.Name == "H");
                if (h.Position.X == x && h.Position.Y == y)
                    character = h.Name;
                Trace.Write(character);
            }
            Trace.Write("\n");
        }
        Trace.Write("\n");
    }

    public void PrintPath(string name)
    {
        var knots = GetKnots();
        var knot = knots.Single(k => k.Name == name);

        for (var y = MapHeight - 1; y >= MapMinY; y--)
        {
            for (var x = MapMinX; x < MapWidth; x++)
            {
                var character = ".";

                if (knot.Positions.Any(p => p.Item1 == x && p.Item2 == y))
                {
                    character = "#";
                }
                if (Start.X == x && Start.Y == y)
                    character = "s";
                Trace.Write(character);
            }
            Trace.Write("\n");
        }
        Trace.Write("\n");
    }

    public void PrintMotion(Motion motion)
    {
        var motionLetter = motion.Direction switch
        {
            Direction.Right => "R",
            Direction.Left => "L",
            Direction.Up => "U",
            Direction.Down => "D",
            _ => ""
        };

        Trace.WriteLine($"== {motionLetter} {motion.Count} ==");
        Trace.Write("\n");
    }
}