using Aver3.Win.Setting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aver3
{
    public partial class Setting : Form
    {
        private string SelectedNode = "";

        public Setting(string strKeyName = "Custom")
        {
            InitializeComponent();
            CreateSetChild(strKeyName);
        }

        private void Setting_Load(object sender, EventArgs e)
        {

            //自动完成文本
            textBox1.AutoCompleteCustomSource.Add(GetChildNodes(treeView1.Nodes).ToString());     //添加字段搜索
        }
        //递归获取子节点
        TreeNode GetChildNodes(TreeNodeCollection Baba)
        {
            foreach (TreeNode item in Baba)
            {
                if ((!(item.Nodes.Count == 0)))
                {
                    GetChildNodes(item.Nodes);
                }
                return item;
            }
            return null;
        }
        //遍历TreeView,查找某个节点
        private TreeNode FindNode(TreeNode tnParent, string strValue)
        {
            if (tnParent == null)
                return null;
            if (tnParent.Text == strValue)
                return tnParent;

            TreeNode tnRet = null;
            foreach (TreeNode tn in tnParent.Nodes)
            {
                tnRet = FindNode(tn, strValue);
                if (tnRet != null) break;
            }
            return tnRet;
        }
      
      
        //搜索功能
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            foreach (TreeNode item in treeView1.Nodes)
            {
                if (textBox1.Text == item.Text)
                {
                    if (item.Parent!=null)
                    {
                        item.Parent.Expand();//展开
                    }
                    treeView1.SelectedNode = item;
                    //item.Checked = true;//选中效果无效-好像是AfterSelect的锅
                }
            }
        }

        //现在后加载页面
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)//会让选中效果失效？
        {
            //Panel p = (Panel)GetType().GetField(PanelName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);//反射获取控件
            TreeView tv = (TreeView)sender;
            TreeNode treeNode = tv.SelectedNode;
            if (treeNode.Text != null)
               { CreateSetChild(treeNode.Text.ToString()); }
        }

        private void CreateSetChild(string strName)
        {
            Form f = null;
            //string strTitle = "";
            switch (strName)
            {
                case "Custom":
                    {
                        f = new Custom();
                        //strTitle = "Custom";
                        break;
                    }
                case "Backup":
                    {
                        f = new Backup();
                        //strTitle = "Backup";
                        break;
                    }
                case "Func":
                    {
                        f = new Func();
                        //strTitle = "Func";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            if (f != null && SelectedNode != strName)
            {
                f.TopLevel = false;     //取消顶级窗体
                SelectedNode = strName;
                MainPanel.Controls.Clear();
                MainPanel.Controls.Add(f);
                f.Show();               //显示窗口
            }
        }

       
    }
}
