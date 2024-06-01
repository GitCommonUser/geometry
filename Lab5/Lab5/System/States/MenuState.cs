using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lab5
{
    public class MenuState : State
    {

        private Button Lab5, Lab6,Lab7, quitButton;
        private Button.ButtonAction Lab5Action, Lab6Action, Lab7Action, quitAction;

        private void Quit(){
            _game.Exit();
        }

        private void GoToLab5(){
            _game.ChangeState(new Lab5State(_game, _graphicsDevice, _content));
        }

        private void GoToLab6(){
            _game.ChangeState(new Lab6State(_game, _graphicsDevice, _content));
            //_game.ChangeState(new LoadGameState(_game, _graphicsDevice, _content));
        }

        private void GoToLab7(){
            Console.WriteLine("Lab7");
            //_game.ChangeState(new SettingsState(_game, _graphicsDevice, _content));
        }
        public MenuState(Main game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {


            quitAction = Quit;
            Lab5Action = GoToLab5;
            Lab6Action = GoToLab6;
            Lab7Action = GoToLab7;

            int startX = 100, startY = 800, buttonW = 500, buttonH = 120, buttonSpace = 50;

            Lab5 = new Button(new Rectangle(startX,startY,buttonW,buttonH), "Lab 5 - Convex hull",Lab5Action);
            Lab6 = new Button(new Rectangle(startX,startY + 1*buttonH + 1*buttonSpace,buttonW,buttonH), "Lab 6",Lab6Action);
            Lab7 = new Button(new Rectangle(startX,startY + 2*buttonH + 2*buttonSpace,buttonW,buttonH), "Lab 7",Lab7Action);
            quitButton = new Button(new Rectangle(startX,startY + 3*buttonH + 3*buttonSpace,buttonW,buttonH), "Quit",quitAction);
            
        }



        public override void Update(GameTime gameTime)
        {
            Lab5.Update(/*gameTime*/);
            Lab6.Update();
            Lab7.Update();
            quitButton.Update();

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, null,null,null,null,null, Main.resolutionScale);

            Lab5.Draw(spriteBatch);
            Lab6.Draw(spriteBatch);
            Lab7.Draw(spriteBatch);
            quitButton.Draw(spriteBatch);

            spriteBatch.End();

        }

    }
}

