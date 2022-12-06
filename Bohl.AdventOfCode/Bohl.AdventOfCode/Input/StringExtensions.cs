namespace Bohl.AdventOfCode.Input;

public static class StringExtensions
{
    public static List<string> Sections(this string input)
    {
        return input
            .Replace("\r", "")
            .Split("\n\n")
            .ToList();
    }

    public static string[] Rows(this string input)
    {
        return input
            .Replace("\r", "")
            .Split('\n');
    }

    public static T Parse<T>(this string input) where T : IParsable<T>
    {
        return T.Parse(input, null);
    }
}