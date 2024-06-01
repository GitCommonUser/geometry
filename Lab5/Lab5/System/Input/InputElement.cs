using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Lab5{

    public abstract class InputElement{

        protected static bool isLMBpressed = false;
        protected static bool isRMBpressed = false;


        #region Mouse Methods
        protected virtual bool IsLMBpressed(){
            MouseState mouseState = Main.mouseState;
            if(mouseState.LeftButton == ButtonState.Pressed){
                return true;
            }

            return false;
        }

        protected virtual bool IsLMBreleased(){
            MouseState mouseState = Main.mouseState;
            if(mouseState.LeftButton == ButtonState.Released){
                return true;
            }

            return false;
        }

        public virtual bool IsLMBclick(){

            if(IsLMBpressed()){
                isLMBpressed = true;
            }

            if(isLMBpressed && IsLMBreleased()){
                isLMBpressed = false;
                return true;
            }

            return false;
        }

        protected virtual bool IsRMBpressed(){
            MouseState mouseState = Main.mouseState;
            if(mouseState.RightButton == ButtonState.Pressed){
                return true;
            }

            return false;
        }

        protected virtual bool IsRMBreleased(){
            MouseState mouseState = Main.mouseState;
            if(mouseState.RightButton == ButtonState.Released){
                return true;
            }

            return false;
        }

        protected virtual bool IsRMBclick(){

            if(IsRMBpressed()){
                isRMBpressed = true;
            }

            if(isRMBpressed && IsRMBreleased()){
                isRMBpressed = false;
                return true;
            }

            return false;
        }

        protected virtual bool IsMouseInRectangle(Rectangle rectangle){
            MouseState mouseState = Main.mouseState;
            Vector2 realMousePos = Vector2.Transform(new Vector2(mouseState.Position.X, mouseState.Position.Y), Matrix.Invert(Main.resolutionScale));
            if(rectangle.Contains(realMousePos)){
                return true;
            }

            return false;
        }

        protected virtual bool IsRMBrectangle(Rectangle rectangle){

            if(IsMouseInRectangle(rectangle)){
                if(IsRMBclick()){
                    return true;
                }
            }

            return false;
        }

        protected virtual bool IsLMBrectangle(Rectangle rectangle){

            if(IsMouseInRectangle(rectangle)){
                if(IsLMBclick()){
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}

/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WM{

    public abstract class InputElement{

        protected static bool isLMBpressed = false;
        protected static bool isRMBpressed = false;


        #region Mouse Methods
        protected virtual bool IsLMBpressed(){
            MouseState mouseState = Mouse.GetState();
            if(mouseState.LeftButton == ButtonState.Pressed){
                return true;
            }

            return false;
        }

        protected virtual bool IsLMBreleased(){
            MouseState mouseState = Mouse.GetState();
            if(mouseState.LeftButton == ButtonState.Released){
                return true;
            }

            return false;
        }

        public virtual bool IsLMBclick(){

            if(IsLMBpressed()){
                isLMBpressed = true;
            }

            if(isLMBpressed && IsLMBreleased()){
                isLMBpressed = false;
                return true;
            }

            return false;
        }

        protected virtual bool IsRMBpressed(){
            MouseState mouseState = Mouse.GetState();
            if(mouseState.RightButton == ButtonState.Pressed){
                return true;
            }

            return false;
        }

        protected virtual bool IsRMBreleased(){
            MouseState mouseState = Mouse.GetState();
            if(mouseState.RightButton == ButtonState.Released){
                return true;
            }

            return false;
        }

        protected virtual bool IsRMBclick(){

            if(IsRMBpressed()){
                isRMBpressed = true;
            }

            if(isRMBpressed && IsRMBreleased()){
                isRMBpressed = false;
                return true;
            }

            return false;
        }

        protected virtual bool IsMouseInRectangle(Rectangle rectangle){
            MouseState mouseState = Mouse.GetState();
            Vector2 realMousePos = Vector2.Transform(new Vector2(mouseState.Position.X, mouseState.Position.Y), Matrix.Invert(Game1.resolutionScale));
            if(rectangle.Contains(realMousePos)){
                return true;
            }

            return false;
        }

        protected virtual bool IsRMBrectangle(Rectangle rectangle){

            if(IsMouseInRectangle(rectangle)){
                if(IsRMBclick()){
                    return true;
                }
            }

            return false;
        }

        protected virtual bool IsLMBrectangle(Rectangle rectangle){

            if(IsMouseInRectangle(rectangle)){
                if(IsLMBclick()){
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}*/