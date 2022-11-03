using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2UgersProject_Stardew_Valley
{
    internal class Player : GameObjects
    {
        private float energy;
        private float hunger;
        private int x1 = 0;




        public Player(Vector2 pos) : base(pos)
        {
            scale = 2f;//scale of the player
        }

        public override void LoadContent(ContentManager content)
        {
            charaset = new Texture2D[7];
            #region Idle
            threshold = 40;//miliseconds for each image on spritesheet
            charaset[0] = content.Load<Texture2D>("Animation/IdleSpriteSheet");
            position1[0] = new Vector2(10, 10);
            idleTimer = 0;
            #endregion
            #region WalkRight
            charaset[1] = content.Load<Texture2D>("Animation/WalkAnim");
            walkThreshold = 350;//miliseconds
            walkTimer = 0;
            #endregion
            #region WalkLeft
            charaset[2] = content.Load<Texture2D>("Animation/Walk Left");
            walkThreshold = 350;//miliseconds for each image on spritesheet
            walkTimer = 0;
            #endregion
            #region WalkBack
            charaset[3] = content.Load<Texture2D>("Animation/WalkBackAnim");
            forBackThreshold = 350;//miliseconds for each image on spritesheet
            walkTimer = 0;
            #endregion
            #region WalkForward
            charaset[4] = content.Load<Texture2D>("Animation/WalkForwardAnim");
            forBackThreshold = 350;//miliseconds for each image on spritesheet
            walkTimer = 0;
            #endregion
            #region Load Food and energy bars
            barSprite = new Texture2D[5];
            barSprite[0] = content.Load<Texture2D>("Sprites/FoodEnergyBarSprite/EnergyBar");//load energybar sprite
            energyRecBar[0] = new Rectangle(0, 0, 8, 64);//size of energybar
            position1[1] = new Vector2(50, 200);//position of energy sprite
            energyRecBar[1] = new Rectangle(0, 0, 12, 68);//size of energybackgroundbar
            barSprite[1] = content.Load<Texture2D>("Sprites/FoodEnergyBarSprite/BackgroundBar");//loader backgroundbar sprite
            position1[2] = new Vector2(54, 204);//position of energybackground
            barSprite[2] = content.Load<Texture2D>("Sprites/FoodEnergyBarSprite/foodBar");//loader foodBar sprite
            position1[3] = new Vector2(80, 200);//position af foodBar
            energyRecBar[2] = new Rectangle(0, 0, 8, 64);//size of foodBar
            barSprite[3] = content.Load<Texture2D>("Sprites/FoodEnergyBarSprite/BackgroundBar");//loader backgroundbar sprite
            position1[4] = new Vector2(84, 204);//position af foodbackgroundBar 
            energyRecBar[3] = new Rectangle(0, 0, 12, 68);//size of foodbackgroundBar
            #endregion


            energy = energyRecBar[0].Height;
            hunger = energyRecBar[2].Height;

        }
        public override void Update(GameTime gameTime)
        {
            sourceRectangles = new Rectangle(x1, 0, 32, 64);

            Move(gameTime);
            HandleInput(gameTime);
            HandleEnergyAndFood(gameTime);


        }
        /// <summary>
        /// Handles the events about walking and doing stuff in the GameWorld
        /// </summary>
        /// <param name="gameTime"></param>
        private void HandleInput(GameTime gameTime)
        {
            velocity = Vector2.Zero;

            KeyboardState keySate = Keyboard.GetState();
            walkTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (keySate.IsKeyDown(Keys.E))
            {
                Terrain.Terrain_Change(position.X, position.Y, 3);
            }
            //Runs Idle Animation
            #region Idle anim
            if (!keySate.IsKeyDown(Keys.S) && !keySate.IsKeyDown(Keys.W) && !keySate.IsKeyDown(Keys.A) && !keySate.IsKeyDown(Keys.D))
            {
                idleTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                // Check if the timer has exceeded the threshold.
                charSpriteIndex = 0;
                if (idleTimer > threshold)
                {
                    if (x1 >= 32 * 66)
                    {
                        x1 = 0;
                        idleTimer = 0;
                    }
                    else
                    {
                        x1 += 32;
                        idleTimer = 0;
                    }
                }
            }
            #endregion
            #region walk
            #region WalkForward
            if (keySate.IsKeyDown(Keys.W))
            {
                charSpriteIndex = 3;
                velocity += new Vector2(0, -1);
                if (walkTimer > forBackThreshold)
                {

                    if (x1 >= 32 * 3)
                    {
                        x1 = 0;
                        walkTimer = 0;
                    }
                    else
                    {
                        x1 += 32;
                        walkTimer = 0;
                    }
                }
            }
            #endregion
            #region WalkBack
            if (keySate.IsKeyDown(Keys.S))
            {
                charSpriteIndex = 4;
                velocity += new Vector2(0, 1);
                if (walkTimer > forBackThreshold)
                {

                    if (x1 >= 32 * 3)
                    {
                        x1 = 0;
                        walkTimer = 0;
                    }
                    else
                    {
                        x1 += 32;
                        walkTimer = 0;
                    }
                }
            }
            #endregion
            #region Walkleft
            if (keySate.IsKeyDown(Keys.A))
            {
                charSpriteIndex = 2;
                velocity += new Vector2(-1, 0);
                if (walkTimer > walkThreshold)
                {
                    if (x1 >= 32 * 3)
                    {
                        x1 = 0;
                        walkTimer = 0;
                    }
                    else
                    {
                        x1 += 32;
                        walkTimer = 0;
                    }
                }
            }
            #endregion
            #region WalkRight
            walkTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (keySate.IsKeyDown(Keys.D))
            {
                charSpriteIndex = 1;
                velocity += new Vector2(1, 0);
                if (walkTimer > walkThreshold)
                {
                    if (x1 >= 32 * 3)
                    {
                        x1 = 0;
                        walkTimer = 0;
                    }
                    else
                    {
                        x1 += 32;
                        walkTimer = 0;
                    }
                }
            }
            #endregion
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }
            #endregion
            if (keySate.IsKeyDown(Keys.P))
            {
                //new Plants(vector pos);
            }
        }
        private void HandleEnergyAndFood(GameTime gameTime)
        {
            //RecBar[0] = energyBar | RecBar[2] = foodBar
            hunger -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            energyRecBar[2].Height = (int)hunger;
            if ((int)hunger <= energyRecBar[2].Height *1f)
            {
                energy -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                energyRecBar[0].Height = (int)energy;

            }
        }
        private void Eat()
        {

        }
        public override void OnCollision(GameObjects other)
        {

        }
    }
}
