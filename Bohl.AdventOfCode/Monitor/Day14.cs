using Bohl.AdventOfCode.Day14;
using Bohl.AdventOfCode;
using Bohl.AdventOfCode.Input;

namespace Monitor
{
    public partial class Day14 : Form
    {
        public Day14()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var map = Inputs.Day14Challenge.Parse<Map>();
            map.EnablePartTwo();

            //var monitor = new Monitor(map.Pixels);
            //monitor.RenderFull();

            var simulator = new SandSimulator
            {
                //Monitor = monitor
            };

            var s = 3;

            var g = e.Graphics;
            var sandBrush = new SolidBrush(Color.Orange);
            var sand = new Pen(sandBrush);
            var rockBrush = new SolidBrush(Color.Green);
            var rock = new Pen(rockBrush);
            var airBrush = new SolidBrush(Color.CornflowerBlue);
            var air = new Pen(airBrush);

            for (var y = 0; y < map.Pixels.GetLength(1); y++)
            {
                for (var x = 0; x < map.Pixels.GetLength(0); x++)
                {
                    var mapPixel = map.Pixels[x, y];
                    if (mapPixel is Block.Sand)
                    {
                        g.FillRectangle(sandBrush, x * s, y * s, s, s);
                    }
                    else if (mapPixel is Block.Rock)
                    {
                        g.FillRectangle(rockBrush, x * s, y * s, s, s);
                    }
                    else if (mapPixel is Block.Air)
                    {
                        g.FillRectangle(airBrush, x * s, y * s, s, s);
                    }
                }
            }

            simulator.SpawnSand(map);
            while (true)
            {
                var atTop = simulator.UpdateSand(map);
                if (atTop)
                {
                    break;
                }

                var x1 = simulator.SandXCurrent;
                var y1 = simulator.SandYCurrent;
                var p1 = map.Pixels[x1, y1];
                if (p1 is Block.Sand)
                {
                    g.FillRectangle(sandBrush, x1 * s, y1 * s, s, s);
                }
                else if (p1 is Block.Rock)
                {
                    g.FillRectangle(rockBrush, x1 * s, y1 * s, s, s);
                }
                else if (p1 is Block.Air)
                {
                    g.FillRectangle(airBrush, x1 * s, y1 * s, s, s);
                }

                var x2 = simulator.SandXPrevious ?? 0;
                var y2 = simulator.SandYPrevious ?? 0;
                var p2 = map.Pixels[x2, y2];
                if (p2 is Block.Sand)
                {
                    g.FillRectangle(sandBrush, x2 * s, y2 * s, s, s);
                }
                else if (p2 is Block.Rock)
                {
                    g.FillRectangle(rockBrush, x2 * s, y2, s, s);
                }
                else if (p2 is Block.Air)
                {
                    g.FillRectangle(airBrush, x2 * s, y2 * s, s, s);
                }
            }
        }
    }
}