namespace Bohl.AdventOfCode.Day07.v2;

public class Directory : DirectoryContent
{
    public List<DirectoryContent> Content { get; set; } = new();

    public override int Size => Content.Select(c => c.Size).Sum();

    public List<Directory> Directories()
    {
        var directories = Content.OfType<Directory>().ToList();
        var subDirectories = directories.SelectMany(d => d.Directories()).ToList();
        directories.AddRange(subDirectories);
        return directories;
    }

    public override string ToString()
    {
        return $"dir {Name}";
    }
}