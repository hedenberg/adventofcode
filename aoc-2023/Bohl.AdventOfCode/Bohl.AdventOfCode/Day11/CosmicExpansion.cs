using System.Diagnostics;

namespace Bohl.AdventOfCode.Day11;

internal class CosmicExpansion
{
    public static int Sum(string input)
    {
        var cells = input.CharRows();

        Expand(cells);

        //DebugPrint(cells);

        var galaxies = Galaxies(cells);

        //DebugPrint(cells, galaxies);

        var sum = 0;

        var pairs = 0;

        if (galaxies.Any())
        {
            for (int i = 0; i < galaxies.Count - 1; i++)
            {
                var start = galaxies[i];
                for (int j = i; j < galaxies.Count; j++)
                {
                    pairs++;
                    var target = galaxies[j];
                    sum += CalculateDistance(start, target);
                }
            }
        }

        return sum;
    }

    private static int CalculateDistance(Galaxy start, Galaxy target)
    {
        // Assumptions: ordered by Y (target.y >= start.y)
        // X can be whatever

        var distance = 0;

        if (start.X > target.X)
        {
            distance += start.X - target.X;
        }
        else
        {
            distance += target.X - start.X;
        }

        distance += target.Y - start.Y;

        return distance;
    }

    private static List<Galaxy> Galaxies(List<List<char>> cells)
    {
        var galaxies = new List<Galaxy>();
        for (int y = 0; y < cells.Count; y++)
        {
            var row = cells[y];
            for (int x = 0; x < row.Count; x++)
            {
                var symbol = row[x];

                if (symbol is '#')
                {
                    galaxies.Add(new Galaxy(x, y));
                }
            }
        }

        return galaxies;
    }

    private static void Expand(List<List<char>> cells)
    {
        var emptyRows = new List<int>();
        for (var y = 0; y < cells.Count; y++)
        {
            var row = cells[y];
            if (!row.Any(c => c is '#'))
            {
                emptyRows.Add(y);
            }
        }

        var emptyColumns = new List<int>();
        for (var x = 0; x < cells.First().Count; x++)
        {
            var column = cells.Select(row => row[x]);
            if (!column.Any(c => c is '#'))
            {
                emptyColumns.Add(x);
            }
        }

        for (var i = 0; i < emptyRows.Count; i++)
        {
            var emptyRow = emptyRows[i];
            cells.Insert(emptyRow + i, Enumerable.Repeat('.', cells[0].Count).ToList());
        }

        for (var i = 0; i < emptyColumns.Count; i++)
        {
            var emptyColumn = emptyColumns[i];
            foreach (var row in cells)
            {
                row.Insert(emptyColumn + i, '.');
            }
        }
    }

    private static void DebugPrint(List<List<char>> cells)
    {
        Debug.WriteLine("");

        foreach (var row in cells)
        {
            Debug.WriteLine(new string(row.ToArray()));
        }
    }

    private static void DebugPrint(List<List<char>> cells, List<Galaxy> galaxies)
    {
        var galaxyCells = new List<List<char>>(cells);

        if (galaxies.Count < 10)
        {
            for (int y = 0; y < galaxyCells.Count; y++)
            {
                var row = galaxyCells[y];
                for (int x = 0; x < row.Count; x++)
                {
                    var galaxy = galaxies.SingleOrDefault(g => g.X == x && g.Y == y);
                    if (galaxy != null)
                    {
                        row[x] = char.Parse(galaxy.Id.ToString());
                    }
                }
            }
        }

        DebugPrint(galaxyCells);
    }
}

internal class Galaxy
{
    private static int nextId;
    public int Id { get; private set; }
    public int X { get; set; }
    public int Y { get; set; }

    internal Galaxy(int x, int y)
    {
        Id = Interlocked.Increment(ref nextId);
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"{Id}: ({X}, {Y})";
    }
}