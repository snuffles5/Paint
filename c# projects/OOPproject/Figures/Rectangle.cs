using System;
using System.Drawing;

public class Rectangle : Quadrilateral // not supporting rotation
{
    float _width;
    float _height;
    public Rectangle(MyPoint point, float width, float height, int strokeWidth = 0): base(point, new MyPoint(point.X + width, point.Y), new MyPoint(point.X + width, point.Y + height), new MyPoint(point.X, point.Y + height))
    {
        Width = width;
        Height = height;
        StrokeColor = Color.Black;
        FillColor = Color.Black;
        StrokeWidth = strokeWidth;
    }
    public Rectangle(float x=0, float y=0, float width = 0, float height = 0, int strokeWidth = 0) : base(new MyPoint(x,y), new MyPoint(x + width, y), new MyPoint(x + width, y + height), new MyPoint(x, y + height))
    {
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
            if (value >= 0)
                updateParams(MyPoint.X, MyPoint.Y, value, Height);
            else
                updateParams(MyPoint.X, MyPoint.Y, 0, Height);

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
            if (value >= 0)
                updateParams(MyPoint.X, MyPoint.Y, Width, value);
            else
               updateParams(MyPoint.X, MyPoint.Y, Width, 0);
        }
    }

    private void updateParams(float x, float y, float width, float height) // this method receive width and height after CHECK!
    {
        MyPoint.X = x; 
        MyPoint.Y = y;
        _width = width;
        _height = height;
        Vertices[0] = new MyPoint(x + width, y);
        Vertices[1] = new MyPoint(x + width, y + height);
        Vertices[2] = new MyPoint(x, y + height);
        //Vertices = new MyPoint[] { new MyPoint(x + width, y), new MyPoint(x + width, y + height), 
        //new MyPoint(x, y + height) };
    }
    public MyPoint Center { get { return MyPoint; } set { updateParams(value.X, value.Y, Width, Height); } }

    public override void Draw(Graphics graphic)
    {
        SolidBrush br = new SolidBrush(FillColor);
        Pen pen = new Pen(StrokeColor, StrokeWidth);
        graphic.FillRectangle(br, MyPoint.X, MyPoint.Y, Width, Height);
        graphic.DrawRectangle(pen, MyPoint.X, MyPoint.Y, Width, Height);
    }
    public override bool isInside(MyPoint MyPoint)
    {
        return Math.Abs(MyPoint.X - X) <= Width / 2 && Math.Abs(MyPoint.Y - Y) <= Height / 2;
    }

    ~Rectangle() { System.Diagnostics.Debug.WriteLine("Destructor Rectangle"); }

}
