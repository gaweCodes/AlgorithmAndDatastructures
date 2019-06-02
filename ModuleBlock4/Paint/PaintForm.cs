using System;
using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
    public partial class PaintForm : Form
    {
        private ContextMenu menu;
        private string color = "Black";
        private Pen pen;
        private bool mouseDown = false;
        private Graphics graphicsBitmap;
        private Point point;
        private Bitmap cacheBitmap;
        public PaintForm()
        {
            InitializeComponent();
        }
        private void PaintForm_Load(object sender, System.EventArgs e)
        {
            menu = new ContextMenu();
            var h = new EventHandler(OnMenuClick);
            menu.MenuItems.AddRange(new MenuItem[]
            {
                new MenuItem(Color.Black.Name, h),
                new MenuItem(Color.Red.Name, h),
                new MenuItem(Color.Green.Name, h),
                new MenuItem(Color.Blue.Name, h),
                new MenuItem(Color.Yellow.Name, h)
            });
            pen = new Pen(Color.FromName(color), 5);
            cacheBitmap = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            graphicsBitmap = Graphics.FromImage(cacheBitmap);
            graphicsBitmap.DrawRectangle(new Pen(Color.FromName("gray"), 1), 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);

        }
        protected void OnMenuClick(object sender, EventArgs e)
        {
            color = ((MenuItem)sender).Text;
            pen = new Pen(Color.FromName(color), 5);
        }
        private void PaintForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                menu.Show(this, e.Location);
            if (e.Button != MouseButtons.Left)
                return;
            mouseDown = true;
            point = e.Location;
        }
        private void PaintForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown == false)
                return;
            using (var g = CreateGraphics())
                g.DrawLine(pen, point, e.Location);
            graphicsBitmap.DrawLine(pen, point, e.Location);
            point = e.Location;
        }
        private void PaintForm_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        private void PaintForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(cacheBitmap, new Point(0, 0));
        }

        private void PaintForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var c = Cursor;
            Cursor = Cursors.WaitCursor;
            PaintTools.FloodFill(cacheBitmap, e.Location, Color.FromName(color));
            //PaintTools.FloodFillRecursive(cacheBitmap, e.Location, Color.FromName(color), cacheBitmap.GetPixel(e.Location.X, e.Location.Y));
            Invalidate();
            Cursor = c;
        }
    }
}
