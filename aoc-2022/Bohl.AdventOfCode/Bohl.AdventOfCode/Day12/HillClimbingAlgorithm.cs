using System.Drawing;
using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCode.Day12;

public class HillClimbingAlgorithm
{
}

public class HeightMap : IParsable<HeightMap>
{
    public Square[,] Squares { get; set; }
    public Point Start { get; set; }
    public Point End { get; set; }

    public static HeightMap Parse(string input, IFormatProvider? provider)
    {
        var rows = input.Rows();
        var firstRow = rows[0];

        var squares = new Square[firstRow.Length, rows.Length];
        var start = new Point { X = 0, Y = 0 };
        var end = new Point { X = 0, Y = 0 };
        for (var y = 0; y < rows.Length; y++)
        {
            var row = rows[y];
            for (var x = 0; x < row.Length; x++)
            {
                var character = row[x];
                if (character == 'S')
                {
                    squares[x, y] = new Square
                    {
                        Height = 0,
                        X = x,
                        Y = y,
                        IsStart = true
                    };
                    start = new Point
                    {
                        X = x,
                        Y = y
                    };
                }
                else if (character == 'E')
                {
                    squares[x, y] = new Square
                    {
                        Height = 'z' - 'a',
                        X = x,
                        Y = y,
                        IsEnd = true
                    };
                    end = new Point
                    {
                        X = x,
                        Y = y
                    };
                }
                else
                {
                    squares[x, y] = new Square
                    {
                        Height = character - 'a',
                        X = x,
                        Y = y
                    };
                }
            }
        }

        return new HeightMap
        {
            Squares = squares,
            Start = start,
            End = end
        };
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out HeightMap result)
    {
        throw new NotImplementedException();
    }

    public void PrintInitialState()
    {
        Console.SetCursorPosition(0, 2);
        Console.WindowWidth = Squares.GetLength(0) + 10;
        Console.WindowHeight = Squares.GetLength(1) + 10;
        //            11111111
        // 8999999999900000000
        // 9012345678912345678

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("   ");
        for (var x = 0; x < Squares.GetLength(0); x++)
            if (x >= 100)
                Console.Write(x / 100 % 10);
            else
                Console.Write(" ");
        Console.Write("\n   ");
        for (var x = 0; x < Squares.GetLength(0); x++)
            if (x < 10)
                Console.Write(" ");
            else
                Console.Write(x / 10 % 10);
        Console.Write("\n   ");
        for (var x = 0; x < Squares.GetLength(0); x++) Console.Write(x % 10);
        Console.Write("\n");
        Console.ResetColor();
        for (var y = 0; y < Squares.GetLength(1); y++)
        {
            for (var x = 0; x < Squares.GetLength(0); x++)
            {
                if (x == 0)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    if (y < 10)
                        Console.Write(" ");
                    Console.Write($"{y} ");
                    Console.ResetColor();
                }

                if (Squares[x, y].ToString() == "S")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(Squares[x, y]);
                    Console.ResetColor();
                }
                else if (Squares[x, y].ToString() == "E")
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(Squares[x, y]);
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(Squares[x, y]);
                }
            }

            Console.Write("\n");
        }
    }

    public void PrintBox(int x, int y, string character, ConsoleColor color)
    {
        var left = 3;
        var top = Console.WindowTop + 5;
        Console.SetCursorPosition(left + x, top + y);
        Console.ForegroundColor = color;
        Console.Write(character);
        Console.ResetColor();
    }

    public void PrintThis()
    {
        for (var y = 0; y < Squares.GetLength(1); y++)
        {
            for (var x = 0; x < Squares.GetLength(0); x++)
            {
                if (x == 0)
                {
                    if (y < 10)
                        Console.Write("  ");
                    else if (y < 100)
                        Console.Write(" ");
                    Console.Write(y);
                }

                if (Squares[x, y].ToString() == "#")
                    Console.ForegroundColor = ConsoleColor.Red;
                else
                    Console.ForegroundColor = ConsoleColor.White;
                Console.Write(Squares[x, y]);
            }

            Console.Write("\n");
        }
    }

    public override string ToString()
    {
        var str = " ";
        for (var x = 0; x < Squares.GetLength(0); x++) str += x % 10;
        str += "\n";
        for (var y = 0; y < Squares.GetLength(1); y++)
        {
            for (var x = 0; x < Squares.GetLength(0); x++)
            {
                if (x == 0)
                    str += y % 10;
                str += Squares[x, y];
            }

            str += "\n";
        }

        return str;
    }
}

public class Square : IPathNode<object>
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Height { get; set; }
    public bool IsStart { get; set; }
    public bool IsEnd { get; set; }
    public char? CurrentPosition { get; set; }

    public bool IsWalkable(IPathNode<object> fromNode, object inContext)
    {
        if (fromNode is Square square) return IsReachable(square.Height);

        throw new NotImplementedException();
    }

    public bool IsReachable(int height)
    {
        var diff = Height - height;
        var reachable = diff <= 1;
        return reachable;
    }

    public override string ToString()
    {
        if (IsStart)
            return "S";
        if (IsEnd)
            return "E";
        if (CurrentPosition is not null)
            return CurrentPosition.ToString();
        return ((char)('a' + Height)).ToString();
    }
}