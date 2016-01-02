using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Midori.GameObjects;
using Midori.TextureLoading;
using Midori.Enumerations;
using Midori.Interfaces;

namespace Midori.GameObjects.Tiles
{
    public abstract class Tile : GameObject, ITile
    {
        private const int TileTextureWidth = 128;
        private const int TileTextureHeight = 128;

        protected Tile()
            : base()
        {
            this.TextureWidth = TileTextureWidth;
            this.TextureHeight = TileTextureHeight;
        }

        public bool IsSolid { get; protected set; }

        public TileType Type { get; protected set; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.SpriteSheet, this.Position, Color.Yellow);
        }

    }
}
