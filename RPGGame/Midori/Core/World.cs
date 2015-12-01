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
        public static bool isCollidedWithWorldBounds(GraphicsDeviceManager graphics, Unit unit)
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

            return firstObj.FutureBoundingBox.Intersects(secondObj.FutureBoundingBox);
        }

        public static bool ValidateFuturePosition(Rectangle futurePosition)
        {
            foreach (Tile tile in Engine.Tiles)
            {
                if (futurePosition.Intersects(tile.BoundingBox))
                {
                    return false;
                }
            }

            return true;
        }


        public static void Gravity(List<Unit> units)
        {
            foreach (Unit unit in units)
            {
                unit.Y += 2;
            }
        }
        
    }
}
