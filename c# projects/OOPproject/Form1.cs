using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
        public enum FigureSelection
        {
            Point, None, Pencil, Ellipse, PerfectCircle, Rectangle, Line, Rhombus, ObjectEraser, Fill, Color, Clear, Undo, Redo
        }
        Bitmap bm;
        Graphics g;
        const int DEFAULT_WIDTH = 5;
        bool paint = false;
        Point pX, pY;
        Pen pen1 = new Pen(Color.Black, DEFAULT_WIDTH);
        Pen eraser = new Pen(Color.White, 15);
        FigureSelection currSelect = FigureSelection.None;
        int figureIndex=-1;
        int selectedFigureIndex = -1;
        //int x, y, sX, sY, cX, cY;
        ColorDialog cd = new ColorDialog();
        Color New_Color = Color.Black; //Default Stroke Color
        FigureList Flist = new FigureList();
        FigureList FHistoryList = new FigureList();
        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            paint = true;
            pY = e.Location;
            
      
            figureIndex = Flist.NextIndex;
            switch (currSelect)
            {
                case FigureSelection.Pencil:
                    Flist[figureIndex] = new AbstractFig(e.X, e.Y);
                    Flist[figureIndex].FillColor = Color.Transparent;
                    Flist[figureIndex].StrokeColor = New_Color;
                    Flist[figureIndex].StrokeWidth = DEFAULT_WIDTH;
                    break;
                case FigureSelection.Ellipse: // ellipse     
                    Flist[figureIndex] = new Ellipse(e.X, e.Y, e.X, e.Y);
                    Flist[figureIndex].FillColor = Color.Transparent;
                    Flist[figureIndex].StrokeColor = New_Color;
                    // FOR TESTING : TODO
                    Flist[figureIndex].StrokeWidth = DEFAULT_WIDTH;
                    break;
                case FigureSelection.Rectangle: // rect
                    Flist[figureIndex] = new Rectangle(e.X, e.Y, 0, 0);
                    Flist[figureIndex].FillColor = Color.Transparent;
                    Flist[figureIndex].StrokeColor = New_Color;
                    // FOR TESTING : TODO
                    Flist[figureIndex].StrokeWidth = DEFAULT_WIDTH;
                    break;
                case FigureSelection.Line: // line
                    Flist[figureIndex] = new Line(e.X, e.Y, e.X, e.Y);
                    // FOR TESTING : TODO
                    Flist[figureIndex].StrokeWidth = DEFAULT_WIDTH;
                    Flist[figureIndex].StrokeColor = New_Color;
                    break;
                case FigureSelection.Rhombus: //rhombus
                    Flist[figureIndex] = new Rhombus(e.X, e.Y, 0, 0);
                    Flist[figureIndex].FillColor = Color.Transparent;
                    // FOR TESTING : TODO
                    Flist[figureIndex].StrokeWidth = DEFAULT_WIDTH;
                    Flist[figureIndex].StrokeColor = New_Color;
                    break;
                case FigureSelection.PerfectCircle: //"perfect" circle
                    Flist[figureIndex] = new Circle(e.X, e.Y, 0);
                    Flist[figureIndex].FillColor = Color.Transparent;
                    // FOR TESTING : TODO
                    Flist[figureIndex].StrokeWidth = DEFAULT_WIDTH;
                    Flist[figureIndex].StrokeColor = New_Color;
                    break;
            }
            pic.Text = Flist.NextIndex + "";
        }
        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            if(paint && figureIndex != -1)
            {
                Figure c = (Figure)Flist[figureIndex];
                switch (currSelect)
                {
                    case FigureSelection.Point:
                        if (selectedFigureIndex >= 0 && selectedFigureIndex < Flist.NextIndex)
                        {
                            Flist[selectedFigureIndex].Move(e.X, e.Y);
                        }

                        break;
                    //case FigureSelection.Pencil:
                    //    //pX = e.Location;
                    //    //g.DrawLine(pen1, pX, pY);
                    //    //pY = pX;
                    //    ((AbstractFig)c).Add(e.X ,e.Y);
                    //    break;    
                    case FigureSelection.Ellipse:
                    case FigureSelection.Rectangle:
                    case FigureSelection.Line: // line
                    case FigureSelection.Rhombus: //rhombus
                    case FigureSelection.Pencil:
                        c.Change(e.X, e.Y);
                        //((Ellipse)c).Draw(g);
                        break;
                    //case FigureSelection.Rectangle: // rect
                    //    ((Rectangle)c).Width = e.X - c.X;
                    //    ((Rectangle)c).Height = e.Y - c.Y;
                    //    //((Rectangle)c).Draw(g);
                    //    break;
                    //case FigureSelection.Line: // line
                    //    ((Line)c).X2 = e.X;
                    //    ((Line)c).Y2 = e.Y;
                    //    break;
                    case FigureSelection.Fill: // fill
                        break;// pencil 
                    case FigureSelection.ObjectEraser:
                        int index = Flist.Find(e.X, e.Y);
                        if (index != -1)
                        {
                            Flist.Remove(Flist.Find(e.X, e.Y));
                            pic.Text = Flist.NextIndex + "";
                            //MessageBox.Show("inside !" + ((Flist[i]).GetType()).ToString()); // when clicking inside with pencil pencil - just to test
                        }
                        break;
                    //case FigureSelection.Rhombus: //rhombus
                    //    ((Rhombus)c).Width = e.X - c.X;
                    //    ((Rhombus)c).Height = e.Y - c.Y;
                    //    //((Rhombus)c).Draw(g);
                    //    break;
                    case FigureSelection.PerfectCircle: //"perfect" circle
                        // TODO: check why Line.Distance doesnt work ---- double to float casting ----- point of start should be top left (angle 45) 
                        float newX = Math.Abs(((Circle)c).X - e.X);
                        float newY = Math.Abs(((Circle)c).Y - e.Y);
                        double dis = Math.Sqrt(newX * newX + newY * newY);
                        ((Circle)c).Radius = (float)dis; // double to float TODO
                        //((Circle)c).Center = new MyPoint(newX, newY);
                        break;

                }
                pic.Invalidate();
                    //if (currSelect==1)
                    //{
                    //    pX = e.Location;
                    //    g.DrawLine(pen1,pX,pY);
                    //    pY = pX;
                    //}
                    //if (currSelect == 2)
                    //{
                    //    pX = e.Location;
                    //    g.DrawLine(eraser, pX, pY);
                    //    pY = pX;
                    //}
                
            }
            //pic.Refresh();
            //pic.Invalidate();

            //x = e.X;
            //y = e.Y;
            //sX = e.X - cX;
            //sY = e.Y - cY;
        }
        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            paint = false;
            figureIndex = -1;
            //sX = x - cX;
            //sY = y - cY;
            //Figure c = (Figure)Flist[figureIndex];
            //if (currSelect == 3)
            //{
            //    g.DrawEllipse(pen1, cX, cY, sX, sY);
            //}
            //if (currSelect == 4)
            //{
            //    g.DrawRectangle(pen1, cX, cY, sX, sY);
            //    if (figureIndex >= 1)
            //        ((Rectangle)c).Width = e.X - c.X;
            //    ((Rectangle)c).Width = e.X - c.X;
            //    ((Rectangle)c).Height = e.Y - c.Y;
            //    ((Rectangle)c).Draw(g);
            //}
            //if (currSelect == 5)
            //{
            //    Line l;
            //    l.Draw(g);
            //    g.DrawLine(pen1, cX, cY, x, y);
            //}
            //pic.Invalidate();
        }

        /***************************    Buttons       *******************************/
        #region Buttons Clicks Events Methods
        private void btn_clear_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            pic.Image = bm;
            currSelect =  FigureSelection.Clear;
            Flist.Clear();
            pic.Text = Flist.NextIndex + "";
            clearSelection(true);
        }
        private void btn_color_Click(object sender, EventArgs e)
        {
            cd.ShowDialog();
            currSelect = FigureSelection.Color;
            New_Color = cd.Color;
            pic_color.BackColor = cd.Color;
            Figure.SELECTED_COLOR = New_Color;
            pen1.Color = cd.Color;
            if (selectedFigureIndex != -1)
                Flist[selectedFigureIndex].StrokeColor = New_Color;
            clearSelection(false);
            pic.Invalidate();
        }
        private void btn_pencil_Click(object sender, EventArgs e)
        {
            currSelect = FigureSelection.Pencil;
            clearSelection(true);
        }
        
        //private void btn_eraser_Click(object sender, EventArgs e)
        //{
        //    currSelect = FigureSelection.Eraser;
        //    clearSelection(false);
        //}
        private void btn_circle_Click(object sender, EventArgs e)
        {
            currSelect = FigureSelection.PerfectCircle;
            paint = false;
            clearSelection(true);
          
        }
        private void btn_rect_Click(object sender, EventArgs e)
        {
            currSelect = FigureSelection.Rectangle;
            clearSelection(true);
        }
        private void btn_line_Click(object sender, EventArgs e)
        {
            currSelect = FigureSelection.Line;
            clearSelection(true);
        }
        private void btn_fill_Click(object sender, EventArgs e)
        {
            currSelect = FigureSelection.Fill;
            showEditMenu();
            clearSelection(false);
        }
        private void btn_rhombus_Click(object sender, EventArgs e)
        {
            currSelect = FigureSelection.Rhombus;
            clearSelection(true);
        }

        //private void btn_pen_eraser_Click(object sender, EventArgs e)
        //{
        //    currSelect = FigureSelection.PenEraser;
        //    clearSelection(false);

        //}

        private void btn_object_eraser_Click(object sender, EventArgs e)
        {
            currSelect = FigureSelection.ObjectEraser;
            clearSelection(true);
        }

        private void btn_undo_Click(object sender, EventArgs e) //doesnt work for pencil yet
        {
            currSelect = FigureSelection.Undo;
            if (Flist.NextIndex > 0)
            {
                FHistoryList[FHistoryList.NextIndex] = Flist[Flist.NextIndex-1];
                Flist.Remove(Flist.NextIndex - 1);
                pic.Invalidate();
            }
            clearSelection(true);
        }

        private void btn_redo_Click(object sender, EventArgs e)
        {
            currSelect = FigureSelection.Redo;
            if (FHistoryList.NextIndex > 0)
            {
                Flist[Flist.NextIndex] = FHistoryList[FHistoryList.NextIndex - 1];
                FHistoryList.Remove(FHistoryList.NextIndex - 1);
                pic.Invalidate();
            }
            clearSelection(true);
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Image(*.jpg)|*.jpg|(*.*|*.*";
            if(sfd.ShowDialog()==DialogResult.OK)
            {
                switch (sfd.FilterIndex)
                {
                    case 1: // Image
                        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, pic.Width, pic.Height);
                        Bitmap btm = bm.Clone(rect, bm.PixelFormat);
                        btm.Save(sfd.FileName, ImageFormat.Jpeg);
                        MessageBox.Show("Image saved sucessfully!");
                        break;
                    case 2: // 

                        break;

                    case 3: // All

                        break;
                }
            }
            clearSelection(true);
            currSelect = FigureSelection.None;
        }

        private void pic_MouseClick(object sender, MouseEventArgs e)
        {
            clearSelection(false);
            switch (currSelect)
            {
                case FigureSelection.Fill:
                    Point point = set_point(pic, e.Location);
                    Fill(bm, point.X, point.Y, New_Color);
                    break;
                case FigureSelection.ObjectEraser:
                    int index = Flist.Find(e.X, e.Y);
                    if (index != -1)
                    {
                        Flist.Remove(Flist.Find(e.X, e.Y));
                        pic.Text = Flist.NextIndex + "";
                        //MessageBox.Show("inside !" + ((Flist[i]).GetType()).ToString()); // when clicking inside with pencil pencil - just to test
                        pic.Invalidate();
                    } 
                    break;
                case FigureSelection.Point:
                    bool foundFig = false;
                    for (int i = Flist.NextIndex - 1; i >= 0; i--)
                    {
                        if (Flist[i].isInside(e.X, e.Y))
                        {
                            pic.Text = ((Flist[i]).GetType()).ToString() + " [" + i + "]";
                            if (selectedFigureIndex != -1) {
                                clearSelectedFig();
                            }
                            selectedFigureIndex = i;
                            (Flist[i]).IsSelected = true;
                            foundFig = true;
                            break;
                        }
                    }
                    if (!foundFig)
                        clearSelectedFig();
                    pic.Invalidate();
                    break;
            }
            
        }
        #endregion


        private void pic_Paint(object sender, PaintEventArgs e)
        {
            Graphics paintGraphics = e.Graphics;
            paintGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Flist.DrawAll(paintGraphics);
            //textBoxForTesting.Text = Flist.NextIndex + "-" + currSelect.ToString();
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

        private void textBoxForTesting_Click(object sender, EventArgs e)
        {
            currSelect = FigureSelection.Point;
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

        public void clearSelection(bool clearAll)
        {
            paint = false;
            if (clearAll)
            {
                clearSelectedFig();
                btn_fill.Hide();
                btn_change_clr.Hide();
                Figure.SELECTED_COLOR = Color.Red;
            }
        }

        private void btn_EditObject_Click(object sender, EventArgs e)
        {
            currSelect = FigureSelection.Point;
            showEditMenu();
        }

        private void btn_change_clr_Click(object sender, EventArgs e)
        {
            showEditMenu();
            clearSelection(false);
        }

        private void btn_changeSize_Click(object sender, EventArgs e)
        {
            showEditMenu();
            clearSelection(false);
        }

        public void clearSelectedFig()
        {
            if (selectedFigureIndex  >= 0 && selectedFigureIndex < Flist.NextIndex)
            {
                (Flist[selectedFigureIndex]).IsSelected = false;
                selectedFigureIndex = -1;
            }
        }

        public void showEditMenu()
        {
            btn_fill.Show();
            btn_change_clr.Show();
            btn_strokeWidth.Show();
        }
    }
}
