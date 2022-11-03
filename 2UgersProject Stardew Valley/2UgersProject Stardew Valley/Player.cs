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
            scale = 5f;//1.875f for nomral scale
        }

        public override void LoadContent(ContentManager content)
        {
            charaset = new Texture2D[7];
            threshold = 40;//miliseconds
            #region Idle
            charaset[0] = content.Load<Texture2D>("Animation/IdleSpriteSheet");
            position1 = new Vector2(10, 10);
            idleTimer = 0;
            #endregion
            #region WalkRight
            charaset[1] = content.Load<Texture2D>("Animation/WalkAnim");
            position1 = new Vector2(10, 10);
            walkThreshold = 150;//miliseconds
            walkTimer = 0;
            #endregion
            #region WalkLeft
            charaset[2] = content.Load<Texture2D>("Animation/WalkAnim");
            position1 = new Vector2(10, 10);
            walkThreshold = 150;//miliseconds
            walkTimer = 0;
            #endregion
            #region WalkBack
            charaset[3] = content.Load<Texture2D>("Animation/WalkBackAnim");
            position1 = new Vector2(10, 10);
            forBackThreshold = 150;//miliseconds
            walkTimer = 0;
            #endregion
            #region WalkForward
            charaset[4] = content.Load<Texture2D>("Animation/WalkForwardAnim");
            position1 = new Vector2(10, 10);
            forBackThreshold = 150;//miliseconds
            walkTimer = 0;
            #endregion

        }
        public override void Update(GameTime gameTime)
        {
            sourceRectangles = new Rectangle(x1, 0, 32, 64);

            Move(gameTime);
            //Animate(gameTime);
            HandleInput(gameTime);

        }
        private void HandleInput(GameTime gameTime)
        {
            velocity = Vector2.Zero;

            KeyboardState keySate = Keyboard.GetState();
            walkTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
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
                charSpriteIndex = 1;
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
        private void Eat()
        {

        }
        public override void OnCollision(GameObjects other)
        {

        }
    }
}
