using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace Lab5{

    public class Button : InputElement{

        private Rectangle buttonRectangle;
        private Texture2D[] texture;
        private string text;
        private bool isOn;
        private float depth;

        private Color fontColorOn;
        private Color fontColorOff;

        private SpriteFont textFont;
        private Vector2 textPosition = new Vector2();

        public delegate void ButtonAction();

        private ButtonAction buttonAction;

        private bool _isEnable;
        private Color _disableFontColor, _buttonColorOn, _buttonColorOff;

        public Button(Rectangle buttonRectangle, string text, ButtonAction buttonAction ){

            this.buttonRectangle = buttonRectangle;
            this.text = text;
            this.buttonAction = buttonAction;

            texture = ContentLoad.plate;

            depth = 0.5f;

            fontColorOff = new Color(215,212,187);
            fontColorOn = new Color(249,247,229);


    
            _buttonColorOn = new Color(120,120,120);
            _buttonColorOff = new Color(100,100,100);
            

            textFont = ContentLoad.mainFont;

            if(!string.IsNullOrEmpty(text)){
                textPosition.X = buttonRectangle.X + buttonRectangle.Width/2 - textFont.MeasureString(this.text).X/2;
                textPosition.Y = buttonRectangle.Y + buttonRectangle.Height/2 - textFont.MeasureString(this.text).Y/2;
                
            }

            _isEnable = true;
            _disableFontColor = Color.Black * 0.4f;
        }

        public Button(Rectangle buttonRectangle, string text, ButtonAction buttonAction, Color fontColor ){

            this.buttonRectangle = buttonRectangle;
            this.text = text;
            this.buttonAction = buttonAction;

            texture = ContentLoad.plate;

            depth = 0.5f;

            fontColorOff = fontColor;
            fontColorOn = fontColor;


    
            _buttonColorOn = new Color(120,120,120);
            _buttonColorOff = new Color(100,100,100);
            

            textFont = ContentLoad.mainFont;

            if(!string.IsNullOrEmpty(text)){
                textPosition.X = buttonRectangle.X + buttonRectangle.Width/2 - textFont.MeasureString(this.text).X/2;
                textPosition.Y = buttonRectangle.Y + buttonRectangle.Height/2 - textFont.MeasureString(this.text).Y/2;
                
            }

            _isEnable = true;
            _disableFontColor = Color.Black * 0.4f;
        }

        public void Update(){

            /*if(buttonRectangle.Contains(Input.mousePosition)){
                isOn = true;
            }*/
            if(IsMouseInRectangle(buttonRectangle) && _isEnable){
                isOn = true;
            }
            else{
                isOn = false;
            }

            //Добавить каждой кнопке делегат ее действия при нажатии
            if(IsLMBrectangle(buttonRectangle) && _isEnable){
                buttonAction?.Invoke();
            }


        }

        public void Draw(SpriteBatch spriteBatch){

            if(isOn && _isEnable){
                SliceDraw(spriteBatch, texture, buttonRectangle, _buttonColorOn, depth);
                if(!string.IsNullOrEmpty(text)){
                    spriteBatch.DrawString(textFont, text, textPosition,fontColorOn,0f, default, 1f, SpriteEffects.None, depth + 0.0001f);
                }
            }
            else if(!isOn && _isEnable){
                SliceDraw(spriteBatch, texture, buttonRectangle, _buttonColorOff, depth);
                if(!string.IsNullOrEmpty(text)){
                    spriteBatch.DrawString(textFont, text, textPosition,fontColorOff,0f, default, 1f, SpriteEffects.None, depth + 0.0001f);
                }
            }
            else if(!_isEnable){
                SliceDraw(spriteBatch, texture, buttonRectangle, Color.Lerp(_buttonColorOff, Color.Black,0.15f), depth);
                if(!string.IsNullOrEmpty(text)){
                    spriteBatch.DrawString(textFont, text, textPosition,_disableFontColor,0f, default, 1f, SpriteEffects.None, depth + 0.0001f);
                }
            }
        }

        public void Enable(){
            _isEnable = true;
        }

        public void Disable(){
            _isEnable = false;
        }

        public void ChangeDisableFontColor(Color color){
            _disableFontColor = color;
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