
namespace OOPproject
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.pic_color = new System.Windows.Forms.Button();
            this.btn_redo = new System.Windows.Forms.Button();
            this.btn_rhombus = new System.Windows.Forms.Button();
            this.Undo = new System.Windows.Forms.Button();
            this.pnl_temp = new System.Windows.Forms.Panel();
            this.ObjectsEraser = new System.Windows.Forms.Button();
            this.PenEraserBtn = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_line = new System.Windows.Forms.Button();
            this.btn_rect = new System.Windows.Forms.Button();
            this.btn_circle = new System.Windows.Forms.Button();
            this.btn_eraser = new System.Windows.Forms.Button();
            this.btn_pencil = new System.Windows.Forms.Button();
            this.btn_fill = new System.Windows.Forms.Button();
            this.btn_color = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pic = new System.Windows.Forms.PictureBox();
            this.textBoxForTesting = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.pnl_temp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.pic_color);
            this.panel1.Controls.Add(this.btn_redo);
            this.panel1.Controls.Add(this.btn_rhombus);
            this.panel1.Controls.Add(this.Undo);
            this.panel1.Controls.Add(this.pnl_temp);
            this.panel1.Controls.Add(this.btn_clear);
            this.panel1.Controls.Add(this.btn_save);
            this.panel1.Controls.Add(this.btn_line);
            this.panel1.Controls.Add(this.btn_rect);
            this.panel1.Controls.Add(this.btn_circle);
            this.panel1.Controls.Add(this.btn_eraser);
            this.panel1.Controls.Add(this.btn_pencil);
            this.panel1.Controls.Add(this.btn_fill);
            this.panel1.Controls.Add(this.btn_color);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(928, 121);
            this.panel1.TabIndex = 0;
            // 
            // pic_color
            // 
            this.pic_color.BackColor = System.Drawing.Color.Black;
            this.pic_color.Enabled = false;
            this.pic_color.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.pic_color.FlatAppearance.BorderSize = 0;
            this.pic_color.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pic_color.ForeColor = System.Drawing.Color.Black;
            this.pic_color.Location = new System.Drawing.Point(20, 93);
            this.pic_color.Name = "pic_color";
            this.pic_color.Size = new System.Drawing.Size(48, 18);
            this.pic_color.TabIndex = 0;
            this.pic_color.UseVisualStyleBackColor = false;
            // 
            // btn_redo
            // 
            this.btn_redo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_redo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_redo.ForeColor = System.Drawing.Color.Wheat;
            this.btn_redo.Location = new System.Drawing.Point(19, 53);
            this.btn_redo.Name = "btn_redo";
            this.btn_redo.Size = new System.Drawing.Size(49, 32);
            this.btn_redo.TabIndex = 13;
            this.btn_redo.Text = "Redo";
            this.btn_redo.UseVisualStyleBackColor = true;
            this.btn_redo.Click += new System.EventHandler(this.btn_redo_Click);
            // 
            // btn_rhombus
            // 
            this.btn_rhombus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_rhombus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.btn_rhombus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btn_rhombus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_rhombus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_rhombus.ForeColor = System.Drawing.Color.Wheat;
            this.btn_rhombus.Image = global::OOPproject.Properties.Resources.rectangle;
            this.btn_rhombus.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_rhombus.Location = new System.Drawing.Point(719, 10);
            this.btn_rhombus.Name = "btn_rhombus";
            this.btn_rhombus.Size = new System.Drawing.Size(74, 63);
            this.btn_rhombus.TabIndex = 12;
            this.btn_rhombus.Text = "Rhombus";
            this.btn_rhombus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_rhombus.UseVisualStyleBackColor = true;
            this.btn_rhombus.Click += new System.EventHandler(this.btn_rhombus_Click);
            // 
            // Undo
            // 
            this.Undo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Undo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Undo.ForeColor = System.Drawing.Color.Wheat;
            this.Undo.Location = new System.Drawing.Point(19, 13);
            this.Undo.Name = "Undo";
            this.Undo.Size = new System.Drawing.Size(49, 32);
            this.Undo.TabIndex = 11;
            this.Undo.Text = "Undo";
            this.Undo.UseVisualStyleBackColor = true;
            this.Undo.Click += new System.EventHandler(this.Undo_Click);
            // 
            // pnl_temp
            // 
            this.pnl_temp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_temp.Controls.Add(this.ObjectsEraser);
            this.pnl_temp.Controls.Add(this.PenEraserBtn);
            this.pnl_temp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnl_temp.ForeColor = System.Drawing.Color.Transparent;
            this.pnl_temp.Location = new System.Drawing.Point(90, 83);
            this.pnl_temp.Name = "pnl_temp";
            this.pnl_temp.Size = new System.Drawing.Size(724, 32);
            this.pnl_temp.TabIndex = 10;
            // 
            // ObjectsEraser
            // 
            this.ObjectsEraser.ForeColor = System.Drawing.Color.Black;
            this.ObjectsEraser.Location = new System.Drawing.Point(318, 0);
            this.ObjectsEraser.Name = "ObjectsEraser";
            this.ObjectsEraser.Size = new System.Drawing.Size(89, 32);
            this.ObjectsEraser.TabIndex = 1;
            this.ObjectsEraser.Text = "Objects Eraser ";
            this.ObjectsEraser.UseVisualStyleBackColor = true;
            this.ObjectsEraser.Visible = false;
            this.ObjectsEraser.Click += new System.EventHandler(this.EraserObjects_Click);
            // 
            // PenEraserBtn
            // 
            this.PenEraserBtn.ForeColor = System.Drawing.Color.Black;
            this.PenEraserBtn.Location = new System.Drawing.Point(213, 0);
            this.PenEraserBtn.Name = "PenEraserBtn";
            this.PenEraserBtn.Size = new System.Drawing.Size(89, 32);
            this.PenEraserBtn.TabIndex = 0;
            this.PenEraserBtn.Text = "Pen Eraser ";
            this.PenEraserBtn.UseVisualStyleBackColor = true;
            this.PenEraserBtn.Visible = false;
            this.PenEraserBtn.Click += new System.EventHandler(this.PenEraserBtn_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.BackColor = System.Drawing.Color.Gray;
            this.btn_clear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_clear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.btn_clear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btn_clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_clear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_clear.ForeColor = System.Drawing.Color.Wheat;
            this.btn_clear.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_clear.Location = new System.Drawing.Point(833, 71);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(80, 42);
            this.btn_clear.TabIndex = 8;
            this.btn_clear.Text = "Clear";
            this.btn_clear.UseVisualStyleBackColor = false;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.Gray;
            this.btn_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_save.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.btn_save.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_save.ForeColor = System.Drawing.Color.Wheat;
            this.btn_save.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_save.Location = new System.Drawing.Point(833, 12);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(80, 43);
            this.btn_save.TabIndex = 9;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_line
            // 
            this.btn_line.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_line.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.btn_line.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btn_line.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_line.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_line.ForeColor = System.Drawing.Color.Wheat;
            this.btn_line.Image = global::OOPproject.Properties.Resources.line;
            this.btn_line.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_line.Location = new System.Drawing.Point(630, 10);
            this.btn_line.Name = "btn_line";
            this.btn_line.Size = new System.Drawing.Size(74, 63);
            this.btn_line.TabIndex = 7;
            this.btn_line.Text = "Line";
            this.btn_line.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_line.UseVisualStyleBackColor = true;
            this.btn_line.Click += new System.EventHandler(this.btn_line_Click);
            // 
            // btn_rect
            // 
            this.btn_rect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_rect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.btn_rect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btn_rect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_rect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_rect.ForeColor = System.Drawing.Color.Wheat;
            this.btn_rect.Image = global::OOPproject.Properties.Resources.rectangle;
            this.btn_rect.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_rect.Location = new System.Drawing.Point(539, 10);
            this.btn_rect.Name = "btn_rect";
            this.btn_rect.Size = new System.Drawing.Size(74, 63);
            this.btn_rect.TabIndex = 6;
            this.btn_rect.Text = "Rectangle";
            this.btn_rect.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_rect.UseVisualStyleBackColor = true;
            this.btn_rect.Click += new System.EventHandler(this.btn_rect_Click);
            // 
            // btn_circle
            // 
            this.btn_circle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_circle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.btn_circle.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btn_circle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_circle.ForeColor = System.Drawing.Color.Wheat;
            this.btn_circle.Image = global::OOPproject.Properties.Resources.circle;
            this.btn_circle.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_circle.Location = new System.Drawing.Point(449, 10);
            this.btn_circle.Name = "btn_circle";
            this.btn_circle.Size = new System.Drawing.Size(73, 63);
            this.btn_circle.TabIndex = 5;
            this.btn_circle.Text = "Circle";
            this.btn_circle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_circle.UseVisualStyleBackColor = true;
            this.btn_circle.Click += new System.EventHandler(this.btn_circle_Click);
            // 
            // btn_eraser
            // 
            this.btn_eraser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_eraser.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.btn_eraser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btn_eraser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_eraser.ForeColor = System.Drawing.Color.Wheat;
            this.btn_eraser.Image = global::OOPproject.Properties.Resources.eraser;
            this.btn_eraser.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btn_eraser.Location = new System.Drawing.Point(364, 10);
            this.btn_eraser.Name = "btn_eraser";
            this.btn_eraser.Size = new System.Drawing.Size(70, 63);
            this.btn_eraser.TabIndex = 4;
            this.btn_eraser.Text = "Eraser";
            this.btn_eraser.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_eraser.UseVisualStyleBackColor = true;
            this.btn_eraser.Click += new System.EventHandler(this.btn_eraser_Click);
            // 
            // btn_pencil
            // 
            this.btn_pencil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_pencil.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.btn_pencil.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btn_pencil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_pencil.ForeColor = System.Drawing.Color.Wheat;
            this.btn_pencil.Image = global::OOPproject.Properties.Resources.pencil;
            this.btn_pencil.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btn_pencil.Location = new System.Drawing.Point(280, 10);
            this.btn_pencil.Name = "btn_pencil";
            this.btn_pencil.Size = new System.Drawing.Size(71, 63);
            this.btn_pencil.TabIndex = 3;
            this.btn_pencil.Text = "Pencil";
            this.btn_pencil.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_pencil.UseVisualStyleBackColor = true;
            this.btn_pencil.Click += new System.EventHandler(this.btn_pencil_Click);
            // 
            // btn_fill
            // 
            this.btn_fill.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_fill.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.btn_fill.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btn_fill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_fill.ForeColor = System.Drawing.Color.Wheat;
            this.btn_fill.Image = global::OOPproject.Properties.Resources.bucket;
            this.btn_fill.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_fill.Location = new System.Drawing.Point(194, 10);
            this.btn_fill.Name = "btn_fill";
            this.btn_fill.Size = new System.Drawing.Size(72, 63);
            this.btn_fill.TabIndex = 2;
            this.btn_fill.Text = "Fill";
            this.btn_fill.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_fill.UseVisualStyleBackColor = true;
            this.btn_fill.Click += new System.EventHandler(this.btn_fill_Click);
            // 
            // btn_color
            // 
            this.btn_color.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_color.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.btn_color.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btn_color.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_color.ForeColor = System.Drawing.Color.Wheat;
            this.btn_color.Image = global::OOPproject.Properties.Resources.color;
            this.btn_color.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_color.Location = new System.Drawing.Point(108, 10);
            this.btn_color.Name = "btn_color";
            this.btn_color.Size = new System.Drawing.Size(71, 63);
            this.btn_color.TabIndex = 1;
            this.btn_color.Text = "Color";
            this.btn_color.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_color.UseVisualStyleBackColor = true;
            this.btn_color.Click += new System.EventHandler(this.btn_color_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 633);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(928, 28);
            this.panel2.TabIndex = 1;
            // 
            // pic
            // 
            this.pic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic.Location = new System.Drawing.Point(0, 121);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(928, 512);
            this.pic.TabIndex = 0;
            this.pic.TabStop = false;
            this.pic.Paint += new System.Windows.Forms.PaintEventHandler(this.pic_Paint);
            this.pic.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pic_MouseClick);
            this.pic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic_MouseDown);
            this.pic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic_MouseMove);
            this.pic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pic_MouseUp);
            // 
            // textBoxForTesting
            // 
            this.textBoxForTesting.Location = new System.Drawing.Point(828, 610);
            this.textBoxForTesting.Name = "textBoxForTesting";
            this.textBoxForTesting.ReadOnly = true;
            this.textBoxForTesting.Size = new System.Drawing.Size(100, 23);
            this.textBoxForTesting.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 661);
            this.Controls.Add(this.textBoxForTesting);
            this.Controls.Add(this.pic);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.pnl_temp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button pic_color;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Button btn_color;
        private System.Windows.Forms.Button btn_fill;
        private System.Windows.Forms.Button btn_eraser;
        private System.Windows.Forms.Button btn_pencil;
        private System.Windows.Forms.Button btn_line;
        private System.Windows.Forms.Button btn_rect;
        private System.Windows.Forms.Button btn_circle;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Panel pnl_temp;
        private System.Windows.Forms.TextBox textBoxForTesting;
        private System.Windows.Forms.Button btn_redo;
        private System.Windows.Forms.Button btn_rhombus;
        private System.Windows.Forms.Button Undo;
        private System.Windows.Forms.Button PenEraserBtn;
        private System.Windows.Forms.Button ObjectsEraser;
    }
}

