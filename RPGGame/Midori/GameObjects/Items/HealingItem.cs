using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Midori.Enumerations;
using Midori.TextureLoading;

namespace Midori.GameObjects.Items
{
    public class HealingItem : Item
    {
        public HealingItem(Vector2 position) : base(TextureLoader.HealthBox, position, ItemType.Heal)
        {
            this.Color = Color.White;
        }
    }
}
