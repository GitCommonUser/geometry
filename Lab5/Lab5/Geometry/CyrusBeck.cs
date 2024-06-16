
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

        Point[] vert = new Point[regionList.Count];
        for(int i = 0; i < regionList.Count; i++){
            vert[i] = regionList[i];
        }
            
        Point[] line = new Point[2];
        line[0] = figureList[figureList.Count - 1];
        line[1] = figureList[0];
        Point[] result = new Point[2];
        result = CyrusBeck1(vert, line);
        lines.Add(new Line(result[0],result[1], Color.Blue));
        for(int i = 0; i < figureList.Count-1; i++){

            line[0] = figureList[i];
            line[1] = figureList[i+1];
            result = CyrusBeck1(vert, line);
            lines.Add(new Line(result[0], result[1], Color.Blue));
        }

        return lines;

    }



    /// TEST
    static Point[] CyrusBeck1(Point[] vertices, Point[] line)
    {
        // Значение временного владельца, которое будет возвращено
        Point[] newPair = new Point[2];

        // Нормали инициализируются динамически (можно делать это и статически, не имеет значения)
        Point[] normal = new Point[vertices.Length];

        // Считаем нормали
        for (int i = 0; i < vertices.Length; i++)
        {
            normal[i] = new Point(0,0);
            normal[i].Y = vertices[(i + 1) % vertices.Length].X - vertices[i].X;
            normal[i].X = vertices[i].Y - vertices[(i + 1) % vertices.Length].Y;
        }

        // Считаем P1 - P0
        Point P1_P0 = new Point(line[1].X - line[0].X, line[1].Y - line[0].Y);

        // Задаём массив для всех значений p1-p0
        Point[] P0_PEi = new Point[vertices.Length];

        // Считаем значения P0 - PEi для всех вершин
        for (int i = 0; i < vertices.Length; i++)
        {
            P0_PEi[i] = new Point(0,0);
            P0_PEi[i].X = vertices[i].X - line[0].X;
            P0_PEi[i].Y = vertices[i].Y - line[0].Y;
        }

        int[] numerator = new int[vertices.Length], denominator = new int[vertices.Length];

        // Считаем числитель и знаменатель

        for (int i = 0; i < vertices.Length; i++)
        {
            numerator[i] = Dot(normal[i], P0_PEi[i]);
            denominator[i] = Dot(normal[i], P1_P0);
        }

        // Инизицализируем массив с параметром t
        float[] t = new float[vertices.Length];

        // Создаем два вектора, называемых "не входящими
        // и "не выходящими", чтобы сгруппировать "т"
        List<float> tE = new List<float>(), tL = new List<float>();

        // Вычисление "t" и группировка их соответствующим образом
        for (int i = 0; i < vertices.Length; i++)
        {
            t[i] = (float)numerator[i] / (float)denominator[i];

            if (denominator[i] > 0)
                tE.Add(t[i]);
            else
                tL.Add(t[i]);
        }

        // Инициализируем последние два значения 't'
        float[] temp = new float[2];

        // Берем максимальное значение для всех 'tE' и 0, таким образом, нажимаем 0
        tE.Add(0f);
        temp[0] = Max(tE);

        // Берем минимальное значение всех 'tL' и 1, таким образом, нажимаем 1
        tL.Add(1f);
        temp[1] = Min(tL);

 
        if (temp[0] > temp[1])
        {
            newPair[0] = new Point(-1, -1);
            newPair[1] = new Point(-1, -1);
            Console.WriteLine("here");
            return newPair;
        }

        // Вычисление координат в терминах x и y
        newPair[0] = new Point((int)(line[0].X + P1_P0.X * temp[0]), (int)(line[0].Y + P1_P0.Y * temp[0]));
        newPair[1] = new Point((int)(line[0].X + P1_P0.X * temp[1]), (int)(line[0].Y + P1_P0.Y * temp[1]));

        return newPair;
    }

    static int Dot(Point p0, Point p1)
    {
        return (int)(p0.X * p1.X + p0.Y * p1.Y);
    }

    // Function to calculate the max from a list of floats
    static float Max(List<float> t)
    {
        return t.Max();
    }

    // Function to calculate the min from a list of floats
    static float Min(List<float> t)
    {
        return t.Min();
    }
}