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
using System.Diagnostics;


namespace STPnetApp
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        public Net net;
        public NetView nw;

        private void FormMain_Load(object sender, EventArgs e)
        {
            net = new Net();
            nw = new NetView();
        }

        private void addBridgeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            nw.Print(g,net);
            //g.DrawLine(new Pen(Color.Black, 1), new System.Drawing.Point(200, 200), new System.Drawing.Point(200, 300));
        }

        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            
            
        }

        private void FormMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                nw.Find(e.X, e.Y, out int type, out int id, out int idb);
                Debug.WriteLine(type.ToString());
                if (type == 0) return;
                if (type == 1)
                {
                    nw.EditPosBridge(id, e.X, e.Y);
                }
                else if (type == 2)
                {
                    nw.EditPosPort(idb, id, e.X, e.Y);
                }
                else if (type == 3)
                {
                    nw.EditPosLink(id, e.X, e.Y);
                }
                Refresh();
            }
        }
    }
}
