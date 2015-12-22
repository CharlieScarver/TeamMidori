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
        private const int MidoriSmallProjectileTextureWidth = 100;
        private const int MidoriSmallProjectileTextureHeight = 36;
        private const int MidoriSmallProjectileDelay = 100;
        private const int MidoriSmallProjectileBasicAnimationFrameCount = 2;
        private const float MidoriSmallProjectileDefaultMovementSpeed = 22;

        public MidoriSmallProjectile(Vector2 position, bool moveLeft, Unit owner)
            : base()
        {
            this.Position = position;

            this.SpriteSheet = TextureLoader.MidoriSmallProjectileSheet;
            this.TextureWidth = MidoriSmallProjectileTextureWidth;
            this.TextureHeight = MidoriSmallProjectileTextureHeight;

            this.IsFacingLeft = moveLeft;
            if (this.IsFacingLeft)
            { 
                this.SourceRect = new Rectangle(0 * MidoriSmallProjectileTextureWidth, MidoriSmallProjectileTextureHeight * 1, MidoriSmallProjectileTextureWidth, MidoriSmallProjectileTextureHeight);
            }
            else
            {
                this.SourceRect = new Rectangle(0 * MidoriSmallProjectileTextureWidth, MidoriSmallProjectileTextureHeight * 0, MidoriSmallProjectileTextureWidth, MidoriSmallProjectileTextureHeight);
            }

            this.DefaultMovementSpeed = MidoriSmallProjectileDefaultMovementSpeed;
            this.MovementSpeed = this.DefaultMovementSpeed;

            this.BoundingBox = new Rectangle(
                (int)this.X,
                (int)this.Y,
                this.TextureWidth,
                this.TextureHeight);

            this.Owner = owner;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.SpriteSheet, this.Position, this.SourceRect, Color.White);
        }

        
    }
}
