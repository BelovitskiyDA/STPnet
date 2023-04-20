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
using System.IO;

namespace STPnetApp
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            myDelegate = new EnableMenuDelegate(EnableMenuMetod);
        }

        public NetApp netApp;

        public delegate void EnableMenuDelegate();
        public EnableMenuDelegate myDelegate;
        int typeObjectMove, idObjectMove, idBridgeMove, idPortMove;
        int typeObjectChoose1, idObjectChoose1, idBridgeChoose1, idPortChoose1;
        int typeObjectChoose2, idObjectChoose2, idBridgeChoose2, idPortChoose2;
        bool flagMove = false;
        System.Drawing.Point prevPosition;
        float scale = 1;
        string filenamePath = null;
        string Filename {
            get { return Path.GetFileName(filenamePath); }
        }
        

        private void editLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int weight = netApp.net.links[idObjectChoose1].weight;
            LinkDialog ld = new LinkDialog(weight);
            ld.ShowDialog();
            if (ld.DialogResult == DialogResult.OK)
            {
                weight = ld.weight;
                if (weight != 0)
                {
                    netApp.net.EditLink(idObjectChoose1, weight);
                }
            }
            ClearStrip();
            Refresh();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            netApp.net.DisconnectLink(idObjectChoose1, idBridgeChoose1, idPortChoose1);

            ClearStrip();
            Refresh();
        }

        private void editBridgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idBridge = idObjectChoose1;
            string priority = netApp.net.bridges[idBridge].priority.ToString("X");
            BridgeDialog bd = new BridgeDialog(idBridge, priority);
            bd.ShowDialog();
            if (bd.DialogResult == DialogResult.OK)
            {
                priority = bd.priority;
                if (String.IsNullOrWhiteSpace(priority))
                {
                    ClearStrip();
                    return;
                }
                netApp.net.EditBridge(idBridge, priority);
                ClearStrip();
                Refresh();
            }
        }

        private void deleteBridgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            netApp.net.DeleteBridge(idObjectChoose1);

            ClearStrip();
            Refresh();
        }

        private void addPortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int number = 0;
            PortDialog pd = new PortDialog();
            pd.ShowDialog();
            if (pd.DialogResult == DialogResult.OK)
            {
                number = pd.number;
                if (number != 0)
                {
                    netApp.net.AddPort(idObjectChoose1, number);
                }
            }
            ClearStrip();
            Refresh();
        }

        private void deletePortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            netApp.net.DeletePort(idBridgeChoose1, idObjectChoose1);

            ClearStrip();
            Refresh();
        }

        private void rootBridgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            netApp.net.Reset();
            netApp.net.RootBridge();
            Refresh();
        }


        

        public void EnableMenuMetod()
        {
            for (int i = 0; i < 4; i++)
            {
                menuStrip1.Items[i].Enabled = true;
            }
        }

        async void EnableMenu(FormMain form)
        {
            while (!netApp.net.isCompleted)
            {
                await Task.Delay(100);
            }

            form.Invoke(form.myDelegate);
            
        }

        void DisableMenu()
        {
            for (int i = 0; i < 4; i++)
            {
                menuStrip1.Items[i].Enabled = false;
            }
        }

        private void rootPortsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            netApp.net.Reset();
            netApp.net.RootBridge();
            if (stepByStepToolStripMenuItem.Checked)//checkBoxModeling.Checked
            {
                netApp.net.RootPorts(1);
                DisableMenu();
                var t = new Task(() => EnableMenu(this));
                t.Start();
            }
            else
            {
                netApp.net.RootPorts(0);
                netApp.net.waitComplete();
            }

            Refresh();
        }

        private void desPortsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            netApp.net.Reset();
            netApp.net.RootBridge();
            netApp.net.RootPorts(0);
            netApp.net.waitComplete();
            if (stepByStepToolStripMenuItem.Checked)//checkBoxModeling.Checked
            {
                netApp.net.NonRootPorts(1);
                DisableMenu();
                var t = new Task(() => EnableMenu(this));
                t.Start();
            }
            else
            {
                netApp.net.NonRootPorts(0);
                netApp.net.waitComplete();

            }

            Refresh();
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            netApp.net.Reset();
            Refresh();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            netApp.net = new Net();
            netApp.nw = new NetView();
            Refresh();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filenamePath == null)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                filenamePath = saveFileDialog1.FileName;
            }

            try
            {
                netApp.Save(filenamePath);
            }
            catch
            {
                string message = "Ошибка сохранения файла";
                string caption = "Упс";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
                return;
            }
            Text = $"STPnet: {Filename}";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            filenamePath = openFileDialog1.FileName;

            try
            {
                netApp = NetApp.Load(filenamePath);
            }
            catch
            {
                string message = "Ошибка загрузки файла";
                string caption = "Упс";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
                netApp = new NetApp();
            }

            Text = $"STPnet: {Filename}";
            Refresh();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            netApp = new NetApp();
            filenamePath = null;
            Text = $"STPnet";
            scale = 1;
            ClearStrip();
            Refresh();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            filenamePath = saveFileDialog1.FileName;

            try
            {
                netApp.Save(filenamePath);
            }
            catch
            {
                string message = "Ошибка сохранения файла";
                string caption = "Упс";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
                return;
            }
            Text = $"STPnet: {Filename}";
        }

        private void stepByStepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //modeling = true;
            if (!netApp.net.isCompleted) return;
            if (stepByStepToolStripMenuItem.Checked) stepByStepToolStripMenuItem.Checked = false;
            else stepByStepToolStripMenuItem.Checked = true;
            //(ToolStripMenuItem)(menuStrip1.Items["stepByStepToolStripMenuItem"]).DropDownItems

        }

        private void completeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            netApp.net.CompleteModeling();
            /*if (netApp.net.isCompleted)
            {
                for (int i = 0; i < 4; i++)
                {
                    menuStrip1.Items[i].Enabled = true;
                }
            }*/
            Refresh();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            netApp.net.CompleteModeling();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            netApp = new NetApp();
        }

        private void nextStepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            netApp.net.NextStep();
            /*if (netApp.net.isCompleted)
            {
                for (int i = 0; i < 4; i++)
                {
                    menuStrip1.Items[i].Enabled = true;
                }
            }*/
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.ScaleTransform(scale, scale);
            netApp.nw.ScaleTransform(scale);
            netApp.nw.Print(g, netApp.net);
        }

        public void ClearStrip()
        {
            typeObjectChoose1 = 0;
            idObjectChoose1 = 0;
            idBridgeChoose1 = 0;
            idPortChoose1 = 0;
            toolStripStatusLabel1.Text = "Element";
        }
        private void FormMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (!netApp.net.isCompleted) return;
            if (flagMove)
            {
                ClearStrip();
                flagMove = false;
                return;
            }
            netApp.nw.Find((int)(e.X * 1/scale), (int)(e.Y * 1/scale), out int type, out int id, out int idb, out int idp);

            if (e.Button == MouseButtons.Left)
            {
                if (type == 0)
                {
                    ClearStrip();
                }
                else
                {
                    if (typeObjectChoose1 == 0 || typeObjectChoose1 == 1)
                    {
                        typeObjectChoose1 = type;
                        idObjectChoose1 = id;
                        idBridgeChoose1 = idb;
                        idPortChoose1 = idp;
                        string name = "";
                        if (type == 1) name = $"Bridge (id: {id})";
                        else if (type == 2) name = $"Port (id: {id}, idBridge: {idb})";
                        else if (type == 3) name = $"Link (id: {id}, idBridge: {idb}, idPort: {idp})";
                        toolStripStatusLabel1.Text = $"Element: {name}";
                    }
                    else
                    {
                        typeObjectChoose2 = type;
                        idObjectChoose2 = id;
                        idBridgeChoose2 = idb;
                        idPortChoose2 = idp;

                        if (typeObjectChoose1 == 2 && typeObjectChoose2 == 2) //addLink
                        {
                            if (!netApp.net.PortIsEmpty(idBridgeChoose1, idObjectChoose1)
                                || !netApp.net.PortIsEmpty(idBridgeChoose2, idObjectChoose2)
                                || idBridgeChoose1 == idBridgeChoose2)
                            {
                                ClearStrip();
                                return;
                            }
                            int weight = 0;
                            LinkDialog ld = new LinkDialog(weight);
                            ld.ShowDialog();
                            if (ld.DialogResult == DialogResult.OK)
                            {
                                weight = ld.weight;
                                if (weight != 0)
                                {
                                    netApp.net.AddLink(idBridgeChoose1, idObjectChoose1, idBridgeChoose2, idObjectChoose2, weight);
                                }
                            }
                            ClearStrip();
                        }
                        else if (typeObjectChoose1 == 2 && typeObjectChoose2 == 3)
                        {
                            if (netApp.net.PortIsEmpty(idBridgeChoose1, idObjectChoose1))
                                if (MessageBox.Show("Добавить связь?", "AddConnection", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    netApp.net.AddConnect(idObjectChoose2, idBridgeChoose1, idObjectChoose1);
                                    myPoint point = netApp.nw.bridges[idBridgeChoose1].ports[idObjectChoose1];
                                    netApp.nw.AddConnectionLink(idObjectChoose2, idBridgeChoose1, point);
                                }
                        }
                        else if (typeObjectChoose1 == 3 && typeObjectChoose2 == 2)
                        {
                            if (netApp.net.PortIsEmpty(idBridgeChoose2, idObjectChoose2))
                                if (MessageBox.Show("Добавить связь?", "AddConnection", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    netApp.net.AddConnect(idObjectChoose1, idBridgeChoose2, idObjectChoose2);
                                    myPoint point = netApp.nw.bridges[idBridgeChoose2].ports[idObjectChoose2];
                                    netApp.nw.AddConnectionLink(idObjectChoose1, idBridgeChoose2, point);
                                }
                                    
                        }
                        else
                        {
                            typeObjectChoose1 = type;
                            idObjectChoose1 = id;
                            idBridgeChoose1 = idb;
                            idPortChoose1 = idp;
                            string name = "";
                            if (type == 1) name = $"Bridge (id: {id})";
                            else if (type == 2) name = $"Port (id: {id}, idBridge: {idb})";
                            else if (type == 3) name = $"Link (id: {id}, idBridge: {idb}, idPort: {idp})";
                            toolStripStatusLabel1.Text = $"Element: {name}";
                        }

                        Refresh();
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                typeObjectChoose1 = type;
                idObjectChoose1 = id;
                idBridgeChoose1 = idb;
                idPortChoose1 = idp;

                if (type == 0)
                {
                    // bridgeDialog
                    int idBridge = 0;
                    string priority = "";
                    BridgeDialog bd = new BridgeDialog(idBridge, priority);
                    bd.ShowDialog();
                    if (bd.DialogResult == DialogResult.OK)
                    {
                        idBridge = bd.id;
                        priority = bd.priority;
                        if (String.IsNullOrWhiteSpace(priority)) 
                        {
                            ClearStrip();
                            return;
                        }
                        netApp.net.AddBridge(idBridge, priority);
                        ClearStrip();
                        Refresh();
                        netApp.nw.EditPosBridge(idBridge, e.X, e.Y);
                        Refresh();
                    }
                }
                else if (type == 1)
                {
                    // contextMenuBridge
                    contextMenuStripBridge.Show(Cursor.Position);
                }
                else if (type == 2)
                {
                    // contextMenuPort
                    contextMenuStripPort.Show(Cursor.Position);
                }
                else if (type == 3)
                {
                    // contextMenuLink
                    contextMenuStripLink.Show(Cursor.Position);
                }
                
            }
        }

        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            netApp.nw.Find((int)(e.X * 1 / scale), (int)(e.Y * 1 / scale), out int type, out int id, out int idb, out int idp);
            typeObjectMove = type;
            idObjectMove = id;
            idBridgeMove = idb;
            idPortMove = idp;
            if (typeObjectMove == 0)
            {
                Cursor.Current = Cursors.SizeAll;
                prevPosition.X = (int)(e.X * 1 / scale);
                prevPosition.Y = (int)(e.Y * 1 / scale);
            }
        }

        
        private void FormMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                flagMove = true;
                int X = (int)(e.X * 1 / scale);
                int Y = (int)(e.Y * 1 / scale);
                //Debug.WriteLine(typeObjectMove.ToString());
                if (typeObjectMove == 0)
                {

                    foreach (var (i, b) in netApp.nw.bridges)
                    {
                        netApp.nw.EditPosBridge(i, b.x + X - prevPosition.X, b.y + Y - prevPosition.Y);
                    }
                    foreach (var (i, l) in netApp.nw.links)
                    {
                        netApp.nw.EditPosLink(i, l.x + X - prevPosition.X, l.y + Y - prevPosition.Y);
                    }
                    Refresh();
                    prevPosition.X = X;
                    prevPosition.Y = Y;
                }
                else if (typeObjectMove == 1)
                {
                    netApp.nw.EditPosBridge(idObjectMove, X, Y);
                }
                else if (typeObjectMove == 2)
                {
                    netApp.nw.EditPosPort(idBridgeMove, idObjectMove, X, Y);
                }
                else if (typeObjectMove == 3)
                {
                    netApp.nw.EditPosLink(idObjectMove, X, Y);
                }
                Refresh();
            }
        }

        private void FormMain_MouseUp(object sender, MouseEventArgs e)
        {
            typeObjectMove = 0;
            idObjectMove = 0;
            idBridgeMove = 0;
            idPortMove = 0;
            Cursor.Current = Cursors.Default;
        }

        
        private void FormMain_MouseWheel(object sender, MouseEventArgs e)
        {
            float k = (float)0.01;
            if (e.Delta > 0)
                scale += k;
            else
                scale -= k;

            if (scale <= 0) scale = (float)0.0001;

            Refresh();
            
        }
    }
}
