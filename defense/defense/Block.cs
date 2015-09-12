using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace defense
{
    public class Block
    {
        public int x;
        public int y;
        public int size;

        public Block(int x, int y)
        {
            this.x = x;
            this.y = y;
            size = 15;
        }

        private Pen pen = new Pen(Color.Black);

        public void tick()
        {
        }

        public void render(Graphics g)
        {
            g.DrawRectangle(pen, x - size, y - size, 2 * size + 1, 2 * size + 1);
        }
    }
}
