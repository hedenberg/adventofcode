using Bohl.AdventOfCode.Input;
using Microsoft.Toolkit.HighPerformance;

namespace Bohl.AdventOfCode.Day08;

public class Treetops : IParsable<Treetops>
{
    public required int[,] Trees { get; set; }

    public static Treetops Parse(string input, IFormatProvider? provider)
    {
        var treeRows = input.Rows();

        var height = treeRows.Length;
        var width = treeRows.GroupBy(row => row.Length).Single().Key;

        var trees = new int[height, width];

        for (var i = 0; i < height; i++)
        for (var j = 0; j < width; j++)
            trees[i, j] = int.Parse(treeRows[i][j].ToString());

        return new Treetops
        {
            Trees = trees
        };
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out Treetops result)
    {
        throw new NotImplementedException();
    }

    public int TopScenicScore()
    {
        var top = 0;

        // All outer trees are visible;
        var height = Trees.GetLength(0);
        var width = Trees.GetLength(1);

        for (var y = 1; y < height - 1; y++)
        for (var x = 1; x < width - 1; x++)
        {
            var score = ScenicScore(x, y);
            if (score > top)
                top = score;
        }

        return top;
    }

    public int ScenicScore(int x, int y)
    {
        var left = ViewingDistance(x, y, -1, 0);
        var right = ViewingDistance(x, y, 1, 0);
        var top = ViewingDistance(x, y, 0, -1);
        var bottom = ViewingDistance(x, y, 0, 1);

        return left * right * top * bottom;
    }

    public int ViewingDistance(int x, int y, int vx, int vy)
    {
        var tree = Trees[y, x];

        var trees = TreesInDirection(x, y, vx, vy);

        var score = 0;
        foreach (var height in trees)
        {
            if (height >= tree)
            {
                score++;
                break;
            }

            score++;
        }

        return score;
    }

    public int[] TreesInDirection(int x, int y, int vx, int vy)
    {
        return (vx, vy) switch
        {
            (-1, 0) =>
                // Left
                Trees.GetRow(y).ToArray()[..x].Reverse().ToArray(),
            (1, 0) =>
                // Right
                Trees.GetRow(y).ToArray()[(x + 1)..],
            (0, -1) =>
                // Top
                Trees.GetColumn(x).ToArray()[..y].Reverse().ToArray(),
            (0, 1) =>
                // Bottom
                Trees.GetColumn(x).ToArray()[(y + 1)..],
            _ => throw new ArgumentException("Vector not supported")
        };
    }

    public int VisibleTrees()
    {
        var visible = 0;

        // All outer trees are visible;
        var height = Trees.GetLength(0);
        var width = Trees.GetLength(1);
        visible += width * 2 + (height - 2) * 2;

        for (var y = 1; y < height - 1; y++)
        for (var x = 1; x < width - 1; x++)
            if (IsVisible(x, y))
                visible++;

        return visible;
    }

    public bool IsVisible(int x, int y)
    {
        var tree = Trees[y, x];
        var row = Trees.GetRow(y).ToArray();
        var left = row[..x];
        var right = row[(x + 1)..];
        var column = Trees.GetColumn(x).ToArray();
        var top = column[..y];
        var bottom = column[(y + 1)..];

        var result = left.All(t => t < tree) ||
                     right.All(t => t < tree) ||
                     top.All(t => t < tree) ||
                     bottom.All(t => t < tree);
        return result;
    }
}