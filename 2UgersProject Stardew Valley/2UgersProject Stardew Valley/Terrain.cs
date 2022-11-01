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
        public static void LoadContent(content: ContentManager)
        {

        }
        public static void Update(gametime: GameTime)
        {

        }
        public static void Draw(spritebach: SpriteBatch)
        {

        }
        private static void Animate(gametime: GameTime)
        {

        }
        private static void LoadContent()
        {

        }
        private static void Decrease_Speed()
        {

        }
        private static void Inmoveable()
        {

        }
        public static string X
        {
            get { return ; }
        }
    }
}
