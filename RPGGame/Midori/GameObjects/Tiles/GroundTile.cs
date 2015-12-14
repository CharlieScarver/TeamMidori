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

        public GroundTile(Vector2 position, string type)
        {
            this.Position = position;
            
            switch (type)
            {
                case "(":
                    this.SpriteSheet = TextureLoader.GreenTileStart;
                    break;
                case "_":
                    this.SpriteSheet = TextureLoader.GreenTileMiddle;
                    break;
                case ")":
                    this.SpriteSheet = TextureLoader.GreenTileEnd;
                    break;
                case "4":
                    this.SpriteSheet = TextureLoader.CornerTileLeft;
                    break;
                case "5":
                    this.SpriteSheet = TextureLoader.CornerTileRight;
                    break;
                case "6":
                    this.SpriteSheet = TextureLoader.LeftWallTile;
                    break;
                case "7":
                    this.SpriteSheet = TextureLoader.RightWallTile;
                    break;
                case "8":
                    this.SpriteSheet = TextureLoader.GroundTileStart;
                    break;
                case "9":
                    this.SpriteSheet = TextureLoader.GroundTileEnd;
                    break;
                case "2":
                    this.SpriteSheet = TextureLoader.GroundTileMiddle;
                    break;
                case "A":
                    this.SpriteSheet = TextureLoader.InnerGroundTile;
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

        public string Type { get; private set; }

        public bool IsSolid { get; protected set; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.SpriteSheet, this.Position);
        }
    }
}
