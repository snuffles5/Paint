using System;
using System.Drawing;
using System.Drawing.Drawing2D;

[Serializable]
public abstract class Quadrilateral : Figure 
{
    MyPoint[] _vertices = new MyPoint[3];
    [field: NonSerialized] public GraphicsPath _path = new GraphicsPath();
    public Quadrilateral() : this(new MyPoint(), new MyPoint(), new MyPoint(), new MyPoint()) { }
    public Quadrilateral(MyPoint fPoint, MyPoint[] vertices, int strokeWidth = 1)
    {
        MyPoint = fPoint;
        // save the vertices only if all exist, if not set them all to default X,Y values
        for (int i = 0; vertices.Length == 3 && i < vertices.Length; i++)
        {
            Vertices[i] = vertices[i];
        }
        StrokeColor = Color.Black;
        FillColor = Color.Black;
        StrokeWidth = strokeWidth;
    } 
    
    public Quadrilateral(MyPoint fPoint, MyPoint[] vertices, Color strokeColor, Color fillColor, int strokeWidth = 1)
    {
        MyPoint = fPoint;
        for (int i = 0; vertices.Length == 3 && i < vertices.Length; i++)
        {
            Vertices[i] = vertices[i];
        }
        StrokeColor = strokeColor;
        FillColor = fillColor;
        StrokeWidth = strokeWidth;
    }
    public Quadrilateral(MyPoint p1, MyPoint p2, MyPoint p3, MyPoint p4, int strokeWidth = 1)
    {
        MyPoint = p1;
        Vertices[0] = p2;
        Vertices[1] = p3;
        Vertices[2] = p4;
        StrokeColor = Color.Black;
        FillColor = Color.Black;
        StrokeWidth = strokeWidth;
    }
    public MyPoint[] Vertices
    {
        get
        {
            return _vertices;
        }
        set
        {
            for (int i = 0; i < value.Length; i++)
            {

                _vertices[i] = value[i];
            }
        }
    }

}
