using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Midori.Core;
using Midori.Core.TextureLoading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.GameObjects.Units
{
    public class Aya : PlayableCharacter
    {
        private const int textureWidth = 236;
        private const int textureHeight = 130;
        private const int delay = 100;
        private const int runningAndIdleFrameCount = 6;
        private const int jumpFrameCount = 12;
        private const float defaultMovementSpeed = 10;
        private const float defaultJumpSpeed = 21;
        private int jumpAnimRow;

        public Aya(Vector2 position)
            : base(position, 
            Aya.textureWidth, 
            Aya.textureHeight, 
            Aya.delay, 
            Aya.runningAndIdleFrameCount, 
            Aya.jumpFrameCount,
            Aya.defaultMovementSpeed, 
            Aya.defaultJumpSpeed)
        {
            this.SpriteSheet = TextureLoader.AyaSheet;
            this.JumpAnimRow = 3;
        }

        public int JumpAnimRow
        {
            get { return this.jumpAnimRow; }
            set { this.jumpAnimRow = value; }
        }

        public override void Update(GameTime gameTime)
        {
            InputHandler.HandleInput(gameTime, this);
            ManageMovement(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
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
            
            this.Timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.Timer >= 50) //40
            {
                this.CurrentFrameJump++;

                if (this.CurrentFrameJump == this.JumpFrameCount / 2 && this.JumpAnimRow == 3)
                {
                    if (this.JumpCounter > 1)
                    {
                        this.JumpAnimRow = 5;
                    }
                    else
                    {
                        this.JumpAnimRow = 3;
                    }

                    this.CurrentFrameJump = 0;
                }
                else if (this.CurrentFrameJump == this.JumpFrameCount / 2)
                {
                    this.CurrentFrameJump = 0;
                    if (this.JumpCounter > 1)
                    {
                        this.JumpAnimRow = 5;
                    }
                    else
                    {
                        this.JumpAnimRow = 3;
                    }
                }

                this.Timer = 0.0;
            }

            System.Diagnostics.Debug.WriteLine(this.CurrentFrameJump);
            this.SourceRect = new Rectangle(this.CurrentFrameJump * this.TextureWidth, this.TextureHeight * this.JumpAnimRow, this.TextureWidth, this.TextureHeight);
        }

        public override void AnimateJumpLeft(GameTime gameTime)
        {
            this.Timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.Timer >= 50) //40
            {
                this.CurrentFrameJump++;

                if (this.CurrentFrameJump == this.JumpFrameCount / 2 && this.JumpAnimRow == 4)
                {                    
                    this.CurrentFrameJump = 0;

                    if (this.JumpCounter > 1)
                    {
                        this.JumpAnimRow = 6;
                    }
                    else
                    {
                        this.JumpAnimRow = 4;
                    }
                }
                else if (this.CurrentFrameJump == this.JumpFrameCount / 2)
                {
                    this.CurrentFrameJump = 0;

                    if (this.JumpCounter > 1)
                    {
                        this.JumpAnimRow = 6;
                    }
                    else
                    {
                        this.JumpAnimRow = 4;
                    }
                }

                this.Timer = 0.0;
            }

            this.SourceRect = new Rectangle(this.CurrentFrameJump * this.TextureWidth, this.TextureHeight * this.JumpAnimRow, this.TextureWidth, this.TextureHeight);
        }

    }
}
