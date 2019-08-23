using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aver3
{
    public partial class Web : System.Windows.Forms.Form
    {
        public Web()
        {
            InitializeComponent();
        }

        private void 浏览器_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;//默认选择第一个
            webBrowser1.GoHome();//浏览器默认主页
        }
        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ////下面是你的执行操作代码
            //try
            //{
            //    textBox1.Text = webBrowser1.Document.GetElementById("password").ToString();
            //    webBrowser1.Document.GetElementById("password").SetAttribute("value", "Welcome123");

            //}
            //catch (Exception m)
            //{
            //    MessageBox.Show(m.ToString());
            //    return;
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(comboBox1.SelectedItem.ToString());
        }
        //private void button2_Click(object sender, EventArgs e)
        //{
        //   
        //}


    }
}
