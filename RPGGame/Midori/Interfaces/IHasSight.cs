using Microsoft.Xna.Framework;

namespace Midori.Interfaces
{
    public interface IHasSight
    {
        int Sight { get; }

        Rectangle SightRect { get; }
    }
}
