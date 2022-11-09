using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using static System.Formats.Asn1.AsnWriter;

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
        private Texture2D pixel;
        private Texture2D[] onions_sprite = new Texture2D[2];
        private Rectangle onions_rec;
        protected Texture2D seedChest;
        private List<GameObjects> gameObjects = new List<GameObjects>();
        private static List<GameObjects> gameObjectsToAdd = new List<GameObjects>();
        private float worldScale = 1.875f;//2.4f så passer den i width
        private bool inv = false;
        private int Onion_x = 0;
        private int o_s_wet = 0;
        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            _graphics.IsFullScreen = false;
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
            seedChest = Content.Load<Texture2D>("Sprites/seedCheestSprite");
            onions_sprite[0] = Content.Load<Texture2D>("Sprites/OnionGrowingNotWater");
            onions_sprite[1] = Content.Load<Texture2D>("Sprites/Watered growin Onion");
            pixel = Content.Load<Texture2D>("pixel");
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

            #region Store

            if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                if (Store.CollisionWithChest(Inventory.player_pos_x, Inventory.player_pos_y, 32, 64))
                {
                    Inventory.inv_give(1, 1);
                    //Index mangler ^


                }


            }

                #endregion

                Plant_t.update(deltaTime);
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
                    switch (Plant_t.Plant_Check_G(px, py))
                    {
                        case int n when n >= 0 && n < 200:
                            Onion_x = 0;
                            break;
                        case int n when n >= 200 && n < 400:
                            Onion_x = 32;
                            break;
                        case int n when n >= 400 && n < 600:
                            Onion_x = 64;
                            break;
                        case int n when n >= 600 && n < 800:
                            Onion_x = 96;
                            break;
                        case int n when n >= 800 && n < 1000:
                            Onion_x = 128;
                            break;
                    }
                    if (Plant_t.Plant_Check_wet(px, py) < 50)
                    {
                        o_s_wet = 0;
                    }
                    else
                    {
                        o_s_wet = 1;
                    }
                    onions_rec = new Rectangle(Onion_x, 0, 32, 32);
                    _spriteBatch.Draw(onions_sprite[o_s_wet],//what to draw
                new Vector2(px, py),//place to draw it
                onions_rec,//rectangle
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
            #region box color
            box();
            #endregion
            #region store
            _spriteBatch.Draw(seedChest,//what to draw
                   new Vector2(100, 100),//place to draw it
                   null,//rectangle
                   Color.White,//color of player
                   0f, //Rotation of player in radianer
            new Vector2(0, 0),//Orgin Point
                   2f,//How big is the player
                   SpriteEffects.None,//effects
                   0.9f);//Layer higher the number further back it is
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
        // drawing box
        private void box()
        {
            //
            float x = Inventory.player_pos_x + 32 - (Inventory.player_pos_x + 32) % 60;
            float y = Inventory.player_pos_y + 32 - (Inventory.player_pos_y + 32) % 60;
            int width = (int)(32 * worldScale);
            int height = (int)(32 * worldScale);
            Rectangle top = new Rectangle((int)x, (int)y, width, 1);
            Rectangle bottom = new Rectangle((int)x, (int)y + height, width, 1);
            Rectangle left = new Rectangle((int)x, (int)y, 1, height);
            Rectangle right = new Rectangle((int)x + width, (int)y, 1, height);

            _spriteBatch.Draw(pixel, top, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            _spriteBatch.Draw(pixel, bottom, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            _spriteBatch.Draw(pixel, left, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            _spriteBatch.Draw(pixel, right, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }
    }
}