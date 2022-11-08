using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace _2UgersProject_Stardew_Valley
{
    public class GameWorld : Game
    {
        private float terainBlockAmount = 576f;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private static Vector2 screenSize;
        private Texture2D grass_terrain;
        private Texture2D grass2_terrain;
        private Texture2D dirt_terrain;
        private Texture2D Hoe_Water_terrain;
        private Texture2D dirt2_terrain;
        private Texture2D hoe_terrain;
        private Texture2D texture_terrain;
        private Texture2D texture_plants;
        private Texture2D button_inv;
        private Texture2D button_baground;
        private List<GameObjects> gameObjects = new List<GameObjects>();
        private static List<GameObjects> gameObjectsToAdd = new List<GameObjects>();
        private float worldScale = 1.875f;//2.4f så passer den i width
        private bool inv = false;
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
            gameObjects.Add(new Player(new Vector2(400, 250)));
            Terrain.Give_Terrain();
            Inventory.Start_Inv();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            grass_terrain = Content.Load<Texture2D>("pixil-frame-0");
            grass2_terrain = Content.Load<Texture2D>("pixil-frame-1");
            dirt_terrain = Content.Load<Texture2D>("pixil-frame-2");
            dirt2_terrain = Content.Load<Texture2D>("pixil-frame-3");
            hoe_terrain = Content.Load<Texture2D>("pixilart-drawing_1");
            button_inv = Content.Load<Texture2D>("CtYf6HCWIAEwvF9_2");
            texture_plants = Content.Load<Texture2D>("CtYf6HCWIAEwvF9_2");
            button_baground = Content.Load<Texture2D>("CtYf6HCWIAEwvF9");
            Hoe_Water_terrain = Content.Load<Texture2D>("Sprites/WatedHoeGround");
            //player
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].LoadContent(Content);
            }


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update(gameTime);
            }
            #region inventory
            // needed for inventory, as spam opening it without a timer hurts my eyes.
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            // input for inventory
            if (Keyboard.GetState().IsKeyDown(Keys.I))
            {
                //  if its open
                if (inv == true)
                {
                    if (Inventory.timer(deltaTime) >= 0.5f)
                    {
                        Inventory.start_timer_closed();
                        // inventory closed, we stop it
                        inv = false;
                    }
                }
                else
                {
                    if(Inventory.timer_closed(deltaTime) >= 0.5f)
                    {
                        Inventory.start_timer();
                        // inventory open, start rendering and check for inputs from mouse
                        inv = true;
                    }
                }
            }
            // whats needed to update if inventory is open
            if (inv == true)
            {
                var mouseState = Mouse.GetState();
                var mousePosition = new Point(mouseState.X, mouseState.Y);
                // i send the mouse position over as i didn't wan't to load more libraries ind inventory
                Inventory.update(mousePosition, button_baground.Width*worldScale);
            }
            //counting for the inventory timer
            Inventory.timer_count(deltaTime);
            Inventory.timer_count_closed(deltaTime);
            #endregion
            // TODO: Add your update logic here
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            #region terain World
            // x and y coords of where the terrain tiles are drawn.
            float gx = 0f;
            float gy = 0f;
            // for loop to draw all the terrain.
            for (int i = 0; i < terainBlockAmount; i++)
            {
                #region texture terrain switch
                // the switch changes the terrain drawn depending on the terrain int.
                switch (Terrain.Which_Terrain(gx, gy))
                {
                    case 0:
                        texture_terrain = grass_terrain;
                        break;
                    case 1:
                        texture_terrain = grass2_terrain;
                        break;
                    case 2:
                        texture_terrain = dirt_terrain;
                        break;
                    case 3:
                        texture_terrain = dirt2_terrain;
                        break;
                    case 4:
                        texture_terrain = hoe_terrain;
                        break;
                    case 5:
                        texture_terrain = grass2_terrain;
                        break;
                    case 6:
                        texture_terrain = Hoe_Water_terrain;
                        break;
                }
                #endregion
                _spriteBatch.Draw(texture_terrain,//what to draw
                new Vector2(gx, gy),//place to draw it
                null,//rectangle
                Color.White,//color of player
                0f, //Rotation of player
                Vector2.Zero,//Orgin Point
                worldScale,//How big is the player
                SpriteEffects.None,//effects
                1f);//Layer 
                if (gx >= 1920 - 32 * worldScale)
                {
                    gx = 0;
                    gy += 32f * worldScale;
                }
                else
                {
                    gx += 32f * worldScale;
                }
            }
            #endregion
            #region plants
            float px = 0f;
            float py = 0f;
            // for loop to draw all the terrain.
            for (int i = 0; i < terainBlockAmount; i++)
            {
                if (Plant_t.Plant_Check_b(px, py))
                {
                    _spriteBatch.Draw(texture_plants,//what to draw
                new Vector2(px, py),//place to draw it
                null,//rectangle
                Color.White,//color of player
                0f, //Rotation of player
                Vector2.Zero,//Orgin Point
                worldScale,//How big is the player
                SpriteEffects.None,//effects
                0f);//Layer 

                }
                if (px >= 1920 - 32 * worldScale)
                {
                    px = 0;
                    py += 32f * worldScale;
                }
                else
                {
                    px += 32f * worldScale;
                }
            }
            #endregion
            #region Player
            foreach (GameObjects go in gameObjects)
            {
                go.Draw(_spriteBatch);
            }
            #endregion
            #region buttons
            if (inv == true)
            {
                // runescape baground drawn.
                _spriteBatch.Draw(button_baground,//what to draw
                    new Vector2(Inventory.Inventory_x, Inventory.Inventory_y),//place to draw it
                    null,//rectangle
                    Color.White,//color 
                    0f, //Rotation
                    Vector2.Zero,//Orgin Point
                    worldScale,//How big
                    SpriteEffects.None,//effects
                    0f);//Layer
                //for loop for all buttons drawn, if theres an item to be drawn, its drawn here.
                for (int i = 0; i < Inventory.But_y.Length; i++)
                {
                    Color button_color = Color.White;
                    int bx = Inventory.But_x[i];
                    int by = Inventory.But_y[i];
                    if (Inventory.Butten_higlight - 1 == i)
                    {
                        button_color = Color.Black;
                    }
                    _spriteBatch.Draw(button_inv,//what to draw
                    new Vector2(Inventory.Inventory_x + bx, Inventory.Inventory_y+ by),//place to draw it
                    null,//rectangle
                    button_color,//color 
                    0f, //Rotation
                    Vector2.Zero,//Orgin Point
                    worldScale,//How big 
                    SpriteEffects.None,//effects
                    0f);//Layer
                }
            }
            #endregion
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        protected void InstantiateGameObjects(GameObjects go)
        {

        }
    }
}