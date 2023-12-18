namespace Bohl.AdventOfCode;

public static class StringExtensions
{
    public static string[] Sections(this string input)
    {
        return input
            .Replace("\r", "")
            .Split("\n\n");
    }

    public static string[] Rows(this string input)
    {
        return input
            .Replace("\r", "")
            .Split('\n');
    }

    public static char[][] RowsAndColumns(this string input)
    {
        var rows = input.Rows();

        var cells = new List<char[]>();

        foreach (var row in rows)
        {
            cells.Add(row.ToCharArray());
        }

        return cells.ToArray();
    }

    public static List<List<char>> CharRows(this string input)
    {
        var rows = input.Rows();

        return rows.Select(row => row.ToCharArray().ToList()).ToList();
    }

    public static char[,] Cells(this string input)
    {
        var rows = input.Rows();
        var height = rows.Length;
        if (height == 0)
        {
            return new char[0,0];
        }
        var width = rows[0].Length;
        var cells = new char[height,width];

        for (var r = 0; r < rows.Length; r++)
        {
            var row = rows[r];
            var rowCells = row.ToCharArray();
            for (var c = 0; c < rowCells.Length; c++)
            {
                var cell = rowCells[c];
                cells[r,c] = cell;
            }
        }

        return cells;
    }

    public static T Parse<T>(this string input) where T : IParsable<T>
    {
        return T.Parse(input, null);
    }

    public static bool TryParse<T>(this string input, out T result) where T : IParsable<T>
    {
        return T.TryParse(input, null, out result);
    }
}