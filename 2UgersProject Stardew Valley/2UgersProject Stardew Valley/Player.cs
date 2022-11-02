using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Net.NetworkInformation;

namespace _2UgersProject_Stardew_Valley
{
    internal class Player : GameObjects
    {
        private float energy;
        private float hunger;

        public Player(Vector2 pos) : base(pos)
        {
            scale = 10f;
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = new Texture2D[19];
            for (int i = 0; i < sprite.Length; i++)
            {
                sprite[i] = content.Load<Texture2D>($"Animation/Idle/pixil-frame-{i + 1}");
            }
        }
        public override void Update(GameTime gameTime)
        {
            Move(gameTime);
            Animate(gameTime);
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
