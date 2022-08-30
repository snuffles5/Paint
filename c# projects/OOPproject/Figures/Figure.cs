using System;
using System.Drawing.Imaging;
using System.Globalization;

public abstract class Figure
{
    public enum colors
    {
        Black, Red, Blue, Green, Yellow, Purple, Orange
    }
    Point _point;
    Color _strokeColor;
    Color _fillColor;
    int _strokeWidth;

    public Point Point { get { return _point; } set { _point = new Point(value.X, value.Y); } }
    public float X { get { return _point.X; } set { _point.X = value; } }
    public float Y { get { return _point.Y; } set { _point.Y = value; } }
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
    public abstract bool isInside(Point point);

}
