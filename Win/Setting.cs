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
    public partial class Setting : System.Windows.Forms.Form
    {
        private string SelectedNode = "";

        public Setting()
        {
            InitializeComponent();
            string strKeyName = "Custom";//默认
            CreateSetChild(strKeyName);
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            //自动完成文本
            GetChildNodes(textBox1.AutoCompleteCustomSource,treeView1.Nodes);
        }
        //递归获取所以子节点
        TreeNode GetChildNodes(AutoCompleteStringCollection o ,TreeNodeCollection Baba)
        {
            foreach (TreeNode item in Baba)
            {
                o.Add(item.Text);
                if ((!(item.Nodes.Count == 0)))
                {
                    GetChildNodes(o,item.Nodes);
                }
            }
            return null;
        }
        
        //遍历TreeView,查找某个节点
         TreeNode FindNode(TreeNode Nod, string strValue)
        {
            if (Nod == null)
                return null;
            if (Nod.Text == strValue)
                return Nod;

            TreeNode tnRet = null;
            foreach (TreeNode tn in Nod.Nodes)
            {
                tnRet = FindNode(tn, strValue);
                if (tnRet != null) break;
            }
            return tnRet;
        }
      
      
        //搜索功能
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TreeNode TargetNode = null;
            foreach (TreeNode item in treeView1.Nodes)
            {
                TargetNode = FindNode(item, textBox1.Text);
                if (TargetNode != null) break;
            }
            if (TargetNode != null)
            {
                TargetNode.Checked = true;//选中效果无效-好像是AfterSelect的锅
                treeView1.SelectedNode = TargetNode;
                if (TargetNode.Parent != null)
                {
                    TargetNode.Parent.Expand();//展开
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
            System.Windows.Forms.Form f = null;
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
