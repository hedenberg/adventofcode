using System.Text.RegularExpressions;

namespace Bohl.AdventOfCode.Day02;

internal class Cube
{
    public static int Sum(List<string> input, int red, int green, int blue)
    {
        var sum = 0;

        for (var index = 0; index < input.Count; index++)
        {
            var item = input[index];

            if (IsGamePossible(item, red, green, blue))
                sum += index + 1;
        }

        return sum;
    }

    public static int Sum(List<string> input)
    {
        var sum = 0;
        foreach (var row in input)
        {
            sum += Power(row);
        }
        return sum;
    }

    public static int Power(string row)
    {
        return GetMaxUse(row, "red") * GetMaxUse(row, "green") * GetMaxUse(row, "blue");
    }

    public static bool IsGamePossible(string row, int red, int green, int blue)
    {
        if (GetGameColorValue(row, "red").Any(v => v > red))
            return false;
        if (GetGameColorValue(row, "green").Any(v => v > green))
            return false;
        if (GetGameColorValue(row, "blue").Any(v => v > blue))
            return false;
        return true;
    }

    public static int GetMaxUse(string row, string color)
    {
        return GetGameColorValue(row, color).Max();
    }

    public static int[] GetGameColorValue(string row, string color)
    {
        var colorValues = new List<int>();
        var rg = new Regex(@"[ ]\d+[ ]\w*");

        var match = rg.Match(row);

        while (match.Success)
        {
            if (match.Value.Contains(color))
            {
                var value = match.Value.Replace(color, "").Replace(" ", "");
                colorValues.Add(int.Parse(value));
            }
            match = match.NextMatch();
        }

        return colorValues.ToArray();
    }
}