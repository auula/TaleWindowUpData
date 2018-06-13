using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaleWindow.utils
{
    class Tools
    {
        /// <summary>
        /// 根据路径删除文件
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteFile(string path)
        {
            FileAttributes attr = File.GetAttributes(path);
            if (attr == FileAttributes.Directory)
            {
                Directory.Delete(path, true);
            }
            else
            {
                File.Delete(path);
            }
        }
        public static String start()
        {

            
            System.Diagnostics.ProcessStartInfo myStartInfo = new System.Diagnostics.ProcessStartInfo();
            myStartInfo.FileName = @"C:\TaleBlog\tale\start.bat";
            System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
            myProcess.StartInfo = myStartInfo;
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProcess.Start();
            string P_id = myProcess.Id.ToString();
            return P_id;
        }

        /// <summary>
        /// 读取txt文件内容
        /// </summary>
        /// <param name="Path">文件地址</param>
        public static String ReadTxtContent(string Path)
        {
            byte[] byData = new byte[100];
            char[] charData = new char[1000];
            
                try
                {
                    FileStream file = new FileStream("‪C:\\TaleBlog\\tale\\version.ini", FileMode.Open);
                    file.Seek(0, SeekOrigin.Begin);
                    file.Read(byData, 0, 100); //byData传进来的字节数组,用以接受FileStream对象中的数据,第2个参数是字节数组中开始写入数据的位置,它通常是0,表示从数组的开端文件中向数组写数据,最后一个参数规定从文件读多少字符.
                    Decoder d = Encoding.Default.GetDecoder();
                    d.GetChars(byData, 0, byData.Length, charData, 0);
                    Console.WriteLine(charData);
                    file.Close();
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.ToString());
                }
            return charData.ToString();
        }
    }
}
