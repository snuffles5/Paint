using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOPproject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Width = 900;
            this.Height = 700;
            bm = new Bitmap(pic.Width,pic.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            pic.Image = bm;
        }
        Bitmap bm;
        Graphics g;
        bool paint = false;
        Point pX, pY;
        Pen pen1 = new Pen(Color.Black,1);
        Pen eraser = new Pen(Color.White, 10);
        int x, y, sX, sY, cX, cY;
        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            paint = true;
            pY = e.Location;
            cX = e.X;
            cY = e.Y;

        }

        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            if(paint)
            {
                if(index==1)
                {
                    pX = e.Location;
                    g.DrawLine(pen1,pX,pY);
                    pY = pX;
                }
                if (index == 2)
                {
                    pX = e.Location;
                    g.DrawLine(eraser, pX, pY);
                    pY = pX;
                }
            }
            pic.Refresh();
            x = e.X;
            y = e.Y;
            sX = e.X - cX;
            sY = e.Y - cY;
        }

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            paint = false;
        }

        private void btn_pencil_Click(object sender, EventArgs e)
        {
            index = 1;
        }

        private void btn_eraser_Click(object sender, EventArgs e)
        {
            index = 2;
        }

        int index;
    }
}
