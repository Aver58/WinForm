using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

namespace Aver3
{

    public partial class Form1 : Form
    {
        //窗体界面的初始化
        public Form1()
        {
            InitializeComponent();
        }
        //窗体程序的初始化Init
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;//默认选择第一个
            webBrowser1.GoHome();//浏览器默认主页
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            console.Text = Name;

            #region 读取文件
            /*
            label1.Text = Application.StartupPath + @"\data\111.txt";
            try
            {
                FileStream fs = new FileStream(Application.StartupPath + @"\data\111.txt", FileMode.Open, FileAccess.Read);//读取文件设定
                StreamReader m_streamReader = new StreamReader(fs);//设定读写的编码//使用StreamReader类来读取文件  
                m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
                //从数据流中读取每一行，直到文件的最后一行，并在angel.Text中显示出内容
                angel.Text = "";
                string strLine = m_streamReader.ReadLine();
                int i = 0;
                int j = 0;
                string[] a = new string[] { };
                string[] b = new string[1];
                string[] c = new string[1];
                while (strLine != null)
                {
                    a = strLine.Split('\t');   //将取得一行的数据分割成两个字符串放入Array类型的 a 中
                    b[j] = a[i];                //将Tab前面的数据放入Array类型的 b 中
                    c[j] = a[i + 1];            //将Tab前面的数据放入Array类型的 c 中
                    angel.Text = b[j].ToString() + '\r' + '\n';
                    level.Text = c[j].ToString() + '\r' + '\n';
                    strLine = m_streamReader.ReadLine();    //读取文件中的下一行
                    Array.Resize(ref b, b.Length + 1);
                    Array.Resize(ref c, c.Length + 1);
                    j = j + 1;
                }
                m_streamReader.Close();    //关闭此StreamReader对象
                int t = c.Length - 1;
                float maxNum = float.Parse(c[0]);
                //找到b中数据最大的电平，并找到和它同行的角度
                for (i = 1; i < t; i++)
                {
                    while (maxNum < float.Parse(c[i]))
                    {
                        maxNum = float.Parse(c[i]);
                        int m = i;
                        maxLevel.Text = maxNum.ToString();
                        maxAngle.Text = b[m].ToString();
                    }
                }
            }
            catch
            {
                //抛出异常
                MessageBox.Show("发生错误，请检查！");
                return;
            }
            //异常检测结束
            */
            #endregion

        }

        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(comboBox1.SelectedItem.ToString());
        }
        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ////下面是你的执行操作代码
            //try
            //{
            //    textBox1.Text = webBrowser1.Document.GetElementById("password").ToString();
            //    webBrowser1.Document.GetElementById("password").SetAttribute("value", "Welcome123");

            //}
            //catch (Exception m)
            //{
            //    MessageBox.Show(m.ToString());
            //    return;
            //}
        }
        #region RunCmd
        /// <summary>
        /// 运行cmd命令
        /// 不显示命令窗口
        /// </summary>
        /// <param name="cmdExe">指定应用程序的完整路径</param>
        /// <param name="cmdStr">执行命令行参数</param>
        bool RunCmd(string cmdExe, string cmdStr)
        {
            bool result = false;
            try
            {
                using (Process cmd = new Process())
                {
                    cmd.StartInfo.FileName = "cmd.exe";//设置运行的命令行文件问ping.exe文件，这个文件系统会自己找到
                                                       //如果是其它exe文件，则有可能需要指定详细路径，如运行winRar.exe
                    cmd.StartInfo.UseShellExecute = false;
                    cmd.StartInfo.RedirectStandardInput = true; //接受来自调用程序的输入信息
                    cmd.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
                    cmd.StartInfo.RedirectStandardError = true; //重定向标准错误输出
                    cmd.StartInfo.CreateNoWindow = true;        //不显示程序窗口
                    cmd.StartInfo.UseShellExecute = false;//是否指定操作系统外壳进程启动程序
                    //cmd.Arguments = command.Text;//设置命令参数  
                    cmd.Start();
                    //如果调用程序路径中有空格时，cmd命令执行失败，可以用双引号括起来 ，在这里两个引号表示一个引号（转义）
                    string str = string.Format(@"""{0}"" {1} {2}", cmdExe, cmdStr, "&exit");
                    StreamReader reader = cmd.StandardOutput;//截取输出流
                    string line = reader.ReadLine();//每次读取一行
                    while (!reader.EndOfStream)
                    {
                        Screen.AppendText("\r\n " + line);
                        line = reader.ReadLine();
                    }
                    //cmd.StandardInput.AutoFlush = true;
                    cmd.WaitForExit();//等待程序执行完退出进程
                    cmd.Close();//关闭进程
                    reader.Close();//关闭流
                    result = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return result;
        }
        /// <summary>
        /// 运行cmd命令
        /// 会显示命令窗口
        /// </summary>
        /// <param name="cmdExe">指定应用程序的完整路径</param>
        /// <param name="cmdStr">执行命令行参数</param>
        static bool RunCmd2(string cmdExe, string cmdStr)
        {
            bool result = false;
            try
            {
                using (Process myPro = new Process())
                {
                    //指定启动进程是调用的应用程序和命令行参数
                    ProcessStartInfo psi = new ProcessStartInfo(cmdExe, cmdStr);
                    myPro.StartInfo = psi;
                    myPro.Start();
                    myPro.WaitForExit();
                    result = true;
                }
            }
            catch
            {

            }
            return result;
        }
        #endregion
        #region RUNcmd-Original
        /*
        /// <summary>
        /// 运行cmd命令
        /// 会显示命令窗口
        /// </summary>
        /// <param name="cmdExe">指定应用程序的完整路径</param>
        /// <param name="cmdStr">执行命令行参数</param>
        static bool RunCmd(string cmdExe, string cmdStr)
        {
            bool result = false;
            try
            {
                using (Process myPro = new Process())
                {
                    //指定启动进程是调用的应用程序和命令行参数
                    ProcessStartInfo psi = new ProcessStartInfo(cmdExe, cmdStr);
                    myPro.StartInfo = psi;
                    myPro.Start();
                    myPro.WaitForExit();
                    result = true;
                }
            }
            catch
            {

            }
            return result;
        }

        /// <summary>
        /// 运行cmd命令
        /// 不显示命令窗口
        /// </summary>
        /// <param name="cmdExe">指定应用程序的完整路径</param>
        /// <param name="cmdStr">执行命令行参数</param>
        static bool RunCmd2(string cmdExe, string cmdStr)
        {
            bool result = false;
            try
            {
                using (Process myPro = new Process())
                {
                    myPro.StartInfo.FileName = "cmd.exe";
                    myPro.StartInfo.UseShellExecute = false;
                    myPro.StartInfo.RedirectStandardInput = true;
                    myPro.StartInfo.RedirectStandardOutput = true;
                    myPro.StartInfo.RedirectStandardError = true;
                    myPro.StartInfo.CreateNoWindow = true;
                    myPro.Start();
                    //如果调用程序路径中有空格时，cmd命令执行失败，可以用双引号括起来 ，在这里两个引号表示一个引号（转义）
                    string str = string.Format(@"""{0}"" {1} {2}", cmdExe, cmdStr, "&exit");

                    myPro.StandardInput.WriteLine(str);
                    myPro.StandardInput.AutoFlush = true;
                    myPro.WaitForExit();

                    result = true;
                }
            }
            catch
            {

            }
            return result;
        }*/
        #endregion
        private void button3_Click(object sender, EventArgs e)
        {
            //RunCmd(command.Text, param.Text);
            //RunCmd2(command.Text, param.Text);
            #region GetByte
            UnicodeEncoding unicode = new UnicodeEncoding();
            byte[] first = unicode.GetBytes("一起摇摆");//将汉字字符转化为BYTE数组
            MemoryStream ms = new MemoryStream(2);
            ms.Write(first, 0, first.Length);
            //console.Text = ms.Capacity.ToString() + "||" + ms.Length.ToString();
            #endregion
            #region FileStream
            string filePath = "1.txt";
            string str = "555";
            
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Create);//实例化

            }
            catch (Exception)
            {

                throw;
            }
            #endregion
        }
    }
}
