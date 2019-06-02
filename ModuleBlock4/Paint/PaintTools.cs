using System.Drawing;
using Stack;

namespace Paint
{
    internal class PaintTools
    {
        public static void FloodFill(Bitmap b, Point p, Color newColor)
        {
            var oldColor = b.GetPixel(p.X, p.Y);
            var stack = new StackArrayList<Point>();
            stack.Push(p);
            while (stack.Count != 0)
            {
                p = stack.Pop();
                if (b.GetPixel(p.X, p.Y).ToArgb() != oldColor.ToArgb()) continue;
                b.SetPixel(p.X, p.Y, newColor);

                stack.Push(new Point(p.X, p.Y + 1));
                stack.Push(new Point(p.X + 1, p.Y));
                stack.Push(new Point(p.X, p.Y - 1));
                stack.Push(new Point(p.X - 1, p.Y));

                stack.Push(new Point(p.X + 1, p.Y + 1));
                stack.Push(new Point(p.X - 1, p.Y + 1));
                stack.Push(new Point(p.X + 1, p.Y - 1));
                stack.Push(new Point(p.X - 1, p.Y - 1));
            }
        }
        public static void FloodFillRecursive(Bitmap b, Point p, Color newColor, Color oldColor)
        {
            if (b.GetPixel(p.X, p.Y).ToArgb() != oldColor.ToArgb())
                return;

            b.SetPixel(p.X, p.Y, newColor);
            FloodFillRecursive(b, new Point(p.X, p.Y + 1), newColor, oldColor);
            FloodFillRecursive(b, new Point(p.X + 1, p.Y), newColor, oldColor);
            FloodFillRecursive(b, new Point(p.X, p.Y - 1), newColor, oldColor);
            FloodFillRecursive(b, new Point(p.X - 1, p.Y), newColor, oldColor);
        }
    }
}
