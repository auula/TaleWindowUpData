using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaleWindow.utils;

namespace TaleWindow.view
{
    public partial class Tale : Form
    {
        public Tale()
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.exit;
            DialogResult dr = MessageBox.Show("确定要退出吗？你要好好想想哦(⊙o⊙)…", "提示: ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dr == DialogResult.OK)   //如果单击“是”按钮
            {
                //关闭窗体
                Application.Exit();
            }
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void main_Load(object sender, EventArgs e)
        {
            Tools.start();
            
        }


        
        private void Type(Control sender, int p_1, double p_2)
        {
            GraphicsPath oPath = new GraphicsPath();
            oPath.AddClosedCurve(new Point[] { new Point(0, sender.Height / p_1), new Point(sender.Width / p_1, 0), new Point(sender.Width - sender.Width / p_1, 0), new Point(sender.Width, sender.Height / p_1), new Point(sender.Width, sender.Height - sender.Height / p_1), new Point(sender.Width - sender.Width / p_1, sender.Height), new Point(sender.Width / p_1, sender.Height), new Point(0, sender.Height - sender.Height / p_1) }, (float)p_2);
            sender.Region = new Region(oPath);
        }
      

        private void main_Paint_1(object sender, PaintEventArgs e)
        {
            Type(this, 25, 0.1);
        }

        private void main_Resize_1(object sender, EventArgs e)
        {
            Type(this, 25, 0.1);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://localhost:9000/admin");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            
            System.Diagnostics.Process.Start("http://localhost:9000/admin/article/publish");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            
            System.Diagnostics.Process.Start("http://localhost:9000/admin/attach");
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            about about = new about();
            about.Show();
        }
    }
}
