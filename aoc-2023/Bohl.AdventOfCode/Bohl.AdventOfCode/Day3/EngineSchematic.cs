namespace Bohl.AdventOfCode.Day3;

internal class EngineSchematic
{
    public static int Sum(string[] rows)
    {
        var height = rows.Length;
        var width = rows.First().Length;

        var sum = 0;

        for (var y = 0; y < height; y++)
        {
            var candidate = "";
            var isPartNr = false;
            for (var x = 0; x < width; x++)
            {
                var c = rows[y][x];
                if (char.IsDigit(c))
                {
                    candidate += c;
                    isPartNr = isPartNr || HasAdjacentSymbol(rows, y, x);
                    if (x + 1 == width && isPartNr)
                    {
                        sum += int.Parse(candidate);
                    }
                }
                else
                {
                    if (isPartNr)
                    {
                        sum += int.Parse(candidate);
                    }
                    candidate = "";
                    isPartNr = false;
                }
            }
        }

        return sum;
    }

    public static bool HasAdjacentSymbol(string[] rows, int y1, int x1)
    {
        var height = rows.Length;
        var width = rows.First().Length;

        if (y1 > 0)
        {
            if (IsSymbol(rows[y1 - 1][x1]))
                return true;
            if (x1 > 0)
            {
                if (IsSymbol(rows[y1 - 1][x1 - 1]))
                    return true;
            }

            if (x1 + 1 < width)
            {
                if (IsSymbol(rows[y1 - 1][x1 + 1]))
                    return true;
            }
        }

        if (x1 > 0)
        {
            if (IsSymbol(rows[y1][x1 - 1]))
                return true;
        }

        if (x1 + 1 < width)
        {
            if (IsSymbol(rows[y1][x1 + 1]))
                return true;
        }

        if (y1 + 1 < height)
        {
            if (IsSymbol(rows[y1 + 1][x1]))
                return true;
            if (x1 > 0)
            {
                if (IsSymbol(rows[y1 + 1][x1 - 1]))
                    return true;
            }
            if (x1 + 1 < width)
            {
                if (IsSymbol(rows[y1 + 1][x1 + 1]))
                    return true;
            }
        }

        return false;
    }

    public static bool IsSymbol(char c)
    {
        if (char.IsDigit(c))
            return false;
        return c != '.';
    }
}