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
        int CurrentFrame { get; set; }

        int Delay { get; }

        double Timer { get; }

        int FrameCount { get; }

        void AnimateLeft(GameTime gameTime);

        void AnimateRight(GameTime gameTime);

        void AnimateIdle();

    }
}
