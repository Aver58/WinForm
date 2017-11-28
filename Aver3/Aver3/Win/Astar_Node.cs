using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aver3.Win
{
    public class Astar_Node
    {
        public int x;
        public int y;
        public int G;//ToFather's value从起始点走的距离
        public int H;//你离目的地的距离
                     //用F=G+H表示你周围的格子，你要去目的地花的代价。 越小越好
                     //选个F最小的格子，移出OpenList，存到CloseList去。
         //将方块添加到open列表中，该列表有最小的和值。且将这个方块称为S吧。
         //将S从open列表移除，然后添加S到closed列表中。
         //对于与S相邻的每一块可通行的方块T：
            //如果T在closed列表中：不管它。
            //如果T不在open列表中：添加它然后计算出它的和值。
            //如果T已经在open列表中：当我们使用当前生成的路径到达那里时，检查F 和值是否更小。如果是，更新它的和值和它的前继。
        public Astar_Node father;
        public Astar_Node()
        {
        }
        public Astar_Node(int x0, int y0, int G0, int H0, Astar_Node F)
        {
            x = x0;
            y = y0;
            G = G0;
            H = H0;
            father = F;
        }

        public override string ToString()
        {
            return "{" + x.ToString() + "," + y.ToString() + "}";
        }
    }
}
