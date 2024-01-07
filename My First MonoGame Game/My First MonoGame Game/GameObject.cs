using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace My_First_MonoGame_Game
{
    public class GameObject
    {
        //Fields
        protected Texture2D texture;
        protected Rectangle position;



        //Field Properties:

        //Texture Property
        public Texture2D Texture
        {
            get
            {
                return texture;
            }
            set
            {
                texture = value;
            }
        }

        //Position Property
        public Rectangle Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        //X Property
        public int X
        {
            get
            {
                return position.X;
            }
            set
            {
                position.X = value;
            }
        }

        //Y Property
        public int Y
        {
            get
            {
                return position.Y;
            }
            set
            {
                position.Y = value;
            }
        }

        //Width Property
        public int Width
        {
            get
            {
                return position.Width;
            }
            set
            {
                position.Width = value;
            }
        }

        //Height Property
        public int Height
        {
            get
            {
                return position.Height;
            }
            set
            {
                position.Height = value;
            }
        }



        //Constructor
        public GameObject(Texture2D img, int x, int y, int width, int height)
        {
            texture = img;
            position = new Rectangle(x, y, width, height);
        }



        //Methods:

        //Overridable Draw Method
        public virtual void Draw(SpriteBatch sb, Color tint)
        {
            sb.Draw(texture, position, tint);
        }
    }
}
