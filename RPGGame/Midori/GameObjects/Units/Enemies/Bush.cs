using Microsoft.Xna.Framework;
using Midori.Interfaces;
using Midori.TextureLoading;

namespace Midori.GameObjects.Units.Enemies
{
    public class Bush : Enemy, IAnimatableIdle, IAnimatableMovable
    {
        private const int BushTextureWidth = 128;
        private const int BushTextureHeight = 128;
        private const int BushDelay = 150;
        private const int BushIdleAnimationFrameCount = 27;
        private const int BushBasicAnimationFrameCount = 11;
        private const int BushAttackMeleeFrameCount = 1111111;
        private const float BushDefaultMovementSpeed = 3;
        private const float BushDefaultJumpSpeed = 21;
        private const int BushDefaultHealth = 150;

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

            this.BasicAnimationFrameCount = BushBasicAnimationFrameCount;
            this.Delay = BushDelay;

            this.DefaultMovementSpeed = BushDefaultMovementSpeed;
            this.DefaultJumpSpeed = BushDefaultJumpSpeed;
            this.MovementSpeed = BushDefaultMovementSpeed;
            this.JumpSpeed = BushDefaultJumpSpeed;
        }


        #region Methods

        public override void Update(GameTime gameTime)
        {
            if (this.Health <= 0)
            {
                this.Nullify();
            }
            else
            { 
                //this.IsMovingLeft = true;
                this.ManageMovement(gameTime);
                this.AnimateIdle(gameTime);
                //this.isFacingLeft = true;
                //this.AnimateRunning(gameTime);
                this.UpdateBoundingBox();
            }
        }

        protected override void UpdateBoundingBox()
        {
            // update bounding box
            this.BoundingBoxX = (int)this.X + (BushTextureWidth / 4) - 5;
            this.BoundingBoxY = (int)this.Y + 10;
        }

        # region Animations
        protected override void ManageAnimation(GameTime gameTime)
        {
            // TODO: Bush Manage Animation
        }

        public void AnimateIdle(GameTime gameTime)
        {
            this.BasicAnimationLogic(gameTime, BushDelay, BushIdleAnimationFrameCount);
            this.SourceRect = new Rectangle(this.CurrentFrame * BushTextureWidth, BushTextureHeight * 0, BushTextureWidth, BushTextureHeight);
        }

        public void AnimateRunning(GameTime gameTime)
        {
            this.BasicAnimationLogic(gameTime, BushDelay, BushBasicAnimationFrameCount);
            if (this.IsFacingLeft)
            {                
                this.SourceRect = new Rectangle(this.CurrentFrame * BushTextureWidth, BushTextureHeight * 1, BushTextureWidth, BushTextureHeight);
            }
            else
            {
                this.SourceRect = new Rectangle(this.CurrentFrame * BushTextureWidth, BushTextureHeight * 1, BushTextureWidth, BushTextureHeight);
            }
        }

        #endregion

        #endregion




        

        
    }
}
