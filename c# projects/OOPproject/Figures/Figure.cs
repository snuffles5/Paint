using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;

[Serializable]
public abstract class Figure
{
    MyPoint _myPoint = new MyPoint();
    Color _strokeColor;
    Color _fillColor;
    int _strokeWidth;
    bool _isSelected = false;
    Pen _pen;
    
    public
        //TODO make private and getters
    static Color SELECTED_COLOR = Color.Red;
    // TODO maybe add _static path to make the eraser less heavier. need to save index too.

    public MyPoint MyPoint 
    { 
        get 
        { 
            return _myPoint;
        } 
        set 
        { 
            _myPoint.X = value.X; _myPoint.Y = value.Y; 
        } 
    }
    public float X { get { return _myPoint.X; } set { _myPoint.X = value; } }
    public float Y { get { return _myPoint.Y; } set { _myPoint.Y = value; } }
    public bool IsSelected { get { return _isSelected; } set { _isSelected = value; } }
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
    } public Pen Pen
    {
        get { return _pen; }
        set
        {
            if (value !=  _pen)
                _pen = value;
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
    public abstract void Change(float x, float y);
    public abstract void Move(float x, float y);
    public abstract void Draw(Graphics g);
    public abstract bool isInside(MyPoint MyPoint);
    public abstract bool isInside(float x, float y);

}
