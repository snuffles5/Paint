using System;
using System.Drawing;

public class Rectangle : Quadrilateral // not supporting rotation
{
    float _width;
    float _height;
    public Rectangle() : this(1, 1) { }
    public Rectangle(myPoint myPoint, float width, float height)
    {
        myPoint = new myPoint(myPoint);
        Vertices = new myPoint[] { };
        Width = width;
        Height = height;
        StrokeColor = Color.Black;
        FillColor = Color.Black;
    }
    public Rectangle(float width, float height)
    {
        myPoint = new myPoint();
        Width = width;
        Height = height;
        StrokeColor = Color.Black;
        FillColor = Color.Black;
    }
    public Rectangle(float x, float y, float width, float height)
    {
        myPoint = new myPoint(x, y);
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
    public myPoint FirstmyPoint
    {
        get
        {
            return new myPoint(this.myPoint.X - (Width / 2), this.myPoint.Y - (Height / 2));
        }
    }
    public myPoint SecondmyPoint
    {
        get
        {
            return new myPoint(this.myPoint.X + (Width / 2), this.myPoint.Y + (Height / 2));
        }
    }
    public override void Draw(Graphics graphic)
    {
        SolidBrush br = new SolidBrush(FillColor);
        Pen pen = new Pen(StrokeColor, StrokeWidth);
        //g.FillRectangle(br, X - width / 2, Y - height / 2, width, height);
        graphic.FillRectangle(br, FirstmyPoint.X, FirstmyPoint.Y, Width, Height);
        graphic.DrawRectangle(pen, FirstmyPoint.X, FirstmyPoint.Y, Width, Height);
    }
    public override bool isInside(myPoint myPoint)
    {
        return Math.Abs(myPoint.X - X) <= Width / 2 && Math.Abs(myPoint.Y - Y) <= Height / 2;
    }

    ~Rectangle() { System.Diagnostics.Debug.WriteLine("Destructor Rectangle"); }

}
