using System;
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

        public enum SelectedMenuButton
        {
            EditObject, None, 
            Pencil, Ellipse, PerfectCircle, Rectangle, Line, Rhombus, 
            ObjectEraser, Fill, Color, Clear, Undo, Redo, StrokeWidth, ChangeStrokeColor,
            Import, Save            
        }
        public const int DEFAULT_STROKE_WIDTH = 5;
        int new_width = DEFAULT_STROKE_WIDTH;
        public const int DEFAULT_FORM_WIDTH = 950;
        public const int DEFAULT_FORM_HEIGHT = 700;
        Color New_Color = Color.Black; //Default Stroke Color
        bool paint = false;
        bool isFigureMoved = false;
        bool isErased = false;
        private bool isMouseMoved = false;
        private bool isMouseDownOnPic = false;
        private bool isFiguredCreated = false;
        int figureIndex = -1;
        int selectedFigureIndex = -1;
        int currentFlistIndex = -1;
        Bitmap bm;
        Graphics g;
        ColorDialog cd = new ColorDialog();
        Pen pen1 = new Pen(Color.Black, DEFAULT_STROKE_WIDTH);
        List<FigureList> FHistoryList = new List<FigureList>();
        FigureList Flist = new FigureList();
        SelectedMenuButton currSelect = SelectedMenuButton.None;
        MyPoint mouseDownPoint = new MyPoint();
        
        #region main events

        /***************************  Mouse Down inside Pic    *******************************/

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            paint = true;
            isMouseMoved = false;
            isMouseDownOnPic = true;
            mouseDownPoint.X = e.X;
            mouseDownPoint.Y = e.Y;
            
            if (Flist.NextIndex == 0 || (selectedFigureIndex >= 0 && Flist[selectedFigureIndex].isInside(e.X, e.Y))) clearSelectedFig();
            //createFigure(currSelect, e.X, e.Y);
            pic.Text = Flist.NextIndex + "";
        }

        /***************************    Mouse Move       *******************************/

        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDownOnPic && selectedFigureIndex < 0 )
            {
                Logger.WriteLog("MouseMove");
                figureIndex = Flist.NextIndex;
                createFigure(currSelect, e.X, e.Y);
            }
            if (paint && figureIndex != -1)
            {
                Figure c = (Figure)Flist[figureIndex];
                switch (currSelect)
                {
                    case SelectedMenuButton.Ellipse:
                    case SelectedMenuButton.Rectangle:
                    case SelectedMenuButton.Line: // line
                    case SelectedMenuButton.Rhombus: //rhombus
                    case SelectedMenuButton.Pencil:
                    case SelectedMenuButton.PerfectCircle:
                        if (selectedFigureIndex < 0) // none object is selected
                            c.Change(e.X, e.Y);
                        break;
                    case SelectedMenuButton.ObjectEraser: // remove
                        int index = Flist.Find(e.X, e.Y);
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
                if ((selectedFigureIndex >= 0 && selectedFigureIndex < Flist.NextIndex) && (Flist[selectedFigureIndex].isInsideSurrounding(e.X, e.Y)))
                { // object was selected and mouse clicked inside surrounding
                    float offsetX = e.X - mouseDownPoint.X;
                    mouseDownPoint.X = e.X;
                    float offsetY = e.Y - mouseDownPoint.Y;
                    mouseDownPoint.Y = e.Y;
                    Flist[selectedFigureIndex].Move(offsetX, offsetY);
                    isFigureMoved = true;
                }
                pic.Invalidate();
            }
            isMouseDownOnPic = false;
        }
        /***************************    Mouse Up       *******************************/

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            //TODO removed last created figure if not drawn. if isMouseMoved = false and  paint = true ???
            //if (paint && !isMouseMoved)
            
            if (selectedFigureIndex >= 0 && paint &&  isMouseMoved &&  !isFigureMoved) // // figure is selected, mouse pressed and moved but figure not moved
                clearSelectedFig();

            //figureIndex = -1;
            switch (currSelect)
            {
                case SelectedMenuButton.Ellipse:
                case SelectedMenuButton.Rectangle:
                case SelectedMenuButton.Line: // line
                case SelectedMenuButton.Rhombus: //rhombus
                case SelectedMenuButton.Pencil:
                case SelectedMenuButton.PerfectCircle:
                    if (isFiguredCreated)
                    {
                        Logger.WriteLog("MouseUp Figured Created ");
                        isFiguredCreated = false;
                        saveCurrentState();
                    }
                    break;
                case SelectedMenuButton.ObjectEraser:
                    if (isErased) saveCurrentState();
                    isErased = false;
                    break;
            }
            if (paint && isFigureMoved && isFiguredCreated) // figure was moved
            {
                saveCurrentState();
                isFigureMoved = isFiguredCreated = false;

            }
            if (isMouseDownOnPic && !isMouseMoved) // mouse was down but didn't moved
            {
                Logger.WriteLog("isMouseDownOnPic && !isMouseMoved) // mouse was down but didn't moved");

                bool foundFig = false;
                for (int i = Flist.NextIndex - 1; i >= 0; i--)
                {
                    if (Flist[i].isInside(e.X, e.Y)) // found object
                    {
                        Logger.WriteLog("pic_MouseUp found object " + i);
                        pic.Text = ((Flist[i]).GetType()).ToString() + " [" + i + "]";
                        if (selectedFigureIndex != -1) // found object before this one, need to clear
                        {
                            clearSelectedFig();
                        }
                        selectedFigureIndex = i;
                        (Flist[i]).IsSelected = foundFig = true;
                        break;
                    }
                }
                if (!foundFig)
                    clearSelectedFig();
                pic.Invalidate();
            }
            isMouseMoved = paint = isMouseDownOnPic = false;
        }

        /***************************    Mouse Click       *******************************/

        private void pic_MouseClick(object sender, MouseEventArgs e)
        {
            clearSelection(false);
            switch (currSelect)
            {
                case SelectedMenuButton.Fill:
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
               // case SelectedMenuButton.ObjectEraser:
                    //index = Flist.Find(e.X, e.Y);
                    //if (index != -1)
                    //{
                    //    Flist.Remove(Flist.Find(e.X, e.Y));
                    //    pic.Text = Flist.NextIndex + "";
                    //    //MessageBox.Show("inside !" + ((Flist[i]).GetType()).ToString()); // when clicking inside with pencil pencil - just to test
                    //    pic.Invalidate();
                    //}
                  //  break;
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
            currSelect = SelectedMenuButton.Clear;
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
            currSelect = SelectedMenuButton.Color;
            New_Color = cd.Color;
            pic_color.BackColor = cd.Color;
            pen1.Color = cd.Color;
            clearSelection(false);
            pic.Invalidate();
        }
        private void btn_pencil_Click(object sender, EventArgs e)
        {
            currSelect = SelectedMenuButton.Pencil;
            clearSelection(true);
        }

        private void btn_circle_Click(object sender, EventArgs e)
        {
            currSelect = SelectedMenuButton.PerfectCircle;
            paint = false;
            clearSelection(true);

        }
        private void btn_rect_Click(object sender, EventArgs e)
        {
            currSelect = SelectedMenuButton.Rectangle;
            clearSelection(true);
        }
        private void btn_line_Click(object sender, EventArgs e)
        {
            currSelect = SelectedMenuButton.Line;
            clearSelection(true);
        }
        private void btn_fill_Click(object sender, EventArgs e)
        {
            currSelect = SelectedMenuButton.Fill;
            showEditMenu();
            clearSelection(false);
        }
        private void btn_rhombus_Click(object sender, EventArgs e)
        {
            currSelect = SelectedMenuButton.Rhombus;
            clearSelection(true);
        }

        private void btn_object_eraser_Click(object sender, EventArgs e)
        {
            currSelect = SelectedMenuButton.ObjectEraser;
            if((Flist[selectedFigureIndex]).IsSelected)
            {
                    Flist.Remove(selectedFigureIndex);
                    pic.Text = Flist.NextIndex + "";
                    //MessageBox.Show("inside !" + ((Flist[i]).GetType()).ToString()); // when clicking inside with pencil pencil - just to test
                    pic.Invalidate();
            }
            clearSelection(true);
        }


        private void btn_undo_Click(object sender, EventArgs e) //doesnt work for pencil yet
        {
            currSelect = SelectedMenuButton.Undo;
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
            currSelect = SelectedMenuButton.Redo;
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
            currSelect = SelectedMenuButton.Save;
            var sfd = new SaveFileDialog();
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
            currSelect = SelectedMenuButton.None;
        }

        private void btn_import_Click(object sender, EventArgs e)
        {
            currSelect = SelectedMenuButton.Import;
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

        //static Point set_point(PictureBox pb, Point p)
        //{
        //    float pX = 1f * pb.Image.Width / pb.Width;
        //    float pY = 1f * pb.Image.Height / pb.Height;
        //    return new Point((int)(p.X * pX), (int)(p.Y * pY));
        //}
       
        private void textBoxForTesting_Click(object sender, EventArgs e)
        {
            currSelect = SelectedMenuButton.None;
        }

        private void btn_EditObject_Click(object sender, EventArgs e)
        {
            currSelect = SelectedMenuButton.EditObject;
            showEditMenu();
        }

        private void btn_change_clr_Click(object sender, EventArgs e) //change stroke color
        {
            if (selectedFigureIndex != -1)
                Flist[selectedFigureIndex].StrokeColor = New_Color;
            pic.Invalidate();
            showEditMenu();
            clearSelection(false);
        }
        private void btn_strokeWidth_Click(object sender, EventArgs e)
        {
            currSelect = SelectedMenuButton.StrokeWidth;
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
        public void createFigure(SelectedMenuButton SelectedMenuButton, float x, float y)
        {
            bool tempFigureCreated = false;
            switch (currSelect)
            {
                case SelectedMenuButton.Pencil:
                    Flist[figureIndex] = new AbstractFig(x, y);
                    tempFigureCreated = true;
                    break;
                case SelectedMenuButton.Ellipse: // ellipse     
                    Flist[figureIndex] = new Ellipse(x, y, x, y);
                    tempFigureCreated = true;
                    break;
                case SelectedMenuButton.Rectangle: // rect
                    Flist[figureIndex] = new Rectangle(x, y, 0, 0);
                    tempFigureCreated = true;
                    break;
                case SelectedMenuButton.Line: // line
                    Flist[figureIndex] = new Line(x, y, x, y);
                    tempFigureCreated = true;
                    break;
                case SelectedMenuButton.Rhombus: //rhombus
                    Flist[figureIndex] = new Rhombus(x, y, 0, 0);
                    tempFigureCreated = true;
                    break;
                case SelectedMenuButton.PerfectCircle: //"perfect" circle
                    Flist[figureIndex] = new Circle(x, y, 0);
                    tempFigureCreated = true;
                    break;
                default:
                    paint = false;
                    break;
            }
            if (tempFigureCreated)
            {
                Flist[figureIndex].StrokeColor = New_Color;
                Flist[figureIndex].StrokeWidth = DEFAULT_STROKE_WIDTH;
                Flist[figureIndex].FillColor = Color.Transparent;
                isFiguredCreated = true;
            }
            Logger.WriteLog("createFigure --- figure count " + Flist.NextIndex);
            Logger.WriteLog("createFigure --- tempFigureCreated " + tempFigureCreated);
        }

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
            new_width = int.Parse(cB_selestSize.SelectedItem.ToString());
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
        #endregion

    }
}
