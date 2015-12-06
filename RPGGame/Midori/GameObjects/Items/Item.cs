using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Midori.Interfaces;

namespace Midori.GameObjects.Items
{
    public class Item : IGameObject, Interfaces.IDrawable, ICollidable
    {
        private Vector2 position;
        private Texture2D sprite;
        private ItemTypes type;
        private Rectangle boundingBox;

        public Item(Texture2D sprite, Vector2 position, ItemTypes type)
        {
            this.Position = position;
            this.SpriteSheet = sprite;
            this.Type = type;
            this.boundingBox = new Rectangle((int)this.X, (int)this.Y, this.TextureWidth, this.TextureHeight);
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

        public Rectangle BoundingBox
        {
            get
            {
                return this.boundingBox;
            }
        }

        public int BoundingBoxX
        {
            get
            {
                return this.boundingBox.X;
            }

            set
            {
                this.boundingBox.X = value;
            }
        }

        public int BoundingBoxY
        {
            get
            {
                return this.boundingBox.Y;
            }

            set
            {
                this.boundingBox.Y = value;
            }
        }

        public int Id
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsActive
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Vector2 Position
        {
            get
            {
                return this.position;
            }
            protected set
            {
                this.position = value;
            }
        }

        public Texture2D SpriteSheet
        {
            get
            {
                return this.sprite;
            }
            protected set
            {
                this.SpriteSheet = value;
            }
        }

        public int TextureHeight
        {
            get
            {
                return this.sprite.Height;
            }
        }

        public int TextureWidth
        {
            get
            {
                return this.sprite.Width;
            }
        }

        public float X
        {
            get
            {
                return this.position.X;
            }

            set
            {
                this.position.X = value;
            }
        }

        public float Y
        {
            get
            {
                return this.position.Y;
            }

            set
            {
                this.position.Y = value;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.SpriteSheet, this.Position, Color.White);
        }
    }
}
