using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public RayParticle(Vector2 position, bool movingRight, Unit owner)
        {
            this.Position = position;
            this.SpriteSheet = TextureLoader.RaySprite;
            this.SourceRect = new Rectangle(0, 0, RayParticle.textureWidth, RayParticle.textureHeight);

            this.BoundingBox = new Rectangle((int)this.X, (int)this.Y, 13 * this.Offset, RayParticle.textureHeight);

            this.Offset = 0;
            this.SpriteSwitch = true;

            this.IsMovingRight = movingRight;
            this.Owner = owner;
            
        }

        private int Offset { get; set; }

        private bool SpriteSwitch { get; set; }

        public override void Update(GameTime gameTime)
        {
            if (this.IsMovingRight)
            {
                if (this.Offset < 140)
                {
                    this.Offset += 20;
                }
            }
            else
            {
                if (this.Offset > -140)
                {
                    this.Offset -= 20;
                }
            }


            this.Timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.Timer >= RayParticle.delay)
            {
                if (this.SpriteSwitch)
                {
                    this.SourceRect = new Rectangle(0, 130, RayParticle.textureWidth, RayParticle.textureHeight);
                    this.SpriteSwitch = false;
                }
                else
                {
                    this.SourceRect = new Rectangle(0, 0, RayParticle.textureWidth, RayParticle.textureHeight);
                    this.SpriteSwitch = true;
                }

                this.Timer = 0.0;
            }

            this.BoundingBox = new Rectangle((int)this.X, (int)this.Y, 13 * Math.Abs(this.Offset), RayParticle.textureHeight);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.SpriteSheet, this.BoundingBox, this.SourceRect, Color.White); //this.Position
        }

    }
}
