using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaleWindow.utils;
using TaleWindow.view;

namespace TaleWindow
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            string txtContent = GetVersion("https://raw.githubusercontent.com/coding1618/TaleWindowUpData/master/version.txt");
            string L = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            //MessageBox.Show(Tools.ReadTxtContent(@"‪C:\TaleBlog\tale\version.ini") + ":"+txtContent);
            //检查环境变量
            if (Set_OS_Path.CheckSysEnvironmentExist("TaleHome"))
            {
                //检查更新
                if (!txtContent.Equals(L))
                {
                    MessageBox.Show("软件已经发布更新!请去更新程序！");
                    Application.Run(new updata());
                }
                else
                {
                    Application.Run(new Tale());
                }

            }
            else
            {
                Application.Run(new install());
            }
            
        }
        private static string GetVersion(string strURL)
        {
            HttpWebRequest request;
            // 创建一个<a href="https://www.baidu.com/s?wd=HTTP%E8%AF%B7%E6%B1%82&tn=44039180_cpr&fenlei=mv6quAkxTZn0IZRqIHckPjm4nH00T1Y4m19Bmyw9nvDzPj6dPhmL0ZwV5Hcvrjm3rH6sPfKWUMw85HfYnjn4nH6sgvPsT6KdThsqpZwYTjCEQLGCpyw9Uz4Bmy-bIi4WUvYETgN-TLwGUv3EnWDkrjn1nHbsn1czPWDdPHTYPs" target="_blank" class="baidu-highlight">HTTP请求</a>
            request = (HttpWebRequest)WebRequest.Create(strURL);
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            System.IO.StreamReader myreader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string responseText = myreader.ReadToEnd();
            myreader.Close();
            return responseText;
        }
    }
}
