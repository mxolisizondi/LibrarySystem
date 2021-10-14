using LibrarySystem.Forms;
using System;
using System.Windows.Forms;

namespace LibraryManagementSystem.UI
{
    public partial class frmSplash : Form
    {
        private int _staffID;
        public frmSplash(int staffID)
        {
            InitializeComponent();
            _staffID = staffID;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel2.Width += 3;
            if (panel2.Width >= 700)
            {
                this.Hide();
                timer1.Stop();
                frmMain f = new frmMain(_staffID);
                f.ShowDialog();
                Application.Exit();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
