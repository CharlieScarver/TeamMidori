using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Midori.Core.TextureLoading;

namespace Midori.GameObjects.Items
{
    public class HealingItem : Item
    {
        private int healAmount = 10;
        public HealingItem(Texture2D sprite, Vector2 position) : base(sprite, position, ItemTypes.Heal)
        {
            this.Color = Color.Green;
        }

        public int HealAmount
        {
            get { return this.healAmount; }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(
                spriteFont: TextureLoader.Font,
                text: healAmount.ToString(),
                position: this.Position,
                color: this.Color * 0.8f);
        }
    }
}
