using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lab5
{
    public class Lab5State : State
    {

        private Button Grackhem, Jarvis, backButton;
        private Button.ButtonAction GrackhemAction, JarvisAction, backAction;
        private TextBox numCountInput;

        private void Back(){
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        private void GrackhemStart(){
            int count = Int32.Parse(numCountInput.GetData());
            grahamPointList = Point.GeneratePointList(count, new Rectangle(100 + 20,100 + 20,(int)Main.screenCenter.X-150 - 50, 900 - 50));
            grahamConvex = GrahamAlgorithm.Execute(grahamPointList);
            //_game.ChangeState(new NewGameState(_game, _graphicsDevice, _content));
        }

        private void JarvisStart(){

            int count = Int32.Parse(numCountInput.GetData());
            jarvisPointList = Point.GeneratePointList(count, new Rectangle((int)Main.screenCenter.X+50 + 20,100 + 20,(int)Main.screenCenter.X-150 - 50, 900 - 50));
            jarvisConvex = JarvisAlgorithm.Execute(jarvisPointList);
            //_game.ChangeState(new LoadGameState(_game, _graphicsDevice, _content));
        }

        private List<Point> grahamPointList = new List<Point>();
        private List<Line> grahamConvex = new List<Line>();
        private List<Point> jarvisPointList = new List<Point>();
        private List<Line> jarvisConvex = new List<Line>();


        //TEST

        public Lab5State(Main game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {


            backAction = Back;
            GrackhemAction = GrackhemStart;
            JarvisAction = JarvisStart;

            int startX = 100, startY = 800, buttonW = 500, buttonH = 120, buttonSpace = 50;
            backButton = new Button(new Rectangle(startX,startY + 3*buttonH + 3*buttonSpace,buttonW,buttonH), "Back to Menu",backAction);

            numCountInput = new TextBox(ContentLoad.plate, new Rectangle(startX + 1200,startY + 3*buttonH + 3*buttonSpace,200, 80), ContentLoad.mainFont, Color.Black, new Vector2(20,20),5,0.5f);
            
            Grackhem = new Button(new Rectangle((int)Main.screenCenter.X/2 - (buttonW/2),startY + 3*buttonH + 3*buttonSpace - 200,buttonW,buttonH), "Graham",GrackhemAction);
            Jarvis = new Button(new Rectangle((int)(Main.screenCenter.X + Main.screenCenter.X/2 - buttonW/2),startY + 3*buttonH + 3*buttonSpace - 200,buttonW,buttonH), "Jarvis",JarvisAction);

            Grackhem.Disable();
            Jarvis.Disable();

        }



        public override void Update(GameTime gameTime)
        {
            CheckEnable();

            Grackhem.Update();
            Jarvis.Update();
            backButton.Update();

            numCountInput.Update();

        }

        private void CheckEnable(){

            if(!String.IsNullOrEmpty(numCountInput.GetData())){
                Grackhem.Enable();
                Jarvis.Enable();
            }
            else{
                Grackhem.Disable();
                Jarvis.Disable();
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null,null,null,null,null, Main.resolutionScale);

            Grackhem.Draw(spriteBatch);
            Jarvis.Draw(spriteBatch);
            backButton.Draw(spriteBatch);

            numCountInput.Draw(spriteBatch);

            SliceDraw(spriteBatch, ContentLoad.plate, new Rectangle(100,100,(int)Main.screenCenter.X-150, 900), Color.White, 0.5f);
            SliceDraw(spriteBatch, ContentLoad.plate, new Rectangle((int)Main.screenCenter.X+50,100,(int)Main.screenCenter.X-150, 900), Color.White, 0.5f);

            if(grahamPointList.Count > 0){

                foreach(Point point in grahamPointList){
                    point.Draw(spriteBatch);
                }
            }

            if(jarvisPointList.Count > 0){

                foreach(Point point in jarvisPointList){
                    point.Draw(spriteBatch);
                }
            }

            if(grahamConvex.Count > 0){

                foreach(Line l in grahamConvex){
                    l.Draw(spriteBatch);
                }
            }

            if(jarvisConvex.Count > 0){
                foreach(Line l in jarvisConvex){
                    l.Draw(spriteBatch);
                }
            }

            

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

