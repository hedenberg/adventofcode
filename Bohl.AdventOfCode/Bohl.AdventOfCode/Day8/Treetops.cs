using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCode.Day8;

public class Treetops : IParsable<Treetops>
{
    public required int[][] Trees { get; set; }

    public int VisibleTrees()
    {
        var visible = 0;
        // All outer trees are visible
        visible += Trees.Length * 2;
        visible += (Trees[0].Length - 2) * 2;

        for (var y = 1; y < Trees.Length - 1; y++)
        {
            for (var x = 1; x < Trees[y].Length - 1; x++)
            {
                if (IsVisible(x, y))
                {
                    visible++;
                }
            }
        }


        return visible;
    }

    private bool IsVisible(int x, int y)
    {
        var tree = Trees[y][x];
        var leftSide = Trees[y][..x];
        var rightSide = Trees[y][(x+1)..];
        var upperSide = Trees[..y].Select(row => row[x]).ToArray();
        var lowerSide = Trees[(y+1)..].Select(row => row[x]).ToArray();

        var result = leftSide.All(t => t < tree) || 
               rightSide.All(t => t < tree) || 
               upperSide.All(t => t < tree) ||
               lowerSide.All(t => t < tree);
        return result;
    }

    static Treetops IParsable<Treetops>.Parse(string input, IFormatProvider? provider)
    {
        var treeRows = input.Rows();
        var trees = treeRows.Select(ts => ts.ToCharArray().Select(t => int.Parse(t.ToString())).ToArray()).ToArray();

        return new Treetops
        {
            Trees = trees,
        };
    }

    static bool IParsable<Treetops>.TryParse(string? s, IFormatProvider? provider, out Treetops result)
    {
        throw new NotImplementedException();
    }
}