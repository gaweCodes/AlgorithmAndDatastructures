using System;
using System.Windows.Forms;

namespace PostScriptViewer
{
    public partial class ViewerForm : Form
    {
        private Viewer viewer;
        public ViewerForm()
        {
            InitializeComponent();
        }

        private void Open_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog()
            {
                Filter = "PostScript (*.ps)|*.ps|Alle Dateien (*.*)|*.*",
                RestoreDirectory = false
            };

            if (dlg.ShowDialog() == DialogResult.OK) viewer = new Viewer(dlg.FileName);
            Invalidate();
        }
        private void ViewerForm_Paint(object sender, PaintEventArgs e)
        {
            viewer?.Paint(e.Graphics);
        }
    }
}
