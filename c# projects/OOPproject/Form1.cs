using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

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
            KeyPreview = true;
        }

        private void Form1_Shown(Object sender, EventArgs e)
        {
            FHistoryList.Add(new FigureList(Flist));
            currentFlistIndex++;
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
        private int _selectedWidth = -1;
        Bitmap bm; //readonly(?)
        Graphics g; 
        ColorDialog cd = new ColorDialog();
        Pen pen1 = new Pen(Color.Black, DEFAULT_STROKE_WIDTH);
        List<FigureList> FHistoryList = new List<FigureList>();
        FigureList Flist = new FigureList();
        SelectedMenuButton currSelect = SelectedMenuButton.None;
        MyPoint mouseDownPoint = new MyPoint();
        private bool isShiftPressed;
        private bool isControlPressed;

        #region main events

        /***************************  Mouse Down inside Pic    *******************************/

        private void pic_MouseDown(object sender, MouseEventArgs e)
        { 
            paint = true;
            isMouseMoved = false;
            isMouseDownOnPic = true;
            mouseDownPoint.X = e.X;
            mouseDownPoint.Y = e.Y;

            if (Flist.NextIndex == 0 || 
                (selectedFigureIndex >= 0 && 
                (!Flist[selectedFigureIndex].isInside(e.X, e.Y) && !Flist[selectedFigureIndex].isInsideSurrounding(e.X, e.Y)))) 
            {
                // flist empty OR there is selected figure AND (either click outside object AND outside surrounding rectangle)
                clearSelectedFig();
            }
        }

        /***************************    Mouse Move       *******************************/

        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDownOnPic && selectedFigureIndex < 0 ) // mouse move after mouse down and figure is not selected
            {
                figureIndex = Flist.NextIndex;
                createFigure(currSelect, e.X, e.Y); //Create Figure
            }
            if (paint && figureIndex != -1) //mouse is down on picture box and there is no figures created
            {
                Figure c = (Figure)Flist[figureIndex];
                switch (currSelect) // which menu button selected
                {
                    case SelectedMenuButton.Ellipse:
                    case SelectedMenuButton.Line: 
                    case SelectedMenuButton.Rhombus:
                    case SelectedMenuButton.Pencil:
                    case SelectedMenuButton.PerfectCircle:
                        if (selectedFigureIndex < 0) // none object is selected
                            c.Change(e.X, e.Y); // change created figure
                        break;
                    case SelectedMenuButton.Rectangle:
                        if (selectedFigureIndex < 0)
                        { // none object is selected
                            if (!isShiftPressed) 
                                c.Change(e.X, e.Y);
                            else
                                c.Change(e.X, -1f); // Change the Rectangle to square figure
                        }
                        break;
                }

                pic.Invalidate();
            }
            if (paint) // mouse was down inside picture box
            {
                    isMouseMoved = true;
                if ((selectedFigureIndex >= 0 && selectedFigureIndex < Flist.NextIndex) && (Flist[selectedFigureIndex].isInsideSurrounding(e.X, e.Y)))
                { // figure was selected and mouse clicked inside surrounding: Move object
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
            if (selectedFigureIndex >= 0 && paint &&  isMouseMoved &&  !isFigureMoved) //  figure is selected, mouse pressed and moved but figure not moved : Clear Selected Figure
            {
                clearSelectedFig();
            }
            switch (currSelect) // which menu button selected
            {
                case SelectedMenuButton.Ellipse:
                case SelectedMenuButton.Rectangle:
                case SelectedMenuButton.Line: 
                case SelectedMenuButton.Rhombus: 
                case SelectedMenuButton.Pencil:
                case SelectedMenuButton.PerfectCircle:
                    if (isFiguredCreated)
                    {
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
                        (Flist[i]).IsSelected = foundFig = true;
                        break;
                    }
                }
                if (!foundFig)
                {
                    clearSelectedFig();
                }
                pic.Invalidate();
            }
            isMouseMoved = paint = isMouseDownOnPic = false;
        }

        /***************************    Mouse Click       *******************************/

        private void pic_MouseClick(object sender, MouseEventArgs e)
        {
            clearSelection(false);
            if(currSelect == SelectedMenuButton.Fill && Flist.Find(e.X, e.Y) != -1 && // mouse clicked inside picture box AND inside figure AND fill menu button is selected : Change to selcted color
                selectedFigureIndex >= 0 && selectedFigureIndex < Flist.NextIndex)
            { 
                Flist[selectedFigureIndex].FillColor = New_Color;
            }
        }

        private void pic_Paint(object sender, PaintEventArgs e) //Invalidate the picture box
        {
            Graphics paintGraphics = e.Graphics;
            paintGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Flist.DrawAll(paintGraphics);
        }

        #endregion

        /***************************    Menu clicks events       *******************************/

        #region Menu clicks events Methods
        private void btn_clear_Click(object sender, EventArgs e) // Clear all figures
        {
            g.Clear(Color.White);
            pic.Image = bm;
            currSelect = SelectedMenuButton.Clear;
            pic.Text = Flist.NextIndex + "";
            Flist.Clear();
            FHistoryList.Clear();
            FHistoryList.Add(new FigureList(Flist));
            currentFlistIndex = 0;
            updateUndoRedoEnabled();
            clearSelection(true);
        }
        private void btn_color_Click(object sender, EventArgs e) // Change selected color through the color picker dialog
        {
            cd.ShowDialog();
            currSelect = SelectedMenuButton.Color;
            New_Color = cd.Color;
            pic_color.BackColor = cd.Color;
            pen1.Color = cd.Color;
            clearSelection(false);
            pic.Invalidate();
        }
        private void btn_pencil_Click(object sender, EventArgs e) // Menu button Pencil click event
        {
            currSelect = SelectedMenuButton.Pencil;
            clearSelection(true);
        }

        private void btn_circle_Click(object sender, EventArgs e)// Menu button Circle click event
        {
            currSelect = SelectedMenuButton.PerfectCircle;
            paint = false;
            clearSelection(true);

        }
        private void btn_rect_Click(object sender, EventArgs e)// Menu button Rectangle click event
        {
            currSelect = SelectedMenuButton.Rectangle;
            clearSelection(true);
        }
        private void btn_line_Click(object sender, EventArgs e)// Menu button Line click event
        {
            currSelect = SelectedMenuButton.Line;
            clearSelection(true);
        }
        private void btn_fill_Click(object sender, EventArgs e)// Menu button Fill click event
        {
            currSelect = SelectedMenuButton.Fill;
            showEditMenu();
            clearSelection(false);
        }
        private void btn_rhombus_Click(object sender, EventArgs e) // Menu button Rhombus click event
        {
            currSelect = SelectedMenuButton.Rhombus;
            clearSelection(true);
        }

        private void btn_object_eraser_Click(object sender, EventArgs e) // Menu button Eraser click event
        {
            currSelect = SelectedMenuButton.ObjectEraser;
            if(selectedFigureIndex >= 0 && selectedFigureIndex < Flist.NextIndex && (Flist[selectedFigureIndex]).IsSelected) // figured is selected: Remove the selected figure
            {
                Flist[selectedFigureIndex].IsSelected = false;
                Flist.Remove(selectedFigureIndex);
                pic.Text = Flist.NextIndex + "";
                saveCurrentState();
                pic.Invalidate();
            }
            clearSelection(true);
        }


        private void btn_undo_Click(object sender, EventArgs e) // Menu button Undo click event
        {
            currSelect = SelectedMenuButton.Undo;
            if (currentFlistIndex > 0)
            {
                currentFlistIndex--;
                Flist = new FigureList(FHistoryList[currentFlistIndex]);
                pic.Invalidate();
            }
            updateUndoRedoEnabled();
            clearSelection(true);
        }

        private void btn_redo_Click(object sender, EventArgs e) // Menu button Redo click event
        {
            currSelect = SelectedMenuButton.Redo;
            if (currentFlistIndex < FHistoryList.Count - 1)
            {
                currentFlistIndex++;
                Flist = new FigureList(FHistoryList[currentFlistIndex]);
                pic.Invalidate();
            }
            updateUndoRedoEnabled();
            clearSelection(true);
        }

        private void btn_save_Click(object sender, EventArgs e) // Menu button Save click event
        {
            currSelect = SelectedMenuButton.Save;
            var sfd = new SaveFileDialog();
            sfd.Filter = "Model (*.mdl)|*.mdl|Image (*.png)|*.png";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                switch (sfd.FilterIndex)
                {
                    case 1: //mdl file
                        IFormatter formatter = new BinaryFormatter();
                        using (Stream stream = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
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
                }
            }
            clearSelection(true);
            currSelect = SelectedMenuButton.None;
        }

        private void btn_import_Click(object sender, EventArgs e) // Menu button Import click event
        {
            currSelect = SelectedMenuButton.Import;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Model (*.mdl)|*.mdl";
            if (ofd.ShowDialog() == DialogResult.OK && ofd.FilterIndex == 1) // mdl file
            {
                Stream stream = File.Open(ofd.FileName, FileMode.Open);
                var binaryFormatter = new BinaryFormatter();
                        
                Flist = (FigureList)binaryFormatter.Deserialize(stream);
                stream.Close();
                pic.Invalidate();
            }
            clearSelection(true);
        }
       
        private void textBoxForTesting_Click(object sender, EventArgs e) //Text box for testing clicked
        {
            currSelect = SelectedMenuButton.None;
        }

        private void btn_EditObject_Click(object sender, EventArgs e) // Menu button Edit Object click event: Show the edit options
        {
            currSelect = SelectedMenuButton.EditObject;
            showEditMenu();
        }

        private void btn_change_clr_Click(object sender, EventArgs e) // Menu button Stroke Color click event
        {
            if (selectedFigureIndex != -1)
                Flist[selectedFigureIndex].StrokeColor = New_Color;
            pic.Invalidate();
            showEditMenu();
            clearSelection(false);
        }
        private void btn_strokeWidth_Click(object sender, EventArgs e) // Menu button Stroke Width click event
        {
            cbSelectSize.Show();
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
                Flist[figureIndex].StrokeWidth = _selectedWidth > 0 ?_selectedWidth: DEFAULT_STROKE_WIDTH;
                Flist[figureIndex].FillColor = Color.Transparent;
                isFiguredCreated = true;
            }
        }

        public void saveCurrentState() // Add current state to Figure History List (For undo / redo options)
        {
            if (currentFlistIndex < FHistoryList.Count - 1)
                FHistoryList.RemoveRange(currentFlistIndex + 1, FHistoryList.Count - currentFlistIndex - 1);
            currentFlistIndex++;
            FHistoryList.Add(new FigureList(Flist));
            updateUndoRedoEnabled();
        }


        public void clearSelectedFig() // remove the selection of figure
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

        public void clearSelection(bool clearAll) // initialize the variable to their start point
        {
            paint = false;
            if (clearAll)
            {
                clearSelectedFig();
                selectedFigureIndex = -1;
                btn_fill.Hide();
                btn_changeStkClr.Hide();
                Figure.SELECTED_COLOR = Color.DimGray;
                paint = false;
                isFigureMoved = false;
                isMouseMoved = false;
                isMouseDownOnPic = false;
                isFiguredCreated = false;
                figureIndex = -1;
            }
        }

        private void cbSelectSize_SelectedIndexChanged(object sender, EventArgs e) // Event for changing Stroke width from combo box list
        {
           _selectedWidth = int.Parse(cbSelectSize.SelectedItem.ToString());
            cbSelectSize.Visible = false;
        }

        private void updateUndoRedoEnabled() // Update the redo and undo buttons states (depends on the current state from history list)
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
    
        #endregion

        private void Form1_KeyDown(object sender, KeyEventArgs e) // Keyboard  down event
        {
            switch (e.KeyCode)
            {
                case Keys.ShiftKey:
                    isShiftPressed = true;
                    break;
                case Keys.ControlKey:
                    isControlPressed = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e) // Keyboard up event
        {
            switch (e.KeyCode)
            {
                case Keys.ShiftKey:
                    isShiftPressed = false;
                    break;
                case Keys.ControlKey:
                    isControlPressed = false;
                    break;
                case Keys.Z: // Control + Z
                    if (isControlPressed) // 
                        btn_undo_Click(sender, e);
                        break;
                case Keys.Y: // Control + Y
                    if (isControlPressed)
                        btn_redo_Click(sender, e);
                        break;
            }
        }
    }
}
