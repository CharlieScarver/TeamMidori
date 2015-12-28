using Microsoft.Xna.Framework;
using Midori.Core;
using Midori.Interfaces;
using Midori.TextureLoading;
using Midori.Timer;
using System;

namespace Midori.GameObjects.Units.Enemies
{
    public class Bush : Enemy, IAnimatableIdle, IAnimatableMovable
    {
        private const int BushTextureWidth = 128;
        private const int BushTextureHeight = 128;
        private const int BushDelay = 150;
        private const int BushIdleAnimationFrameCount = 27;
        private const int BushRunningAnimationFrameCount = 5;
        private const int BushAttackMeleeAndAwakingFrameCount = 8;
        private const float BushDefaultMovementSpeed = 3;
        private const float BushDefaultJumpSpeed = 21;
        private const int BushDefaultHealth = 150;
        private const int BushSight = 150;

        public Bush(Vector2 position)
            : base()
        {
            this.Position = position;
            this.MaxHealth = BushDefaultHealth;
            this.Health = this.MaxHealth;

            this.BoundingBox = new Rectangle(
                (int)this.X + (BushTextureWidth / 4) - 5,
                (int)this.Y + 10,
                (128 / 2) + 10,
                86);

            this.SpriteSheet = TextureLoader.BushSheet;
            this.TextureWidth = BushTextureWidth;
            this.TextureHeight = BushTextureHeight;

            //this.BasicAnimationFrameCount = BushBasicAnimationFrameCount;
            this.Delay = BushDelay;

            this.DefaultMovementSpeed = BushDefaultMovementSpeed;
            this.DefaultJumpSpeed = BushDefaultJumpSpeed;
            this.MovementSpeed = BushDefaultMovementSpeed;
            this.JumpSpeed = BushDefaultJumpSpeed;

            this.AttackCounter = 0;

            this.AttackTimer = new CountDownTimer();
            this.MovementTimer = new CountDownTimer();
            this.Randomizer = new Random(1000000 * this.Id);

            this.Sight = BushSight;
            this.SightRect = new Rectangle(
                (int) this.X - this.Sight,
                (int) this.Y,
                this.BoundingBox.Width + (2*this.Sight),
                this.BoundingBox.Height);

            this.IsAwake = false;
            this.IsAwaking = false;
        }

        # region Properties
        protected Random Randomizer { get; set; }

        protected int AttackCounter { get; set; }

        protected CountDownTimer AttackTimer { get; set; }

        protected CountDownTimer MovementTimer { get; set; }

        // New
        protected bool IsAwake { get; set; }

        protected bool IsAwaking { get; set; }

        #endregion


        #region Methods

        public override void Update(GameTime gameTime)
        {
            if (this.Health <= 0)
            {
                this.Nullify();
            }
            else
            {
                this.AI(gameTime);
                
                this.ManageMovement(gameTime);

                this.ManageAnimation(gameTime);

                this.UpdateSightRect();
                this.UpdateBoundingBox();
            }
        }

        private void AI(GameTime gameTime)
        {
            if (this.IsAwake)
            {
                // Movement
                if (this.MovementTimer.CheckTimer(gameTime))
                {
                    var nextRandomNumber = this.Randomizer.Next(0, 2);

                    if (nextRandomNumber == 0)
                    {
                        this.IsMovingLeft = false;                        
                        this.IsMovingRight = true;

                        this.IsFacingLeft = false;
                    }
                    else if (nextRandomNumber == 1)
                    {
                        this.IsMovingRight = false;                        
                        this.IsMovingLeft = true;

                        this.IsFacingLeft = true;
                    }
                    //else
                    //{
                    //    this.IsMovingLeft = false;
                    //    this.IsMovingRight = false;
                    //}


                    this.MovementTimer.SetTimer(gameTime, this.Randomizer.Next(1, 4));
                }
            }
            else if (!this.IsAwaking)
            {
                if (Collision.CheckForCollisionWithSightRect(Engine.Player, this))
                {
                    this.IsAwaking = true;
                    this.ResetAnimation();
                }
            }


        }

        protected override void UpdateBoundingBox()
        {
            // update bounding box
            this.BoundingBoxX = (int)this.X + (BushTextureWidth / 4) - 5;
            this.BoundingBoxY = (int)this.Y + 10;
        }

        protected void UpdateSightRect()
        {
            this.SightRect = new Rectangle(
                (int)this.X - this.Sight,
                (int)this.Y,
                this.BoundingBox.Width + (2 * this.Sight),
                this.BoundingBox.Height);
        }

        # region Animations
        protected override void ManageAnimation(GameTime gameTime)
        {
            if (this.IsAwaking)
            {
                this.AnimateAwaking(gameTime);
            }
            else if (this.IsMovingRight || this.IsMovingLeft)
            {
                this.AnimateRunning(gameTime);
            }
            else
            {
                this.AnimateIdle(gameTime);
            }
        }

        public void AnimateIdle(GameTime gameTime)
        {
            this.BasicAnimationLogic(gameTime, BushDelay, BushIdleAnimationFrameCount);
            this.SourceRect = new Rectangle(this.CurrentFrame * BushTextureWidth, BushTextureHeight * 0, BushTextureWidth, BushTextureHeight);
        }

        public void AnimateRunning(GameTime gameTime)
        {
            this.BasicAnimationLogic(gameTime, BushDelay, BushRunningAnimationFrameCount);
            if (this.IsFacingLeft)
            {                
                this.SourceRect = new Rectangle(this.CurrentFrame * BushTextureWidth, BushTextureHeight * 1, BushTextureWidth, BushTextureHeight);
            }
            else
            {
                this.SourceRect = new Rectangle((this.CurrentFrame + 12) * BushTextureWidth, BushTextureHeight * 1, BushTextureWidth, BushTextureHeight);
            }
        }

        public void AnimateAwaking(GameTime gameTime)
        {
            if (this.IsFacingLeft)
            {
                this.SourceRect = new Rectangle(this.CurrentFrame * BushTextureWidth, BushTextureHeight * 5, BushTextureWidth, BushTextureHeight);
            }
            else
            {
                this.SourceRect = new Rectangle((this.CurrentFrame + 12) * BushTextureWidth, BushTextureHeight * 5, BushTextureWidth, BushTextureHeight);
            }

            this.Timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.Timer >= BushDelay)
            {
                this.CurrentFrame++;

                if (this.CurrentFrame >= BushAttackMeleeAndAwakingFrameCount)
                {
                    this.CurrentFrame = 0;
                    this.IsAwaking = false;
                    this.IsAwake = true;
                }

                this.Timer = 0.0;
            }            
        }
        #endregion

        #endregion




        

        
    }
}
