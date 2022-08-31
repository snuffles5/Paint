using System;
using System.Drawing;

public class AbstractFigure : Figure
{

    //public AbstractFigure() : this(new Point()) { }
    //public AbstractFigure(Point[] vertices, Color strokeColor, Color fillColor)
    //{
    //    Vertices = vertices;
    //    StrokeColor = strokeColor;
    //    FillColor = fillColor;
    //}
    //public AbstractFigure(Point p)
    //{
    //    Vertices = new Point[] {p};
    //    StrokeColor = Color.Black;
    //    FillColor = Color.Black;
    //}
    //public void addVertex(Point p)
    //{
    //    int i; 
    //    Point[] points = new Point[Vertices.Length];
    //    for (i = 0; i < Vertices.Length; i++)
    //    {
    //        points[i] = Vertices[i];
    //    }
    //    points[i] = p;
    //    Vertices = points;
    //}
 

    public override void Draw(Graphics graphic) 
    {
        //SolidBrush br = new SolidBrush(FillColor);
        //Pen pen = new Pen(StrokeColor, StrokeWidth);
        ////g.FillRectangle(br, X - width / 2, Y - height / 2, width, height);
        //graphic.FillPolygon(br, new System.Drawing.Point[]
        //{
        //    new System.Drawing.Point( (int)Math.Floor(Vertices[0].X), (int)Math.Floor(Vertices[0].Y)),
        //    new System.Drawing.Point( (int)Math.Floor(Vertices[1].X), (int)Math.Floor(Vertices[1].Y)),
        //    new System.Drawing.Point( (int)Math.Floor(Vertices[2].X), (int)Math.Floor(Vertices[2].Y)),
        //    new System.Drawing.Point( (int)Math.Floor(Vertices[3].X), (int)Math.Floor(Vertices[3].Y)),
            
        //});
        //graphic.DrawPolygon(pen, new System.Drawing.Point[]
        //{
        //    new System.Drawing.Point( (int)Math.Floor(Vertices[0].X), (int)Math.Floor(Vertices[0].Y)),
        //    new System.Drawing.Point( (int)Math.Floor(Vertices[1].X), (int)Math.Floor(Vertices[1].Y)),
        //    new System.Drawing.Point( (int)Math.Floor(Vertices[2].X), (int)Math.Floor(Vertices[2].Y)),
        //    new System.Drawing.Point( (int)Math.Floor(Vertices[3].X), (int)Math.Floor(Vertices[3].Y)),

        //});
    }
    public abstract bool isInside(MyPoint MyPoint)
    {
        throw new NotImplementedException(); // TODO: Sorry, wasn't implemented yet
        return false;
    }

    ~AbstractFigure() { System.Diagnostics.Debug.WriteLine("Destructor AbstractFigure"); }

}
