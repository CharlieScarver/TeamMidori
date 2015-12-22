using Microsoft.Xna.Framework;

namespace Midori.Interfaces
{
    public interface IAnimatableMovable : IAnimatable
    {
        void AnimateRunning(GameTime gameTime);
    }
}
