using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Midori.Core;
using Midori.GameObjects.Projectiles;
using Midori.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.GameObjects.Units
{
    public abstract class Unit : GameObject, IAnimatableUnit, Interfaces.IUpdatable, IMoveable
    {
        private const int gravity = 13;
        private const int consequentJumps = 2;

        public Unit()
            : base()
        {
            this.Timer = 0.0;
            this.CurrentFrame = 0;
            this.SourceRect = new Rectangle();

            this.JumpCounter = 0;

            this.IsJumping = false;
            this.IsFalling = false;
            this.IsMovingLeft = false;
            this.IsMovingRight = false; 
            this.HasFreePathing = false;

            this.IsFacingLeft = false;

            this.IsAttackingRanged = false;
        }

        # region Properties
        // Properties
        public int Health { get; protected set; }

        protected int MaxHealth { get; set; }

        public int CurrentFrame { get; protected set; }

        public int BasicAnimationFrameCount { get; protected set; }

        public double Timer { get; protected set; }

        public int Delay { get; protected set; }

        public Rectangle SourceRect { get; protected set; }

        public float MovementSpeed { get; protected set; }

        public float JumpSpeed { get; protected set; }

        public float DefaultMovementSpeed { get; protected set; }

        public float DefaultJumpSpeed { get; protected set; }

        public int JumpCounter { get; protected set; }

        public Rectangle FuturePosition { get; set; }
        
        public bool IsJumping { get; protected set; }

        public bool IsFalling { get; protected set; }

        public bool HasFreePathing { get; protected set; }

        public bool IsMovingRight { get; protected set; }

        public bool IsMovingLeft { get; protected set; }

        public bool IsFacingLeft { get; protected set; }

        public bool IsAttackingRanged { get; protected set; }

        public int DamageRanged { get; protected set; }


# endregion 

        # region Non-abstract Methods
        // Non-abstract Methods
        protected void ManageMovement(GameTime gameTime)
        {
            
            if (this.IsJumping)
            {
                if (this.ValidateUpperPosition())
                {
                    // if upper position is valid continue jumping
                    this.Y -= this.JumpSpeed;
                    this.JumpSpeed--;
                }
                else
                {
                    // else stop the jump; start falling
                    this.JumpSpeed = 0;
                }

                // if jump is over
                if (this.JumpSpeed == 0)
                {
                    // if unit is inside a tile => gain free pathing
                    if (Collision.CheckForCollisionWithPlatform(this.BoundingBox))
                    {
                        this.HasFreePathing = true;
                    }
                    this.IsJumping = false;
                    this.IsFalling = true;
                }
            }
            else if (this.IsFalling)
            {
                if (this.HasFreePathing)
                { 
                    if (!Collision.CheckForCollisionWithAnyTiles(this.BoundingBox))                    
                    {
                        // if unit is already not inside a tile => lose free pathing
                        this.HasFreePathing = false;
                    }
                    else
                    {
                        // if unit is still inside a tile => fall
                        this.ApplyGravity();
                    }
                }
                else
                {
                    if (!this.ValidateLowerPosition())
                    {
                        if (!Collision.CheckForCollisionWithAnyTiles(this.BoundingBox))
                        {
                            // if the lower position is invalid and the current is valid => unit is on ground
                            this.ApplyOnGroundEffect();
                        }
                        else
                        {
                            // fall to avoid getting stuck in a tile (side entry bug)
                            this.ApplyGravity();
                        }
                    }
                    else
                    {
                        // if the lower position is valid => fall
                        this.ApplyGravity();                      
                    }
                }
            }
            else
            {
                if (this.ValidateLowerPosition())
                {
                    // if the lower position is valid
                    this.IsFalling = true;
                }
            }

            // Left & Right Movement
            if (this.IsMovingLeft)
            {
                this.FuturePosition = new Rectangle(
                            (int)(this.BoundingBox.X - this.MovementSpeed),
                            (int)this.BoundingBox.Y,
                            this.BoundingBox.Width,
                            this.BoundingBox.Height);

                if ((this.HasFreePathing || Collision.CheckForCollisionWithAnyTiles(this.BoundingBox))
                    && !Collision.CheckForCollisionWithWalls(this.FuturePosition))
                {
                    // if unit has free pathing OR is in a tile
                    // AND will not collide with a wall => move
                    this.X -= this.MovementSpeed;
                }
                else
                {                    
                    if (!Collision.CheckForCollisionWithAnyTiles(this.FuturePosition))
                    {
                        // if next position is valid => move
                        this.X -= this.MovementSpeed;
                    }
                    else
                    {
                        // else => stop
                        this.IsMovingLeft = false;
                    }
                }
                
            }
            else if (this.IsMovingRight)
            {
                this.FuturePosition = new Rectangle(
                                (int)(this.BoundingBox.X + this.BoundingBox.Width + this.MovementSpeed),
                                (int)this.BoundingBox.Y,
                                this.BoundingBox.Width,
                                this.BoundingBox.Height);

                if ((this.HasFreePathing || Collision.CheckForCollisionWithAnyTiles(this.BoundingBox))
                    && !Collision.CheckForCollisionWithWalls(this.FuturePosition))
                {
                    // if unit has free pathing OR is in a tile
                    // AND will not collide with a wall => move
                    this.X += this.MovementSpeed;
                }
                else
                {                    
                    if (!Collision.CheckForCollisionWithAnyTiles(this.FuturePosition))
                    {
                        // if next position is valid => move
                        this.X += this.MovementSpeed;
                    }
                    else
                    {
                        // else => stop
                        this.IsMovingRight = false;
                    }
                }
                
            }

            // Return from opposite side if left the field
            if (Collision.CheckForCollisionWithWorldBounds(this))
            {
                ReturnFromOppositeSide();
            }

        }

        // returns true if the next position (by gravity pull) is valid
        private bool ValidateLowerPosition()
        {
            this.FuturePosition = new Rectangle(
                (int)this.BoundingBox.X,
                (int)(this.BoundingBox.Y + gravity),
                this.BoundingBox.Width,
                this.BoundingBox.Height);
            if (Collision.CheckForCollisionWithAnyTiles(this.FuturePosition))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool ValidateUpperPosition()
        {
            this.FuturePosition = new Rectangle(
                (int)this.BoundingBox.X,
                (int)(this.BoundingBox.Y - this.JumpSpeed),
                this.BoundingBox.Width,
                this.BoundingBox.Height);
            if (Collision.CheckForCollisionWithOtherThanPlatform(this.FuturePosition))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void ApplyGravity()
        {
            this.Y += gravity;
        }

        private void ApplyOnGroundEffect()
        {
            this.IsFalling = false;
            this.HasFreePathing = false;
            this.JumpCounter = 0;
        }

        private void ReturnFromOppositeSide()
        {
            if (this.BoundingBoxX > Engine.LevelBounds.Width)
            {
                this.X = -this.BoundingBox.Width;
            }
            if (this.BoundingBoxX < -this.BoundingBox.Width)
            {
                this.X = Engine.LevelBounds.Width - 5;
            }
            if (this.BoundingBoxY > Engine.LevelBounds.Height)
            {
                this.Y = -70;
            }
        }

        protected void BasicAnimationLogic(GameTime gameTime, int delay, int basicAnimationFrameCount)
        {
            this.Timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.Timer >= delay)
            {
                this.CurrentFrame++;

                if (this.CurrentFrame == basicAnimationFrameCount)
                {
                    this.CurrentFrame = 0;
                }

                this.Timer = 0.0;
            }
        }

        public void ResetAnimationCounter()
        {
            this.CurrentFrame = 0;
        }

        public void MakeUnitIdle()
        {
            this.IsMovingRight = false;
            this.IsMovingLeft = false;
        }

        public void GetHitByProjectile(Projectile projectile)
        {
            if (projectile is RayParticle)
            {
                this.Health -= 3;
            }
            else
            {
                this.Health -= projectile.Owner.DamageRanged;
            }
        }

        # endregion

        # region Abstract Methods
        // Abstract Methods
        public abstract void Update(GameTime gameTime);

        protected abstract void UpdateBoundingBox();

        protected abstract void ManageAnimation(GameTime gameTime);

        public abstract void AnimateRunningRight(GameTime gameTime);

        public abstract void AnimateRunningLeft(GameTime gameTime);

        public abstract void AnimateIdle(GameTime gameTime);

        public abstract void AnimateJumpRight(GameTime gameTime);

        public abstract void AnimateJumpLeft(GameTime gameTime);

        public abstract void AnimateFallRight(GameTime gameTime);

        public abstract void AnimateFallLeft(GameTime gameTime);

        # endregion
    }
}
