using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace My_First_MonoGame_Game
{
    //Enums for the game's Finite State
    enum States
    {
        Menu,
        Game,
        GameOver
    }





    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;



        //Fields:

        //Enum Finite State Fields
        private States state;

        //Texture Related Fields
        private Texture2D objTexture;

        //Player Related Fields
        private Player player;

        //Collectible Related Fields
        private List<Collectible> collectibles;
        private List<Collectible> powerups;
        private List<Collectible> powerdowns;
        private int collectibleCount;

        //Level Related Fields
        private int level;
        private double timer;

        //KeyboardState Related Fields
        private KeyboardState kbState;
        private KeyboardState previousKbState;

        //Screen Size Related Fields
        private int screenWidth;
        private int screenHeight;

        //Text Related Fields
        private SpriteFont menuText;
        private SpriteFont scoreText;



        //Methods:

        //NextLevel Method
        public void NextLevel()
        {
            //Incrementing the level and resetting the timer
            level++;
            timer = 10;

            //Updating the player's TotalScore and resetting the player's LevelScore
            player.TotalScore += player.LevelScore;
            player.LevelScore = 0;

            //Centering the player in the window
            player.X = screenWidth / 2 - 25;
            player.Y = screenHeight / 2 - 25;

            //Updating the CollectibleCount
            if(level == 1)
            {
                collectibleCount = 5;
            }
            else
            {
                collectibleCount += 3;
            }

            //Creating a random object to generate collectibles
            Random rng = new Random();

            //Generating new collectibles
            for(int repeat = 0; repeat < collectibleCount; repeat++)
            {
                collectibles.Add(new Collectible(objTexture, rng.Next(screenWidth - 30) + 15, rng.Next(screenHeight - 100) + 50, 15, 50));
            }

            //Generating new powerups
            for(int repeat = 0; repeat < level / 3; repeat++)
            {
                powerups.Add(new Collectible(objTexture, rng.Next(screenWidth - 30) + 15, rng.Next(screenHeight - 100) + 50, 15, 50));
            }

            //Generating new powerdowns once every 5 levels, starting at level 4
            for(int repeat = 0; repeat < level % 5 - 3; repeat++)
            {
                powerdowns.Add(new Collectible(objTexture, rng.Next(screenWidth - 30) + 15, rng.Next(screenHeight - 100) + 50, 15, 50));
            }
        }

        //ResetGame Method
        public void ResetGame()
        {
            //Setting the current level and collectible count to 0 and resetting the plaer's TotalScore and LevelScore to 0
            level = 0;
            player.TotalScore = 0;
            player.LevelScore = 0;

            //Emptying all lists
            collectibles = new List<Collectible>();
            powerups = new List<Collectible>();
            powerdowns = new List<Collectible>();

            //Calling NextLevel to set up the first level of the game
            NextLevel();
        }

        //ScreenWrap Method
        public void ScreenWrap(GameObject objToWrap)
        {
            //Checking whether the object has moved off either the left or right side of the screen
            if(objToWrap.X > screenWidth + objToWrap.Width)
            {
                //Wrapping the object to the left side if it moves off the right side
                objToWrap.X = 2 - objToWrap.Width;
            }
            else if(objToWrap.X < 1 - objToWrap.Width)
            {
                //Wrapping the object to the right side if it moved off the left side
                objToWrap.X = screenWidth + objToWrap.Width - 2;
            }

            //Checking whether the object has moved off either the top or bottom side of the screen
            if (objToWrap.Y > screenHeight + objToWrap.Height)
            {
                //Wrapping the object to the top if it moved off the bottom
                objToWrap.Y = 2 - objToWrap.Height;
            }
            else if(objToWrap.Y < 1 - objToWrap.Height)
            {
                //Wrapping the object to the bottom if it moved off the top
                objToWrap.Y = screenHeight + objToWrap.Height - 2;
            }
        }

        //SingleKeyPress Method
        public bool SinglekeyPress(Keys key)
        {
            kbState = Keyboard.GetState();
            return kbState.IsKeyDown(key) && previousKbState.IsKeyUp(key);
        }

        //MovePlayerMethod
        public void MovePlayer()
        {
            //Using 4 if statements to check if the 4 arrow keys are pressed,
            //  and moving ths PlayerSquare or PlayerCircle image accordingly
            if (kbState.IsKeyDown(Keys.Down))
            {
                player.Y += 5;
            }
            if (kbState.IsKeyDown(Keys.Up))
            {
                player.Y -= 5;
            }
            if (kbState.IsKeyDown(Keys.Right))
            {
                player.X += 5;
            }
            if (kbState.IsKeyDown(Keys.Left))
            {
                player.X -= 5;
            }
        }



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Initializing the Screen Width and Screen Height
            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

            //Initializing the Timer and current Level
            timer = 10;
            level = 0;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Loading the PlayerTexture and CollectibleTexture
            objTexture = Content.Load<Texture2D>("square");

            //Loading the menuText and scoreText
            menuText = Content.Load<SpriteFont>("menuText");
            scoreText = Content.Load<SpriteFont>("scoreText");

            //Loading the Collectibles list, Powerup list, and Powerdown list
            collectibles = new List<Collectible>();
            powerups = new List<Collectible>();
            powerdowns = new List<Collectible>();

            //Loading the Player
            player = new Player(objTexture, screenWidth / 2 - 25, screenHeight / 2 - 25, 50, 50);

            //Setting the initial state of the game
            state = States.Menu;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Saving the old keyboard state in the PreviousKbState field and getting a new one
            previousKbState = kbState;
            kbState = Keyboard.GetState();

            //While in the menu state 
            if (state == States.Menu)
            {
                //Checking for a press of the Enter key using the SingleKeyPress method, and starting the game accordingly
                if (SinglekeyPress(Keys.Enter))
                {
                    state = States.Game;
                    ResetGame();
                }
            }

            //While in the game state
            if(state == States.Game)
            {
                //Adjusting the timer
                timer -= gameTime.ElapsedGameTime.TotalSeconds;

                //Processing input to move tha player and keep them in the window with the MovePlayer and ScreenWrap methods
                MovePlayer();
                ScreenWrap(player);

                //Checking all collectibles to see if the player has hit them and updating them and the score appropriately
                foreach(Collectible collectible in collectibles)
                {
                    if(collectible.Active && collectible.CheckCollision(player))
                    {
                        //Touching the collectible deactivates it and increments the score
                        collectible.Active = false;
                        player.LevelScore++;
                    }
                }

                //Checking all powerups to see if the player has hit them and updating them and the timer appropriately
                foreach (Collectible power in powerups)
                {
                    if (power.Active && power.CheckCollision(player))
                    {
                        //Touching the powerup deactivates it and increments the timer
                        power.Active = false;
                        timer += 1;
                    }
                }

                //Checking all powerdowns to see if the player has hit them and updating them and the timer appropriately
                foreach (Collectible power in powerdowns)
                {
                    if (power.Active && power.CheckCollision(player))
                    {
                        //Touching the powerdown deactivates it and decreases the timer by 3 seconds
                        power.Active = false;
                        timer -= 3;
                    }
                }

                //Checking if the player has collected each collectible and moving them to the next level if they have
                if (player.LevelScore == collectibleCount)
                {
                    NextLevel();
                }

                //Checking if the time ran out, and going to the GameOver state accordingly
                if(timer <= 0)
                {
                    state = States.GameOver;
                }
            }

            //While in the GameOver state
            if(state == States.GameOver)
            {
                //Checking for a press of the Enter key using the SingleKeyPress method, and going to the menu accordingly
                if (SinglekeyPress(Keys.Enter))
                {
                    state = States.Menu;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //Begin the spritebatch
            spriteBatch.Begin();

            //While in the Menu state
            if(state == States.Menu)
            {
                //Displaying the name of the game and which key to press to start
                spriteBatch.DrawString(menuText, "Coin Collector", new Vector2(10, 10), Color.White);
                spriteBatch.DrawString(scoreText, "Press 'Enter' to start", new Vector2(10, 80), Color.White);
            }

            //While in the Game state
            if(state == States.Game)
            {
                //Drawing the player
                player.Draw(spriteBatch, Color.Blue);

                //Drawing every collectible
                foreach(Collectible collectible in collectibles)
                {
                    if(collectible.Active)
                    {
                        collectible.Draw(spriteBatch, Color.Gold);
                    }
                }

                //Drawing every powerup
                foreach(Collectible power in powerups)
                {
                    if(power.Active)
                    {
                        power.Draw(spriteBatch, Color.Green);
                    }
                }

                //Drawing every powerdown
                foreach (Collectible power in powerdowns)
                {
                    if (power.Active)
                    {
                        power.Draw(spriteBatch, Color.Red);
                    }
                }

                //Displaying the current level and time remaining, as well as the player's current and total score
                spriteBatch.DrawString(scoreText, "Level: " + level + "      Time Remaining: " + String.Format("{0:0.00}", timer), new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(scoreText, "Total Score: " + player.TotalScore + "       Current Score: " + player.LevelScore, new Vector2(0, 15), Color.White);
            }

            //While in the GameOver state
            if(state == States.GameOver)
            {
                //Displaying 'Game Over' and telling the player what level they reached, their total score, and what key to press to return to the menu
                spriteBatch.DrawString(menuText, "Game Over", new Vector2(10, 10), Color.White);
                spriteBatch.DrawString(scoreText, "Level Reached: " + level + "      Total Score: " + player.TotalScore, new Vector2(10, 80), Color.White);
                spriteBatch.DrawString(scoreText, "Press 'Enter' to return to the menu", new Vector2(10, 95), Color.White);
            }

            //End the spritebatch
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
