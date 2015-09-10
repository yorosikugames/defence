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

    public class Enemy
    {
        public MyPoint curPos = new MyPoint { X = 50, Y = 50 };
        public double health = 100;

        private static MyPoint[,] grid = new MyPoint[450, 450];

        private int nextDestIdx = 0;
        private static List<MyPoint> DestList = new List<MyPoint> {
            new MyPoint {X = 225, Y = 225 },
            new MyPoint {X = 50, Y = 225 },
            new MyPoint {X = 225, Y = 225 },
            new MyPoint {X = 225, Y = 50 },
            new MyPoint {X = 225, Y = 225 },
            new MyPoint {X = 225, Y = 400 },
            new MyPoint {X = 225, Y = 225 },
            new MyPoint {X = 400, Y = 225 },
            new MyPoint {X = 225, Y = 225 },
            new MyPoint {X = 400, Y = 400 },
        };

        public Enemy()
        {
            for (int x = 0; x < 450; x++)
                for (int y = 0; y < 450; y++)
                    grid[x, y] = new MyPoint { X = x, Y = y, IsWall = false };
        }

        private Pen pen = new Pen(Color.Red);

        public void tick(Graphics g)
        {
            g.DrawEllipse(pen, curPos.X - 10, curPos.Y - 10, 20, 20);

            // goto next pos
            if (nextDestIdx < DestList.Count)
            {
                MyPoint dest = DestList[nextDestIdx];
                if (curPos.X == dest.X && curPos.Y == dest.Y)
                {
                    nextDestIdx++;
                }

                curPos = getNextPos(curPos, DestList[nextDestIdx]);
            }
        }

        private MyPoint getNextPos(MyPoint curPos, MyPoint point)
        {
            MySolver<MyPoint, Object> aStar = new MySolver<MyPoint, Object>(grid);
            IEnumerable<MyPoint> path = aStar.Search(new Point(curPos.X, curPos.Y), new Point(point.X, point.Y), null);

            return (path.Count() > 3 ? path.ElementAt(2) : path.ElementAt(1));
        }
    }
}
