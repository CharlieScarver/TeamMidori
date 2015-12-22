using Microsoft.Xna.Framework;

namespace Midori.Interfaces
{
    public interface IAnimatableFalling : IAnimatable
    {
        void AnimateFall(GameTime gameTime);
    }
}
