using System.Numerics;
using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCode.Day11;

public class MonkeySolver : IParsable<MonkeySolver>
{
    public required List<Monkey> Monkeys { get; set; }

    public static MonkeySolver Parse(string input, IFormatProvider? provider)
    {
        var sections = input.Sections();

        var monkeys = sections.Select(section => section.Parse<Monkey>()).ToList();

        foreach (var monkeyItem in monkeys.SelectMany(monkey => monkey.Items))
        {
            monkeyItem.SetupWorryLevels(monkeys);
        }

        return new MonkeySolver
        {
            Monkeys = monkeys
        };
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out MonkeySolver result)
    {
        throw new NotImplementedException();
    }

    public void PerformRounds(int rounds = 20, bool divideByThree = true)
    {
        //Console.WriteLine("");
        for (var i = 0; i < rounds; i++)
        {
            //Console.SetCursorPosition(0, Console.CursorTop - 1);
            //Console.WriteLine($"Round # {i}");
            foreach (var monkey in Monkeys)
            {
                monkey.InspectItems(divideByThree);
                monkey.TestItems();
                monkey.ThrowItems(Monkeys);
            }
        }
    }

    public BigInteger MonkeyBusiness()
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
    public BigInteger Divider { get; set; }
    public int TargetTrue { get; set; }
    public int TargetFalse { get; set; }

    public BigInteger Inspections { get; set; }

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
        var divider = BigInteger.Parse(dividerString);

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

    public void InspectItems(bool divideByThree = true)
    {
        var threads = new List<Thread>();
        foreach (var item in Items)
        {
            Inspections++;

            var thread = new Thread(() =>
            {
                item.Inspect(Operation, divideByThree);
            });
            thread.Start();
            threads.Add(thread);
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }
    }

    public void TestItems()
    {
        var threads = new List<Thread>();
        foreach (var item in Items)
        {
            var thread = new Thread(() =>
            {
                item.TestResult = item.WorryLevels[Divider] % Divider == 0;
            });
            thread.Start();
            threads.Add(thread);
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }
    }

    public void ThrowItems(List<Monkey> monkeys)
    {
        var targetTrue = monkeys.Single(m => m.Id == TargetTrue);
        var targetFalse = monkeys.Single(m => m.Id == TargetFalse);

        foreach (var item in Items)
            if (item.TestResult)
                targetTrue.Items.Add(item);
            else
                targetFalse.Items.Add(item);

        Items.Clear();
    }

    public override string ToString()
    {
        return $"Monkey {Id}: inspected items {Inspections} times.";
    }
}

public class Item : IParsable<Item>
{
    public BigInteger OriginalWorryLevel { get; set; }
    public bool TestResult { get; set; }

    public Dictionary<BigInteger, BigInteger> WorryLevels { get; set; } = new ();

    public static Item Parse(string s, IFormatProvider? provider)
    {
        return new Item
        {
            OriginalWorryLevel = BigInteger.Parse(s),
        };
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out Item result)
    {
        throw new NotImplementedException();
    }

    public void SetupWorryLevels(List<Monkey> monkeys)
    {
        foreach (var monkey in monkeys)
        {
            WorryLevels.Add(monkey.Divider, OriginalWorryLevel);
        }
    }

    public void Inspect(Operation operation, bool divideByThree = true)
    {
        foreach (var (divider, worryLevel) in WorryLevels)
        {
            var updatedWorryLevel = operation.Calculate(worryLevel);
            if (divideByThree)
            {
                updatedWorryLevel /= 3;
            }
            else
            {
                updatedWorryLevel %= divider;
            }
            WorryLevels[divider] = updatedWorryLevel;
        }

        //OriginalWorryLevel = operation.Calculate(OriginalWorryLevel);
    }

    public override string ToString()
    {
        return $"{OriginalWorryLevel}";
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

    public BigInteger Calculate(BigInteger worryLevel)
    {
        var secondValue = Value == "old" ? worryLevel : BigInteger.Parse(Value);

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