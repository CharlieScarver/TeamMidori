using Microsoft.Xna.Framework;

namespace Midori.Interfaces
{
    public interface IGameObject : ICollidable, IDrawable
    {
        int Id { get; set; }

        bool IsActive { get; set; }

        Vector2 Position { get; }

        float X { get; set; }

        float Y { get; set; }

        void Nullify();
    }
}
