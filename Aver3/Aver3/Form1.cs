using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
namespace Aver3
{
    public partial class Main : Form
    {
        string address = "D:";//保存文件路径
        bool Round = false;//界面按钮测试
        //窗体界面的初始化
        public Main()
        {
            InitializeComponent();
        }
        //窗体程序的初始化Init
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        #region ButtonStudy
        private void button1_Click(object sender, EventArgs e)
        {
            console.Text = "";
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
        }
       

        #endregion
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
                    if (item.ToString()== sr.ReadLine()||sr.ReadLine()==null)
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
                Constants.MShow("");
                
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
        #endregion


    }
}
