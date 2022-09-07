using OOPproject;
using System;
using System.Drawing;

[Serializable]
public class MyPoint
{
    const float MAX_X =  Form1.DEFAULT_FORM_WIDTH;
    const float MAX_Y = Form1.DEFAULT_FORM_HEIGHT;
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
            if (value >= 0 && value < MAX_X)
                _x = value;
            else if (value >= 0)
                _x = MAX_X;
            else
                _x = 0;
        }
    }
    public float Y
    {
        get { return _y; }
        set
        {
            if (value >= 0 && value < MAX_Y)
                _y = value;
            else if (value >= 0)
                _y = MAX_Y;
            else
                _y = 0;
        }
    }
}
