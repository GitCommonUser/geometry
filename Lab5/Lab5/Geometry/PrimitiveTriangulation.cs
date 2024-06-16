


using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Lab5;

public static class PrimitiveTriangulation{

    private static List<Line> result = new List<Line>();
    private static List<Point> figure = new List<Point>();
    public static List<Line> Execute(List<Point> _figure){

        result.Clear();
        figure.Clear();

        for(int i = 0; i < _figure.Count; i++){
            figure.Add(_figure[i]);
        }


        bool isFind = false;
        // Проходим по всем точкам
        for(int i = 0; i < figure.Count && !isFind; i++){

            if(result.Count - 3 != figure.Count){

                // Создаем все возможные диагонали
                for(int k = 0; k < figure.Count; k++){

                    if(i != k){


                        // Проверяем все возможные пересечения
                        bool isCross = false;
                        Console.WriteLine("try");
                        //isCross = IsSegmentCross(figure[i], figure[k], figure[figure.Count-1], figure[0]);
                        if(IsSegmentCross(figure[i], figure[k], figure[figure.Count - 1], figure[0])){
                            Point crossPoint = FindCrossPoint(figure[i],figure[k],figure[figure.Count -1], figure[0]);
                            if(!((crossPoint.X == figure[figure.Count-1].X && crossPoint.Y == figure[figure.Count - 1].Y )|| (crossPoint.X == figure[0].X && crossPoint.Y == figure[0].Y))){
                                isCross = true;
                            }
                        }
                        for (int j = 0; j < figure.Count-1 && !isCross ; j++){
                            //isCross = IsSegmentCross(figure[i], figure[k], figure[j], figure[j+1]);
                            Point crossPoint = FindCrossPoint(figure[i], figure[k], figure[j], figure[j+1]);
                            if (!((crossPoint.X == figure[j].X && crossPoint.Y == figure[j].Y) || (crossPoint.X == figure[j+1].X && crossPoint.Y == figure[j+1].Y)))
                            {
                                isCross = true;
                            }
                        }

                        // Если данная диагональ НЕ пересеклась ни с каким отрезком многоугольника
                        if(!isCross){
                            Console.WriteLine("try1");
                            Line line = new Line(figure[i], figure[k], Color.Blue);

                            bool isInside = true;
                            if(CalculateScalar(new Vector2(figure[i].X - figure[k].X, figure[i].Y - figure[i].X), new Vector2(figure[figure.Count-1].X - figure[0].X, figure[figure.Count-1].Y - figure[0].Y)) >= 0){
                                for (int j = 0; j < figure.Count - 1 && isInside; j++)
                                {
                                    if(CalculateScalar(new Vector2(figure[i].X - figure[k].X, figure[i].Y - figure[i].X), new Vector2(figure[j].X - figure[j+1].X, figure[j].Y - figure[j+1].Y)) < 0){
                                        isInside = false;
                                    }
                                }
                            }
                            else{
                                for (int j = 0; j < figure.Count - 1 && isInside; j++)
                                {
                                    if (CalculateScalar(new Vector2(figure[i].X - figure[k].X, figure[i].Y - figure[i].X), new Vector2(figure[j].X - figure[j + 1].X, figure[j].Y - figure[j + 1].Y)) >= 0)
                                    {
                                        isInside = false;
                                    }
                                }
                            }

                            if(isInside){
                                result.Add(line);
                            }
                        }
                    }
                }

            }
            else{
                isFind = true;
            }
        }



        return result;

    }

    private static float CalculateScalar(Vector2 vecA, Vector2 vecB){

        return vecA.X*vecB.X + vecA.Y*vecB.Y;
    }

    private static Point FindCrossPoint(Point A, Point B, Point C, Point D)
    {
        float a1 = B.Y - A.Y, b1 = A.X - B.X, c1 = -A.X * B.Y + A.Y * B.X;
        float a2 = D.Y - C.Y, b2 = C.X - D.X, c2 = -C.X * D.Y + C.Y * D.X;

        Point crossPoint = new Point(1, 1);
        crossPoint.X = -1 * (Solve(c1, b1, c2, b2) / Solve(a1, b1, a2, b2));
        crossPoint.Y = -1 * (Solve(a1, c1, a2, c2) / Solve(a1, b1, a2, b2));

        return crossPoint;
    }


    private static bool IsSegmentCross(Point A, Point B, Point C, Point D)
    {
        float a1 = B.Y - A.Y, b1 = A.X - B.X, c1 = -A.X * B.Y + A.Y * B.X;
        float a2 = D.Y - C.Y, b2 = C.X - D.X, c2 = -C.X * D.Y + C.Y * D.X;


        Point crossPoint = new Point(1, 1);
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

    private static float Solve(float x1, float y1, float x2, float y2)
    {
        return x1 * y2 - y1 * x2;
    }
}