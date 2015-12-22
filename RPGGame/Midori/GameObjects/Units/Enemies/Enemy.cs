using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Midori.Interfaces;
using Midori.TextureLoading;

namespace Midori.GameObjects.Units.Enemies
{
    public abstract class Enemy : Unit, IEnemy
    {
        protected Enemy()
            : base()
        { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.Health != this.MaxHealth)
            {
                var healthPercent = (float)this.Health / this.MaxHealth;
                var healthBarWidth = (int)(100 * healthPercent);
                var healthBar = new Rectangle(((int)this.BoundingBoxX + this.BoundingBox.Width / 2) - 50,
                    (int)this.Position.Y - 20,
                    healthBarWidth,
                    10);
                spriteBatch.Draw(TextureLoader.TheOnePixel, healthBar, null, Color.Red);
            }
            spriteBatch.Draw(this.SpriteSheet, this.Position, this.SourceRect, Color.White);
        }

    }
}
