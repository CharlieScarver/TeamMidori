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
        private readonly int textureWidth;
        private readonly int textureHeight;
        private readonly int delay;
        private readonly int frameCount;
        private readonly float defaultJumpSpeed;
        private readonly float defaultMovementSpeed;
        private const int gravity = 10;
        private const int consequentJumps = 2;

        private int currentFrame;
        private double timer;
        private float movementSpeed;
        private float jumpSpeed;
        private int jumpCounter;

        private bool isJumping;
        private bool isFalling;
        private bool isOnGround;

        //Testing
        public float scale;


        public Unit(Vector2 position, int textureWidth, int textureHeight, int delay, int frameCount, float defaultMovementSpeed, float defaultJumpSpeed)
        {
            this.Position = position;
            this.textureWidth = textureWidth;
            this.textureHeight = textureHeight;
            this.delay = delay;
            this.frameCount = frameCount;
            this.defaultMovementSpeed = defaultMovementSpeed;
            this.defaultJumpSpeed = defaultJumpSpeed;

            this.timer = 0.0;
            this.CurrentFrame = 0;
            this.SourceRect = new Rectangle();
            this.BoundingBox = new Rectangle(
                (int)this.X + (this.textureWidth / 4), 
                (int)this.Y, 
                this.textureWidth / 2, 
                this.textureHeight);
            this.FutureBoundingBox = new Rectangle(this.BoundingBox.X, this.BoundingBox.Y, this.textureWidth / 2, this.textureHeight);

            this.MovementSpeed = defaultMovementSpeed; 
            this.JumpSpeed = defaultJumpSpeed;
            this.jumpCounter = 0;

            this.IsJumping = false;
            this.IsFalling = true;
        }

        // Properties
        

        public int CurrentFrame
        {
            get { return this.currentFrame; }
            set
            {
                if (value < 0 || value > this.frameCount)
                {
                    throw new ArgumentOutOfRangeException("Current frame should be between 0 and the amount of frames");
                }

                this.currentFrame = value;
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

        public int FrameCount
        {
            get { return this.frameCount; }
        }

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

        public bool IsJumping
        {
            get { return this.isJumping; }
            set { this.isJumping = value; }
        }

        public bool IsFalling
        {
            get { return this.isFalling; }
            set { this.isFalling = value; }
        }

        public bool IsOnGround
        {
            get { return this.isOnGround; }
            set { this.isOnGround = value; }
        }

        // Abstract Methods
        public abstract void Update(GameTime gameTime);


        // Non-abstract Methods
        protected void ManageMovement()
        {

            this.BoundingBoxX = this.FutureBoundingBoxX;
            this.BoundingBoxY = this.FutureBoundingBoxY;
            

            if (this.IsFalling)
            {
                var futurePosition = new Rectangle(
                     (int)this.Position.X,
                     (int)(this.Position.Y + gravity),
                     this.BoundingBox.Width,
                     this.BoundingBox.Height);
                if (World.ValidateFuturePosition(futurePosition))
                {
                    this.Y += gravity;
                }
                else
                {
                    this.IsOnGround = true;
                    this.JumpCounter = 0;
                }
                 
            }
            else
            {
                var futurePosition = new Rectangle(
                    (int)this.Position.X,
                    (int)(this.Position.Y + gravity),
                    this.BoundingBox.Width,
                    this.BoundingBox.Height);
                if (World.ValidateFuturePosition(futurePosition))
                {
                    this.IsFalling = true;
                    this.IsOnGround = false;
                }
                 
            }

            if (this.IsJumping)
            {
                this.Y -= this.JumpSpeed;
                this.JumpSpeed--;
                
                if (this.JumpSpeed < 0)
                {
                    this.IsJumping = false;
                    this.IsFalling = true;
                }
            }


            this.FutureBoundingBoxX = (int)this.X + (this.textureWidth / 4);
            this.FutureBoundingBoxY = (int)this.Y;
            
        }
        public abstract void AnimateRight(GameTime gameTime);

        public abstract void AnimateLeft(GameTime gameTime);

        public abstract void AnimateIdle();
    }
}
