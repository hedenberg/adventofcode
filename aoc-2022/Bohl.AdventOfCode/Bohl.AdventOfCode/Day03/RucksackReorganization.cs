using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCode.Day03;

public class RucksackReorganization
{
    public RucksackReorganization(List<ElfGroup> elfGroups)
    {
        ElfGroups = elfGroups;
    }

    public List<ElfGroup> ElfGroups { get; set; }

    public static RucksackReorganization ParseInput(string input)
    {
        var rucksacks = input.Rows();

        var elfGroups = new List<ElfGroup>();
        for (var i = 0; i < rucksacks.Length; i = i + 3)
            elfGroups.Add(ElfGroup.ParseInput(new List<string>
            {
                rucksacks[i],
                rucksacks[i + 1],
                rucksacks[i + 2]
            }));

        return new RucksackReorganization(elfGroups);
    }

    public int BadgePrioritySummary()
    {
        return ElfGroups.Select(g => g.BadgePriority()).Sum();
    }
}

public class ElfGroup
{
    public List<Rucksack> Rucksacks { get; set; }

    public static ElfGroup ParseInput(string input)
    {
        return ParseInput(input.Rows());
    }

    public static ElfGroup ParseInput(IEnumerable<string> input)
    {
        var rucksacks = input.Select(r => new Rucksack(r)).ToList();

        return new ElfGroup
        {
            Rucksacks = rucksacks
        };
    }

    public int MisplacedPrioritySummary()
    {
        return Rucksacks.Select(r => r.MisplacedItemPriority()).Sum();
    }

    public int BadgePriority()
    {
        var rucksacks = Rucksacks
            .Select(rs => rs.Items)
            .ToList();

        var badge = rucksacks
            .First()
            .First(item => rucksacks
                .All(rucksack => rucksack
                    .Any(i => i.Priority() == item.Priority())));
        return badge.Priority();
    }
}

public class Rucksack
{
    public Rucksack(string items)
    {
        Compartment1 = items[..(items.Length / 2)].Select(i => new Item(i)).ToArray();
        Compartment2 = items[(items.Length / 2)..].Select(i => new Item(i)).ToArray();
    }

    public Item[] Compartment1 { get; set; }
    public Item[] Compartment2 { get; set; }
    public Item[] Items => Compartment1.Concat(Compartment2).ToArray();

    public int MisplacedItemPriority()
    {
        return Compartment1.First(item1 => Compartment2.Any(item2 => item1.Priority() == item2.Priority())).Priority();
    }

    public override string ToString()
    {
        return
            $"Compartment1:\n{string.Join('\n', Compartment1.ToList().Select(i => i.ToString()))}\nCompartment2:\n{string.Join('\n', Compartment2.ToList().Select(i => i.ToString()))}";
    }
}

public class Item
{
    private readonly char _character;

    public Item(char character)
    {
        _character = character;
    }

    public int Priority()
    {
        int result;
        // a-z 1-26
        // A-Z 27-52
        if (_character >= 'A' && _character <= 'Z')
            result = _character - 'A' + 27;
        else
            result = _character - 'a' + 1;
        return result;
    }

    public override string ToString()
    {
        return $"{_character}:{Priority()}";
    }
}