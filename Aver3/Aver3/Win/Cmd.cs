using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aver3
{
    public partial class Cmd : Form
    {
        public Cmd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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
            //string filePath = "1.txt";
            //string str = "555";
            try
            {
                //FileStream fs = new FileStream(filePath, FileMode.Create);//实例化
            }
            catch (Exception)
            {
                throw;
            }
            #endregion
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
    }
}
