using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point 
{
    public float x { get; set; }
    public float y { get; set; }

    public Point(float newX, float newY)
    {
        x = newX;
        y = newY;
    }

    public bool isEqual(Point p)
    {
        if (p != null)
        {
            return ((this.x == p.x) && (this.y == p.y));
        }
        else
        {
            return false;
        }
    }
}
