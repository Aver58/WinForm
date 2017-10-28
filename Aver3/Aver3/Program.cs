using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace Aver3
{
    static class Program
    {
        //[DllImport("kernel32.dll")]
        //static extern bool FreeConsole();//Call Sysytem API，Disposed Console
        //[DllImport("kernel32.dll")]
        //public static extern bool AllocConsole();//Call Sysytem API，Show Console
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //AllocConsole();//Show Console
            Application.Run(new Form1());
            //FreeConsole();//Disposed Console
        }
    }
}
