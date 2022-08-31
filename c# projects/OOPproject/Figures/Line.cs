using System;
using System.Drawing;

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
            MyPoint = value;
        }
    }
    public MyPoint Point2
    {
        get { return _point2; }
        set
        {
            _point2 = value;
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
        get { return MyPoint.X; }
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
    public double Distance
    {
        get
        { // √[( y2 –  y1)² + ( x1 –  x2)²]
            return Math.Sqrt(Math.Pow(Y2 - Y1, 2) + Math.Pow(X1 - X2, 2));
        }
    }

    public override void Draw(Graphics g)
    {
        Pen pen = new Pen(StrokeColor, StrokeWidth);
        g.DrawLine(pen, Point1.X, Point1.Y, Point2.X, Point2.Y);
    }

    public override bool isInside(MyPoint MyPoint)
    {
        throw new NotImplementedException(); // TODO: Sorry, wasn't implemented yet
        //return true;
    }
}
