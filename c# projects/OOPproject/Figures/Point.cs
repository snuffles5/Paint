using System;


public class myPoint
{
    float _x;
    float _y;
    public myPoint(float x = 0, float y = 0)
    {
        X = x;
        Y = y;
    }
    public myPoint(myPoint myPoint)
    {
        X = myPoint.X;
        Y = myPoint.Y;
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
