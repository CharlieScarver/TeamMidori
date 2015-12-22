using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Midori.Core;
using Midori.TextureLoading;
using Midori.GameObjects.Projectiles;
using Midori.GameObjects.Units.Enemies;
using Midori.Interfaces;
using Midori.GameObjects.Items;
using Midori.Input;

namespace Midori.GameObjects.Units.PlayableCharacters
{
    public class Midori : PlayableCharacter, IRangedAttacker, IAnimatableRangedAttacker
    {
        private const int MidoriTextureWidth = 236;
        private const int MidoriTextureHeight = 130;
        private const int MidoriBasicAnimationDelay = 100;
        private const int MidoriAttackDelay = 100;
        private const int MidoriBasicAnimationFrameCount = 6;
        private const int MidoriAttackRangedFrameCount = 4;
        private const int MidoriRayComboFrameCount = 17;
        private const float MidoriDefaultMovementSpeed = 10;
        private const float MidoriDefaultJumpSpeed = 21;
        private const int MidoriDefaultHealth = 100;
 
        public Midori(Vector2 position)
            : base()
        {
            this.Position = position;
            this.MaxHealth = MidoriDefaultHealth;
            this.Health = this.MaxHealth;
            this.DamageRanged = 20;
            this.IsAttackingRanged = false;

            this.BoundingBox = new Rectangle(
                (int)this.X + (MidoriTextureWidth - 45) / 2,
                (int)this.Y + 5,
                45,
                110);

            this.SpriteSheet = TextureLoader.MidoriSheet;
            this.TextureWidth = MidoriTextureWidth;
            this.TextureHeight = MidoriTextureHeight;

            this.BasicAnimationFrameCount = MidoriBasicAnimationFrameCount;
            this.Delay = MidoriBasicAnimationDelay;

            this.DefaultMovementSpeed = MidoriDefaultMovementSpeed;
            this.DefaultJumpSpeed = MidoriDefaultJumpSpeed;
            this.MovementSpeed = MidoriDefaultMovementSpeed;
            this.JumpSpeed = MidoriDefaultJumpSpeed;
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
            this.BoundingBoxX = (int)this.X + (MidoriTextureWidth - 45) / 2 + 5;
            this.BoundingBoxY = (int)this.Y + 5;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var healthPercent = (float) this.Health / this.MaxHealth;
            var healthBarWidth = (int) (100 * healthPercent);
            var healthBar = new Rectangle(((int)this.BoundingBoxX + this.BoundingBox.Width/2) - 50, 
                (int)this.Position.Y - 20,
                healthBarWidth,
                10);
            var healthBarEmpty = new Rectangle(
                this.BoundingBox.X + (this.BoundingBox.Width / 2) - 50,
                (int)this.Position.Y - 20,
                100,
                10);

            spriteBatch.Draw(TextureLoader.TheOnePixel, healthBarEmpty, Color.LightBlue * 0.8f);
            spriteBatch.Draw(TextureLoader.TheOnePixel, healthBar, null, Color.Red);

            var indent = 5;
            foreach (var item in Engine.PlayerTimedBonuses)
            {
                spriteBatch.Draw(item.SpriteSheet,
                    new Rectangle(healthBarEmpty.Right + indent, healthBarEmpty.Top, 10, 10),
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
                this.AnimateJump(gameTime);
            }
            else if (this.IsFalling)
            {
                this.AnimateFall(gameTime);                
            } 
            else if (this.IsAttackingRanged)
            {
                this.AnimateAttackRanged(gameTime);
            }
            else if (this.IsMovingLeft || this.IsMovingRight)
            {                
                this.AnimateRunning(gameTime);
            }
            else
            {
                this.AnimateIdle(gameTime);
            }
        }
                
        // Idle and running Animations
        public override void AnimateIdle(GameTime gameTime)
        {
            this.BasicAnimationLogic(gameTime, this.Delay, MidoriBasicAnimationFrameCount);
            this.SourceRect = new Rectangle(this.CurrentFrame * MidoriTextureWidth, MidoriTextureHeight * 0, MidoriTextureWidth, MidoriTextureHeight);
        }

        public override void AnimateRunning(GameTime gameTime)
        {
            this.BasicAnimationLogic(gameTime, this.Delay, MidoriBasicAnimationFrameCount);
            if (this.IsFacingLeft)
            {
                this.SourceRect = new Rectangle(this.CurrentFrame * MidoriTextureWidth, MidoriTextureHeight * 2, MidoriTextureWidth, MidoriTextureHeight);
            }
            else
            {
                this.SourceRect = new Rectangle(this.CurrentFrame * MidoriTextureWidth, MidoriTextureHeight * 1, MidoriTextureWidth, MidoriTextureHeight);
            }
        }           
        
        // Jumping Animation
        public override void AnimateJump(GameTime gameTime)
        {
            this.BasicAnimationLogic(gameTime, this.Delay, MidoriBasicAnimationFrameCount);
            if (this.IsFacingLeft)
            {
                this.SourceRect = new Rectangle(this.CurrentFrame * MidoriTextureWidth, MidoriTextureHeight * 4, MidoriTextureWidth, MidoriTextureHeight);                
            }
            else
            {
                this.SourceRect = new Rectangle(this.CurrentFrame * MidoriTextureWidth, MidoriTextureHeight * 3, MidoriTextureWidth, MidoriTextureHeight);
            }
        }

        // Falling Animation
        public override void AnimateFall(GameTime gameTime)
        {
            this.BasicAnimationLogic(gameTime, this.Delay, MidoriBasicAnimationFrameCount); if (this.IsFacingLeft)
            {
                this.SourceRect = new Rectangle(this.CurrentFrame * MidoriTextureWidth, MidoriTextureHeight * 6, MidoriTextureWidth, MidoriTextureHeight);
            }
            else
            {
                this.SourceRect = new Rectangle(this.CurrentFrame * MidoriTextureWidth, MidoriTextureHeight * 5, MidoriTextureWidth, MidoriTextureHeight);
            }
        }
        
        // Ranged Attack
        public void AnimateAttackRanged(GameTime gameTime)
        {
            this.Timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.Timer >= MidoriAttackDelay)
            {
                this.CurrentFrame++;

                if (this.ComboStageCounter > 1 && this.CurrentFrame > 3)
                {                    
                    this.AnimateRayCombo();
                    this.Timer = 0.0;
                    return;
                }

                if (this.CurrentFrame == 2)
                {
                    if (this.IsFacingLeft)
                    {
                        Engine.AddProjectile(
                         new MidoriSmallProjectile(
                             new Vector2(this.BoundingBoxX - 25, this.BoundingBoxY + 15),
                             (this.IsFacingLeft ? true : false),
                             this)); 
                    }
                    else
                    {
                        Engine.AddProjectile(
                        new MidoriSmallProjectile(
                            new Vector2(this.BoundingBoxX - 10, this.BoundingBoxY + 22),
                            (this.IsFacingLeft ? true : false),
                            this));
                    }                    
                }                
                else if (this.CurrentFrame >= MidoriAttackRangedFrameCount)
                {                    
                    this.CurrentFrame = 0;
                    this.ComboStageCounter = 0;
                    this.IsAttackingRanged = false;                    
                }

                this.Timer = 0.0;
            }

            if (this.IsFacingLeft)
            {
                this.SourceRect = new Rectangle(this.CurrentFrame * MidoriTextureWidth, MidoriTextureHeight * 8, MidoriTextureWidth, MidoriTextureHeight);
            }
            else
            {
                this.SourceRect = new Rectangle(this.CurrentFrame * MidoriTextureWidth, MidoriTextureHeight * 7, MidoriTextureWidth, MidoriTextureHeight);
            }
            
        }

