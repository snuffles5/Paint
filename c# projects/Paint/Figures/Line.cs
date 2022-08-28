using System;
using static System.Windows.Forms.LinkLabel;


public class Line
{
	Point _point1;
	Point _point2;
	public Line(float x1 = 0, float y1 = 0, float x2=0, float y2=0)
	{
		Point1 = new Point(x1, y1);	
		Point2 = new Point(x2, y2);
	}
	public Line( Line Line)
	{
        Point1 = new Point(Line.X1, Line.Y1);
        Point2 = new Point(Line.X2, Line.Y2);
    }
    // Properties
    public Point Point1
	{
		get { return _point1; }
		set
		{
			_point1 = value;
		}
	}
	public Point Point2
	{
		get { return _point2; }
		set
		{
			_point2 = value;
		}
	}
	public float X1 
	{
		get { return _point1.X; } 
		set 
		{
			_point1.X = value;
		} 
	}
    public float Y1
    {
        get { return _point1.Y; }
        set
        {
            _point1.Y = value;
        }
    }
	public float X2 
	{
		get { return _point1.X; } 
		set 
		{
			_point1.X = value;
		} 
	}
    public float Y2
    {
        get { return _point1.Y; }
        set
        {
            _point1.Y = value;
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
