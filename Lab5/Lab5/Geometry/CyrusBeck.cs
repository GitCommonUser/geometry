
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Lab5;

public static class CyrusBeck{

    private static List<Point> _figureList = new List<Point>();
    private static List<Point> _regionList = new List<Point>();

    private static List<Point> normalList = new List<Point>();

    private static List<Line> lines = new List<Line>();

    public static List<Line> Execute(List<Point> figureList, List<Point> regionList){

        lines.Clear();
        _figureList.Clear();
        _regionList.Clear();
        normalList.Clear();

        //List<Point> normalList = new List<Point>();

        for(int i = 0; i < regionList.Count; i++){
            Point point = new Point(regionList[i].X,regionList[i].Y);
            _regionList.Add(point);

            normalList.Add(new Point(regionList[(i+1) % regionList.Count].X - regionList[i].X,regionList[i].Y - regionList[(i+1) % regionList.Count].Y));
        }

        for(int i = 0; i < figureList.Count; i++){
            Point point = new Point(figureList[i].X,figureList[i].Y);
            _figureList.Add(point);
        }
        

        /*for(int i =0; i < figureList.Count; i++){
            _figureList.Add(figureList[i]);
        }

        for(int i =0; i < regionList.Count; i++){
            _regionList.Add(regionList[i]);

            normalList.Add(new Point(regionList[(i+1) % regionList.Count].X - regionList[i].X,regionList[i].Y - regionList[(i+1) % regionList.Count].Y));
        }*/

        Sub(_figureList[_figureList.Count-1], _figureList[0]);

        for(int i = 0; i < _figureList.Count-1; i++){

            Sub(_figureList[i], _figureList[i+1]);
        }

        return lines;

    }

    private static void Sub(Point a, Point b){

        Point[] pair = new Point[2];

        Vector2 P1_P0 = new Vector2(b.X - a.X , b.Y - a.Y);
        Vector2[] P0_PEi = new Vector2[_regionList.Count];

        for(int i = 0; i < _regionList.Count; i++){
            P0_PEi[i].X = _regionList[i].X - a.X;
            P0_PEi[i].Y = _regionList[i].Y - a.Y;
        }

        float[] numerator = new float[_regionList.Count], denominator = new float[_regionList.Count];

        for (int i = 0; i < _regionList.Count; i++)
        {
            numerator[i] = DotProduct(normalList[i].GetVector(), P0_PEi[i]);
            denominator[i] = DotProduct(normalList[i].GetVector(), P1_P0);
        }

        float[] t = new float[_regionList.Count];

        List<float> tE = new List<float>(), tL = new List<float>();

        // Calculating 't' and grouping them accordingly
        for (int i = 0; i < _regionList.Count; i++)
        {
            t[i] = (float)numerator[i] / (float)denominator[i];

            if (denominator[i] > 0)
                tE.Add(t[i]);
            else
                tL.Add(t[i]);
        }

        // Initializing the final two values of 't'
        float[] temp = new float[2];

        // Taking the max of all 'tE' and 0, so pushing 0
        tE.Add(0f);
        //temp[0] = Max(tE);
        temp[0] = tE.Max();

        // Taking the min of all 'tL' and 1, so pushing 1
        tL.Add(1f);
        temp[1] = tL.Min();
        //temp[1] = Min(tL);

    
        if (temp[0] > temp[1])
        {
            pair[0] = new Point(-1,-1);
            pair[1] = new Point(-1, -1);

            lines.Add(new Line(pair[0],pair[1],Color.Blue));
            return;
            //return newPair;
        }

        pair[0] = new Point((int)(a.X + P1_P0.X * temp[0]), (int)(a.Y + P1_P0.Y * temp[0]));
        pair[1] = new Point((int)(a.X + P1_P0.X * temp[1]), (int)(a.Y + P1_P0.Y * temp[1]));

        lines.Add(new Line(pair[0],pair[1],Color.Blue));
        return;
        //return newPair;
    }

    private static float DotProduct(Vector2 p0, Vector2 p1)
    {
        return p0.X * p1.X + p0.Y * p1.Y;
    }
}