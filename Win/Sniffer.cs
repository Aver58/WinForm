using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Threading;

namespace Aver3.Win
{
    public partial class Sniffer : Form
    {
        public Sniffer()
        {
            InitializeComponent();
        }
        #region 抓包执行部分
        //IP标头结构定义
        //StructLayoutAttribute 可控制类或结构的数据字段的物理布局
        //枚举值LayoutKind.Explicit表示对象的各个成员在非托管内存中的精确位置
        //被显式控制
        //每个成员必须使用FieldOffsetAttribute 知识字段在类型中的位置
        [StructLayout(LayoutKind.Explicit)]
        public struct IPHeader
        {
            [FieldOffset(0)]
            public byte versionAndLength;        //4位首部长度+4位IP版本号
            [FieldOffset(1)]
            public byte ip_tos;                  //8位服务类型TOS
            [FieldOffset(2)]
            public ushort ip_totallength;        //16位数据包总长度（字节）
            [FieldOffset(4)]
            public ushort identifier;            //16位标识
            [FieldOffset(6)]
            public ushort flagsAndOffset;        //3位标志位
            [FieldOffset(8)]
            public byte timeToLive;              //8位生存时间 TTL
            [FieldOffset(9)]
            public byte protocol;                //8位协议(TCP, UDP, ICMP, Etc.)
            [FieldOffset(10)]
            public ushort checksum;              //16位IP首部校验和
            [FieldOffset(12)]
            public uint sourceAddress;           //32位源IP地址
            [FieldOffset(16)]
            public uint destinationAddress;      //32位目的IP地址
        }
        //当每个封包到达是，就可以强制类型转化，把包中的数据流转化为一个个IPHeader对象
        public class SnifferScocket
        {
            public bool keepRunning { get; set; }                                //是否继续进行
            private static int receiverBufferLength;                 //得到的数据流的长度
            byte[] receive_buf_bytes;                                //收到的字节
            private Socket socket = null;                            //声明套接字
            public SnifferScocket()                                  //构造函数
            {
                ErrorOccurred = false;
                receiverBufferLength = 4096;                         //设置数据包最大长度
                //为数据包预分配存储空间
                receive_buf_bytes = new byte[receiverBufferLength];
            }
            public bool ErrorOccurred { get; private set; }
            //创建并绑定套接字对象
            public void CreateAndBindSocket(string IP)
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
                socket.Blocking = false;                                        //置socket非阻塞状态
                socket.Bind(new IPEndPoint(IPAddress.Parse(IP), 0));            //绑定套接字
                if (SetSocketOption() == false) ErrorOccurred = true;
            }
            //设置socket选项
            private bool SetSocketOption()
            {
                SniffSocketException ex;
                bool ret_value = true;
                try
                {
                    socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, 1);
                    byte[] inValue = new byte[4] { 1, 0, 0, 0 };
                    byte[] outValue = new byte[4];                                   //操作返回的输出数据
                    const int ioControlCode = unchecked((int)0x98000001);       //指定执行的操作的控制代码

                    //低级别操作模式,接受所有的数据包，这一步是关键，必须把socket设成raw和IP Level才可用ioControlCode
                    int returnCode = socket.IOControl(ioControlCode, inValue, outValue);
                    returnCode = outValue[0] + outValue[1] + outValue[2] + outValue[3];//把4个8位字节合成一个32位整数
                    if (returnCode != 0)
                    {
                        ex = new SniffSocketException("command execute error!");
                        ret_value = false;
                    }
                }
                catch (SocketException se)
                {
                    ret_value = false;
                    ex = new SniffSocketException("socket error!", se);
                    MessageBox.Show(ex.Message);
                }
                return ret_value;
            }
            //自定义异常
            public class SniffSocketException : Exception
            {
                public SniffSocketException() : base() { }
                public SniffSocketException(string message) : base(message) { }
                public SniffSocketException(string message, Exception innerException) : base(message, innerException) { }
            }
            //关闭raw socket
            public void Shutdown()
            {
                if (socket != null)
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
            }
            //开始监听从连接的Socket中异步接收数据
            public void Run()
            {
                try
                {
                    IAsyncResult ar = socket.BeginReceive(receive_buf_bytes, 0, receiverBufferLength, SocketFlags.None, new AsyncCallback(CallReceive), this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
            //异步回调
            private void CallReceive(IAsyncResult ar)
            {
                int received_bytes;
                received_bytes = socket.EndReceive(ar);//结束过去的异步读取
                //解析接收的数据包，并引发PacketArrival事件
                Receive(receive_buf_bytes, received_bytes);
                if (keepRunning) Run();
            }
            //解析接收的数据包，形成PacketArrivedEventArgs事件数据类对象，并引发OnPacketArrival事件
            unsafe private void Receive(byte[] buf, int len)
            {
                byte temp_protocol = 0;
                uint temp_version = 0;
                uint temp_sourceAddress = 0;
                uint temp_destinationAddress = 0;
                short temp_srcport = 0;
                short temp_dstport = 0;
                IPAddress temp_ip;
                PacketArrivedEventArgs e = new PacketArrivedEventArgs();//新网络数据包信息事件
                fixed (byte* fixed_buf = buf)
                {
                    IPHeader* head = (IPHeader*)fixed_buf;              //把数据流整和为IPHeader结构
                    e.HeaderLength = (uint)(head->versionAndLength & 0x0F) << 2;
                    e.IPHeaderBuffer = new byte[e.HeaderLength];
                    temp_protocol = head->protocol;
                    switch (temp_protocol)                              //提取协议类型
                    {
                        case 1: e.Protocol = "ICMP"; break;
                        case 2: e.Protocol = "IGMP"; break;
                        case 6: e.Protocol = "TCP"; break;
                        case 17: e.Protocol = "UDP"; break;
                        default: e.Protocol = "UNKNOWN"; break;
                    }
                    temp_version = (uint)(head->versionAndLength & 0xF0) >> 4;//提取IP协议版本
                    e.IPVersion = temp_version.ToString();
                    //以下语句提取出了PacketArrivedEventArgs对象中的其他参数
                    temp_sourceAddress = head->sourceAddress;
                    temp_destinationAddress = head->destinationAddress;
                    temp_ip = new IPAddress(temp_sourceAddress);
                    e.OriginationAddress = temp_ip.ToString();
                    temp_ip = new IPAddress(temp_destinationAddress);
                    e.DestinationAddress = temp_ip.ToString();
                    temp_srcport = *(short*)&fixed_buf[e.HeaderLength];
                    temp_dstport = *(short*)&fixed_buf[e.HeaderLength + 2];
                    e.OriginationPort = IPAddress.NetworkToHostOrder(temp_srcport).ToString();
                    e.DestinationPort = IPAddress.NetworkToHostOrder(temp_dstport).ToString();
                    e.PacketLength = (uint)len;
                    e.MessageLength = (uint)len - e.HeaderLength;
                    e.MessageBuffer = new byte[e.MessageLength];
                    e.ReceiveBuffer = buf;
                    //把buf中的IP头赋给PacketArrivedEventArgs中的IPHeaderBuffer
                    Array.Copy(buf, 0, e.IPHeaderBuffer, 0, (int)e.HeaderLength);
                    //把buf中的包中内容赋给PacketArrivedEventArgs中的MessageBuffer
                    Array.Copy(buf, (int)e.HeaderLength, e.MessageBuffer, 0, (int)e.MessageLength);
                }
                //引发PacketArrival事件
                OnPacketArrival(e);
            }


            public delegate void PacketArrivedEventHandler(Object sender, PacketArrivedEventArgs args);
            //事件句柄：包到达时引发事件
            public event PacketArrivedEventHandler PacketArrival;//声明时间句柄函数
            private void OnPacketArrival(PacketArrivedEventArgs e)
            {
                PacketArrival?.Invoke(this, e);//触发事件
            }
            //定义封装包数据的事件参数类PacketArrivedEventArgs
            public class PacketArrivedEventArgs : EventArgs
            {
                public string Protocol;              //协议
                public string IPVersion;             //IP版本号
                public string OriginationAddress;    //源地址
                public string DestinationAddress;    //目标地址
                public string OriginationPort;       //源端口
                public string DestinationPort;       //目标端口
                public uint HeaderLength;            //数据包头长度
                public uint PacketLength;            //数据包总长
                public uint MessageLength;           //数据包消息长度
                public byte[] ReceiveBuffer;         //数据字节流
                public byte[] IPHeaderBuffer;        //包头字节流
                public byte[] MessageBuffer;         //消息字节流
                public DateTime data = DateTime.Now; //捕获时间

                public PacketArrivedEventArgs()
                {
                    Protocol = "";
                    IPVersion = "";
                    OriginationAddress = "";
                    DestinationAddress = "";
                    OriginationPort = "";
                    DestinationPort = "";

                    HeaderLength = 0;
                    PacketLength = 0;
                    MessageLength = 0;

                    ReceiveBuffer = new byte[receiverBufferLength];
                    IPHeaderBuffer = new byte[receiverBufferLength];
                    MessageBuffer = new byte[receiverBufferLength];
                    data = DateTime.Now;
                }
                public override string ToString()
                {
                    StringBuilder sb = new StringBuilder();
                    //sb.Append(""r"n----------------"r"n");
                    //sb.AppendFormat(
                    //"src = {0}:{1}, dst= {2}:{3}"r"n", OriginationAddress, OriginationPort,
                    //DestinationAddress, DestinationPort);
                    //sb.AppendFormat("protocol = {0}, ipVersion={1}"r"n", Protocol, IPVersion);
                    //sb.AppendFormat("PacketLength={0},MessageLength={1}", PacketLength, MessageLength);
                    //sb.Append(""r"n----------------"r"n");
                    return sb.ToString();
                }
            }
        }
        #endregion
        SnifferScocket mySniffSocket;
        private void Sniffer_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            //初始化listview的column
            listView1.Columns.Add("协议"   , 50);
            listView1.Columns.Add("源地址"  , 100);
            listView1.Columns.Add("目标地址" , 100);
            listView1.Columns.Add("源端口"  , 50);
            listView1.Columns.Add("目标端口" , 80);
            listView1.Columns.Add("时间"   , 150);
            string sqlstr = "SELECT * FROM Win32_NetworkAdapter Configuration";//搜寻WMI类型
            ManagementObjectSearcher query1 = new ManagementObjectSearcher(sqlstr);
            //获取格子管理对象集合
            ManagementObjectCollection queryCollection1 = query1.Get();
            string[] IPString = new string[10];
            int x = 0;
            string[] temp;
            mySniffSocket = new SnifferScocket();

            //获取本地网络配置
            foreach (ManagementObject item in queryCollection1)
            {
                temp = item["IPAddress"] as string[];
                if (temp != null)
                {
                    foreach (string item0 in temp)
                    {
                        IPString[x] = item0;
                        x++;
                    }
                }
            }
            //为组合框添加列表项
            for (int i = 0; i < x; i++)
            {
                if (IPString[i]!="")
                {
                    listView1.Items.Add(IPString[i]);
                }
            }
            comboBox1.Text = comboBox1.Items[0] as string;
            try
            {
                //创建和绑定Socket连接
                mySniffSocket.CreateAndBindSocket(comboBox1.Text);
            }
            catch (SnifferScocket.SniffSocketException ex)
            {
                MessageBox.Show(ex.Message);
            }
            mySniffSocket.PacketArrival += new SnifferScocket.PacketArrivedEventHandler(DataArrival);
        }

        private void DataArrival(object sender, SnifferScocket.PacketArrivedEventArgs args)
        {
            string str = "";
            //处理数据包的信息（16进制显示）
            foreach (byte item in args.MessageBuffer)
            {
                str += item.ToString("x") + "\t";
            }
            string[] data = new string[]
            {
                args.Protocol.ToString(),
                args.OriginationAddress.ToString(),
                args.OriginationPort.ToString(),
                args.DestinationAddress.ToString(),
                args.DestinationPort.ToString(),
                args.data.ToString(),str
            };
            //将基本信息显示到组件上
            listView1.Items.Add(new ListViewItem(data));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (mySniffSocket.keepRunning == false)
            {
                mySniffSocket.keepRunning = true;
                try
                {
                    mySniffSocket.Run();//开始监听
                }
                catch (SocketException se)
                {
                    MessageBox.Show(se.Message);
                    mySniffSocket.CreateAndBindSocket(comboBox1.Text);
                    mySniffSocket.Run();
                }
                button1.Text = "暂停";
            }
            else
            {
                button1.Text = "恢复";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mySniffSocket.keepRunning = false;
            button1.Text = "开始";
            Thread.Sleep(10);
            mySniffSocket.Shutdown();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            richTextBox1.Text = "";
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    richTextBox1.Text = item.SubItems[6].Text;
                }
            }
        }
    }
}
