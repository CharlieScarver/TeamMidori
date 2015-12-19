using Microsoft.Xna.Framework;
using Midori.Core;
using Midori.TextureLoading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.GameObjects.Tiles
{
    public class InnerGroundTile : Tile
    {
        public InnerGroundTile(Vector2 position)
        {
            this.Position = position;
            
            this.TextureWidth = 128;
            this.TextureHeight = 128;

            this.SpriteSheet = TextureLoader.InnerGroundTile;
            
            this.Type = TileType.InnerGroundTile;
                      
            this.IsSolid = false;
        }
    }
}
