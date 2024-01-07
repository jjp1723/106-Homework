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
    class Player : GameObject
    {
        //Fields
        protected int levelScore;
        protected int totalScore;



        //Field Properties:

        //LevelScore Property
        public int LevelScore
        {
            get
            {
                return levelScore;
            }
            set
            {
                levelScore = value;
            }
        }

        //TotalScore Property
        public int TotalScore
        {
            get
            {
                return totalScore;
            }
            set
            {
                totalScore = value;
            }
        }



        //Constructor
        public Player(Texture2D img, int x, int y, int width, int height) : base(img, x, y, width, height)
        {
            levelScore = 0;
            totalScore = 0;
        }
    }
}
