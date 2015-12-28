using Microsoft.Xna.Framework;

namespace Midori.Interfaces
{
    public interface IAnimatable : IDrawable
    {
        int CurrentFrame { get; }

        int BasicAnimationFrameCount { get; }

        double Timer { get; }

        int Delay { get; }

        Rectangle SourceRect { get; }               
    }
}
