using OOPproject;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;

[Serializable]
public class AbstractFig : Figure 
{
    [DataMember] List<MyPoint> _vertices = new List<MyPoint>();
    //[field: NonSerialized] public GraphicsPath
    //_path = new GraphicsPath();
    public AbstractFig() : this((List <MyPoint>) null) 
    {
    }
    public AbstractFig(List<MyPoint> vertices, int strokeWidth = 1)
    {
        if (vertices != null)
        {
            MyPoint = vertices[0];
            Vertices = vertices;
        }
        else
        {
            MyPoint = new MyPoint();
        }
        StrokeColor = Color.Black;
        FillColor = Color.Black;
        StrokeWidth = strokeWidth;
    } 
    
    public AbstractFig(List<MyPoint> vertices, Color strokeColor, Color fillColor, int strokeWidth = 1)
    {
        if (vertices != null)
        {
            MyPoint = vertices[0];
            for (int i = 1; i < vertices.Count; i++)
                Vertices[i] = vertices[i];
        }
        else
            MyPoint = new MyPoint();
        StrokeColor = strokeColor;
        FillColor = fillColor;
        StrokeWidth = strokeWidth;
    }
    public AbstractFig(float x, float y, int strokeWidth = 1)
    {
        MyPoint = new MyPoint(x, y);
        StrokeColor = Color.Black;
        FillColor = Color.Black;
        StrokeWidth = strokeWidth;
    }


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
        }
    }

    public override void InitializePath()
    {
        base.InitializePath();
        if (MyPoint != null)
        {
            MyPoint prev = Vertices != null ? Vertices[0] : MyPoint;
            _path.AddLine( new PointF(MyPoint.X, MyPoint.Y), new PointF(prev.X, prev.Y));
        }
        for (int i = 1; i < Vertices.Count ; i++)
        {
            MyPoint prev = Vertices[i - 1];
            _path.AddLine(new PointF(prev.X, prev.Y), new PointF(Vertices[i].X, Vertices[i].Y));

        }
    }
     public void Add(float x, float y)
    {
        MyPoint prev = Vertices != null && Vertices.Count > 0 ? Vertices[Vertices.Count - 1] : MyPoint;
        MyPoint curr = new MyPoint(x, y);
        _path.AddLine(new PointF(prev.X, prev.Y), new PointF(curr.X, curr.Y));
        //Logger.WriteLog("Add: prev point (" + prev.X + "," + prev.Y + ") current (" + x + "," + y + ")");
        Vertices.Add(new MyPoint(x, y));
    }


    public override void Draw(Graphics g)
    {
        if (_path == null)
            InitializePath(); // for desrialize
        if (IsSelected)
        {
            Pen surrundingRec = new Pen(SELECTED_COLOR, StrokeWidth / 2);
            surrundingRec.DashStyle = DashStyle.Dash;
            g.DrawRectangle(surrundingRec, _path.GetBounds().X, _path.GetBounds().Y, _path.GetBounds().Width, _path.GetBounds().Height);  // surrounding rectangle
        }
        if (Pen == null) 
            Pen = new Pen(StrokeColor, StrokeWidth); // for desrialize
        g.DrawPath(Pen,_path);
    }

    public override bool isInside(float x, float y)
    {
        return  isOnPath(x,y);
    }

    public override bool isOnPath(float x, float y)
    {
        InitializePath(); // for desrialize
        if (Pen == null)
            Pen = new Pen(StrokeColor, StrokeWidth); // for desrialize
        return _path.IsOutlineVisible(x, y, Pen);
    }
    public override bool isInsideSurrounding(float x, float y)
    {
        RectangleF recf = new RectangleF(_path.GetBounds().X, _path.GetBounds().Y, _path.GetBounds().Width, _path.GetBounds().Height);  // surrounding rectangle
        return recf.Contains(x, y);
    }
            

    public override void Change(float x, float y)
    {
        Add(x, y);
    }

    public override void Move(float xOffset, float yOffset)
    {
        MyPoint.X += xOffset;
        MyPoint.Y += yOffset;
        
        for (int i = 0; i < Vertices.Count; i++)
        {
            Vertices[i].X += xOffset;
            Vertices[i].Y += yOffset;
        }
            Matrix m = new Matrix();
            m.Translate(xOffset, yOffset);
            _path.Transform(m);
    }

}
