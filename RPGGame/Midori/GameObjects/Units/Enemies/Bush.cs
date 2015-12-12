using Microsoft.Xna.Framework;
using Midori.Core.TextureLoading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.GameObjects.Units.Enemies
{
    public class Bush : Enemy
    {
        private const int textureWidth = 128;
        private const int textureHeight = 128;
        private const int delay = 100;
        private const int basicAnimationFrameCount = 11;
        private const int attackRangedFrameCount = 4;
        private const int attackMeleeFrameCount = 1111111;
        private const float defaultMovementSpeed = 3;
        private const float defaultJumpSpeed = 21;

        public Bush(Vector2 position)
            : base()
        {
            this.Position = position;
            this.Health = 50;

            this.BoundingBox = new Rectangle(
                (int)this.X + (Bush.textureWidth / 4) - 5,
                (int)this.Y + 10,
                (128 / 2) + 10,
                86);

            this.SpriteSheet = TextureLoader.BushSheet;
            this.TextureWidth = Bush.textureWidth;
            this.TextureHeight = Bush.textureHeight;

            this.BasicAnimationFrameCount = Bush.basicAnimationFrameCount;
            this.Delay = Bush.delay;

            this.DefaultMovementSpeed = Bush.defaultMovementSpeed;
            this.DefaultJumpSpeed = Bush.defaultJumpSpeed;
            this.MovementSpeed = Bush.defaultMovementSpeed;
            this.JumpSpeed = Bush.defaultJumpSpeed;
        }


        #region Methods

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (this.Health < 0)
            {
                this.Nullify();
            }
            else
            { 
                this.IsMovingLeft = true;
                this.ManageMovement(gameTime);
                this.AnimateRunningLeft(gameTime);
                this.UpdateBoundingBox();
            }
        }

        protected override void UpdateBoundingBox()
        {
            // update bounding box
            this.BoundingBoxX = (int)this.X + (Bush.textureWidth / 4) - 5;
            this.BoundingBoxY = (int)this.Y + 10;
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.SpriteSheet, this.Position, this.SourceRect, Color.Yellow);
        }

        # region Animations
        public override void AnimateRunningRight(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void AnimateRunningLeft(Microsoft.Xna.Framework.GameTime gameTime)
        {
            this.BasicAnimationLogic(gameTime, Bush.delay, Bush.basicAnimationFrameCount);
            this.SourceRect = new Rectangle(this.CurrentFrame * Bush.textureWidth, Bush.textureHeight * 1, Bush.textureWidth, Bush.textureHeight);
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

        #endregion

        #endregion



    }
}
