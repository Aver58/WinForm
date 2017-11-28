using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aver3
{
    class TestThread : CThread
    {
        public override void Func()
        {
            Form1 f = new Form1();
            //f.console.Text = "2222222";
        }
        //void Main()
        //{
        //    this.Init();
        //}
        private void button1_Click(object sender, EventArgs e)
        {
           // this.Init();
        }
    }
}
