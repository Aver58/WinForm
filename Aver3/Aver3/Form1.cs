using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Net;
using Aver3.Win;
using Newtonsoft.Json;
namespace Aver3
{
    public partial class Form1 : Form
    {
        string ConfigFile = Application.StartupPath + "\\Config.ini";
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
        public Form1()
        {
            InitializeComponent();
        }
        //窗体程序的初始化Init
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(ConfigFile))  // 判断是否已有相同文件 
            {
                //FileStream fs = new FileStream(ConfigFile, FileMode.Create, FileAccess.ReadWrite);
                //fs.Close();
                //不存在文件先不处理
                Console.WriteLine(111);
            }
            else
            {
                try//读取文件、修改值、保存
                {
                    string json = File.ReadAllText(ConfigFile);
                    object obj = JsonConvert.DeserializeObject(json);
                    config c = JsonConvert.DeserializeObject<config>(json);//反序列化成数组
                         configs.isAutoBackup = c.isAutoBackup;
                         configs.BackupTime = c.BackupTime;
                         configs.address = c.address;
                         configs.filesName = c.filesName;
                         configs.Round = c.Round;
                }
                catch (Exception d)
                {
                    MessageBox.Show(d.Message);
                }
            }
        }
        public class config
        {
            public bool isAutoBackup { get; set; }
            public string BackupTime { get; set; }     //自动备份时间
            public string address { get; set; }        //自动保存文件路径
            public List<string> filesName { get; set; }//需要自动保存的文件名
            public bool Round { get; set; }            //界面按钮测试
        }
        private void button1_Click(object sender, EventArgs e)
        {
            console.Text = configs.BackupTime;
        }
        //errorProvider错误提示
        protected void textBox1_Validating(object sender,CancelEventArgs e)
        {
            try
            {
                int x = Int32.Parse(textBox1.Text);
                if (x != 11)
                {
                    errorProvider1.SetError(textBox1, "11");
                }
                else
                {
                    errorProvider1.Clear();
                }
            }
            catch (Exception)
            {
                errorProvider1.SetError(textBox1, "Not an integer value.");
            }
        }
        #region Paint
        private void Main_Paint(object sender, PaintEventArgs e)
        {
            if (configs.Round)
            {
                System.Drawing.Drawing2D.GraphicsPath firstPath = new System.Drawing.Drawing2D.GraphicsPath();
                firstPath.AddEllipse(30, 30, this.Width - 40, this.Height - 40);
                ShowWindows(openFileDialog1.FileName);
                this.Region = new Region(firstPath);
            }
        }
        #endregion
        config configs = new config();
        #region AutoUpdate
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (configs.isAutoBackup)
            {
                if (DateTime.Now.ToShortTimeString() == configs.BackupTime)
                {
                    if (Directory.Exists(configs.address + "\\" + DateTime.Now.Month.ToString()) == false)//指定路径没有文件夹
                    {
                        Directory.CreateDirectory(configs.address + "\\" + DateTime.Now.Month.ToString());//创建文件夹
                    }
                    AutoBackup(configs.filesName, configs.address);
                }
            }
        }
        //将指定文件拷贝到备份文件夹
        private void AutoBackup(List<string> fromFiles, string ToPath)
        {
            //DirectoryInfo dirInfo = new DirectoryInfo();
            foreach (string item in fromFiles)
            {
                File.Copy(item, ToPath, true);
            }
        }
        #endregion
        #region Menu
        //LoadIni读取INI文件，并将信息载入菜单
        void LoadIni()
        {
            StreamWriter sw = new StreamWriter(configs.address + "\\Menu.ini", true);
            sw.Flush();
            sw.Close();
            StreamReader sr = new StreamReader(configs.address + "\\Menu.ini");
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
            System.Windows.Forms.Form f = new System.Windows.Forms.Form();
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
                StreamReader sr = new StreamReader(configs.address + "\\Menu.ini");
                //如果没打开文件，就不写入||如果有了就不写入
                if (openFileDialog1.FileName == "" || sr.ReadLine() == openFileDialog1.FileName)
                {
                    sr.Close();
                    return;
                }
                sr.Close();
                StreamWriter sw = new StreamWriter(configs.address + "\\Menu.ini", true);
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
        #region SendMsgSMS  Abandon
        //private string url = "http://utf8.sms.webchinese.cn/?";
        //private string strUid = "Uid=";
        //private string strKey = "&key=*******************"; //这里*代表秘钥，由于从长有点麻烦，就不在窗口上输入了  
        //private string strMob = "&smsMob=";
        //private string strContent = "&smsText=";
        //private void button2_Click(object sender, EventArgs e)
        //{
        //    if (textBox1.Text.ToString().Trim() != "" && textBox2.Text.ToString().Trim() != "" && textBox3.Text.ToString() != null)
        //    {
        //        url = url + strUid + textBox1.Text + strKey + strMob + textBox2.Text + strContent + textBox3.Text;
        //        string Result = GetHtmlFromUrl(url);
        //        MessageBox.Show(Result);
        //    }
        //}
        //public string GetHtmlFromUrl(string url)
        //{
        //    string strRet = null;
        //    if (url == null || url.Trim().ToString() == "")
        //    {
        //        return strRet;
        //    }
        //    string targeturl = url.Trim().ToString();
        //    try
        //    {
        //        HttpWebRequest hr = (HttpWebRequest)WebRequest.Create(targeturl);
        //        hr.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
        //        hr.Method = "GET";
        //        hr.Timeout = 30 * 60 * 1000;
        //        WebResponse hs = hr.GetResponse();
        //        Stream sr = hs.GetResponseStream();
        //        StreamReader ser = new StreamReader(sr, Encoding.Default);
        //        strRet = ser.ReadToEnd();
        //    }
        //    catch (Exception ex)
        //    {
        //        strRet = null;
        //        MessageBox.Show(ex.ToString());
        //    }
        //    return strRet;
        //}
        #endregion
        //拖放文件直接打开
        private void Main_DragEnter(object sender, DragEventArgs e)
        {
            Cons.DragEffect(sender, e);
        }
        private void Main_DragDrop(object sender, DragEventArgs e)
        {
            string FilePath;
            FilePath = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();//这个地方得到的就是拖放的文件路径
            Process.Start(FilePath);
        }
        private void Main_Leave(object sender, EventArgs e)
        {
        }
        public void SaveSetting()
        {
            //todo保存设置
            if (!File.Exists(ConfigFile))// 判断是否已有相同文件 
            {
                SaveIni();
            }
            else
            {
                //如果存在，直接删掉，重新保存数据
                File.Delete(ConfigFile);
                SaveIni();
            }
        }
         void SaveIni()
        {
            FileStream fs = new FileStream(ConfigFile, FileMode.Create, FileAccess.ReadWrite);
            config c = new config
            {
                isAutoBackup = configs.isAutoBackup,
                BackupTime = configs.BackupTime,
                address = configs.address,
                filesName = configs.filesName,
                Round = configs.isAutoBackup
            };
            string json = JsonConvert.SerializeObject(c, Formatting.Indented); //有缩进输出 
            File.WriteAllText(ConfigFile, json);                               //写入Json
            fs.Close();
        }
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
        private void iP工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IPTools w = new IPTools();
            w.ShowDialog();
        }
        private void sortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sort w = new Sort();
            w.ShowDialog();
        }
        private void regex工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegexTool w = new RegexTool();
            w.ShowDialog();
        }
        private void spiderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Spider w = new Spider();
            w.ShowDialog();
        }
        private void hackerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrojanHorse w = new TrojanHorse();
            w.ShowDialog();
        }
        private void snifferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sniffer w = new Sniffer();
            w.ShowDialog();
        }
        #endregion
    }
}
