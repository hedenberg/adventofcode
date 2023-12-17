using System.Diagnostics;

namespace Bohl.AdventOfCode.Day10;

internal class PipeMaze
{
    public static int StepsToFarthestPoint(char[][] cells, bool enclosure = false)
    {
        var steps = 0;

        var start = GetPositionOf(cells, 'S');

        var lastLocation = start;
        var currentLocation = start;

        var pipe = new List<(int, int)>();

        do
        {
            var symbol = cells[currentLocation.r][currentLocation.c];

            if (!TryGetPossibleDirections(symbol, out var directions))
            {
                directions = GetStartDirection(currentLocation.r, currentLocation.c, cells);
            }

            var filteredDirections = directions
                .Where(d => (currentLocation.r + d.r, currentLocation.c + d.c) != lastLocation)
                .ToArray();

            var direction = filteredDirections.First();

            lastLocation = currentLocation;
            pipe.Add(lastLocation);
            currentLocation = (currentLocation.r + direction.r, currentLocation.c + direction.c);

            steps++;

        } while (currentLocation != start);

        if (enclosure)
        {
            var ioCells = CopyCells(cells);

            var enclosed = 0;
            for (int r = 0; r < cells.Length; r++)
            {
                var row = cells[r];
                var inside = false;
                var onPipe = false;
                var pipeStart = 'S';
                for (int c = 0; c < row.Length; c++)
                {
                    var symbol = row[c];
                    if (symbol == 'S')
                        symbol = GetStartSymbol(r, c, cells);

                    if (pipe.Contains((r, c)))
                    {
                        if (symbol is '|')
                        {
                            inside = !inside;
                            onPipe = false;
                            pipeStart = 'S';
                            continue;
                        }

                        if (symbol is 'F' or 'L')
                        {
                            onPipe = true;
                            pipeStart = symbol;
                            continue;
                        }

                        if (pipeStart is 'L' && symbol is '7' || pipeStart is 'F' && symbol is 'J')
                        {
                            inside = !inside;
                            onPipe = false;
                            pipeStart = 'S';
                            continue;
                        }
                    }
                    else if (inside)
                    {
                        ioCells[r][c] = 'I';
                        enclosed++;
                    }
                    else if (!inside)
                    {
                        ioCells[r][c] = 'O';
                    }

                    //DebugWriteLine(ioCells, r, c);
                }

                if (inside)
                {

                }
            }

            return enclosed;
        }
        return steps / 2;
    }

    public static bool TryGetPossibleDirections(char symbol, out (int r, int c)[] directions)
    {
        switch (symbol)
        {
            case '|':
                directions = new [] {(-1, 0), (1, 0)};
                return true;
            case '-':
                directions = new[] {(0, -1), (0, 1)};
                return true;
            case 'L':
                directions = new[] { (-1, 0), (0, 1) };
                return true;
            case 'J':
                directions = new[] { (-1, 0), (0, -1) };
                return true;
            case '7':
                directions = new [] { (1, 0), (0, -1) };
                return true;
            case 'F':
                directions = new[] { (1, 0), (0, 1) };
                return true;
            case '.':
            case 'S':
            default:
                directions = default;
                return false;
        }
    }

    public static char GetStartSymbol(int r, int c, char[][] map)
    {
        var directions = GetStartDirection(r, c, map);

        if (directions.Contains((-1, 0)) && directions.Contains((1, 0)))
            return '|';
        if (directions.Contains((0, -1)) && directions.Contains((0, 1)))
            return '-';
        if (directions.Contains((-1, 0)) && directions.Contains((0, 1)))
            return 'L';
        if (directions.Contains((-1, 0)) && directions.Contains((0, -1)))
            return 'J';
        if (directions.Contains((1, 0)) && directions.Contains((0, -1)))
            return '7';
        if (directions.Contains((1, 0)) && directions.Contains((0, 1)))
            return 'F';
        return 'S';
    }

    public static (int r, int c)[] GetStartDirection(int r, int c, char[][] map)
    {
        var directions = new List<(int, int)>();
        if (r > 0)
        {
            if (TryGetPossibleDirections(map[r - 1][c], out var upDirections) && upDirections.Contains((1, 0)))
            {
                directions.Add((-1, 0));
            }
        }

        if (r + 1 < map.Length)
        {
            if (TryGetPossibleDirections(map[r + 1][c], out var downDirections) && downDirections.Contains((-1, 0)))
            {
                directions.Add((1, 0));
            }
        }

        if (c > 0)
        {
            if (TryGetPossibleDirections(map[r][c - 1], out var leftDirections) && leftDirections.Contains((0, 1)))
            {
                directions.Add((0, -1));
            }
        }

        if (c + 1 < map[1].Length)
        {
            if (TryGetPossibleDirections(map[r][c + 1], out var rightDirections) && rightDirections.Contains((0, -1)))
            {
                directions.Add((0, 1));
            }
        }

        return directions.ToArray();
    }

    public static (int r, int c) GetPositionOf(char[][] cells, char target)
    {
        for (int i = 0; i < cells.Length; i++)
        {
            var row = cells[i];
            for (int j = 0; j < row.Length; j++)
            {
                var cell = row[j];

                if (cell == target)
                    return (i, j);
            }
        }

        return (-1, -1);
    }

    private static char[][] CopyCells(char[][] cells)
    {
        var ioCells = new List<char[]>();

        for (int i = 0; i < cells.Length; i++)
        {
            var row = cells[i];
            var target = new char[row.Length];
            row.CopyTo(target, 0);
            ioCells.Add(target);
        }

        return ioCells.ToArray();
    }

    private static void DebugWriteLine(char[][] cells, int? r = null, int? c = null)
    {
        if (r is not null && c is not null)
        {
            Debug.WriteLine($"Row: {r} Cell: {c}");
        }
        for (int i = 0; i < cells.Length; i++)
        {
            var row = cells[i];
            Debug.WriteLine(new string(row));
            //for (int j = 0; j < row.Length; j++)
            //{
            //    var cell = row[j];


            //}
        }
        Debug.WriteLine("");
    }
}