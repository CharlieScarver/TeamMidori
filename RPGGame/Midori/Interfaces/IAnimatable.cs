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
        int CurrentFrameRunningAndIdle { get; set; }

        int CurrentFrameJump { get; set; } 

        int Delay { get; }

        double Timer { get; }

        int RunningAndIdleFrameCount { get; }

        int JumpFrameCount { get; }

        void AnimateLeft(GameTime gameTime);

        void AnimateRight(GameTime gameTime);

        void AnimateIdle(GameTime gameTime);

        void AnimateJumpRight(GameTime gameTime);

        void AnimateJumpLeft(GameTime gameTime);

    }
}
