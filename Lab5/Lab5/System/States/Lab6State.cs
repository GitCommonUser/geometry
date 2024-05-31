using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lab5
{
    public class Lab6State : State
    {

        private Button cohenSutherlandButton, sproullSutherlandButton, cyrusBeckButton, backButton, resetButton;
        private Button.ButtonAction cohenAction, sproullAction, cyrusAction, backAction, resetAction;


        private List<Point> figurePoints = new List<Point>();
        private List<Point> regionPoints = new List<Point>();
        private List<Line> figureLines = new List<Line>();
        private List<Line> regionLines = new List<Line>();

        private void Back(){
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        private void ResetAction(){

            GenerateTest();
            figureLines = GenerateLines(figurePoints, Color.Blue);

            cyrusBeckButton.Enable();
            sproullSutherlandButton.Enable();
            cohenSutherlandButton.Enable();
        }

        private void SproullStart(){
            
            cyrusBeckButton.Enable();
            sproullSutherlandButton.Disable();
            cohenSutherlandButton.Enable();

            GenerateTest();
            figureLines = GenerateLines(figurePoints, Color.Blue);

            figureLines = SproullSutherlandAlgorithm.Execute(regionPoints, figurePoints);
        }

        private void CohenStart(){

            cyrusBeckButton.Enable();
            sproullSutherlandButton.Enable();
            cohenSutherlandButton.Disable();

            GenerateTest();
            figureLines = GenerateLines(figurePoints, Color.Blue);

            figureLines = CohenSutherlandAlgorithm.Execute(regionPoints, figurePoints);

        }

        private void CyrusStart(){

            cyrusBeckButton.Disable();
            sproullSutherlandButton.Enable();
            cohenSutherlandButton.Enable();

            GenerateTest();
            figureLines = GenerateLines(figurePoints, Color.Blue);
        }



        private List<Line> GenerateLines(List<Point> points, Color color){

            List<Line> lines = new List<Line>();
            lines.Add(new Line(points[points.Count-1], points[0], color));

            for(int i = 0; i < points.Count-1; i++){
                lines.Add(new Line(points[i], points[i+1], color));
            }

            return lines;
        }

        private void GenerateTest(){

            regionPoints.Clear();
            figurePoints.Clear();

            Point figA = new Point(800, 400);
            Point figB = new Point(1400, 400);
            Point figC = new Point(1400, 900);
            Point figD = new Point(800, 900);

            regionPoints.Add(figA);
            regionPoints.Add(figB);
            regionPoints.Add(figC);
            regionPoints.Add(figD);

            Point A = new Point(1000, 420);
            Point B = new Point(840, 520);
            Point C = new Point(740, 700);

            figurePoints.Add(A);
            figurePoints.Add(B);
            figurePoints.Add(C);
        }

        public Lab6State(Main game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {


            backAction = Back;
            cohenAction = CohenStart;
            cyrusAction = CyrusStart;
            sproullAction = SproullStart;
            resetAction = ResetAction;

            int startX = 100, startY = 800, buttonW = 500, buttonH = 120, buttonSpace = 30;
            backButton = new Button(new Rectangle(startX,startY + 3*buttonH + 3*buttonSpace,buttonW,buttonH), "Back to Menu",backAction);

            cohenSutherlandButton = new Button(new Rectangle(startX,startY - 300,buttonW,buttonH), "Cohen-Sutherland",cohenAction);
            sproullSutherlandButton = new Button(new Rectangle(startX,startY + 1*buttonH + 1*buttonSpace - 300,buttonW,buttonH), "Sproull-Sutherland",sproullAction);
            cyrusBeckButton = new Button(new Rectangle(startX,startY + 2*buttonH + 2*buttonSpace - 300,buttonW,buttonH), "Cyrus-Beck",cyrusAction);
            resetButton = new Button(new Rectangle(startX,startY + 3*buttonH + 3*buttonSpace - 260,buttonW,buttonH), "Reset",resetAction, Color.LightGreen);






            GenerateTest();

            regionLines = GenerateLines(regionPoints, Color.Red);
            figureLines = GenerateLines(figurePoints, Color.Blue);


        }



        public override void Update(GameTime gameTime)
        {

            cohenSutherlandButton.Update();
            sproullSutherlandButton.Update();
            cyrusBeckButton.Update();
            resetButton.Update();
            backButton.Update();

            //numCountInput.Update();

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null,null,null,null,null, Main.resolutionScale);

            cohenSutherlandButton.Draw(spriteBatch);
            sproullSutherlandButton.Draw(spriteBatch);
            cyrusBeckButton.Draw(spriteBatch);
            resetButton.Draw(spriteBatch);

            backButton.Draw(spriteBatch);

            foreach(Line l in figureLines){
                l.Draw(spriteBatch);
            }

            foreach(Line l in regionLines){
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

