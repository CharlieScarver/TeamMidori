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
                unit.X = 0;
                return true;
            }
            if (unit.X < 0)
            {
                unit.X = graphics.GraphicsDevice.Viewport.Width;
                return true;
            }
            return false;
        }

        public static bool CollidesWith(ICollidable firstObj, ICollidable secondObj)
        {
            return firstObj.BoundingBox.Intersects(secondObj.BoundingBox);
        }

        
    }
}
