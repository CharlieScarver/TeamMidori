using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.Interfaces
{
    public interface IAnimatableUnit : IAnimatable
    {
        void AnimateRunningLeft(GameTime gameTime);

        void AnimateRunningRight(GameTime gameTime);

        void AnimateIdle(GameTime gameTime);

        void AnimateJumpRight(GameTime gameTime);

        void AnimateJumpLeft(GameTime gameTime);

        void AnimateFallRight(GameTime gameTime);

        void AnimateFallLeft(GameTime gameTime);
    }
}
