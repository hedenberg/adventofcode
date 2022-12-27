using Bohl.AdventOfCode.Input;
using Microsoft.Toolkit.HighPerformance;
using static System.Net.Mime.MediaTypeNames;

namespace Bohl.AdventOfCode.Day14;

public class SandSimulator
{
    public Monitor Monitor { get; set; }

    public int Counter { get; set; } = 0;

    public int SandXCurrent { get; set; }
    public int SandYCurrent { get; set; }
    public int? SandXPrevious { get; set; }
    public int? SandYPrevious { get; set; }

    public int DropSand(Map map)
    {
        var test = true;
        while (test)
        {
            // map.Pixels.GetRow(map.MaxY-1).ToArray().All(p => p is not Block.Sand)
            var x1 = 500 - map.MinX;
            var y1 = 0;
            Counter++;
            map.Pixels[x1, y1] = Block.Sand;
            //Monitor.RenderPixel(x1, y1, Block.Sand);
            //map.UpdateCell(x1,y1);

            var x2 = x1;
            var y2 = y1 + 1;
            //map.UpdateCell(x1, y1);
            while (true)
            {
                if (y2 == map.Pixels.GetLength(1))
                {
                    test = false;
                    break;
                }
                if (map.Pixels[x2, y2] is Block.Rock or Block.Sand)
                {
                    // Slide to side?
                    if (x2 - 1 < 0 || x2 - 1 >= 0 && map.Pixels[x2 - 1, y2] is Block.Rock or Block.Sand)
                    {
                        if (x2 + 1 == map.Pixels.GetLength(0) || x2 + 1 < map.Pixels.GetLength(0) && map.Pixels[x2 + 1, y2] is Block.Rock or Block.Sand)
                        {
                            if ((x1 == (500 - map.MinX) && y1 == 0) || (x2 == 500 && y2 == 0))
                            {
                                test = false;
                            }
                            break;
                        }
                        x2++;
                    }
                    else
                    {
                        x2--;
                    }
                }
                map.Pixels[x2, y2] = Block.Sand;
                //Monitor.RenderPixel(x2, y2, Block.Sand);
                //Thread.Sleep(2);
                map.Pixels[x1, y1] = Block.Air;
                //Monitor.RenderPixel(x1, y1, Block.Air);
                //Thread.Sleep(2);
                y1 = y2++;
                x1 = x2;
            }
        }

        return Counter;
    }

    public bool UpdateSand(Map map)
    {
        SandXPrevious = SandXCurrent;
        SandYPrevious = SandYCurrent;

        // Jump down one
        var x = SandXCurrent;
        var y = SandYCurrent + 1;
        
        if (map.Pixels[x, y] is Block.Rock or Block.Sand)
        {
            // Below is Rock or Sand - attempt to slide to side
            if (x - 1 < 0 || x - 1 >= 0 && map.Pixels[x - 1, y] is Block.Rock or Block.Sand)
            {
                if (x + 1 == map.Pixels.GetLength(0) 
                    || x + 1 < map.Pixels.GetLength(0) 
                    && map.Pixels[x + 1, y] is Block.Rock or Block.Sand)
                {
                    // Nowhere to slide - 
                    return SpawnSand(map);
                }
                // Slide to right
                x++;
            }
            else
            {
                // Slide to left
                x--;
            }
        }

        SandXCurrent = x;
        SandYCurrent = y;

        map.Pixels[(int)SandXPrevious, (int)SandYPrevious] = Block.Air;
        map.Pixels[SandXCurrent, SandYCurrent] = Block.Sand;

        return false;
    }

    public bool SpawnSand(Map map)
    {
        SandXCurrent = 500 - map.MinX;
        SandYCurrent = 0;
        if (map.Pixels[SandXCurrent, SandYCurrent] is Block.Sand)
        {
            // Already sand - abort true
            return true;
        }
        map.Pixels[SandXCurrent, SandYCurrent] = Block.Sand;
        Counter++;
        // Spawning sand worked fine
        return false;
    }
}

public class Map : IParsable<Map>
{
    public int MinX { get; set; }
    public int MaxX { get; set; }
    public int MinY { get; set; }
    public int MaxY { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public Block[,] Pixels { get; set; }

    public void EnablePartTwo()
    {
        Height += 2;
        var newPixels = new Block[Width, Height];

        for (var x = 0; x < Pixels.GetLength(0); x++)
        for (var y = 0; y < Pixels.GetLength(1); y++)
        {
            newPixels[x, y] = Pixels[x, y];
        }

        for (var x = 0; x < Pixels.GetLength(0); x++)
        {
            newPixels[x, Height-1] = Block.Rock;
        }

        Pixels = newPixels;
    }

    public static Map Parse(string input, IFormatProvider? provider)
    {
        var rows = input.Rows();

        var integers = rows.SelectMany(r => r.Split(" -> ")).Select(l => l.Split(",").Select(i => int.Parse(i)));
        var minX = int.MaxValue;
        var maxX = int.MinValue;
        var minY = int.MaxValue;
        var maxY = int.MinValue;
        foreach (var pair in integers)
        {
            var (x, y) = pair.ToArray();
            if (x > maxX) maxX = x;
            if (y > maxY) maxY = y;
            if (x < minX) minX = x;
            if (y < minY) minY = y;
        }

        // Enable part two
        minX = 320; // maxX-290;
        maxX = 630;

        if (minY < 100)
            minY = 0;

        var width = maxX - minX + 1;
        var height = maxY - minY + 1;

        var pixels = new Block[width, height];

        for (var x = 0; x < pixels.GetLength(0); x++)
        for (var y = 0; y < pixels.GetLength(1); y++)
            pixels[x, y] = Block.Air;

        foreach (var row in rows)
        {
            var lines = row.Split(" -> ");

            int? fromX = null;
            int? fromY = null;
            foreach (var line in lines)
            {
                var (x, y) = line.Split(',').Select(int.Parse).ToArray();

                var xi = x - minX;
                var yi = y - minY;
                if (xi == fromX)
                {
                    var start = fromY < yi ? fromY : yi;
                    var end = fromY > yi ? fromY : yi;
                    for (var pointerY = (int)start; pointerY <= end; pointerY++) pixels[xi, pointerY] = Block.Rock;
                }
                else
                {
                    var start = fromX < xi ? fromX : xi;
                    var end = fromX > xi ? fromX : xi;
                    for (var pointerX = (int)start; pointerX <= end; pointerX++) pixels[pointerX, yi] = Block.Rock;
                }

                fromX = xi;
                fromY = yi;
            }
        }

        return new Map
        {
            MinX = minX,
            MaxX = maxX,
            MinY = minY,
            MaxY = maxY,
            Width = width,
            Height = height,
            Pixels = pixels
        };
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out Map result)
    {
        throw new NotImplementedException();
    }
}

public enum Block
{
    Air,
    Rock,
    Sand,
    Trail
}