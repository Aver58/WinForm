using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace Aver3
{
    static class Program
    {
        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole();


        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region CMD尝试
            /*
#if DEBUG
            AllocConsole();
            Shell.WriteLine("注意：启动程序...");
            Shell.WriteLine("\tWritten by wuming");
            Shell.WriteLine("{0}：{1}", "警告", "这是一条警告信息。");
            Shell.WriteLine("{0}：{1}", "错误", "这是一条错误信息！");
            Shell.WriteLine("{0}：{1}", "注意", "这是一条需要的注意信息。");
            Shell.WriteLine("");
#endif  
#if DEBUG
            Shell.WriteLine("注意：2秒后关闭...");
            Thread.Sleep(1000);
            Shell.WriteLine("注意：1秒后关闭...");
            Thread.Sleep(1000);
            Shell.WriteLine("注意：正在关闭...");
            Thread.Sleep(100);
            FreeConsole();
#endif
             static class Shell
    {
        /// <summary>  
        /// 输出信息  
        /// </summary>  
        /// <param name="format"></param>  
        /// <param name="args"></param>  
        public static void WriteLine(string format, params object[] args)
        {
            WriteLine(string.Format(format, args));
        }

        /// <summary>  
        /// 输出信息  
        /// </summary>  
        /// <param name="output"></param>  
        public static void WriteLine(string output)
        {
            Console.ForegroundColor = GetConsoleColor(output);
            Console.WriteLine(@"[{0}]{1}", DateTimeOffset.Now, output);
        }

        /// <summary>  
        /// 根据输出文本选择控制台文字颜色  
        /// </summary>  
        /// <param name="output"></param>  
        /// <returns></returns>  
        private static ConsoleColor GetConsoleColor(string output)
        {
            if (output.StartsWith("警告")) return ConsoleColor.Yellow;
            if (output.StartsWith("错误")) return ConsoleColor.Red;
            if (output.StartsWith("注意")) return ConsoleColor.Green;
            return ConsoleColor.Gray;
        }
    }
    */
            #endregion
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            #region Socket
            //Socket connectedClient;         //接收到连接上的客户端
            ////List<Socket> clientList = new List<Socket>();//存放连上的socket

            ////1.创建客户端
            //Socket tcpServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ////2.发起连接请求
            //IPAddress ip = IPAddress.Parse("192.168.1.8");
            //int port = 8080;
            //EndPoint endPoint = new IPEndPoint(ip, port);//IPEndPoint对IP和端口进行封装
            //tcpServer.Bind(endPoint);//向系统申请可用的IP和端口进行通信
            ////3.开始监听
            //tcpServer.Listen(100); //挂起连接队列的最大长度。
            //Console.WriteLine("服务器已开启，等待连接。。。");

            ////while (true)
            ////{
            //    //4.接受客户端的连接Socket,
            //    //暂停当前线程，开始等待，直到有客户端连接进来
            //    connectedClient = tcpServer.Accept();
            //    Console.WriteLine("已有一个客户端连接到该服务器。。。");
                //clientList.Add(connectedClient);
            //}
            #endregion
            //Win.Sockets.Server s = new Win.Sockets.Server();
            //s.StartServer();
            Application.Run(new Win.Sockets.Client());

            //Application.Run(new Form1());
            //Application.Run(new Cmd());

            //Application.Run(new Win.Sort());
            //Application.Run(new Win.Astar());
            //Application.Run(new Win.Sniffer());
            //Application.Run(new Win.Setting.Backup());
        }
    }
   
}
