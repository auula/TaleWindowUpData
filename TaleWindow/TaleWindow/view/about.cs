using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaleWindow.view
{
    public partial class about : Form
    {
        public about()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wparam, int lparam);
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)//按下的是鼠标左键            
            {
                Capture = false;//释放鼠标使能够手动操作                
                SendMessage(Handle, 0x00A1, 2, 0);//拖动窗体            
            }
        }
        private void about_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/otale/tale/stargazers");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/otale/tale/blob/master/README_ZH.md");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/otale");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://lets-blade.com/");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
