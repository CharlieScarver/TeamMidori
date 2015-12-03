using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.GameObjects
{
    public abstract class GameObject : Interfaces.IDrawable, Interfaces.ICollidable
    {
        private Vector2 position;
        private Texture2D spriteSheet;
        private Rectangle sourceRect;
        private Rectangle boundingBox;
        private Rectangle futureBoundingBox;

        public int Id { get; set; }

        public bool IsActive { get; set; }


        public Vector2 Position
        {
            get { return this.position; }
            set 
            {
                this.position = value;
            }
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

        public Texture2D SpriteSheet
        {
            get { return this.spriteSheet; }
            protected set { this.spriteSheet = value; }
        }

        public Rectangle SourceRect
        {
            get { return this.sourceRect; }
            protected set { this.sourceRect = value; }
        }

        public Rectangle BoundingBox
        {
            get { return this.boundingBox; }
            protected set { this.boundingBox = value; }
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

        public Rectangle FutureBoundingBox
        {
            get { return this.futureBoundingBox;  }
            set { this.futureBoundingBox = value; }
        }

        public int FutureBoundingBoxX
        {
            get { return this.futureBoundingBox.X; }
            set { this.futureBoundingBox.X = value; }
        }

        public int FutureBoundingBoxY
        {
            get { return this.futureBoundingBox.Y; }
            set { this.futureBoundingBox.Y = value; }
        }

        // Methods
        public void DrawBB(SpriteBatch spriteBatch, ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Sprites/TheOnePixel");

            spriteBatch.Draw(texture, this.BoundingBox, Color.White);
            spriteBatch.Draw(texture: texture, destinationRectangle: futureBoundingBox, color: Color.Orange, layerDepth: 0);
        }

        // Abstract Methods        
        public abstract void Draw(SpriteBatch spriteBatch);




        
    }
}
