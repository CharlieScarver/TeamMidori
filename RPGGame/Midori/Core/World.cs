using Microsoft.Xna.Framework;
using Midori.GameObjects.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Midori.Interfaces;

namespace Midori.Core
{
    public static class World
    {
        public static bool CheckForCollisionWithWorldBounds(GraphicsDeviceManager graphics, Unit unit)
        {
            if (unit.X > graphics.GraphicsDevice.Viewport.Width)
            {
                unit.X = -30;
                return true;
            }
            if (unit.X < -30)
            {
                unit.X = graphics.GraphicsDevice.Viewport.Width;
                return true;
            }

            if (unit.Y > graphics.GraphicsDevice.Viewport.Height)
            {
                unit.Y = -70;
                return true;
            }
            return false;
        }

        public static bool CollidesWith(ICollidable firstObj, ICollidable secondObj)
        {
            return firstObj.BoundingBox.Intersects(secondObj.BoundingBox);
        }

        // return true if the position collides with a tile
        public static bool CheckForCollisionWithTiles(Rectangle boundingBox)
        {
            foreach (Tile tile in Engine.Tiles)
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
