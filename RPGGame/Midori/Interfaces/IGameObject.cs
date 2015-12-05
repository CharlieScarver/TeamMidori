using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.Interfaces
{
    public interface IGameObject
    {
        int Id { get; set; }

        bool IsActive { get; set; }

        Vector2 Position { get; }

        float X { get; set; }

        float Y { get; set; }
    }
}
