using Bohl.AdventOfCode.Renderer;

namespace Bohl.AdventOfCode.Day10.Renderer;

//internal class PipeGrid
//{
//    public static readonly string ExampleOne = @".....
//.S-7.
//.|.|.
//.L-J.
//.....";

//    public static readonly string ExampleTwo = @"-L|F7
//7S-7|
//L|7||
//-L-J|
//L|-JF";

//    public static Grid GenerateGrid(string input)
//    {
//        var rows = input.Rows();
//        var charGrid = new List<char[]>();
//        foreach (var row in rows)
//        {
//            var chars = row.ToCharArray();
//            charGrid.Add(chars);
//        }

//        var char2d = charGrid.ToArray();

//        var gridRows = char2d.Length * 3;
//        var gridColumns = char2d[0].Length * 3;

//        var grid = new Grid(gridRows, gridColumns);

//        //for (int r = 0; r < char2d.Length; r++)
//        //{
//        //    var row = char2d[r];
//        //    for (int c = 0; c < row.Length; c++)
//        //    {
//        //        var character = row[c];

//        //        for (int gr = r*3; gr < r*3 + 3; gr++)
//        //        {
//        //            for (int gc = c*3; gc < c*3 + 3; gc++)
//        //            {
//        //                if (character == '.')
//        //                {
//        //                    grid[gr, gc] = 'E';
//        //                }

//        //                if (character == '|')
//        //                {
//        //                    grid[gr, gc] = 'P';
//        //                }
//        //            }
//        //        }
//        //    }
//        //}

//        return grid;
//    }
//}