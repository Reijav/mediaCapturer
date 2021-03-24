using MediaCampturerControlerLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraCapturer
{
    public partial class FormUsandoControlLib : Form
    {

        private UserControlTest ControlTest1;
        private UserControlVideoCapturer VideoCapturer1;

        public UserControlTest ControlTest
        {
            get
            {
                if (ControlTest1 == null)
                {
                    ControlTest1 = new UserControlTest();
                    
                }
                return ControlTest1;
            }
        }
        public UserControlVideoCapturer VideoCapturer
        {
            get
            {
                if (VideoCapturer1 == null)
                {
                    VideoCapturer1 = new UserControlVideoCapturer();
                    VideoCapturer1.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                  
                    VideoCapturer1.Size = new Size(this.Size.Width,this.Size.Height-50);
                }
                return VideoCapturer1;
            }
        }

        public FormUsandoControlLib()
        {
            InitializeComponent();
            panelControl.Controls.Clear();
            panelControl.Controls.Add(VideoCapturer);

        }

        private void FormUsandoControlLib_Load(object sender, EventArgs e)
        {
            
        }

        private void FormUsandoControlLib_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (VideoCapturer.SavingVideo())
            {

                e.Cancel = true;
                MessageBox.Show(this,"Se encuentra grabando, debe parar la grabación", "Cerrar Ventana", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }
    }
}
