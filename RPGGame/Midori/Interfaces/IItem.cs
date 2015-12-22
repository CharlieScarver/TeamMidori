using Microsoft.Xna.Framework;
using Midori.Enumerations;

namespace Midori.Interfaces
{
    public interface IItem : IAnimatable, IUpdatable
    {
        Color Color { get; }

        Rectangle FuturePosition { get; }

        ItemType Type { get; }
    }
}
