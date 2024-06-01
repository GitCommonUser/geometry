
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Lab5;

public class TextBox : InputElement{


    private Texture2D[] _boxTexture;
    private Texture2D _cursorTexture;
    private Rectangle _cursorRectangle;
    private Color _textColor;
    private Color _boxColor;
    private Rectangle _boxRectangle;
    private Vector2 _textOffset;
    private bool _isActive;
    private SpriteFont _font;
    private float _depth;
    private string _text;
    private int _textLength;



    private float _cursorTime, _cursorTimeLeft;
    private bool _isCursorVisible;
    private Keys userControlInput;

    private bool _isInputError;
    private float _errorTime, _errorTimeLeft;
    private Color _textFinalColor;

    public TextBox(Texture2D[] boxTexture, Rectangle boxRectangle, SpriteFont textFont, Color textColor, Vector2 textOffset, int textMaxLength, float depth){

        _boxRectangle = boxRectangle;
        _boxTexture = boxTexture;
        _font = textFont;
        _textColor = textColor;
        _textOffset = textOffset;
        _textLength = textMaxLength;
        _depth = depth;

        _boxColor = Color.White;
        _cursorTexture = ContentLoad.blankSquare;
        _cursorRectangle = new Rectangle(boxRectangle.X + (int)textOffset.X,boxRectangle.Y + (int)textOffset.Y, 5, (int)_font.MeasureString("a").Y);

        _isActive = false;
        _text = String.Empty;
        _cursorTime = 0.5f;
        _cursorTimeLeft = _cursorTime;
        _isCursorVisible = false;

        _errorTime = 0.25f;
        _errorTimeLeft = 0;
        _isInputError = false;
    }

    public void Update(){

        //Console.WriteLine(Game1.userCharacterInput);
        if(!_isActive){

            _textFinalColor = _textColor * 0.7f;

            _isCursorVisible = false;
            _cursorTimeLeft = _cursorTime;

            if(IsLMBrectangle(_boxRectangle)){

                _isActive = true;
                _isCursorVisible = true;
            }
            return;            
        }

        userControlInput = Main.userControlInput;

        /*if(_text.Length < _textLength){
            _text += Game1.userCharacterInput;
        }*/


        if((_text + Main.userCharacterInput).Length <= _textLength){

            _text += Main.userCharacterInput;

        }
        else if(!_isInputError){
            _isInputError = true;
            _errorTimeLeft = 0;
        }

        if(_isInputError){
            _errorTimeLeft += Globals.TotalSeconds;


            float lerp =  1- (_errorTimeLeft/_errorTime);
            _textFinalColor = Color.Lerp(_textColor, Color.Red, lerp);

            if(_errorTimeLeft >= _errorTime){
                _errorTimeLeft = 0;
                _isInputError = false;
            }
        }
        else{
            _textFinalColor = _textColor;
        }

        if(userControlInput == Keys.Back && _text.Length >= 1){
            _text = _text.Remove(_text.Length-1);
        }

        _cursorTimeLeft -= Globals.TotalSeconds;
        if(_cursorTimeLeft <= 0){
            _isCursorVisible = !_isCursorVisible;
            _cursorTimeLeft = _cursorTime;
        }

        if(((IsLMBclick() || IsRMBclick() ) && !IsMouseInRectangle(_boxRectangle)) || userControlInput == Keys.Enter){
            Console.WriteLine("Close");
            _isActive = false;
        }

    }

    public void Draw(SpriteBatch spriteBatch){

        /*Color textPreColor, textFinalColor;
        if(_text.Length < _textLength){
            textPreColor = _textColor;
        }
        else{
            textPreColor = Color.Red;
        }

    
        if(!_isActive){

            textFinalColor = textPreColor * 0.7f;
        }
        else{
            textFinalColor = textPreColor;
        }*/

        if(_boxTexture != null){
            SliceDraw(spriteBatch, _boxTexture, _boxRectangle, _boxColor, _depth);
        }

        if(_text != String.Empty){
            spriteBatch.DrawString(_font,_text,new Vector2(_boxRectangle.X + _textOffset.X, _boxRectangle.Y + _textOffset.Y),_textFinalColor, 0f, default, 1f, SpriteEffects.None, _depth + 0.001f);
        }

        if(_isCursorVisible){
            spriteBatch.Draw(_cursorTexture, new Rectangle(_cursorRectangle.X + (int)_font.MeasureString(_text).X, _cursorRectangle.Y, _cursorRectangle.Width, _cursorRectangle.Height), null,_textFinalColor, 0f, default, SpriteEffects.None, _depth + 0.001f);
        }

    }

