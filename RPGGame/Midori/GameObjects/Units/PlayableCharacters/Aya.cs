using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Midori.Core;
using Midori.Core.TextureLoading;
using Midori.GameObjects.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.GameObjects.Units.PlayableCharacters
{
    public class Aya : PlayableCharacter
    {
        private const int textureWidth = 236;
        private const int textureHeight = 130;
        private const int delay = 100;
        private const int basicAnimationFrameCount = 6;
        private const int attackRangedFrameCount = 4;
        private const float defaultMovementSpeed = 10;
        private const float defaultJumpSpeed = 21;
 
        public Aya(Vector2 position)
            : base()
        {
            this.Position = position;

            this.BoundingBox = new Rectangle(
                (int)this.X + (Aya.textureWidth - 45) / 2,
                (int)this.Y + 5,
                45,
                110);

            this.SpriteSheet = TextureLoader.AyaSheet;
            this.TextureWidth = Aya.textureWidth;
            this.TextureHeight = Aya.textureHeight;

            this.BasicAnimationFrameCount = Aya.basicAnimationFrameCount;
            this.Delay = Aya.delay;

            this.DefaultMovementSpeed = Aya.defaultMovementSpeed;
            this.DefaultJumpSpeed = Aya.defaultJumpSpeed;
            this.MovementSpeed = Aya.defaultMovementSpeed;
            this.JumpSpeed = Aya.defaultJumpSpeed;
        }

        // Override Methods
        public override void Update(GameTime gameTime)
        {
            InputHandler.HandleInput(gameTime, this);
            ManageMovement(gameTime);

            if (this.IsJumping)
            {
                if (this.IsFacingLeft)
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
                if (this.IsFacingLeft)
                {
                    this.AnimateFallLeft(gameTime);
                }
                else
                {
                    this.AnimateFallRight(gameTime);
                }
            } 
            else if (this.IsAttackingRanged)
            {
                if (this.IsFacingLeft)
                {
                    this.AnimateAttackRangedLeft(gameTime);
                }
                else
                {
                    this.AnimateAttackRangedRight(gameTime);
                }
            }
            else if (this.IsMovingLeft)
            {
                this.AnimateRunningLeft(gameTime);
            }
            else if (this.IsMovingRight)
            {
                this.AnimateRunningRight(gameTime);
            }
            else
            {
                this.AnimateIdle(gameTime);
            }
                        
            // update bounding box
            this.BoundingBoxX = (int)this.X + (Aya.textureWidth - 45) / 2 + 5;
            this.BoundingBoxY = (int)this.Y + 5;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.SpriteSheet, this.Position, this.SourceRect, Color.White);
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

        // Ranged Attack
        public void AnimateAttackRangedRight(GameTime gameTime)
        {
            this.Timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.Timer >= Aya.delay)
            {
                this.CurrentFrame++;
                if (this.CurrentFrame > 4)
                {
                   // throw new Exception("Out of range");
                }
                if (this.CurrentFrame == 2)
                {
                    Engine.AddProjectile(
                        new AyaSmallProjectile(
                            new Vector2(this.BoundingBoxX - 25, this.BoundingBoxY + 22),
                            (this.IsFacingRight ? true : false)));
                }
                else if (this.CurrentFrame >= Aya.attackRangedFrameCount)
                {
                    this.CurrentFrame = 0;
                    this.IsAttackingRanged = false;
                }

                this.Timer = 0.0;
            }

            this.SourceRect = new Rectangle(this.CurrentFrame * Aya.textureWidth, Aya.textureHeight * 7, Aya.textureWidth, Aya.textureHeight);
        }

        private void AnimateAttackRangedLeft(GameTime gameTime)
        {
            this.Timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.Timer >= Aya.delay)
            {
                this.CurrentFrame++;

                if (this.CurrentFrame == 2)
                {
                    Engine.AddProjectile(
                        new AyaSmallProjectile(
                            new Vector2(this.BoundingBoxX - 10, this.BoundingBoxY + 22),
                            (this.IsFacingRight ? true : false)));
                }
                else if (this.CurrentFrame == Aya.attackRangedFrameCount)
                {
                    this.CurrentFrame = 0;
                    this.IsAttackingRanged = false;
                }

                this.Timer = 0.0;
            }

            this.SourceRect = new Rectangle(this.CurrentFrame * Aya.textureWidth, Aya.textureHeight * 8, Aya.textureWidth, Aya.textureHeight);
        }

    }
}
