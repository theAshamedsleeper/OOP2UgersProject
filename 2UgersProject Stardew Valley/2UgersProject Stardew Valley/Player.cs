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
        private float moveTimer;
        private int x1 = 0;


        public Player(Vector2 pos) : base(pos)
        {
            scale = 1.875f;
        }

        public override void LoadContent(ContentManager content)
        {
            //tester
            charaset = content.Load<Texture2D>("Animation/IdleSpriteSheet");
            position1 = new Vector2(10, 10);
            timer = 0;
            threshold = 50;//miliseconds
        }
        public override void Update(GameTime gameTime)
        {
            sourceRectangles = new Rectangle(x1, 0, 32, 64);
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            // Check if the timer has exceeded the threshold.
            if (timer > threshold)
            {
                if (x1 >= 32*66)
                {
                    x1 = 0;
                    timer = 0;
                }
                else
                {
                    x1 += 32;
                    timer = 0;
                }
            }
            Move(gameTime);
            //Animate(gameTime);
            HandleInput(gameTime);

        }
        private void HandleInput(GameTime gameTime)
        {
            velocity = Vector2.Zero;

            KeyboardState keySate = Keyboard.GetState();
            if (keySate.IsKeyDown(Keys.W))
            {
                velocity += new Vector2(0, -1);
            }
            if (keySate.IsKeyDown(Keys.S))
            {
                velocity += new Vector2(0, 1);
            }
            if (keySate.IsKeyDown(Keys.A))
            {
                velocity += new Vector2(-1, 0);
            }
            if (keySate.IsKeyDown(Keys.D))
            {
                velocity += new Vector2(1, 0);
            }
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
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
