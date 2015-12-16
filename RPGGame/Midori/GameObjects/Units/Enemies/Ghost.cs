using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Midori.Core;
using Midori.Core.TextureLoading;
using Midori.GameObjects.Projectiles;
using Midori.GameObjects.Units.PlayableCharacters;
using Midori.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.GameObjects.Units.Enemies
{
    public class Ghost : Enemy
    {
        private const int textureWidth = 128;
        private const int textureHeight = 128;
        private const int delay = 160;
        private const int basicAnimationFrameCount = 2;//11;
        private const int attackRangedFrameCount = 4;
        private const int attackMeleeFrameCount = 1111111;
        private const float defaultMovementSpeed = 3;
        private const float defaultJumpSpeed = 21;
        private const int defaultHealth = 50;

        public Ghost(Vector2 position)
            : base()
        {
            this.Position = position;

            this.MaxHealth = Ghost.defaultHealth;
            this.Health = this.MaxHealth;

            this.DamageRanged = 15;
            this.IsAttackingRanged = false;

            this.BoundingBox = new Rectangle(
                (int)this.X + 60,
                (int)this.Y + 20,
                34,
                86);

            this.SpriteSheet = TextureLoader.GhostSheet;
            this.TextureWidth = Ghost.textureWidth;
            this.TextureHeight = Ghost.textureHeight;

            this.BasicAnimationFrameCount = Ghost.basicAnimationFrameCount;
            this.Delay = Ghost.delay;

            this.DefaultMovementSpeed = Ghost.defaultMovementSpeed;
            this.DefaultJumpSpeed = Ghost.defaultJumpSpeed;
            this.MovementSpeed = Ghost.defaultMovementSpeed;
            this.JumpSpeed = Ghost.defaultJumpSpeed;

            this.AttackCounter = 0;

            this.AttackTimer = new CountDownTimer();
            this.MovementTimer = new CountDownTimer();
        }

        # region Properties
        public int AttackCounter { get; set; }

        protected CountDownTimer AttackTimer { get; set; }

        protected CountDownTimer MovementTimer { get; set; }

        #endregion

        # region Methods
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (this.Health <= 0)
            {
                this.Nullify();
                Engine.SpawnItem(this.Position);
            }
            else
            {
                                               
                this.AI(gameTime);

                this.ManageMovement(gameTime);

                this.ManageAnimation(gameTime);

                this.UpdateBoundingBox();
            }
        }

        private void AI(GameTime gameTime)
        {
            var r = new Random((int)gameTime.TotalGameTime.TotalMilliseconds * 1000000 * this.Id);

            // Movement

            if (this.MovementTimer.CheckTimer(gameTime))
            {
                if (r.Next(0,2) == 1)
                {
                    
                    this.IsMovingLeft = false;
                    this.IsFacingLeft = false;
                    this.IsMovingRight = true;
                    this.AnimateRunningRight(gameTime);
                }
                else
                {
                    
                    this.IsMovingRight = false;
                    this.IsFacingLeft = true;
                    this.IsMovingLeft = true;
                    this.AnimateRunningLeft(gameTime);
                }


                this.MovementTimer.SetTimer(gameTime, r.Next(1, 4));
            }


            // Attacking

            if (this.AttackCounter == 30)
            {
                this.AttackCounter = 0;
                this.IsAttackingRanged = false;
            }

            if (this.AttackTimer.CheckTimer(gameTime))
            {
                this.IsAttackingRanged = true;
                this.AttackTimer.SetTimer(gameTime, 2);
            }

        }

        protected override void ManageAnimation(GameTime gameTime)
        {
            if (this.IsAttackingRanged)
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
            else if (this.IsMovingRight)
            {
                this.AnimateRunningRight(gameTime);
            }
            else
            {
                this.AnimateRunningLeft(gameTime);
            }
        }

        protected override void UpdateBoundingBox()
        {
            // update bounding box
            this.BoundingBoxX = (int)this.X + 60;
            this.BoundingBoxY = (int)this.Y + 20;
        }

        # region Animations
        
        public override void AnimateRunningRight(GameTime gameTime)
        {
            this.BasicAnimationLogic(gameTime, Ghost.delay, Ghost.basicAnimationFrameCount);
            this.SourceRect = new Rectangle(this.CurrentFrame * Ghost.textureWidth, Ghost.textureHeight * 1, Ghost.textureWidth, Ghost.textureHeight);
        }

        public override void AnimateRunningLeft(GameTime gameTime)
        {
            this.BasicAnimationLogic(gameTime, Ghost.delay, Ghost.basicAnimationFrameCount);
            this.SourceRect = new Rectangle(this.CurrentFrame * Ghost.textureWidth, Ghost.textureHeight * 0, Ghost.textureWidth, Ghost.textureHeight);
        }

        public override void AnimateIdle(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void AnimateJumpRight(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void AnimateJumpLeft(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void AnimateFallRight(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void AnimateFallLeft(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        // Ranged Attack
        public void AnimateAttackRangedRight(GameTime gameTime)
        {
            this.AttackCounter++;
            if (this.AttackCounter == 10)
            {
                Engine.AddProjectile(
                        new GhostProjectile(
                            new Vector2(this.BoundingBoxX - 10, this.BoundingBoxY + 10),
                            true,
                            this));
            }
            this.SourceRect = new Rectangle(0 * Ghost.textureWidth, Ghost.textureHeight * 3, Ghost.textureWidth, Ghost.textureHeight);
        }

        private void AnimateAttackRangedLeft(GameTime gameTime)
        {
            this.AttackCounter++;
            if (this.AttackCounter == 10)
            {
                Engine.AddProjectile(
                        new GhostProjectile(
                            new Vector2(this.BoundingBoxX - 50, this.BoundingBoxY + 10),
                            false,
                            this));
            }

            this.SourceRect = new Rectangle(0 * Ghost.textureWidth, Ghost.textureHeight * 2, Ghost.textureWidth, Ghost.textureHeight);
        }

        # endregion

        #endregion


        
    }
}
