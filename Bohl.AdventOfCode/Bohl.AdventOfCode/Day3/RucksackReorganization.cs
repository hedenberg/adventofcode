using System.Linq;

namespace Bohl.AdventOfCode.Day3;

public class RucksackReorganization
{
    public List<Rucksack> Rucksacks { get; set; }

    public static RucksackReorganization ParseInputWithHalves(string input)
    {
        var rucksacks = input.Split('\n').Select(r => new Rucksack(r)).ToList();

        return new RucksackReorganization
        {
            Rucksacks = rucksacks
        };
    }

    public int MisplacedPrioritySummary()
    {
        return Rucksacks.Select(r => r.MisplacedItemPriority()).Sum();
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

    public int MisplacedItemPriority()
    {
        return Compartment1.First(item1 => Compartment2.Any(item2 => item1.Priority() == item2.Priority())).Priority();
    }

    public override string ToString()
    {
        return $"Compartment1:\n{string.Join('\n', Compartment1.ToList().Select(i => i.ToString()))}\nCompartment2:\n{string.Join('\n', Compartment2.ToList().Select(i => i.ToString()))}";
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
            result =  _character - 'a' + 1;
        return result;
    }

    public override string ToString()
    {
        return $"{_character}:{Priority()}";
    }
}