using System;
using System.Drawing;

public class Circle : Figure
{
    const float DEFAULT_RADIUS = 1f;
    float radius;
    public Circle() : this(10, 10, DEFAULT_RADIUS) { }
    public Circle(myPoint myPoint, float radius = DEFAULT_RADIUS)
    {
        myPoint = new myPoint(myPoint);
        Radius = radius;
        StrokeColor = Color.Black;
        FillColor = Color.Black;
    }

    public Circle(float x, float y, float radius = DEFAULT_RADIUS)
    {
        myPoint = new myPoint(x, y);
        Radius = radius;
        StrokeColor = Color.Black;
        FillColor = Color.Black;
    }
    public float Radius
    {
        get
        {
            return radius;
        }
        set
        {
            if (radius >= 0)
                radius = value;
            else
                radius = 0;
        }
    }
    public override void Draw(Graphics graphic)
    {
        SolidBrush br = new SolidBrush(FillColor);
        Pen pen = new Pen(StrokeColor, StrokeWidth);
        graphic.FillEllipse(br, X - Radius, Y - Radius, 2 * Radius, 2 * Radius);
        graphic.DrawEllipse(pen, X - Radius, Y - Radius, 2 * Radius, 2 * Radius);
    }
    public override bool isInside(myPoint myPoint)
    {
        return Math.Sqrt(Math.Pow(myPoint.X - X, 2) + Math.Pow(myPoint.Y - Y, 2)) < Radius;
    }

    ~Circle() { Console.WriteLine("Destructor Circle"); }
}
