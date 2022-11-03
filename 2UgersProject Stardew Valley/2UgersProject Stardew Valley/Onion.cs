using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace _2UgersProject_Stardew_Valley
{
    internal class Onion : Plants
{
        private float growth_amount = 0;
        private int growth_full = 1000;
        private bool is_harvestable = false;

        public Onion(Vector2 pos) : base(pos)
        {
            scale = 1.875f;
        }
        private void grow(float time)
        {
            growth_amount += time;
        }
        public bool can_harvest()
        {
            if (growth_amount > growth_full*0.9f)
            {
                is_harvestable = true;
            }
            return is_harvestable;
        }
        public override void LoadContent(ContentManager content)
        {
            sprite = new Texture2D[5];
            for (int i = 0; i < sprite.Length; i++)
            {
                sprite[i] = content.Load<Texture2D>($"pixilart-frames_4/pixil-frame-{i}");
            }
        }
        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            grow(deltaTime);
        }
        public Texture2D Onion_sprite()
        {
            switch (growth_amount)
            {
                case float n when (n >= 0 && n <= growth_full * 0.2f):
                    return sprite[0];
                case float n when (n > growth_full * 0.2f && n <= growth_full * 0.4f):
                    return sprite[1];
                case float n when (n > growth_full * 0.4f && n <= growth_full * 0.6f):
                    return sprite[2];
                case float n when (n > growth_full * 0.6f && n <= growth_full * 0.8f):
                    return sprite[3];
                case float n when (n > growth_full * 0.8f && n <= growth_full):
                    return sprite[4];
            }
            return sprite[0];
        }
    }
}
