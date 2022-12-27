namespace Bohl.AdventOfCode.Day14;

public class Monitor
{
    public PixelPair[,] PixelPairs { get; set; }
    public Block[,] Pixels { get; set; }

    public Monitor(Block[,] pixels)
    {
        Pixels = pixels;
        PixelPairs = LoadPixelPairs(Pixels);
    }

    public void RenderFull()
    {
        Console.SetCursorPosition(0, 0);
        Console.WindowWidth = 300;
        Console.WindowHeight = 85;
        //Pixels = GetTestPixels();
        //PixelPairs = LoadPixelPairs(Pixels);

        Console.WriteLine(new string('░', PixelPairs.GetLength(0) + 2));
        for (var y = 0; y < PixelPairs.GetLength(1); y++)
        {
            Console.Write('░');
            for (var x = 0; x < PixelPairs.GetLength(0); x++)
            {
                var pixelPair = PixelPairs[x, y];
                Console.BackgroundColor = pixelPair.BackgroundColor;
                Console.ForegroundColor = pixelPair.ForegroundColor;
                Console.Write(pixelPair.Character);
                Console.ResetColor();
            }
            Console.Write('░');
            Console.Write('\n');
        }
        Console.WriteLine(new string('░', PixelPairs.GetLength(0) + 2));
    }

    public void RenderPixel(int x, int y, Block block)
    {
        Pixels[x, y] = block;
        if (y % 2 == 0)
        {
            // upper Pixel
            var y1 = y / 2;
            PixelPairs[x, y1] = new PixelPair(Pixels[x, y], Pixels[x, y + 1]);
            Console.BackgroundColor = PixelPairs[x, y1].BackgroundColor;
            Console.ForegroundColor = PixelPairs[x, y1].ForegroundColor;
            Console.SetCursorPosition(x + 1, y1 + 1);
            Console.Write(PixelPairs[x, y1].Character);
            Console.ResetColor();
        }
        else
        {
            var y1 = (y - 1) / 2;
            PixelPairs[x, y1] = new PixelPair(Pixels[x, y-1], Pixels[x, y]);
            Console.BackgroundColor = PixelPairs[x, y1].BackgroundColor;
            Console.ForegroundColor = PixelPairs[x, y1].ForegroundColor;
            Console.SetCursorPosition(x + 1, y1 + 1);
            Console.Write(PixelPairs[x, y1].Character);
            Console.ResetColor();
        }
        Console.SetCursorPosition(0, 0);
        //RenderFull();
    }

    public static PixelPair[,] LoadPixelPairs(Block[,] pixels)
    {
        if (pixels.GetLength(1) % 2 != 0)
        {
            throw new ArgumentException("Requires even pixel rows");
        }

        var pixelPairs = new PixelPair[pixels.GetLength(0), pixels.GetLength(1)/2];

        for (var x = 0; x < pixelPairs.GetLength(0); x++)
        for (var y = 0; y < pixelPairs.GetLength(1); y++)
        {
            var upperPixel = pixels[x, y * 2];
            var lowerPixel = pixels[x, (y * 2) + 1];
            pixelPairs[x,y] = new PixelPair(upperPixel, lowerPixel);
        }

        return pixelPairs;
    }

    public static Block[,] GetTestPixels()
    {
        var pixels = new Block[10, 10];

        for (var x = 0; x < pixels.GetLength(0); x++)
        for (var y = 0; y < pixels.GetLength(1); y++)
        {
            pixels[x, y] = Block.Air;
        }

        // Row 4
        pixels[4, 4] = Block.Rock;
        pixels[8, 4] = Block.Rock;
        pixels[9, 4] = Block.Rock;

        // Row 5
        pixels[4, 5] = Block.Rock;
        pixels[5, 5] = Block.Sand;
        pixels[8, 5] = Block.Rock;

        // Row 6
        pixels[2, 6] = Block.Rock;
        pixels[3, 6] = Block.Rock;
        pixels[4, 6] = Block.Rock;

        pixels[8, 6] = Block.Rock;

        // Row 7
        pixels[8, 7] = Block.Rock;

        // Row 8
        pixels[3, 8] = Block.Sand;
        pixels[4, 8] = Block.Sand;
        pixels[5, 8] = Block.Sand;
        pixels[8, 8] = Block.Rock;

        // Row 9
        pixels[0, 9] = Block.Rock;
        pixels[1, 9] = Block.Rock;
        pixels[2, 9] = Block.Rock;
        pixels[3, 9] = Block.Rock;
        pixels[4, 9] = Block.Rock;
        pixels[5, 9] = Block.Rock;
        pixels[6, 9] = Block.Rock;
        pixels[7, 9] = Block.Rock;
        pixels[8, 9] = Block.Rock;
        pixels[9, 9] = Block.Rock;

        return pixels;
    }
}

public class PixelPair
{
    public PixelPair(Block upperBlock, Block lowerBlock)
    {
        UpperBlock = upperBlock;
        LowerBlock = lowerBlock;
    }

    public Block UpperBlock { get; set; }
    public Block LowerBlock { get; set; }

    public char Character
    {
        get
        {
            switch (TopColor: UpperBlock, BottomColor: LowerBlock)
            {
                case (Block.Rock, Block.Rock):
                case (Block.Sand, Block.Sand):
                    return '█';
                case (Block.Air, Block.Rock):
                case (Block.Sand, Block.Rock):
                case (Block.Air, Block.Sand):
                    return '▄';
                case (Block.Rock, Block.Air):
                case (Block.Rock, Block.Sand):
                case (Block.Sand, Block.Air):
                    return '▀';
                default:
                    return ' ';
            }
        }
    }

    public ConsoleColor BackgroundColor
    {
        get
        {
            switch (TopColor: UpperBlock, BottomColor: LowerBlock)
            {
                case (Block.Air, Block.Rock):
                case (Block.Air, Block.Sand):
                case (Block.Air, Block.Air):
                case (Block.Rock, Block.Air):
                case (Block.Sand, Block.Air):
                    return ConsoleColor.Cyan;
                case (Block.Rock, Block.Sand):
                case (Block.Sand, Block.Rock):
                    return ConsoleColor.DarkYellow;
                case (Block.Rock, Block.Rock):
                case (Block.Sand, Block.Sand):
                default:
                    return ConsoleColor.Cyan;
            }
        }
    }

    public ConsoleColor ForegroundColor
    {
        get
        {
            switch (TopColor: UpperBlock, BottomColor: LowerBlock)
            {
                case (Block.Air, Block.Rock):
                case (Block.Rock, Block.Air):
                case (Block.Rock, Block.Rock):
                case (Block.Rock, Block.Sand):
                case (Block.Sand, Block.Rock):
                    return ConsoleColor.DarkGreen; // TODO: Verify this is default console color
                case (Block.Sand, Block.Sand):
                case (Block.Air, Block.Sand):
                case (Block.Sand, Block.Air):
                    return ConsoleColor.DarkYellow;
                case (Block.Air, Block.Air):
                default:
                    return ConsoleColor.Cyan;
            }
        }
    }
}

//switch (TopColor, BottomColor)
//{
//    case (Color.Black, Color.White):
//    case (Color.Black, Color.Brown):
//    case (Color.Black, Color.Black):
//    case (Color.White, Color.Black):
//    case (Color.White, Color.White):
//    case (Color.White, Color.Brown):
//    case (Color.Brown, Color.Black):
//    case (Color.Brown, Color.White):
//    case (Color.Brown, Color.Brown):
//    default:
//        return ConsoleColor.Black;
//}