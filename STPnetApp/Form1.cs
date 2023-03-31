﻿using System;
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
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        public Net net;
        public NetView nw;
        int typeObjectMove, idObjectMove, idBridgeMove, idPortMove;
        int typeObjectChoose1, idObjectChoose1, idBridgeChoose1, idPortChoose1;
        int typeObjectChoose2, idObjectChoose2, idBridgeChoose2, idPortChoose2;

        private void editLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int weight = net.links[idObjectChoose1].weight;
            LinkDialog ld = new LinkDialog(weight);
            ld.ShowDialog();
            if (ld.DialogResult == DialogResult.OK)
            {
                weight = ld.weight;
                if (weight != 0)
                {
                    net.EditLink(idObjectChoose1, weight);
                }
            }
            ClearStrip();
            Refresh();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            net.DisconnectLink(idObjectChoose1, idBridgeChoose1, idPortChoose1);
            nw.DeleteConnectionLink(idObjectChoose1, idBridgeChoose1, idPortChoose1);

            ClearStrip();
            Refresh();
        }

        private void editBridgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idBridge = idObjectChoose1;
            string priority = net.bridges[idBridge].priority.ToString("X");
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
                net.EditBridge(idBridge, priority);
                ClearStrip();
                Refresh();
            }
        }

        bool flagMove = false;

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
            if (flagMove)
            {
                ClearStrip();
                flagMove = false;
                return;
            }
            nw.Find(e.X, e.Y, out int type, out int id, out int idb, out int idp);

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
                            if (!net.PortIsEmpty(idBridgeChoose1, idObjectChoose1)
                                || !net.PortIsEmpty(idBridgeChoose2, idObjectChoose2)
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
                                    net.AddLink(idBridgeChoose1, idObjectChoose1, idBridgeChoose2, idObjectChoose2, weight);
                                }
                            }
                            ClearStrip();
                        }
                        else if (typeObjectChoose1 == 2 && typeObjectChoose2 == 3)
                        {
                            if (net.PortIsEmpty(idBridgeChoose1, idObjectChoose1))
                                if (MessageBox.Show("Добавить связь?", "AddConnection", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    net.AddConnect(idObjectChoose2, idBridgeChoose1, idObjectChoose1);
                        }
                        else if (typeObjectChoose1 == 3 && typeObjectChoose2 == 2)
                        {
                            if (net.PortIsEmpty(idBridgeChoose2, idObjectChoose2))
                                if (MessageBox.Show("Добавить связь?", "AddConnection", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    net.AddConnect(idObjectChoose1, idBridgeChoose2, idObjectChoose2);
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
                        /*typeObjectChoose1 = 0;
                        idObjectChoose1 = 0;
                        idBridgeChoose1 = 0;
                        idPortChoose1 = 0;
                        toolStripStatusLabel1.Text = "Element";*/
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
                        net.AddBridge(idBridge, priority);
                        ClearStrip();
                        Refresh();
                        nw.EditPosBridge(idBridge, e.X, e.Y);
                        Refresh();
                    }
                }
                else if (type == 1)
                {
                    // contextMenuBridge
                    contextMenuStripBridge.Show(Cursor.Position);
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
            nw.Find(e.X, e.Y, out int type, out int id, out int idb, out int idp);
            typeObjectMove = type;
            idObjectMove = id;
            idBridgeMove = idb;
            idPortMove = idp;
        }

        private void FormMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                flagMove = true;
                Debug.WriteLine(typeObjectMove.ToString());
                if (typeObjectMove == 0) return;
                if (typeObjectMove == 1)
                {
                    nw.EditPosBridge(idObjectMove, e.X, e.Y);
                }
                else if (typeObjectMove == 2)
                {
                    nw.EditPosPort(idBridgeMove, idObjectMove, e.X, e.Y);
                }
                else if (typeObjectMove == 3)
                {
                    nw.EditPosLink(idObjectMove, e.X, e.Y);
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
        }
    }
}
