using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

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
            //Application.Run(new Main());
            Application.Run(new Setting());
            //Application.Run(new Win.Setting.Backup());
        }
    }
   
}
