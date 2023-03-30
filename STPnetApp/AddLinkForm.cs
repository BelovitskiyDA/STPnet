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
    public partial class AddLinkForm : Form
    {
        Net net;
        public AddLinkForm(Net net)
        {
            InitializeComponent();
            this.net = net;
        }

        private void LinkForm_Load(object sender, EventArgs e)
        {
            foreach(var (id,b) in net.bridges)
            {
                comboBoxId1.Items.Add(id);
                comboBoxId2.Items.Add(id);
            }
        }

        private void comboBoxId1_TextChanged(object sender, EventArgs e)
        {
            Bridge bridge = net.bridges[Int32.Parse(comboBoxId1.Text)];
            foreach (var (pn, p) in bridge.ports)
            {
                if (!bridge.PortIsEmpty(pn)) continue;
                comboBoxN1.Items.Add(pn);
            }
        }

        private void comboBoxId2_TextChanged(object sender, EventArgs e)
        {
            Bridge bridge = net.bridges[Int32.Parse(comboBoxId2.Text)];
            foreach (var (pn, p) in bridge.ports)
            {
                if (!bridge.PortIsEmpty(pn)) continue;
                comboBoxN2.Items.Add(pn);
            }
        }
    }
}
