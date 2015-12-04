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
    public abstract class Unit : GameObject, Interfaces.IAnimatable, Interfaces.ICollidable
    {
        //ADD PROPS FOR MOVING STATE
        private readonly int textureWidth;
        private readonly int textureHeight;
        private readonly int delay;
        private readonly int runningAndIdleFrameCount;
        private readonly int jumpFrameCount;
        private readonly float defaultJumpSpeed;
        private readonly float defaultMovementSpeed;
        private const int gravity = 13;
        private const int consequentJumps = 2;

        private int currentFrameRunningAndIdle;
        private int currentFrameJump;
        private double timer;
        private float movementSpeed;
        private float jumpSpeed;
        private int jumpCounter;

        public Unit(Vector2 position, int textureWidth, int textureHeight, int delay, int runningAndIdleFrameCount, int jumpFrameCount, float defaultMovementSpeed, float defaultJumpSpeed)
        {
            this.Position = position;
            this.textureWidth = textureWidth;
            this.textureHeight = textureHeight;
            this.delay = delay;
            this.runningAndIdleFrameCount = runningAndIdleFrameCount;
            this.jumpFrameCount = jumpFrameCount;
            this.defaultMovementSpeed = defaultMovementSpeed;
            this.defaultJumpSpeed = defaultJumpSpeed;

            this.timer = 0.0;
            this.CurrentFrameRunningAndIdle = 0;
            this.CurrentFrameJump = 0;
            this.SourceRect = new Rectangle();
            this.BoundingBox = new Rectangle(
                (int)this.X + (this.textureWidth / 4), 
                (int)this.Y, 
                this.textureWidth / 2, 
                this.textureHeight);

            this.MovementSpeed = defaultMovementSpeed; 
            this.JumpSpeed = defaultJumpSpeed;
            this.jumpCounter = 0;

            this.IsJumping = false;
            this.IsFalling = true;
        }

        // Properties


        public int CurrentFrameRunningAndIdle
        {
            get { return this.currentFrameRunningAndIdle; }
            set 
            {
                if (value < 0 || value > this.RunningAndIdleFrameCount)
                {
                    throw new ArgumentOutOfRangeException("Current frame should be between 0 and the amount of frames");
                }

                this.currentFrameRunningAndIdle = value; 
            }
        }

        public int CurrentFrameJump
        {
            get { return this.currentFrameJump; }
            set 
            {
                if (value < 0 || value > this.JumpFrameCount)
                {
                    throw new ArgumentOutOfRangeException("Current frame should be between 0 and the amount of frames");
                }

                this.currentFrameJump = value; 
            }
        }

        public float MovementSpeed {
            get { return this.movementSpeed;  }
            set { this.movementSpeed = value; }
        }

        public float JumpSpeed
        {
            get { return this.jumpSpeed; }
            set { this.jumpSpeed = value; }
        }

        public int Delay
        {
            get { return this.delay; }
        }

        public double Timer
        {
            get { return this.timer; }
            protected set { this.timer = value; }
        }

        public int RunningAndIdleFrameCount
        {
            get { return this.runningAndIdleFrameCount; }
        }

        public int JumpFrameCount
        {
            get { return this.jumpFrameCount; }
        }

        //public int JumpAnimRow { get; set; }

        public int TextureWidth { get { return this.textureWidth; } }

        public int TextureHeight { get { return this.textureHeight; } }

        public float DefaultJumpSpeed
        {
            get { return this.defaultJumpSpeed; }
        }

        public int JumpCounter
        {
            get { return this.jumpCounter; }
            set { this.jumpCounter = value; }
        }

        public Rectangle FuturePosition { get; set; }
        
        public bool IsJumping { get; set; }

        public bool IsFalling { get; set; }

        public bool HasFreePathing { get; set; }

        public bool isMovingRight { get; set; }

        public bool isMovingLeft { get; set; }

        // Non-abstract Methods
        protected void ManageMovement(GameTime gameTime)
        {
            
            if (this.IsJumping)
            {
                this.Y -= this.JumpSpeed;
                this.JumpSpeed--;

                this.CallJumpAnimation(gameTime);

                // if jump is over => fall/gain free pathing
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
                this.CallJumpAnimation(gameTime);

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
                            // if the lower position is invalid and the current is valid
                            this.ApplyOnGroundEffect();
                        }
                    }
                    else
                    {
                        // if the lower position is valid
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


            // update bounding box
            this.BoundingBoxX = (int)this.X + (this.textureWidth / 4);
            this.BoundingBoxY = (int)this.Y;
        }

        // returns true if the next position (by gravity pull) is valid
        private bool ValidateLowerPosition()
        {
            this.FuturePosition = new Rectangle(
                (int)this.Position.X,
                (int)(this.Position.Y + gravity),
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
            //this.IsOnGround = true;
            this.IsFalling = false;
            this.HasFreePathing = false;
            this.JumpCounter = 0;
        }

        private void CallJumpAnimation(GameTime gameTime)
        {
            if (this.isMovingLeft)
            {
                this.AnimateJumpLeft(gameTime);
            }
            else
            {
                this.AnimateJumpRight(gameTime);
            }
        }

        // Abstract Methods
        public abstract void Update(GameTime gameTime);

        public abstract void AnimateRight(GameTime gameTime);

        public abstract void AnimateLeft(GameTime gameTime);

        public abstract void AnimateIdle(GameTime gameTime);

        public abstract void AnimateJumpRight(GameTime gameTime);

        public abstract void AnimateJumpLeft(GameTime gameTime);

    }
}
