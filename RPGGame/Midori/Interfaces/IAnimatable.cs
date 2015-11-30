using Midori.GameObjects.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.Interfaces
{
    public interface IAnimatable : IDrawable
    {
        void Animate(Unit unit);
    }
}
