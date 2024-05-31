

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lab5;

public class Line{


    public Point startPoint;
    public Point endPoint;

    private Texture2D lineTexture;
    private Color lineColor;

    private Rectangle lineRectangle;
    private float rotation;

    public Line(Point startPoint, Point endPoint, Color color){

        this.startPoint = startPoint;
        this.endPoint = endPoint;
        lineColor = color;

        lineRectangle = new Rectangle((int)startPoint.X+5, (int)startPoint.Y+5, (int)Vector2.Distance(new Vector2(startPoint.X,startPoint.Y), new Vector2(endPoint.X,endPoint.Y)),5);

        lineTexture = ContentLoad.blankSquare;

        rotation = (float)Math.Atan2(endPoint.Y-startPoint.Y, endPoint.X - startPoint.X);

    }

    public void Draw(SpriteBatch spriteBatch){

        spriteBatch.Draw(lineTexture, lineRectangle,null,lineColor, rotation, default, SpriteEffects.None, 0.59f);
    }


}
