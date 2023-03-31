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
    public partial class PortDialog : Form
    {
        public int number = 0;
        public PortDialog()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                number = Int32.Parse(textBox1.Text);
            }
            catch
            {
                number = 0;
            }
        }
    }
}
