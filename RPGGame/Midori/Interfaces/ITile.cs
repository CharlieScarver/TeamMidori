using Midori.Enumerations;

namespace Midori.Interfaces
{
    interface ITile
    {
        bool IsSolid { get; }

        TileType Type { get; }
    }
}
