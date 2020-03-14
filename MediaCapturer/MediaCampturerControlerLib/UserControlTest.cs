using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaCampturerControlerLib
{
    public partial class UserControlTest : UserControl
    {
        public UserControlTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           var dialogResult= colorDialog1.ShowDialog();
            pictureBox1.BackColor= colorDialog1.Color;
        }
    }
}
