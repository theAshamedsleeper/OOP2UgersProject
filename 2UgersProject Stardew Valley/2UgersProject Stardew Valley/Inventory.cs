using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace _2UgersProject_Stardew_Valley
{
    internal static class Inventory
{
        private static int[] inv_space = new int[28];
        private static int[] b_x = new int[28];
        private static int[] b_y = new int[28];
        private static int inventory_x = 500;
        private static int inventory_y = 200;
        private static int button_width = 50;
        private static int button_height = 50;
        private static int button_chosen = 0;
        private static bool inv_move = false;
        private static float time = 0.5f;
        private static float time_2 = 0.5f;
        private static int inv_mos_mod_x = 0;
        private static int inv_mos_mod_y = 0;
        #region gets
        //Values we wanna be able to call in this and other classes
        public static int Inventory_x { get { return inventory_x; } }
        public static int Inventory_y { get { return inventory_y; } }
        public static int[] But_x { get { return b_x; } }
        public static int[] But_y { get { return b_y; } }
        public static int Butten_higlight { get { return button_chosen; } }
        public static float inv_scale { get { return 1.875f + 1.125f; } }
        #region me not liking non main static classes
        private static float player_x = 0;
        private static float player_y = 0;
        public static float player_pos_x { get { return player_x; } }
        public static float player_pos_y { get { return player_y; } }

        public static void player_pos(float x, float y)
        {
            player_x = x;
            player_y = y;
        }
        #endregion
        #endregion
        #region collis
        /// <summary>
        /// Simple box collision
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x_2"></param>
        /// <param name="y_2"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private static bool box_collis(int x, int y, int x_2, int y_2, float width, float height)
        {
            if (x >= x_2 && x <= x_2 + width
             && y >= y_2 && y <= y_2 + height)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region startup
        /// <summary>
        /// Giving the location of all buttons on startup, cuz i don't wanna write em in manually.
        /// Makes longer startup, but less space of program, and its more eazy to add or decrease buttons.
        /// </summary>
        public static void Start_Inv()
        {
            for (int i = 0; i < b_x.Length; i++)
            {
                if (i%4 != 0)
                {
                    //same row, plus the x coords.
                    b_x[i] = b_x[i - 1] + 90;
                    b_y[i] = b_y[i - 1];
                }
                else
                {
                    // new row
                    b_x[i] = 25;
                    b_y[i] = 70 + i/4 * 70;
                }
                // assining the inventory to the location.
                inv_space[i] = 0;
            }
        }
        #endregion
        #region Interactions
        /// <summary>
        /// Change of items in inventory, takes value i_1 (inventory space 1) and gives it to i_2 (inventory space 2)
        /// Checks first if i_2 has a value thats not 0, if so saves that on another value, to then give to i_1
        /// </summary>
        /// <param name="i_1"> selected inventory space 1 </param>
        /// <param name="i_2"> selected inventory space 2 </param>
        private static void Inv_cha(int i_1, int i_2)
        {
            i_1 -= 1;
            int i_3 = 0;
            if (inv_space[i_2] != 0)
            {
                inv_space[i_3] = inv_space[i_2];
            }
            inv_space[i_2] = inv_space[i_1];
            inv_space[i_1] = inv_space[i_3];
        }
        public static int inv_which(int index)
        {
            return inv_space[index];
        }
        public static void inv_give(int index, int item)
        {
            inv_space[index] = item; 
        }
        /// <summary>
        /// Checks for a collision of a mouse click and inventory positions.
        /// </summary>
        /// <param name="mousePosition"> mousePosition couldn't be made in this class, without the correct library, 
        /// but a point variable can be transfered to other classe with funktions, saves the space of loading another library. </param>
        /// <param name="inv_width"> I hate not working with the main class as a static class. Cuz i needed to do this to get the width of sprite of inventory</param>
        public static void update(Point mousePosition, float inv_width)
        {
            //getting the variable button press, for checking if ppl click
            var mouseState = Mouse.GetState();
            // this if checks if the movement of inventory is taking place
            if (inv_move == false)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {   //For loop, checks collision for all buttons
                    for (int i = 0; i < b_x.Length; i++)
                    {
                        //standard box collision
                        if (box_collis(mousePosition.X, mousePosition.Y, b_x[i] + inventory_x, b_y[i] + inventory_y,
                            button_width * 1.30f, button_height * 1.30f) == true)
                        {
                            //checks for if a inventory space is selected ( button_chosen gets a +1 cuz 0 is none selected).
                            if (button_chosen == 0 || button_chosen == i + 1)
                            {
                                button_chosen = i + 1;
                            }
                            else
                            {
                                // if button chosen will change location of em.
                                Inv_cha(button_chosen, i);
                                button_chosen = 0;
                            }
                        }
                    }
                    // if u click on top part of inventory, u start moving it
                    if (box_collis(mousePosition.X, mousePosition.Y, inventory_x, inventory_y, inv_width, 50))
                    {
                        inv_move = true;
                        // modifier, for then u try to move a window then the windows origin (top left cornor) dosen't become the mouse position.
                        // its not natural feeling, so these modifiers makes the origin moves with mouse movement, and not directly on mouse.
                        inv_mos_mod_x = mousePosition.X - inventory_x;
                        inv_mos_mod_y = mousePosition.Y - inventory_y;
                    }
                }
            }
            else
            {
                // ur moving inventory, sets inventorys coords to mouse pos (- modifier), then checks if u stop pressing left click
                inventory_x = mousePosition.X - inv_mos_mod_x;
                inventory_y = mousePosition.Y - inv_mos_mod_y;
                if (mouseState.LeftButton == ButtonState.Released)
                {
                    inv_move = false;
                }
            }
        }
        #endregion
        #region timer
        // timer checker.
        public static float timer(float deltatime)
        {
            time += deltatime;
            return time;
        }
        // starting the timer
        public static void start_timer()
        {
            time = 0;
        }
        // timer counting
        public static void timer_count(float deltatime)
        {
            time += deltatime;
        }
        // the same but for closed.
        public static float timer_closed(float deltatime)
        {
            time_2 += deltatime;
            return time_2;
        }
        public static void start_timer_closed()
        {
            time_2 = 0;
        }
        public static void timer_count_closed(float deltatime)
        {
            time_2 += deltatime;
        }
        #endregion
    }
}
