using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lab5{

    static class ContentLoad{

        #region FontsField
        public static SpriteFont mainFont;

        #endregion

        #region SpritesField

        public static Texture2D blankSquare;
        public static Texture2D circle;
        public static Texture2D[] plate = new Texture2D[9];


        
        #endregion



        public static void Load(ContentManager content){

            #region Sprites

            blankSquare = content.Load<Texture2D>("Sprites/blankSquare");

            circle = content.Load<Texture2D>("Sprites/Circle");

            plate[0] = content.Load<Texture2D>("Sprites/plateP0");
            plate[1] = content.Load<Texture2D>("Sprites/plateP1");
            plate[2] = content.Load<Texture2D>("Sprites/plateP2");
            plate[3] = content.Load<Texture2D>("Sprites/plateP3");
            plate[4] = content.Load<Texture2D>("Sprites/plateP4");
            plate[5] = content.Load<Texture2D>("Sprites/plateP5");
            plate[6] = content.Load<Texture2D>("Sprites/plateP6");
            plate[7] = content.Load<Texture2D>("Sprites/plateP7");
            plate[8] = content.Load<Texture2D>("Sprites/plateP8");
            


            #endregion

            #region Fonts

            mainFont = content.Load<SpriteFont>("Fonts/mainFont");

            #endregion

        }


    }
}