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
        private const int textureWidth = 137;
        private const int textureHeight = 120;
        private const int delay = 100;
        private const int MaxOffset = 1300;

        public RayParticle(Vector2 position, bool movingRight, Unit owner)
        {
            this.Position = position;
            
            this.SpriteSheet = TextureLoader.RaySprite;
            this.TextureWidth = RayParticle.textureWidth;
            this.TextureHeight = RayParticle.textureHeight;

            this.SourceRect = new Rectangle(0, 0, this.TextureWidth, this.TextureHeight);

            this.OffsetX = 0;
            this.OffsetY = 200;
            this.IsMovingRight = movingRight;

            if (this.IsMovingRight)
            {
                this.BoundingBox = new Rectangle(
                    (int)this.X, 
                    (int)this.Y, 
                    this.OffsetX, 
                    this.OffsetY);
            }
            else
            {
                this.BoundingBox = new Rectangle(
                    (int)this.Position.X - RayParticle.MaxOffset, 
                    (int)this.Position.Y, 
                    this.OffsetX, 
                    this.OffsetY);
            }
                        
            this.Owner = owner;

        }

        private int OffsetY { get; set; }

        private int OffsetX { get; set; }

        public override void Update(GameTime gameTime)
        {            
            if (!this.IsMovingRight)
            {
                this.BoundingBox = new Rectangle((int)this.Position.X - this.OffsetX, (int)this.Y, this.OffsetX, this.OffsetY);
            }
            else
            {
                this.BoundingBox = new Rectangle((int)this.X, (int)this.Y, this.OffsetX, this.OffsetY);
            }

            this.OffsetX += 100;
            this.OffsetY -= 5;
            if (this.OffsetX > RayParticle.MaxOffset)
            {
                this.OffsetX = RayParticle.MaxOffset;
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
