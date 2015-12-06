using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.GameObjects.Projectiles
{
    public class AyaSmallProjectile : Projectile
    {
        private const int textureWidth = 101;
        private const int textureHeight = 36;
        private const int delay = 100;
        private const int basicAnimationFrameCount = 2;
        private const float defaultMovementSpeed = 10;

        public AyaSmallProjectile(Vector2 position)
            : base()
        {
            this.Position = position;

            this.SourceRect = new Rectangle(0 * AyaSmallProjectile.textureWidth, AyaSmallProjectile.textureHeight * 0, AyaSmallProjectile.textureWidth, AyaSmallProjectile.textureHeight);

            this.MovementSpeed = AyaSmallProjectile.defaultMovementSpeed;
        }

        public override void Update(GameTime gameTime)
        {
            this.X += this.MovementSpeed;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.SpriteSheet, this.Position, this.SourceRect, Color.White);
        }

        
    }
}