        private void AnimateRayCombo()
        {

            if (this.CurrentFrame == 6)
            {
                if (this.IsFacingLeft)
                {
                    Engine.AddProjectile(
                        new MidoriSmallProjectile(
                            new Vector2(this.BoundingBoxX - 25, this.BoundingBoxY + 15),
                            (this.IsFacingLeft ? true : false),
                            this));
                }
                else
                {
                    Engine.AddProjectile(
                        new MidoriSmallProjectile(
                            new Vector2(this.BoundingBoxX - 25, this.BoundingBoxY + 22),
                            (this.IsFacingLeft ? true : false),
                            this));
                }
            }
            else if (this.CurrentFrame == 11)
            {
                if (this.IsFacingLeft)
                {
                    Engine.AddProjectile(new RayParticle(new Vector2(this.X + 80, this.Y + 32), (this.IsFacingLeft ? true : false), this));
                }
                else
                {
                    Engine.AddProjectile(new RayParticle(new Vector2(this.X + MidoriTextureWidth - 100, this.Y + 32), (this.IsFacingLeft ? true : false), this));
                }
            }
            else if (this.CurrentFrame >= MidoriRayComboFrameCount)
            {
                this.ComboStageCounter = 0;
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

            if (this.IsFacingLeft)
            {
                this.SourceRect = new Rectangle(this.CurrentFrame * MidoriTextureWidth, MidoriTextureHeight * 8, MidoriTextureWidth, MidoriTextureHeight);
            }
            else 
            {
                this.SourceRect = new Rectangle(this.CurrentFrame * MidoriTextureWidth, MidoriTextureHeight * 7, MidoriTextureWidth, MidoriTextureHeight);
            }
        }

        # endregion

#endregion

        

    }
}
