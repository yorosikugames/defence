using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace defense
{
    public class MyPoint : SettlersEngine.IPathNode<Object>
    {
        public Int32 X { get; set; }
        public Int32 Y { get; set; }
        public Boolean IsWall { get; set; }
        public Block LinkedBlock;

        public bool IsWalkable(Object unused)
        {
            return !IsWall;
        }
    }

    public class MySolver<TPathNode, TUserContext> : SettlersEngine.SpatialAStar<TPathNode, TUserContext> where TPathNode : SettlersEngine.IPathNode<TUserContext>
    {
        protected override Double Heuristic(PathNode inStart, PathNode inEnd)
        {
            return Math.Abs(inStart.X - inEnd.X) + Math.Abs(inStart.Y - inEnd.Y);
        }

        protected override Double NeighborDistance(PathNode inStart, PathNode inEnd)
        {
            return Heuristic(inStart, inEnd);
        }

        public MySolver(TPathNode[,] inGrid)
            : base(inGrid)
        {
        }
    }

    public class Map
    {
        private static MyPoint[,] grid = new MyPoint[450, 450];
        private List<Enemy> EnemyList = new List<Enemy>();
        private List<Block> BlockList = new List<Block>();
        MySolver<MyPoint, Object> aStar = null;
        public Boolean InvalidatePath = false;
        private Block ActivatedBlock;

        public Map()
        {
            for (int x = 0; x < 450; x++)
                for (int y = 0; y < 450; y++)
                    grid[x, y] = new MyPoint { X = x, Y = y, IsWall = false };
            aStar = new MySolver<MyPoint, Object>(grid);
        }

        public IEnumerable<MyPoint> getNextPosList(MyPoint curPos, MyPoint toPoint, bool force)
        {
            if (InvalidatePath || force)
                return aStar.Search(new Point(curPos.X, curPos.Y), new Point(toPoint.X, toPoint.Y), null);

            return null;
        }

        public Block getElemAt(int x, int y)
        {
            if (!(0 <= x && x < 450)) ActivatedBlock = null;
            if (!(0 <= y && y < 450)) ActivatedBlock = null;

            ActivatedBlock = grid[x, y].LinkedBlock;

            return ActivatedBlock;
        }

        public void setElem(Block block)
        {
            BlockList.Add(block);

            for (int x = block.x - block.size; x <= block.x + block.size; x++)
            {
                for (int y = block.y - block.size; y <= block.y + block.size; y++)
                {
                    if (!(0 <= x && x < 450)) continue;
                    if (!(0 <= y && y < 450)) continue;

                    grid[x, y].LinkedBlock = block;
                    grid[x, y].IsWall = true;
                }
            }

            InvalidatePath = true;
        }

        public void registerEnemy(Enemy enemy)
        {
            EnemyList.Add(enemy);
        }

        public void tick()
        {
            foreach (var e in EnemyList)
            {
                e.tick();
            }

            foreach (var b in BlockList)
            {
                b.tick();
            }

            InvalidatePath = false;
        }


        public void render(Graphics g)
        {
            foreach (var e in EnemyList)
            {
                e.render(g);
            }

            foreach (var b in BlockList)
            {
                b.render(g, b == ActivatedBlock);
            }
        }

    }
}
