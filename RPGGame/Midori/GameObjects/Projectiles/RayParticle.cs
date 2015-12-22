using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Midori.Core;
using Midori.GameObjects.Units;
using Midori.TextureLoading;

namespace Midori.GameObjects.Projectiles
{
    public class RayParticle : Projectile
    {
        private const int RayParticleTextureWidth = 137;
        private const int RayParticleTextureHeight = 120;
        private const int RayParticleDelay = 100;
        private const int RayParticleInitialOffsetY = 200;
        private const int RayParticleDefaultExpandSpeed = 100;
        private const int RayParticleMaxExpandSize = 1300;

        public RayParticle(Vector2 position, bool moveLeft, Unit owner)
        {
            this.Position = position;
            
            this.SpriteSheet = TextureLoader.RaySprite;
            this.TextureWidth = RayParticleTextureWidth;
            this.TextureHeight = RayParticleTextureHeight;

            this.SourceRect = new Rectangle(0, 0, this.TextureWidth, this.TextureHeight);

            this.OffsetX = 0;
            this.OffsetY = RayParticleInitialOffsetY;
            this.IsFacingLeft = moveLeft;

            if (this.IsFacingLeft)
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
                    (int)this.Position.X - RayParticleMaxExpandSize, 
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
            if (this.IsFacingLeft)
            {
                this.BoundingBox = new Rectangle((int)this.Position.X - this.OffsetX, (int)this.Y, this.OffsetX, this.OffsetY);
            }
            else
            {
                this.BoundingBox = new Rectangle((int)this.X, (int)this.Y, this.OffsetX, this.OffsetY);
            }

            this.OffsetX += RayParticleDefaultExpandSpeed;
            this.OffsetY -= 5;
            if (this.OffsetX > RayParticleMaxExpandSize)
            {
                this.OffsetX = RayParticleMaxExpandSize;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.IsFacingLeft)
            {
                spriteBatch.Draw(
                texture: this.SpriteSheet,
                sourceRectangle: this.SourceRect,
                destinationRectangle: this.BoundingBox,
                color: Color.White,
                origin: new Vector2(0, 64),
                effects: SpriteEffects.FlipHorizontally);
            }
            else
            {                
                spriteBatch.Draw(
                texture: this.SpriteSheet,
                sourceRectangle: this.SourceRect,
                destinationRectangle: this.BoundingBox,
                color: Color.White,
                origin: new Vector2(0, 64),
                effects: SpriteEffects.None);
            }
        }

    }
}
