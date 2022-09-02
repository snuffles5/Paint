using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;

public class AbstractFig : Figure 
{
    List<MyPoint> _vertices = new List<MyPoint>();
    public GraphicsPath _path = new GraphicsPath();
    public AbstractFig() : this((List <MyPoint>) null) { }
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
            Vertices.Add(point);
            _path.AddPolygon(new PointF[] { new PointF(point.X, point.Y) });
        }
    }
     public void Add(float x, float y)
    {
        Vertices.Add(new MyPoint(x,y));
        _path.AddPolygon(new PointF[] { new PointF(x, y)});
    }


    public override void Draw(Graphics g)
    {
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
        return _path.IsOutlineVisible(x, y, Pen);
    }
    //~AbstractFig() { System.Diagnostics.Debug.WriteLine("Destructor AbstractFig"); }

}
