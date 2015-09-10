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
        private Enemy enemy = new Enemy();

        public Defense()
        {
            InitializeComponent();
        }

        private void Defense_Load(object sender, EventArgs e)
        {

        }

        private void ticker_Tick(object sender, EventArgs e)
        {
            Graphics g = label1.CreateGraphics();
            using (BufferedGraphics graphic = BufferedGraphicsManager.Current.Allocate(g, label1.ClientRectangle))
            {
                Bitmap bgImg = new Bitmap(@"Resources\map.png");
                graphic.Graphics.DrawImage(bgImg, 0, 0, 450, 450);

                enemy.tick(graphic.Graphics);

                //bufferedgraphic.Graphics.Clear(Color.Silver);
                //bufferedgraphic.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //bufferedgraphic.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                //bufferedgraphic.Graphics.TranslateTransform(this.AutoScrollPosition.X, this.AutoScrollPosition.Y);

                //Pen p = new Pen(Color.FromArgb(111, 91, 160), 3);
                //bufferedgraphic.Graphics.DrawLine(p, 0, 0, 100, 100);
                //p.Dispose();

                graphic.Render(g);
            }

        }
    }
}