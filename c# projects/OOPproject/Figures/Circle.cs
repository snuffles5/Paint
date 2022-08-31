﻿using System;
using System.Drawing;

public class Circle : Figure
{
    const float DEFAULT_RADIUS = 1f;
    float radius;
    public Circle() : this(10, 10, DEFAULT_RADIUS) { }
    public Circle(MyPoint MyPoint, float radius = DEFAULT_RADIUS, int strokeWidth = 1)
    {
        MyPoint = new MyPoint(MyPoint);
        Radius = radius;
        StrokeColor = Color.Black;
        FillColor = Color.Black;
        StrokeWidth = strokeWidth;
    }
    public Circle(Color strokeColor, Color fillColor, MyPoint MyPoint, float radius = DEFAULT_RADIUS, int strokeWidth = 1)
    {
        MyPoint = new MyPoint(MyPoint);
        Radius = radius;
        StrokeColor = strokeColor;
        FillColor = fillColor;
        StrokeWidth = strokeWidth;
    }

    public Circle(float x, float y, float radius = DEFAULT_RADIUS, int strokeWidth = 1)
    {
        MyPoint = new MyPoint(x, y);
        Radius = radius;
        StrokeColor = Color.Black;
        FillColor = Color.Black;
        StrokeWidth = strokeWidth;
    }
    public MyPoint Center { get { return MyPoint; } set { MyPoint = new MyPoint(value.X, value.Y); } }

    public float Radius
    {
        get
        {
            return radius;
        }
        set
        {
            if (radius >= 0)
                radius = value;
            else
                radius = 0;
        }
    }
    public override void Draw(Graphics graphic)
    {
        SolidBrush br = new SolidBrush(FillColor);
        Pen pen = new Pen(StrokeColor, StrokeWidth);
        graphic.FillEllipse(br, X - Radius, Y - Radius, 2 * Radius, 2 * Radius);
        graphic.DrawEllipse(pen, X - Radius, Y - Radius, 2 * Radius, 2 * Radius);
    }
    public override bool isInside(MyPoint MyPoint)
    {
        return Math.Sqrt(Math.Pow(MyPoint.X - X, 2) + Math.Pow(MyPoint.Y - Y, 2)) < Radius;
    }

    ~Circle() { Console.WriteLine("Destructor Circle"); }
}