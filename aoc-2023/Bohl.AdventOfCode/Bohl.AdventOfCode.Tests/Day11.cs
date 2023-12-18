using Bohl.AdventOfCode.Day11;
using FluentAssertions;

namespace Bohl.AdventOfCode.Tests;

public class Day11
{
    [Fact]
    public void Day11_PartOne_Example1()
    {
        var val = CosmicExpansion.Sum(Example1Input);

        val.Should().Be(374);
    }

    [Fact]
    public void Day11_PartOne_Test()
    {
        var val = CosmicExpansion.Sum(PuzzleInput);

        val.Should().Be(9681886);
    }

    [Fact]
    public void Day11_PartTwo_Example1()
    {
        var val = CosmicExpansion.Sum(Example1Input, 10);

        val.Should().Be(1030);
    }

    [Fact]
    public void Day11_PartTwo_Example2()
    {
        var val = CosmicExpansion.Sum(Example1Input, 100);

        val.Should().Be(8410);
    }

    [Fact]
    public void Day11_PartTwo_Test()
    {
        var val = CosmicExpansion.Sum(PuzzleInput, 1000000);

        val.Should().BeLessThan(791134890760);
        val.Should().Be(791134099634);
    }

    private const string Example1Input = @"...#......
.......#..
#.........
..........
......#...
.#........
.........#
..........
.......#..
#...#.....";

    private const string Example2Input = @"";

    private const string PuzzleInput = @".#......................#........................#.........................#................#...............................................
...........................................................................................................#...........#....................
........#...................#.......#............................................#....................#.................................#...
.........................................#............#.........................................#...........................................
....................................................................................................................................#.......
#...........................................................................................................................................
................#......................................................#.............#......................................................
......#..............#.........#......#........................#............................................................................
............#.....................................#......................................#...........................#......................
.....................................................................................................#....................#.................
.....................................................................#.......#............................#......#..............#...........
#...........................................................................................................................................
...................................#.....................................#..................................................................
.........#................#................................#...........................#......#.......................#..................#..
...............#....................................#..........................................................#............................
...........................................#..................................................................................#.............
...............................#...........................................#.......#.................#......................................
#..............................................................#..........................................#.................................
...........#.........................................................#.............................................#........................
......................#...........#...........................................#..........#.....#............................................
........................................................#...............................................................#......#............
...#..........#.........................#................................#..................................................................
............................................................................................................................................
..................#..................................#..............#..................................................................#....
...............................#...........................#...................#.............................#..............................
....................................#....................................................#.............#...............#...................#
................................................................................................#...........................................
.#............#............#........................................................#......................................#................
.......#...................................#........#.......................................................................................
..........................................................#.................................................................................
..............................................................................#..............#.......#................................#.....
.................................#..........................................................................#.........#.....................
....................#....................................................................#......................................#...........
........#...................................#.....................................................................#.........................
#........................#.........................#.............................................#..........................................
.................#.........................................#.........................#......................................................
..............................#.................................#.......................................................................#...
......#...............................#..............................................................................#.............#........
......................................................#..................#.................#............#.......#...........................
..............#...........#......................................................#..........................................................
............................................................#...............................................................................
.....................................................................#......................................................................
.#..................#........................................................................#................................#.............
.........................................#..........................................................................#.......................
.........#.....#..............#..............................................#.........#..............#..............................#.....#
...............................................................................................................#............................
...................................................#...................#...................................................#................
.......................#...........#..............................#......................................#..............................#...
...........#................#...............................................................................................................
...........................................................................#.................................#..............................
................#......................................................................#...........#........................................
.....................................................................................................................#........#......#......
..................................................#.............#..............................#............................................
......#.....#...........#..................#..............................................................#.................................
............................................................................................................................................
...................#...................#..................#....................................................#........................#...
............................................................................................................................................
.........................................................................#..................#...............................................
.....................................................................................................................................#.....#
...................................#...........#....................#.......................................................................
..#.....#.....................................................................#.........................#...................................
...........................#.........................#..................................#..........#..................#.....................
..................................................................................#............................................#............
..............#................#...............................#............................................................................
............................................................................................................................................
.....................................#.........#......................................#..............#........#..........#..................
.....#............#...............................................#........................................................................#
..........................................................................................................#.................................
..........#.............#.....#...........................#.....................#...........................................................
...............................................................#.................................#....................................#.....
....................................#..................................#....................................................................
#......................................................................................................#..........#..........#..............
..........................................#.............................................#...................................................
.................#.............................#........#......................................#.........................#...............#..
......................................#..........................................#..........................................................
............#...............................................#...............................................................................
...........................................................................................#..........................................#.....
.....#................#...............................................................................#.....................................
............................#......................#................#.............................................................#.........
...................................#.........................................................................................#..............
.#.......................................................................#.........#........................#...............................
.........................................#.................#.....................................#...............#..........................
..............................#.............................................................................................................
.................#...............................#.............................#.......#.....#.......#......................................
.......................................................................#....................................................................
............................................................................................................................................
......................................#...........................#.........................................................................
.................................................................................#..........................................................
..................................#.......................#.................................................................................
......................#..............................................#...........................#..........................#.....#.....#...
...........................#............#................................................................#.....#............................
...........................................................................................#................................................
............................................................................................................................................
.........#.........#..........................................................#.............................#.........#.............#.......
...........................................#.............#..................................................................................
............................#..........................................................#..........................#.........................
.......................#.........#..........................................................................................................
......#...........................................#....................#.......................#..........#.................................
...............#.................................................................#..........................................#...............
..........................................................#......#....................................................#............#......#.
......................................................................................................#.....................................
...............................................................................................................#............................
...............................#.....#................#.............................#.......................................................
..#............................................................................#...............#..........................#.................
.......#...................................#........................................................#.......................................
..............................................................#........#.....................................#..............................
...........................#.......#..............#......................................................................................#..
......................#......................................................................................................#......#.......
..............#...........................................................................................#.............#...................
..........................................................#.........#.........................#.............................................
.......................................#.......#...............................#..............................#.............................
................................#...................................................#.......................................................
#..........................................#........#..........#............................................................................
...........................................................................#............#.........................#..................#......
............#...........................................................................................#...................................
...................................#........................................................#...............................................
........................#.................................#......#.......................................................#..................
........#................................................................#..................................................................
...................#..............................#.................................#..............#.........#.....#.............#..........
..........................................#...................#........................................................................#....
.........................................................................................................#..................................
..............................#.........................................................#...................................................
.#..............#...........................................................................................................................
...................................#...........#........................................................................#...................
.........#...............................#........................................................................................#.........
....................#........................................................................#.....................#.........#............#.
..............................................................................#.......................#.......#.............................
.................................#.......................#...............#...............#..................................................
............................................................................................................................................
............................#........#......................................................................................................
......................#........................................#................#...........#..............#........................#.......
.....#.......#........................................................................#..........#........................................#.
..................................................#.........................................................................................
...............................................................................................................#.........#..................
.........................#........#........................#.............#..........................#.......................................
..........#..............................#......................#..........................#................................................
#..................................................................................#........................................................
...............................#................................................................................................#...........
.....................................................#.......#.............................................#................................
......#...............#..............#.........#..................#...............................................#......#...............#..";
}