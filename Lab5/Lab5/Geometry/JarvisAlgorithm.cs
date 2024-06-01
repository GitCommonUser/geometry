

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Lab5;

public static class JarvisAlgorithm{


    public static List<Line> Execute(List<Point> _pointList){

        List<Line> lineList = new List<Line>();

        _pointList = _pointList.OrderBy(p => p.Y).ToList();

        List<Point> pointList = new List<Point>();
        for(int i = 0 ; i < _pointList.Count; i++){

            pointList.Add(_pointList[i]);
        }


        //int index = 0;
        List<Point> finalList = new List<Point>();
        finalList.Add(pointList[0]);

        pointList.RemoveAt(0);
        pointList.Add(_pointList[0]);

        int index = 0;
        while(true){

            index = 0;
            for(int i = 1; i < pointList.Count; i++ ){

                if(CheckRotation(finalList[finalList.Count-1],pointList[index], pointList[i])){

                    index = i;

                }

            }

            if(pointList[index] == finalList[0]){

                break;
            }
            else{
                finalList.Add(pointList[index]);
                pointList.RemoveAt(index);
            }

        }


        // Create lines

        lineList.Add(new Line(finalList[0], finalList[finalList.Count-1], Color.Blue));
        for(int i = 0 ;i < finalList.Count-1; i++){

            lineList.Add(new Line(finalList[i],finalList[i+1],Color.Blue));
        }


        return lineList;
    }



    private static bool CheckRotation(Point A, Point B, Point C){

        if((B.X - A.X) * (C.Y - A.Y) - (C.X - A.X) * (B.Y - A.Y) < 0){
            return true;
        }

        return false;
    }
}