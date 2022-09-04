using System;
using System.Drawing;
using System.Drawing.Drawing2D;

[Serializable]
public class Ellipse : Figure
{
    MyPoint _secondPoint;
    //[field: NonSerialized] public GraphicsPath _path = new GraphicsPath();

    public Ellipse() : this(10, 10, 10, 10) { }
    public Ellipse(MyPoint firstPoint, MyPoint secondPoint, int strokeWidth = 1)
    {
        FirstPoint = new MyPoint(firstPoint);
        SecondPoint = new MyPoint(secondPoint);
        StrokeColor = Color.Black;
        FillColor = Color.Black;
        StrokeWidth = strokeWidth;
        Pen = new Pen(StrokeColor, StrokeWidth);
    }
    public Ellipse(Color strokeColor, Color fillColor, MyPoint firstPoint, MyPoint secondPoint, int strokeWidth = 1)
    {
        FirstPoint = new MyPoint(firstPoint);
        SecondPoint = new MyPoint(secondPoint);
        StrokeColor = strokeColor;
        FillColor = fillColor;
        StrokeWidth = strokeWidth;
        Pen = new Pen(StrokeColor, StrokeWidth);
    }

    public Ellipse(float x1, float y1, float x2, float y2, int strokeWidth = 1)
    {
        MyPoint.X = x1;
        MyPoint.Y = y1;
        SecondPoint = new MyPoint(x2, y2);
        StrokeColor = Color.Black;
        FillColor = Color.Black;
        StrokeWidth = strokeWidth;
        Pen = new Pen(StrokeColor, StrokeWidth);
    }
    public MyPoint FirstPoint { get { return MyPoint; } set { MyPoint.X = value.X; MyPoint.Y = value.Y; } }
    public MyPoint SecondPoint { get { return _secondPoint; } set { _secondPoint = new MyPoint(value.X, value.Y); } }
    public float X2 { get { return _secondPoint.X; } set { _secondPoint.X = value; } }
    public float Y2 { get { return _secondPoint.Y; } set { _secondPoint.Y = value; } }

    public override void Draw(Graphics graphic)
    {
        SolidBrush br = new SolidBrush(FillColor);
        if (IsSelected)
            Pen = new Pen(SELECTED_COLOR, StrokeWidth);
        //else
        //    Pen = new Pen(StrokeColor, StrokeWidth);
        if (Pen == null) 
            Pen = new Pen(StrokeColor, StrokeWidth);  // for desrialize
        graphic.FillEllipse(br, FirstPoint.X, FirstPoint.Y, SecondPoint.X-FirstPoint.X, SecondPoint.Y-FirstPoint.Y);
        graphic.DrawEllipse(Pen, FirstPoint.X, FirstPoint.Y, SecondPoint.X - FirstPoint.X, SecondPoint.Y - FirstPoint.Y);
        if (_path == null)
            InitializePath(); // for desrialize
        _path.AddEllipse(FirstPoint.X, FirstPoint.Y, SecondPoint.X - FirstPoint.X, SecondPoint.Y - FirstPoint.Y);
    }
    public override bool isInside(float x, float y)
    {
        return true && isOnPath(x, y); //TODO
    }
    public override bool isOnPath(float x, float y)
    {
        return _path.IsOutlineVisible(x, y, Pen);
    }

    public override bool isInsideSurrounding(float x, float y)
    {
        return false; //TODO
    }

    public override void Change(float x, float y)
    {
        SecondPoint.X = x;
        SecondPoint.Y = y;
    }

    public override void Move(float x, float y)
    {
        //TODO   
    }

    ~Ellipse() { Console.WriteLine("Destructor Ellipse"); }
}
