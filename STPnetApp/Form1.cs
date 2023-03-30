using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using STPnet;

namespace STPnetApp
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        public Net net;

        private void FormMain_Load(object sender, EventArgs e)
        {
            net = new Net();
        }

        private void addBridgeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            //g.DrawLine(new Pen(Color.Black, 1), new System.Drawing.Point(200, 200), new System.Drawing.Point(200, 300));
        }
    }
}
