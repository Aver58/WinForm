using Microsoft.Win32;
using System;
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
    public partial class Func : Form
    {
        public Func()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string key = @"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Control Panel";
            RegistryKey reg = Registry.CurrentUser.CreateSubKey(key);
            if (checkBox1.Checked)
            {
                DialogResult Confirm = MessageBox.Show("你确定要禁止修改主页的操作吗!?", "通过注册表来修改Internet Explore的主页", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (Confirm == DialogResult.OK)
                {
                    reg.SetValue("HomePage", 1, RegistryValueKind.DWord);
                }
            }
            else
            {
                
                DialogResult Confirm = MessageBox.Show("你的浏览器主页可以被修改啦！", "恭喜您", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (Confirm == DialogResult.OK)
                {
                    reg.SetValue("HomePage", 0, RegistryValueKind.DWord);
                }
            }
        }
        //修改主页
        void ChangeStartPages()
        {
            string key = @"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main";
            string valueName = "Start Page";
            string value = "http://www.dtan.so";
            DialogResult Confirm = MessageBox.Show("你确定要执行修改主页的操作吗!?", "通过注册表来修改Internet Explore的主页", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (Confirm == DialogResult.OK)
            {
                Registry.SetValue(key, valueName, value);
                MessageBox.Show("你的浏览器主页已被更改为:http://www.dtan.so", "恭喜您", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
