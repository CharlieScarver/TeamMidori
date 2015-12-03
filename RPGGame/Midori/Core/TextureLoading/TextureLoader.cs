using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.Core.TextureLoading
{
    public static class TextureLoader
    {
        public static Texture2D TempHeroSheet { get; set; }
        public static Texture2D GreenTileMiddle { get; set; }
        public static Texture2D GreenTileStart { get; set; }
        public static Texture2D GreenTileEnd { get; set; }
        public static Texture2D Background { get; set; }
        public static Texture2D SingleWhitePixel { get; set; }
        public static SpriteFont Font { get; set; }

        public static Texture2D TempEnemySheet { get; set; }

        public static void Load(ContentManager content)
        {
            TempHeroSheet = content.Load<Texture2D>("Sprites/old_guy");
            GreenTileMiddle = content.Load<Texture2D>("Tiles/Main road");
            GreenTileStart = content.Load<Texture2D>("Tiles/End road left");
            GreenTileEnd = content.Load<Texture2D>("Tiles/End road right");
            Background = content.Load<Texture2D>("Background/bg edit");
            TempEnemySheet = content.Load<Texture2D>("Sprites/enemy");
            SingleWhitePixel = content.Load<Texture2D>("Sprites/TheOnePixel");
            Font = content.Load<SpriteFont>("Fonts/Font");
        }
    }
}
