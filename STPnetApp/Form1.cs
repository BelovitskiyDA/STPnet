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

        Net net;

        private void FormMain_Load(object sender, EventArgs e)
        {
            net = new Net();
        }

        private void buttonAddLink_Click(object sender, EventArgs e)
        {

        }

        
    }
}
