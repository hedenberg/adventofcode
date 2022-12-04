using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCode.Day4;

public class CampCleanup : IParsable<CampCleanup>
{
    public required List<AssignmentPair> AssignmentPairs { get; set; }

    public static CampCleanup Parse(string input, IFormatProvider? provider)
    {
        var assignmentPairs = input
            .Rows()
            .Select(pair => pair.Parse<AssignmentPair>())
            .ToList();

        return new CampCleanup
        {
            AssignmentPairs = assignmentPairs
        };
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out CampCleanup result)
    {
        throw new NotImplementedException();
    }

    public int ContainedPairs()
    {
        return AssignmentPairs.Count(p =>
            (p.First.Left <= p.Second.Left && p.First.Right >= p.Second.Right) ||
            (p.Second.Left <= p.First.Left && p.Second.Right >= p.First.Right));
    }

    public int OverlapCount()
    {
        var overlaps = AssignmentPairs.Count(p => p.Overlaps());
        return overlaps;
    }
}

public class AssignmentPair : IParsable<AssignmentPair>
{
    public Assignment First { get; set; }
    public Assignment Second { get; set; }

    public static AssignmentPair Parse(string input, IFormatProvider? provider)
    {
        var (first, second) = input
            .Split(',')
            .Select(a => a.Parse<Assignment>())
            .ToArray();

        return new AssignmentPair
        {
            First = first,
            Second = second
        };
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out AssignmentPair result)
    {
        throw new NotImplementedException();
    }

    public bool Overlaps()
    {
        return First.Overlaps(Second);
    }

    public override string ToString()
    {
        return $"{First},{Second}";
    }
}

public readonly struct Assignment : IParsable<Assignment>
{
    public Assignment(int left, int right)
    {
        Left = left;
        Right = right;
    }

    public int Left { get; }
    public int Right { get; }

    public bool Overlaps(Assignment other)
    {
        return Right >= other.Left && Left <= other.Right;
    }

    public static Assignment Test(string s)
    {
        return new Assignment();
    }

    public static Assignment Parse(string s, IFormatProvider? provider)
    {
        var assignment = s.Split('-');
        var left = assignment[0].Parse<int>();
        var right = assignment[1].Parse<int>();
        return new Assignment(left, right);
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out Assignment result)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return $"{Left}-{Right}";
    }
}