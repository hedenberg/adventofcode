using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCode.Day11;

public class MonkeySolver : IParsable<MonkeySolver>
{
    public required List<Monkey> Monkeys { get; set; }

    public static MonkeySolver Parse(string input, IFormatProvider? provider)
    {
        var sections = input.Sections();

        var monkeys = sections.Select(section => section.Parse<Monkey>()).ToList();

        return new MonkeySolver
        {
            Monkeys = monkeys
        };
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out MonkeySolver result)
    {
        throw new NotImplementedException();
    }

    public void PerformRounds()
    {
        for (var i = 0; i < 20; i++)
            foreach (var monkey in Monkeys)
            {
                monkey.InspectItems();
                monkey.ThrowItems(Monkeys);
            }
    }

    public int MonkeyBusiness()
    {
        var monkeys = Monkeys
            .OrderByDescending(m => m.Inspections)
            .Take(2)
            .ToArray();
        var totalInspections = monkeys[0].Inspections * monkeys[1].Inspections;

        return totalInspections;
    }
}

public class Monkey : IParsable<Monkey>
{
    public int Id { get; set; }
    public required List<Item> Items { get; set; }
    public required Operation Operation { get; set; }
    public int Divider { get; set; }
    public int TargetTrue { get; set; }
    public int TargetFalse { get; set; }

    public int Inspections { get; set; }

    public static Monkey Parse(string input, IFormatProvider? provider)
    {
        var rows = input.Rows();
        var (_, name) = rows[0].Split(' ');
        var id = int.Parse(name.Split(":").First());

        var (_, itemsString) = rows[1].Replace(" ", "").Split(":");
        var items = itemsString.Split(",").Select(s => s.Parse<Item>()).ToList();

        var (_, operationInput) = rows[2].Split("  Operation: ");
        var operation = operationInput.Parse<Operation>();

        var (_, dividerString) = rows[3].Split("  Test: divisible by ");
        var divider = int.Parse(dividerString);

        var targetTrue = int.Parse(rows[4].Last().ToString());

        var targetFalse = int.Parse(rows[5].Last().ToString());

        return new Monkey
        {
            Id = id,
            Items = items,
            Operation = operation,
            Divider = divider,
            TargetTrue = targetTrue,
            TargetFalse = targetFalse
        };
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out Monkey result)
    {
        throw new NotImplementedException();
    }

    public void InspectItems()
    {
        foreach (var item in Items)
        {
            Inspections++;
            item.Inspect(Operation);
            item.WorryLevel /= 3;
        }
    }

    public void ThrowItems(List<Monkey> monkeys)
    {
        var targetTrue = monkeys.Single(m => m.Id == TargetTrue);
        var targetFalse = monkeys.Single(m => m.Id == TargetFalse);

        foreach (var item in Items)
            if (item.WorryLevel % Divider == 0)
                targetTrue.Items.Add(item);
            else
                targetFalse.Items.Add(item);

        Items.Clear();
    }

    public override string ToString()
    {
        return $"Monkey {Id}: {string.Join(", ", Items)}";
    }
}

public class Item : IParsable<Item>
{
    public long WorryLevel { get; set; }

    public static Item Parse(string s, IFormatProvider? provider)
    {
        return new Item
        {
            WorryLevel = int.Parse(s)
        };
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out Item result)
    {
        throw new NotImplementedException();
    }

    public void Inspect(Operation operation)
    {
        WorryLevel = operation.Calculate(WorryLevel);
    }

    public override string ToString()
    {
        return $"{WorryLevel}";
    }
}

public class Operation : IParsable<Operation>
{
    public string Operator { get; set; }
    public string Value { get; set; }

    public static Operation Parse(string input, IFormatProvider? provider)
    {
        var (_, _, _, op, value) = input.Split(" ");
        return new Operation
        {
            Operator = op,
            Value = value
        };
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out Operation result)
    {
        throw new NotImplementedException();
    }

    public long Calculate(long worryLevel)
    {
        var secondValue = Value == "old" ? worryLevel : int.Parse(Value);

        return Operator switch
        {
            "*" => worryLevel * secondValue,
            "+" => worryLevel + secondValue,
            _ => throw new NotImplementedException()
        };
    }

    public override string ToString()
    {
        return $"new = old {Operator} {Value}";
    }
}