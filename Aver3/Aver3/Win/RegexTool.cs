using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aver3.Win
{
    public partial class RegexTool : Form
    {
        string RegexFile = Application.StartupPath + "\\RegexExpressions.json";
        List<string> titles = new List<string>();   //Titles
        List<string> codes = new List<string>();    //RegexCodes
        public RegexTool()
        {
            InitializeComponent();
        }
        private void Regex_Load(object sender, EventArgs e)
        {
            WriteJson();
            //从文件中读取对象obj的步骤：//直接从文件中反序列化到对象即可
            // 获取当前程序所在路径，并将要创建的文件命名为RegexExpressions.json 
            if (!File.Exists(RegexFile))  // 判断是否已有相同文件 
            {
                FileStream fs = new FileStream(RegexFile, FileMode.Create, FileAccess.ReadWrite);
                fs.Close();
            }
            try
            {
                string json = File.ReadAllText(RegexFile);  
                object obj = JsonConvert.DeserializeObject(json);
                List<EasyRegex> er = JsonConvert.DeserializeObject<List<EasyRegex>>(json);//反序列化成数组
                foreach (var item in er)    //赋值
                {
                    titles.Add(item.Titles);
                    codes.Add(item.RegexCode);
                }
            }
            catch (Exception s)
            {
                MessageBox.Show(s.Message);
                throw;
            }
            foreach (var item in titles)
            {
                treeView1.Nodes.Add(item);
            }
        }
        public class EasyRegex
        {
            public string Titles;//标题

            public string RegexCode;//正则表达式
        }
        void WriteJson()
        {
            //Json的段落：
                
            List<EasyRegex> er = new List<EasyRegex>() {
                //检查文本中是否含有指定的特征词
                //在字符类中，字符的重复和出现顺序并不重要。[dabaaabcc]与[abc]是相同的
                //在字符类之外，短横线没有特殊含义。正则表达式a-z，表示匹配字符串“以a开头，然后是一个短横线，以z结尾”。
                //？与{0,1}相同，比如，colou?r表示匹配colour或者color
                //*与{0,}相同。比如，.*表示匹配任意内容
                //+与{1，}相同。比如,\w+表示匹配一个词。其中”一个词”表示由一个或一个以上的字符组成的字符串，比如_var或者AccountName1.
                //选择匹配 |
                new EasyRegex(){Titles = "任何不是“XXX”的字符",RegexCode = "[^XXX]"},
                new EasyRegex(){Titles = "任何不是字母也不是数字的字符",RegexCode = "[^a-zA-Z0-9]或者[\\W]"},
                new EasyRegex(){Titles = "任何不为“^”的字符",RegexCode = "[^\\^]"},
                new EasyRegex(){Titles = "任何一个非数字字符",RegexCode = "\\D或者[^0-9]"},
                new EasyRegex(){Titles = "XX和XX中间夹着任何一个字符",RegexCode = "XX.XX"},
                new EasyRegex(){Titles = "重复字符XX{个数}",RegexCode = "XX{n}"},
                new EasyRegex(){Titles = "重复字符XX{个数，个数}",RegexCode = "XX{n,m}"},
              

                //找出文中匹配特征词的位置
                new EasyRegex(){Titles = "一个空字符（空格，制表符，回车或者换行）",RegexCode = "[\\s]"},

                 //从文本中提取信息，比如：字符串的子串
                new EasyRegex(){Titles = "一个空字符（空格，制表符，回车或者换行）",RegexCode = "[\\s]反义词是：\\S表示匹配一个非空字符"},
                new EasyRegex(){Titles = "找到双引号,其中字符串可能包含任意个字符",RegexCode = " \".*\" "},
                new EasyRegex(){Titles = "一个空行",RegexCode = " ^& "},
                new EasyRegex(){Titles = "在一对双引号中间不再包含其他的双引号",RegexCode = " \"[^\"]*\" "},
                new EasyRegex(){Titles = "匹配长度为73的一行",RegexCode = " ^.{73,}$ "},//将^和$作为文本的开始符号和结束符号
                new EasyRegex(){Titles = "匹配长度为73的一行",RegexCode = " ^.{73,}$ "},

                 //修改文本
                new EasyRegex(){Titles = "任何不是“XXX”的字符",RegexCode = "[^XXX]"},
                new EasyRegex(){Titles = "任何不是“XXX”的字符",RegexCode = "[^XXX]"},
                new EasyRegex(){Titles = "任何不是“XXX”的字符",RegexCode = "[^XXX]"},
            };
            string json = JsonConvert.SerializeObject(er, Formatting.Indented);//有缩进输出 
            //写入Json
            File.WriteAllText(RegexFile,json);

        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            for (int i = 0; i < titles.Count-1; i++)
            {
                if (treeView1.SelectedNode.Text == titles[i])
                {
                    textBox2.Text = codes[i];
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
      
    }
}
