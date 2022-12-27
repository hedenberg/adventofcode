namespace Bohl.AdventOfCode.Day14;

public static class SandViewer
{
    private const int TopMargin = 5;
    private const int LeftMargin = 4;

    public static void PrintInitialState(this Map map)
    {
        var windowWidth = map.Width + LeftMargin;
        var windowHeight = map.Height + TopMargin;

        Console.SetCursorPosition(0, 2);
        Console.CursorVisible = false;
        Console.WindowWidth = windowWidth > 120 ? windowHeight : 120;
        Console.WindowHeight = 85; // windowHeight > 30 ? windowHeight : 30;


        //Console.ForegroundColor = ConsoleColor.White;
        //Console.Write("   ");

        PrintHeader(map.MinX, map.MaxX);

        for (var yi = 0; yi < map.Pixels.GetLength(1); yi++)
        {
            var y = yi + map.MinY;
            PrintLeftSide(y);
            for (var xi = 0; xi < map.Pixels.GetLength(0); xi++)
            {
                var x = xi + map.MinX;
                var mapPixel = map.Pixels[xi, yi];
                if (mapPixel is Block.Air)
                    Console.Write(".");
                if (mapPixel is Block.Rock)
                    Console.Write("#");
            }

            Console.Write("\n");
        }
    }

    public static void PrintHeader(int start, int end)
    {
        Console.Write("    ");
        for (var x = start; x <= end; x++)
            if (x >= 100 && (x == start || x % 10 == 0 || x == end))
                Console.Write(x / 100 % 10);
            else
                Console.Write(" ");
        Console.Write("\n    ");
        for (var x = start; x <= end; x++)
            if (x >= 10 && (x == start || x % 10 == 0 || x == end))
                Console.Write(x / 10 % 10);
            else
                Console.Write(" ");
        Console.Write("\n    ");
        for (var x = start; x <= end; x++)
            if (x == start || x % 10 == 0 || x == end)
                Console.Write(x % 10);
            else
                Console.Write(" ");
        Console.Write("\n");
    }

    public static void PrintLeftSide(int y)
    {
        if (y < 10)
            Console.Write("  ");
        else if (y < 100)
            Console.Write(" ");
        Console.Write($"{y} ");
    }

    public static void UpdateCell(this Map map, int x, int y)
    {
        var xi = x - map.MinX;
        if (xi > 0)
        {
            map.PrintCell(xi - 1, y);
            if (y > 0) map.PrintCell(xi - 1, y - 1);
            if (y < map.MaxY) map.PrintCell(xi - 1, y + 1);
        }

        if (y > 0) map.PrintCell(xi, y - 1);
        if (y < map.MaxY) map.PrintCell(xi, y + 1);
        map.PrintCell(xi, y);
    }

    public static void PrintCell(this Map map, int xi, int yi)
    {
        var value = map.Pixels[xi, yi] switch
        {
            Block.Rock => "#",
            Block.Sand => "o",
            _ => "."
        };

        PrintPosition(xi, yi, value);
    }

    public static void PrintPosition(int xi, int yi, string value)
    {
        Console.SetCursorPosition(xi + LeftMargin, yi + TopMargin);
        Console.Write(value);
    }
}