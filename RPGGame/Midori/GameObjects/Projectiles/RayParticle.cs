using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Midori.Core;
using Midori.GameObjects.Units;
using Midori.TextureLoading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.GameObjects.Projectiles
{
    public class RayParticle : Projectile
    {
        private const int textureWidth = 4;
        private const int textureHeight = 130;
        private const int delay = 100;
        private const int MaxOffset = 1100;
        public RayParticle(Vector2 position, bool movingRight, Unit owner)
        {
            this.Position = position;
            this.SpriteSheet = TextureLoader.RaySprite;
            this.SourceRect = new Rectangle(0, 0, RayParticle.textureWidth, RayParticle.textureHeight);
            if (movingRight)
            {
                this.BoundingBox = new Rectangle((int)this.X, (int)this.Y, 13 * this.Offset, RayParticle.textureHeight);
            }
            else
            {
                this.BoundingBox = new Rectangle((int)this.Position.X - RayParticle.MaxOffset, (int)this.Position.Y, 0, this.OffsetY);
            }

            this.Offset = 0;
            this.OffsetY = 128;
            this.SpriteSwitch = true;
            this.Rotation = 0;
            this.IsMovingRight = movingRight;
            this.Owner = owner;
            this.CurrentFrame = 0;


        }
        private int OffsetY { get; set; }

        private int Offset { get; set; }

        private float Rotation { get; set; }
        private bool SpriteSwitch { get; set; }

        public override void Update(GameTime gameTime)
        {
            this.Timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.Timer >= RayParticle.delay)
            {
                CurrentFrame++;
                if (CurrentFrame > 0)
                {
                    this.CurrentFrame = 0;
                }
                this.Timer = 0.0;
            }

            if (!this.IsMovingRight)
            {
                this.BoundingBox = new Rectangle((int)this.Position.X - this.Offset, (int)this.Y, this.Offset, this.OffsetY);
            }
            else
            {
                this.BoundingBox = new Rectangle((int)this.X, (int)this.Y, this.Offset, this.OffsetY);
            }
            this.SourceRect = new Rectangle(this.CurrentFrame * 128, 0, 128, 128);

            this.Offset += 100;
            this.OffsetY -= 2;
            if (this.Offset > RayParticle.MaxOffset)
            {
                this.Offset = RayParticle.MaxOffset;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.IsMovingRight)
            {
                spriteBatch.Draw(
                texture: this.SpriteSheet,
                sourceRectangle: this.SourceRect,
                destinationRectangle: this.BoundingBox,
                color: Color.White,
                origin: new Vector2(0, 64),
                effects: SpriteEffects.None);
            }
            else
            {
                spriteBatch.Draw(
                texture: this.SpriteSheet,
                sourceRectangle: this.SourceRect,
                destinationRectangle: this.BoundingBox,
                color: Color.White,
                origin: new Vector2(0, 64),
                effects: SpriteEffects.FlipHorizontally);
            }
        }

    }
}
