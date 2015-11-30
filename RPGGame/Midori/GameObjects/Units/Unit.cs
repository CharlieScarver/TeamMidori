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
    public abstract class Unit : GameObject, Interfaces.IDrawable, Interfaces.ICollidable
    {
        private readonly float defaultMovementSpeed;
        private readonly int textureWidth;
        private readonly int textureHeight;
        private readonly int delay;
        private readonly int frameCount;

        private Vector2 position;
        private Texture2D spriteSheet;
        private Rectangle sourceRect;
        private int currentFrame;
        private double timer;
        private float movementSpeed;
        private int jumpSpeed;
        private Rectangle boundingBox;

        public Unit(Vector2 position, float defaultMovementSpeed, int textureWidth, int textureHeight, int delay, int frameCount)
        {
            this.position = position;
            this.defaultMovementSpeed = defaultMovementSpeed;
            this.textureWidth = textureWidth;
            this.textureHeight = textureHeight;
            this.delay = delay;
            this.frameCount = frameCount;

            this.boundingBox = new Rectangle((int)this.X + (this.textureWidth / 2), (int)Position.Y, this.textureWidth / 2, this.textureHeight);
            this.sourceRect = new Rectangle(0, 192, this.textureWidth, this.textureHeight);
            this.CurrentFrame = 0;
            this.timer = 0.0;
            
            this.MovementSpeed = defaultMovementSpeed;
        }

        public int JumpSpeed
        {
            get { return this.jumpSpeed; }
            set { this.jumpSpeed = value; }
        }

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

        public Vector2 Position 
        {
            get { return this.position; }
        }

        public float X
        {
            get { return this.position.X; }
            set
            {
                this.position.X = value;
            }
        }

        public float Y
        {
            get { return this.position.Y; }
            set
            {
                this.position.Y = value;
            }
        }

        public float MovementSpeed { get; set; }

        protected Texture2D SpriteSheet { get; set; }

        protected Rectangle SourceRect 
        {
            get { return this.sourceRect;  }
            set { this.sourceRect = value; }
        }

        public Rectangle BoundingBox
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Load(ContentManager content)
        {
            this.SpriteSheet = content.Load<Texture2D>("Sprites/old_guy");
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        public void AnimateRight(GameTime gameTime)
        {
            this.timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.timer >= this.delay)
            {
                this.CurrentFrame++;

                if (this.CurrentFrame == this.frameCount)
                {
                    this.CurrentFrame = 0;
                }

                    
                this.timer = 0.0;
            }

            this.SourceRect = new Rectangle(this.CurrentFrame * this.textureWidth, this.textureHeight * 3, this.textureWidth, this.textureHeight);
        }

        public void AnimateLeft(GameTime gameTime)
        {
            this.timer += gameTime.ElapsedGameTime.TotalMilliseconds;


            if (this.timer >= this.delay)
            {
                this.CurrentFrame++;

                if (this.CurrentFrame == this.frameCount)
                {
                    this.CurrentFrame = 0;
                }

                  
                this.timer = 0.0;
            }

            this.SourceRect = new Rectangle(this.CurrentFrame * this.textureWidth, this.textureHeight * 2, this.textureWidth, this.textureHeight);
        }

        public void Idle()
        {
            this.SourceRect = new Rectangle(0, 0, this.textureWidth, this.textureHeight);
        }
    }
}
