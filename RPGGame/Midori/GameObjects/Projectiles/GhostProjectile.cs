using Microsoft.Xna.Framework;
using Midori.Core;
using Midori.Core.TextureLoading;
using Midori.GameObjects.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.GameObjects.Projectiles
{
    public class GhostProjectile : Projectile
    {
        private const int textureWidth = 100;
        private const int textureHeight = 50;
        private const float defaultMovementSpeed = 22;

        public GhostProjectile(Vector2 position, bool movingRight, Unit owner)
            : base()
        {
            this.Position = position;

            this.SpriteSheet = TextureLoader.GhostProjectile;
            this.TextureWidth = GhostProjectile.textureWidth;
            this.TextureHeight = GhostProjectile.textureHeight;

            this.IsMovingRight = movingRight;
            if (this.IsMovingRight)
            {
                this.SourceRect = new Rectangle(0 * GhostProjectile.textureWidth, GhostProjectile.textureHeight * 1, GhostProjectile.textureWidth, GhostProjectile.textureHeight);
            }
            else
            {
                this.SourceRect = new Rectangle(0 * GhostProjectile.textureWidth, GhostProjectile.textureHeight * 0, GhostProjectile.textureWidth, GhostProjectile.textureHeight);
            }

            this.MovementSpeed = GhostProjectile.defaultMovementSpeed;

            this.BoundingBox = new Rectangle(
                (int)this.X,
                (int)this.Y,
                this.TextureWidth,
                this.TextureHeight);

            this.Owner = owner;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (Collision.CheckForCollisionWithWorldBounds(this) || Collision.CheckForCollisionWithTiles(this.BoundingBox))
            {
                this.Nullify();
            }
            else
            {
                this.ManageMovement(gameTime);

                this.BoundingBoxX = (int)this.X;
                this.BoundingBoxY = (int)this.Y;
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.SpriteSheet, this.Position, this.SourceRect, Color.White);
        }
    }
}
