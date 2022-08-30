using System;
using System.Drawing;

public class MyPoint
{
    float _x;
    float _y;
    public MyPoint(float x = 0, float y = 0)
    {
        X = x;
        Y = y;
    }
    public MyPoint(MyPoint MyPoint)
    {
        X = MyPoint.X;
        Y = MyPoint.Y;
    }
    public MyPoint(Point point)
    {
        X = point.X;
        Y = point.Y;
    }
    // Properties
    public float X
    {
        get { return _x; }
        set
        {
            if (value >= 0)
                _x = value;
            else
                _x = 0;
        }
    }
    public float Y
    {
        get { return _y; }
        set
        {
            if (value > 0)
                _y = value;
            else
                _y = 0;
        }
    }
}
