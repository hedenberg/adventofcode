namespace Bohl.AdventOfCode.Day9.Part2;

public enum Direction
{
    Right,
    Left,
    Up,
    Down
}

public static class DirectionExtensions
{
    public static Direction Parse(this string input)
    {
        return input switch
        {
            "R" => Direction.Right,
            "L" => Direction.Left,
            "U" => Direction.Up,
            "D" => Direction.Down,
            _ => throw new NotImplementedException()
        };
    }
}