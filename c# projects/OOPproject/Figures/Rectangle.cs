using System;
using System.Drawing;

public class Rectangle : Quadrilateral // not supporting rotation
{
    float _width;
    float _height;
    public Rectangle() : this(1, 1) { }
    public Rectangle(MyPoint point, float width, float height, int strokeWidth = 0)
    {
        MyPoint = new MyPoint(point);
        Vertices = new MyPoint[] { new MyPoint(point.X - width / 2, point.Y - height / 2), 
            new MyPoint(point.X + width / 2, point.Y - height / 2), new MyPoint(point.X + width / 2, point.Y + height / 2), 
            new MyPoint(point.X - width / 2, point.Y + height / 2) };
        Width = width;
        Height = height;
        StrokeColor = Color.Black;
        FillColor = Color.Black;
        StrokeWidth = strokeWidth;
    }
    public Rectangle(float width, float height, int strokeWidth = 0)
    {
        MyPoint = new MyPoint(width / 2,height / 2);
        Vertices = new MyPoint[] { new MyPoint(MyPoint.X - width / 2, MyPoint.Y - height / 2),
            new MyPoint(MyPoint.X + width / 2, MyPoint.Y - height / 2), new MyPoint(MyPoint.X + width / 2, MyPoint.Y + height / 2),
            new MyPoint(MyPoint.X - width / 2, MyPoint.Y + height / 2) };
        Height = height;
        StrokeColor = Color.Black;
        FillColor = Color.Black;
        StrokeWidth = strokeWidth;
    }
    public Rectangle(float x, float y, float width, float height, int strokeWidth = 0)
    {
        MyPoint = new MyPoint(x, y);
        Width = width;
        Height = height;
        StrokeColor = Color.Black;
        FillColor = Color.Black;
        StrokeWidth = strokeWidth;
    }
    public float Width
    {
        get
        {
            return _width;
        }
        set
        {
            if (_width >= 0)
                _width = value;
            else
                _width = 0;
        }
    }
    public float Height
    {
        get
        {
            return _height;
        }
        set
        {
            if (_height >= 0)
                _height = value;
            else
                _height = 0;
        }
    }
    public MyPoint Center { get { return MyPoint; } set { MyPoint = new MyPoint(value.X, value.Y); } }

    public override void Draw(Graphics graphic)
    {
        SolidBrush br = new SolidBrush(FillColor);
        Pen pen = new Pen(StrokeColor, StrokeWidth);
        graphic.FillRectangle(br, Vertices[0].X, Vertices[0].Y, Width, Height);
        graphic.DrawRectangle(pen, Vertices[0].X, Vertices[0].Y, Width, Height);
    }
    public override bool isInside(MyPoint MyPoint)
    {
        return Math.Abs(MyPoint.X - X) <= Width / 2 && Math.Abs(MyPoint.Y - Y) <= Height / 2;
    }

    ~Rectangle() { System.Diagnostics.Debug.WriteLine("Destructor Rectangle"); }

}
