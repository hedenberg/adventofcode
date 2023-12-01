namespace Bohl.AdventOfCode.ConsoleMonitor;

public class Monitor
{
    public int Width { get; set; }
    public int Height { get; set; }

    public int X0 { get; set; }
    public int Y0 { get; set; }
    public int DisplayWidth { get; set; }
    public int DisplayHeight { get; set; }

    public int MarginTop { get; set; }
    public int MarginLeft { get; set; }

    public void Initialize(char[,] data, int x0 = 0, int y0 = 0, int displayWidth = 100, int displayHeight = 50)
    {
        // [0 .. x0 .. --displayWidth--> .. x1 .. data.GetLength(0)-1]
        // [0 .. y0 .. --displayHeight-> .. y1 .. data.GetLength(1)-1]
        Width = data.GetLength(0);
        Height = data.GetLength(1);
        X0 = x0;
        Y0 = y0;

        DisplayWidth = Width < displayWidth ? Width : displayWidth;
        DisplayHeight = Height < displayHeight ? Height : displayHeight;

        var x1 = x0 + DisplayWidth;
        var y1 = y0 + DisplayHeight;

        var lx1 = x1.ToString().Length;
        var ly1 = y1.ToString().Length;

        MarginTop = lx1;
        MarginLeft = ly1 + 1;

        var windowWidth = Width + ly1 + 1;
        var windowHeight = Height + lx1 + 1; // One extra for potential new line at end

        Console.WindowWidth = windowWidth < 120 ? 120 : windowWidth;
        Console.WindowHeight = windowHeight < 30 ? 30 : windowHeight;
        Console.Clear();
        Console.SetCursorPosition(0, 0);

        // Write labels
        RenderLabels(data, lx1, ly1);
    }

    public void RenderLabels(char[,] data, int lx1, int ly1)
    {
        for (var i = lx1 - 1; i >= 0; i--)
        {
            Console.Write(new string(' ', ly1 + 1));
            // ex lx1 = 2: i = 1 -> 0
            var div = Math.Pow(10, i);
            var mod = Math.Pow(10, i+1);
            for (var x = 0; x < data.GetLength(0); x++)
            {
                // x = 1234, div = 
                var divided = (int)(x / div);
                var modded = divided % mod;
                if (divided > 0 || i == 0)
                {
                    Console.Write(modded);
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.Write("\n");
        }


        for (var y = 0; y < data.GetLength(1); y++)
        {
            var length = y.ToString().Length;
            Console.Write(new string(' ', ly1-length));
            Console.Write(y);
            Console.Write("\n");
        }
    }

    public void Render2D(char[,] data)
    {
        Console.SetCursorPosition(MarginLeft, MarginTop);

        for (var y = 0; y < data.GetLength(1); y++)
        {
            Console.SetCursorPosition(MarginLeft, MarginTop + y);
            for (var x = 0; x < data.GetLength(0); x++)
            {
                Console.Write(data[x, y]);
            }
        }
    }
}