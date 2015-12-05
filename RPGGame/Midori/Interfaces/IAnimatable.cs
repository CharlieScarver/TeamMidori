using Microsoft.Xna.Framework;
using Midori.GameObjects.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.Interfaces
{
    public interface IAnimatable : IDrawable
    {
        int CurrentFrame { get; }

        double Timer { get; }

        Rectangle SourceRect { get; }

        void AnimateRunningLeft(GameTime gameTime);

        void AnimateRunningRight(GameTime gameTime);

        void AnimateIdle(GameTime gameTime);

        void AnimateJumpRight(GameTime gameTime);

        void AnimateJumpLeft(GameTime gameTime);

        void AnimateFallRight(GameTime gameTime);

        void AnimateFallLeft(GameTime gameTime);
    }
}
