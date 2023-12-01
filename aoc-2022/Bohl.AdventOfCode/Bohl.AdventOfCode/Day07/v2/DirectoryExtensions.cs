namespace Bohl.AdventOfCode.Day07.v2;

public static class DirectoryExtensions
{
    public static int TotalSize(this List<Directory> directories)
    {
        return directories.Select(d => d.Size).Sum();
    }
}