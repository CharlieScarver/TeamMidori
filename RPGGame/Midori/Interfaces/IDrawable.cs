using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Midori.Interfaces
{
    public interface IDrawable
    {
        Vector2 Position { get; }

        float X { get; set; }

        float Y { get; set; }

        Texture2D SpriteSheet { get; }

        Rectangle SourceRect { get; }

        void Draw(SpriteBatch spriteBatch);
    }
}
