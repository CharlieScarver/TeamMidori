using Microsoft.Xna.Framework;

namespace Midori.Interfaces
{
    public interface IAnimatableJumper : IAnimatable
    {
        void AnimateJump(GameTime gameTime);
    }
}
