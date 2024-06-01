
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace Lab5;

public static class CohenSutherlandAlgorithm{

    private static List<Point> _displayArea = new List<Point>(), _displayFigure = new List<Point>(), _displayFigureCopy = new List<Point>();

    private static List<ByteCode> byteCodes = new List<ByteCode>();
    private static List<ByteCode> byteCodesCopy = new List<ByteCode>();

    private static List<Line> lineList = new List<Line>();

    private static float minX, minY, maxX, maxY;

    public static List<Line> Execute(List<Point> displayArea, List<Point> displayFigure){

        //_displayArea = displayArea;
        //_displayFigure = displayFigure;

        for(int i = 0; i < displayArea.Count; i++){
            Point point = new Point(displayArea[i].X,displayArea[i].Y);
            _displayArea.Add(point);
            //_displayArea.Add(displayArea[i]);
        }

        for(int i = 0; i < displayFigure.Count; i++){
            Point point = new Point(displayFigure[i].X,displayFigure[i].Y);
            _displayFigure.Add(point);
            _displayFigureCopy.Add(point);

            //_displayFigure.Add(displayFigure[i]);
            //_displayFigureCopy.Add(displayFigure[i]);
        }


        minX = 10000;
        minY = -1;
        maxX = -1;
        maxY = 10000;

        //List<Line> lineList = new List<Line>();
        lineList.Clear();

        // Определение минимальный и максимальный X Y
        foreach(Point p in displayArea){
            if(p.X > maxX){
                maxX = p.X;
            }

            if(p.Y > minY){
                minY = p.Y;
            }

            if(p.X < minX){
                minX = p.X;
            }

            if(p.Y < maxY){
                maxY = p.Y;
            }
        }

        // Получение байт кодов
        //List<ByteCode> byteCodes = new List<ByteCode>();
        byteCodes.Clear();
        for(int i = 0; i < displayFigure.Count; i++){

            bool b0 = true, b1 = true, b2 = true, b3 = true;
            if(displayFigure[i].X >= minX){
                b0 = false;
            }

            if(displayFigure[i].X <= maxX){
                b1 = false;
            }

            if(displayFigure[i].Y <= minY){
                b2 = false; 
            }

            if(displayFigure[i].Y >= maxY){
                b3 = false;
            }


            byteCodes.Add(new ByteCode(b0,b1,b2,b3));
            byteCodesCopy.Add(new ByteCode(b0,b1,b2,b3));

            /*Console.WriteLine(displayFigure[i].Y);
            Console.WriteLine(b0+" "+b1+" "+b2+" "+b3);*/
        }

        // Определение положений отрезков

        CheckPosition(displayFigure.Count-1,0);

        for(int i = 0; i < displayFigure.Count-1; i++){

            CheckPosition(i, i+1);
        }


        return lineList;

    }

