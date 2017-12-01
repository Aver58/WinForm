using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aver3.Win.Setting
{
    public class config
    {
        public bool isAutoBackup { get; set; }
        public string BackupTime { get; set; }     //自动备份时间
        public string address { get; set; }        //自动保存文件路径
        public List<string> backupFile { get; set; }//需要自动保存的文件名
        public List<string> OpenList  { get; set; }//打开过的文件名

    }
}
