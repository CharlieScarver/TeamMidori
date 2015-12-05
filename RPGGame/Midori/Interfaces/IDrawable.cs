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
        Texture2D SpriteSheet { get; }

        void Draw(SpriteBatch spriteBatch);
    }
}
