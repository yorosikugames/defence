﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace defense
{
    public partial class Defense : Form
    {
        private Map map = new Map();

        public Defense()
        {
            InitializeComponent();
        }

        private void Defense_Load(object sender, EventArgs e)
        {
        }

        private void ticker_Tick(object sender, EventArgs e)
        {
            map.tick();
            mapLabel.Refresh();
            goldLabel.Text = map.gold.ToString();
            stageLabel.Text = map.nextStage.ToString();
        }

        private void mapLabel_Click(object sender, EventArgs e)
        {
            var mouseEventArgs = e as MouseEventArgs;
            if (mouseEventArgs == null) return;

            var mousePos = new Point(mouseEventArgs.X - (mouseEventArgs.X % 10), mouseEventArgs.Y - (mouseEventArgs.Y % 10));
            var mapElem = map.getElemAt(mousePos.X, mousePos.Y);
            if (mapElem == null && map.gold >= 5)
            {
                map.gold -= 5;

                Block block = new Block(mousePos.X, mousePos.Y, map.genBlockType());
                map.setElem(block);
            }
        }

        private void mapLabel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            using (BufferedGraphics graphic = BufferedGraphicsManager.Current.Allocate(g, mapLabel.ClientRectangle))
            {
                Bitmap bgImg = new Bitmap(@"Resources\map.png");
                graphic.Graphics.DrawImage(bgImg, 0, 0, 450, 450);

                //enemy.tick(graphic.Graphics);
                map.render(graphic.Graphics);

                //bufferedgraphic.Graphics.Clear(Color.Silver);
                //bufferedgraphic.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //bufferedgraphic.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                //bufferedgraphic.Graphics.TranslateTransform(this.AutoScrollPosition.X, this.AutoScrollPosition.Y);

                //Pen p = new Pen(Color.FromArgb(111, 91, 160), 3);
                //bufferedgraphic.Graphics.DrawLine(p, 0, 0, 100, 100);
                //p.Dispose();

                var mousePos = mapLabel.PointToClient(Cursor.Position);
                mousePos = new Point(mousePos.X - mousePos.X % 10, mousePos.Y - mousePos.Y % 10);
                var elem = map.getElemAt(mousePos.X, mousePos.Y);
                if (elem == null)
                    graphic.Graphics.DrawRectangle(Pens.LightSlateGray, mousePos.X, mousePos.Y, 10, 10);

                graphic.Render(g);
            }
        }

        private void addEnemy_Click(object sender, EventArgs e)
        {
            map.registerEnemy(new Enemy(map));
        }
    }
}
