using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GoldSoft.Identiter.UI
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            //var main = new Main();
            //main.Show();
            Cursor = Cursors.WaitCursor;
            this.Enabled = false;
            var message = "";
            var remoting = Remoting.Instance("key", out message);
            Cursor = Cursors.Default;

            if (remoting == null)
            {
                MessageBox.Show(message,"错误");
            }
            else
            {
                Main.RemotingInstance = remoting;
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }

            this.Cursor = Cursors.Default;
            this.Enabled = true;
        }

        private void ButtonMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
