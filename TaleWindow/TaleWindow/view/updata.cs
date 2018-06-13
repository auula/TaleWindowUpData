using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Text;
using System.Windows.Forms;
using TaleWindow.utils;

namespace TaleWindow.view
{
    public partial class updata : Form
    {
        public updata()
        {
            InitializeComponent();
        }
        JObject jo = null;
        Timer timer = null;
        private void updata_Load(object sender, EventArgs e)
        {
            string txtContent = GetVersion("https://raw.githubusercontent.com/coding1618/TaleWindowUpData/master/updata.json");
            //MessageBox.Show(txtContent);
            //string jsonText = "{\"zone\":\"海淀\",\"zone_en\":\"haidian\"}";
            jo = (JObject)JsonConvert.DeserializeObject(txtContent);
            //MessageBox.Show(jo["uptime"].ToString());
            textBox1.Text = jo["uplog"].ToString();
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
            MessageBox.Show(jo["fileurl"].ToString());
            DownloadFile(jo["fileurl"].ToString(), @"C:\tale.zip", progressBar1, label2);
            Tools.DeleteFile(@"C:\TaleBlog\tale");
            SharpZip.UnZip(@"C:\\tale.zip", @"C:\TaleBlog");
            MessageBox.Show("更新成功！请重启软件!请使用请客户端启动程序！！客户端路径为C:\\TaleBlog\\tale\\tale.exe", "提示");
        }

        private void label2_Click(object sender, EventArgs e)
        {

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


        /// <summary>        
        /// c#,.net 下载文件        
        /// </summary>        
        /// http://localhost:8080/tomcat.zip
        /// <param name="URL">下载文件地址</param>       
        /// 
        /// <param name="Filename">下载后的存放地址</param>        
        /// <param name="Prog">用于显示的进度条</param>        
        /// 
        public void DownloadFile(string URL, string filename, System.Windows.Forms.ProgressBar prog, System.Windows.Forms.Label label1)
        {
            float percent = 0;
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

                    percent = (float)totalDownloadedByte / (float)totalBytes * 100;
                    label1.Text = "当前补丁下载进度" + percent.ToString() + "%";
                    System.Windows.Forms.Application.DoEvents(); //必须加注这句代码，否则label1将因为循环执行太快而来不及显示信息
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("确定要退出更新吗？你要好好想想哦(⊙o⊙)…", "提示: ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dr == DialogResult.OK)   //如果单击“是”按钮
            {
                e.Cancel = false;                 //关闭窗体
                //Application.Exit();
            }
            else if (dr == DialogResult.Cancel)
            {
                e.Cancel = true;                  //不执行操作
               
            }
        }
    }
}
