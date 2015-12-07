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
    public class Item : GameObject, IGameObject, Interfaces.IDrawable, ICollidable, IAnimatable
    {
        #region fields
        private ItemTypes type;
        private Rectangle futurePosition;
        private int delay;
        private double timer;
        private Color color;
        private string drawString;
        #endregion

        #region Constructor
        public Item(Texture2D sprite, Vector2 position, ItemTypes type)
        {
            this.drawString = "";
            this.Position = position;
            this.SpriteSheet = sprite;
            this.Type = type;
            this.BoundingBox = new Rectangle((int)this.X, (int)this.Y, 32, 32);
            color = Color.Black;
        }
        #endregion

        #region Properties
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
            get
            {
                return 0;
            }
        }

        public int BasicAnimationFrameCount
        {
            get
            {
                return 0;
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
                return new Rectangle();
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
            if (World.CheckForCollisionWithTiles(this.FuturePosition))
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
            
            if (this.ValidateLowerPosition())
            {
                this.Y += 13;
            }

            this.BoundingBoxX = (int)this.X;
            this.BoundingBoxY = (int)this.Y;

            if (this.BoundingBox.Intersects(Engine.Player.BoundingBox))
            {
                this.color = Color.DarkGoldenrod;
                this.drawString = "Pick me the fuck up";
            }
            else
            {
                this.drawString = "";
                this.color = Color.Black;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureLoader.Font, this.drawString, new Vector2(this.Position.X - (TextureLoader.Font.MeasureString(this.drawString).X/2), this.Position.Y - 20), color * 2f);
            spriteBatch.Draw(texture: this.SpriteSheet,
                destinationRectangle: new Rectangle((int)this.Position.X,
                (int)this.Position.Y, 32, 32),
                color: color * 0.6f,
                origin: new Vector2(this.SpriteSheet.Width / 2, this.SpriteSheet.Height / 2));
        }
        #endregion
    }
}
