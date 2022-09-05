using OOPproject;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

[Serializable]
public class Rectangle : Quadrilateral // not supporting rotation
{
    float _width;
    float _height;
    public Rectangle(MyPoint point, float width, float height, int strokeWidth = 0): base(point, new MyPoint(point.X + width, point.Y), new MyPoint(point.X + width, point.Y + height), new MyPoint(point.X, point.Y + height))
    {
        Width = width;
        Height = height;
        StrokeColor = Color.Black;
        FillColor = Color.Black;
        StrokeWidth = strokeWidth;
    }
    public Rectangle(float x=0, float y=0, float width = 0, float height = 0, int strokeWidth = 0) : base(new MyPoint(x,y), new MyPoint(x + width, y), new MyPoint(x + width, y + height), new MyPoint(x, y + height))
    {
        StrokeColor = Color.Black;
        FillColor = Color.Black;
        StrokeWidth = strokeWidth;
    }
    public float Width
    {
        get
        {
            return _width;
        }
        set
        {
            if (value >= 0)
                updateParams(MyPoint.X, MyPoint.Y, value, Height);
            else
                updateParams(MyPoint.X, MyPoint.Y, 0, Height);

        }
    }
    public float Height
    {
        get
        {
            return _height;
        }
        set
        {
            if (value >= 0)
                updateParams(MyPoint.X, MyPoint.Y, Width, value);
            else
               updateParams(MyPoint.X, MyPoint.Y, Width, 0);
        }
    }

    private void updateParams(float x, float y, float width, float height) // this method receive width and height after CHECK!
    {
        MyPoint.X = x; 
        MyPoint.Y = y;
        _width = width;
        _height = height;
        Vertices[0] = new MyPoint(x + width, y);
        Vertices[1] = new MyPoint(x + width, y + height);
        Vertices[2] = new MyPoint(x, y + height);
        InitializePath();
        //Vertices = new MyPoint[] { new MyPoint(x + width, y), new MyPoint(x + width, y + height), 
        //new MyPoint(x, y + height) };
    }

    public override void InitializePath()
    {
        base.InitializePath();
        _path.AddLine(MyPoint.X, MyPoint.Y, Vertices[0].X, Vertices[0].Y);
        _path.AddLine(Vertices[0].X, Vertices[0].Y, Vertices[1].X, Vertices[1].Y);
        _path.AddLine(Vertices[1].X, Vertices[1].Y, Vertices[2].X, Vertices[2].Y);
        _path.AddLine(Vertices[2].X, Vertices[2].Y, MyPoint.X, MyPoint.Y);
        
    }

    public override void Draw(Graphics g)
    {
        InitializePath();
        if (IsSelected)
        {
            Pen surrundingRec = new Pen(SELECTED_COLOR, StrokeWidth);
            surrundingRec.DashStyle = DashStyle.Dash;
            surrundingRec.Alignment = PenAlignment.Outset;
            //float diff = 0.01f;
            //g.DrawRectangle(surrundingRec, X - X*diff, Y - Y*diff, Width + Width*diff, Height+ Height*diff);  // surrounding rectangle
            g.DrawRectangle(surrundingRec, X, Y, Width, Height);  // surrounding rectangle
        }
        if (Pen == null)
        {
            Pen = new Pen(StrokeColor, StrokeWidth); // for desrialize
        }
        Pen.Alignment = PenAlignment.Inset;
        SolidBrush br = new SolidBrush(FillColor);
        g.FillRectangle(br, MyPoint.X, MyPoint.Y, Width, Height);
        g.DrawRectangle(Pen, MyPoint.X, MyPoint.Y, Width, Height);
        
    }
    public override bool isInside(float x, float y)
    {
        bool b = (x > MyPoint.X && x < Vertices[0].X &&
            y > MyPoint.Y && y < Vertices[0].Y) || isOnPath(x, y);
        Logger.WriteLog("inside rectangle = " +b );
        return (x > MyPoint.X && x < Vertices[0].X &&
            y > MyPoint.Y && y < Vertices[0].Y) || isOnPath(x,y) ;
    }
    public override bool isOnPath(float x, float y)
    {
        if (Vertices.Length != 0)
        {
            if (Pen == null)
                Pen = new Pen(StrokeColor, StrokeWidth); // for desrialize
            if (_path == null)
                InitializePath(); // for desrialize
            return _path.IsOutlineVisible(x, y, Pen);
        }
        return false;
    }

    public override bool isInsideSurrounding(float x, float y)
    {
        return isInside(x,y) || isOnPath(x,y);
    }

    public override void Change(float x, float y)
    {
        Width = x - X;
        Height = y - Y;
    }
    public override void Move(float offsetX, float offsetY)
    {
        //TODO
        updateParams(X+ offsetX, Y+ offsetY, Width, Height);
    }


    ~Rectangle() { System.Diagnostics.Debug.WriteLine("Destructor Rectangle"); }

}
