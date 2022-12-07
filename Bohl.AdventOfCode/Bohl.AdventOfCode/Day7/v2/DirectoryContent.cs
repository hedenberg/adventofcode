using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCode.Day7.v2;

public class DirectoryContent : IParsable<DirectoryContent>
{
    public required string Name { get; set; }
    public virtual int Size { get; set; }

    public static DirectoryContent Parse(string input, IFormatProvider? provider)
    {
        var (prefix, name) = input.Split(' ');

        if (prefix == "dir")
            return new Directory
            {
                Name = name
            };

        var size = int.Parse(prefix);
        return new File
        {
            Name = name,
            Size = size
        };
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out DirectoryContent result)
    {
        throw new NotImplementedException();
    }
}