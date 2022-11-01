using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;


namespace _2UgersProject_Stardew_Valley
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private static Vector2 screenSize;
        private Texture2D grass_terrain;
        private Texture2D texture_terrain;
        private Rectangle terainRec;
        private List<GameObjects> gameObjects = new List<GameObjects>();
        private static List<GameObjects> gameObjectsToAdd = new List<GameObjects>();
        private float worldScale = 2.3f;//error visable at 2.3f not if over
        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            grass_terrain = Content.Load<Texture2D>("pixil-frame-0");

           
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            float gx = 0f;
            float gy = 0f;
            for (int i = 0; i < 375; i++)
            {
                switch (Terrain.Which_Terrain(gx, gy))
                {
                    case int n when (n == 0 || n == 1):
                        texture_terrain = grass_terrain;
                        break;
                }
                _spriteBatch.Draw(texture_terrain,//what to draw
                new Vector2(gx, gy),//place to draw it
                null,//rectangle
                Color.White,//color of player
               0f, //Rotation of player
                Vector2.Zero,//Orgin Point
                worldScale,//How big is the player
                SpriteEffects.None,//effects
                0f);//Layer 
                if (gx >= 1920 - 32*worldScale)
                {
                    gx = 0;
                    gy += 32f * worldScale;
        }
                else
                {
                    gx += 32f*worldScale;
                }
            }
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        protected void InstantiateGameObjects(GameObjects go)
        {

        }
    }
}