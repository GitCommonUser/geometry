using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lab5;

public class Main : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;

    private const int TargetWidth = 2560;
    private const int TargetHeight = 1440;

    private State currentState, nextState;

    public void ChangeState(State state){
        nextState = state;
    }

    public static Vector2 screenCenter;
    public static Matrix resolutionScale;

    public static string userCharacterInput;
    public static Keys userControlInput;

    //TEST ZONE//

    public static MouseState mouseState;
    public Main()
    {

        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {

        graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        //graphics.PreferredBackBufferWidth = 2560;
        //graphics.PreferredBackBufferHeight = 1440;

        float scaleX = (float)graphics.PreferredBackBufferWidth / (float)TargetWidth;
        float scaleY = (float)graphics.PreferredBackBufferHeight / (float)TargetHeight;

        graphics.IsFullScreen = true; 
        graphics.HardwareModeSwitch = false;
        graphics.ApplyChanges();

        resolutionScale = Matrix.CreateScale(new Vector3(scaleX,scaleY, 1));
        //screenCenter = Vector2.Transform(new Vector2(TargetWidth/2, TargetHeight/2), Matrix.Invert(resolutionScale));
        screenCenter = Vector2.Transform(new Vector2(graphics.PreferredBackBufferWidth/2, graphics.PreferredBackBufferHeight/2), Matrix.Invert(resolutionScale));

        Window.TextInput += TextInputHandler;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        Globals.SpriteBatch = spriteBatch;

        ContentLoad.Load(Content);


        currentState = new MenuState(this, graphics.GraphicsDevice, Content);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        mouseState = Mouse.GetState();
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        //Input.Update();
        //Console.WriteLine(userControlInput);
        //userInput = String.Empty;

        Globals.Update(gameTime);
        
        if(nextState != null){
            currentState = nextState;
            nextState = null;
        }

        currentState.Update(gameTime);
        //Console.WriteLine(userControlInput);
        // TODO: Add your update logic here
        //rec.X += 5;
        //rec.Y += 5;
    
        userCharacterInput = String.Empty; // Обязательно должен находиться тут, иначе инпут будет очищаться до того, как его возможно будет считать
        userControlInput = Keys.None;

        base.Update(gameTime);

    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.LightSteelBlue);

        currentState.Draw(gameTime, spriteBatch);

        /*spriteBatch.Begin(SpriteSortMode.Immediate, null,null,null,null,null, resolutionScale);
        spriteBatch.DrawString(ContentLoad.headerFont, "ABCDefghi HELLO Beginaaaaajkdhfkjshdkfjhsdkjfsdjkfgdsmjflksjdhfkljhdflkjshdfkjlsdhfkjhdfg;lkdflkjghsklhfdgkjshdfkjlghsdlkjghslkjdhglksjdhfgkjlshdgkljsdhfgkjsdfghlookaa", new Vector2(0,1400),Color.White);

        spriteBatch.Draw(ContentLoad.charNoScale, new Vector2(400,200), null, Color.White, 0f, default, 5f, SpriteEffects.None, 0.5f);
        spriteBatch.Draw(ContentLoad.char5Scale, new Vector2(500,200), null, Color.White, 0f, default, 1f, SpriteEffects.None, 0.5f);

        but.Draw(spriteBatch);


        spriteBatch.End();*/

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }

    private void TextInputHandler(object sender, TextInputEventArgs args)
    {
        if(!char.IsControl(args.Character)){
            userCharacterInput += args.Character;
        }
        else{
            userControlInput = args.Key;
        }
    }

}

