using Bohl.AdventOfCode;
using Bohl.AdventOfCode.Day07.v2;
using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCodeTests.Day07Tests.v2;

public class FileSystemTests
{
    [Test]
    public void Day7_Challenge1_Test()
    {
        var filesystem = Inputs.Day7Tests.Parse<FileSystem>();

        var directories = filesystem.Directories(100000);
        var size = directories.TotalSize();

        Assert.That(size == 95437);
    }

    [Test]
    public void Day7_Challenge1()
    {
        var filesystem = Inputs.Day7Challenge.Parse<FileSystem>();

        var directories = filesystem.Directories(100000);
        var size = directories.TotalSize();

        Assert.That(size == 1086293);
    }

    [Test]
    public void Day7_Challenge2_Test()
    {
        var total = 70000000;
        var required = 30000000;

        var filesystem = Inputs.Day7Tests.Parse<FileSystem>();

        var candidate = filesystem.DirectoryToDelete(total, required);
        var size = candidate.Size;

        Assert.That(size == 24933642);
    }

    [Test]
    public void Day7_Challenge2()
    {
        var total = 70000000;
        var required = 30000000;

        var filesystem = Inputs.Day7Challenge.Parse<FileSystem>();

        var candidate = filesystem.DirectoryToDelete(total, required);
        var size = candidate.Size;

        Assert.That(size == 366028);
    }
}