namespace Bohl.AdventOfCode.Day1;

public static class CalorieCounter
{
    public static int GetTopCalories(string input)
    {
        var elves = input
            .Split("\n\n")
            .Select(elf
                => elf
                    .Split("\n")
                    .Where(calorie => !string.IsNullOrWhiteSpace(calorie))
                    .Select(int.Parse))
            .Select(elf => elf.Sum()).OrderByDescending(elf => elf);
        return elves.First();
    }
    public static int[] GetTopCalories(int top, string input)
    {
        var elves = input
            .Split("\n\n")
            .Select(elf
                => elf
                    .Split("\n")
                    .Where(calorie => !string.IsNullOrWhiteSpace(calorie))
                    .Select(int.Parse))
            .Select(elf => elf.Sum()).OrderByDescending(elf => elf);
        return elves.Take(top).ToArray();
    }
}