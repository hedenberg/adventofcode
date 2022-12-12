using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCode.Day07;

public class Terminal
{
    public Terminal(string input)
    {
        Index = 0;
        Rows = input.Rows();
        Root = new Directory
        {
            Name = "/",
            DirectoryItems = Read()
        };
    }

    public int Index { get; set; }
    public string[] Rows { get; set; }
    public Directory Root { get; set; }

    public List<IDirectoryItem> Read()
    {
        Index++;
        var directoryItems = Ls();
        if (Index == Rows.Length) return directoryItems;

        var cmdRow = Rows[Index];
        while (cmdRow[0] == '$')
        {
            var (_, cmd, dir) = cmdRow.Split(' ');
            if (cmd != "cd") throw new NotImplementedException("");

            if (dir == "..") return directoryItems;

            var directory = (Directory)directoryItems.Single(d => d.Name == dir);
            directory.DirectoryItems = Read();
            if (++Index >= Rows.Length) return directoryItems;
            cmdRow = Rows[Index];
        }

        return directoryItems;
    }

    public List<Directory> GetDirectories()
    {
        var directories = Root.DirectoryItems.OfType<Directory>().ToList();
        var subDirectories = directories.SelectMany(d => d.GetDirectories()).ToList();
        directories.AddRange(subDirectories);
        return directories;
    }

    public int TotalSizeOfCandidates(int maxSize)
    {
        var candidates = CandidatesForDeletion(100000);
        var size = candidates.Select(d => d.Size).Sum();
        return size;
    }

    public List<Directory> CandidatesForDeletion(int maxSize)
    {
        var directories = GetDirectories()
            .Where(d => d.Size < maxSize)
            .ToList();
        return directories;
    }

    public List<IDirectoryItem> Ls()
    {
        Index++;
        var directoryItems = new List<IDirectoryItem>();

        var row = Rows[Index];
        while (row[0] != '$')
        {
            var directoryItem = ReadItem(row);
            directoryItems.Add(directoryItem);
            if (++Index == Rows.Length) return directoryItems;
            row = Rows[Index];
        }

        return directoryItems;
    }

    public static IDirectoryItem ReadItem(string row)
    {
        var (prefix, name) = row.Split(' ');
        if (prefix == "dir")
            return new Directory
            {
                Name = name,
                DirectoryItems = new List<IDirectoryItem>()
            };

        return new File
        {
            Name = name,
            Size = int.Parse(prefix)
        };
    }

    public int SmallestDirectoryMeetingRequirementSize(int totalDiskSpace, int requiredDiskSpace)
    {
        var usedSpace = Root.Size;
        var freeSpace = totalDiskSpace - usedSpace;
        var target = requiredDiskSpace - freeSpace;

        var candidates = GetDirectories().OrderBy(d => d.Size);

        var toBeDeleted = candidates.First(d => d.Size > target);
        return toBeDeleted.Size;
    }
}

public class Directory : IDirectoryItem
{
    public required List<IDirectoryItem> DirectoryItems { get; set; }
    public required string Name { get; set; }

    public int Size => DirectoryItems.Select(i => i.Size).Sum();

    public List<Directory> GetDirectories()
    {
        var directories = DirectoryItems.OfType<Directory>().ToList();
        var subDirectories = directories.SelectMany(d => d.GetDirectories()).ToList();
        directories.AddRange(subDirectories);
        return directories;
    }

    public override string ToString()
    {
        return $"dir {Name}";
    }
}

public class File : IDirectoryItem
{
    public required string Name { get; set; }
    public required int Size { get; set; }

    public override string ToString()
    {
        return $"{Size} {Name}";
    }
}

public interface IDirectoryItem
{
    public string Name { get; set; }
    public int Size { get; }
}