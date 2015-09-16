using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace defense
{

    public class Enemy
    {
        public MyPoint curPos = new MyPoint { X = 50, Y = 50 };
        public double health = 200;
        public double maxHealth = 200;
        public Map Map { get; private set; }

        private MyPoint[] PathList = null;
        private int curPathIdx = 0;

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

        public Enemy(Map map)
        {
            Map = map;
        }

        public void tick()
        {
            // goto next pos
            MyPoint dest = DestList[nextDestIdx];
            if (PathList != null && curPathIdx + 1 >= PathList.Length)
            {
                nextDestIdx++;
                nextDestIdx %= DestList.Count;
                PathList = null;
            }
            curPos = getNextPos(curPos, DestList[nextDestIdx]);
        }

        public void render(Graphics g)
        {
            g.FillEllipse(Brushes.IndianRed, curPos.X - 5, curPos.Y - 5, 10, 10);
            g.DrawEllipse(Pens.Red, curPos.X - 5, curPos.Y - 5, 10, 10);

            int healthRatio = (int)(health * 10 / maxHealth);
            if (healthRatio >= 7)
            {
                g.FillRectangle(Brushes.LightGreen, curPos.X - 5, curPos.Y - 12, healthRatio, 3);
                g.DrawRectangle(Pens.Green, curPos.X - 5, curPos.Y - 12, 10, 3);
            }
            else if (healthRatio >= 3)
            {
                g.FillRectangle(Brushes.LightYellow, curPos.X - 5, curPos.Y - 12, healthRatio, 3);
                g.DrawRectangle(Pens.Yellow, curPos.X - 5, curPos.Y - 12, 10, 3);
            }
            else
            {
                g.FillRectangle(Brushes.PaleVioletRed, curPos.X - 5, curPos.Y - 12, healthRatio, 3);
                g.DrawRectangle(Pens.Red, curPos.X - 5, curPos.Y - 12, 10, 3);
            }
        }

        public bool isAlive()
        {
            return health > 0;
        }

        private MyPoint getNextPos(MyPoint curPos, MyPoint point)
        {
            if (PathList == null)
            {
                PathList = Map.getNextPosList(curPos, point, true).ToArray();
                curPathIdx = 0;
            }
            else
            {
                IEnumerable<MyPoint> pathList = Map.getNextPosList(curPos, point, false);
                if (pathList != null)
                {
                    PathList = pathList.ToArray();
                    curPathIdx = 0;
                }
            }

            MyPoint nextPos = PathList[curPathIdx];
            curPathIdx++;

            return nextPos;
        }
    }
}
