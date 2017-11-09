using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aver3.Win.Setting
{
    public partial class Custom : System.Windows.Forms.Form
    {
        public Custom()
        {
            InitializeComponent();
            GetMyComputerInfo();
        }
        void GetMyComputerInfo()//获取系统环境信息
        {
            listView1.View = View.Details;      //设置控件显示方式
            listView1.GridLines = true;         //是否显示网格
            listView1.Columns.Add("环境变量", 150, HorizontalAlignment.Left);
            listView1.Columns.Add("变量值", 150, HorizontalAlignment.Left);
            ListViewItem myInfo;
            foreach (DictionaryEntry item in Environment.GetEnvironmentVariables())
            {
                myInfo = new ListViewItem(item.Key.ToString(), 0);
                myInfo.SubItems.Add(item.Value.ToString());        //添加子项
                listView1.Items.Add(myInfo);                       //将子项集合添加到控件中
            }
        }

    }
}
