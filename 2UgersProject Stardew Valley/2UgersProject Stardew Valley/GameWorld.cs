using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;


namespace _2UgersProject_Stardew_Valley
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private static Vector2 screenSize;
        private Texture2D grass_terrain;
        private Texture2D texture_terrain;
        private List<GameObjects> gameObjects = new List<GameObjects>();
        private static List<GameObjects> gameObjectsToAdd = new List<GameObjects>();
        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            int gx = 0;
            int gy = 0;
            for (int i = 0; i < 375; i++)
            {
                switch (Terrain.Which_Terrain(gx, gy))
                {
                    case int n when (n == 0 || n == 1):
                        texture_terrain = grass_terrain;
                        break;
                }
                _spriteBatch.Draw(texture_terrain, new Vector2(gx, gy), Color.White);
                if (gx == 800 - 32)
                {
                    gx = 0;
                    gy += 32;
                }
                else
                {
                    gx += 32;
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