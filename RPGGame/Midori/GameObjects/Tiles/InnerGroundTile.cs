using Microsoft.Xna.Framework;
using Midori.Core;
using Midori.Enumerations;
using Midori.TextureLoading;

namespace Midori.GameObjects.Tiles
{
    public class InnerGroundTile : Tile
    {
        public InnerGroundTile(Vector2 position)
        {
            this.Position = position;          

            this.SpriteSheet = TextureLoader.InnerGroundTile;
            
            this.Type = TileType.InnerGroundTile;
                      
            this.IsSolid = false;
        }
    }
}
