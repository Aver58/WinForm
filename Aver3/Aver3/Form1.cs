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

namespace Aver3
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
            //textBox1.Text = "";
            ProcessStartInfo start = new ProcessStartInfo("Ping.exe");//设置运行的命令行文件问ping.exe文件，这个文件系统会自己找到
            //如果是其它exe文件，则有可能需要指定详细路径，如运行winRar.exe
            start.FileName = "cmd.exe";
            start.RedirectStandardInput = true;//接受来自调用程序的输入信息
            start.RedirectStandardOutput = true;//由调用程序获取输出信息
            start.RedirectStandardError = true;//重定向标准错误输出
            start.CreateNoWindow = true;//不显示程序窗口
            start.UseShellExecute = false;//是否指定操作系统外壳进程启动程序
            //textBox1.Text += "&exit";
            start.Arguments = textBox1.Text;//设置命令参数  
            //p.StandardInput.WriteLine(str + "&exit"); //向cmd窗口发送输入信息
            Process p = Process.Start(start);
            StreamReader reader = p.StandardOutput;//截取输出流
            string line = reader.ReadLine();//每次读取一行
            while (!reader.EndOfStream)
            {
                textBox1.AppendText(line + "\r\n ");
                line = reader.ReadLine();
            }
            p.WaitForExit();//等待程序执行完退出进程
            p.Close();//关闭进程
            reader.Close();//关闭流
        }

   


        //Process myProcess = new Process();
        //myProcess.StartInfo.FileName = "路径"	myProcess.Start();


    }
}
