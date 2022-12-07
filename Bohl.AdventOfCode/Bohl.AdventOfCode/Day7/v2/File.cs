namespace Bohl.AdventOfCode.Day7.v2;

public class File : DirectoryContent
{
    public override string ToString()
    {
        return $"{Size} {Name}";
    }
}