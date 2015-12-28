using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Midori.Core;
using Midori.TextureLoading;
using Midori.GameObjects.Projectiles;
using Midori.GameObjects.Units.PlayableCharacters;
using Midori.Interfaces;
using System;
using Midori.Timer;

namespace Midori.GameObjects.Units.Enemies
{
    public class Ghost : Enemy, IAnimatableMovable, IRangedAttacker, IAnimatableRangedAttacker
    {
        private const int GhostTextureWidth = 128;
        private const int GhostTextureHeight = 128;
        private const int GhostDelay = 160;
        private const int GhostBasicAnimationFrameCount = 2;
        private const int GhostAttackRangedFrameCount = 4;
        private const int GhostAttackMeleeFrameCount = 0;
        private const float GhostDefaultMovementSpeed = 3;
        private const float GhostDefaultJumpSpeed = 21;
        private const int GhostDefaultHealth = 50;

        public Ghost(Vector2 position)
            : base()
        {
            this.Position = position;

            this.MaxHealth = GhostDefaultHealth;
            this.Health = this.MaxHealth;

            this.DamageRanged = 15;
            this.IsAttackingRanged = false;

            this.BoundingBox = new Rectangle(
                (int)this.X + 60,
                (int)this.Y + 20,
                34,
                86);

            this.SpriteSheet = TextureLoader.GhostSheet;
            this.TextureWidth = GhostTextureWidth;
            this.TextureHeight = GhostTextureHeight;

            this.BasicAnimationFrameCount = GhostBasicAnimationFrameCount;
            this.Delay = GhostDelay;

            this.DefaultMovementSpeed = GhostDefaultMovementSpeed;
            this.DefaultJumpSpeed = GhostDefaultJumpSpeed;
            this.MovementSpeed = GhostDefaultMovementSpeed;
            this.JumpSpeed = GhostDefaultJumpSpeed;

            this.AttackCounter = 0;

            this.AttackTimer = new CountDownTimer();
            this.MovementTimer = new CountDownTimer();
            this.Randomizer = new Random(1000000 * this.Id);
        }

        # region Properties
        protected Random Randomizer { get; set; }

        protected int AttackCounter { get; set; }

        protected CountDownTimer AttackTimer { get; set; }

        protected CountDownTimer MovementTimer { get; set; }

        #endregion

        # region Methods
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (this.Health <= 0)
            {
                this.Nullify();
                // Engine.SpawnItem(new Vector2(this.X + this.TextureWidth / 2, this.Y));
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

            // Movement

            if (this.MovementTimer.CheckTimer(gameTime))
            {
                if (this.Randomizer.Next(0, 2) == 1)
                {
                    this.IsMovingLeft = false;
                    this.IsFacingLeft = false;
                    this.IsMovingRight = true;
                }
                else
                {
                    this.IsMovingRight = false;
                    this.IsFacingLeft = true;
                    this.IsMovingLeft = true;
                }

                this.AnimateRunning(gameTime);
                this.MovementTimer.SetTimer(gameTime, this.Randomizer.Next(1, 4));
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
                this.AttackTimer.SetTimer(gameTime, this.Randomizer.Next(2,5));
            }

        }
                
        protected override void UpdateBoundingBox()
        {
            // update bounding box
            this.BoundingBoxX = (int)this.X + 60;
            this.BoundingBoxY = (int)this.Y + 20;
        }

        # region Animations
        protected override void ManageAnimation(GameTime gameTime)
        {
            if (this.IsAttackingRanged)
            {                
                this.AnimateAttackRanged(gameTime);
            }
            else
            {
                this.AnimateRunning(gameTime);
            }
        }

        public void AnimateRunning(GameTime gameTime)
        {
            this.BasicAnimationLogic(gameTime, GhostDelay, GhostBasicAnimationFrameCount);
            if (this.IsFacingLeft)
            {
                this.SourceRect = new Rectangle(this.CurrentFrame * GhostTextureWidth, GhostTextureHeight * 0, GhostTextureWidth, GhostTextureHeight);                
            }
            else
            {
                this.SourceRect = new Rectangle(this.CurrentFrame * GhostTextureWidth, GhostTextureHeight * 1, GhostTextureWidth, GhostTextureHeight);
            }
        }

        // Ranged Attack
        public void AnimateAttackRanged(GameTime gameTime)
        {
            this.AttackCounter++;
            if (this.AttackCounter == 10)
            {
                if (this.IsFacingLeft)
                {
                    Engine.AddProjectile(
                        new GhostProjectile(
                            new Vector2(this.BoundingBoxX - 50, this.BoundingBoxY + 10),
                            true,
                            this));
                }
                else
                {
                    Engine.AddProjectile(
                        new GhostProjectile(
                            new Vector2(this.BoundingBoxX - 10, this.BoundingBoxY + 10),
                            false,
                            this));
                }                
            }

            if (this.IsFacingLeft)
            {
                this.SourceRect = new Rectangle(0 * GhostTextureWidth, GhostTextureHeight * 2, GhostTextureWidth, GhostTextureHeight);
            }
            else
            {
                this.SourceRect = new Rectangle(0 * GhostTextureWidth, GhostTextureHeight * 3, GhostTextureWidth, GhostTextureHeight);
            }
            
        }

        # endregion

        #endregion




       
    }
}
