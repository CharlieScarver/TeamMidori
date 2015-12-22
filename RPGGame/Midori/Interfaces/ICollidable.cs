using Microsoft.Xna.Framework;

namespace Midori.Interfaces
{
    public interface ICollidable
    {
        Rectangle BoundingBox { get; }

        int BoundingBoxX { get; set; }

        int BoundingBoxY { get; set; }

    }
}
