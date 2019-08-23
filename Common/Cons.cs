using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aver3
{
    public static class Cons
    {
        /// <summary>
        /// 执行外部程序
        /// </summary>
        /// <param name="strCmdFilePath"></param>
        /// <param name="strFilePath"></param>
        public static void CommandFile(string strCmdFilePath, string strFilePath)
        {
            try
            {
                //所有文件路径前后加引号，避免路径中出现空格引起问题
                strCmdFilePath = "\"" + strCmdFilePath + "\"";
                strFilePath = "\"" + strFilePath + "\"";

                //参数设定
                Process proScriptBrush = new Process();
                proScriptBrush.StartInfo.FileName = strCmdFilePath;
                proScriptBrush.StartInfo.RedirectStandardInput = true;
                proScriptBrush.StartInfo.RedirectStandardOutput = true;
                proScriptBrush.StartInfo.RedirectStandardError = true;
                proScriptBrush.StartInfo.CreateNoWindow = true;
                proScriptBrush.StartInfo.UseShellExecute = false;

                //开始
                proScriptBrush.Start();
                proScriptBrush.StandardInput.WriteLine(strFilePath);
            }
            catch (Exception Err)
            {
                MessageBox.Show(Err.ToString());
            }
        }
        /// <summary>
        /// 拖进文件修改鼠标
        /// </summary>
        public static void DragEffect(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        /// <summary>
        /// C# 打开指定目录并定位到文件
        /// </summary>
        /// <param name="fileFullName"></param>
        public static void OpenFolderAndSelectFile(String fileFullName)
        {
            ProcessStartInfo psi = new ProcessStartInfo("Explorer.exe");
            psi.Arguments = "/e,/select," + fileFullName;
            Process.Start(psi);
        }
    }
}
