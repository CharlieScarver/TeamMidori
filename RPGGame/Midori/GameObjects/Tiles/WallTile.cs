using Microsoft.Xna.Framework;
using Midori.Core;
using Midori.TextureLoading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.GameObjects.Tiles
{
    public class WallTile : Tile
    {
        private const int BoundingBoxOffset = 50;

        public WallTile(Vector2 position, TileType type)
        {
            this.Position = position;
            
            this.TextureWidth = 128;
            this.TextureHeight = 128;

            switch (type)
            {
                case TileType.LeftWallTile:
                    this.SpriteSheet = TextureLoader.LeftWallTile;
                    this.BoundingBox = new Rectangle(
                        (int)this.Position.X, 
                        (int)this.Position.Y, 
                        this.TextureWidth - WallTile.BoundingBoxOffset, 
                        this.TextureHeight);
                    break;
                case TileType.RightWallTile:
                    this.SpriteSheet = TextureLoader.RightWallTile;
                    this.BoundingBox = new Rectangle(
                        (int)this.Position.X + (this.TextureWidth - (this.TextureWidth - WallTile.BoundingBoxOffset)), 
                        (int)this.Position.Y, 
                        this.TextureWidth - WallTile.BoundingBoxOffset, 
                        this.TextureHeight);
                    break;
                default:
                    throw new ArgumentException("Invalid tile type");
            }
            this.Type = type;
                      
            this.IsSolid = true;
        }

        
    }
}
