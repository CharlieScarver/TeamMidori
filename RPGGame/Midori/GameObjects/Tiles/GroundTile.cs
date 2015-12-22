using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Midori.Core;
using Midori.Enumerations;
using Midori.TextureLoading;
using System;

namespace Midori.GameObjects.Tiles
{
    public class GroundTile : Tile
    {
        private const int GroundTileDefaultBoundingBoxHeight = 30;
        private const int GroundTileStartEndBoundingBoxOffset = 30;

        public GroundTile(Vector2 position, TileType type)
            : base()
        {
            this.Position = position;

            this.BoundingBox = new Rectangle(
                (int)this.Position.X, 
                (int)this.Position.Y - 12 + (this.TextureHeight / 2), 
                this.TextureWidth, 
                GroundTileDefaultBoundingBoxHeight);
            
            switch (type)
            {
                case TileType.StartPlatformTile:
                    this.SpriteSheet = TextureLoader.GreenTileStart;
                    this.BoundingBox = new Rectangle(
                        (int)this.Position.X + GroundTileStartEndBoundingBoxOffset, 
                        (int)this.Position.Y - 12 + (this.TextureHeight / 2), 
                        this.TextureWidth - GroundTileStartEndBoundingBoxOffset, 
                        GroundTileDefaultBoundingBoxHeight);
                    break;
                case TileType.MiddlePlatformTile:
                    this.SpriteSheet = TextureLoader.GreenTileMiddle;
                    break;
                case TileType.EndPlatformTile:
                    this.SpriteSheet = TextureLoader.GreenTileEnd;
                    this.BoundingBox = new Rectangle(
                        (int)this.Position.X, 
                        (int)this.Position.Y - 12 + (this.TextureHeight / 2), 
                        this.TextureWidth - GroundTileStartEndBoundingBoxOffset, 
                        GroundTileDefaultBoundingBoxHeight);
                    break;
                case TileType.LeftCornerGroundTile:
                    this.SpriteSheet = TextureLoader.CornerTileLeft;
                    break;
                case TileType.RightCornerGroundTile:
                    this.SpriteSheet = TextureLoader.CornerTileRight;
                    break;
                case TileType.StartGroundTile:
                    this.SpriteSheet = TextureLoader.GroundTileStart;
                    this.BoundingBox = new Rectangle(
                        (int)this.Position.X + GroundTileStartEndBoundingBoxOffset, 
                        (int)this.Position.Y - 12 + (this.TextureHeight / 2), 
                        this.TextureWidth - GroundTileStartEndBoundingBoxOffset, 
                        GroundTileDefaultBoundingBoxHeight);
                    break;
                case TileType.EndGroundTile:
                    this.SpriteSheet = TextureLoader.GroundTileEnd;
                    this.BoundingBox = new Rectangle(
                        (int)this.Position.X, 
                        (int)this.Position.Y - 12 + (this.TextureHeight / 2), 
                        this.TextureWidth - GroundTileStartEndBoundingBoxOffset,
                        GroundTileDefaultBoundingBoxHeight);
                    break;
                case TileType.MiddleGroundTile:
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
