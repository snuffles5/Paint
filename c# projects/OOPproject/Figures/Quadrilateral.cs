using System;
using System.Drawing;
using System.Drawing.Drawing2D;

public abstract class Quadrilateral : Figure 
{
    MyPoint[] _vertices = new MyPoint[3];
    public GraphicsPath _path = new GraphicsPath();
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

    //public override void Draw(Graphics graphic)
    //{
    //    SolidBrush br = new SolidBrush(FillColor);
    //    Pen pen = new Pen(StrokeColor, StrokeWidth);
    //    //g.FillRectangle(br, X - width / 2, Y - height / 2, width, height);
    //    graphic.FillPolygon(br, new System.Drawing.MyPoint[]
    //    {
    //        new System.Drawing.Point( (int)Math.Floor(Vertices[0].X), (int)Math.Floor(Vertices[0].Y)),
    //        new System.Drawing.Point( (int)Math.Floor(Vertices[1].X), (int)Math.Floor(Vertices[1].Y)),
    //        new System.Drawing.Point( (int)Math.Floor(Vertices[2].X), (int)Math.Floor(Vertices[2].Y)),
    //        new System.Drawing.Point( (int)Math.Floor(Vertices[3].X), (int)Math.Floor(Vertices[3].Y)),

    //    });
    //    graphic.DrawPolygon(pen, new System.Drawing.MyPoint[]
    //    {
    //        new System.Drawing.Point( (int)Math.Floor(Vertices[0].X), (int)Math.Floor(Vertices[0].Y)),
    //        new System.Drawing.Point( (int)Math.Floor(Vertices[1].X), (int)Math.Floor(Vertices[1].Y)),
    //        new System.Drawing.Point( (int)Math.Floor(Vertices[2].X), (int)Math.Floor(Vertices[2].Y)),
    //        new System.Drawing.Point( (int)Math.Floor(Vertices[3].X), (int)Math.Floor(Vertices[3].Y)),

    //    });
    //}
    //public override bool isInside(Point MyPoint)
    //{
    //    float angle01 = (float)Math.Atan2(Vertices[1].Y - MyPoint.Y, Vertices[1].X - MyPoint.X) -
    //            (float)Math.Atan2(Vertices[0].Y - MyPoint.Y, Vertices[0].X - MyPoint.X);

    //    float angle02 = (float)Math.Atan2(Vertices[2].Y - MyPoint.Y, Vertices[2].X - MyPoint.X) -
    //            (float)Math.Atan2(Vertices[3].Y - MyPoint.Y, Vertices[3].X - MyPoint.X);

    //    float angle03 = (float)Math.Atan2(Vertices[1].Y - MyPoint.Y, Vertices[1].X - MyPoint.X) -
    //            (float)Math.Atan2(Vertices[2].Y - MyPoint.Y, Vertices[2].X - MyPoint.X);

    //    float angle04 = (float)Math.Atan2(Vertices[3].Y - MyPoint.Y, Vertices[3].X - MyPoint.X) -
    //            (float)Math.Atan2(Vertices[0].Y - MyPoint.Y, Vertices[0].X - MyPoint.X);

    //    return (angle01 + angle02 + angle03 + angle04 == 2 * Math.PI);

    //    // p1 , p2, p3
    //    // 01 APB (Point, 0, 1) 
    //    // 02 DPC (Point, 3, 2)
    //    // 03 CPB (Point, 2, 1)
    //    // 04 APD (Point, 0, 3)
    //    //sum_of_angles = θ1 + θ2 + θ3 + θ4 = 2 π->Point is inside]

    //    //sum_of_angles = θ1 + θ2 + θ3 + θ4 = 0->Point is outside.
    //    //https://towardsdatascience.com/is-the-Point-inside-the-polygon-574b86472119

    //    //double result = Math.Atan2(P3.y - P1.y, P3.x - P1.x) -
    //    //        Math.Atan2(P2.y - P1.y, P2.x - P1.x);

    //    // todo -> verify implement
    //}

    ~Quadrilateral() { System.Diagnostics.Debug.WriteLine("Destructor Quadrilateral"); }

}
