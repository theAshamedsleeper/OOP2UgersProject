using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2UgersProject_Stardew_Valley
{
    internal class Player : GameObjects
    {
        private float energy;//energy bar.
        private float hunger;//food bar.
        private int x1 = 0;
        private bool canMove = true;//Makes it so you cant move during an animation
        private float foodDecrease = 2f;//Increase to slowdown food decrease
        private float idleTimer;
        private float animationIsRunningtimer;
        private bool hasEnergy = true;
        private bool canHoe;
        //Walk right/left anim
        private int walkThreshold;
        private float walkTimer;
        //Walk forward/back anim
        private int forBackThreshold;
        //Hoeing Ground
        private int Hoeingthreshold;
        private float HoeingTimer;
        private bool animationIsRunningHoe = false;
        //Watering Ground
        private int WateringThreshold;
        private float WateringTimer;
        private bool animationIsRunningWater = false;


        public Player(Vector2 pos) : base(pos)
        {
            scale = 2f;//scale of the player
        }
        public override void LoadContent(ContentManager content)
        {
            charaset = new Texture2D[9];
            #region Idle
            threshold = 40;//miliseconds for each image on spritesheet
            charaset[0] = content.Load<Texture2D>("Animation/IdleSpriteSheet");
            position1[0] = new Vector2(10, 10);
            idleTimer = 0;
            #endregion
            #region WalkRight
            charaset[1] = content.Load<Texture2D>("Animation/WalkAnim");
            walkThreshold = 350;////miliseconds for each image on spritesheet
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
            #region Watering
            charaset[5] = content.Load<Texture2D>("Animation/Watering");
            WateringThreshold = 300;//miliseconds for each image on spritesheet
            WateringTimer = 0;
            #endregion
            #region Hoeing
            charaset[6] = content.Load<Texture2D>("Animation/HoeingGround");
            Hoeingthreshold = 300;//miliseconds for each image on spritesheet
            HoeingTimer = 0;
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
        /// Handles the Walking, Hoeing the ground and watering the ground.
        /// </summary>
        /// <param name="gameTime"></param>
        private void HandleInput(GameTime gameTime)
        {
            velocity = Vector2.Zero;

            KeyboardState keySate = Keyboard.GetState();
            walkTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            #region planting
            if (animationIsRunningWater == false && animationIsRunningHoe == false)
            {
                if (hasEnergy == true)
                {
                    if (keySate.IsKeyDown(Keys.F))
                    {
                        if (Terrain.Which_Terrain(position.X+32,position.Y+32) == 6)
                        {
                            if (Plant_t.Plant_Check_b(position.X+32, position.Y+32) == false)
                            {
                                Plant_t.New_Plant(position.X+32, position.Y+32, 1);
                            }
                        }
                    }
                }
            }
            #endregion
            #region watering
            if (animationIsRunningWater == false && animationIsRunningHoe == false)
            {
                if (hasEnergy == true)
                {
                    if (keySate.IsKeyDown(Keys.E))
                    {
                        x1 = 0;
                        animationIsRunningWater = true;
                        charSpriteIndex = 5;
                        
                        energy -= 5;
                        if (Plant_t.Plant_Check_b(position.X + 32, position.Y + 32))
                        {
                            Plant_t.plant_wet(position.X + 32, position.Y + 32);
                        }
                    }
                }

            }
            if (animationIsRunningWater == true && animationIsRunningHoe == false)
            {
                canMove = false;
                WateringTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (WateringTimer > WateringThreshold)
                {
                    if (x1 >= 32 * 2)
                    {
                        x1 = 0;
                        WateringTimer = 0;
                    }
                    else
                    {
                        x1 += 32;
                        WateringTimer = 0;
                    }
                }
                animationIsRunningtimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (animationIsRunningtimer > 1.5f)//the lower you set the value the faster the animation is over.
                {
                    if (Terrain.Which_Terrain(position.X+32, position.Y + 32) == 4) //if the value of the terrain is equal to 4 then change the terrain to terrain value 6
                    {
                        Terrain.Terrain_Change(position.X + 32, position.Y + 32, 6);//changes to Watered hoed ground
                    }
                    animationIsRunningWater = false;
                    animationIsRunningtimer = 0;
                    x1 = 0;
                }
            }

            #endregion
            #region Hoeing
            if (Terrain.Which_Terrain(position.X, position.Y) == 2)
            {
                canHoe = false;
            }
            else
            {
                canHoe = true;
            }
            if (canHoe == true && animationIsRunningHoe == false && animationIsRunningWater == false)
            {
                if (hasEnergy == true)
                {
                    if (keySate.IsKeyDown(Keys.Q))
                    {
                        x1 = 0;
                        animationIsRunningHoe = true;
                        energy -= 5;
                        charSpriteIndex = 6;
                    }
                }

            }
            if (animationIsRunningHoe == true)
            {
                canMove = false;
                HoeingTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (HoeingTimer > Hoeingthreshold)
                {
                    if (x1 >= 32 * 3)
                    {
                        x1 = 0;
                        HoeingTimer = 0;
                        Terrain.Terrain_Change(position.X+32, position.Y+32, 4);//Changes the tile to tile 4 from player pos
                    }
                    else
                    {
                        x1 += 32;
                        HoeingTimer = 0;
                    }
                }
                animationIsRunningtimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (animationIsRunningtimer > 1.5f)//the lower you set the value the faster the animation is over.
                {
                    animationIsRunningHoe = false;
                    animationIsRunningtimer = 0;
                    x1 = 0;
                }
            }
            #endregion
            //Runs Idle Animation
            #region Idle anim
            if (!keySate.IsKeyDown(Keys.S) && !keySate.IsKeyDown(Keys.W) && !keySate.IsKeyDown(Keys.A)
                && !keySate.IsKeyDown(Keys.D) && !keySate.IsKeyDown(Keys.E) && animationIsRunningWater == false && animationIsRunningHoe == false)
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
            if (canMove == true)
            {
                #region walk
                #region WalkForward
                if (keySate.IsKeyDown(Keys.W) && animationIsRunningWater == false && animationIsRunningHoe == false)
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
                    if (position.Y < 0)
                    {
                        position.Y = 1080;
                    }
                }
                #endregion
                #region WalkBack
                if (keySate.IsKeyDown(Keys.S) && animationIsRunningWater == false && animationIsRunningHoe == false)
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
                    if (position.Y > 1080)
                    {
                        position.Y = 0;
                    }
                }
                #endregion
                #region Walkleft
                if (keySate.IsKeyDown(Keys.A) && animationIsRunningWater == false && animationIsRunningHoe == false)
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
                    if (position.X < 0 )
                    {
                        position.X = 1920;
                    }
                }
                #endregion
                #region WalkRight
                walkTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (keySate.IsKeyDown(Keys.D) && animationIsRunningWater == false && animationIsRunningHoe == false)
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
                    if (position.X > 1920)
                    {
                        position.X = 0;
                    }
                }
                #endregion
                if (velocity != Vector2.Zero)
                {
                    velocity.Normalize();
                }
                #endregion

            }
            if (keySate.IsKeyDown(Keys.R)) //gives you food at the press of a button
            {
                hunger += 5;
                if (hunger > 64)
                {
                    hunger = 64;
                }
            }
            if (keySate.IsKeyDown(Keys.P))
            {
                //new Plants(vector pos);
            }
        }
        /// <summary>
        /// Takes care of the energybar and foodbar.
        /// The energybar follows the foodbar in which if the foodbar goes down so does the energy, 
        /// energy regens when the foodbar is higher than the energybar.
        /// When Hoeing or Watering it consumes a sertain amount of energy.
        /// </summary>
        /// <param name="gameTime"></param>
        private void HandleEnergyAndFood(GameTime gameTime)
        {
            //RecBar[0] = energyBar | RecBar[2] = foodBar
            hunger -= (float)gameTime.ElapsedGameTime.TotalSeconds / foodDecrease;
            energyRecBar[2].Height = (int)hunger;
            if (hunger <= 0)
            {
                energy -= (float)gameTime.ElapsedGameTime.TotalSeconds * 2;
                energyRecBar[0].Height = (int)energy;
            }
            energy += (float)gameTime.ElapsedGameTime.TotalSeconds;
            energyRecBar[0].Height = (int)energy;
            if (energy > 64)//so the energy bar doesnt get bigger than 64
            {
                energy = 64;//64 is the max height of the energy sprite bar
            }
            if (energy >= hunger)//makes sure the the energy bar follows the amount of food you have.
            {
                energy = hunger;
            }
            if (energy <= 0)// if you have 0 energy then you cant move.
            {
                canMove = false;
                hasEnergy = false;
                energy = 0;
            }
            else if (energy > 0)//if you have 1 or more energy then you can move.
            {
                canMove = true;
                hasEnergy = true;
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
