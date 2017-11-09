using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aver3
{
    public partial class Cmd : System.Windows.Forms.Form
    {
        public Cmd()
        {
            InitializeComponent();
            Generate.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "测网页/IP速度":
                    {
                        label3.Text = "请输入待测网站：";
                        //RunCmd("Ping", param.Text);
                        TestPing();
                        Generate.Text = Screen.Text; //计算速度ms，思路，正则出ms以及前面的3位，然后求平均值
                        break;
                    }
                case "用远程IP得主机名":
                    {
                        //label3.Text = "请输入待测网站：";
                        RunCmd("cmd.exe", "/c" + "nbtstat -A" +param.Text);
                        Get.Text = "主机名：";
                        Generate.Text = Screen.Text;
                        break;
                    }
                    //arp - s 192.168.0.11就看到对方mac地址C:\\Windows\\system32\\nbtstat.exe
                    //C:\\Windows\\sysnative\\nbtstat.exe
                    //RunCmd2("cmd.exe", "/c" + "nbtstat -n" + param.Text);//其中的“/”表示执行完命令后马上退出
                default:
                    break;
            }
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
        void TestPing()
        {
            try
            {
                Ping PingInfo = new Ping();
                PingOptions options = new PingOptions();
                options.DontFragment = true;    //数据分段
                string myInfo = "dkhakasbq";    //Test
                byte[] bufferInfo = Encoding.ASCII.GetBytes(myInfo);//获取字节
                int TimeOut = 120;
                //发送数据包
                PingReply reply = PingInfo.Send(param.Text, TimeOut, bufferInfo, options);
                if (reply.Status == IPStatus.Success)
                {
                    Screen.Text += "耗费时间：" + reply.RoundtripTime.ToString()+ "\r\n";  //耗费时间
                    Screen.Text += "路由节点：" + reply.Options.Ttl.ToString() + "\r\n";    //路由节点
                    Screen.Text += "数据分段：" + (reply.Options.DontFragment ? "发生分段" : "没有分段") + "\r\n";
                    Screen.Text += "缓冲区大小：" + reply.Buffer.Length.ToString();    //缓冲区大小
                }
                else
                {
                    MessageBox.Show("无法Ping");
                }
            }
            catch (Exception me)
            {
                MessageBox.Show(me.Message);
                throw;
            }
        }
        /// <summary>
        /// 运行cmd命令
        /// </summary>
        /// <param name="cmdExe">指定应用程序的完整路径</param>
        /// <param name="cmdArg">执行命令行参数</param>
        /// <summary>
        bool RunCmd(string cmdExe, string cmdArg)
        {
            bool result = false;
            try
            {
                using (Process myPro = new Process())
                {
                    //指定启动进程是调用的应用程序和命令行参数
                    Process[] MyProcess = Process.GetProcessesByName(cmdExe);
                    ProcessStartInfo psi = new ProcessStartInfo(cmdExe, cmdArg);
                    psi.UseShellExecute = false;
                    psi.CreateNoWindow = true;         //不显示程序窗口
                    psi.RedirectStandardInput = true;   //重定向输入信息
                    psi.RedirectStandardOutput = true;  //重定向获取输出信息
                    psi.RedirectStandardError = true;   //重定向标准错误输出
                    myPro.StartInfo = psi;
                    //方法①
                    //RunCmd2("cmd.exe", "/c" + "nbtstat -n" + param.Text);//其中的“/”表示执行完命令后马上退出
                    //方法②
                    //myPro.StandardInput.WriteLine(cmdArg + "&exit"); //向cmd窗口发送输入信息
                    //myPro.StandardInput.AutoFlush = true;//提交
                    myPro.Start();

                    //获取cmd窗口的输出信息 
                    StreamReader reader = myPro.StandardOutput;//截取输出流
                    string line = reader.ReadLine();//每次读取一行
                    //string StrInfo = myPro.StandardOutput.ReadToEnd();
                    //Screen.Text += StrInfo;
                    while (!reader.EndOfStream)
                    {
                        Screen.AppendText(line + "\r\n ");
                        line = reader.ReadLine();
                    }

                    myPro.WaitForExit();
                    //Console.WriteLine(MyProcess[0]);
                    //MyProcess[0].Kill();//结束进程
                    result = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return result;
        }
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
