using Microsoft.Xna.Framework;
using Midori.Core;
using Midori.TextureLoading;
using Midori.GameObjects.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Midori.GameObjects.Projectiles
{
    public class GhostProjectile : Projectile
    {
        private const int GhostProjectileTextureWidth = 100;
        private const int GhostProjectileTextureHeight = 50;
        private const float GhostProjectileDefaultMovementSpeed = 22;

        public GhostProjectile(Vector2 position, bool moveLeft, Unit owner)
            : base()
        {
            this.Position = position;

            this.SpriteSheet = TextureLoader.GhostProjectileSheet;
            this.TextureWidth = GhostProjectileTextureWidth;
            this.TextureHeight = GhostProjectileTextureHeight;

            this.IsFacingLeft = moveLeft;
            if (this.IsFacingLeft)
            {
                this.SourceRect = new Rectangle(0 * GhostProjectileTextureWidth, GhostProjectileTextureHeight * 0, GhostProjectileTextureWidth, GhostProjectileTextureHeight);
            }
            else
            {
                this.SourceRect = new Rectangle(0 * GhostProjectileTextureWidth, GhostProjectileTextureHeight * 1, GhostProjectileTextureWidth, GhostProjectileTextureHeight);
            }

            this.DefaultMovementSpeed = GhostProjectileDefaultMovementSpeed;
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
