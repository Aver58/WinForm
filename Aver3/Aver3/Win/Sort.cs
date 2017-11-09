using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aver3.Win
{
    public partial class Sort : Form
    {
        public Sort()
        {
            InitializeComponent();
        }
        string s;
        string[] strings;
        int[] Nums;
        private void Sort_Load(object sender, EventArgs e)
        {
            s = richTextBox1.Text;
            strings = richTextBox1.Text.Split(',');
            Nums = new int[strings.Length];
            for (int i = 0; i < strings.Length; i++)
            {
                Nums[i] = int.Parse(strings[i]);
            }
        }
        void ReStart()
        {
            richTextBox1.Text = s;
            Nums = new int[strings.Length];
            for (int i = 0; i < strings.Length; i++)
            {
                Nums[i] = int.Parse(strings[i]);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ReStart();
            Stopwatch timer = new Stopwatch();
            timer.Start();
            BubbleSort(Nums);
            timer.Stop();
            SortTime(timer);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ReStart();
            Stopwatch timer = new Stopwatch();
            timer.Start();
            BubbleSort2(Nums);
            timer.Stop();
            SortTime(timer);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ReStart();
            Stopwatch timer = new Stopwatch();
            timer.Start();
            BubbleSort3(Nums);
            timer.Stop();
            SortTime(timer);
        }
        void SortTime(Stopwatch sw)
        {
            richTextBox1.Clear();
            richTextBox1.Text += "\r\n排序后为：\r\n";
            for (int i = 0; i < Nums.Length; i++)
            {
                richTextBox1.Text += Nums[i].ToString() + ",";
            }
            richTextBox1.Text += "\r\n耗时为：" + sw.ElapsedMilliseconds.ToString() + "毫秒" + "\r\n";
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }
        //冒泡排序--应该是这样的，按照顺序从最下面一个（数组索引最大）上升到第一个：
        public void BubbleSort(int[] data)
        {
            for (int i = 0; i < data.Length - 1; i++)
            {
                for (int j = 0; j < data.Length - 1 - i; j++)
                {
                    if (data[j] > data[j + 1])
                    {
                        int temp = data[j];
                        data[j + 1] = data[j];
                        data[j] = temp;
                    }
                }
            }
        }
        //冒泡排序--最下底下的元素对应最小索引
        public void BubbleSort2(int[] data)
        {
            for (int i = 0; i < data.Length - 1; i++)
            {
                for (int j = 0; j < data.Length - 1 - i; j++)
                {
                    if (data[j] > data[j + 1])
                    {
                        int temp = data[j];
                        data[j + 1] = data[j];
                        data[j] = temp;
                    }
                }
            }
        }
        //古法冒泡最快
        public static void BubbleSort3(int[] list)
        {
            int temp;
            int i, j ;  //先定义一下要用的变量
            for (i = 0; i < list.Length - 1; i++)
            {
                for (j = i + 1; j < list.Length; j++)
                {
                    if (list[i] > list[j]) //如果第二个小于第一个数
                    {
                        //交换两个数的位置，在这里你也可以单独写一个交换方法，在此调用就行了
                        temp = list[i]; //把大的数放在一个临时存储位置
                        list[i] = list[j]; //然后把小的数赋给前一个，保证每趟排序前面的最小
                        list[j] = temp; //然后把临时位置的那个大数赋给后一个
                    }
                }
            }
            //return list;
        }
        //Func<int[], int[]> fun3 = new Func<int[], int[]>(BubbleSort3);
        //Action<int[]> action3 = new Action<int[]>(BubbleSort3);

        //传不同方法
        void SortTime1(Func<int[], int[]> fun)
        {
            ReStart();
            Stopwatch timer = new Stopwatch();
            timer.Start();
            //Func< BubbleSort2>;
            //fun.Invoke();
            timer.Stop();
            richTextBox1.Clear();
            richTextBox1.Text += "\r\n排序后为：\r\n";
            for (int i = 0; i < Nums.Length; i++)
            {
                richTextBox1.Text += Nums[i].ToString() + ",";
            }
            richTextBox1.Text += "\r\n耗时为：" + timer.ElapsedMilliseconds.ToString() + "毫秒" + "\r\n";
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }
        private void button13_Click(object sender, EventArgs e)
        {
            ReStart();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReStart();
            Stopwatch timer = new Stopwatch();
            timer.Start();
            SelectionSort(Nums);
            timer.Stop();
            SortTime(timer);
        }
        ///选择排序
        int min;
        void SelectionSort(int[] data)
        {
            for (int i = 0; i < data.Length - 1; i++)     //假设 i 下标就是最小的数
            {                                             //记录我认为最小的数的下标
                min = i;                                  //这里只是找出这一趟最小的数值并记录下它的下标
                for (int j = i+1; j < data.Length; j++)
                {                                         //说明我们认为的最小值，不是最小
                    if (data[min] > data[j])              //这里大于号是升序(大于是找出最小值) 小于是降序(小于是找出最大值)
                    {
                        min = j;                          //更新这趟最小(或最大)的值 (上面要拿这个数来跟后面的数继续做比较)
                    }                                     //记下它的下标
                }
                 int temp = data[i];                      //最后把最小的数与第一的位置交换
                 data[i] = data[min];                     //把第一个原先认为是最小值的数,临时保存起来
                 data[min] = temp;                        //把最终我们找到的最小值赋给这一趟的比较的第一个位置
                                                          //把原先保存好临时数值放回这个数组的空地方，  保证数组的完整性
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ReStart();
            Stopwatch timer = new Stopwatch();
            timer.Start();
            InsertSort(Nums);
            timer.Stop();
            SortTime(timer);
        }
        /// <summary>
        /// 插入排序
        ///插入排序在大概有序时速度会快很多,比如:3,1,7,4,5,9,10,15,12
        ///这种,你会发现大概上已经升序了,这时插入快,
        ///平时速度跟其他排序是差不多的.
        /// </summary>
        void InsertSort(int[] arr)
        {
            //⒈ 从第一个元素开始，该元素可以认为已经被排序
            //⒉ 取出下一个元素，在已经排序的元素序列中从后向前扫描
            //⒊ 如果该元素（已排序）大于新元素，将该元素移到下一位置
            //⒋ 重复步骤3，直到找到已排序的元素小于或者等于新元素的位置
            //⒌ 将新元素插入到下一位置中
            //⒍ 重复步骤2~5
           
            for (int i = 1; i < arr.Length; i++)
            { //插入排序是把无序列的数一个一个插入到有序的数
              //先默认下标为0这个数已经是有序
                int insertVal = arr[i];  //首先记住这个预备要插入的数
                int insertIndex = i - 1; //找出它前一个数的下标（等下 准备插入的数 要跟这个数做比较）

                //如果这个条件满足，说明，我们还没有找到适当的位置
                while (insertIndex >= 0 && insertVal < arr[insertIndex])   //这里小于是升序，大于是降序
                {
                    arr[insertIndex + 1] = arr[insertIndex];   //同时把比插入数要大的数往后移
                    insertIndex--;      //指针继续往前移，等下插入的数也要跟这个指针指向的数做比较         
                }
                
                arr[insertIndex + 1] = insertVal;//插入(这时候给insertVal找到适当位置)
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ReStart();
            Stopwatch timer = new Stopwatch();
            timer.Start();
            ShellSort(Nums);
            timer.Stop();
            SortTime(timer);
        }
        /// <summary>
        /// Shell Sort
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        void ShellSort<T>(T[] array) where T : IComparable
        {
            int length = array.Length;
            for (int h = length / 2; h > 0; h = h / 2)
            {
                //here is insert sort
                for (int i = h; i < length; i++)
                {
                    T temp = array[i];
                    if (temp.CompareTo(array[i - h]) < 0)
                    {
                        for (int j = 0; j < i; j += h)
                        {
                            if (temp.CompareTo(array[j]) < 0)
                            {
                                temp = array[j];
                                array[j] = array[i];
                                array[i] = temp;
                            }
                        }
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            //思路：将CS文件保存成txt
            //正则出相关语句（特定符合或者）
        }
        void CSBuild()
        {

        }
        void SendPropertyString(SqlClassMaker.DataTable F)
        {

        }
    }
}
