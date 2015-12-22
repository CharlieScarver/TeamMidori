using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Midori.TextureLoading
{
    public static class TextureLoader
    {
        public static Texture2D TempHeroSheet { get; private set; }

        public static Texture2D GreenTileMiddle { get; private set; }
        public static Texture2D GreenTileStart { get; private set; }
        public static Texture2D GreenTileEnd { get; private set; }
        public static Texture2D GroundTileMiddle { get; private set; }
        public static Texture2D CornerTileLeft { get; private set; }
        public static Texture2D CornerTileRight { get; private set; }
        public static Texture2D RightWallTile { get; private set; }
        public static Texture2D LeftWallTile { get; private set; }
        public static Texture2D GroundTileEnd { get; private set; }
        public static Texture2D GroundTileStart { get; private set; }
        public static Texture2D InnerGroundTile { get; private set; }

        public static Texture2D Background { get; private set; }
        public static Texture2D TheOnePixel { get; private set; }
        public static Texture2D TempEnemySheet { get; private set; }
        public static Texture2D MidoriSheet { get; private set; }
        public static Texture2D MidoriSmallProjectileSheet { get; private set; }
        public static Texture2D RaySprite { get; private set; }
        public static Texture2D BushSheet { get; private set; }
        public static Texture2D GhostSheet { get; private set; }
        public static Texture2D GhostProjectileSheet { get; private set; }
        public static Texture2D Box { get; private set; }
        public static Texture2D AttackBox { get; private set; }
        public static Texture2D SpeedBox { get; private set; }
        public static Texture2D HealthBox { get; private set; }

        public static SpriteFont Font { get; private set; }
        
        
        
        public static void Load(ContentManager content)
        {
            TempHeroSheet = content.Load<Texture2D>("Sprites/old_guy");
            GreenTileMiddle = content.Load<Texture2D>("Tiles/_");
            GreenTileStart = content.Load<Texture2D>("Tiles/(");
            GreenTileEnd = content.Load<Texture2D>("Tiles/)");
            GroundTileMiddle = content.Load<Texture2D>("Tiles/-");
            GroundTileEnd = content.Load<Texture2D>("Tiles/]");
            GroundTileStart = content.Load<Texture2D>("Tiles/[");
            CornerTileRight = content.Load<Texture2D>("Tiles/{");
            CornerTileLeft = content.Load<Texture2D>("Tiles/}");
            RightWallTile = content.Load<Texture2D>("Tiles/i");
            LeftWallTile = content.Load<Texture2D>("Tiles/!");
            InnerGroundTile = content.Load<Texture2D>("Tiles/'");

            Background = content.Load<Texture2D>("Background/bg edit");
            TheOnePixel = content.Load<Texture2D>("Sprites/TheOnePixel");

            TempEnemySheet = content.Load<Texture2D>("Sprites/enemy");

            MidoriSheet = content.Load<Texture2D>("Sprites/AyaSheet 236x130");
            MidoriSmallProjectileSheet = content.Load<Texture2D>("Sprites/projectiles 101 x 36");
            RaySprite = content.Load<Texture2D>("Sprites/beam 137 x 120");
            
            BushSheet = content.Load<Texture2D>("Sprites/bush 128 x 128");
            
            GhostSheet = content.Load<Texture2D>("Sprites/ghost 128 x 128");
            GhostProjectileSheet = content.Load<Texture2D>("Sprites/blob both ways 100 x 50");
            
            Box = content.Load<Texture2D>("Sprites/box");
            SpeedBox = content.Load<Texture2D>("Sprites/speed crate blue 40 x 40");
            AttackBox = content.Load<Texture2D>("Sprites/attack crate 40 x 40");
            HealthBox = content.Load<Texture2D>("Sprites/health crate new 40 x 40");

            Font = content.Load<SpriteFont>("Fonts/Font");
        }
    }
}
