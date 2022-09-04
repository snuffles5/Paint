using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;

[Serializable]
public class Rhombus : Quadrilateral 
{
    float _width;
    float _height;
    public Rhombus(MyPoint point, float width, float height, int strokeWidth = 0): base(point.Y < height/2 ? new MyPoint(point.X, height / 2): point, // top point of rhombus is minus and exceeding canvas TODO Verify
        new MyPoint(point.X + width / 2, point.Y - height / 2), new MyPoint(point.X + width, point.Y), new MyPoint(point.X + width / 2, point.Y + height / 2))
    {
        Width = width;
        Height = height;
        StrokeColor = Color.Black;
        FillColor = Color.Black;
        StrokeWidth = strokeWidth;
    }
    public Rhombus(float x = 0, float y = 0, float width = 0, float height = 0, int strokeWidth = 0) : base(  new MyPoint(x, y = y < height / 2 ? height / 2 : y) , // top point of rhombus is minus and exceeding canvas TODO Verify
        new MyPoint(x + width / 2, y - height / 2), new MyPoint(x + width, y), new MyPoint(x  + width / 2, y + height / 2))
    {
        Width = width;
        Height = height;
        StrokeColor = Color.Black;
        FillColor = Color.Black;
        StrokeWidth = strokeWidth;
    }
    public float Width
    {
        get
        {
            return _width;
        }
        set
        {
            if (value >= 0)
                updateParams(MyPoint.X, MyPoint.Y, value, Height);
            else
                updateParams(MyPoint.X, MyPoint.Y, 0, Height);

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
            if (value >= 0)
                updateParams(MyPoint.X, MyPoint.Y, Width, value);
            else
               updateParams(MyPoint.X, MyPoint.Y, Width, 0);
        }
    }

    private void updateParams(float x, float y, float width, float height) // this method receive width and height after CHECK!
    {
        MyPoint.X = x;
        MyPoint.Y = (y = y < height / 2 ? height / 2 : y);
        _width = width;
        _height = height;
        Vertices[0] = new MyPoint(x + width / 2, y - height / 2);
        Vertices[1] = new MyPoint(x + width, y);
        Vertices[2] = new MyPoint(x + width / 2, y + height / 2);
    }
    public MyPoint Center { get { return MyPoint; } set { updateParams(value.X, value.Y, Width, Height); } }

    public override void Draw(Graphics graphic)
    {
        if (IsSelected)
            Pen = new Pen(SELECTED_COLOR, StrokeWidth);
        //else
        //    Pen = new Pen(StrokeColor, StrokeWidth);
        if (Pen == null) Pen = new Pen(StrokeColor, StrokeWidth);
        SolidBrush br = new SolidBrush(FillColor);
        graphic.FillPolygon(br, new PointF[] {new PointF(MyPoint.X, MyPoint.Y), new PointF(Vertices[0].X, Vertices[0].Y), new PointF(Vertices[1].X, Vertices[1].Y), new PointF(Vertices[2].X, Vertices[2].Y) });
        graphic.DrawPolygon(Pen, new PointF[] {new PointF(MyPoint.X, MyPoint.Y), new PointF(Vertices[0].X, Vertices[0].Y), new PointF(Vertices[1].X, Vertices[1].Y), new PointF(Vertices[2].X, Vertices[2].Y) });
        if (_path == null)
            InitializePath();// after desrialize
        _path.AddLine(MyPoint.X, MyPoint.Y, Vertices[0].X, Vertices[0].Y);
        _path.AddLine(Vertices[0].X, Vertices[0].Y, Vertices[1].X, Vertices[1].Y);
        _path.AddLine(Vertices[1].X, Vertices[1].Y, Vertices[2].X, Vertices[2].Y);
        _path.AddLine(Vertices[2].X, Vertices[2].Y, MyPoint.X, MyPoint.Y);
    }
    public override bool isInside(float x, float y)
    {
        return (Math.Abs(x - X) <= Width / 2 && Math.Abs(y - Y) <= Height / 2) && isOnPath(x, y); //TODO
    }
    public override bool isOnPath(float x, float y)
    {
        if (Vertices.Length != 0)
        {
            if (Pen == null)
                Pen = new Pen(StrokeColor, StrokeWidth); // for desrialize
            if (_path == null)
                InitializePath(); // for desrialize
            return _path.IsOutlineVisible(x, y, Pen);
        }
        return false;
    }

    public override bool isInsideSurrounding(float x, float y)
    {
        return false; //TODO
    }

    public override void Change(float x, float y)
    {
        Width = x - X;
        Height = y - Y;
    }
    public override void Move(float x, float y)
    {
        //TODO   
    }

}
