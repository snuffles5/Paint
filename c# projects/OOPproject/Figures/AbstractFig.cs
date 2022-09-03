using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;

[Serializable]
public class AbstractFig : Figure 
{
    List<MyPoint> _vertices = new List<MyPoint>();
    public GraphicsPath _path = new GraphicsPath();
    public AbstractFig() : this((List <MyPoint>) null) 
    {
        Pen = new Pen(StrokeColor, StrokeWidth);
    }
    public AbstractFig(List<MyPoint> vertices, int strokeWidth = 1)
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
        StrokeColor = Color.Black;
        FillColor = Color.Black;
        StrokeWidth = strokeWidth;
        Pen = new Pen(StrokeColor, StrokeWidth);
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
     public void Add(float x, float y)
    {
        MyPoint prev = Vertices != null && Vertices.Count > 0 ? Vertices[Vertices.Count - 1] : MyPoint;
        _path.AddLine(new PointF(prev.X, prev.Y), new PointF(x, y));
        Vertices.Add(new MyPoint(x, y));
    }


    public override void Draw(Graphics g)
    {
        if(IsSelected)
            Pen = new Pen(SELECTED_COLOR, StrokeWidth);
        else
            Pen = new Pen(StrokeColor, StrokeWidth);
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

    public override void Move(float x, float y)
    {
     //TODO   
    }



    //~AbstractFig() { System.Diagnostics.Debug.WriteLine("Destructor AbstractFig"); }

}
