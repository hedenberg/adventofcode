namespace Bohl.AdventOfCode.Day6;

public static class TuningTrouble
{
    public static int StartOfPacketMarker(string input, int length)
    {
        var window = new Window(length);
        var inputCharacters = input.ToCharArray();
        for (var index = 0; index < inputCharacters.Length; index++)
        {
            if (window.Scan(inputCharacters[index])) 
                return index + 1;
        }

        throw new ArgumentOutOfRangeException("start-of-packet Marker not found");
    }
}

public class Window
{
    private readonly int _length;

    public Window(int length)
    {
        _length = length;
        Characters = new List<char>();
    }

    public List<char> Characters { get; set; }

    public bool Scan(char character)
    {
        if (Characters.Count == _length)
            Characters = Characters.Skip(1).ToList();

        if (Characters.Any(c => c == character))
        {
            Characters = Characters.Skip(Characters.IndexOf(character) + 1).ToList();
            Characters.Add(character);
            return false;
        }

        Characters.Add(character);

        return Characters.Count == _length;
    }
}