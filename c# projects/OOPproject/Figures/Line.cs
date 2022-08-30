using System;
using static System.Windows.Forms.LinkLabel;


public class Line
{
    myPoint _myPoint1;
    myPoint _myPoint2;
    public Line(float x1 = 0, float y1 = 0, float x2 = 0, float y2 = 0)
    {
        myPoint1 = new myPoint(x1, y1);
        myPoint2 = new myPoint(x2, y2);
    }
    public Line(Line Line)
    {
        myPoint1 = new myPoint(Line.X1, Line.Y1);
        myPoint2 = new myPoint(Line.X2, Line.Y2);
    }
    // Properties
    public myPoint myPoint1
    {
        get { return _myPoint1; }
        set
        {
            _myPoint1 = value;
        }
    }
    public myPoint myPoint2
    {
        get { return _myPoint2; }
        set
        {
            _myPoint2 = value;
        }
    }
    public float X1
    {
        get { return _myPoint1.X; }
        set
        {
            _myPoint1.X = value;
        }
    }
    public float Y1
    {
        get { return _myPoint1.Y; }
        set
        {
            _myPoint1.Y = value;
        }
    }
    public float X2
    {
        get { return _myPoint1.X; }
        set
        {
            _myPoint1.X = value;
        }
    }
    public float Y2
    {
        get { return _myPoint1.Y; }
        set
        {
            _myPoint1.Y = value;
        }
    }
    public double Distance
    {
        get
        { // √[( y2 –  y1)² + ( x1 –  x2)²]
            return Math.Sqrt(Math.Pow(Y2 - Y1, 2) + Math.Pow(X1 - X2, 2));
        }
    }
}
