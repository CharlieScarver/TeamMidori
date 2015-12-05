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
        private const int basicAnimationFrameCount = 6;
        private const float defaultMovementSpeed = 10;
        private const float defaultJumpSpeed = 21;
 
        public Aya(Vector2 position)
            : base(position, 
            Aya.textureWidth, 
            Aya.textureHeight,
            Aya.basicAnimationFrameCount,
            Aya.defaultMovementSpeed, 
            Aya.defaultJumpSpeed)
        {
            this.SpriteSheet = TextureLoader.AyaSheet;

            this.BoundingBox = new Rectangle(
                (int)this.X + (Aya.textureWidth - 45) / 2,
                (int)this.Y,
                45,
                110);
        }

        private void BasicAnimationLogic(GameTime gameTime)
        {
            this.Timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.Timer >= Aya.delay)
            {
                this.CurrentFrame++;

                if (this.CurrentFrame == Aya.basicAnimationFrameCount)
                {
                    this.CurrentFrame = 0;
                }

                this.Timer = 0.0;
            }
        }

        // Override Methods
        public override void Update(GameTime gameTime)
        {
            InputHandler.HandleInput(gameTime, this);
            ManageMovement(gameTime);

            if (this.IsJumping)
            {
                if (this.isMovingLeft)
                {
                    this.AnimateJumpLeft(gameTime);
                }
                else
                {
                    this.AnimateJumpRight(gameTime);
                }
            }
            else if (this.IsFalling)
            {                
                if (this.isMovingLeft)
                {
                    this.AnimateFallLeft(gameTime);
                }
                else
                {
                    this.AnimateFallRight(gameTime);
                }
            }

            // update bounding box
            this.BoundingBoxX = (int)this.X + (Aya.textureWidth - 45) / 2 + 5;
            this.BoundingBoxY = (int)this.Y + 5;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.SpriteSheet, this.Position, this.SourceRect, Color.White);
        }

        // Idle and running Animations
        public override void AnimateIdle(GameTime gameTime)
        {
            this.BasicAnimationLogic(gameTime);
            this.SourceRect = new Rectangle(this.CurrentFrame * Aya.textureWidth, Aya.textureHeight * 0, Aya.textureWidth, Aya.textureHeight);
        }

        public override void AnimateRunningRight(GameTime gameTime)
        {
            this.BasicAnimationLogic(gameTime);
            this.SourceRect = new Rectangle(this.CurrentFrame * Aya.textureWidth, Aya.textureHeight * 1, Aya.textureWidth, Aya.textureHeight);
        }

        public override void AnimateRunningLeft(GameTime gameTime)
        {
            this.BasicAnimationLogic(gameTime);
            this.SourceRect = new Rectangle(this.CurrentFrame * Aya.textureWidth, Aya.textureHeight * 2, Aya.textureWidth, Aya.textureHeight);
        }                
        
        // Jumping Animation
        public override void AnimateJumpRight(GameTime gameTime)
        {
            this.BasicAnimationLogic(gameTime);
            this.SourceRect = new Rectangle(this.CurrentFrame * Aya.textureWidth, Aya.textureHeight * 3, Aya.textureWidth, Aya.textureHeight);
        }

        public override void AnimateJumpLeft(GameTime gameTime)
        {
            this.BasicAnimationLogic(gameTime);
            this.SourceRect = new Rectangle(this.CurrentFrame * Aya.textureWidth, Aya.textureHeight * 4, Aya.textureWidth, Aya.textureHeight);
        }

        // Falling Animation
        public override void AnimateFallRight(GameTime gameTime)
        {
            this.BasicAnimationLogic(gameTime);
            this.SourceRect = new Rectangle(this.CurrentFrame * Aya.textureWidth, Aya.textureHeight * 5, Aya.textureWidth, Aya.textureHeight);
        }

        public override void AnimateFallLeft(GameTime gameTime)
        {
            this.BasicAnimationLogic(gameTime);
            this.SourceRect = new Rectangle(this.CurrentFrame * Aya.textureWidth, Aya.textureHeight * 6, Aya.textureWidth, Aya.textureHeight);
        }

    }
}
