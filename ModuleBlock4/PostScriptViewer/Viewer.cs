using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Stack;
namespace PostScriptViewer
{
    internal class Viewer
    {
        private readonly ArrayList<string[]> _commands = new ArrayList<string[]>();
        private Pen p = new Pen(Color.Black);
        private int _x1 = 1, _y1 = 1;
        public Viewer(string path)
        {
            using (var r = new StreamReader(path))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    var tokens = line.Split( separator: new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (tokens.Length > 0)
                        _commands.Add(tokens);
                }
            }
        }
        public void Paint(Graphics g)
        {
            Transform(g);

            foreach (var command in _commands)
            {
                var stack = new StackArrayList<string>();
                foreach (var param in command) stack.Push(param);

                switch (stack.Pop().ToLower())
                {
                    case "setlinewidth":
                        p.Width = int.Parse(stack.Pop());
                        break;

                    case "setrgbcolor":
                        var ci = CultureInfo.InvariantCulture;
                        var blue = (int)(double.Parse(stack.Pop(), ci) * 255);
                        var green = (int)(double.Parse(stack.Pop(), ci) * 255);
                        var red = (int)(double.Parse(stack.Pop(), ci) * 255);
                        p.Brush = new SolidBrush(Color.FromArgb(red, green, blue));
                        break;
                    case "arc":
                        var sweepAngle = float.Parse(stack.Pop());
                        var startAngle = float.Parse(stack.Pop());
                        var radius = float.Parse(stack.Pop());
                        var y = float.Parse(stack.Pop());
                        var x = float.Parse(stack.Pop());
                        g.DrawArc(p, x - radius, y - radius, radius * 2, radius * 2, startAngle, sweepAngle);
                        break;
                    case "moveto":
                        _y1 = int.Parse(stack.Pop());
                        _x1 = int.Parse(stack.Pop());
                        break;
                    case "lineto":
                        var y2 = int.Parse(stack.Pop());
                        var x2 = int.Parse(stack.Pop());
                        g.DrawLine(p, _x1, _y1, x2, y2);
                        _x1 = x2;
                        _y1 = y2;
                        break;
                }
            }
        }
        private void Transform(Graphics g)
        {
            var matrix = new Matrix(1, 0, 0, -1, 0, 0);
            matrix.Translate(0, -Screen.PrimaryScreen.WorkingArea.Height);
            g.Transform = matrix;
        }
    }
}
