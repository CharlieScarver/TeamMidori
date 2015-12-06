using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Midori.GameObjects;
using Midori.Core.TextureLoading;

namespace Midori.Core
{
    public class Tile : GameObject
    {
        private int type;

        public Tile(Vector2 position, int type)
        {
            this.Position = position;
            this.type = type;
            switch (type)
            {
                case 1:
                    this.SpriteSheet = TextureLoader.GreenTileStart;
                    break;
                case 2:
                    this.SpriteSheet = TextureLoader.GreenTileMiddle;
                    break;
                case 3:
                    this.SpriteSheet = TextureLoader.GreenTileEnd;
                    break;

            }
            //this.SourceRect = new Rectangle(0, 192, this.textureWidth, this.textureHeight);

            this.TextureWidth = 128;
            this.TextureHeight = 128;
            this.BoundingBox = new Rectangle((int)this.Position.X, (int)this.Position.Y - 12 + (this.TextureHeight / 2), this.TextureWidth, 5);
            //this.BoundingBox = new Rectangle((int)this.Position.X, (int)this.Position.Y - 12 + (128 / 2), 128, 100);

            this.IsSolid = true;
        }

        public int Type 
        {
            get { return this.type; }
            private set {
                if (value < 1 || value > 3)
                {
                    throw new ArgumentOutOfRangeException("Type should be between 1 and 3");
                }
            }
        }

        public bool IsSolid { get; protected set; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.SpriteSheet, this.Position);
        }

    }
}
