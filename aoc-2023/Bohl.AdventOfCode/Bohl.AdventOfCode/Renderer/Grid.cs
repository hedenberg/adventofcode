using Bohl.AdventOfCode.Renderer;

namespace Bohl.AdventOfCode.Renderer;

//internal class Grid
//{
//    private readonly char[,] grid;

//    public int Rows { get; }
//    public int Columns { get; }

//    public char this[int r, int c]
//    {
//        get => grid[r, c];
//        set => grid[r, c] = value;
//    }

//    public Grid(int rows, int columns)
//    {
//        Rows = rows;
//        Columns = columns;
//        grid = new char[rows, columns];
//    }
//}

//internal class Position
//{
//    public int Row { get; set; }
//    public int Column { get; set; }

//    public Position(int row, int column)
//    {
//        Row = row;
//        Column = column;
//    }
//}

//internal abstract class Item
//{
//    protected abstract char Id { get; }
//    protected abstract Position[][] Tiles { get; }
//    protected abstract Position Offset { get; }
//    protected int rotationState;

//    internal Item()
//    {
//    }

//    public IEnumerable<Position> TilePositions()
//    {
//        foreach (var position in Tiles[rotationState])
//        {
//            yield return new Position(position.Row + Offset.Row, position.Column + Offset.Column);
//        }
//    }
//}

//internal class PipeItem : Item
//{
//    public PipeItem(char id, int startOffsetRow, int startOffsetColumn)
//    {
//        Id = id;
//        Offset = new Position(startOffsetRow, startOffsetColumn);
//        rotationState = id switch
//        {
//            '|' => 0,
//            '-' => 1,
//            _ => rotationState
//        };
//    }

//    protected override char Id { get; }
//    protected override Position Offset { get; }

//    /*
//     * . x .
//     * . x .
//     * . x .
//     *
//     * . . .
//     * x x x
//     * . . .
//     */
//    protected override Position[][] Tiles { get; } = {
//        new Position[] { new(0, 1), new(1, 1), new(2, 1) },
//        new Position[] { new(1, 0), new(1, 1), new(1, 2) },
//    };
//}

//internal class BendItem : Item
//{
//    public BendItem(char id, int startOffsetRow, int startOffsetColumn)
//    {
//        Id = id;
//        Offset = new Position(startOffsetRow, startOffsetColumn);
//        rotationState = id switch
//        {
//            'L' => 0,
//            'J' => 1,
//            '7' => 2,
//            'F' => 3,
//            _ => rotationState
//        };
//    }

//    protected override char Id { get; }
//    protected override Position Offset { get; }
//    /*
//     * . x .
//     * . x x
//     * . . .
//     *
//     * . x .
//     * x x .
//     * . . .
//     *
//     * . . .
//     * x x .
//     * . x .
//     *
//     * . . .
//     * . x x
//     * . x .
//     */
//    protected override Position[][] Tiles { get; } = {
//        new Position[] { new(0, 1), new(1, 1), new(1, 2) },
//        new Position[] { new(0, 1), new(1, 0), new(1, 1) },
//        new Position[] { new(1, 0), new(1, 1), new(2, 1) },
//        new Position[] { new(1, 1), new(1, 2), new(2, 1) }
//    };
//}