using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aver3.Win
{
    public partial class Astar : Form
    {
        #region 准备工作
        /// <summary>
        /// 当前的模式
        /// </summary>
        enum MyChoose
        {
            wall,
            start,
            destination
        }
        MyChoose mychoose;
        //考虑列表
        List<Astar_Node> Open_List = new List<Astar_Node>();
        //不考虑列表
        List<Astar_Node> Close_List = new List<Astar_Node>();
        //亲爱的按钮们
        Astar_Grid[,] myBtn = new Astar_Grid[20, 20];
        //存储walls的byte
        byte[,] walls = new byte[20, 20];


        Astar_Node strPoint = new Astar_Node();         //起点
        Astar_Node desPoint = new Astar_Node();         //终点

        Astar_Grid myButton = null;                     //用来存储上一次点击的按钮
        byte[,] R = new byte[20, 20];

        public Astar()
        {
            InitializeComponent();
        }

        private void Astar_Load(object sender, EventArgs e)
        {
            //创建20*20个按钮
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    myBtn[i, j] = new Astar_Grid();
                    myBtn[i, j].X = i;
                    myBtn[i, j].Y = j;
                    myBtn[i, j].Size = new Size(20, 20);
                    myBtn[i, j].Location = new Point(i * 20, j * 20);
                    this.Controls.Add(myBtn[i, j]);             //生成按钮
                    myBtn[i, j].MouseDown += Astar_MouseDown;   //挂载事件
                    R[i, j] = 1;
                }
            }
        }

        //鼠标点击事件
        private void Astar_MouseDown(object sender, MouseEventArgs e)
        {
            myButton = (Astar_Grid)sender;//接收对象
            int x = myButton.X;
            int y = myButton.Y;
            //todo 判断没按到按钮上的情况
            if (mychoose == MyChoose.wall)
            {
                if (sender.GetType().Name == "Astar") return;
                myBtn[x, y].BackColor = Color.Black;//界面
                R[x, y] = 0;                        //属性
            }
            else if (mychoose == MyChoose.start)
            {
                myBtn[x, y].BackColor = Color.Transparent;
                myBtn[x, y].BackColor = Color.Green;
                strPoint.x = x;
                strPoint.y = y;
            }
            else if (mychoose == MyChoose.destination)
            {
                myBtn[x, y].BackColor = Color.Transparent;
                myBtn[x, y].BackColor = Color.Red;
                desPoint.x = x;
                desPoint.y = y;
            }
        }
        void Init()
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    R[i, j] = 1;
                    myBtn[i, j].BackColor = Color.Transparent;
                    strPoint = new Astar_Node();
                    desPoint = new Astar_Node();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Init();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            mychoose = MyChoose.wall;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            mychoose = MyChoose.start;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mychoose = MyChoose.destination;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (desPoint == null||strPoint == null)
            {
                MessageBox.Show("请选择起点和终点后再尝试！");
                return;
            }
            //FindeWay(strPoint, desPoint);
            List<Astar_Node> myp = new List<Astar_Node>();  
            myp = FindeWay(strPoint, desPoint);

            foreach (Astar_Node p in myp)
            {
                myBtn[p.x, p.y].BackColor = Color.Yellow;           
            }
            myBtn[desPoint.x, desPoint.y].BackColor = Color.Red;    //终点是红色的 
        }  
        #endregion

        //判断一个点是否为障碍物  
        private bool IsBar(Astar_Node p, byte[,] map)
        {
            if (map[p.y, p.x] == 0) return true;
            return false;
        }

        //判断关闭列表是否包含一个坐标的点  
        private bool IsInCloseList(int x, int y)
        {
            foreach (Astar_Node p in Close_List)
                if (p.x == x && p.y == y)
                    return true;
            return false;
        }
        //判断开启列表是否包含一个坐标的点
        private bool IsInOpenList(int x, int y)
        {
            foreach (Astar_Node p in Open_List)
                if (p.x == x && p.y == y)
                    return true;
            return false;
        }
        //从关闭列表返回对应坐标的点  
        private Astar_Node GetPointFromCloseList(int x, int y)
        {
            foreach (Astar_Node p in Close_List)
                if (p.x == x && p.y == y)
                    return p;
            return null;
        }
        //从开启列表返回对应坐标的点
        private Astar_Node GetPointFromOpenList(int x, int y)
        {
            foreach (Astar_Node p in Open_List)
                if (p.x == x && p.y == y)
                    return p;
            return null;
        }
        
        //计算某个点的G值
        private int GetG(Astar_Node p)
        {
            if (p.father == null) return 0; //该点为起始点
            if (p.x == p.father.x || p.y == p.father.y)
                return p.father.G + 10;     //垂直走的距离是10
            else
                return p.father.G + 14;     //斜着走的距离是14（即根号2）
        }
        //计算某个点的H值
        private int GetH(Astar_Node p, Astar_Node pb)
        {
            return Math.Abs(p.x - pb.x) * 10 + Math.Abs(p.y - pb.y) * 10;
        }
        //从开启列表查找F值最小的节点  
        private Astar_Node GetMinFFromOpenList()
        {
            Astar_Node Pmin = null;
            foreach (Astar_Node p in Open_List)
                if (Pmin == null || Pmin.G + Pmin.H > p.G + p.H)
                    Pmin = p;
            return Pmin;
        }
       
        int GetH(Astar_Node p)
        {
            return Math.Abs(p.x - desPoint.x) * 10 + Math.Abs(p.y - desPoint.y) * 10;
        }
     
        /// <summary>
        /// 检查当前节点附近的节点
        /// </summary>
        /// <param name="now">   当前节点</param>
        /// <param name="map">  障碍</param>
        /// <param name="pa">   起点</param>
        /// <param name="pb">   终点</param>
        private void CheckP8(Astar_Node now, byte[,] map, ref Astar_Node destination)
        {
            for (int x = now.x - 1; x <= now.x + 1; x++)
            {
                for (int y = now.y - 1; y <= now.y + 1; y++)
                {
                    if (  (x >= 0 && x < R.GetLength(0) //排除超过边界的点
                        && y >= 0 && y < R.GetLength(1)) 
                        && !(x == now.x && y == now.y)  //关闭自身的点（自身点总是最小） 
                        && map[x, y] == 1               //排除障碍点
                        && !IsInCloseList(x, y))        //关闭列表中的点  
                    {
                        if (IsInOpenList(x, y))         //在搜寻列表中
                        {
                            Astar_Node tempPoint = GetPointFromOpenList(x, y);
                            int G_new = 0;
                            if (now.x == tempPoint.x || now.y == tempPoint.y)
                                G_new = now.G + 10;
                            else
                                G_new = now.G + 14;

                            if (G_new < tempPoint.G)
                            {
                                Open_List.Remove(tempPoint);
                                tempPoint.father = now;
                                tempPoint.G = G_new;
                                Open_List.Add(tempPoint);
                            }
                        }
                        else                            //不在开启列表中  
                        {
                            Astar_Node tempPoint = new Astar_Node();
                            tempPoint.x = x;
                            tempPoint.y = y;
                            tempPoint.father = now;
                            tempPoint.G = GetG(tempPoint);
                            tempPoint.H = GetH(tempPoint, destination);
                            Open_List.Add(tempPoint);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 寻径调用-主方法
        /// </summary>
        /// <param name="start">起始点</param>
        /// <param name="end">  终点  </param>
        /// <returns></returns>
        public List<Astar_Node> FindeWay(Astar_Node start, Astar_Node end)
        {
            List<Astar_Node> Path = new List<Astar_Node>();
            Open_List.Add(start);
                          
            while (!(IsInOpenList(end.x, end.y) //当终点不在Open_List时        
                || Open_List.Count == 0))       //或者Open_List的数量为0循环(终点在list就停)
            {
                Astar_Node now = GetMinFFromOpenList();
                if (now == null) return null;
                Open_List.Remove(now);
                Close_List.Add(now);
                CheckP8(now, R, ref end);
            }

            Astar_Node p = GetPointFromOpenList(end.x, end.y);
            while (p.father != null)
            {
                Path.Add(p);
                p = p.father;
            }
            return Path;
        }

        
    }
}
