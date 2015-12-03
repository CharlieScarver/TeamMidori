using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Midori.Core.TextureLoading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.GameObjects.Units
{
    public class TempEnemy : Enemy
    {
        private const int textureWidth = 160;
        private const int textureHeight = 145;
        private const int delay = 200;
        private const int frameCount = 10;
        private const float defaultMovementSpeed = 10;
        private const float defaultJumpSpeed = 30;

        public TempEnemy(Vector2 position)
            : base(position, TempEnemy.textureWidth, TempEnemy.textureHeight, TempEnemy.delay, TempEnemy.frameCount, TempEnemy.defaultMovementSpeed, TempEnemy.defaultJumpSpeed)
        {
            this.SpriteSheet = TextureLoader.TempEnemySheet;
        }

        public override void Update(GameTime gameTime)
        {
            ManageMovement();
            this.AnimateRight(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(
            //    this.SpriteSheet, 
            //    new Rectangle((int)this.Position.X, (int)this.Position.Y, this.TextureWidth, this.TextureHeight), 
            //    this.SourceRect, 
            //    Color.White, 
            //    0.0f, 
            //    new Vector2(0,0), 
            //    SpriteEffects.FlipHorizontally, 
            //    1);
            spriteBatch.Draw(this.SpriteSheet, this.Position, this.SourceRect, Color.White);
        }

        public override void AnimateRight(GameTime gameTime)
        {
            this.Timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.Timer >= this.Delay)
            {
                this.CurrentFrame++;

                if (this.CurrentFrame == this.FrameCount)
                {
                    this.CurrentFrame = 0;
                }

                this.Timer = 0.0;
            }

            this.SourceRect = new Rectangle(this.CurrentFrame * this.TextureWidth, 0, this.TextureWidth, this.TextureHeight);
        }

        public override void AnimateLeft(GameTime gameTime)
        {
            this.Timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.Timer >= this.Delay)
            {
                this.CurrentFrame++;

                if (this.CurrentFrame == this.FrameCount)
                {
                    this.CurrentFrame = 0;
                }
                
                this.Timer = 0.0;
            }

            this.SourceRect = new Rectangle(this.CurrentFrame * this.TextureWidth, 0, this.TextureWidth, this.TextureHeight);
        }

        public override void AnimateIdle()
        {
            throw new NotImplementedException();
        }

    }
}
