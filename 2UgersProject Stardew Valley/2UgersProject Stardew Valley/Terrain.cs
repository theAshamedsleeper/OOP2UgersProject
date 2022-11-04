using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using System.Reflection.Metadata;

namespace _2UgersProject_Stardew_Valley
{
    internal static class Terrain
{

        private static int width = 1024 / 32;
        private static int height = 576 / 32;
        private static int x_1 = 0;
        private static int y_1 = 0;
        private static int z_1 = 0;
        private static int terrain_amount = 5; 
        private static float scale = 1.875f;
        private static int[] tiles_x = new int[width * height];
        private static int[] tiles_y = new int[width * height];
        private static int[] tiles_t = new int[width * height];
        #region terrain making
        /// <summary>
        /// a method to give value to 3 arrays, so we can more easily allocate which is dirt grass or hoed dirt.
        /// it will give the x and y values, and check if those coordinates are dirt or grass.
        /// it has 3 arrays one for x, y and terrain type, the terrain type is chosen with a number.
        /// this way we will be able to pin point a location, 
        /// we believe this will be more effective than an object for each tile.
        /// </summary>
        public static void Give_Terrain()
        {
            for (int i_2 = 0; i_2 < width*height; i_2++)
            {
                // function/method to see if the given x and y coordinates have a predetermined value for the terrain
                z_1 = start_terrain(x_1, y_1);
                // giving the values to the arrays
                tiles_x[i_2] = x_1;
                tiles_y[i_2] = y_1;
                tiles_t[i_2] = z_1;
                // updating the coordinates
                if (x_1 == width - 1)
                {
                    x_1 = 0;
                    y_1 += 1;
                }
                else
                {
                    x_1 += 1;
                }
            }
        }
        // our start terrain method,
        // it will say which coordinates should return dirt or grass,
        // with what we wish the starting land should look like
        static int start_terrain(int x_1, int y_1)
        {
            switch (y_1)
            {
                case int n when (n >= 7 && n <=10):
                    return 2;
                case int n when (n >= 11 && n <= 15):
                    return 3;
            }
            return 0;
        }
        #endregion
        #region LoadUpdateDraw
        public static void LoadContent(ContentManager content)
        {

        }
        public static void Update(GameTime gametime)
        {

        }
        public static void Draw(SpriteBatch spritebatch)
        {
            for (int i = 0; i < width * height; i++)
            {

            }
        }
        private static void Animate(GameTime gametime)
        {

        }
        private static void LoadContent()
        {

        }
        #endregion
        private static void Decrease_Speed()
        {

        }
        private static void Inmoveable()
        {

        }
        #region getinfo
        /// <summary>
        /// method to change a tile, takes the x,y coord and the tile wanted to change into.
        /// it will then check if the tile is within the amount of tiles useable, if yes,
        /// it will find where in the array its changing the value, and then change it.
        /// </summary>
        /// <param name="x"> x coordinate </param>
        /// <param name="y"> y coordinate </param>
        /// <param name="z"> the value tile wanted to be changed to </param>
        public static void Terrain_Change(float x, float y, int z)
        {
            int x_mod = 0;
            if (0 <= z && z <= terrain_amount)
            {
                float x_1 = x - x % 1;
                float y_1 = y - y % 1;
                for (int i = 0; i < height; i++)
                {
                    if (tiles_y[i * width] == y_1)
                    {
                        x_mod = i;
                    }
                }
                for (int i = 0; i < width; i++)
                {
                    if (tiles_x[(x_mod * width) + i] == x_1)
                    {
                        tiles_t[(x_mod * width) + i] = z;
                    }
                }
            }

        }
        /// <summary>
        /// a method to see which terrain a given input is. 
        /// </summary>
        /// <param name="x"> x coord </param>
        /// <param name="y"> y coord </param>
        /// <returns></returns>
        public static int Which_Terrain(float x, float y)
        {
            // Makes input applicable for sprite size.
            float y_1 = (((y / scale) - ((y / scale) % 32f))/32f);
            float x_1 = (((x / scale) - ((x / scale) % 32f))/32f);
            // xmod * width is basically the location of our y pos in y array
            int x_mod = 0;
            // locate location of y pos in y array
            for (int i = 0; i < height; i++)
            {
                if (tiles_y[i * width + 1] == y_1)
                {
                    x_mod = i;
                }
            }
            // locates x pos in x array, but its increases by y array possion amount.
            for (int i = 0; i < width; i++)
            {
                if (tiles_x[(x_mod * width) + i] == x_1)
                {
                    // found the location of the value of terrain in terrain array
                    return tiles_t[(x_mod * width) + i];
                }
            }
            return 0;
        }
        #endregion
    }
}
