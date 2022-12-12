using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCode.Day09.Part2;

public class Motion : IParsable<Motion>
{
    public Direction Direction { get; set; }
    public int Count { get; set; }

    public static Motion Parse(string input, IFormatProvider? provider)
    {
        var (d, c) = input.Split(' ');

        var direction = d.Parse();
        var count = c.Parse<int>();

        return new Motion
        {
            Direction = direction,
            Count = count
        };
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out Motion result)
    {
        throw new NotImplementedException();
    }
}