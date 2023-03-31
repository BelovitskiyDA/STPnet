using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STPnetApp
{
    public partial class BridgeDialog : Form
    {
        public int id = 0;
        public string priority = "";
        public BridgeDialog(int idBridge, string priority)
        {
            InitializeComponent();
            if (idBridge != 0)
            {
                id = idBridge;
                this.priority = priority;
                textBox1.Text = id.ToString();
                textBox1.Enabled = false;
                textBox2.Text = this.priority;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                id = Int32.Parse(textBox1.Text);
            }
            catch
            {
                id = 0;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            priority = textBox2.Text;
        }
    }
}
