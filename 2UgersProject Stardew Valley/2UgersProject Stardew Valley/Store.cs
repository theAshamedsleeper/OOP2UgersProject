using Microsoft.Xna.Framework.Graphics;

namespace _2UgersProject_Stardew_Valley
{
    public static class Store
    {
        private static int storeX = 60;
        private static int storeY = 60;
        private static int storeWidth = 120;
        private static int storeHeight = 120;


        //A function that checks if another specific object is within the borders of the store chest.
        // - (Used in Update.Region:Store in GameWorld)
        public static bool CollisionWithChest(float otherX,float otherY,int otherWidth,int otherHeight)
        {
            if (otherX > storeX && otherX < storeX + storeWidth
                && otherX + otherWidth > storeX && otherX + otherWidth < storeX + storeWidth
                && otherY > storeY && otherY < storeY + storeHeight
                && otherY + otherHeight > storeY && otherY + otherHeight < storeY + storeHeight)
            {
                return true;

            }
            return false;
        }
    }

}
