using System.Diagnostics;

namespace Bohl.AdventOfCode.Day11;

internal class CosmicExpansion
{
    public static long Sum(string input, long expansion = 2)
    {
        var cells = input.CharRows();

        //Expand(cells);

        var expansionRows = ExpansionRows(cells);
        var expansionColumns = ExpansionColumns(cells);

        var galaxies = Galaxies(cells);

        var sum = 0L;

        if (galaxies.Any())
        {
            for (var i = 0; i < galaxies.Count - 1; i++)
            {
                var start = galaxies[i];
                for (var j = i; j < galaxies.Count; j++)
                {
                    var target = galaxies[j];
                    sum += CalculateDistance(start, target, expansion, expansionRows, expansionColumns);
                }
            }
        }

        return sum;
    }

    private static long CalculateDistance(Galaxy start, Galaxy target, long expansion, List<int> rows, List<int> columns)
    {
        // Assumptions: ordered by Y (target.y >= start.y)
        // X can be whatever

        var distance = 0L;

        if (start.X > target.X)
        {
            var expansionColumns = columns.Where(x => x > target.X && x < start.X).ToList();
            distance += start.X - target.X + expansionColumns.Count() * (expansion - 1);
        }
        else
        {
            var expansionColumns = columns.Where(x => x > start.X && x < target.X).ToList();
            distance += target.X - start.X + expansionColumns.Count() * (expansion - 1);
        }

        var expansionRows = rows.Where(y => y > start.Y && y < target.Y).ToList();
        distance += target.Y - start.Y + expansionRows.Count() * (expansion - 1);

        return distance;
    }

    private static List<Galaxy> Galaxies(List<List<char>> cells)
    {
        var galaxies = new List<Galaxy>();
        for (var y = 0; y < cells.Count; y++)
        {
            var row = cells[y];
            for (var x = 0; x < row.Count; x++)
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

    private static List<int> ExpansionRows(List<List<char>> cells)
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

        return emptyRows;
    }

    private static List<int> ExpansionColumns(List<List<char>> cells)
    {
        var emptyColumns = new List<int>();
        for (var x = 0; x < cells.First().Count; x++)
        {
            var column = cells.Select(row => row[x]);
            if (!column.Any(c => c is '#'))
            {
                emptyColumns.Add(x);
            }
        }

        return emptyColumns;
    }

    private static void Expand(List<List<char>> cells)
    {
        var emptyRows = ExpansionRows(cells);

        var emptyColumns = ExpansionColumns(cells);

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