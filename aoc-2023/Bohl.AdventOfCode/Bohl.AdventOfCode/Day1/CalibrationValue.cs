namespace Bohl.AdventOfCode.Day1;

internal class CalibrationValue
{
    internal static int GetLineDigits(string input)
    {
        var cs = input.ToCharArray();
        var d1 = FirstDigit(cs);
        Array.Reverse(cs);
        var d2 = FirstDigit(cs);

        var ds = "" + d1 + d2;
        return int.Parse(ds);
    }

    internal static char FirstDigit(char[] cs)
    {
        return cs.SkipWhile(c => !char.IsDigit(c))
            .First(char.IsDigit);
    }
    internal static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}