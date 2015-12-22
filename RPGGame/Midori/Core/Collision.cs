using Microsoft.Xna.Framework;
using Midori.GameObjects;
using Midori.GameObjects.Units;
using Midori.GameObjects.Items;
using Midori.Interfaces;
using Midori.Enumerations;
using Midori.GameObjects.Tiles;

namespace Midori.Core
{
    public static class Collision
    {
        public static Item GetCollidingItem()
        {
            foreach (var item in Engine.Items)
            {
                if (Engine.Player.BoundingBox.Intersects(item.BoundingBox))
                {
                    item.Nullify();
                    return item;
                }
            }
            return null;
        }

        public static bool CheckForCollisionWithWorldBounds(GameObject obj)
        {
            if (obj.X > Engine.LevelBounds.Width)
            {
                return true;
            }
            if (obj.X < -(obj.BoundingBox.Width))
            {
                return true;
            }
            if (obj.Y > Engine.LevelBounds.Height)
            {
                return true;
            }

            return false;
        }

        public static bool CheckForCollisionBetween(ICollidable firstObj, ICollidable secondObj)
        {
            return firstObj.BoundingBox.Intersects(secondObj.BoundingBox);
        }

        // return true if the position collides with a tile
        public static bool CheckForCollisionWithAnyTiles(Rectangle boundingBox)
        {
            foreach (Tile tile in Engine.Tiles)
            {
                if (tile.Type != TileType.InnerGroundTile)
                {
                    if (boundingBox.Intersects(tile.BoundingBox) && tile.IsSolid)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool CheckForCollisionWithWalls(Rectangle boundingBox)
        {
            foreach (Tile tile in Engine.Tiles)
            {
                if (tile is WallTile && tile.Type != TileType.InnerGroundTile)
                {
                    if (boundingBox.Intersects(tile.BoundingBox) && tile.IsSolid)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool CheckForCollisionWithPlatform(Rectangle boundingBox)
        {
            foreach (Tile tile in Engine.Tiles)
            {
                if (tile.Type == TileType.StartPlatformTile 
                    || tile.Type == TileType.MiddlePlatformTile
                    || tile.Type == TileType.EndPlatformTile)
                {
                    if (boundingBox.Intersects(tile.BoundingBox) && tile.IsSolid)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool CheckForCollisionWithOtherThanPlatform(Rectangle boundingBox)
        {
            foreach (Tile tile in Engine.Tiles)
            {
                if (tile.Type != TileType.StartPlatformTile
                    && tile.Type != TileType.MiddlePlatformTile
                    && tile.Type != TileType.EndPlatformTile)
                {
                    if (boundingBox.Intersects(tile.BoundingBox) && tile.IsSolid)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        

    }
}