    private static void CheckPosition(int firstIndex, int secondIndex){

        bool b0 = byteCodes[firstIndex].b0;
        bool b1 = byteCodes[firstIndex].b1;
        bool b2 = byteCodes[firstIndex].b2;
        bool b3 = byteCodes[firstIndex].b3;

        bool a0 = byteCodes[secondIndex].b0;
        bool a1 = byteCodes[secondIndex].b0;
        bool a2 = byteCodes[secondIndex].b0;
        bool a3 = byteCodes[secondIndex].b0;

        if(!b0 && !b1 && !b2 && !b3 && !a0 && !a1 && !a2 && !a3){
            lineList.Add(new Line(_displayFigure[firstIndex], _displayFigure[secondIndex], Color.Blue));
            return;
        }
        else if((b0 && a0) || (b1 && a1) || (b2 && a2) || (b3 && a3)){

            return;
        }
        else{

            float k = (_displayFigure[firstIndex].Y - _displayFigure[secondIndex].Y)/(_displayFigure[firstIndex].X - _displayFigure[secondIndex].X);
            float b = _displayFigure[firstIndex].Y - _displayFigure[firstIndex].X*k;

            // Определение точек области, к которым будут смещаться координаты
            int closestPointFirst = -1;
            int closestPointSecond = -1;
            float minDistance = 10000;

            for(int i = 0; i < _displayArea.Count; i++){

                float distance = Vector2.Distance(new Vector2(_displayFigure[firstIndex].X, _displayFigure[firstIndex].Y), new Vector2(_displayArea[i].X, _displayArea[i].Y));

                if(distance <= minDistance){
                    minDistance = distance;
                    closestPointFirst = i;
                }
            }

            minDistance = 10000;
            for(int i = 0; i < _displayArea.Count; i++){

                float distance = Vector2.Distance(new Vector2(_displayFigure[secondIndex].X, _displayFigure[secondIndex].Y), new Vector2(_displayArea[i].X, _displayArea[i].Y));

                if(distance <= minDistance){
                    minDistance = distance;
                    closestPointSecond = i;
                }
            }

            // Смещение координат



            // Вычисление расстояния между точками до смещения
            float oldDistance = Vector2.Distance(new Vector2(_displayFigure[firstIndex].X, _displayFigure[firstIndex].Y), new Vector2(_displayFigure[secondIndex].X, _displayFigure[secondIndex].Y));

            // Смещение 1 точки 
            if(b0 || b1 || b2 || b3){

                if(Vector2.Distance(new Vector2(_displayArea[closestPointFirst].X, k*_displayArea[closestPointFirst].X + b),new Vector2(_displayFigure[secondIndex].X, _displayFigure[secondIndex].Y)) < oldDistance){

                    _displayFigureCopy[firstIndex] = new Point(_displayArea[closestPointFirst].X, k*_displayArea[closestPointFirst].X + b);
                }
                else{
                
                    if(k == 0){
                        _displayFigureCopy[firstIndex] = new Point(_displayArea[closestPointFirst].X, _displayArea[closestPointFirst].Y);
                    }
                    else{
                        _displayFigureCopy[firstIndex] = new Point((_displayArea[closestPointFirst].Y - b)/k, _displayArea[closestPointFirst].Y);
                    }
                }

                // Перевычисление байт кода для новой 1 точки
                b0 = true;
                b1 = true;
                b2 = true;
                b3 = true;
                
                if(_displayFigureCopy[firstIndex].X >= minX){
                    b0 = false;
                }
                if(_displayFigureCopy[firstIndex].X <= maxX){
                    b1 = false;
                }

                if(_displayFigureCopy[firstIndex].Y <= minY){
                    b2 = false; 
                }

                if(_displayFigureCopy[firstIndex].Y >= maxY){
                    b3 = false;
                }

                byteCodesCopy[firstIndex] = new ByteCode(b0,b1,b2,b3);
            }


            // Смещение 2 точки
            if(a0 || a1 || a2 || a3){

                if(Vector2.Distance(new Vector2(_displayArea[closestPointSecond].X, k*_displayArea[closestPointSecond].X + b),new Vector2(_displayFigure[firstIndex].X, _displayFigure[firstIndex].Y)) < oldDistance){

                    _displayFigureCopy[secondIndex] = new Point(_displayArea[closestPointSecond].X, k*_displayArea[closestPointSecond].X + b);
                }
                else{
                
                    if(k == 0){
                        _displayFigureCopy[secondIndex] = new Point(_displayArea[closestPointSecond].X, _displayArea[closestPointSecond].Y);
                    }
                    else{
                        _displayFigureCopy[secondIndex] = new Point((_displayArea[closestPointSecond].Y - b)/k, _displayArea[closestPointSecond].Y);
                    }
                }

                // Перевычисление байт кода для новой 2 точки
                b0 = true;
                b1 = true;
                b2 = true;
                b3 = true;
                
                if(_displayFigureCopy[secondIndex].X >= minX){
                    b0 = false;
                }
                if(_displayFigureCopy[secondIndex].X <= maxX){
                    b1 = false;
                }

                if(_displayFigureCopy[secondIndex].Y <= minY){
                    b2 = false; 
                }

                if(_displayFigureCopy[secondIndex].Y >= maxY){
                    b3 = false;
                }

                byteCodesCopy[secondIndex] = new ByteCode(b0,b1,b2,b3);
            }



            SubCheckPosition(firstIndex, secondIndex);
            return;

        }
    }

