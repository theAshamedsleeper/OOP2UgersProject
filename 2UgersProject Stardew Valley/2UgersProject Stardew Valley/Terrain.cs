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
    private Texture2D sprites;

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
                    return 2;
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
        static int Which_Terrain(int[] pos, int[] tx, int[] ty, int[] t)
        {
            // posision of a given input, and returns the position of where in the array of briks, that is the given value of the coordinate of the input.
            // checks where in array of y coordinates, the given inpus is.
            int y_1 = pos[1] - pos[1] % 32;
            int x_1 = pos[0] - pos[0] % 32;
            int x_mod = 0;
            /* it locates the correct pos in the array by going down 8 times in x by each y value, as 8 is then the y value increases.
            * and the coordinate system could be written in one array, where each pos is x, up to 64, so i return it to that. */
            for (int i = 0; i < height; i++)
            {
                if (ty[i * width] == y_1)
                {
                    x_mod = i;
                }
            }
            for (int i = 0; i < width; i++)
            {
                if (tx[(x_mod * width) + i] == x_1)
                {
                    return t[(x_mod * width) + i];
                }
            }
            return 0;
        }
        #endregion
    }
}
