using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2UgersProject_Stardew_Valley
{
    public abstract class Plants : GameObjects
{
        public Plants(Vector2 pos) : base(pos)
        {
            scale = 1.875f;
        }
        private void grow()
        {

        }
        public override void LoadContent(ContentManager content)
        {
        }
        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
