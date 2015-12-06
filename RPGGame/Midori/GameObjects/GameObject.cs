using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Midori.Core.TextureLoading;
using Midori.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.GameObjects
{
    public abstract class GameObject : IGameObject, Interfaces.IDrawable, Interfaces.ICollidable
    {
        private Vector2 position;
        private Texture2D spriteSheet;
        private Rectangle boundingBox;

        public GameObject()
        {
            this.Id = this.GetHashCode();
            this.IsActive = true;
            this.BoundingBox = new Rectangle();
        }

        public int Id { get; set; }

        public bool IsActive { get; set; }

        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
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

        public int TextureWidth { get; protected set; }

        public int TextureHeight { get; protected set; }

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

        // Methods
        public void DrawBB(SpriteBatch spriteBatch, Color boundingBoxColor)
        {
            spriteBatch.Draw(TextureLoader.TheOnePixel, this.BoundingBox, boundingBoxColor);
        }

        // Abstract Methods        
        public abstract void Draw(SpriteBatch spriteBatch);     

    }
}
