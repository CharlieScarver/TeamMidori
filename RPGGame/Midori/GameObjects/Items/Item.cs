using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Midori.Interfaces;
using Midori.Core;
using Midori.Core.TextureLoading;

namespace Midori.GameObjects.Items
{
    public class Item : GameObject, Interfaces.IUpdatable, IAnimatable
    {
        #region fields
        private ItemTypes type;
        private Rectangle futurePosition;
        private int delay;
        private double timer;
        private Color color;
        private string drawString;
        private int currentFrame;
        private Rectangle sourceRect;
        private bool isActive;
        #endregion

        #region Constructor
        public Item(Texture2D sprite, Vector2 position, ItemTypes type)
        {
            this.TextureWidth = 40;
            this.TextureHeight = 40;
            this.drawString = "";
            this.Position = position;
            this.SpriteSheet = sprite;
            this.Type = type;
            this.BoundingBox = new Rectangle((int)this.X, (int)this.Y, this.TextureWidth, this.TextureHeight);
            this.SourceRect = new Rectangle(this.currentFrame*this.TextureWidth, this.TextureHeight, this.TextureWidth, this.TextureHeight);
            this.delay = 60;
            this.isActive = true;
        }
        #endregion

        #region Properties

        public bool IsActive
        {
            get { return this.isActive; }
            set { this.isActive = value; }
        }

        public Rectangle FuturePosition
        {
            get { return this.futurePosition; }
            protected set { this.futurePosition = value; }
        }

        public ItemTypes Type
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

        public int CurrentFrame
        {
            get { return currentFrame; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("CurrentFrame cannot be negative.");
                }
                this.currentFrame = value;
            }
        }

        public int BasicAnimationFrameCount
        {
            get
            {
                return 3;
            }
        }

        public double Timer
        {
            get
            {
                return this.timer;
            }
            private set
            {
                this.timer = value;
            }
        }

        public int Delay
        {
            get
            {
                return this.delay;
            }
            set
            {
                this.delay = value;
            }
        }

        public Rectangle SourceRect
        {
            get
            {
                return this.sourceRect;
            }
            set { this.sourceRect = value; }
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
            if (Collision.CheckForCollisionWithTiles(this.FuturePosition))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Update(GameTime gameTime)
        {
            this.sourceRect = new Rectangle(this.currentFrame*this.TextureWidth, this.TextureHeight, this.TextureWidth, this.TextureHeight);
            this.timer += gameTime.ElapsedGameTime.TotalMilliseconds;
               
            if (this.ValidateLowerPosition())
            {
                this.Y += 13;
            }

            this.BoundingBoxX = (int)this.X;
            this.BoundingBoxY = (int)this.Y;

            if (this.BoundingBox.Intersects(Engine.Player.BoundingBox))
            {
                this.Nullify();
            }
            else
            {
                this.drawString = "";
                this.color = Color.Black;
            }

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

        public void DrawMessage(SpriteBatch spriteBatch)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureLoader.Font, this.drawString, new Vector2(this.X - (TextureLoader.Font.MeasureString(this.drawString).X / 2), this.Y - 20), Color.Black);
            spriteBatch.Draw(texture: this.SpriteSheet,
                sourceRectangle: this.SourceRect,
                destinationRectangle: new Rectangle((int)this.Position.X,
                (int)this.Position.Y, 40, 40),
                color: Color.White
                );
        }
        #endregion
    }
}
