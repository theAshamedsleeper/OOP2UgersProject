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
        private static int growth = 0;
        private static float grow = 0; 
        #region new plants
        public static void New_Plant(float x, float y, int p)
        {
            float scale = Terrain.t_scale;
            float y_1 = (((y / scale) - ((y / scale) % 32f)) / 32f);
            float x_1 = (((x / scale) - ((x / scale) % 32f)) / 32f);
            int x_i = (int)x_1;
            int y_i = (int)y_1;
            int[] pla = new int[3];
            pla[0] = x_i;
            pla[1] = y_i;
            pla[2] = p;
            plantys.Add(pla);
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

        }
        public static void Grow(float deltatime)
        {
            grow += deltatime;
            if (grow > 1)
            {

            }
        }
        #endregion
        #region actions
        public static int Plant_Check(float x, float y)
        {
            x = (((x / scale) - ((x / scale) % 32f)) / 32f);
            y = (((y / scale) - ((y / scale) % 32f)) / 32f);
            for (int i = 0; i < plantys.Count; i++)
            {
                int[] pla = plantys[i];
                if (x == pla[0])
                {
                    for (int i_2 = 0; i_2 < plantys.Count; i_2++)
                    {
                        int[] pla_2 = plantys[i_2];
                        if (y == pla_2[1])
                        {
                            return pla_2[2];
                        }
                    }
                }
            }
            return 0; 
        }
        public static bool Plant_Check_b(float x, float y)
        {
            for (int i = 0; i < plantys.Count; i++)
            {
                int pla = plantys[i][0];
                if ((((x / scale) - ((x / scale) % 32f)) / 32f) == pla)
                {
                    int pla_y = plantys[i][1];
                    if ((((y / scale) - ((y / scale) % 32f)) / 32f) == pla_y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

    }
}
