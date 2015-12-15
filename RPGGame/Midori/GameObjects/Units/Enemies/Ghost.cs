using Microsoft.Xna.Framework;
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

        public Ghost(Vector2 position)
            : base()
        {
            this.Position = position;
            this.Health = 50;
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

            this.MovementCounter = 0;
            this.AttackCounter = 0;
        }

        # region Properties
        public int MovementCounter { get; set; }
        public int AttackCounter { get; set; }

        #endregion

        # region Methods
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (this.Health < 0)
            {
                this.Nullify();
                Engine.SpawnItem(this.Position);
            }
            else
            {
                this.AI(gameTime);

                this.ManageMovement(gameTime);

                this.UpdateBoundingBox();
            }
        }

        private void AI(GameTime gameTime)
        {
            if (this.MovementCounter <= 100)
            {
                this.IsMovingRight = true;
                this.IsMovingLeft = false;

                if (this.IsAttackingRanged)
                {
                    this.AnimateAttackRangedRight(gameTime);
                    this.IsAttackingRanged = false;
                }
                else
                {
                    this.AnimateRunningRight(gameTime);
                }
            }
            else if (this.MovementCounter > 100 && this.MovementCounter < 200)
            {
                this.IsMovingLeft = true;
                this.IsMovingRight = false;

                if (this.IsAttackingRanged)
                {
                    this.AnimateAttackRangedLeft(gameTime);
                }
                else
                {
                    this.AnimateRunningLeft(gameTime);
                }
            }
            else
            {
                this.MovementCounter = 0;
            }

            if (this.AttackCounter == 30)
            {
                this.AttackCounter = 0;
                this.IsAttackingRanged = false;
            }

            var r = new Random(this.MovementCounter);
            if (this.MovementCounter == 101)
            {
                this.IsAttackingRanged = true;
            }

            this.MovementCounter++;
        }

        protected override void UpdateBoundingBox()
        {
            // update bounding box
            this.BoundingBoxX = (int)this.X + 60;
            this.BoundingBoxY = (int)this.Y + 20;
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.SpriteSheet, this.Position, this.SourceRect, Color.White);
        }

        # region Animations
        public override void AnimateRunningRight(Microsoft.Xna.Framework.GameTime gameTime)
        {
            this.BasicAnimationLogic(gameTime, Ghost.delay, Ghost.basicAnimationFrameCount);
            this.SourceRect = new Rectangle(this.CurrentFrame * Ghost.textureWidth, Ghost.textureHeight * 1, Ghost.textureWidth, Ghost.textureHeight);
        }

        public override void AnimateRunningLeft(Microsoft.Xna.Framework.GameTime gameTime)
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
                            false,
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
