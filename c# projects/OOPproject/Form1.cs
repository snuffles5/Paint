﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
//System.Windows.Media.Imaging;


namespace OOPproject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Width = DEFAULT_FORM_WIDTH;
            this.Height = DEFAULT_FORM_HEIGHT;
            bm = new Bitmap(pic.Width, pic.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            pic.Image = bm;
        }

        private void Form1_Shown(Object sender, EventArgs e)
        {
            FHistoryList.Add(new FigureList(Flist));
            currentFlistIndex++;
            txtBoxForTesting.Text = "i=" + currentFlistIndex + " count=";
            txtBoxForTesting.Text += FHistoryList != null ? " " + FHistoryList.Count : "0";
            btn_undo.Enabled = false;
            btn_redo.Enabled = false;
        }

        public enum FigureSelection
        {
            EditObject, None, 
            Pencil, Ellipse, PerfectCircle, Rectangle, Line, Rhombus, 
            ObjectEraser, Fill, Color, Clear, Undo, Redo, StrokeWidth, ChangeStrokeColor,
            Import, Save            
        }
        public const int DEFAULT_STROKE_WIDTH = 5;
        public const int DEFAULT_FORM_WIDTH = 950;
        public const int DEFAULT_FORM_HEIGHT = 700;
        Color New_Color = Color.Black; //Default Stroke Color
        bool paint = false;
        bool isFigureMoved = false;
        bool isErased = false;
        private bool isMouseMoved = false;
        int figureIndex = -1;
        int selectedFigureIndex = -1;
        int currentFlistIndex = -1;
        Bitmap bm;
        Graphics g;
        ColorDialog cd = new ColorDialog();
        Pen pen1 = new Pen(Color.Black, DEFAULT_STROKE_WIDTH);
        List<FigureList> FHistoryList = new List<FigureList>();
        FigureList Flist = new FigureList();
        FigureSelection currSelect = FigureSelection.None;
        MyPoint mouseDownPoint = new MyPoint();

        //int new_width;


        #region main events

        /***************************  Mouse Down inside Pic    *******************************/

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            paint = true;
            isMouseMoved = false;
            mouseDownPoint.X = e.X;
            mouseDownPoint.Y = e.Y;
            figureIndex = Flist.NextIndex;
            if (selectedFigureIndex >= 0) clearSelectedFig();
            switch (currSelect) 
            {
                case FigureSelection.Pencil:
                    Flist[figureIndex] = new AbstractFig(e.X, e.Y);
                    Flist[figureIndex].FillColor = Color.Transparent;
                    Flist[figureIndex].StrokeColor = New_Color;
                    Flist[figureIndex].StrokeWidth = DEFAULT_STROKE_WIDTH;
                    break;
                case FigureSelection.Ellipse: // ellipse     
                    Flist[figureIndex] = new Ellipse(e.X, e.Y, e.X, e.Y);
                    Flist[figureIndex].FillColor = Color.Transparent;
                    Flist[figureIndex].StrokeColor = New_Color;
                    // FOR TESTING : TODO
                    Flist[figureIndex].StrokeWidth = DEFAULT_STROKE_WIDTH;
                    break;
                case FigureSelection.Rectangle: // rect
                    Flist[figureIndex] = new Rectangle(e.X, e.Y, 0, 0);
                    Flist[figureIndex].FillColor = Color.Transparent;
                    Flist[figureIndex].StrokeColor = New_Color;
                    // FOR TESTING : TODO
                    Flist[figureIndex].StrokeWidth = DEFAULT_STROKE_WIDTH;
                    break;
                case FigureSelection.Line: // line
                    Flist[figureIndex] = new Line(e.X, e.Y, e.X, e.Y);
                    // FOR TESTING : TODO
                    Flist[figureIndex].StrokeWidth = DEFAULT_STROKE_WIDTH;
                    Flist[figureIndex].StrokeColor = New_Color;
                    break;
                case FigureSelection.Rhombus: //rhombus
                    Flist[figureIndex] = new Rhombus(e.X, e.Y, 0, 0);
                    Flist[figureIndex].FillColor = Color.Transparent;
                    // FOR TESTING : TODO
                    Flist[figureIndex].StrokeWidth = DEFAULT_STROKE_WIDTH;
                    Flist[figureIndex].StrokeColor = New_Color;
                    break;
                case FigureSelection.PerfectCircle: //"perfect" circle
                    Flist[figureIndex] = new Circle(e.X, e.Y, 0);
                    Flist[figureIndex].FillColor = Color.Transparent;
                    // FOR TESTING : TODO
                    Flist[figureIndex].StrokeWidth = DEFAULT_STROKE_WIDTH;
                    Flist[figureIndex].StrokeColor = New_Color;
                    break;
                default:
                    paint = false;
                    break;
            }
            pic.Text = Flist.NextIndex + "";
        }

        /***************************    Mouse Move       *******************************/

        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
           
            if (paint && figureIndex != -1)
            {
                Figure c = (Figure)Flist[figureIndex];
                switch (currSelect)
                {
                    case FigureSelection.Ellipse:
                    case FigureSelection.Rectangle:
                    case FigureSelection.Line: // line
                    case FigureSelection.Rhombus: //rhombus
                    case FigureSelection.Pencil:
                    case FigureSelection.PerfectCircle:
                        if (selectedFigureIndex < 0) // none object is selected
                            c.Change(e.X, e.Y);
                        break;
                    case FigureSelection.ObjectEraser: // remove
                        int index = Flist.Find(e.X, e.Y);
                        //txtBoxForTesting.Text = index + " index to be erased";
                        if (index != -1)
                        {
                            Flist.Remove(Flist.Find(e.X, e.Y));
                            txtBoxForTesting.Text = index + " erased.";
                            isErased = true;
                            //MessageBox.Show("inside !" + ((Flist[i]).GetType()).ToString()); // when clicking inside with pencil pencil - just to test
                        }
                        break;
                }

                pic.Invalidate();
            }
            if (paint)
            {
                isMouseMoved = true;
                if (selectedFigureIndex >= 0 && selectedFigureIndex < Flist.NextIndex && Flist[selectedFigureIndex].isInsideSurrounding(e.X, e.Y))
                {
                    float offsetX = e.X - mouseDownPoint.X;
                    mouseDownPoint.X = e.X;
                    float offsetY = e.Y - mouseDownPoint.Y;
                    mouseDownPoint.Y = e.Y;
                    Flist[selectedFigureIndex].Move(offsetX, offsetY);
                    isFigureMoved = true;
                }
                pic.Invalidate();
            }
        }
        /***************************    Mouse Up       *******************************/

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            //TODO removed last created figure if not drawn. if isMouseMoved = false and  paint = true ???



            figureIndex = -1;
            switch (currSelect)
            {
                case FigureSelection.Ellipse:
                case FigureSelection.Rectangle:
                case FigureSelection.Line: // line
                case FigureSelection.Rhombus: //rhombus
                case FigureSelection.Pencil:
                case FigureSelection.PerfectCircle:
                    saveCurrentState();
                    break;
                case FigureSelection.ObjectEraser:
                    if (isErased) saveCurrentState();
                    isErased = false;
                    break;
            }
            if (isFigureMoved) // figure was moved
            {
                saveCurrentState();
                isFigureMoved = false;
            }
            if (!isMouseMoved) // mouse was clicked but didn't moved
            {
               
                bool foundFig = false;
                for (int i = Flist.NextIndex - 1; i >= 0; i--)
                {
                    if (Flist[i].isInside(e.X, e.Y)) // found object
                    {          
                        pic.Text = ((Flist[i]).GetType()).ToString() + " [" + i + "]";
                        if (selectedFigureIndex != -1) // found object before this one, need to clear
                        {
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
            }
            isMouseMoved = false;
            paint = false;
        }

        /***************************    Mouse Click       *******************************/

        private void pic_MouseClick(object sender, MouseEventArgs e)
        {
            clearSelection(false);
            switch (currSelect)
            {
                case FigureSelection.Fill:
                    //Point point = set_point(pic, e.Location);
                    int index = Flist.Find(e.X, e.Y);
                    if (index != -1)
                    {
                        //Fill(bm, point.X, point.Y, New_Color);
                        if (selectedFigureIndex >= 0 && selectedFigureIndex < Flist.NextIndex)
                        {
                            Flist[selectedFigureIndex].FillColor = New_Color;
                        }
                    }
                    break;
                case FigureSelection.ObjectEraser:
                    //int index = Flist.Find(e.X, e.Y);
                    //if (index != -1)
                    //{
                    //    Flist.Remove(Flist.Find(e.X, e.Y));
                    //    pic.Text = Flist.NextIndex + "";
                    //    //MessageBox.Show("inside !" + ((Flist[i]).GetType()).ToString()); // when clicking inside with pencil pencil - just to test
                    //    pic.Invalidate();
                    //} 
                    break;
                case FigureSelection.ChangeStrokeColor:

                    break;
            }
            

        }

        private void pic_Paint(object sender, PaintEventArgs e)
        {
            Graphics paintGraphics = e.Graphics;
            paintGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Flist.DrawAll(paintGraphics);
        }

        #endregion

        /***************************    Menu clicks events       *******************************/

        #region Menu clicks events Methods
        private void btn_clear_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            pic.Image = bm;
            currSelect = FigureSelection.Clear;
            pic.Text = Flist.NextIndex + "";
            txtBoxForTesting.Text = "i=" + currentFlistIndex + " count=";
            txtBoxForTesting.Text += FHistoryList != null ? " " + FHistoryList.Count : "0";
            Flist.Clear();
            FHistoryList.Clear();
            FHistoryList.Add(new FigureList(Flist));
            currentFlistIndex = 0;
            txtBoxForTesting.Text = "i=" + currentFlistIndex + " count=";
            txtBoxForTesting.Text += FHistoryList != null ? " " + FHistoryList.Count : "0";
            updateUndoRedoEnabled();
            clearSelection(true);
        }
        private void btn_color_Click(object sender, EventArgs e)
        {
            cd.ShowDialog();
            currSelect = FigureSelection.Color;
            New_Color = cd.Color;
            pic_color.BackColor = cd.Color;
            //Figure.SELECTED_COLOR = New_Color;
            pen1.Color = cd.Color;
            //if (selectedFigureIndex != -1)
            //   Flist[selectedFigureIndex].StrokeColor = New_Color;
            clearSelection(false);//?
            pic.Invalidate();
        }
        private void btn_pencil_Click(object sender, EventArgs e)
        {
            currSelect = FigureSelection.Pencil;
            clearSelection(true);
        }

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

        private void btn_object_eraser_Click(object sender, EventArgs e)
        {
            currSelect = FigureSelection.ObjectEraser;
            clearSelection(true);
        }

        
        private void btn_undo_Click(object sender, EventArgs e) //doesnt work for pencil yet
        {
            currSelect = FigureSelection.Undo;
            if (currentFlistIndex > 0)
            {
                currentFlistIndex--;
                Flist = new FigureList(FHistoryList[currentFlistIndex]);
                pic.Invalidate();
                txtBoxForTesting.Text = "i=" + currentFlistIndex + " count=";
                txtBoxForTesting.Text += FHistoryList != null ? " " + FHistoryList.Count : "0";

            }
            updateUndoRedoEnabled();
            clearSelection(true);
        }

        private void btn_redo_Click(object sender, EventArgs e)
        {
            currSelect = FigureSelection.Redo;
            if (currentFlistIndex < FHistoryList.Count - 1)
            {
                currentFlistIndex++;
                Flist = new FigureList(FHistoryList[currentFlistIndex]);
                pic.Invalidate();
                txtBoxForTesting.Text = "i=" + currentFlistIndex + " count=";
                txtBoxForTesting.Text += FHistoryList != null ? " " + FHistoryList.Count : "0";
            }
            updateUndoRedoEnabled();
            clearSelection(true);
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            currSelect = FigureSelection.Save;
            var sfd = new SaveFileDialog();
            //sfd.Filter = "Image(*.jpg)|*.jpg|(*.*|*.*";
            sfd.Filter = "Model (*.mdl)|*.mdl|Image (*.png)|*.png|(*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                switch (sfd.FilterIndex)
                {
                    case 1: // 
                        IFormatter formatter = new BinaryFormatter();
                        using (Stream stream = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            //!!!!
                            formatter.Serialize(stream, Flist);
                            stream.Close();
                            MessageBox.Show("File saved successfully!");
                        }
                        break;
                    case 2: // Image
                        clearSelectedFig();
                        pic.DrawToBitmap(bm, new System.Drawing.Rectangle(0, 0, pic.Width, pic.Height));
                        bm.Save(sfd.FileName, ImageFormat.Png);
                        MessageBox.Show("Image saved successfully!");
                        break;
                    case 3: // All

                        break;
                }
            }
            clearSelection(true);
            currSelect = FigureSelection.None;
        }

        private void btn_import_Click(object sender, EventArgs e)
        {
            currSelect = FigureSelection.Import;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Model (*.mdl)|*.mdl|Image (*.jpg)|*.jpg|(*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                switch (ofd.FilterIndex)
                {
                    case 1: // 
                        Stream stream = File.Open(ofd.FileName, FileMode.Open);
                        var binaryFormatter = new BinaryFormatter();
                        //!!!!
                        Flist = (FigureList)binaryFormatter.Deserialize(stream);
                        //MessageBox.Show("File imported successfully sucessfully!");

                        stream.Close();
                        pic.Invalidate();
                        break;
                    case 2: // Image
                        //pic.Image.Save(ofd.FileName, ImageFormat.Jpeg);
                        //MessageBox.Show("Image imported successfully sucessfully!");
                        MessageBox.Show("Not implemented!");
                        break;

                    case 3: // All

                        break;
                }
            }
            clearSelection(true);
        }

        static Point set_point(PictureBox pb, Point p)
        {
            float pX = 1f * pb.Image.Width / pb.Width;
            float pY = 1f * pb.Image.Height / pb.Height;
            return new Point((int)(p.X * pX), (int)(p.Y * pY));
        }
       
        private void textBoxForTesting_Click(object sender, EventArgs e)
        {
            currSelect = FigureSelection.None;
        }

        private void btn_EditObject_Click(object sender, EventArgs e)
        {
            currSelect = FigureSelection.EditObject;
            // if (Flist.NextIndex > 0)
            showEditMenu();
        }

        private void btn_change_clr_Click(object sender, EventArgs e) //change stroke color
        {
            //currSelect = FigureSelection.Point;
            //Figure.SELECTED_COLOR = New_Color;
            //currSelect = FigureSelection.Point;
            if (selectedFigureIndex != -1)
                Flist[selectedFigureIndex].StrokeColor = New_Color;
            //pic.Invalidate();
            showEditMenu();
            clearSelection(false);
        }
        private void btn_strokeWidth_Click(object sender, EventArgs e)
        {
            currSelect = FigureSelection.StrokeWidth;
            if (Flist[selectedFigureIndex].IsSelected)
            {
                Flist[selectedFigureIndex].StrokeWidth++;
                pic.Invalidate();
            }
            cB_selestSize.Show();
            clearSelection(false);
        }

        private void btn_changeSize_Click(object sender, EventArgs e)
        {
            showEditMenu();
            clearSelection(false);
        }
        #endregion

        /***************************    Others       *******************************/
        #region other methods
        public void saveCurrentState()
        {
            if (currentFlistIndex < FHistoryList.Count - 1)
                FHistoryList.RemoveRange(currentFlistIndex + 1, FHistoryList.Count - currentFlistIndex - 1);
            currentFlistIndex++;
            FHistoryList.Add(new FigureList(Flist));
            txtBoxForTesting.Text = "i=" + currentFlistIndex + " count=";
            txtBoxForTesting.Text += FHistoryList != null ? " " + FHistoryList.Count : "0";
            updateUndoRedoEnabled();
        }


        public void clearSelectedFig()
        {
            if (selectedFigureIndex >= 0 && selectedFigureIndex < Flist.NextIndex)
            {
                (Flist[selectedFigureIndex]).IsSelected = false;
                selectedFigureIndex = -1;
            }
        }

        public void showEditMenu()
        {
            btn_fill.Show();
            btn_changeStkClr.Show();
        }

        public void clearSelection(bool clearAll)
        {
            paint = false;
            if (clearAll)
            {
                clearSelectedFig();
                btn_fill.Hide();
                btn_changeStkClr.Hide();
                Figure.SELECTED_COLOR = Color.DimGray;
                isErased = false;
            }
        }

        private void cB_selestSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //new_width = (int)cB_selestSize.SelectedIndexChanged; //need to fix
        }

        private void updateUndoRedoEnabled()
        {
            if (currentFlistIndex == 0)
                btn_undo.Enabled = false;
            else
                btn_undo.Enabled = true;
            if (currentFlistIndex < FHistoryList.Count - 1)
                btn_redo.Enabled = true;
            else
                btn_redo.Enabled = false;
        }
        private void validate(Bitmap bm, Stack<Point> sp, int x, int y, Color oldColor, Color newColor)
        {
            Color cx = bm.GetPixel(x, y);
            if (cx == oldColor)
            {
                sp.Push(new Point(x, y));
                bm.SetPixel(x, y, newColor);
            }
        }
        //public void Fill(Bitmap bm,int x ,int y,Color newColor )
        //{
        //    Color oldColor = bm.GetPixel(x, y);
        //    Stack<Point> pixel = new Stack<Point>();
        //    pixel.Push(new Point(x,y));
        //    bm.SetPixel(x, y, newColor);
        //    if (oldColor == newColor) return;
        //    while(pixel.Count>0)
        //    {
        //        Point p = (Point)pixel.Pop();
        //        if (p.X > 0 && p.Y > 0 && p.X < bm.Width - 1 && p.Y < bm.Height - 1)
        //        {
        //            validate(bm, pixel, p.X - 1, p.Y, oldColor, newColor);
        //            validate(bm, pixel, p.X , p.Y-1, oldColor, newColor);
        //            validate(bm, pixel, p.X+ 1, p.Y, oldColor, newColor);
        //            validate(bm, pixel, p.X , p.Y+1, oldColor, newColor);
        //        }
        //    }
        //}


        #endregion

    }
}
