namespace Bohl.AdventOfCode.Day03;

internal class Gear
{
    public int X { get; set; }
    public int Y { get; set; }
    public List<int> Numbers { get; set; } = new List<int>();
}

internal class GearCounter
{
    public static int Sum(string[] rows)
    {
        var height = rows.Length;
        var width = rows.First().Length;

        var stars = new List<(int,int)>();
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var c = rows[y][x];
                if (c == '*')
                {
                    stars.Add((x,y));
                }
            }
        }

        var countedCandidates = new List<string>();

        var sum = 0;
        foreach (var (x, y) in stars)
        {
            /*
             * a1 a2 a3 a4 a5 a6 a7
             * b1 b2 b3  * b5 b6 b7
             * c1 c2 c3 c4 c5 c7 c7
             */

            var x1 = x - 3;
            var y1 = y - 1;
            var x2 = x + 4;
            var y2 = y + 2;

            if (x1 < 0)
                x1 = 0;
            if (y1 < 0)
                y1 = 0;
            if (x2 > width)
                x2 = width;
            if (y2 > height)
                y2 = height;

            var candidates = new List<string>();

            for (int ys = y1; ys < y2; ys++)
            {
                var candidate = "";
                var isGear = false;
                for (int xs = x1; xs < x2; xs++)
                {
                    var c = rows[ys][xs];
                    if (char.IsDigit(c))
                    {
                        candidate += c;
                        isGear = isGear || IsAdjacent(xs, ys, x, y);
                        if (xs + 1 == x2 && isGear)
                        {
                            //if (!countedCandidates.Contains(candidate))
                            //{
                            candidates.Add(candidate);
                            //}
                        }
                    }
                    else
                    {
                        if (isGear)
                        {
                            //if (!countedCandidates.Contains(candidate))
                            //{
                            candidates.Add(candidate);
                            //}
                        }
                        candidate = "";
                        isGear = false;
                    }
                }
            }

            countedCandidates.AddRange(candidates);
            if (candidates.Count == 2)
            {
                sum += int.Parse(candidates[0]) * int.Parse(candidates[1]);
            }
        }
        
        return sum;
    }

    public static bool IsAdjacent(int x1, int y1, int x2, int y2)
    {
        /*
         * 1 2 3
         * 4 * 5
         * 6 7 8
         */

        if (x1 - 1 == x2 && y1 - 1 == y2)
            return true;
        if (x1 == x2 && y1 - 1 == y2)
            return true;
        if (x1 + 1 == x2 && y1 - 1 == y2)
            return true;
        if (x1 - 1 == x2 && y1 == y2)
            return true;
        if (x1 + 1== x2 && y1 == y2)
            return true;
        if (x1 - 1 == x2 && y1 + 1 == y2)
            return true;
        if (x1 == x2 && y1 + 1 == y2)
            return true;
        if (x1 + 1 == x2 && y1 + 1 == y2)
            return true;
        return false;
    }

    public static (int y, int x) AdjacentStarCoordinates(string[] rows, int y1, int x1)
    {
        var height = rows.Length;
        var width = rows.First().Length;

        if (y1 > 0)
        {
            if (rows[y1 - 1][x1] == '*')
                return (y1 - 1, x1);
            if (x1 > 0)
            {
                if (rows[y1 - 1][x1 - 1] == '*')
                    return (y1 - 1, x1 - 1);
            }

            if (x1 + 1 < width)
            {
                if (rows[y1 - 1][x1 + 1] == '*')
                    return (y1 - 1, x1 + 1);
            }
        }

        if (x1 > 0)
        {
            if (rows[y1][x1 - 1] == '*')
                return (y1, x1 - 1);
        }

        if (x1 + 1 < width)
        {
            if (rows[y1][x1 + 1] == '*')
                return (y1, x1 + 1);
        }

        if (y1 + 1 < height)
        {
            if (rows[y1 + 1][x1] == '*')
                return (y1 + 1, x1);
            if (x1 > 0)
            {
                if (rows[y1 + 1][x1 - 1] == '*')
                    return (y1 + 1, x1 - 1);
            }
            if (x1 + 1 < width)
            {
                if (rows[y1 + 1][x1 + 1] == '*')
                    return (y1 + 1, x1 + 1);
            }
        }

        return (-1, -1);
    }

    public static bool HasAdjacentStar(string[] rows, int y1, int x1)
    {
        var height = rows.Length;
        var width = rows.First().Length;

        if (y1 > 0)
        {
            if (rows[y1 - 1][x1] == '*')
                return true;
            if (x1 > 0)
            {
                if (rows[y1 - 1][x1 - 1] == '*')
                    return true;
            }

            if (x1 + 1 < width)
            {
                if (rows[y1 - 1][x1 + 1] == '*')
                    return true;
            }
        }

        if (x1 > 0)
        {
            if (rows[y1][x1 - 1] == '*')
                return true;
        }

        if (x1 + 1 < width)
        {
            if (rows[y1][x1 + 1] == '*')
                return true;
        }

        if (y1 + 1 < height)
        {
            if (rows[y1 + 1][x1] == '*')
                return true;
            if (x1 > 0)
            {
                if (rows[y1 + 1][x1 - 1] == '*')
                    return true;
            }
            if (x1 + 1 < width)
            {
                if (rows[y1 + 1][x1 + 1] == '*')
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