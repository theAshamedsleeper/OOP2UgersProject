using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2UgersProject_Stardew_Valley
{
    internal static class Inventory
{
        
        private static int[] inv_space = new int[10];
        public static void Inv_cha(int i_1, int i_2)
        {
            int i_3 = 0;
            if (i_2 != 0)
            {
                i_3 = i_2;
            }
            i_2 = i_1;
            i_1 = i_3;
        }
        
        
}
}
