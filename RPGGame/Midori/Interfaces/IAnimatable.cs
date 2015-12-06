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

        int BasicAnimationFrameCount { get; }

        double Timer { get; }

        int Delay { get; }

        Rectangle SourceRect { get; }

    }
}
