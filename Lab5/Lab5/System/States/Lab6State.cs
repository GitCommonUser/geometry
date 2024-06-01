using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lab5
{
    public class Lab6State : State
    {

        private Button cohenSutherlandButton, sproullSutherlandButton, cyrusBeckButton, backButton, resetButton, prevButton, nextButton;
        private Button.ButtonAction cohenAction, sproullAction, cyrusAction, backAction, resetAction, prevAction, nextAction;



        private List<List<Point>> figureList = new List<List<Point>>();
        private List<List<Point>> regionList = new List<List<Point>>();

        private int currentFigure = 0;

        private List<Point> figurePoints = new List<Point>();
        private List<Point> regionPoints = new List<Point>();
        private List<Line> figureLines = new List<Line>();
        private List<Line> regionLines = new List<Line>();


        private void Back(){
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        private void PrevAction(){
            if(currentFigure >= 1){
                currentFigure--;
            }
            ResetAction();
            /*regionPoints.Clear();
            figurePoints.Clear();
            for(int i = 0; i < regionList[currentFigure].Count; i++){
                Point point = new Point(regionList[currentFigure][i].X,regionList[currentFigure][i].Y);
                regionPoints.Add(point);
            }
            for(int i = 0; i < figureList[currentFigure].Count; i++){
                Point point = new Point(figureList[currentFigure][i].X,figureList[currentFigure][i].Y);
                figurePoints.Add(point);
            }
            regionLines = GenerateLines(regionPoints, Color.Red);
            figureLines = GenerateLines(figurePoints, Color.Blue);*/
        }

        private void NextAction(){
            if(currentFigure < figureList.Count-1){
                currentFigure++;
            }
            ResetAction();
            /*regionPoints.Clear();
            figurePoints.Clear();
            for(int i = 0; i < regionList[currentFigure].Count; i++){
                Point point = new Point(regionList[currentFigure][i].X,regionList[currentFigure][i].Y);
                regionPoints.Add(point);
            }
            for(int i = 0; i < figureList[currentFigure].Count; i++){
                Point point = new Point(figureList[currentFigure][i].X,figureList[currentFigure][i].Y);
                figurePoints.Add(point);
            }
            regionLines = GenerateLines(regionPoints, Color.Red);
            figureLines = GenerateLines(figurePoints, Color.Blue);*/
        }

        private void ResetAction(){

            //GenerateTest();
            regionPoints.Clear();
            figurePoints.Clear();
            for(int i = 0; i < regionList[currentFigure].Count; i++){
                //regionPoints.Add(regionList[currentFigure][i]);
                Point point = new Point(regionList[currentFigure][i].X,regionList[currentFigure][i].Y);
                regionPoints.Add(point);
            }
            for(int i = 0; i < figureList[currentFigure].Count; i++){
                //figurePoints.Add(figureList[currentFigure][i]);
                Point point = new Point(figureList[currentFigure][i].X,figureList[currentFigure][i].Y);
                figurePoints.Add(point);
            }
            //figurePoints = figureList[currentFigure];
            //regionPoints = regionList[currentFigure];
            figureLines = GenerateLines(figurePoints, Color.Blue);
            regionLines = GenerateLines(regionPoints, Color.Red);

            cyrusBeckButton.Enable();
            sproullSutherlandButton.Enable();
            cohenSutherlandButton.Enable();
        }

        private void SproullStart(){
            
            cyrusBeckButton.Enable();
            sproullSutherlandButton.Disable();
            cohenSutherlandButton.Enable();

            regionPoints.Clear();
            figurePoints.Clear();
            for(int i = 0; i < regionList[currentFigure].Count; i++){
                //regionPoints.Add(regionList[currentFigure][i]);
                Point point = new Point(regionList[currentFigure][i].X,regionList[currentFigure][i].Y);
                regionPoints.Add(point);
            }
            for(int i = 0; i < figureList[currentFigure].Count; i++){
                //figurePoints.Add(figureList[currentFigure][i]);
                Point point = new Point(figureList[currentFigure][i].X,figureList[currentFigure][i].Y);
                figurePoints.Add(point);
            }
            figureLines = GenerateLines(figurePoints, Color.Blue);
            regionLines = GenerateLines(regionPoints, Color.Red);

            figureLines = SproullSutherlandAlgorithm.Execute(regionPoints, figurePoints);

            Console.WriteLine(currentFigure);
        }

        private void CohenStart(){

            cyrusBeckButton.Enable();
            sproullSutherlandButton.Enable();
            cohenSutherlandButton.Disable();

            //GenerateTest();
            //figurePoints = figureList[currentFigure];
            //regionPoints = regionList[currentFigure];
            regionPoints.Clear();
            figurePoints.Clear();
            for(int i = 0; i < regionList[currentFigure].Count; i++){
                Point point = new Point(regionList[currentFigure][i].X,regionList[currentFigure][i].Y);
                regionPoints.Add(point);
                //regionPoints.Add(regionList[currentFigure][i]);
            }
            for(int i = 0; i < figureList[currentFigure].Count; i++){
                Point point = new Point(figureList[currentFigure][i].X,figureList[currentFigure][i].Y);
                figurePoints.Add(point);
                //figurePoints.Add(figureList[currentFigure][i]);
            }
            figureLines = GenerateLines(figurePoints, Color.Blue);
            regionLines = GenerateLines(regionPoints, Color.Red);

            figureLines = CohenSutherlandAlgorithm.Execute(regionPoints, figurePoints);

            Console.WriteLine(currentFigure);

        }

        private void CyrusStart(){

            cyrusBeckButton.Disable();
            sproullSutherlandButton.Enable();
            cohenSutherlandButton.Enable();

            //GenerateTest();
            //figurePoints = figureList[currentFigure];
            //regionPoints = regionList[currentFigure];
            regionPoints.Clear();
            figurePoints.Clear();
            for(int i = 0; i < regionList[currentFigure].Count; i++){
                Point point = new Point(regionList[currentFigure][i].X,regionList[currentFigure][i].Y);
                regionPoints.Add(point);
                //regionPoints.Add(regionList[currentFigure][i]);
            }
            for(int i = 0; i < figureList[currentFigure].Count; i++){
                Point point = new Point(figureList[currentFigure][i].X,figureList[currentFigure][i].Y);
                figurePoints.Add(point);
                //figurePoints.Add(figureList[currentFigure][i]);
            }
            figureLines = GenerateLines(figurePoints, Color.Blue);
            regionLines = GenerateLines(regionPoints, Color.Red);

            figureLines = CyrusBeck.Execute(figurePoints,regionPoints);

            Console.WriteLine(currentFigure);
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

            List<Point> regPoints1 = new List<Point>();
            List<Point> figPoints1 = new List<Point>();

            Point figA = new Point(800, 400);
            Point figB = new Point(1400, 500); 
            Point figC = new Point(700, 900); 
            Point figD = new Point(550, 650); 
            Point figE = new Point(1100,200);

            regPoints1.Add(figD);
            regPoints1.Add(figC);
            regPoints1.Add(figB);
            regPoints1.Add(figE);
            regPoints1.Add(figA);

            Point A = new Point(1000, 420);
            Point B = new Point(840, 520);
            Point C = new Point(740, 700);

            figPoints1.Add(A);
            figPoints1.Add(B);
            figPoints1.Add(C);

            figureList.Add(figPoints1);
            regionList.Add(regPoints1);


            //regionPoints.Clear();
            //figurePoints.Clear();

            List<Point> regPoints2 = new List<Point>();
            List<Point> figPoints2 = new List<Point>();

            figA = new Point(1000, 400);
            figB = new Point(1400, 400); 
            figC = new Point(1400, 900); 
            figD = new Point(1000, 900); 

            regPoints2.Add(figA);
            regPoints2.Add(figB);
            regPoints2.Add(figC);
            regPoints2.Add(figD);

            A = new Point(900, 600);
            B = new Point(1500, 600);
            C = new Point(1200, 1000);

            figPoints2.Add(A);
            figPoints2.Add(B);
            figPoints2.Add(C);

            figureList.Add(figPoints2);
            regionList.Add(regPoints2);

            List<Point> regPoints3 = new List<Point>();
            List<Point> figPoints3 = new List<Point>();

            figA = new Point(1200, 400);
            figB = new Point(1500, 400); 
            figC = new Point(1500, 700); 
            figD = new Point(1200, 700); 

            regPoints3.Add(figA);
            regPoints3.Add(figB);
            regPoints3.Add(figC);
            regPoints3.Add(figD);

            A = new Point(1100, 300);
            B = new Point(1250, 500);
            C = new Point(1400, 600);

            figPoints3.Add(A);
            figPoints3.Add(B);
            figPoints3.Add(C);

            figureList.Add(figPoints3);
            regionList.Add(regPoints3);




            regionPoints.Clear();
            figurePoints.Clear();
            for(int i = 0; i < regionList[0].Count; i++){
                regionPoints.Add(regionList[0][i]);
            }
            for(int i = 0; i < figureList[0].Count; i++){
                figurePoints.Add(figureList[0][i]);
            }
        }

        public Lab6State(Main game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {


            backAction = Back;
            cohenAction = CohenStart;
            cyrusAction = CyrusStart;
            sproullAction = SproullStart;
            resetAction = ResetAction;

            nextAction = NextAction;
            prevAction = PrevAction;

            int startX = 100, startY = 800, buttonW = 500, buttonH = 120, buttonSpace = 30;
            backButton = new Button(new Rectangle(startX,startY + 3*buttonH + 3*buttonSpace,buttonW,buttonH), "Back to Menu",backAction);

            cohenSutherlandButton = new Button(new Rectangle(startX,startY - 300,buttonW,buttonH), "Cohen-Sutherland",cohenAction);
            sproullSutherlandButton = new Button(new Rectangle(startX,startY + 1*buttonH + 1*buttonSpace - 300,buttonW,buttonH), "Sproull-Sutherland",sproullAction);
            cyrusBeckButton = new Button(new Rectangle(startX,startY + 2*buttonH + 2*buttonSpace - 300,buttonW,buttonH), "Cyrus-Beck",cyrusAction);
            resetButton = new Button(new Rectangle(startX,startY + 3*buttonH + 3*buttonSpace - 260,buttonW,buttonH), "Reset",resetAction, Color.LightGreen);

            prevButton = new Button(new Rectangle(1600-200,1300-25,100,100), "<",prevAction);
            nextButton = new Button(new Rectangle(1600-200 + 300,1300-25,100,100), ">",nextAction);






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

            prevButton.Update();
            nextButton.Update();

            if(currentFigure == 0){
                prevButton.Disable();
            }
            else{
                prevButton.Enable();
            }

            if(currentFigure == figureList.Count-1){
                nextButton.Disable();
            }
            else{
                nextButton.Enable();
            }

            //numCountInput.Update();

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null,null,null,null,null, Main.resolutionScale);

            cohenSutherlandButton.Draw(spriteBatch);
            sproullSutherlandButton.Draw(spriteBatch);
            cyrusBeckButton.Draw(spriteBatch);
            resetButton.Draw(spriteBatch);

            prevButton.Draw(spriteBatch);
            nextButton.Draw(spriteBatch);

            spriteBatch.DrawString(ContentLoad.mainFont, (currentFigure+1).ToString() , new Vector2(1600,1300), Color.Black);

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

