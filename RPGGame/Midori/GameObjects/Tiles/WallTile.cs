using Microsoft.Xna.Framework;
using Midori.Core;
using Midori.Enumerations;
using Midori.TextureLoading;
using System;

namespace Midori.GameObjects.Tiles
{
    public class WallTile : Tile
    {
        private const int WallTileBoundingBoxOffset = 50;

        public WallTile(Vector2 position, TileType type)
        {
            this.Position = position;
            
            switch (type)
            {
                case TileType.LeftWallTile:
                    this.SpriteSheet = TextureLoader.LeftWallTile;
                    this.BoundingBox = new Rectangle(
                        (int)this.Position.X, 
                        (int)this.Position.Y, 
                        this.TextureWidth - WallTileBoundingBoxOffset, 
                        this.TextureHeight);
                    break;
                case TileType.RightWallTile:
                    this.SpriteSheet = TextureLoader.RightWallTile;
                    this.BoundingBox = new Rectangle(
                        (int)this.Position.X + (this.TextureWidth - (this.TextureWidth - WallTileBoundingBoxOffset)), 
                        (int)this.Position.Y, 
                        this.TextureWidth - WallTileBoundingBoxOffset, 
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
