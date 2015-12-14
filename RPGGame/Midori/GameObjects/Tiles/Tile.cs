using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Midori.GameObjects;
using Midori.Core.TextureLoading;

namespace Midori.Core
{
    public abstract class Tile : GameObject
    {

        public Tile()
            : base()
        {

        }

        public bool IsSolid { get; protected set; }

        public string Type { get; protected set; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.SpriteSheet, this.Position, Color.Yellow);
        }

    }
}
