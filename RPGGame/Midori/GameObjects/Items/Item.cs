using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Midori.Interfaces;
using Midori.Core;
using Midori.TextureLoading;
using Midori.GameObjects.Units.PlayableCharacters;
using System;
using Midori.Enumerations;

namespace Midori.GameObjects.Items
{
    public abstract class Item : GameObject, IItem
    {
        #region fields
        private const int ItemDefaultAnimationFrameCount = 3;
        private const int ItemDelay = 100;
        private const int ItemTextureWidth = 40;
        private const int ItemTextureHeight = 40;

        private ItemType type;
        private Rectangle futurePosition;

        private int currentFrame;
        private int basicAnimationFrameCount;
        private double timer;
        private int delay; 
        private Rectangle sourceRect;
        #endregion

        #region Constructor
        protected Item(Texture2D sprite, Vector2 position, ItemType type)
        {
            this.Position = position;

            this.TextureWidth = ItemTextureWidth;
            this.TextureHeight = ItemTextureHeight;
            
            this.SpriteSheet = sprite;

            this.CurrentFrame = 0;
            this.Timer = 0.0;
            this.BasicAnimationFrameCount = ItemDefaultAnimationFrameCount;
            this.delay = ItemDelay;
           
            this.Type = type;

            this.BoundingBox = new Rectangle((int)this.X, (int)this.Y, this.TextureWidth, this.TextureHeight);
            this.SourceRect = new Rectangle(this.currentFrame*this.TextureWidth, this.TextureHeight, this.TextureWidth, this.TextureHeight);
            
        }
        #endregion

        #region Properties

        public Color Color { get; protected set; }

        public Rectangle FuturePosition
        {
            get { return this.futurePosition; }
            protected set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Future position shouldn't be null");
                }

                this.futurePosition = value;
            }
        }

        public ItemType Type
        {
            get
            {
                return this.type;
            }
            protected set
            {
                this.type = value;
            }
        }

        // IAnimatable
        public int CurrentFrame
        {
            get
            {
                return this.currentFrame;
            }
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Current Frame should not be negative");
                }

                this.currentFrame = value;
            }
        }

        public int BasicAnimationFrameCount
        {
            get
            {
                return this.basicAnimationFrameCount;
            }
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Basic animation frame count Frame should not be negative");
                }

                this.basicAnimationFrameCount = value;
            }
        }

        public double Timer
        {
            get
            {
                return this.timer;
            }
            protected set
            {
                if (value < 0.0)
                {
                    throw new ArgumentOutOfRangeException("Timer should not be negative");
                }

                this.timer = value;
            }
        }

        public int Delay
        {
            get
            {
                return this.delay;
            }
            protected set
            {
                if (value < 0.0)
                {
                    throw new ArgumentOutOfRangeException("Delay should not be negative");
                }

                this.delay = value;
            }
        }

        public Rectangle SourceRect
        {
            get { return this.sourceRect; }
            protected set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Source rectangle shouldn't be null");
                }

                this.sourceRect = value;
            }
        }

        #endregion

        #region Methods

        private bool ValidateLowerPosition()
        {
            this.FuturePosition = new Rectangle(
                (int)this.BoundingBox.X,
                (int)(this.BoundingBox.Y + 13),
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

        public virtual void Update(GameTime gameTime)
        {
            this.sourceRect = new Rectangle(this.currentFrame*this.TextureWidth, this.TextureHeight, this.TextureWidth, this.TextureHeight);
            this.timer += gameTime.ElapsedGameTime.TotalMilliseconds;
               
            if (this.ValidateLowerPosition())
            {
                this.Y += 13;
            }

            this.BoundingBoxX = (int)this.X;
            this.BoundingBoxY = (int)this.Y;

            if (this.timer >= this.delay)
            {
                this.currentFrame++;
                if (this.CurrentFrame > this.BasicAnimationFrameCount)
                {
                    this.CurrentFrame = 0;
                }
                this.timer = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture: this.SpriteSheet,
                sourceRectangle: this.SourceRect,
                destinationRectangle: new Rectangle((int)this.Position.X,
                (int)this.Position.Y, 40, 40),
                color: this.Color
                );
        }

        #endregion
    }
}
