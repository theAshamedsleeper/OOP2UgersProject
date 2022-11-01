using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _2UgersProject_Stardew_Valley
{
    public abstract class GameObjects
    {
        protected Texture2D[] sprite;
        protected Vector2 position;
        protected float scale;
        private float animationSpeed;
        private float animationTime;
        protected float speed;
        protected Vector2 velocity;

        public GameObjects(Vector2 pos)
        {

        }
        private Texture2D currentSprite
        {
            get
            {
                return sprite[(int)animationTime];
            }
        }
        private Vector2 spriteSize
        {
            get
            {
                return new Vector2(currentSprite.Width * scale, currentSprite.Height * scale);
            }
        }
        public void LoadContent(ContentManager content)
        {

        }
        public void Update(GameTime gameTime)
        {

        }
        protected void Animate(GameTime gameTime)
        {

        }
        protected void HandleInput(GameTime gameTime)
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {

        }
        public Rectangle GetCollisionBox
        {
            get
            {
                return new Rectangle
                (
                    (int)(position.X - spriteSize.X / 2),
                    (int)(position.Y - spriteSize.Y / 2),
                    (int)spriteSize.X,
                    (int)spriteSize.Y
                );
            }
        }
        public bool IsColliding(GameObjects other)
        {
            if (this == other)
            {
                return false;
            }
            return GetCollisionBox.Intersects(other.GetCollisionBox);
        }
        public virtual void OnCollision(GameObjects other)
        {

        }
    }



}
