using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace _2UgersProject_Stardew_Valley
{
    public abstract class GameObjects
    {
        protected Texture2D[] sprite;
        protected Vector2 position;
        protected float scale;
        private float animationSpeed = 15f;
        private float animationTime;
        protected float speed = 400f;
        protected Vector2 velocity;

        #region IdleAnimation
        protected Texture2D charaset;
        protected Vector2 position1;
        protected float timer;
        protected int threshold;
        protected Rectangle sourceRectangles;
        protected byte previousAnimationIndex;
        protected byte currentAnimationIndex;
        #endregion

        public GameObjects(Vector2 pos)
        {
            position = pos;
        }
        private Texture2D currentSprite
        {
            get
            {
                return charaset;
            }
        }
        private Vector2 spriteSize
        {
            get
            {
                return new Vector2(sourceRectangles.Width * scale, sourceRectangles.Height * scale);
            }
        }
        public abstract void LoadContent(ContentManager content);
        public abstract void Update(GameTime gameTime);
        /*
        protected void Animate(GameTime gameTime)
        {
            animationTime += (float)gameTime.ElapsedGameTime.TotalSeconds * animationSpeed;

            if (animationTime > sprite.Length)
            {
                animationTime = 0;
            }
        }*/
        protected void Move(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            position += ((velocity * speed) * deltaTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //player
            Vector2 origin = new Vector2(sourceRectangles.Width / 2, sourceRectangles.Height / 2);
            spriteBatch.Draw(charaset,//what to draw
                position,//place to draw it
                sourceRectangles,//rectangle
                Color.White,//color of player
                0f, //Rotation of player
                origin,//Orgin Point
                scale,//How big is the player
                SpriteEffects.None,//effects
                0f);//Layer
            spriteBatch.Draw(charaset, position1, Color.White);
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
