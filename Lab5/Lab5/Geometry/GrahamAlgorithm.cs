
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Lab5;

public static class GrahamAlgorithm{


    public static List<Line> Execute(List<Point> _pointList){

        List<Line> lineList = new List<Line>();
        List<Point> pointList = new List<Point>();
        for (int i = 0; i < _pointList.Count; i++){

            pointList.Add(_pointList[i]);
        }

        // STEP 2
        float averageX = 0, averageY = 0;

        foreach(Point p in pointList){
            averageX += p.X;
            averageY += p.Y;
        }

        averageX /= pointList.Count;
        averageY /= pointList.Count;

        Point A = new Point(averageX, averageY);

        // STEP 3
        List<PolarPoint> polarPoints = new List<PolarPoint>();

        for(int i = 0; i < pointList.Count; i++){


            PolarPoint polarPoint = new PolarPoint(Vector2.Distance(new Vector2(A.X,A.Y),new Vector2(pointList[i].X, pointList[i].Y)),GetAngle(A,pointList[i]), i);
            polarPoints.Add(polarPoint);
        }

        // STEP 4
        polarPoints = polarPoints.OrderBy(p => p.angle).ThenByDescending(p => p.distance).ToList();
        pointList = pointList.OrderBy(x => polarPoints.FindIndex(y => x.pointID == y.pointID)).ToList();

        int index = 0;
        while(index < polarPoints.Count){

            if(index + 1 < polarPoints.Count && polarPoints[index].angle == polarPoints[index+1].angle /*&& (Math.Abs(polarPoints[index].angle) - Math.Abs(polarPoints[index+1].angle)) < 0.01*/){
                polarPoints.RemoveAt(index+1);
                pointList.RemoveAt(index+1);

                index--;
            }

            index++;
        }


        // STEP 5

        index = 0;
        int nextIndex = -1, prevIndex = -1;
        bool isStop = false;

        while(index < pointList.Count){

            if(index == pointList.Count-1){
                prevIndex = index-1;
                nextIndex = 0;
            }
            else if(index == 0){
                prevIndex = pointList.Count-1;
                nextIndex = index+1;
            }
            else{
                prevIndex = index-1;
                nextIndex = index+1;
            }

            if(GetAngle(pointList[prevIndex], pointList[index], pointList[nextIndex]) >= Math.PI){
                pointList.RemoveAt(index);

                index--;
                isStop = true;
            }

            index++;

            if(index <= pointList.Count && isStop){
                index = 0;
                isStop = false;
            }
        }


        // CREATE LINES
        lineList.Add(new Line(pointList[0], pointList[pointList.Count-1], Color.Blue));
        for(int i = 0 ;i < pointList.Count-1; i++){

            lineList.Add(new Line(pointList[i],pointList[i+1],Color.Blue));
        }

        return lineList;
    }

    private static float GetAngle(Point A, Point B){

        return (float)Math.Round(Math.Atan2(B.Y-A.Y, B.X - A.X), 2);
    }

    private static float GetAngle(Point A, Point B, Point C){

        //float angle = (float)Math.Atan2(C.Y-A.Y, C.X - A.X) - (float)Math.Atan2(B.Y - A.Y, B.X - A.X);

        double angle = Math.Atan2(C.Y-A.Y, C.X - A.X) - Math.Atan2(B.Y - A.Y, B.X - A.X);

        if(angle < 0){
            angle = Math.PI + (Math.PI - Math.Abs(angle));
        }
        return (float)angle;
        //return (float)Math.Atan2(C.Y-A.Y, C.X - A.X) - (float)Math.Atan2(B.Y - A.Y, B.X - A.X);
    }
}