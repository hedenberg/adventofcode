using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCode.Day07.v2;

public class ExecutedCommand : IParsable<ExecutedCommand>
{
    public Command Cmd { get; set; }

    public string? DirectoryName { get; set; }

    public static ExecutedCommand Parse(string input, IFormatProvider? provider)
    {
        if (input == "$ ls")
            return new ExecutedCommand
            {
                Cmd = Command.ListDirectoryContents
            };

        var (_, _, dir) = input.Split(' ');
        return new ExecutedCommand
        {
            Cmd = Command.ChangeDirectory,
            DirectoryName = dir
        };
    }

    public static bool TryParse(string? input, IFormatProvider? provider, out ExecutedCommand result)
    {
        // Assuming very good input
        if (input?[..1] != "$")
        {
            result = default!;
            return false;
        }

        result = Parse(input, null);
        return true;
    }
}