using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Midori.Core;
using Midori.Core.TextureLoading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.GameObjects.Tiles
{
    public class GroundTile : Tile
    {

        public GroundTile(Vector2 position, int type)
        {
            this.Position = position;
            
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
                default:
                    throw new Exception("Invalid tile type");
            }
            this.Type = type;
            
            this.TextureWidth = 128;
            this.TextureHeight = 128;
            this.BoundingBox = new Rectangle((int)this.Position.X, (int)this.Position.Y - 12 + (this.TextureHeight / 2), this.TextureWidth, 5);
            //this.BoundingBox = new Rectangle((int)this.Position.X, (int)this.Position.Y - 12 + (128 / 2), 128, 100);

            this.IsSolid = true;
        }

        public int Type { get; private set; }

        public bool IsSolid { get; protected set; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.SpriteSheet, this.Position);
        }
    }
}
