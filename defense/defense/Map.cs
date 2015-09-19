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
        private BlockType curBlockType = BlockType.NONE;
        private Random blockRandom = new Random(DateTime.Now.Millisecond);

        private static long tickCount = 0;

        public bool stageStarted = false;
        public int remainEnemySpawnCount = 0;
        public int nextSpawnTick = 0;

        public int nextStage = 100;
        public int gold = 100;
        public int stage = 0;

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

        public BlockType genBlockType()
        {
            if (blockRandom.Next(10) < 8)
                return BlockType.NONE;

            if (curBlockType == BlockType.NONE)
            {
                curBlockType = BlockType.FIRE;
            }
            else if (curBlockType == BlockType.FIRE)
            {
                curBlockType = BlockType.WATER;
            }
            else if (curBlockType == BlockType.WATER)
            {
                curBlockType = BlockType.ELECTRONIC;
            }
            else if (curBlockType == BlockType.ELECTRONIC)
            {
                curBlockType = BlockType.NONE;
            }

            return curBlockType;
        }

        public Block getElemAt(int x, int y)
        {
            if ((!(0 <= x && x < 450)) || (!(0 <= y && y < 450)))
            {
                ActivatedBlock = null;
                return ActivatedBlock;
            }
            ActivatedBlock = grid[x, y].LinkedBlock;
            return ActivatedBlock;
        }

        public void setElem(Block block)
        {
            BlockList.Add(block);

            for (int x = block.x; x <= block.x + block.size; x++)
            {
                for (int y = block.y; y <= block.y + block.size; y++)
                {
                    if (!(0 <= x && x < 450)) continue;
                    if (!(0 <= y && y < 450)) continue;

                    if (!((x == block.x + block.size) || (y == block.y + block.size)))
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
            tickCount++;

            if (remainEnemySpawnCount > 0)
            {
                if (nextSpawnTick == 0)
                {
                    registerEnemy(new Enemy(this));
                    remainEnemySpawnCount--;
                    nextSpawnTick = 10;
                }
                else
                {
                    nextSpawnTick--;
                }
            }

            if (stageStarted && EnemyList.Count == 0)
            {
                nextStage = 300;
                stageStarted = false;
                stage++;

                remainEnemySpawnCount = 5;
            }

            if (nextStage <= 0)
            {
                if (stageStarted == false)
                {
                    stageStarted = true;
                }
            }
            else
            {
                nextStage--;
            }

            var enemyRemovalList = new List<Enemy>();
            var checkedBlockSet = new HashSet<Block>();
            foreach (var e in EnemyList)
            {
                e.tick();
                
                foreach (var x in Enumerable.Range(e.curPos.X - 4, 9))
                {
                    foreach (var y in Enumerable.Range(e.curPos.Y - 4, 9))
                    {
                        var block = getElemAt(x, y);
                        if (block == null) continue;
                        if (block.type == BlockType.NONE) continue;
                        if (checkedBlockSet.Contains(block)) continue;
                        checkedBlockSet.Add(block);

                        switch (block.type)
                        {
                            case BlockType.FIRE:
                                e.health--;
                                break;
                            case BlockType.WATER:
                                e.health--;
                                break;
                            case BlockType.ELECTRONIC:
                                e.health--;
                                break;

                        }
                    }
                }

                if (!e.isAlive()) enemyRemovalList.Add(e);
            }

            foreach (var e in enemyRemovalList)
            {
                EnemyList.Remove(e);
                gold += 10;
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
                //b.render(g, b == ActivatedBlock);
                b.render(g, false);
            }
        }

    }
}
