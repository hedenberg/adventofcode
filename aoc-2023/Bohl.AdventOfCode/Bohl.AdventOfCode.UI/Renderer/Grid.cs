using System.Windows.Media;

namespace Bohl.AdventOfCode.UI.Renderer;

internal class Grid<T>
{
    private readonly T[,] grid;

    public int Rows { get; }
    public int Columns { get; }

    public T this[int r, int c]
    {
        get => grid[r, c];
        set => grid[r, c] = value;
    }

    public Grid(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        grid = new T[rows, columns];
    }
}

internal class ItemGrid<T> : Grid<Item<T>>
{
    public int ItemRows { get; }
    public int ItemColumns { get; }
    public ItemGrid(int rows, int columns, int itemRows, int itemColumns) : base(rows, columns)
    {
        ItemRows = itemRows;
        ItemColumns = itemColumns;
    }
}

internal class MetalGrid : ItemGrid<char>
{
    public static string Example1 = @".....
.F-7.
.|.|.
.L-J.
.....";

    public static string Example2 = @".....
.S-7.
.|.|.
.L-J.
.....";

    public static string Example3 = @"..F7.
.FJ|.
SJ.L7
|F--J
LJ...";

    public MetalGrid(char[][] map) : base(map.Length, map.Length, 3, 3)
    {
        for (int r = 0; r < map.Length; r++)
        {
            var row = map[r];
            for (int c = 0; c < row.Length; c++)
            {
                var item = row[c];
                this[r, c] = MetalItem.Parse(item);
            }
        }
    }
}

internal class Item<T> : Grid<Color>
{
    public T Type { get; set; }
    public Item(T type, int rows, int columns) : base(rows, columns)
    {
        Type = type;
    }
}

internal class MetalItem : Item<char>
{
    public MetalItem(char type) : base(type, 3, 3)
    {
        this[0, 0] = Ground;
        this[0, 1] = Ground;
        this[0, 2] = Ground;

        this[1, 0] = Ground;
        this[1, 1] = Ground;
        this[1, 2] = Ground;

        this[2, 0] = Ground;
        this[2, 1] = Ground;
        this[2, 2] = Ground;
    }

    public static MetalItem Parse(char value)
    {
        switch (value)
        {
            case '|':
                return new VerticalPipe();
            case '-':
                return new HorizontalPipe();
            case 'L':
                return new BendLPipe();
            case 'J':
                return new BendJPipe();
            case '7':
                return new Bend7Pipe();
            case 'F':
                return new BendFPipe();
            case 'S':
                return new Start();
            default:
                return new Ground();
        }
    }

    public static Color Ground = Colors.Black;
    public static Color Metal = Colors.Silver;
    public static Color Animal = Colors.Red;
}

internal class VerticalPipe : MetalItem
{
    public VerticalPipe() : base('|')
    {
        this[0, 1] = Metal;
        this[1, 1] = Metal;
        this[2, 1] = Metal;
    }
}

internal class HorizontalPipe : MetalItem
{
    public HorizontalPipe() : base('-')
    {
        this[1, 0] = Metal;
        this[1, 1] = Metal;
        this[1, 2] = Metal;
    }
}

internal class BendLPipe : MetalItem
{
    public BendLPipe() : base('L')
    {
        this[0, 0] = Ground;
        this[0, 1] = Metal;
        this[0, 2] = Ground;

        this[1, 0] = Ground;
        this[1, 1] = Metal;
        this[1, 2] = Metal;

        this[2, 0] = Ground;
        this[2, 1] = Ground;
        this[2, 2] = Ground;
    }
}

internal class BendJPipe : MetalItem
{
    public BendJPipe() : base('J')
    {
        this[0, 0] = Ground;
        this[0, 1] = Metal;
        this[0, 2] = Ground;

        this[1, 0] = Metal;
        this[1, 1] = Metal;
        this[1, 2] = Ground;

        this[2, 0] = Ground;
        this[2, 1] = Ground;
        this[2, 2] = Ground;
    }
}

internal class Bend7Pipe : MetalItem
{
    public Bend7Pipe() : base('7')
    {
        this[0, 0] = Ground;
        this[0, 1] = Ground;
        this[0, 2] = Ground;

        this[1, 0] = Metal;
        this[1, 1] = Metal;
        this[1, 2] = Ground;

        this[2, 0] = Ground;
        this[2, 1] = Metal;
        this[2, 2] = Ground;
    }
}

internal class BendFPipe : MetalItem
{
    public BendFPipe() : base('F')
    {
        this[0, 0] = Ground;
        this[0, 1] = Ground;
        this[0, 2] = Ground;

        this[1, 0] = Ground;
        this[1, 1] = Metal;
        this[1, 2] = Metal;

        this[2, 0] = Ground;
        this[2, 1] = Metal;
        this[2, 2] = Ground;
    }
}

internal class Start : MetalItem
{
    public Start() : base('S')
    {
        this[0, 0] = Ground;
        this[0, 1] = Ground;
        this[0, 2] = Ground;

        this[1, 0] = Ground;
        this[1, 1] = Animal;
        this[1, 2] = Ground;

        this[2, 0] = Ground;
        this[2, 1] = Ground;
        this[2, 2] = Ground;
    }
}

internal class Ground : MetalItem
{
    public Ground() : base('.')
    {
    }
}

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