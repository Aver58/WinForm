using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aver3.Win.Setting
{
    public partial class Backup : System.Windows.Forms.Form
    {
        Form1.config configs = new Form1.config();
        public Backup()
        {
            InitializeComponent();
            string Times = configs.BackupTime;                           //AutoBackup的时间
            dateTimePicker1.Value = DateTime.Parse(Times);
            textBox2.Text = configs.address;                             //Topath
            checkBox1.Checked = configs.isAutoBackup;
        }


        //时间检测
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            configs.BackupTime = dateTimePicker1.Value.ToShortTimeString();
            //Application.SetSuspendState(PowerState.Hibernate, false, true);//休眠命令
        }
        //自动备份
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            configs.isAutoBackup = checkBox1.Checked;
        }

        //浏览文件夹--选择目标路径
        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                configs.address = fbd.SelectedPath;
                textBox2.Text = configs.address;
                //根据GUID生成文件名:乱码类似：ac3b8319-13c8-4fc1-a522-bdf6bde5f00e
                //File.Create(fbd.SelectedPath + "\\" + Guid.NewGuid().ToString() + ".txt");//生成文件
                //Directory.CreateDirectory(fbd.SelectedPath + "\\" + Guid.NewGuid().ToString());//生成文件夹
            }
        }
        //拖到指定文件夹
        private void textBox2_DragEnter(object sender, DragEventArgs e)
        {
            Cons.DragEffect(sender, e);
        }
        private void textBox2_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);//获取拖入文件的文件名、加入listbox
            foreach (string file in files)
            {
                textBox2.Text = Path.GetDirectoryName(file);
            }
        }
        //浏览文件--添加需要备份的文件
        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath;//指定路径
            openFileDialog1.Title = "请选择导入xml文件";
            openFileDialog1.Filter = "cs文件|*.cs";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                configs.filesName.Add(openFileDialog1.FileName);
                listBox1.Items.Add(openFileDialog1.FileName);
                //listBox1.SaveFile(Main.address + "\\Aver\\", RichTextBoxStreamType.RichText);
            }
        }
        //修改拖入效果
        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            Cons.DragEffect(sender, e);
        }
        //添加拖入文件路径
        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
             string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);//获取拖入文件的文件名、加入listbox
             foreach (string file in files)
             {
                 if (Path.GetExtension(file) == ".txt")  //判断文件类型，只接受txt文件
                 {
                    listBox1.Items.Add(file);
                    //Form1.SaveSetting();
                 }
                else
                {
                    MessageBox.Show("最好是Txt！");
                }
             }
        }
        //删除
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }
        //双击打开指定目录并定位到文件
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            string fileFullName = listBox1.SelectedItem.ToString();
            Cons.OpenFolderAndSelectFile(fileFullName);
        }
        //右键弹出菜单
        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentIndex = e.Y / 12;
                if (listBox1.SelectedItem != null && currentIndex != listBox1.SelectedIndex)
                {
                    listBox1.SetSelected(listBox1.SelectedIndex, false);
                }
                listBox1.SetSelected(currentIndex, true);
            }
        }
    }
}
