using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Midori.TextureLoading;
using Midori.Interfaces;
using System;
using Midori.Core;
using Midori.GameObjects.Units.Enemies;

namespace Midori.GameObjects
{
    public abstract class GameObject : IGameObject
    {
        private Vector2 position;
        private Texture2D spriteSheet;
        private Rectangle boundingBox;
        private  int textureWidth;
        private int textureHeight;

        protected GameObject()
        {
            this.Id = this.GetHashCode();
            this.IsActive = true;
            this.BoundingBox = new Rectangle();
        }

        # region Properties
        // IGameObject
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public Vector2 Position
        {
            get { return this.position; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Position should not be null");
                }

                this.position = value;
            }
        }

        public float X
        {
            get { return this.position.X; }
            set { this.position.X = value; } // protected
        }

        public float Y
        {
            get { return this.position.Y; }
            set { this.position.Y = value; } // protected
        }

        // IDrawable
        public Texture2D SpriteSheet
        {
            get { return this.spriteSheet; }
            protected set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Sprite sheet shouldn't be null");
                }

                this.spriteSheet = value;
            }
        }

        public int TextureWidth
        {
            get { return this.textureWidth; }
            protected set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Texture width should not be negative or zero");
                }

                this.textureWidth = value;
            }
        }

        public int TextureHeight
        {
            get { return this.textureHeight; }
            protected set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Texture height should not be negative or zero");
                }

                this.textureHeight = value;
            }
        }

        // ICollidable
        public Rectangle BoundingBox
        {
            get { return this.boundingBox; }
            protected set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Bounding box shouldn't be null");
                }

                this.boundingBox = value;
            }
        }

        public int BoundingBoxX
        {
            get { return this.boundingBox.X; }
            set { this.boundingBox.X = value; }
        }

        public int BoundingBoxY
        {
            get { return this.boundingBox.Y; }
            set { this.boundingBox.Y = value; }
        }

        #endregion

        #region Methods
        public void Nullify()
        {
            this.IsActive = false;
            this.BoundingBox = new Rectangle();
            if (this is Enemy)
            {
                Engine.SpawnItem(new Vector2(this.X + this.TextureWidth / 2, this.Y));
            }
        }

        public void DrawBB(SpriteBatch spriteBatch, Color boundingBoxColor)
        {
            spriteBatch.Draw(TextureLoader.TheOnePixel, this.BoundingBox, boundingBoxColor);
        }

        // Abstract Methods        
        public abstract void Draw(SpriteBatch spriteBatch);

        #endregion
    }
}
