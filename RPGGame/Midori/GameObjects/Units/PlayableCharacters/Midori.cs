using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Midori.Core;
using Midori.TextureLoading;
using Midori.GameObjects.Projectiles;
using Midori.GameObjects.Units.Enemies;
using Midori.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Midori.GameObjects.Items;

namespace Midori.GameObjects.Units.PlayableCharacters
{
    public class Midori : PlayableCharacter
    {
        private const int textureWidth = 236;
        private const int textureHeight = 130;
        private const int basicAnimationDelay = 100;
        private const int attackDelay = 100;
        private const int basicAnimationFrameCount = 6;
        private const int attackRangedFrameCount = 17;//4;
        private const float defaultMovementSpeed = 10;
        private const float defaultJumpSpeed = 21;
        private const int defaultHealth = 100;
 
        public Midori(Vector2 position)
            : base()
        {
            this.Position = position;
            this.Health = 100;
            this.DamageRanged = 20;
            this.IsAttackingRanged = false;

            this.BoundingBox = new Rectangle(
                (int)this.X + (Midori.textureWidth - 45) / 2,
                (int)this.Y + 5,
                45,
                110);

            this.SpriteSheet = TextureLoader.MidoriSheet;
            this.TextureWidth = Midori.textureWidth;
            this.TextureHeight = Midori.textureHeight;

            this.BasicAnimationFrameCount = Midori.basicAnimationFrameCount;
            this.Delay = Midori.basicAnimationDelay;

            this.DefaultMovementSpeed = Midori.defaultMovementSpeed;
            this.DefaultJumpSpeed = Midori.defaultJumpSpeed;
            this.MovementSpeed = Midori.defaultMovementSpeed;
            this.JumpSpeed = Midori.defaultJumpSpeed;
        }

        # region Properties
        #endregion

