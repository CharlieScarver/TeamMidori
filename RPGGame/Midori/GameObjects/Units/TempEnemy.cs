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
        private const int textureWidth = 236; //160
        private const int textureHeight = 130; //145
        private const int delay = 100;
        private const int runningAndIdleFrameCount = 6;
        private const int jumpFrameCount = 12;
        private const float defaultMovementSpeed = 10;
        private const float defaultJumpSpeed = 21;

        private int animCounter;

        public TempEnemy(Vector2 position)
            : base(position, TempEnemy.textureWidth, TempEnemy.textureHeight, TempEnemy.delay, TempEnemy.runningAndIdleFrameCount, TempEnemy.jumpFrameCount, TempEnemy.defaultMovementSpeed, TempEnemy.defaultJumpSpeed)
        {
            //this.SpriteSheet = TextureLoader.TempEnemySheet;
            this.SpriteSheet = TextureLoader.AyaSheet;

            this.BoundingBox = new Rectangle(
                (int)this.X + (textureWidth / 4),
                (int)this.Y + 15,
                textureWidth / 2,
                textureHeight - 15);

            this.animCounter = 0;
        }

        public override void Update(GameTime gameTime)
        {
            ManageMovement(gameTime);

            if (this.animCounter >= 0 && this.animCounter <= 150)
            {
                this.AnimateRight(gameTime);
                this.X += 6;
            }
            else if (this.animCounter >= 150 && this.animCounter <= 300)
            {
                this.AnimateLeft(gameTime);
                this.X -= 6;
            }
            else
            {
                this.AnimateIdle(gameTime);
            }

            this.animCounter++;

            if (this.animCounter > 400)
            {
                this.animCounter = 0;
            }
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

        public override void AnimateIdle(GameTime gameTime)
        {
            this.RunningAndIdleAnimationLogic(gameTime);

            this.SourceRect = new Rectangle(this.CurrentFrameRunningAndIdle * this.TextureWidth, this.TextureHeight * 0, this.TextureWidth, this.TextureHeight);
        }

        public override void AnimateRight(GameTime gameTime)
        {
            this.RunningAndIdleAnimationLogic(gameTime);

            this.SourceRect = new Rectangle(this.CurrentFrameRunningAndIdle * this.TextureWidth, this.TextureHeight * 1, this.TextureWidth, this.TextureHeight);
        }

        public override void AnimateLeft(GameTime gameTime)
        {
            this.RunningAndIdleAnimationLogic(gameTime);

            this.SourceRect = new Rectangle(this.CurrentFrameRunningAndIdle * this.TextureWidth, this.TextureHeight * 2, this.TextureWidth, this.TextureHeight);
        }

        private void RunningAndIdleAnimationLogic(GameTime gameTime)
        {
            this.Timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.Timer >= this.Delay)
            {
                this.CurrentFrameRunningAndIdle++;

                if (this.CurrentFrameRunningAndIdle == this.RunningAndIdleFrameCount)
                {
                    this.CurrentFrameRunningAndIdle = 0;
                }

                this.Timer = 0.0;
            }
        }

        public override void AnimateJumpRight(GameTime gameTime)
        {
            JumpAnimationLogic(gameTime);

            this.SourceRect = new Rectangle(this.JumpCounter * this.TextureWidth, this.TextureHeight * 3, this.TextureWidth, this.TextureHeight);
        }

        public override void AnimateJumpLeft(GameTime gameTime)
        {
            JumpAnimationLogic(gameTime);

            this.SourceRect = new Rectangle(this.JumpCounter * this.TextureWidth, this.TextureHeight * 3, this.TextureWidth, this.TextureHeight);
        }


        private void JumpAnimationLogic(GameTime gameTime)
        {
            this.Timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.Timer >= this.Delay)
            {
                this.CurrentFrameJump++;

                if (this.CurrentFrameJump == this.JumpFrameCount)
                {
                    this.CurrentFrameJump = 0;
                }

                this.Timer = 0.0;
            }
        }
    }
}
