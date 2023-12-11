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

    public static char[][] Cells(this string input)
    {
        var rows = input.Rows();

        var cells = new List<char[]>();

        foreach (var row in rows)
        {
            cells.Add(row.ToCharArray());
        }

        return cells.ToArray();
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