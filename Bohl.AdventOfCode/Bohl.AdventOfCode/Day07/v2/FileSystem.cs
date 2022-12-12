using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCode.Day07.v2;

public class FileSystem : IParsable<FileSystem>
{
    public required Directory Root { get; set; }

    public static FileSystem Parse(string input, IFormatProvider? provider)
    {
        var rows = input.Rows();

        if (rows.Length < 1 || rows[0] != "$ cd /")
            throw new ArgumentException("First command must be Change Directory to root ('/').");

        var root = new Directory
        {
            Name = "/",
            Content = new List<DirectoryContent>()
        };

        var history = new List<Directory>(); // Stores the last directory when changing directory
        var currentDirectory = root;
        var terminalOutputs = rows.Skip(1).ToList();

        while (terminalOutputs.Any())
        {
            var terminalOutput = terminalOutputs.First();

            ExecutedCommand cmd;
            while (!terminalOutput.TryParse(out cmd))
            {
                var directoryContent = terminalOutput.Parse<DirectoryContent>();
                currentDirectory.Content.Add(directoryContent);
                terminalOutputs = terminalOutputs.Skip(1).ToList();
                if (!terminalOutputs.Any())
                    return new FileSystem
                    {
                        Root = root
                    };
                terminalOutput = terminalOutputs.First();
            }


            if (cmd.Cmd is Command.ChangeDirectory)
            {
                if (cmd.DirectoryName == "..")
                {
                    currentDirectory = history.Last();
                    history = history.SkipLast(1).ToList();
                }
                else
                {
                    history.Add(currentDirectory);
                    currentDirectory = currentDirectory.Content
                        .OfType<Directory>()
                        .Single(d => d.Name == cmd.DirectoryName);
                }
            }

            terminalOutputs = terminalOutputs.Skip(1).ToList();
        }

        return new FileSystem
        {
            Root = root
        };
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out FileSystem result)
    {
        throw new NotImplementedException();
    }

    public List<Directory> Directories(int maxSize)
    {
        var directories = Root
            .Directories()
            .Where(d => d.Size <= maxSize)
            .ToList();
        return directories;
    }

    public Directory DirectoryToDelete(int total, int required)
    {
        var target = required - (total - Root.Size);
        var directories = Root
            .Directories()
            .OrderBy(d => d.Size);

        return directories.First(d => d.Size > target);
    }
}