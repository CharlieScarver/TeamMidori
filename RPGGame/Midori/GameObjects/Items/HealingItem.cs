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
        public HealingItem(Vector2 position) : base(TextureLoader.HealthBox, position, ItemTypes.Heal)
        {
            this.Color = Color.White;
        }
    }
}
