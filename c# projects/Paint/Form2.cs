namespace Paint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Circle circle = new Circle();
            //MessageBox.Show("CIRCLE: x = " + circle.X + " y = " + circle.Y + " r = " + circle.Radius);
            //Quadrilateral quad = new Quadrilateral(1,3);
            //MessageBox.Show("QUAD: x = " + quad.X + " y = " + quad.Y + " w = " + quad.Width + " h = " + quad.Height);
            //MessageBox.Show("QUAD: x = " + quad.SecondPoint.X + " y = " + quad.SecondPoint.Y + " w = " + quad.Width + " h = " + quad.Height);
            //MessageBox.Show("isInside: " + quad.isInside(new Point(0.5f,2.999f)));
        }
    }
}