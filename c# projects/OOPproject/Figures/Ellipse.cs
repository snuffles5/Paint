using System;
using System.Drawing;

public class Ellipse : Figure
{
    const float DEFAULT_RADIUS = 1f;
    MyPoint _secondPoint;
    public Ellipse() : this(10, 10, 10, 10) { }
    public Ellipse(MyPoint firstPoint, MyPoint secondPoint, int strokeWidth = 1)
    {
        FirstPoint = new MyPoint(firstPoint);
        SecondPoint = new MyPoint(secondPoint);
        StrokeColor = Color.Black;
        FillColor = Color.Black;
        StrokeWidth = strokeWidth;
    }
    public Ellipse(Color strokeColor, Color fillColor, MyPoint firstPoint, MyPoint secondPoint, int strokeWidth = 1)
    {
        FirstPoint = new MyPoint(firstPoint);
        SecondPoint = new MyPoint(secondPoint);
        StrokeColor = strokeColor;
        FillColor = fillColor;
        StrokeWidth = strokeWidth;
    }

    public Ellipse(float x1, float y1, float x2, float y2, int strokeWidth = 1)
    {
        MyPoint.X = x1;
        MyPoint.Y = y1;
        SecondPoint = new MyPoint(x2, y2);
        StrokeColor = Color.Black;
        FillColor = Color.Black;
        StrokeWidth = strokeWidth;
    }
    public MyPoint FirstPoint { get { return MyPoint; } set { MyPoint.X = value.X; MyPoint.Y = value.Y; } }
    public MyPoint SecondPoint { get { return _secondPoint; } set { _secondPoint = new MyPoint(value.X, value.Y); } }
    public float X2 { get { return _secondPoint.X; } set { _secondPoint.X = value; } }
    public float Y2 { get { return _secondPoint.Y; } set { _secondPoint.Y = value; } }

    //TODO add X1 X2 Y1 Y2 getters
    public override void Draw(Graphics graphic)
    {
        SolidBrush br = new SolidBrush(FillColor);
        Pen pen = new Pen(StrokeColor, StrokeWidth);
        graphic.FillEllipse(br, FirstPoint.X, FirstPoint.Y, SecondPoint.X-FirstPoint.X, SecondPoint.Y-FirstPoint.Y);
        graphic.DrawEllipse(pen, FirstPoint.X, FirstPoint.Y, SecondPoint.X - FirstPoint.X, SecondPoint.Y - FirstPoint.Y);
    }
    public override bool isInside(MyPoint firstPoint)
    {
        //return Math.Sqrt(Math.Pow(firstPoint.X - X, 2) + Math.Pow(firstPoint.Y - Y, 2)) < Radius;
        // Todo 
        return true;
    }

    ~Ellipse() { Console.WriteLine("Destructor Ellipse"); }
}
