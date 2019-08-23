using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aver3
{
    /// <summary>
    /// 线程类
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    abstract class CThread
    {
        Thread thread = null;
        public abstract void Func();//抽象方法
        public void Init()
        {
            if (thread == null)
            {
                thread = new Thread(Func);
            }
            thread.Start();
        }
    }
}
