using System;
using System.Drawing;

public class Rectangle: Quadrilateral // not supporting rotation
{
    float _width;
    float _height;
    public Rectangle() : this(1, 1) { }
    public Rectangle(Point point, float width, float height)
    {
        Point = new Point(point);
        Vertices = new Point[] { };
        Width = width;
        Height = height;
        StrokeColor = Color.Black;
        FillColor = Color.Black;
    }
    public Rectangle(float width, float height)
    {
        Point = new Point();
        Width = width;
        Height = height;
        StrokeColor = Color.Black;
        FillColor = Color.Black;
    }
    public Rectangle(float x, float y, float width, float height)
    {
        Point = new Point(x,y);
        Width = width;
        Height = height;
        StrokeColor = Color.Black;
        FillColor = Color.Black;
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
    public Point FirstPoint
    {
        get
        {
            return new Point(this.Point.X-(Width / 2), this.Point.Y-(Height / 2));
        }
    }public Point SecondPoint
    {
        get
        {
            return new Point(this.Point.X+(Width / 2), this.Point.Y+(Height / 2));
        }
    }
    public override void Draw(Graphics graphic) 
    {
        SolidBrush br = new SolidBrush(FillColor);
        Pen pen = new Pen(StrokeColor, StrokeWidth);
        //g.FillRectangle(br, X - width / 2, Y - height / 2, width, height);
        graphic.FillRectangle(br, FirstPoint.X, FirstPoint.Y, Width, Height);
        graphic.DrawRectangle(pen, FirstPoint.X, FirstPoint.Y, Width, Height);
    }
    public override bool isInside(Point point) 
    {
        return Math.Abs(point.X - X) <= Width / 2 && Math.Abs(point.Y - Y) <= Height / 2;
    }

    ~Rectangle() { System.Diagnostics.Debug.WriteLine("Destructor Rectangle"); }

}
