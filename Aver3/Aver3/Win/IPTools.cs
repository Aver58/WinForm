using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Aver3.Win
{
    public partial class IPTools : Form
    {
        public IPTools()
        {
            InitializeComponent();
        }
        private void IPTools_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
        }

        /// <summary>
        /// 获取远程主机IP
        /// </summary>
        /// <param name="RemoteHostName"></param>
        /// <returns></returns>
        public string[] getRemoteIP(string RemoteHostName)
        {
            IPHostEntry ipEntry = Dns.GetHostEntry(RemoteHostName);
            IPAddress[] IpAddr = ipEntry.AddressList;
            string[] strAddr = new string[IpAddr.Length];
            for (int i = 0; i < IpAddr.Length; i++)
            {
                strAddr[i] = IpAddr[i].ToString();
            }
            return (strAddr);
        }
        /// <summary>
        /// 开启进程，调用nbtstat命令，通过分析该命令的执行结果获得指定IP的主机名。
        /// </summary>
        /// <param name="clientIP"></param>
        /// <returns></returns>
        public static string GetRemoteHostByNetBIOS(string clientIP)
        {
            string ip = clientIP;
            string dirResults = "";
            char[] hostResult;
            string Result;
            ProcessStartInfo psi = new ProcessStartInfo();
            Process proc = new Process();
            psi.FileName = "cmd.exe";       //命令
            psi.Arguments = "nbtstat -A " + ip;         //参数
            psi.UseShellExecute = false;        //禁止使用操作系统外壳程序启动进程  
            psi.RedirectStandardInput = true;   //应用程序的输入从流中读取  
            psi.RedirectStandardOutput = true;  //应用程序的输出写入流中  
            psi.RedirectStandardError = true;   //将错误信息写入流  
            psi.RedirectStandardError = true;   //是否在新窗口中启动进程  
            proc = Process.Start(psi);

            proc.WaitForExit();
            dirResults = proc.StandardOutput.ReadToEnd();
            hostResult = new char[(dirResults.IndexOf("<") - dirResults.IndexOf("-/r/r/n") - 8)]; //后面4个空格加4个"-/r/r/n"字符
            dirResults.CopyTo(dirResults.IndexOf("-/r/r/n") + 8, hostResult, 0, dirResults.IndexOf("<") - dirResults.IndexOf("-/r/r/n") - 8);

            Result = new string(hostResult);
            return Result;
        }

        public static string[] GetLocalIP()
        {
            string hostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(hostName);
            IPAddress[] arr = ipEntry.AddressList;
            string[] result = new string[arr.Length];
            for (int i = 0; i< arr.Length; i++)
            {
                result[i] = arr[i].ToString();
            }
            return result;
            //fe80::5da8: e7c: af3: b07c % 13
            //fe80::64cd: 96a1: 5d92:ed70 % 20
            //fe80::ad7a:9494:5304:72c5 % 22
            //192.168.72.174
            //192.168.33.1
            //192.168.197.1
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //构造一个主机对象 IPHostEntry两个常用属性(AddressList地址列表|HostName主机名称)  
            IPHostEntry myHost = new IPHostEntry();
            try
            {
                //一.通过获取主机名HostName获取主机Host信息  
                myHost = Dns.GetHostEntry(Dns.GetHostName());

                //textBox1编辑框:显示主机名称  
                textBox1.Text = myHost.HostName.ToString();

                //richTextBox1高级文本输入编辑控件:AppendText函数追加本地主机信息  
                richTextBox1.AppendText("1.本地主机名称-->" + myHost.HostName.ToString() + "\r");

                //二.获取本地ip地址  
                for (int i = 0; i < myHost.AddressList.Length; i++)
                {
                    textBox2.Text = myHost.AddressList[i].ToString();
                    richTextBox1.AppendText("2.本地主机ip地址-->" + myHost.AddressList[i].ToString() + "\r");
                }
                //textBox2.Text = myHost.AddressList[myHost.AddressList.Length-1].ToString();
                //三.输入远程ip地址\域名查询   
                //构造一个远程主机对象和字符串变量strIP存储ip地址  
                IPHostEntry otherHost = new IPHostEntry();
                int j;

                //Resolve函数:将域名转换为ip地址 www.baidu.com  
                otherHost = Dns.GetHostEntry(textBox3.Text);

                //获取远程查询ip地址  
                for (j = 0; j < otherHost.AddressList.Length; j++)
                {
                    textBox4.Text = otherHost.AddressList[j].ToString();
                    richTextBox1.AppendText("3.远程域名-->" + textBox3.Text + "\r*ip地址-->" + otherHost.AddressList[j].ToString() + "\r");
                }

                //④ip地址查找对应的物理位置  
                //通过访问有道网站查询ip的物理位置 (方法二:下载一个ip库,查找ip库中的内容)  
                string sURL = "http://www.youdao.com/smartresult-xml/search.s?type=ip&q="
                    + otherHost.AddressList[j - 1].ToString() + "";  //youdao的URL  

                //定义字符串变量存储物理位置  
                string stringIpAddress = "";

                //获取youdao返回的xml格式文件内容    
                using (XmlReader read = XmlReader.Create(sURL))
                {
                    while (read.Read())      //从流中读取下一个字节  
                    {
                        switch (read.NodeType)
                        {
                            case XmlNodeType.Text:      //取xml格式文件当中的文本内容  查询的是最后一个ip地址AddressList[j-1]  
                                if (string.Format("{0}", read.Value).ToString().Trim() != otherHost.AddressList[j - 1].ToString())
                                {
                                    stringIpAddress = string.Format("{0}", read.Value).ToString().Trim();  //赋值    
                                }
                                break;
                        }
                    }
                }
                //textBox5对话框:赋值物理位置并追加至richTextBox1末尾  
                textBox5.Text = stringIpAddress;
                richTextBox1.AppendText("4.物理位置-->" + stringIpAddress + "\r");
                textBox6.Text = otherHost.HostName;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                textBox6.Enabled = true;
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);          //提示错误信息  
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }
    }
}
