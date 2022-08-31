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
                updateParams(Width / 2, Height / 2, value, Height);
            else
                updateParams(Width / 2, Height / 2, 0, Height);

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
                updateParams(Width / 2, Height / 2, Width, value);
            else
               updateParams(Width / 2, Height / 2, Width, 0);
        }
    }

    private void updateParams(float x, float y, float width, float height)
    {
        MyPoint.X = x; 
        MyPoint.Y = y;
        Width = width;
        Height = height;
        Vertices = new MyPoint[] { new MyPoint(MyPoint.X - Width / 2, MyPoint.Y - Height / 2),
            new MyPoint(MyPoint.X + Width / 2, MyPoint.Y - Height / 2), new MyPoint(MyPoint.X + Width / 2, MyPoint.Y + Height / 2),
            new MyPoint(MyPoint.X - Width / 2, MyPoint.Y + Height / 2)};
    }
    public MyPoint Center { get { return MyPoint; } set { updateParams(value.X, value.Y, Width, Height); } }

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
