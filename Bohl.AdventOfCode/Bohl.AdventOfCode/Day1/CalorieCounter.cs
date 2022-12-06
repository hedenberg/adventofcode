using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCode.Day1;

public static class CalorieCounter
{
    public static int GetTopCalories(string input)
    {
        return GetTopCalories(1, input).First();
    }

    public static int[] GetTopCalories(int top, string input)
    {
        var elves = input
            .Sections()
            .Select(elf => elf
                .Rows()
                .Select(int.Parse)
                .Sum())
            .OrderByDescending(elf => elf);
        return elves
            .Take(top)
            .ToArray();
    }
}