        # region Methods
        // Override Methods
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.Health <= 0)
            {
                this.Nullify();
            }
            else
            {
                InputHandler.HandleInput(gameTime, this);
                this.ManageMovement(gameTime);
                this.ManageAnimation(gameTime);
                this.PickUpItem(gameTime, Collision.GetCollidingItem());
                this.RemoveTimedOutBonuses();
                this.UpdateBoundingBox();
            }
        }

        protected override void UpdateBoundingBox()
        {
            // update bounding box
            this.BoundingBoxX = (int)this.X + (Midori.textureWidth - 45) / 2 + 5;
            this.BoundingBoxY = (int)this.Y + 5;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var healthPercent = (float) this.Health/Midori.defaultHealth;
            var healthBarWidth = (int) (100*healthPercent);
            var healthBar = new Rectangle(((int)this.BoundingBoxX + this.BoundingBox.Width/2) - 50, 
                (int)this.Position.Y - 20,
                healthBarWidth,
                10);
            spriteBatch.Draw(TextureLoader.TheOnePixel, healthBar, null, Color.Red);

            var indent = 5;
            foreach (var item in Engine.PlayerTimedBonuses)
            {
                spriteBatch.Draw(item.SpriteSheet,
                    new Rectangle(healthBar.Right + indent, healthBar.Top, 10, 10),
                    new Rectangle(0, 0, 40, 40),
                    item.Color);
                indent += 12;
            }
            spriteBatch.Draw(this.SpriteSheet, this.Position, this.SourceRect, Color.White);
            //spriteBatch.Draw(TextureLoader.TheOnePixel, new Rectangle((int)(this.BoundingBox.X + this.MovementSpeed), (int)this.BoundingBox.Y, this.BoundingBox.Width, this.BoundingBox.Height), Color.Yellow);
            //spriteBatch.DrawString(TextureLoader.Font, this.FuturePosition.ToString(), new Vector2(this.X, this.Y - 50), Color.Black);
        }

        # region Animaitons
        protected override void ManageAnimation(GameTime gameTime)
        {
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
        }
                
        // Idle and running Animations
        public override void AnimateIdle(GameTime gameTime)
        {
            this.BasicAnimationLogic(gameTime, this.Delay, Midori.basicAnimationFrameCount);
            this.SourceRect = new Rectangle(this.CurrentFrame * Midori.textureWidth, Midori.textureHeight * 0, Midori.textureWidth, Midori.textureHeight);
        }

        public override void AnimateRunningRight(GameTime gameTime)
        {
            this.BasicAnimationLogic(gameTime, this.Delay, Midori.basicAnimationFrameCount);
            this.SourceRect = new Rectangle(this.CurrentFrame * Midori.textureWidth, Midori.textureHeight * 1, Midori.textureWidth, Midori.textureHeight);
        }

        public override void AnimateRunningLeft(GameTime gameTime)
        {
            this.BasicAnimationLogic(gameTime, this.Delay, Midori.basicAnimationFrameCount);
            this.SourceRect = new Rectangle(this.CurrentFrame * Midori.textureWidth, Midori.textureHeight * 2, Midori.textureWidth, Midori.textureHeight);
        }                
        
        // Jumping Animation
        public override void AnimateJumpRight(GameTime gameTime)
        {
            this.BasicAnimationLogic(gameTime, this.Delay, Midori.basicAnimationFrameCount);
            this.SourceRect = new Rectangle(this.CurrentFrame * Midori.textureWidth, Midori.textureHeight * 3, Midori.textureWidth, Midori.textureHeight);
        }

        public override void AnimateJumpLeft(GameTime gameTime)
        {
            this.BasicAnimationLogic(gameTime, this.Delay, Midori.basicAnimationFrameCount);
            this.SourceRect = new Rectangle(this.CurrentFrame * Midori.textureWidth, Midori.textureHeight * 4, Midori.textureWidth, Midori.textureHeight);
        }

        // Falling Animation
        public override void AnimateFallRight(GameTime gameTime)
        {
            this.BasicAnimationLogic(gameTime, this.Delay, Midori.basicAnimationFrameCount);
            this.SourceRect = new Rectangle(this.CurrentFrame * Midori.textureWidth, Midori.textureHeight * 5, Midori.textureWidth, Midori.textureHeight);
        }

        public override void AnimateFallLeft(GameTime gameTime)
        {
            this.BasicAnimationLogic(gameTime, this.Delay, Midori.basicAnimationFrameCount);
            this.SourceRect = new Rectangle(this.CurrentFrame * Midori.textureWidth, Midori.textureHeight * 6, Midori.textureWidth, Midori.textureHeight);
        }
        
        // Ranged Attack
        public void AnimateAttackRangedRight(GameTime gameTime)
        {
            this.Timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.Timer >= Midori.attackDelay)
            {
                this.CurrentFrame++;
                if (this.CurrentFrame == 2)
                {
                    Engine.AddProjectile(
                        new MidoriSmallProjectile(
                            new Vector2(this.BoundingBoxX - 25, this.BoundingBoxY + 15),
                            (!this.IsFacingLeft ? true : false),
                            this));
                }
                else if (this.CurrentFrame == 6)
                {
                    Engine.AddProjectile(
                        new MidoriSmallProjectile(
                            new Vector2(this.BoundingBoxX - 25, this.BoundingBoxY + 15),
                            (!this.IsFacingLeft ? true : false),
                            this));
                }
                else if (this.CurrentFrame == 11)
                {
                    Engine.AddProjectile(new RayParticle(new Vector2(this.X + Midori.textureWidth - 100, this.Y + 32), (!this.IsFacingLeft ? true : false), this));
                }
                else if (this.CurrentFrame >= Midori.attackRangedFrameCount)
                {                    
                    this.CurrentFrame = 0;
                    this.IsAttackingRanged = false;

                    foreach (var proj in Engine.Projectiles)
                    {
                        if (proj is RayParticle)
                        {
                            proj.Nullify();
                        }
                    }
                }

                this.Timer = 0.0;
            }

            this.SourceRect = new Rectangle(this.CurrentFrame * Midori.textureWidth, Midori.textureHeight * 7, Midori.textureWidth, Midori.textureHeight);
        }

        private void AnimateAttackRangedLeft(GameTime gameTime)
        {
            this.Timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.Timer >= Midori.attackDelay)
            {
                this.CurrentFrame++;

                if (this.CurrentFrame == 2)
                {
                    Engine.AddProjectile(
                        new MidoriSmallProjectile(
                            new Vector2(this.BoundingBoxX - 10, this.BoundingBoxY + 22),
                            (!this.IsFacingLeft ? true : false),
                            this));
                }
                else if (this.CurrentFrame == 6)
                {
                    Engine.AddProjectile(
                        new MidoriSmallProjectile(
                            new Vector2(this.BoundingBoxX - 25, this.BoundingBoxY + 22),
                            (!this.IsFacingLeft ? true : false),
                            this));
                }
                else if (this.CurrentFrame == 11)
                {
                    Engine.AddProjectile(new RayParticle(new Vector2(this.X + 80, this.Y + 32), (!this.IsFacingLeft ? true : false), this));
                }
                else if (this.CurrentFrame >= Midori.attackRangedFrameCount)
                {
                    this.CurrentFrame = 0;
                    this.IsAttackingRanged = false;

                    foreach (var proj in Engine.Projectiles)
                    {
                        if (proj is RayParticle)
                        {
                            proj.Nullify();
                        }
                    }
                }

                this.Timer = 0.0;
            }

            this.SourceRect = new Rectangle(this.CurrentFrame * Midori.textureWidth, Midori.textureHeight * 8, Midori.textureWidth, Midori.textureHeight);
        }


        //public void AnimateAttackRangedRight(GameTime gameTime)
        //{
        //    this.Timer += gameTime.ElapsedGameTime.TotalMilliseconds;

        //    if (this.Timer >= this.Delay)
        //    {
        //        this.CurrentFrame++;
        //        if (this.CurrentFrame == 2)
        //        {
        //            Engine.AddProjectile(
        //                new AyaSmallProjectile(
        //                    new Vector2(this.BoundingBoxX - 25, this.BoundingBoxY + 22),
        //                    (!this.IsFacingLeft ? true : false),
        //                    this));
        //        }
        //        else if (this.CurrentFrame >= Midori.attackRangedFrameCount)
        //        {
        //            this.CurrentFrame = 0;
        //            this.IsAttackingRanged = false;
        //        }

        //        this.Timer = 0.0;
        //    }

        //    this.SourceRect = new Rectangle(this.CurrentFrame * Midori.textureWidth, Midori.textureHeight * 7, Midori.textureWidth, Midori.textureHeight);
        //}
        # endregion

#endregion

        

    }
}
