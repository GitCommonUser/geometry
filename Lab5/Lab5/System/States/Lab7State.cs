using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lab5
{
    public class Lab7State : State
    {

        private Button primitiveButton, monotoneButton, backButton, resetButton, prevButton, nextButton;
        private Button.ButtonAction primitiveAction, monotoneAction, backAction, resetAction, prevAction, nextAction;



        private List<List<Point>> figureList = new List<List<Point>>();

        private int currentFigure = 1;

        private List<Point> figurePoints = new List<Point>();
        private List<Line> figureLines = new List<Line>();

        private List<Line> triangleList = new List<Line>();


        private void Back(){
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        private void PrevAction(){
            if(currentFigure >1){
                currentFigure--;
            }
            figurePoints = figureList[currentFigure-1];

            figureLines = GenerateLines(figurePoints, Color.Red);
        }

        private void NextAction(){
            if(currentFigure < figureList.Count){
                currentFigure++;
            }
            figurePoints = figureList[currentFigure-1];

            figureLines = GenerateLines(figurePoints, Color.Red);
        }

        private void ResetAction(){


            monotoneButton.Enable();
            primitiveButton.Enable();

            triangleList.Clear();
        }

        private void PrimitiveStart(){
            
            monotoneButton.Enable();
            primitiveButton.Disable();

            triangleList = PrimitiveTriangulation.Execute(figurePoints);

        }

        private void MonotoneStart(){

            monotoneButton.Disable();
            primitiveButton.Enable();


        }



        private List<Line> GenerateLines(List<Point> points, Color color){

            List<Line> lines = new List<Line>();
            lines.Add(new Line(points[points.Count-1], points[0], color));

            for(int i = 0; i < points.Count-1; i++){
                lines.Add(new Line(points[i], points[i+1], color));
            }

            return lines;
        }

        private void Generate(){

            // 1 Фигура
            List<Point> regPoints1 = new List<Point>();

            Point figA = new Point(1000, 600);
            Point figB = new Point(1600, 600);
            Point figC = new Point(1600, 1000);
            Point figD = new Point(1000, 1000);

            regPoints1.Add(figD);
            regPoints1.Add(figC);
            regPoints1.Add(figB);
            regPoints1.Add(figA);

            figureList.Add(regPoints1);

            // 2 Фигура
            List<Point> regPoints2 = new List<Point>();

            figA = new Point(1000, 600);
            figB = new Point(1600, 600);
            figC = new Point(1600, 1000);
            figD = new Point(1000, 1000);

            regPoints2.Add(figA);
            regPoints2.Add(figB);
            regPoints2.Add(figC);
            regPoints2.Add(figD);

            figureList.Add(regPoints2);

            // 3 Фигура
            List<Point> regPoints3 = new List<Point>();

            figA = new Point(1200, 400);
            figB = new Point(1500, 400);
            figC = new Point(1500, 700);
            figD = new Point(1200, 700);

            regPoints3.Add(figA);
            regPoints3.Add(figB);
            regPoints3.Add(figC);
            regPoints3.Add(figD);


            figureList.Add(regPoints3);


            // Добавление всех фигур
            figurePoints.Clear();
            for (int i = 0; i < figureList[0].Count; i++)
            {
                figurePoints.Add(figureList[0][i]);
            }
        }

        public Lab7State(Main game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {


            backAction = Back;
            monotoneAction = MonotoneStart;
            primitiveAction = PrimitiveStart;
            resetAction = ResetAction;

            nextAction = NextAction;
            prevAction = PrevAction;

            int startX = 100, startY = 800, buttonW = 500, buttonH = 120, buttonSpace = 30;
            backButton = new Button(new Rectangle(startX,startY + 3*buttonH + 3*buttonSpace,buttonW,buttonH), "Back to Menu",backAction);

            primitiveButton = new Button(new Rectangle(startX,startY + 1*buttonH + 1*buttonSpace - 300,buttonW,buttonH), "Primitive",primitiveAction);
            monotoneButton = new Button(new Rectangle(startX,startY + 2*buttonH + 2*buttonSpace - 300,buttonW,buttonH), "Monotone",monotoneAction);
            resetButton = new Button(new Rectangle(startX,startY + 3*buttonH + 3*buttonSpace - 260,buttonW,buttonH), "Reset",resetAction, Color.LightGreen);

            prevButton = new Button(new Rectangle(1600-200,1300-25,100,100), "<",prevAction);
            nextButton = new Button(new Rectangle(1600-200 + 300,1300-25,100,100), ">",nextAction);






            Generate();

            figureLines = GenerateLines(figurePoints, Color.Red);


        }


        public override void Update(GameTime gameTime)
        {
            primitiveButton.Update();
            monotoneButton.Update();
            resetButton.Update();
            backButton.Update();

            prevButton.Update();
            nextButton.Update();

            if(currentFigure <= 1){
                prevButton.Disable();
            }
            else{
                prevButton.Enable();
            }

            if(currentFigure == figureList.Count){
                nextButton.Disable();
            }
            else{
                nextButton.Enable();
            }

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null,null,null,null,null, Main.resolutionScale);

            primitiveButton.Draw(spriteBatch);
            monotoneButton.Draw(spriteBatch);
            resetButton.Draw(spriteBatch);

            prevButton.Draw(spriteBatch);
            nextButton.Draw(spriteBatch);

            spriteBatch.DrawString(ContentLoad.mainFont, currentFigure.ToString(), new Vector2(1600,1300), Color.Black);

            backButton.Draw(spriteBatch);

            foreach(Line l in figureLines){
                l.Draw(spriteBatch);
            }

            foreach (Line l in triangleList)
            {
                l.Draw(spriteBatch);
            }


            SliceDraw(spriteBatch, ContentLoad.plate, new Rectangle(650,50,(int)Main.screenCenter.X*2 - 700, 1100), Color.White, 0.5f);
            //SliceDraw(spriteBatch, ContentLoad.plate, new Rectangle((int)Main.screenCenter.X+50,100,(int)Main.screenCenter.X-150, 900), Color.White, 0.5f);


            spriteBatch.End();

        }

        private void SliceDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.Graphics.Texture2D[] textureList, Microsoft.Xna.Framework.Rectangle rectangle, Microsoft.Xna.Framework.Color color, float depth){

            if(textureList.Length == 9){

                int cornerWidth = textureList[0].Width;
                int cornerHeight = textureList[0].Height;
                int midWidth = rectangle.Width - 2*cornerWidth;
                int midHeight = rectangle.Height - 2*cornerHeight;

                spriteBatch.Draw(textureList[0], new Rectangle(rectangle.X, rectangle.Y, cornerWidth, cornerHeight),null,color,0f, default, SpriteEffects.None, depth);
                spriteBatch.Draw(textureList[1], new Rectangle(rectangle.X + cornerWidth, rectangle.Y, midWidth, cornerHeight),null,color,0f, default, SpriteEffects.None, depth);
                spriteBatch.Draw(textureList[2], new Rectangle(rectangle.X + cornerWidth + midWidth, rectangle.Y, cornerWidth, cornerHeight),null,color,0f, default, SpriteEffects.None, depth);

                spriteBatch.Draw(textureList[3], new Rectangle(rectangle.X, rectangle.Y + cornerHeight, cornerWidth, midHeight),null,color,0f, default, SpriteEffects.None, depth);
                spriteBatch.Draw(textureList[4], new Rectangle(rectangle.X + cornerWidth, rectangle.Y + cornerHeight, midWidth, midHeight),null,color,0f, default, SpriteEffects.None, depth);
                spriteBatch.Draw(textureList[5], new Rectangle(rectangle.X + cornerWidth + midWidth, rectangle.Y + cornerHeight, cornerWidth, midHeight),null,color,0f, default, SpriteEffects.None, depth);
    
                spriteBatch.Draw(textureList[6], new Rectangle(rectangle.X, rectangle.Y + cornerHeight + midHeight, cornerWidth, cornerHeight),null,color,0f, default, SpriteEffects.None, depth);
                spriteBatch.Draw(textureList[7], new Rectangle(rectangle.X + cornerWidth, rectangle.Y + cornerHeight + midHeight, midWidth, cornerHeight),null,color,0f, default, SpriteEffects.None, depth);
                spriteBatch.Draw(textureList[8], new Rectangle(rectangle.X + cornerWidth + midWidth, rectangle.Y + cornerHeight + midHeight, cornerWidth, cornerHeight),null,color,0f, default, SpriteEffects.None, depth);
            }
            else{
                Console.WriteLine("Ошибка отображения!");
            }
        }

    }
}

