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
        protected float speed = 200f;
        protected Vector2 velocity;

        #region Character attributes
        protected byte charSpriteIndex=0;
        protected Texture2D[] charaset;
        protected Vector2 position1;
        protected int threshold;
        protected Rectangle sourceRectangles;
        //IdleAnim
        protected float idleTimer;
        //Walk right/left anim
        protected int walkThreshold;
        protected float walkTimer;
        //Walk forward/back anim
        protected int forBackThreshold;

        #endregion
        public GameObjects(Vector2 pos)
        {
            position = pos;
        }
        private Texture2D currentSprite
        {
            get
            {
                return charaset[charSpriteIndex];
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
        
        protected void Move(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            position += ((velocity * speed) * deltaTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //player
            Vector2 origin = new Vector2(sourceRectangles.Width / 2, sourceRectangles.Height / 2);
            spriteBatch.Draw(charaset[charSpriteIndex],//what to draw
                position,//place to draw it
                sourceRectangles,//rectangle
                Color.White,//color of player
                0f, //Rotation of player
                origin,//Orgin Point
                scale,//How big is the player
                SpriteEffects.None,//effects
                0f);//Layer
            //draws the sprite sheet for debugging
            spriteBatch.Draw(charaset[charSpriteIndex], position1, Color.White);
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