    // Необходимое зло, возникшее от того, что приходится менять координаты точек и их байт коды, что делает невозможным их использование для последующих пар точек
    // Данный метод полностью аналогичен предыдущему за исключением того, что работает только с измененными копиями точек фигуры их байт кодами
    private static void SubCheckPosition(int firstIndex, int secondIndex){

        bool b0 = byteCodesCopy[firstIndex].b0;
        bool b1 = byteCodesCopy[firstIndex].b1;
        bool b2 = byteCodesCopy[firstIndex].b2;
        bool b3 = byteCodesCopy[firstIndex].b3;

        bool a0 = byteCodesCopy[secondIndex].b0;
        bool a1 = byteCodesCopy[secondIndex].b0;
        bool a2 = byteCodesCopy[secondIndex].b0;
        bool a3 = byteCodesCopy[secondIndex].b0;

        if(!b0 && !b1 && !b2 && !b3 && !a0 && !a1 && !a2 && !a3){
            lineList.Add(new Line(_displayFigureCopy[firstIndex], _displayFigureCopy[secondIndex], Color.Blue));
            return;
        }
        else if((b0 && a0) || (b1 && a1) || (b2 && a2) || (b3 && a3)){

            return;
        }
        else{

            float k = (_displayFigureCopy[firstIndex].Y - _displayFigureCopy[secondIndex].Y)/(_displayFigureCopy[firstIndex].X - _displayFigureCopy[secondIndex].X);
            float b = _displayFigureCopy[firstIndex].Y - _displayFigureCopy[firstIndex].X*k;

            // Определение точек области, к которым будут смещаться координаты
            int closestPointFirst = -1;
            int closestPointSecond = -1;
            float minDistance = 10000;

            for(int i = 0; i < _displayArea.Count; i++){

                float distance = Vector2.Distance(new Vector2(_displayFigureCopy[firstIndex].X, _displayFigureCopy[firstIndex].Y), new Vector2(_displayArea[i].X, _displayArea[i].Y));

                if(distance <= minDistance){
                    minDistance = distance;
                    closestPointFirst = i;
                }
            }

            minDistance = 10000;
            for(int i = 0; i < _displayArea.Count; i++){

                float distance = Vector2.Distance(new Vector2(_displayFigureCopy[secondIndex].X, _displayFigureCopy[secondIndex].Y), new Vector2(_displayArea[i].X, _displayArea[i].Y));

                if(distance <= minDistance){
                    minDistance = distance;
                    closestPointSecond = i;
                }
            }

            // Смещение координат


            // Вычисление расстояния между точками до смещения
            float oldDistance = Vector2.Distance(new Vector2(_displayFigureCopy[firstIndex].X, _displayFigureCopy[firstIndex].Y), new Vector2(_displayFigureCopy[secondIndex].X, _displayFigureCopy[secondIndex].Y));

            // Смещение 1 точки
            if(b0 || b1 || b2 || b3){
                if (Vector2.Distance(new Vector2(_displayArea[closestPointFirst].X, k * _displayArea[closestPointFirst].X + b), new Vector2(_displayFigureCopy[secondIndex].X, _displayFigureCopy[secondIndex].Y)) < oldDistance)
                {

                    _displayFigureCopy[firstIndex] = new Point(_displayArea[closestPointFirst].X, k * _displayArea[closestPointFirst].X + b);
                }
                else
                {

                    if (k == 0)
                    {
                        _displayFigureCopy[firstIndex] = new Point(_displayArea[closestPointFirst].X, _displayArea[closestPointFirst].Y);
                    }
                    else
                    {
                        _displayFigureCopy[firstIndex] = new Point((_displayArea[closestPointFirst].Y - b) / k, _displayArea[closestPointFirst].Y);
                    }
                }

                // Перевычисление байт кода для новой 1 точки
                b0 = true;
                b1 = true;
                b2 = true;
                b3 = true;

                if (_displayFigureCopy[firstIndex].X >= minX)
                {
                    b0 = false;
                }
                if (_displayFigureCopy[firstIndex].X <= maxX)
                {
                    b1 = false;
                }

                if (_displayFigureCopy[firstIndex].Y <= minY)
                {
                    b2 = false;
                }

                if (_displayFigureCopy[firstIndex].Y >= maxY)
                {
                    b3 = false;
                }

                byteCodesCopy[firstIndex] = new ByteCode(b0, b1, b2, b3);
            } 

            

            // Смещение 2 точки
            if(a0 || a1 || a2 || a3){
                if (Vector2.Distance(new Vector2(_displayArea[closestPointSecond].X, k * _displayArea[closestPointSecond].X + b), new Vector2(_displayFigureCopy[firstIndex].X, _displayFigureCopy[firstIndex].Y)) < oldDistance)
                {

                    _displayFigureCopy[secondIndex] = new Point(_displayArea[closestPointSecond].X, k * _displayArea[closestPointSecond].X + b);
                }
                else
                {

                    if (k == 0)
                    {
                        _displayFigureCopy[secondIndex] = new Point(_displayArea[closestPointSecond].X, _displayArea[closestPointSecond].Y);
                    }
                    else
                    {
                        _displayFigureCopy[secondIndex] = new Point((_displayArea[closestPointSecond].Y - b) / k, _displayArea[closestPointSecond].Y);
                    }
                }

                // Перевычисление байт кода для новой 2 точки
                b0 = true;
                b1 = true;
                b2 = true;
                b3 = true;

                if (_displayFigureCopy[secondIndex].X >= minX)
                {
                    b0 = false;
                }
                if (_displayFigureCopy[secondIndex].X <= maxX)
                {
                    b1 = false;
                }

                if (_displayFigureCopy[secondIndex].Y <= minY)
                {
                    b2 = false;
                }

                if (_displayFigureCopy[secondIndex].Y >= maxY)
                {
                    b3 = false;
                }

                byteCodesCopy[secondIndex] = new ByteCode(b0, b1, b2, b3);

            }

            SubCheckPosition(firstIndex, secondIndex);
            return;
        }
    }
}



public class ByteCode{

    public bool b0,b1,b2,b3;

    public ByteCode(bool b0, bool b1, bool b2, bool b3){
        this.b0 = b0;
        this.b1 = b1;
        this.b2 = b2;
        this.b3 = b3;
    }
}