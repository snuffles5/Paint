using System.Collections;
using System.Drawing;

public class FigureList
{
    protected SortedList figures;

    public FigureList()
    {
        figures = new SortedList();
    }
    public int NextIndex
    {
        get
        {
            return figures.Count;
        }
    }
    public Figure this[int index]
    {
        get
        {
            if (index >= figures.Count)
                return (Figure)null;
            return (Figure)figures.GetByIndex(index);
        }
        set
        {
            if (index <= figures.Count)
                figures[index] = value;		
        }
    }

    public void Remove(int element)
    {
        if (element >= 0 && element < figures.Count)
        {
            for (int i = element; i < figures.Count - 1; i++)
                figures[i] = figures[i + 1];
            figures.RemoveAt(figures.Count - 1);
        }
    }

    public void DrawAll(Graphics g) // Todo: Verify if needed
    {
        Figure prev, cur;
        for (int i = 1; i < figures.Count; i++)
        {
            prev = (Figure)figures[i - 1];
            cur = (Figure)figures[i];
            //g.DrawLine(Pens.Yellow, prev.X, prev.Y, cur.X, cur.Y);

            ((Figure)figures[i]).Draw(g);
        }
        for (int i = 0; i < figures.Count; i++)
            ((Figure)figures[i]).Draw(g);
    }
}