using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
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
            this.Width = 950;
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
        int index;
        int x, y, sX, sY, cX, cY;
        ColorDialog cd = new ColorDialog();
        Color New_Color;

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
            sX = x - cX;
            sY = y - cY;

            if(index==3)
            {
                g.DrawEllipse(pen1,cX,cY,sX,sY);
            }
            if (index == 4)
            {
                //Rectangle r;
                //r.Draw(g)
                g.DrawRectangle(pen1, cX, cY, sX, sY);
            }
            if(index==5)
            {
                g.DrawLine(pen1,cX,cY,x,y);
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            pic.Image = bm;
            index = 0;
            //מחיקת רשימה מקושרת?
        }

        private void btn_color_Click(object sender, EventArgs e)
        {
            cd.ShowDialog();
            New_Color = cd.Color;
            pic_color.BackColor = cd.Color;
            pen1.Color = cd.Color;
        }

        private void btn_pencil_Click(object sender, EventArgs e)
        {
            index = 1;
        }

        private void color_pick_MouseClick(object sender, MouseEventArgs e)
        {
            Point point = set_point(color_pick, e.Location);
            pic_color.BackColor = ((Bitmap)color_pick.Image).GetPixel(point.X, point.Y);
            New_Color = pic_color.BackColor;
            pen1.Color = pic_color.BackColor;
        }

        private void btn_eraser_Click(object sender, EventArgs e)
        {
            index = 2;
        }
        private void btn_circle_Click(object sender, EventArgs e)
        {
            index = 3;
        }

        private void pic_MouseClick(object sender, MouseEventArgs e)
        {
            if(index==7)
            {
                Point point = set_point(pic, e.Location);
                Fill(bm, point.X, point.Y, New_Color);
            }
        }

        private void btn_fill_Click(object sender, EventArgs e)
        {
            index = 7;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Image(*.jpg)|*.jpg|(*.*|*.*";
            if(sfd.ShowDialog()==DialogResult.OK)
            {
                Rectangle rect = new Rectangle(0, 0, pic.Width, pic.Height);
                Bitmap btm = bm.Clone(rect, bm.PixelFormat);
                btm.Save(sfd.FileName,ImageFormat.Jpeg);
                MessageBox.Show("Image saved sucessfully!");
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_rect_Click(object sender, EventArgs e)
        {
            index = 4;
        }
        private void btn_line_Click(object sender, EventArgs e)
        {
            index = 5;
        }
        private void pic_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if(paint)
            {
                if (index == 3)
                {
                    g.DrawEllipse(pen1, cX, cY, sX, sY);
                }
                if (index == 4)
                {
                    g.DrawRectangle(pen1, cX, cY, sX, sY);
                }
                if (index == 5)
                {
                    g.DrawLine(pen1, cX, cY, x, y);
                }
            }
        }

        static Point set_point(PictureBox pb, Point p)
        {
            float pX = 1f * pb.Image.Width / pb.Width;
            float pY = 1f * pb.Image.Height / pb.Height;
            return new Point((int)(p.X * pX),(int)(p.Y * pY));
        }
         private void validate(Bitmap bm,Stack<Point>sp,int x, int y, Color oldColor,Color newColor)
        {
            Color cx = bm.GetPixel(x, y);
            if(cx==oldColor)
            {
                sp.Push(new Point(x, y));
                bm.SetPixel(x, y, newColor);
            }
        }
        public void Fill(Bitmap bm,int x ,int y,Color newColor )
        {
            Color oldColor = bm.GetPixel(x, y);
            Stack<Point> pixel = new Stack<Point>();
            pixel.Push(new Point(x,y));
            bm.SetPixel(x, y, newColor);
            if (oldColor == newColor) return;
            while(pixel.Count>0)
            {
                Point p = (Point)pixel.Pop();
                if (p.X > 0 && p.Y > 0 && p.X < bm.Width - 1 && p.Y < bm.Height - 1)
                {
                    validate(bm, pixel, p.X - 1, p.Y, oldColor, newColor);
                    validate(bm, pixel, p.X , p.Y-1, oldColor, newColor);
                    validate(bm, pixel, p.X+ 1, p.Y, oldColor, newColor);
                    validate(bm, pixel, p.X , p.Y+1, oldColor, newColor);
                }

            }

        }

    }
}
