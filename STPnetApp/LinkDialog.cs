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
    public partial class LinkDialog : Form
    {
        internal int weight = 0;
        public LinkDialog(int weight)
        {
            InitializeComponent();
            if (weight != 0)
            {
                this.weight = weight;
                textBox1.Text = weight.ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                weight = Int32.Parse(textBox1.Text);
            }
            catch
            {
                weight = 0;
            }

            if (weight < 0) weight = 0;
        }
    }
}
