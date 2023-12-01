namespace Bohl.AdventOfCode.Input;

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

    public static T Parse<T>(this string input) where T : IParsable<T>
    {
        return T.Parse(input, null);
    }

    public static bool TryParse<T>(this string input, out T result) where T : IParsable<T>
    {
        return T.TryParse(input, null, out result);
    }
}