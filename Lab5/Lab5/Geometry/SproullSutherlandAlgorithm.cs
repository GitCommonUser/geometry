
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;


namespace Lab5;

public static class SproullSutherlandAlgorithm
{

    private static List<Point> _displayArea = new List<Point>(), _displayFigure = new List<Point>();

    private static List<ByteCode> byteCodes = new List<ByteCode>();

    private static List<Line> lineList = new List<Line>();

    private static float minX, minY, maxX, maxY;

    public static List<Line> Execute(List<Point> displayArea, List<Point> displayFigure)
    {
        _displayArea.Clear();
        _displayFigure.Clear();

        for(int i = 0; i < displayArea.Count; i++){
            Point point = new Point(displayArea[i].X,displayArea[i].Y);
            _displayArea.Add(point);
        }

        for(int i = 0; i < displayFigure.Count; i++){
            Point point = new Point(displayFigure[i].X,displayFigure[i].Y);
            _displayFigure.Add(point);
        }

        /*for (int i = 0; i < displayArea.Count; i++)
        {
            _displayArea.Add(displayArea[i]);
        }

        for (int i = 0; i < displayFigure.Count; i++)
        {
            _displayFigure.Add(displayFigure[i]);
        }*/


        minX = 10000;
        minY = -1;
        maxX = -1;
        maxY = 10000;

        //List<Line> lineList = new List<Line>();
        lineList.Clear();

        // Определение минимальный и максимальный X Y
        foreach (Point p in displayArea)
        {
            if (p.X > maxX)
            {
                maxX = p.X;
            }

            if (p.Y > minY)
            {
                minY = p.Y;
            }

            if (p.X < minX)
            {
                minX = p.X;
            }

            if (p.Y < maxY)
            {
                maxY = p.Y;
            }
        }

        // Получение байт кодов
        //List<ByteCode> byteCodes = new List<ByteCode>();
        byteCodes.Clear();
        for (int i = 0; i < displayFigure.Count; i++)
        {

            bool b0 = true, b1 = true, b2 = true, b3 = true;
            if (displayFigure[i].X >= minX)
            {
                b0 = false;
            }

            if (displayFigure[i].X <= maxX)
            {
                b1 = false;
            }

            if (displayFigure[i].Y <= minY)
            {
                b2 = false;
            }

            if (displayFigure[i].Y >= maxY)
            {
                b3 = false;
            }

            byteCodes.Add(new ByteCode(b0, b1, b2, b3));
        }

        // Определение положений отрезков

        CheckPosition(displayFigure.Count - 1, 0);

        for (int i = 0; i < displayFigure.Count - 1; i++)
        {

            CheckPosition(i, i + 1);
        }



        return lineList;

    }

    private static void CheckPosition(int firstIndex, int secondIndex)
    {

        bool b0 = byteCodes[firstIndex].b0;
        bool b1 = byteCodes[firstIndex].b1;
        bool b2 = byteCodes[firstIndex].b2;
        bool b3 = byteCodes[firstIndex].b3;

        bool a0 = byteCodes[secondIndex].b0;
        bool a1 = byteCodes[secondIndex].b0;
        bool a2 = byteCodes[secondIndex].b0;
        bool a3 = byteCodes[secondIndex].b0;

        if (!b0 && !b1 && !b2 && !b3 && !a0 && !a1 && !a2 && !a3)
        {
            lineList.Add(new Line(_displayFigure[firstIndex], _displayFigure[secondIndex], Color.Blue));
            return;
        }
        else if ((b0 && a0) || (b1 && a1) || (b2 && a2) || (b3 && a3))
        {

            return;
        }
        else
        {


            // Поиск линий, с которыми пересекается отрезок
            List<Line> crossLines = new List<Line>();
            if(IsSegmentCross(_displayFigure[firstIndex], _displayFigure[secondIndex], _displayArea[_displayArea.Count-1], _displayArea[0])){

                crossLines.Add(new Line(_displayArea[_displayArea.Count-1], _displayArea[0], Color.Blue));
            }

            for(int i = 0; i < _displayArea.Count-1; i++){

                if(IsSegmentCross(_displayFigure[firstIndex], _displayFigure[secondIndex], _displayArea[i], _displayArea[i+1])){
                    crossLines.Add(new Line(_displayArea[i], _displayArea[i+1], Color.Blue));

                }
            }

            // Поиск точек пересечений
            List<Point> crossPoints = new List<Point>();

            for(int i = 0; i < crossLines.Count; i++){

                crossPoints.Add(FindCrossPoint(_displayFigure[firstIndex], _displayFigure[secondIndex], crossLines[i].startPoint, crossLines[i].endPoint));
            }


            // Определение и смещение необходимой точки
            if(crossPoints.Count == 0){
                return;
            }
            else if(crossPoints.Count == 1){
                
                if(!a0 && !a1 && !a2 && !a3){

                    //lineList.Add(new Line(_displayFigure[firstIndex], crossPoints[0], Color.Blue));
                    lineList.Add(new Line(crossPoints[0], _displayFigure[secondIndex], Color.Blue));

                }
                else if(!b0 && !b1 && !b2 && !b3){

                    //lineList.Add(new Line(crossPoints[0], _displayFigure[secondIndex], Color.Blue));
                    lineList.Add(new Line(_displayFigure[firstIndex], crossPoints[0], Color.Blue));
                }
                else{

                    if(Vector2.Distance(_displayFigure[firstIndex].GetVector(), crossPoints[0].GetVector()) < Vector2.Distance(_displayFigure[secondIndex].GetVector(), crossPoints[0].GetVector())){
                        lineList.Add(new Line(crossPoints[0], _displayFigure[secondIndex], Color.Blue));
                    }
                    else{
                        lineList.Add(new Line(_displayFigure[firstIndex], crossPoints[0], Color.Blue));
                    }

                }
            }
            else{

                //Console.WriteLine("Точка пересечения :");

                for (int i = 0; i < crossPoints.Count; i++){

                    //Console.WriteLine(crossPoints[i].GetVector());
                    if(Vector2.Distance(_displayFigure[firstIndex].GetVector(), crossPoints[i].GetVector()) < Vector2.Distance(_displayFigure[secondIndex].GetVector(), crossPoints[i].GetVector())){
                        lineList.Add(new Line(crossPoints[i], _displayFigure[secondIndex], Color.Blue));
                    }
                    else{
                        lineList.Add(new Line(_displayFigure[firstIndex], crossPoints[i], Color.Blue));
                    }
                }
            }




            return;

        }
    }

