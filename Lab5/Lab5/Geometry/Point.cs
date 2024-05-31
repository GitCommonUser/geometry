
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lab5;

public class Point{

    public float X, Y;

    private Texture2D pointTexture;
    private Color pointColor;

    private float scale;

    public string name;

    public int pointID;

    public Point(float x, float y){

        this.X = x;
        this.Y = y;

        name = string.Empty;
        pointTexture = ContentLoad.circle;
        pointColor = Color.Red;
        scale = 0.2f;

        pointID = -1;
    }

    public Point(float x, float y, int id){

        this.X = x;
        this.Y = y;

        name = string.Empty;
        pointTexture = ContentLoad.circle;
        pointColor = Color.Red;
        scale = 0.2f;

        pointID = id;
    }

    public static List<Point> GeneratePointList(int pointCount, Rectangle region){
        List<Point> points = new List<Point>();
        Random rand = new Random();

        for(int i = 0 ; i < pointCount; i++){

            double randX = rand.NextDouble() * region.Width + region.X;
            float x = (float)randX;

            double randY = rand.NextDouble() * region.Height + region.Y;
            float y = (float)randY;

            Point point = new Point(x,y,i);
            points.Add(point);
        }

        return points;
    }

    public Vector2 GetVector(){

        return new Vector2(X,Y);
    }

    public void Draw(SpriteBatch spriteBatch){

        spriteBatch.Draw(pointTexture, new Vector2(X,Y), null,pointColor, 0f, default, scale, SpriteEffects.None, 0.6f);
    }
}

public class PolarPoint{

    public float distance;
    public float angle;

    public int pointID;

    public PolarPoint(float distance, float angle){
        this.distance = distance;
        this.angle = angle;

        pointID = -1;
    }

    public PolarPoint(float distance, float angle, int id){
        this.distance = distance;
        this.angle = angle;

        pointID = id;
    }
}