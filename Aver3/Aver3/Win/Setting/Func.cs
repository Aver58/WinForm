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
    public partial class Func : System.Windows.Forms.Form
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
        /// <summary>
        /// 设置应用程序开机自动运行
        /// </summary>
        /// <param name="fileName">应用程序的文件名</param>
        /// <param name="isAutoRun">是否自动运行，为false时，取消自动运行</param>
        /// <exception cref="System.Exception">设置不成功时抛出异常</exception>
        public static void SetAutoRun(string fileName, bool isAutoRun)
        {
            RegistryKey reg = null;
            try
            {
                if (!System.IO.File.Exists(fileName))
                    throw new Exception("该文件不存在!");
                String name = fileName.Substring(fileName.LastIndexOf(@"\") + 1);
                reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (reg == null)
                    reg = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                if (isAutoRun)
                    reg.SetValue(name, fileName);
                else
                    reg.SetValue(name, false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                if (reg != null)
                    reg.Close();
            }
        }
        //开机启动
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            string MyName = Application.ProductName;//本程序
            if (checkBox2.Checked)
            {
                SetAutoRun(MyName, true);
            }
            else
            {
                SetAutoRun(MyName, false);
            }
        }
        /// <summary>
        /// 关联文件
        /// </summary>
        /// <param name="FilePathString">应用程序路径</param>
        /// <param name="FileTypeName">文件类型</param>
        public static void SaveReg(string FilePathString, string FileTypeName)
        {
            RegistryKey RegKey = Registry.ClassesRoot.OpenSubKey("", true);              //打开注册表
            RegistryKey VRPkey = RegKey.OpenSubKey(FileTypeName, true);
            if (VRPkey != null)
            {
                RegKey.DeleteSubKey(FileTypeName, true);
            }
            RegKey.CreateSubKey(FileTypeName);
            VRPkey = RegKey.OpenSubKey(FileTypeName, true);
            VRPkey.SetValue("", "Exec");
            VRPkey = RegKey.OpenSubKey("Exec", true);
            if (VRPkey != null) RegKey.DeleteSubKeyTree("Exec");         //如果等于空就删除注册表DSKJIVR
            RegKey.CreateSubKey("Exec");
            VRPkey = RegKey.OpenSubKey("Exec", true);
            VRPkey.CreateSubKey("shell");
            VRPkey = VRPkey.OpenSubKey("shell", true);                      //写入必须路径
            VRPkey.CreateSubKey("open");
            VRPkey = VRPkey.OpenSubKey("open", true);
            VRPkey.CreateSubKey("command");
            VRPkey = VRPkey.OpenSubKey("command", true);
            string _PathString = "\"" + FilePathString + "\" \"%1\"";
            VRPkey.SetValue("", _PathString);                                    //写入数据
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //string FileTypeName = comboBox1.Text;
            //string FilePathString = comboBox1.Text;
            //SaveReg(FilePathString, FileTypeName);
        }

     
    }
}
