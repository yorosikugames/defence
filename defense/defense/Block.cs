using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace defense
{
    public enum BlockType
    {
        FIRE,
        WATER,
        ELECTRONIC,

        NONE,
    }

    public class Block
    {
        public int x;
        public int y;
        public int size;
        public BlockType type;

        public Block(int x, int y, BlockType type)
        {
            this.x = x;
            this.y = y;
            size = 10;
            this.type = type;
        }

        private static Pen nonePen = new Pen(Color.Gray);
        private static Pen firePen = new Pen(Color.Red);
        private static Pen waterPen = new Pen(Color.Blue);
        private static Pen electronicPen = new Pen(Color.Yellow);

        private static Brush fireBrush = new SolidBrush(Color.FromArgb(50, Color.OrangeRed));
        private static Brush waterBrush = new SolidBrush(Color.FromArgb(50, Color.LightSkyBlue));
        private static Brush electronicBrush = new SolidBrush(Color.FromArgb(50, Color.Yellow));
        private static Brush noneBrush = new SolidBrush(Color.FromArgb(50, Color.DimGray));

        static Block()
        {
        }

        public void tick()
        {
        }

        public void render(Graphics g, bool activated)
        {
            Brush brush = null;
            switch (type)
            {
                case BlockType.FIRE:
                    brush = fireBrush;
                    break;
                case BlockType.WATER:
                    brush = waterBrush;
                    break;
                case BlockType.ELECTRONIC:
                    brush = electronicBrush;
                    break;
                case BlockType.NONE:
                    brush = noneBrush;
                    break;
            }

            g.FillRectangle(brush, x, y, size, size);

            Pen pen = null;
            switch (type)
            {
                case BlockType.FIRE:
                    pen = firePen;
                    break;
                case BlockType.WATER:
                    pen = waterPen;
                    break;
                case BlockType.ELECTRONIC:
                    pen = electronicPen;
                    break;
                case BlockType.NONE:
                    pen = nonePen;
                    break;
            }

            g.DrawRectangle(pen, x, y, size, size);
        }
    }
}
