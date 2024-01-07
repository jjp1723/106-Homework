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
    class Collectible : GameObject
    {
        //Fields
        protected bool active;



        //Fields Properties:

        //Active Property
        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }



        //Constructor
        public Collectible(Texture2D img, int x, int y, int width, int height) : base(img, x, y, width, height)
        {
            active = true;
        }



        //Methods:

        //CheckCollision Method
        public bool CheckCollision(GameObject check)
        {
            return active && position.Intersects(check.Position);
        }

        //Overriding Draw Method
        public override void Draw(SpriteBatch sb, Color tint)
        {
            if(active)
            {
                base.Draw(sb, tint);
            }
        }
    }
}
