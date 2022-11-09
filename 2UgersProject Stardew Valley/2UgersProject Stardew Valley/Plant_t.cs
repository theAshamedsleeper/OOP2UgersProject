using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace _2UgersProject_Stardew_Valley
{
    internal static class Plant_t
{
        private static List<int[]> plantys = new List<int[]>();
        private static List<int[]> plant_r_s = new List<int[]>();
        private static List<int> plant_r = new List<int>();
        private static float scale = Terrain.t_scale;
        private static float grow = 0; 
        #region new plants
        public static void New_Plant(float x, float y, int p)
        {
            // the  2 if statements are to make sure we don't make many plants the same place
            if (are_there_plants())
            {
                float scale = Terrain.t_scale;
                float y_1 = (((y / scale) - ((y / scale) % 32f)) / 32f);
                float x_1 = (((x / scale) - ((x / scale) % 32f)) / 32f);
                int x_i = (int)x_1;
                int y_i = (int)y_1; 
                int[] pla = new int[5];
                pla[0] = x_i;
                pla[1] = y_i;
                // plant growth
                pla[2] = 1;
                // plant wetness
                pla[3] = 300;
                //plant type
                pla[4] = p;
                plantys.Add(pla);
            }
            else
            {
                if (Plant_Check_b(x, y))
                {
                    float scale = Terrain.t_scale;
                    float y_1 = (((y / scale) - ((y / scale) % 32f)) / 32f);
                    float x_1 = (((x / scale) - ((x / scale) % 32f)) / 32f);
                    int x_i = (int)x_1;
                    int y_i = (int)y_1; 
                    int[] pla = new int[4];
                    pla[0] = x_i;
                    pla[1] = y_i;
                    pla[2] = p;
                    pla[3] = 300;
                    plantys.Add(pla);
                }
            }
        }
        #region remove later
        private static void Sort_Plants(int[] plants)
        {
            if (plant_r.Count > 0 )
            {
                for (int i = 0; i < plant_r.Count; i++)
                {
                    plant_r.RemoveAt(0);
                }
            }
            int fine = plantys.Count;
            for (int i = 0; i < plantys.Count; i++)
            {
                if (plants[0] < plantys[i][0])
                {
                    plant_r.Add(i);
                }
                else
                {
                    fine--;
                }
            }
            if (fine == 0)
            {
                plantys.Add(plants);
            }
            else
            {
                Sorty(plant_r, plants);
            }
        }
        private static void Sorty(List<int> plant_r, int[] Plants)
        {
            for (int i = 0; i < plant_r.Count; i++)
            {
                plant_r_s.Add(plantys[plant_r[plant_r.Count - (1 + i)]]);
            }
            for (int i = 0; i < plant_r.Count; i++)
            {
                plantys.RemoveAt(plant_r[plant_r.Count - (1 + i)]);
            }
            plantys.Add(Plants);
            for (int i = 0; i < plant_r.Count; i++)
            {
                plantys.Add(plant_r_s[plant_r[plant_r.Count - (1 + i)]]);
            }
            for (int i = 0; i < plant_r.Count; i++)
            {
                plant_r_s.RemoveAt(plant_r[plant_r.Count - (1 + i)]);
            }

        }
        #endregion
        #endregion
        #region updating
        public static void update(float deltatime)
        {
            grow += deltatime;
            // forgot i was gonna work with floats, and my plant list is ints. so this int will make more int like.
            if (grow > 1)
            {
                for (int i = 0; i < plantys.Count; i++)
                {
                    //if below max growth
                    if (plantys[i][2] < 1000)
                    {
                        // if wet
                        if (plantys[i][3] > 0)
                        {
                            plantys[i][2] += 10;
                            plantys[i][3] -= 5;
                        }
                    }
                    else
                    {
                        plantys[i][3] -= 5;
                    }
                }
                grow--;
            }
        }
        #endregion
        #region actions
        /// <summary>
        /// General checking function, given standard x and y coords, it will make those coords compatible, 
        /// and then go through all plants to check if theres a plant at given location, was gonna make it return true, 
        /// but giving the index of the plant proves more usefull, returns a -1 in case of false.
        /// </summary>
        /// <param name="x"> x coords </param>
        /// <param name="y"> y coords </param>
        /// <returns></returns>
        private static int check(float x, float y)
        {
            x = (((x / scale) - ((x / scale) % 32f)) / 32f);
            y = (((y / scale) - ((y / scale) % 32f)) / 32f);
            for (int i = 0; i < plantys.Count; i++)
            {
                int[] pla = plantys[i];
                if (x == pla[0])
                {
                    int[] pla_2 = plantys[i];
                    if (y == pla_2[1])
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        //removes plant at location
        public static void plant_re(float x, float y)
        {
            int i = check(x, y);
            if (i > -1)
            {
                plantys.RemoveAt(i);
            }
        }
        // increases wetness of given plant
        public static void plant_wet(float x, float y)
        {
            int i = check(x, y);
            if (i > -1)
            {
                plantys[i][3] += 100;
            }
        }
        #region checks
        // plant check, returns growth
        public static int Plant_Check_G(float x, float y)
        {
            int i = check(x, y);
            if (i > -1)
            {
                return plantys[i][2];
            }
            return 0; 
        }
        // plant check, returns bool, if theres a plant at location
        public static bool Plant_Check_b(float x, float y)
        {
            int i = check(x, y);
            if (i > -1)
            {
                return true;
            }
            return false;
        }
        // plant check, returns wetness
        public static int Plant_Check_wet(float x, float y)
        {
            int i = check(x, y);
            if (i > -1)
            {
                return plantys[i][3];
            }
            return -1; ;
        }
        // plant check, returns bool, if there are plants
        public static bool are_there_plants()
        {
            if (plantys.Count > 0)
            {
                return true;
            }
            return false;
        }
        #endregion
        #endregion

    }
}
