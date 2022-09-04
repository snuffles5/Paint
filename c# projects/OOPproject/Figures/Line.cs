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
        }
    }
    public float X1
    {
        get { return MyPoint.X; }
        set
        {
            MyPoint.X = value;
        }
    }
    public float Y1
    {
        get { return MyPoint.Y; }
        set
        {
            MyPoint.Y = value;
        }
    }
    public float X2
    {
        get { return Point2.X; }
        set
        {
            _point2.X = value;
        }
    }
    public float Y2
    {
        get { return _point2.Y; }
        set
        {
            _point2.Y = value;
        }
    }
    public float Distance
    {
        get
        { // √[( y2 –  y1)² + ( x1 –  x2)²]
            return MathF.Sqrt(MathF.Pow(MathF.Abs(Y2 - Y1), 2) + MathF.Pow(MathF.Abs(X2 - X1), 2));
        }
    }

    public override void Draw(Graphics g)
    {
        if (IsSelected)
            Pen = new Pen(SELECTED_COLOR, StrokeWidth);
        else
            Pen = new Pen(StrokeColor, StrokeWidth);
        if (Pen == null) Pen = new Pen(StrokeColor, StrokeWidth);
        g.DrawLine(Pen, Point1.X, Point1.Y, Point2.X, Point2.Y);
        _path.AddLine(Point1.X, Point1.Y, Point2.X, Point2.Y);
    }

    public override bool isInside(MyPoint point)
    {
        return _path.IsOutlineVisible(point.X, point.Y, Pen); 
        //return true;
    }
    public override bool isInside(float x, float y)
    {
        return _path.IsOutlineVisible(x, y, Pen);
    }

    public override void Change(float x, float y)
    {
        X2 = x;
        Y2 = y;
    }
    public override void Move(float x, float y)
    {
        //TODO   
    }

}
