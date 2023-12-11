using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Bohl.AdventOfCode.UI.Renderer;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace Bohl.AdventOfCode.UI;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly Rectangle[,] rectangles;
    private readonly MetalGrid grid;

    //private readonly Color[] colors = { Color, Color.Purple };

    public MainWindow()
    {
        InitializeComponent();

        var input = MetalGrid.Example3.Rows();

        var map = new List<char[]>();

        foreach (var row in input)
        {
            map.Add(row.ToCharArray());
        }

        grid = new MetalGrid(map.ToArray());

        rectangles = SetupCanvas(grid);
    }

    private Rectangle[,] SetupCanvas<T>(ItemGrid<T> grid)
    {
        Rectangle[,] rectangles = new Rectangle[grid.Rows*grid.ItemRows, grid.Columns*grid.ItemColumns];

        var cellSize = ItemCanvas.Width / (grid.Columns * grid.ItemColumns);

        for (int r = 0; r < grid.Rows; r++)
        {
            for (int c = 0; c < grid.Columns; c++)
            {
                for (int ir = 0; ir < grid.ItemRows; ir++)
                {
                    for (int ic = 0; ic < grid.ItemColumns; ic++)
                    {
                        var rectangleRow = r * grid.ItemRows + ir;
                        var rectangleColumn = c * grid.ItemColumns + ic;
                        Rectangle rectangle = new Rectangle
                        {
                            Width = cellSize,
                            Height = cellSize,
                        };

                        Canvas.SetTop(rectangle, rectangleRow * cellSize);
                        Canvas.SetLeft(rectangle, rectangleColumn * cellSize);

                        ItemCanvas.Children.Add(rectangle);
                        rectangles[rectangleRow, rectangleColumn] = rectangle;
                    }
                }
            }
        }

        return rectangles;
    }

    private void DrawGrid<T>(ItemGrid<T> grid)
    {
        for (int r = 0; r < grid.Rows; r++)
        {
            for (int c = 0; c < grid.Columns; c++)
            {
                var item = grid[r, c];

                for (int ir = 0; ir < grid.ItemRows; ir++)
                {
                    for (int ic = 0; ic < grid.ItemColumns; ic++)
                    {
                        var rectangleRow = r * grid.ItemRows + ir;
                        var rectangleColumn = c * grid.ItemColumns + ic;
                        if (item != null)
                        {
                            var itemColor = item[ir, ic];
                            rectangles[rectangleRow, rectangleColumn].Fill = new SolidColorBrush(item[ir, ic]);
                        }
                        else
                        {
                            rectangles[rectangleRow, rectangleColumn].Fill = new SolidColorBrush(Colors.Black);
                        }
                    }
                }
            }
        }
    }

    private void ItemCanvas_Loaded(object sender, RoutedEventArgs e)
    {
        DrawGrid(grid);
    }
}