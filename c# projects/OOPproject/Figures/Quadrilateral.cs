using System;
using System.Drawing;

public class Quadrilateral : Figure // not supporting rotation
{
    myPoint[] _vertices;
    public Quadrilateral() : this(new myPoint(), new myPoint(), new myPoint(), new myPoint()) { }
    public Quadrilateral(myPoint[] vertexs)
    {
        _vertices = new myPoint[4];
        if (vertexs.Length == 4)
            Vertices = vertexs;
        StrokeColor = Color.Black;
        FillColor = Color.Black;
    }
    public Quadrilateral(myPoint p1, myPoint p2, myPoint p3, myPoint p4)
    {
        _vertices = new myPoint[4];
        Vertices = new myPoint[] { p1, p2, p3, p4 };
        StrokeColor = Color.Black;
        FillColor = Color.Black;
    }



    public myPoint[] Vertices
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

    public override void Draw(Graphics graphic)
    {
        SolidBrush br = new SolidBrush(FillColor);
        Pen pen = new Pen(StrokeColor, StrokeWidth);
        //g.FillRectangle(br, X - width / 2, Y - height / 2, width, height);
        graphic.FillPolygon(br, new System.Drawing.myPoint[]
        {
            new System.Drawing.myPoint( (int)Math.Floor(Vertices[0].X), (int)Math.Floor(Vertices[0].Y)),
            new System.Drawing.myPoint( (int)Math.Floor(Vertices[1].X), (int)Math.Floor(Vertices[1].Y)),
            new System.Drawing.myPoint( (int)Math.Floor(Vertices[2].X), (int)Math.Floor(Vertices[2].Y)),
            new System.Drawing.myPoint( (int)Math.Floor(Vertices[3].X), (int)Math.Floor(Vertices[3].Y)),

        });
        graphic.DrawPolygon(pen, new System.Drawing.myPoint[]
        {
            new System.Drawing.myPoint( (int)Math.Floor(Vertices[0].X), (int)Math.Floor(Vertices[0].Y)),
            new System.Drawing.myPoint( (int)Math.Floor(Vertices[1].X), (int)Math.Floor(Vertices[1].Y)),
            new System.Drawing.myPoint( (int)Math.Floor(Vertices[2].X), (int)Math.Floor(Vertices[2].Y)),
            new System.Drawing.myPoint( (int)Math.Floor(Vertices[3].X), (int)Math.Floor(Vertices[3].Y)),

        });
    }
    public override bool isInside(myPoint myPoint)
    {
        float angle01 = (float)Math.Atan2(Vertices[1].Y - myPoint.Y, Vertices[1].X - myPoint.X) -
                (float)Math.Atan2(Vertices[0].Y - myPoint.Y, Vertices[0].X - myPoint.X);

        float angle02 = (float)Math.Atan2(Vertices[2].Y - myPoint.Y, Vertices[2].X - myPoint.X) -
                (float)Math.Atan2(Vertices[3].Y - myPoint.Y, Vertices[3].X - myPoint.X);

        float angle03 = (float)Math.Atan2(Vertices[1].Y - myPoint.Y, Vertices[1].X - myPoint.X) -
                (float)Math.Atan2(Vertices[2].Y - myPoint.Y, Vertices[2].X - myPoint.X);

        float angle04 = (float)Math.Atan2(Vertices[3].Y - myPoint.Y, Vertices[3].X - myPoint.X) -
                (float)Math.Atan2(Vertices[0].Y - myPoint.Y, Vertices[0].X - myPoint.X);

        return (angle01 + angle02 + angle03 + angle04 == 2 * Math.PI);

        // p1 , p2, p3
        // 01 APB (myPoint, 0, 1) 
        // 02 DPC (myPoint, 3, 2)
        // 03 CPB (myPoint, 2, 1)
        // 04 APD (myPoint, 0, 3)
        //sum_of_angles = θ1 + θ2 + θ3 + θ4 = 2 π->myPoint is inside]

        //sum_of_angles = θ1 + θ2 + θ3 + θ4 = 0->myPoint is outside.
        //https://towardsdatascience.com/is-the-myPoint-inside-the-polygon-574b86472119

        //double result = Math.Atan2(P3.y - P1.y, P3.x - P1.x) -
        //        Math.Atan2(P2.y - P1.y, P2.x - P1.x);

        // todo -> verify implement
    }

    ~Quadrilateral() { System.Diagnostics.Debug.WriteLine("Destructor Quadrilateral"); }

}
