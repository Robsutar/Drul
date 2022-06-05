using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drull.WindowComponents
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Window.Construct();
        }

        public void Repaint()
        {
            Invalidate();
            Validate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Window.render(e.Graphics);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            Window.keyDown(e);
        }
    }
}
