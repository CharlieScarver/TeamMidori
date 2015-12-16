using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Midori.TextureLoading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.GameObjects.Units.Enemies
{
    public abstract class Enemy : Unit
    {
        public Enemy()
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