    public string GetData(){
        return _text;
    }

    public void Disable(){
        _isActive = false;
    }

    public void Enable(){
        _isActive = true;
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






/*using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace WM;

public class TextBox{


    private Texture2D[] _boxTexture;
    private Texture2D _cursorTexture;
    private Rectangle _cursorRectangle;
    private Color _textColor;
    private Color _boxColor;
    private Rectangle _boxRectangle;
    private Vector2 _textOffset;
    private bool _isActive;
    private SpriteFont _font;
    private float _depth;
    private string _text;
    private int _textLength;


    private float _cursorTime, _cursorTimeLeft;
    private bool _isCursorVisible;
    private Keys userControlInput;

    public TextBox(Texture2D[] boxTexture, Rectangle boxRectangle, SpriteFont textFont, Color textColor, Vector2 textOffset, int textMaxLength, float depth){

        _boxRectangle = boxRectangle;
        _boxTexture = boxTexture;
        _font = textFont;
        _textColor = textColor;
        _textOffset = textOffset;
        _textLength = textMaxLength;
        _depth = depth;

        _boxColor = Color.White;
        _cursorTexture = ContentLoad.blankSquare;
        _cursorRectangle = new Rectangle(boxRectangle.X + (int)textOffset.X,boxRectangle.Y + (int)textOffset.Y, 5, (int)_font.MeasureString("H").Y);

        _isActive = false;
        _text = String.Empty;
        _cursorTime = 0.5f;
        _cursorTimeLeft = _cursorTime;
        _isCursorVisible = false;
    }

    public void Update(){

        //Console.WriteLine(Game1.userCharacterInput);
        if(!_isActive){

            _isCursorVisible = false;
            _cursorTimeLeft = _cursorTime;

            if(Input.IsLMBrectangle(_boxRectangle)){

                _isActive = true;
                _isCursorVisible = true;
            }
            return;            
        }

        userControlInput = Game1.userControlInput;
        //Console.WriteLine("user = "+userControlInput);

        if(_text.Length < _textLength){
            _text += Game1.userCharacterInput;
        }

        if(userControlInput == Keys.Back && _text.Length >= 1){
            _text = _text.Remove(_text.Length-1);
        }

        _cursorTimeLeft -= Globals.TotalSeconds;
        if(_cursorTimeLeft <= 0){
            _isCursorVisible = !_isCursorVisible;
            _cursorTimeLeft = _cursorTime;
        }

        if(((Input.IsLMBclick() || Input.IsRMBclick() ) && !Input.IsMouseInRectangle(_boxRectangle)) || userControlInput == Keys.Enter){
            _isActive = false;
        }

    }

    public void Draw(SpriteBatch spriteBatch){

        Color textPreColor, textFinalColor;
        if(_text.Length < _textLength){
            textPreColor = _textColor;
        }
        else{
            textPreColor = Color.Red;
        }

    
        if(!_isActive){

            textFinalColor = textPreColor * 0.7f;
        }
        else{
            textFinalColor = textPreColor;
        }

        if(_boxTexture != null){
            SliceDraw(spriteBatch, _boxTexture, _boxRectangle, _boxColor, _depth);
        }

        if(_text != String.Empty){
            spriteBatch.DrawString(_font,_text,new Vector2(_boxRectangle.X + _textOffset.X, _boxRectangle.Y + _textOffset.Y),textFinalColor, 0f, default, 1f, SpriteEffects.None, _depth + 0.001f);
        }

        if(_isCursorVisible){
            spriteBatch.Draw(_cursorTexture, new Rectangle(_cursorRectangle.X + (int)_font.MeasureString(_text).X, _cursorRectangle.Y, _cursorRectangle.Width, _cursorRectangle.Height), null,textFinalColor, 0f, default, SpriteEffects.None, _depth + 0.001f);
        }

    }

    public string GetData(){
        return _text;
    }

    public void Disable(){
        _isActive = false;
    }

    public void Enable(){
        _isActive = true;
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
}*/