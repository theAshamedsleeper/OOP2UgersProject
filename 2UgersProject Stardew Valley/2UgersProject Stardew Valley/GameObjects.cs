using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace _2UgersProject_Stardew_Valley
{
    public abstract class GameObjects
    {
        protected Texture2D[] sprite;
        protected Texture2D[] barSprite;
        protected Rectangle[] energyRecBar = new Rectangle[5];
        protected Rectangle seedChestRectangle;
        protected Vector2 position;
        protected float scale;
        protected float speed = 200f;
        protected Vector2 velocity;

        #region Character attributes
        protected byte charSpriteIndex=0;
        protected Texture2D[] charaset;
        protected Vector2[] position1 = new Vector2[5];
        protected int threshold;
        protected Rectangle sourceRectangles;
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
            spriteBatch.Draw(currentSprite,//what to draw
                position,//place to draw it
                sourceRectangles,//rectangle
                Color.White,//color of player
                0f, //Rotation of player in radianer
                origin,//Orgin Point
                scale,//How big is the player
                SpriteEffects.None,//effects
                0f);//Layer higher the number further back it is 
            //draws the sprite sheet for debugging
            #region Draw Food and energy
            //draw background color for energyBar
            spriteBatch.Draw(barSprite[1],
                position1[2],
                energyRecBar[1],
                Color.White,
                (float)Math.PI,
                new Vector2(0,0),
                scale,
                SpriteEffects.None,
                0.2f); 
            //draw background color for foodbar
            spriteBatch.Draw(barSprite[3],
                position1[4],
                energyRecBar[3],
                Color.White,
                (float)Math.PI,
                new Vector2(0,0),
                scale,
                SpriteEffects.None,
                0.2f); 
            //draw in foodBar
            spriteBatch.Draw(barSprite[2],
                position1[3],
                energyRecBar[2],
                Color.White,
                (float)Math.PI,
                new Vector2(0,0),
                scale,
                SpriteEffects.None,
                0f);

            //draws in energy bar
            spriteBatch.Draw(barSprite[0],
                position1[1],
                energyRecBar[0],
                Color.White,
                (float)Math.PI,
                new Vector2(0,0),
                scale,
                SpriteEffects.None,
                0f);
            #endregion
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
    }
}
