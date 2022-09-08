using OOPproject;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

[Serializable]
public class Circle : Figure
{
    const float DEFAULT_RADIUS = 1f;
    const float MAX_RADIUS = Form1.DEFAULT_FORM_HEIGHT > Form1.DEFAULT_FORM_WIDTH ? Form1.DEFAULT_FORM_HEIGHT : Form1.DEFAULT_FORM_WIDTH;
    float _radius;
    MyPoint _topLeft;
    bool toChangeTopLeft = true;
    public Circle() : this(10, 10, DEFAULT_RADIUS) { }
    public Circle(MyPoint MyPoint, float radius = DEFAULT_RADIUS, int strokeWidth = 1)
    {
        MyPoint = new MyPoint(MyPoint);
        Radius = radius;
        TopLeft = null;
        toChangeTopLeft = false;
        StrokeColor = Color.Black;
        FillColor = Color.Black;
        StrokeWidth = strokeWidth;
    }
    public Circle(Color strokeColor, Color fillColor, MyPoint MyPoint, float radius = DEFAULT_RADIUS, int strokeWidth = 1)
    {
        MyPoint = new MyPoint(MyPoint);
        Radius = radius;
        TopLeft = null;
        toChangeTopLeft = false;
        StrokeColor = strokeColor;
        FillColor = fillColor;
        StrokeWidth = strokeWidth;
    }
    public Circle(float x, float y, float radius = DEFAULT_RADIUS, int strokeWidth = 1) 
    {
        MyPoint = new MyPoint(x, y);
        Radius = radius;
        TopLeft = null;
        toChangeTopLeft = false; 
        StrokeColor = Color.Black;
        FillColor = Color.Black;
        StrokeWidth = strokeWidth;
    }
    public MyPoint Center 
    { 
        get 
        { 
            return MyPoint; 
        } 
        set 
        {
            if (value.X - _radius < 0)
            {
                value.X = _radius;
            }
            if (value.Y - _radius < 0)
            {
                value.Y = _radius;
            } 
                MyPoint.X = value.X;
                MyPoint.Y = value.Y;
            TopLeft = null;
            InitializePath();
        } 
    }
    public float Radius
    {
        get
        {
            return _radius;
        }
        set
        {
            if (value >= 0 && value < MAX_RADIUS)
            {
                _radius = value;
                InitializePath();
            }

        }
    }

    public MyPoint TopLeft
    {
        get
        {
            return _topLeft;
        }
        private set
        {
            if (value == null && toChangeTopLeft) 
                InititalizeTopLeft(-1, -1);
             else if (toChangeTopLeft)
                InititalizeTopLeft(value.X, value.Y);
        }

    }

    public MyPoint BottomRight
    {
        get
        {
            return new MyPoint(X +_radius, Y +_radius);
        }
        private set
        {
            MyPoint bottomRight = new MyPoint(value.X, value.Y);
            Line topLToBottomR = new Line(TopLeft.X, TopLeft.Y, bottomRight.X, bottomRight.Y);
            Radius = topLToBottomR.Distance / 2; ;
            X = bottomRight.X - Radius;
            Y = bottomRight.Y - Radius;
        }
    }

    private void InititalizeTopLeft(float x, float y)
    {
        
        if (_topLeft == null) _topLeft = new MyPoint();
        if (x != -1 && y != -1)
        {
            _topLeft.X = x;
            _topLeft.Y = y;
        }
        else
        {
            _topLeft.X = X - _radius;
            _topLeft.Y = Y - _radius;
        }
    }

    public override void InitializePath()
    {
        base.InitializePath();
        if (TopLeft == null)
            InititalizeTopLeft(-1, -1);
        _path.AddEllipse(TopLeft.X, TopLeft.Y, Radius*2, Radius*2);
    }
    public override void Draw(Graphics g)
    {
        if(_path == null) InitializePath();
        if (IsSelected)
        {
            Pen surrundingRec = new Pen(SELECTED_COLOR, StrokeWidth / 2);
            surrundingRec.DashStyle = DashStyle.Dash;
            g.DrawRectangle(surrundingRec, TopLeft.X, TopLeft.Y, Radius * 2, Radius * 2);  // surrounding rectangle
        }
        if (TopLeft == null) // for deserialize
        {
            toChangeTopLeft = true;
            TopLeft = null;
            toChangeTopLeft = false;
        }
        SolidBrush br = new SolidBrush(FillColor);
        g.FillEllipse(br, TopLeft.X , TopLeft.Y, Radius * 2, Radius * 2);
        if (Pen == null) 
                Pen = new Pen(StrokeColor, StrokeWidth);  // for desrialize
        g.DrawEllipse(Pen, TopLeft.X, TopLeft.Y , Radius * 2, Radius * 2);
    }
    public override bool isOnPath(float x, float y)
    {
        if (Pen == null)
            Pen = new Pen(StrokeColor, StrokeWidth); // for desrialize
        if (_path == null)
            InitializePath(); // for desrialize
        return _path.IsOutlineVisible(x, y, Pen);
    }
    public override bool isInside(float x, float y)
    {
        return new Line(x,y,X,Y).Distance < Radius || isOnPath(x,y);
    }
    public override bool isInsideSurrounding(float x, float y)
    {
        RectangleF recf = new RectangleF(TopLeft.X, TopLeft.Y, Radius * 2, Radius * 2);  // surrounding rectangle
        return recf.Contains(x, y);
    }
    public override void Change(float x, float y)
    {
        toChangeTopLeft = false;
        BottomRight = new MyPoint(x, y);
    }

    public override void Move(float offsetX, float offsetY)
    {
        toChangeTopLeft = true;
        Center = new MyPoint(Center.X + offsetX, Center.Y + offsetY);
        toChangeTopLeft = false;
    }
}
