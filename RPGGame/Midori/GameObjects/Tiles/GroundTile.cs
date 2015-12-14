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
        private const int DefaultBoundingBoxHeight = 30;
        private const int StartEndBoundingBoxOffset = 40;

        public GroundTile(Vector2 position, string type)
        {
            this.Position = position;

            this.TextureWidth = 128;
            this.TextureHeight = 128;

            this.BoundingBox = new Rectangle(
                (int)this.Position.X, 
                (int)this.Position.Y - 12 + (this.TextureHeight / 2), 
                this.TextureWidth, 
                GroundTile.DefaultBoundingBoxHeight);
            
            switch (type)
            {
                case "(":
                    this.SpriteSheet = TextureLoader.GreenTileStart;
                    this.BoundingBox = new Rectangle(
                        (int)this.Position.X + GroundTile.StartEndBoundingBoxOffset, 
                        (int)this.Position.Y - 12 + (this.TextureHeight / 2), 
                        this.TextureWidth - GroundTile.StartEndBoundingBoxOffset, 
                        GroundTile.DefaultBoundingBoxHeight);
                    break;
                case "_":
                    this.SpriteSheet = TextureLoader.GreenTileMiddle;
                    break;
                case ")":
                    this.SpriteSheet = TextureLoader.GreenTileEnd;
                    this.BoundingBox = new Rectangle(
                        (int)this.Position.X, 
                        (int)this.Position.Y - 12 + (this.TextureHeight / 2), 
                        this.TextureWidth - GroundTile.StartEndBoundingBoxOffset, 
                        GroundTile.DefaultBoundingBoxHeight);
                    break;
                case "4":
                    this.SpriteSheet = TextureLoader.CornerTileLeft;
                    break;
                case "5":
                    this.SpriteSheet = TextureLoader.CornerTileRight;
                    break;
                case "8":
                    this.SpriteSheet = TextureLoader.GroundTileStart;
                    this.BoundingBox = new Rectangle(
                        (int)this.Position.X + GroundTile.StartEndBoundingBoxOffset, 
                        (int)this.Position.Y - 12 + (this.TextureHeight / 2), 
                        this.TextureWidth - GroundTile.StartEndBoundingBoxOffset, 
                        GroundTile.DefaultBoundingBoxHeight);
                    break;
                case "9":
                    this.SpriteSheet = TextureLoader.GroundTileEnd;
                    this.BoundingBox = new Rectangle(
                        (int)this.Position.X, 
                        (int)this.Position.Y - 12 + (this.TextureHeight / 2), 
                        this.TextureWidth - GroundTile.StartEndBoundingBoxOffset,
                        GroundTile.DefaultBoundingBoxHeight);
                    break;
                case "2":
                    this.SpriteSheet = TextureLoader.GroundTileMiddle;
                    break;
                default:
                    throw new ArgumentException("Invalid tile type");
            }
            this.Type = type;
           
            this.IsSolid = true;
        }      
    }
}
