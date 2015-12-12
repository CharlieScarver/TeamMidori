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
        public static Texture2D TempHeroSheet { get; private set; }
        public static Texture2D GreenTileMiddle { get; private set; }
        public static Texture2D GreenTileStart { get; private set; }
        public static Texture2D GreenTileEnd { get; private set; }
        public static Texture2D Background { get; private set; }
        public static Texture2D TheOnePixel { get; private set; }
        public static Texture2D TempEnemySheet { get; private set; }
        public static Texture2D AyaSheet { get; private set; }
        public static Texture2D SmallAyaProjectileSheet { get; private set; }
        public static Texture2D DeathRayAyaSheet { get; private set; }
        public static Texture2D BushSheet { get; private set; }
        public static Texture2D GhostSheet { get; private set; }
        public static Texture2D GhostProjectile { get; private set; }
        

        public static SpriteFont Font { get; private set; }
        
        
        
        public static void Load(ContentManager content)
        {
            TempHeroSheet = content.Load<Texture2D>("Sprites/old_guy");
            GreenTileMiddle = content.Load<Texture2D>("Tiles/Main road");
            GreenTileStart = content.Load<Texture2D>("Tiles/End road left");
            GreenTileEnd = content.Load<Texture2D>("Tiles/End road right");
            Background = content.Load<Texture2D>("Background/bg edit");
            TheOnePixel = content.Load<Texture2D>("Sprites/TheOnePixel");
            TempEnemySheet = content.Load<Texture2D>("Sprites/enemy");
            AyaSheet = content.Load<Texture2D>("Sprites/AyaSheet 236x130");
            SmallAyaProjectileSheet = content.Load<Texture2D>("Sprites/projectiles 101 x 36");
            DeathRayAyaSheet = content.Load<Texture2D>("Sprites/death ray 13 x 98");
            BushSheet = content.Load<Texture2D>("Sprites/bush testing 128x128");
            GhostSheet = content.Load<Texture2D>("Sprites/ghost 128 x 128");
            GhostProjectile = content.Load<Texture2D>("Sprites/blob 100 x 50");
            

            Font = content.Load<SpriteFont>("Fonts/Font");
        }
    }
}
