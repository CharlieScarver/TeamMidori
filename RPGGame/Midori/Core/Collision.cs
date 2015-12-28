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

        public static bool CheckForCollisionWithWorldBounds(IGameObject obj)
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

        public static bool CheckForCollisionWithSightRect(ICollidable collidableObj, IHasSight hasSightObj)
        {
            return collidableObj.BoundingBox.Intersects(hasSightObj.SightRect);
        }

        public static bool CheckForCollisionBetweenCollidables(ICollidable firstObj, ICollidable secondObj)
        {
            return firstObj.BoundingBox.Intersects(secondObj.BoundingBox);
        }

        // return true if the bounding box collides with a tile
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

        public static bool CheckForCollisionWithPlatforms(Rectangle boundingBox)
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