    private static Point IterationMethod(Line line){

        Point C = new Point((line.startPoint.X + line.endPoint.X)/2, (line.startPoint.Y + line.endPoint.Y)/2);

        if(Vector2.Distance(line.startPoint.GetVector(), C.GetVector()) <= 0.01){
            return C;
        }
        else{
            ByteCode codeC = CalculateCode(C);
            ByteCode codeA = CalculateCode(line.startPoint);
            ByteCode codeB = CalculateCode(line.endPoint);

            if(!codeC.b0 && !codeC.b1 && !codeC.b2 && !codeC.b3){
                
            }
            
        }
    }

    private static ByteCode CalculateCode(Point A){

        bool b0 = true, b1 = true, b2 = true, b3 = true;
        if (A.X >= minX)
        {
            b0 = false;
        }

        if (A.X <= maxX)
        {
            b1 = false;
        }

        if (A.Y <= minY)
        {
            b2 = false;
        }

        if (A.Y >= maxY)
        {
            b3 = false;
        }

        return new ByteCode(b0,b1,b2,b3);
    }

    private static bool IsSegmentCross(Point A, Point B, Point C, Point D)
    {
        float a1 = B.Y - A.Y, b1 = A.X - B.X, c1 = -A.X * B.Y + A.Y * B.X;
        float a2 = D.Y - C.Y, b2 = C.X - D.X, c2 = -C.X * D.Y + C.Y * D.X;


        Point crossPoint = new Point(1,1);
        crossPoint.X = -1 * (Solve(c1, b1, c2, b2) / Solve(a1, b1, a2, b2));
        crossPoint.Y = -1 * (Solve(a1, c1, a2, c2) / Solve(a1, b1, a2, b2));


        Point[] abPointsX = { A, B };
        Array.Sort(abPointsX, (a, b) => a.X.CompareTo(b.X));
        Point[] abPointsY = { A, B };
        Array.Sort(abPointsY, (a, b) => a.Y.CompareTo(b.Y));
        Point[] cdPointsX = { C, D };
        Array.Sort(cdPointsX, (a, b) => a.X.CompareTo(b.X));
        Point[] cdPointsY = { C, D };
        Array.Sort(cdPointsY, (a, b) => a.Y.CompareTo(b.Y));

        // Проверка, что точка пересечения лежит внутри всех координат AB и CD
        if ((crossPoint.X >= abPointsX[0].X && crossPoint.X <= abPointsX[1].X && crossPoint.X >= cdPointsX[0].X && crossPoint.X <= cdPointsX[1].X) && (crossPoint.Y >= abPointsY[0].Y && crossPoint.Y <= abPointsY[1].Y && crossPoint.Y >= cdPointsY[0].Y && crossPoint.Y <= cdPointsY[1].Y))
            return true;

        return false;
    }

    //Нету никаких проверок! Использовать только при уверенности, что отрезки точно пересекаются
    public static Point FindCrossPoint(Point A, Point B, Point C, Point D)
    {
        float a1 = B.Y - A.Y, b1 = A.X - B.X, c1 = -A.X * B.Y + A.Y * B.X;
        float a2 = D.Y - C.Y, b2 = C.X - D.X, c2 = -C.X * D.Y + C.Y * D.X;

        Point crossPoint = new Point(1,1);
        crossPoint.X = -1 * (Solve(c1, b1, c2, b2) / Solve(a1, b1, a2, b2));
        crossPoint.Y = -1 * (Solve(a1, c1, a2, c2) / Solve(a1, b1, a2, b2));

        return crossPoint;
    }

    public static float Solve(float x1, float y1, float x2, float y2)
    {
        return x1 * y2 - y1 * x2;
    }


}

