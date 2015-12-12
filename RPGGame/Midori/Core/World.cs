using Microsoft.Xna.Framework;
using Midori.GameObjects;
using Midori.GameObjects.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Midori.Interfaces;
using Midori.GameObjects.Tiles;


namespace Midori.Core
{
    public static class World
    {
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
        public static bool CheckForCollisionWithTiles(Rectangle boundingBox)
        {
            foreach (GroundTile tile in Engine.Tiles)
            {
                if (boundingBox.Intersects(tile.BoundingBox) && tile.IsSolid)
                {
                    return true;
                }
            }

            return false;
        }
        
    }
}
