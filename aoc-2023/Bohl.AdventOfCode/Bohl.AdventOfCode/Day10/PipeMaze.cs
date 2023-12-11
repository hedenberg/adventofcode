namespace Bohl.AdventOfCode.Day10;

internal class PipeMaze
{
    public static int StepsToFarthestPoint(char[][] cells)
    {
        var steps = 0;

        var start = GetPositionOf(cells, 'S');

        var lastLocation = start;
        var currentLocation = start;

        do
        {
            var symbol = cells[currentLocation.r][currentLocation.c];

            //var directions = GetPossibleDirections(symbol);
            if (!TryGetPossibleDirections(symbol, out var directions))
            {
                directions = GetStartDirection(currentLocation.r, currentLocation.c, cells);
            }

            var filteredDirections = directions
                .Where(d => (currentLocation.r + d.r, currentLocation.c + d.c) != lastLocation)
                .ToArray();

            var direction = filteredDirections.First();

            lastLocation = currentLocation;
            currentLocation = (currentLocation.r + direction.r, currentLocation.c + direction.c);

            steps++;


        } while (currentLocation != start);


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

        if (c + 1 < map.Length)
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
}