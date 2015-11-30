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
    }
}
