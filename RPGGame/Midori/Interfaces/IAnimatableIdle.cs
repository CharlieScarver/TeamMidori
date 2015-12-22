using Microsoft.Xna.Framework;

namespace Midori.Interfaces
{
    public interface IAnimatableIdle : IAnimatable
    {        
        void AnimateIdle(GameTime gameTime);        
    }
}
