using OOPproject;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

[Serializable]
public class Line: Figure
{
    MyPoint _point2;
    public Line(float x1 = 0, float y1 = 0, float x2 = 0, float y2 = 0)
    {
        MyPoint = new MyPoint(x1, y1);
        Point2 = new MyPoint(x2, y2);
        StrokeColor = Color.Black;
        StrokeWidth = 1;
        FillColor = Color.Transparent;
    } 
    public Line(Color strokeColor, float x1 = 0, float y1 = 0, float x2 = 0, float y2 = 0, int strokeWidth = 1)
    {
        MyPoint = new MyPoint(x1, y1);
        Point2 = new MyPoint(x2, y2);
        StrokeColor = strokeColor;
        StrokeWidth = strokeWidth;
        FillColor = Color.Transparent;
    }
    public Line(Line line)
    {
        Point1 = new MyPoint(line.X1, line.Y1);
        Point2 = new MyPoint(line.X2, line.Y2);
        StrokeColor = line.StrokeColor;
        StrokeWidth = line.StrokeWidth;
        FillColor = line.FillColor;
    }
    // Properties
    public MyPoint Point1
    {
        get { return MyPoint; }
        set
        {
            if (MyPoint == null) MyPoint = new MyPoint();
            MyPoint.X = value.X;
            MyPoint.Y = value.Y;
        }
    }
    public MyPoint Point2
    {
        get { return _point2; }
        set
        {
            if (_point2 == null) _point2 = new MyPoint();
            _point2.X = value.X;
            _point2.Y = value.Y;
            InitializePath();
        }
    }
    public float X1
    {
        get { return MyPoint.X; }
        set
        {
            MyPoint.X = value;
            InitializePath();
        }
    }
    public float Y1
    {
        get { return MyPoint.Y; }
        set
        {
            MyPoint.Y = value;
            InitializePath();
        }
    }
    public float X2
    {
        get { return Point2.X; }
        set
        {
            _point2.X = value;
            InitializePath();
        }
    }
    public float Y2
    {
        get { return _point2.Y; }
        set
        {
            _point2.Y = value;
            InitializePath();
        }
    }
    public float Distance
    {
        get
        { // √[( y2 –  y1)² + ( x1 –  x2)²]
            return MathF.Sqrt(MathF.Pow(MathF.Abs(Y2 - Y1), 2) + MathF.Pow(MathF.Abs(X2 - X1), 2));
        }
    }

    public override void InitializePath()
    {
        base.InitializePath();
        _path.AddLine(Point1.X, Point1.Y, Point2.X, Point2.Y);
    }

    public override void Draw(Graphics g)
    {
        if (_path == null)
            InitializePath(); // for desrialize
        if (IsSelected)
        {
            Pen surrundingRec = new Pen(SELECTED_COLOR, StrokeWidth / 2);
            surrundingRec.DashStyle = DashStyle.Dash;
            g.DrawRectangle(surrundingRec, _path.GetBounds().X, _path.GetBounds().Y, _path.GetBounds().Width, _path.GetBounds().Height);  // surrounding rectangle
        }
        if (Pen == null)
        {
            Pen = new Pen(StrokeColor, StrokeWidth); // for desrialize
        }
        g.DrawLine(Pen, Point1.X, Point1.Y, Point2.X, Point2.Y);
      
        
    }

    public override bool isInside(float x, float y)
    {
        return isOnPath(x, y);
    }
    public override bool isOnPath(float x, float y)
    {
        if (_path == null)
            InitializePath();
        return _path.IsOutlineVisible(x, y, Pen);
    }

    public override bool isInsideSurrounding(float x, float y)
    {
        RectangleF recf = new RectangleF(_path.GetBounds().X, _path.GetBounds().Y, _path.GetBounds().Width, _path.GetBounds().Height);  // surrounding rectangle
        return recf.Contains(x, y);
    }

    public override void Change(float x, float y)
    {
        X2 = x;
        Y2 = y;
    }
    public override void Move(float x, float y)
    {
        X1 += x;
        X2 += x;
        Y1 += y;
        Y2 += y;
    }

}
