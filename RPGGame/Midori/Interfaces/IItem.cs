using Microsoft.Xna.Framework;
using Midori.Enumerations;

namespace Midori.Interfaces
{
    public interface IItem : IGameObject, IAnimatable, IUpdatable
    {
        Color Color { get; }

        Rectangle FuturePosition { get; }

        ItemType Type { get; }
    }
}
