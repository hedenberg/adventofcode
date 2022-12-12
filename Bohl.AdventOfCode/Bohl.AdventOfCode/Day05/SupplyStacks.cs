using System.Text.RegularExpressions;
using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCode.Day05;

public class SupplyStacks : IParsable<SupplyStacks>
{
    public required List<Stack> Stacks { get; set; }
    public required List<Move> Moves { get; set; }

    public static SupplyStacks Parse(string input, IFormatProvider? provider)
    {
        var (stacksSection, movesSection) = input.Sections().ToArray();

        var stackRows = stacksSection.Rows().ToList();
        stackRows.Reverse();
        var stacks = new List<Stack>();
        var indexRow = stackRows.First().ToCharArray();
        for (var i = 0; i < indexRow.Length; i++)
        {
            if (!int.TryParse(indexRow[i].ToString(), out var stackId))
                continue;
            var crates = stackRows
                .Skip(1)
                .Select(s => s.ToCharArray()[i])
                .Where(c => c != ' ')
                .ToList();
            stacks.Add(new Stack
            {
                Id = stackId,
                Crates = crates
            });
        }

        var moves = movesSection.Rows().Select(m => m.Parse<Move>()).ToList();

        return new SupplyStacks
        {
            Stacks = stacks,
            Moves = moves
        };
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out SupplyStacks result)
    {
        throw new NotImplementedException();
    }

    public void PerformMovesOneByOne()
    {
        foreach (var move in Moves)
        {
            var sourceStack = Stacks.Single(s => s.Id == move.SourceId);
            var targetStack = Stacks.Single(s => s.Id == move.TargetId);
            for (var i = 0; i < move.Quantity; i++)
            {
                var crate = sourceStack.Crates.TakeLast(1).Single();
                sourceStack.Crates.RemoveAt(sourceStack.Crates.Count - 1);
                targetStack.Crates.Add(crate);
            }
        }
    }

    public void PerformMoves()
    {
        foreach (var move in Moves)
        {
            var sourceStack = Stacks.Single(s => s.Id == move.SourceId);
            var targetStack = Stacks.Single(s => s.Id == move.TargetId);
            var crates = sourceStack.Crates.TakeLast(move.Quantity).ToList();
            sourceStack.Crates.RemoveRange(sourceStack.Crates.Count - move.Quantity, move.Quantity);
            targetStack.Crates.AddRange(crates);
        }
    }

    public string GetTopCrates()
    {
        return Stacks
            .Aggregate("", (current, stack) => current + stack.Crates.TakeLast(1).Single());
    }
}

public class Stack
{
    public int Id { get; set; }
    public required List<char> Crates { get; set; }

    public override string ToString()
    {
        return $"{Id} [{string.Join("][", Crates)}]";
    }
}

public struct Move : IParsable<Move>
{
    public int Quantity { get; set; }
    public int SourceId { get; set; }
    public int TargetId { get; set; }

    public static Move Parse(string input, IFormatProvider? provider)
    {
        var regex = new Regex("move (\\d+) from (\\d+) to (\\d+)");
        var (q, s, t) = regex
            .Split(input)
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(int.Parse)
            .ToArray();
        return new Move
        {
            Quantity = q,
            SourceId = s,
            TargetId = t
        };
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out Move result)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return $"move {Quantity} from {SourceId} to {TargetId}";
    }
}