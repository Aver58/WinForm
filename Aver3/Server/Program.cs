using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;

namespace Server
{
    class Program
    {
        static Socket connectedClient;          //接收到连接上的客户端
        static List<Client> clientList = new List<Client>();//存放连上的socket
        static void Main(string[] args)
        {
            //1.创建客户端
            Socket tcpServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //2.发起连接请求
            //IPAddress ip = IPAddress.Parse("192.168.1.8");
            //int port = 8080;
            //IPEndPoint endPoint = new IPEndPoint(ip, port);//IPEndPoint对IP和端口进行封装
            //tcpServer.Bind(endPoint);//向系统申请可用的IP和端口进行通信
            
            tcpServer.Bind(new IPEndPoint(IPAddress.Parse(GetIP()), 8080));
            //3.开始监听
            tcpServer.Listen(100); //挂起连接队列的最大长度。
            Console.WriteLine("服务器已开启，等待连接。。。");
            //Console.ReadKey();//保留界面，便于查看

            while (true)
            {
                //4.接受客户端的连接Socket,
                //暂停当前线程，开始等待，直到有客户端连接进来
                connectedClient = tcpServer.Accept();
                Console.WriteLine("已有一个客户端连接到该服务器。。。");

                Client client = new Client(connectedClient);//把收发消息逻辑放到Client类处理
                clientList.Add(client);
                //Client c = new Client();
                //c.connectHandler(connectedClient);
            }
        }
        static string GetIP()
        {
            //本机IP地址
            string strLocalIP = "";
            //得到计算机名
            string strPcName = Dns.GetHostName();
            //得到本机IP地址数组
            IPHostEntry ipEntry = Dns.GetHostEntry(strPcName);
            foreach (var item in ipEntry.AddressList)
            {
                //判断当前字符串是否为正确IP地址
                if (IsRightIP(item.ToString()))
                {
                    //得到本地IP地址
                    strLocalIP = item.ToString();
                    return strLocalIP;
                    //结束循环
                }
            }
            return null;
        }
        /// <summary>
        /// 判断是否为正确的IP地址
        /// </summary>
        /// <param name="strIPadd">需要判断的字符串</param>
        /// <returns>true = 是 false = 否</returns>
        public static bool IsRightIP(string strIPadd)
        {
            //利用正则表达式判断字符串是否符合IPv4格式
            if (Regex.IsMatch(strIPadd, "[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}"))
            {
                //根据小数点分拆字符串
                string[] ips = strIPadd.Split('.');
                if (ips.Length == 4 || ips.Length == 6)
                {
                    //如果符合IPv4规则
                    if (System.Int32.Parse(ips[0]) < 256 && System.Int32.Parse(ips[1]) < 256 & System.Int32.Parse(ips[2]) < 256 & System.Int32.Parse(ips[3]) < 256)
                        //正确
                        return true;
                    //如果不符合
                    else
                        //错误
                        return false;
                }
                else
                    //错误
                    return false;
            }
            else
                //错误
                return false;
        }
        /// <summary>
        /// 向所有客户端广播接收到的消息
        /// </summary>
        /// <param name="s"></param>
        public static void BroadcastMessage(string s)
        {
            List<Client> notConnected = new List<Client>();
            foreach (Client item in clientList)
            {
                if (item.Connected())
                {
                    item.SendMessage(s);
                }
                else
                {
                    notConnected.Add(item);
                }
            }
            foreach (var item in notConnected)
            {
                clientList.Remove(item);
            }
        }
    }
}
