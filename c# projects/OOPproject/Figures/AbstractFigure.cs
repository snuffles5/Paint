﻿using System;
using System.Drawing;

public class AbstractFigure : Figure // not supporting rotation
{

    public AbstractFigure() : this(new Point()) { }
    public AbstractFigure(Point[] vertices, Color strokeColor, Color fillColor)
    {
        Vertices = vertices;
        StrokeColor = strokeColor;
        FillColor = fillColor;
    }
    public AbstractFigure(Point p)
    {
        Vertices = new Point[] {p};
        StrokeColor = Color.Black;
        FillColor = Color.Black;
    }
    public void addVertex(Point p)
    {
        int i; 
        Point[] points = new Point[Vertices.Length];
        for (i = 0; i < Vertices.Length; i++)
        {
            points[i] = Vertices[i];
        }
        points[i] = p;
        Vertices = points;
    }
 

    public override void Draw(Graphics graphic) 
    {
        SolidBrush br = new SolidBrush(FillColor);
        Pen pen = new Pen(StrokeColor, StrokeWidth);
        //g.FillRectangle(br, X - width / 2, Y - height / 2, width, height);
        graphic.FillPolygon(br, new System.Drawing.Point[]
        {
            new System.Drawing.Point( (int)Math.Floor(Vertices[0].X), (int)Math.Floor(Vertices[0].Y)),
            new System.Drawing.Point( (int)Math.Floor(Vertices[1].X), (int)Math.Floor(Vertices[1].Y)),
            new System.Drawing.Point( (int)Math.Floor(Vertices[2].X), (int)Math.Floor(Vertices[2].Y)),
            new System.Drawing.Point( (int)Math.Floor(Vertices[3].X), (int)Math.Floor(Vertices[3].Y)),
            
        });
        graphic.DrawPolygon(pen, new System.Drawing.Point[]
        {
            new System.Drawing.Point( (int)Math.Floor(Vertices[0].X), (int)Math.Floor(Vertices[0].Y)),
            new System.Drawing.Point( (int)Math.Floor(Vertices[1].X), (int)Math.Floor(Vertices[1].Y)),
            new System.Drawing.Point( (int)Math.Floor(Vertices[2].X), (int)Math.Floor(Vertices[2].Y)),
            new System.Drawing.Point( (int)Math.Floor(Vertices[3].X), (int)Math.Floor(Vertices[3].Y)),

        });
    }
    public override bool isInside(Point point) 
    {
        float angle01 = (float)Math.Atan2(Vertices[1].Y - point.Y, Vertices[1].X - point.X) -
                (float)Math.Atan2(Vertices[0].Y - point.Y, Vertices[0].X - point.X);

        float angle02 = (float)Math.Atan2(Vertices[2].Y - point.Y, Vertices[2].X - point.X) -
                (float)Math.Atan2(Vertices[3].Y - point.Y, Vertices[3].X - point.X);

        float angle03 = (float)Math.Atan2(Vertices[1].Y - point.Y, Vertices[1].X - point.X) -
                (float)Math.Atan2(Vertices[2].Y - point.Y, Vertices[2].X - point.X);

        float angle04 = (float)Math.Atan2(Vertices[3].Y - point.Y, Vertices[3].X - point.X) -
                (float)Math.Atan2(Vertices[0].Y - point.Y, Vertices[0].X - point.X);

        return (angle01 + angle02 + angle03 + angle04 == 2 * Math.PI);

        // p1 , p2, p3
        // 01 APB (point, 0, 1) 
        // 02 DPC (point, 3, 2)
        // 03 CPB (point, 2, 1)
        // 04 APD (point, 0, 3)
        //sum_of_angles = θ1 + θ2 + θ3 + θ4 = 2 π->Point is inside]

        //sum_of_angles = θ1 + θ2 + θ3 + θ4 = 0->Point is outside.
        //https://towardsdatascience.com/is-the-point-inside-the-polygon-574b86472119

        //double result = Math.Atan2(P3.y - P1.y, P3.x - P1.x) -
        //        Math.Atan2(P2.y - P1.y, P2.x - P1.x);

        // todo -> verify implement
    }

    ~AbstractFigure() { System.Diagnostics.Debug.WriteLine("Destructor AbstractFigure"); }

}