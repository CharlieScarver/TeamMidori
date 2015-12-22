using Microsoft.Xna.Framework.Graphics;

namespace Midori.Interfaces
{
    public interface IDrawable
    {
        int TextureWidth { get; }

        int TextureHeight { get; }

        Texture2D SpriteSheet { get; }

        void Draw(SpriteBatch spriteBatch);
    }
}
