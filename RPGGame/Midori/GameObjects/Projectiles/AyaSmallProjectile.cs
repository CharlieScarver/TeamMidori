using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Midori.Core.TextureLoading;
using Midori.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Midori.GameObjects.Units;

namespace Midori.GameObjects.Projectiles
{
    public class AyaSmallProjectile : Projectile
    {
        private const int textureWidth = 100;
        private const int textureHeight = 36;
        private const int delay = 100;
        private const int basicAnimationFrameCount = 2;
        private const float defaultMovementSpeed = 22;

        public AyaSmallProjectile(Vector2 position, bool movingRight, Unit owner)
            : base()
        {
            this.Position = position;

            this.SpriteSheet = TextureLoader.SmallAyaProjectileSheet;
            this.TextureWidth = AyaSmallProjectile.textureWidth;
            this.TextureHeight = AyaSmallProjectile.textureHeight;

            this.IsMovingRight = movingRight;
            if (this.IsMovingRight)
            { 
                this.SourceRect = new Rectangle(0 * AyaSmallProjectile.textureWidth, AyaSmallProjectile.textureHeight * 0, AyaSmallProjectile.textureWidth, AyaSmallProjectile.textureHeight);
            }
            else
            {
                this.SourceRect = new Rectangle(0 * AyaSmallProjectile.textureWidth, AyaSmallProjectile.textureHeight * 1, AyaSmallProjectile.textureWidth, AyaSmallProjectile.textureHeight);
            }

            this.MovementSpeed = AyaSmallProjectile.defaultMovementSpeed;

            this.BoundingBox = new Rectangle(
                (int)this.X,
                (int)this.Y,
                this.TextureWidth,
                this.TextureHeight);

            this.Owner = owner;

        }

        public override void Update(GameTime gameTime)
        {
            if (Collision.CheckForCollisionWithWorldBounds(this) || Collision.CheckForCollisionWithTiles(this.BoundingBox))
            {
                this.Nullify();
            }
            else
            {
                if (this.IsMovingRight)
                {
                     this.X += this.MovementSpeed;
                }
                else
                {
                    this.X -= this.MovementSpeed;
                }

                this.BoundingBoxX = (int)this.X;
                this.BoundingBoxY = (int)this.Y;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.SpriteSheet, this.Position, this.SourceRect, Color.White);
        }

        
    }
}
