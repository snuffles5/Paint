using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;

public abstract class Figure
{
    MyPoint _myPoint;
    Color _strokeColor;
    Color _fillColor;
    int _strokeWidth;

    public MyPoint MyPoint { get { return _myPoint; } set { _myPoint = new MyPoint(value.X, value.Y); } }
    public float X { get { return _myPoint.X; } set { _myPoint.X = value; } }
    public float Y { get { return _myPoint.Y; } set { _myPoint.Y = value; } }
    public Color StrokeColor
    {
        get { return _strokeColor; }
        set
        {
            if (value != _strokeColor)
                _strokeColor = value;
        }
    }
    public Color FillColor
    {
        get { return _fillColor; }
        set
        {
            if (value != _fillColor)
                _fillColor = value;
        }
    }
    public int StrokeWidth
    {
        get { return _strokeWidth; }
        set
        {
            if (value != _strokeWidth)
            {
                if (value >= 0)
                    _strokeWidth = value;
                else
                    _strokeWidth = 1;
            }
        }
    }
    public abstract void Draw(Graphics g);
    public abstract bool isInside(MyPoint MyPoint);

}
