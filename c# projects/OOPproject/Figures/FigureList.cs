﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

[Serializable]
public class FigureList
{
    protected List<Figure> figures;

    public FigureList()
    {
        figures = new List<Figure>();
    }
    public FigureList(FigureList flist)
    {
        figures = new List<Figure>();
        for (int i = 0; i < flist.NextIndex; i++)
        {
            figures.Add(flist[i]);
        }
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
            if (index < 0 || index >= figures.Count)
                return (Figure)null;
            return (Figure)figures[index];
        }
        set
        {
            if (index <= figures.Count)
                figures.Add(value);
        }
    }

    public void Remove(int element)
    {
        if (element >= 0 && element < figures.Count)
        {
            figures.RemoveAt(element);
        }
    }

    public void DrawAll(Graphics g)
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
    public void Clear()
    {
        figures.Clear();
    }

    public int Find(float x, float y)
    {
        for (int i = NextIndex - 1; i >= 0; i--)
        {
            if (this[i].isInside(x, y))
            {
                return i;
            }
        }
        return -1;
    }
}