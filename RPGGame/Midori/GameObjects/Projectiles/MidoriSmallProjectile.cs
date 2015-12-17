using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Midori.TextureLoading;
using Midori.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Midori.GameObjects.Units;

namespace Midori.GameObjects.Projectiles
{
    public class MidoriSmallProjectile : Projectile
    {
        private const int textureWidth = 100;
        private const int textureHeight = 36;
        private const int delay = 100;
        private const int basicAnimationFrameCount = 2;
        private const float defaultMovementSpeed = 22;

        public MidoriSmallProjectile(Vector2 position, bool movingRight, Unit owner)
            : base()
        {
            this.Position = position;

            this.SpriteSheet = TextureLoader.SmallMidoriProjectileSheet;
            this.TextureWidth = MidoriSmallProjectile.textureWidth;
            this.TextureHeight = MidoriSmallProjectile.textureHeight;

            this.IsMovingRight = movingRight;
            if (this.IsMovingRight)
            { 
                this.SourceRect = new Rectangle(0 * MidoriSmallProjectile.textureWidth, MidoriSmallProjectile.textureHeight * 0, MidoriSmallProjectile.textureWidth, MidoriSmallProjectile.textureHeight);
            }
            else
            {
                this.SourceRect = new Rectangle(0 * MidoriSmallProjectile.textureWidth, MidoriSmallProjectile.textureHeight * 1, MidoriSmallProjectile.textureWidth, MidoriSmallProjectile.textureHeight);
            }

            this.MovementSpeed = MidoriSmallProjectile.defaultMovementSpeed;

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
