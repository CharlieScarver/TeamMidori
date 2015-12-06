using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Midori.Core;
using Midori.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.GameObjects.Units
{
    public abstract class Unit : GameObject, IAnimatable, Interfaces.IUpdatable, IMoveable
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
        }

        // Properties
        public int CurrentFrame { get; set; } //protected

        public int BasicAnimationFrameCount { get; protected set; }

        public double Timer { get; protected set; }

        public int Delay { get; protected set; }

        public Rectangle SourceRect { get; protected set; }

        public float MovementSpeed { get; set; }

        public float JumpSpeed { get; set; }

        public float DefaultMovementSpeed { get; protected set; }

        public float DefaultJumpSpeed { get; protected set; }

        public int JumpCounter { get; set; }

        public Rectangle FuturePosition { get; set; }
        
        public bool IsJumping { get; set; }

        public bool IsFalling { get; set; }

        public bool HasFreePathing { get; set; }

        public bool IsMovingRight { get; set; }

        public bool IsMovingLeft { get; set; }

        // Non-abstract Methods
        protected void ManageMovement(GameTime gameTime)
        {
            
            if (this.IsJumping)
            {
                this.Y -= this.JumpSpeed;
                this.JumpSpeed--;

                // if jump is over
                if (this.JumpSpeed == 0)
                {
                    // if unit is inside a tile => gain free pathing
                    if (World.CheckForCollisionWithTiles(this.BoundingBox))
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
                    if (!World.CheckForCollisionWithTiles(this.BoundingBox))                    
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
                        if (!World.CheckForCollisionWithTiles(this.BoundingBox))
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
                this.X -= this.MovementSpeed;
            }
            else if (this.IsMovingRight)
            {
                this.X += this.MovementSpeed;
            }

            // Return from opposite side if left the field
            if (World.CheckForCollisionWithWorldBounds(this))
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
            if (World.CheckForCollisionWithTiles(this.FuturePosition))
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

        

        // Abstract Methods
        public abstract void Update(GameTime gameTime);

        public abstract void AnimateRunningRight(GameTime gameTime);

        public abstract void AnimateRunningLeft(GameTime gameTime);

        public abstract void AnimateIdle(GameTime gameTime);

        public abstract void AnimateJumpRight(GameTime gameTime);

        public abstract void AnimateJumpLeft(GameTime gameTime);

        public abstract void AnimateFallRight(GameTime gameTime);

        public abstract void AnimateFallLeft(GameTime gameTime);

    }
}
