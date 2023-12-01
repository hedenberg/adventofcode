namespace Bohl.AdventOfCode.Day1;

internal class CalibrationValue
{
    public static Dictionary<string, int> Digits = new Dictionary<string, int>
    {
        { "one", 1 },
        { "two", 2 },
        { "six", 6 },
        { "four", 4 },
        { "five", 5 },
        { "nine", 9 },
        { "three", 3 },
        { "seven", 7 },
        { "eight", 8 },
    };

    // ottffssen

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

    internal static int GetLineNumbers(string input)
    {
        var d1 = FirstNumber(input);
        var d2 = LastNumber(input);

        var ds = "" + d1 + d2;
        return int.Parse(ds);
    }

    internal static int FirstNumber(string input)
    {
        if (input is "")
        {
            return 0;
        }
        for (int i = 0; i < input.Length; i++)
        {
            var c = input[i];
            if (char.IsDigit(c))
            {
                return c - '0';
            }

            if (i + 3 > input.Length)
                continue;

            var s3 = input.Substring(i, 3);
            if (Digits.Keys.Contains(s3))
            {
                return Digits[s3];
            }

            if (i + 4 > input.Length)
                continue;

            var s4 = input.Substring(i, 4);
            if (Digits.Keys.Contains(s4))
            {
                return Digits[s4];
            }

            if (i + 5 > input.Length)
                continue;

            var s5 = input.Substring(i, 5);
            if (Digits.Keys.Contains(s5))
            {
                return Digits[s5];
            }
        }

        throw new NotImplementedException("Ooops...");
    }

    internal static int LastNumber(string initial)
    {
        if (initial is "")
        {
            return 0;
        }

        var cs = initial.ToCharArray();
        Array.Reverse(cs);
        var input = new string(cs);

        for (int i = 0; i < input.Length; i++)
        {
            var c = input[i];
            if (char.IsDigit(c))
            {
                return c - '0';
            }

            if (i + 3 > input.Length)
                continue;

            var s3 = input.Substring(i, 3).ToCharArray();
            Array.Reverse(s3);
            var rs3 = new string(s3);
            if (Digits.Keys.Contains(rs3))
            {
                return Digits[rs3];
            }

            if (i + 4 > input.Length)
                continue;

            var s4 = input.Substring(i, 4).ToCharArray();
            Array.Reverse(s4);
            var rs4 = new string(s4);
            if (Digits.Keys.Contains(rs4))
            {
                return Digits[rs4];
            }

            if (i + 5 > input.Length)
                continue;

            var s5 = input.Substring(i, 5).ToCharArray();
            Array.Reverse(s5);
            var rs5 = new string(s5);
            if (Digits.Keys.Contains(rs5))
            {
                return Digits[rs5];
            }
        }

        throw new NotImplementedException("Ooops...");
    }
}