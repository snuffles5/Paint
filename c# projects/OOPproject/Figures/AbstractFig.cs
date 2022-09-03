using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;

[Serializable]
public class AbstractFig : Figure 
{
    List<MyPoint> _vertices = new List<MyPoint>();
    MyPoint _maxPoint;
    MyPoint _minPoint;
    [field: NonSerialized] public GraphicsPath _path = new GraphicsPath();
    public AbstractFig() : this((List <MyPoint>) null) 
    {
        Pen = new Pen(StrokeColor, StrokeWidth);
    }
    public AbstractFig(List<MyPoint> vertices, int strokeWidth = 1)
    {
        if (vertices != null)
        {
            MyPoint = vertices[0];
            
            //for (int i = 1; i < vertices.Count; i++)
            //{
            //    Vertices[i] = vertices[i];
            //    if (vertices[i].X > MaxPoint.X)
            //        MaxPoint.X = vertices[i].X;
            //    else if (vertices[i].X < MinPoint.X)
            //        MinPoint.X = vertices[i].X;
            //    if (vertices[i].Y > MaxPoint.Y)
            //        MaxPoint.Y = vertices[i].Y;
            //    else if (vertices[i].Y < MinPoint.Y)
            //        MinPoint.Y = vertices[i].Y;
            //}
            Vertices = vertices;
        }
        else
        {
            MyPoint = new MyPoint();
        }
        _maxPoint = new MyPoint(MyPoint.X, MyPoint.Y);
        _minPoint = new MyPoint(MyPoint.X, MyPoint.Y);
        StrokeColor = Color.Black;
        FillColor = Color.Black;
        StrokeWidth = strokeWidth;
        Pen = new Pen(StrokeColor, StrokeWidth);
    } 
    
    public AbstractFig(List<MyPoint> vertices, Color strokeColor, Color fillColor, int strokeWidth = 1)
    {
        if (vertices != null)
        {
            MyPoint = vertices[0];
            for (int i = 1; i < vertices.Count; i++)
            {
                Vertices[i] = vertices[i];

            }
        }
        else
        {
            MyPoint = new MyPoint();
        }
        StrokeColor = strokeColor;
        FillColor = fillColor;
        StrokeWidth = strokeWidth;
        Pen = new Pen(StrokeColor, StrokeWidth);
    }
    public AbstractFig(float x, float y, int strokeWidth = 1)
    {
        MyPoint = new MyPoint(x, y);
        _maxPoint = new MyPoint(MyPoint.X, MyPoint.Y);
        _minPoint = new MyPoint(MyPoint.X, MyPoint.Y);
        StrokeColor = Color.Black;
        FillColor = Color.Black;
        StrokeWidth = strokeWidth;
        Pen = new Pen(StrokeColor, StrokeWidth);
    }

    public MyPoint MaxPoint { get { return _maxPoint; } set { _maxPoint = new MyPoint(value.X, value.Y); } }
    public MyPoint MinPoint { get { return _minPoint; } set { _minPoint = new MyPoint(value.X, value.Y); } }

    public List<MyPoint> Vertices
    {
        get
        {
            return _vertices;
        }
        set
        {
            for (int i = 0; i < value.Count; i++)
            {
                _vertices[i] = value[i];
                Vertices[i] = Vertices[i];
                if (Vertices[i].X > MaxPoint.X)
                    MaxPoint.X = Vertices[i].X;
                else if (Vertices[i].X < MinPoint.X)
                    MinPoint.X = Vertices[i].X;
                if (Vertices[i].Y > MaxPoint.Y)
                    MaxPoint.Y = Vertices[i].Y;
                else if (Vertices[i].Y < MinPoint.Y)
                    MinPoint.Y = Vertices[i].Y;
            }
        }
    }

    public void Add(MyPoint point)
    {
        if (point != null)
        {
            MyPoint prev = Vertices != null ? Vertices[Vertices.Count - 1] : MyPoint;
            _path.AddLine(new PointF(prev.X, prev.Y), new PointF(point.X, point.Y));
            Vertices.Add(point);
            if (point.X > MaxPoint.X)
                MaxPoint.X = point.X;
            else if (point.X < MinPoint.X)
                MinPoint.X = point.X;
            if (point.Y > MaxPoint.Y)
                MaxPoint.Y = point.Y;
            else if (point.Y < MinPoint.Y)
                MinPoint.Y = point.Y;
        }
    }
     public void Add(float x, float y)
    {
        MyPoint prev = Vertices != null && Vertices.Count > 0 ? Vertices[Vertices.Count - 1] : MyPoint;
        _path.AddLine(new PointF(prev.X, prev.Y), new PointF(x, y));
        Vertices.Add(new MyPoint(x, y));
        if (x > MaxPoint.X)
            MaxPoint.X = x;
        else if (x < MinPoint.X)
            MinPoint.X = x;
        if (y > MaxPoint.Y)
            MaxPoint.Y = y;
        else if (y < MinPoint.Y)
            MinPoint.Y = y;
    }


    public override void Draw(Graphics g)
    {
        if (IsSelected)
        {
            float width = MaxPoint.X - MinPoint.X;
            float height = MaxPoint.Y - MinPoint.Y;
            Pen surrundingRec = new Pen(SELECTED_COLOR, StrokeWidth / 2);
            surrundingRec.DashStyle = DashStyle.Dash;
            //g.DrawRectangle(surrundingRec, MinPoint.X, MinPoint.Y, width, height);  // surrounding rectangle
            g.DrawRectangle(surrundingRec, _path.GetBounds().X, _path.GetBounds().Y, _path.GetBounds().Width, _path.GetBounds().Height);  // surrounding rectangle
            
        }
        Pen = new Pen(StrokeColor, StrokeWidth);
        if (_path == null) _path = new GraphicsPath(); // after desrialize
        g.DrawPath(Pen,_path);
    }

    public override bool isInside(MyPoint point)
    {
        return _path.IsOutlineVisible(point.X, point.Y, Pen); // TODO: Sorry, wasn't implemented yet
        //return true;
    }

    public override bool isInside(float x, float y)
    {
        if(Pen != null)
            return _path.IsOutlineVisible(x, y, Pen);
        return false;
    }

    public override void Change(float x, float y)
    {
        Add(x, y);
    }

    public override void Move(float xOffset, float yOffset)
    {
        //TODO   
        MyPoint.X += xOffset;
        MyPoint.Y += yOffset;
        //PointF[] newPathPoint = _path.PathPoints;
        //if (_path != null)
        //{
        //    newPathPoint[0].X += xOffset;
        //    newPathPoint[0].Y += yOffset;
        //}
        for (int i = 0; i < Vertices.Count; i++)
        {
            Vertices[i].X += xOffset;
            Vertices[i].Y += yOffset;
            //if (i < _path.PathPoints.Length)
            //{
            //    newPathPoint[i].X += xOffset;
            //    newPathPoint[i].Y += yOffset;
            //}
        }
        //if (newPathPoint.Length == _path.PathPoints.Length)
        //{
            Matrix m = new Matrix();
            m.Translate(xOffset, yOffset);
            _path.Transform(m);

        //}
    }



    //~AbstractFig() { System.Diagnostics.Debug.WriteLine("Destructor AbstractFig"); }

}
