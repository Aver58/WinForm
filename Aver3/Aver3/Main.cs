using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Aver3
{
    public partial class Main : Form
    {
        public static bool isAutoBackup = false;
        public static string BackupTime = "00:00";//自动备份时间
        public static string address = "D:\\Aver";//自动保存文件路径
        public static List<string> filesName = new List<string>();
        bool Round = false;//界面按钮测试

        #region 修正输入法全角/半角的问题
        //声明一些API函数 
        [DllImport("imm32.dll")]
        public static extern IntPtr ImmGetContext(IntPtr hwnd);
        [DllImport("imm32.dll")]
        public static extern bool ImmGetOpenStatus(IntPtr himc);
        [DllImport("imm32.dll")]
        public static extern bool ImmSetOpenStatus(IntPtr himc, bool b);
        [DllImport("imm32.dll")]
        public static extern bool ImmGetConversionStatus(IntPtr himc, ref int lpdw, ref int lpdw2);
        [DllImport("imm32.dll")]
        public static extern int ImmSimulateHotKey(IntPtr hwnd, int lngHotkey);
        private const int IME_CMODE_FULLSHAPE = 0x8;
        private const int IME_CHOTKEY_SHAPE_TOGGLE = 0x11;
        protected override void OnActivated(EventArgs e)
        {
            ChangeIME();
        }
        void ChangeIME()//修改输入法
        {
            //base.OnActivated(e);
            IntPtr HIme = ImmGetContext(this.Handle);
            //设置“搜狗拼音”为当前输入法
            foreach (InputLanguage item in InputLanguage.InstalledInputLanguages)
            {
                if (item.LayoutName.Contains("搜狗拼音"))
                {
                    InputLanguage.CurrentInputLanguage = item;
                    break;
                }
            }
            //如果输入法处于打开状态 
            if (ImmGetOpenStatus(HIme))
            {
                int iMode = 0;
                int iSentence = 0;
                //检索输入法信息 
                bool bSuccess = ImmGetConversionStatus(HIme, ref iMode, ref iSentence);
                if (bSuccess)
                {
                    //如果是全角,转换成半角 
                    if ((iMode & IME_CMODE_FULLSHAPE) > 0)
                        ImmSimulateHotKey(this.Handle, IME_CHOTKEY_SHAPE_TOGGLE);
                }
            }
        }
        #endregion 修正输入法全角/半角的问题
        //窗体界面的初始化
        public Main()
        {
            InitializeComponent();
        }
        //窗体程序的初始化Init
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        #region MenuTool
        //LoadIni读取INI文件，并将信息载入菜单
        void LoadIni()
        {
            StreamWriter sw = new StreamWriter(address + "\\Menu.ini", true);
            sw.Flush();
            sw.Close();
            StreamReader sr = new StreamReader(address + "\\Menu.ini");
            int i = Menu1.DropDownItems.Count - 1;//从第2个开始载入 
            while (sr.Peek() >= 0) //sr.Peek()返回下一个可用字符，但不使用它
            {
                //StreamReader ReadLine从当前流中读取一行字符，并将数据作为字符串返回
                ToolStripMenuItem menuItem = new ToolStripMenuItem(sr.ReadLine());
                foreach (var item in Menu1.DropDownItems)//todo 判断有问题
                {
                    if (item.ToString() == sr.ReadLine() || sr.ReadLine() == null)
                    {
                        //Screen.Text += item.ToString()+ "\r\n";
                        return;
                    }
                    else
                    {
                        Menu1.DropDownItems.Insert(i, menuItem);
                        i++;
                        menuItem.Click += new EventHandler(NewClick);
                        break;
                    }
                }
            }
            sr.Close();
        }
        //Ini文件中对应的点击事件
        void NewClick(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;
            ShowWindows(menu.Text);//打开对应的窗口
        }
        void ShowWindows(string s)
        {
            if (s == null)
            {
                return;
            }
            Form f = new Form();
            f.MdiParent = this;
            f.Show();
        }
        private void MenuItem_Click(object sender, EventArgs e)
        {
            LoadIni();
            //ShowWindows(openFileDialog1.FileName);
        }
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(address + "\\Menu.ini");
                //如果没打开文件，就不写入||如果有了就不写入
                if (openFileDialog1.FileName == "" || sr.ReadLine() == openFileDialog1.FileName)
                {
                    sr.Close();
                    return;
                }

                sr.Close();
                StreamWriter sw = new StreamWriter(address + "\\Menu.ini", true);
                sw.WriteLine(openFileDialog1.FileName);//写入INI
                sw.Flush();
                sw.Close();

            }
        }
        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            console.Text = BackupTime;
            //FolderBrowserDialog fbd = new FolderBrowserDialog();
            //if (fbd.ShowDialog() == DialogResult.OK)
            //{
            //    //根据GUID生成文件名:乱码类似：ac3b8319-13c8-4fc1-a522-bdf6bde5f00e
            //    //File.Create(fbd.SelectedPath + "\\" + Guid.NewGuid().ToString() + ".txt");//生成文件
            //    //Directory.CreateDirectory(fbd.SelectedPath + "\\" + Guid.NewGuid().ToString());//生成文件夹
            //}
        }
        protected void textBox1_Validating(object sender,CancelEventArgs e)
        {
            try
            {
                int x = Int32.Parse(console.Text);
                errorProvider1.SetError(textBox1, "");
            }
            catch (Exception )
            {
                errorProvider1.SetError(textBox1, "Not an integer value.");
            }
        }
        #region Paint
        private void Main_Paint(object sender, PaintEventArgs e)
        {
            if (Round)
            {
                System.Drawing.Drawing2D.GraphicsPath firstPath = new System.Drawing.Drawing2D.GraphicsPath();
                firstPath.AddEllipse(30, 30, this.Width - 40, this.Height - 40);
                ShowWindows(openFileDialog1.FileName);
                this.Region = new Region(firstPath);
            }
        }
        #endregion
        #region Tools
        private void 浏览器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Process.Start();
            Web w = new Web();
            w.ShowDialog();
        }
        private void cmdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cmd w = new Cmd();
            w.ShowDialog();
        }
        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setting w = new Setting();
            w.ShowDialog();
        }

        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isAutoBackup)
            {
                if (DateTime.Now.ToShortTimeString() == BackupTime)
                {
                    if (Directory.Exists(address+ "\\" + DateTime.Now.Month.ToString()) == false)//指定路径没有文件夹
                    {
                        Directory.CreateDirectory(address + "\\" + DateTime.Now.Month.ToString());//创建文件夹
                    }
                    AutoBackup(filesName, address); 
                }
            }
        }
        //将指定文件拷贝到备份文件夹
        private void AutoBackup(List<string> fromFiles,string ToPath)
        {
            //DirectoryInfo dirInfo = new DirectoryInfo();
            foreach (string item in fromFiles)
            {
                File.Copy(item, ToPath,true);
            }
        }

        private void Main_Leave(object sender, EventArgs e)
        {
            //todo保存设置
        }
    }
}
