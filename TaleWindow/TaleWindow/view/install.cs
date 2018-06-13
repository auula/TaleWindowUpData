using IWshRuntimeLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaleWindow.utils;

namespace TaleWindow
{
    public partial class install : Form
    {
        public install()
        {
            
            InitializeComponent();
            

        }
        Timer timer = null;
        private void install_Load(object sender, EventArgs e)
        {
            CreateShortcutOnDesktop();
            
            Directory.CreateDirectory(@"C:\TaleBlog");
            // 3000 毫秒，即3秒
            this.timer = new Timer();
            this.timer.Interval = 2000;
            // 设置运行
            this.timer.Enabled = true;
            this.timer.Tick += timer_Tick;
            
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Enabled = false;
            string txtContent = GetVersion("https://raw.githubusercontent.com/coding1618/TaleWindowUpData/master/updata.json");
            JObject jo = (JObject)JsonConvert.DeserializeObject(txtContent);
            DownloadFile(jo["fileurl"].ToString(), @"C:\tale.zip", progressBar1);
            Set_OS_Path.SetSysEnvironment("TaleHome", @"C:\TaleBlog");
            MessageBox.Show("安装成功请重启,请使用桌面快捷方式启动,本文件请保留！请点击X退出重启软件！","提示:");
            SharpZip.UnZip(@"C:\\tale.zip", @"C:\TaleBlog");
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("确定要退出安装吗？你要好好想想哦(⊙o⊙)…", "女装大佬提示: ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dr == DialogResult.OK)   //如果单击“是”按钮
            {
                e.Cancel = false;                 //关闭窗体
                //Application.Exit();
            }
            else if (dr == DialogResult.Cancel)
            {
                e.Cancel = true;                  //不执行操作
                MessageBox.Show("我就知道你没有那会坏~继续安装吧~");

            }
        }

        private static string GetVersion(string strURL)
        {
            HttpWebRequest request;

            request = (HttpWebRequest)WebRequest.Create(strURL);
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            System.IO.StreamReader myreader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string responseText = myreader.ReadToEnd();
            myreader.Close();
            return responseText;
        }

        private void CreateShortcutOnDesktop()
        {
            //添加引用 (com->Windows Script Host Object Model)，using IWshRuntimeLibrary;
            String shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "Tale.lnk");
            if (!System.IO.File.Exists(shortcutPath))
            {
                // 获取当前应用程序目录地址
                String exePath = Process.GetCurrentProcess().MainModule.FileName;
                IWshShell shell = new WshShell();
                // 确定是否已经创建的快捷键被改名了
                foreach (var item in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "*.lnk"))
                {
                    WshShortcut tempShortcut = (WshShortcut)shell.CreateShortcut(item);
                    if (tempShortcut.TargetPath == exePath)
                    {
                        return;
                    }
                }
                WshShortcut shortcut = (WshShortcut)shell.CreateShortcut(shortcutPath);
                //MessageBox.Show(exePath, "exePath");
                shortcut.TargetPath = exePath;
                shortcut.Arguments = "";// 参数  
                shortcut.Description = "Tale Tools Dev:codin QQ:2420498526 && biezhi QQ:921293209";
                shortcut.WorkingDirectory = Environment.CurrentDirectory;//程序所在文件夹，在快捷方式图标点击右键可以看到此属性  
                shortcut.IconLocation = exePath;//图标，该图标是应用程序的资源文件  
                //shortcut.Hotkey = "CTRL+SHIFT+W";//热键，发现没作用，大概需要注册一下  
                shortcut.WindowStyle = 1;
                shortcut.Save();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://lets-blade.com/");
        }




        /// <summary>        
        /// c#,.net 下载文件        
        /// </summary>        
        /// http://localhost:8080/tomcat.zip
        /// <param name="URL">下载文件地址</param>       
        /// 
        /// <param name="Filename">下载后的存放地址</param>        
        /// <param name="Prog">用于显示的进度条</param>        
        /// 
        public void DownloadFile(string URL, string filename, System.Windows.Forms.ProgressBar prog)
        {
            
            try
            {
                System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;
                if (prog != null)
                {
                    prog.Maximum = (int)totalBytes;
                }
                System.IO.Stream st = myrp.GetResponseStream();
                System.IO.Stream so = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    System.Windows.Forms.Application.DoEvents();
                    so.Write(by, 0, osize);
                    if (prog != null)
                    {
                        prog.Value = (int)totalDownloadedByte;
                    }
                    osize = st.Read(by, 0, (int)by.Length);

                    
                }
                so.Close();
                st.Close();
            }
            catch (System.Exception)
            {
                MessageBox.Show("硬盘读写或者网络连接异常！！\n请检查你相关功能是否能正常使用！请稍后重试^_^~", "更新失败:");
                Application.Exit();
            }
        }
        }

}
