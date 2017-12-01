using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Aver3.Win.Setting
{
    public class IniFile
    {
        public static string AppDirectory { get { return AppDomain.CurrentDomain.SetupInformation.ApplicationBase; } }
        string iniFile = AppDirectory + "ini\\";
        config configs = new config();

        /// <summary>
        /// 加载Ini
        /// </summary>
        void LoadIni()
        {
            try//读取文件、修改值、保存
            {
                string json = File.ReadAllText(iniFile);
                object obj = JsonConvert.DeserializeObject(json);
                config c = JsonConvert.DeserializeObject<config>(json);//反序列化成数组
                configs.isAutoBackup = c.isAutoBackup;
                configs.BackupTime = c.BackupTime;
                configs.address = c.address;
                configs.backupFile = c.backupFile;
            }
            catch (Exception d)
            {
                MessageBox.Show(d.Message);
            }
        }
        /// <summary>
        /// 保存Ini
        /// </summary>
        void SaveIni()
        {
            config c = new config();
            string json = JsonConvert.SerializeObject(c, Formatting.Indented);  //有缩进输出 
            File.WriteAllText(iniFile, json);                                   //写入Json
        }
        #region AutoUpdate
        public void CheckBackupTime()
        {
            if (configs.isAutoBackup)
            {
                if (DateTime.Now.ToShortTimeString() == configs.BackupTime)
                {
                    if (Directory.Exists(IniFile.AppDirectory + "\\AutoBackup\\" + DateTime.Now.Month.ToString()) == false)//指定路径没有文件夹
                    {
                        Directory.CreateDirectory(configs.address + "\\" + DateTime.Now.Month.ToString());//创建文件夹
                    }
                    //AutoBackup(configs.filesName, configs.address);
                    //将指定文件拷贝到备份文件夹
                    foreach (string item in configs.backupFile)
                    {
                        File.Copy(item, configs.address, true);
                    }
                }
            }
        }
       
        //private void AutoBackup(List<string> fromFiles, string ToPath)
        //{
        //    //DirectoryInfo dirInfo = new DirectoryInfo();
        //    foreach (string item in fromFiles)
        //    {
        //        File.Copy(item, ToPath, true);
        //    }
        //}
        #endregion
    }
}
