using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.Interfaces
{
    public interface ICollidable
    {
        Rectangle BoundingBox { get; }

        int BoundingBoxX { get; set; }

        int BoundingBoxY { get; set; }

        Rectangle FutureBoundingBox { get; }

        int FutureBoundingBoxX { get; set; }

        int FutureBoundingBoxY { get; set; }
    }
}
