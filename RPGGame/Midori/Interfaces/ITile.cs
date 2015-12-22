using Midori.Enumerations;

namespace Midori.Interfaces
{
    public interface ITile : IGameObject
    {
        bool IsSolid { get; }

        TileType Type { get; }
    }
}
