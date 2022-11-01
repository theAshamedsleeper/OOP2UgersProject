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

        private static float animation_Speed = 0;
        private static float animation_Time = 0;
        private static int width = 800 / 32;
        private static int height = 480 / 32;
        private static int x_1 = 0;
        private static int y_1 = 0;
        private static int z_1 = 0;
        private static int[] tiles_x = new int[width * height];
        private static int[] tiles_y = new int[width * height];
        private static int[] tiles_t = new int[width * height];
        #region terrain making
        public static void Give_Terrain()
        {
            for (int i_2 = 0; i_2 < 15*25; i_2++)
            {
                // function/method to see if the given x and y have a predetermined brik value
                z_1 = start_terrain(x_1, y_1);
                tiles_x[i_2] = x_1;
                tiles_y[i_2] = y_1;
                tiles_t[i_2] = z_1;
                if (x_1 == width)
                {
                    x_1 = 1;
                    y_1 += 1;
                }
                else
                {
                    x_1 += 1;
                }
            }
        }
        static int start_terrain(int x_1, int y_1)
        {
            switch (y_1)
            {
                case int n when (n >= 1 && n <= 8):
                    return 1;
                case int n when (n >= 9 && n <= 15):
                    return 0;
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
        public static int X
        {
            get { return tiles_x[0]; }
        }
        public static int Which_Terrain(int x, int y)
        {
            // Makes input applicable for sprite size.
            int y_1 = (y - (y % 32))/32;
            int x_1 = (x - (x % 32))/32;
            // xmod * width is basically the location of our y pos in y array
            int x_mod = 0;
            // locate location of y pos in y array
            for (int i = 0; i < height; i++)
            {
                if (tiles_y[i * width] == y_1)
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
