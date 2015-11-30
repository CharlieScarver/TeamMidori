using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Midori.GameObjects;

namespace Midori.Core
{
    public class Tile : GameObject, Midori.Interfaces.IDrawable
    {
        private bool isPassable;
        private Texture2D texture;
        private Vector2 position;
        private Rectangle boundingBox;
        private Rectangle sourceRect;
        private int textureWidth;
        private int textureHeight;

        public Tile(Texture2D texture, Vector2 position)
        {
            this.position = position;
            //this.defaultMovementSpeed = defaultMovementSpeed;
            this.textureWidth = 128;
            this.textureHeight = 128;
            this.boundingBox = new Rectangle((int)this.Position.X, (int)Position.Y - 12, 128, 128);
            //this.delay = delay;
            //this.frameCount = frameCount;
            this.texture = texture;
            this.sourceRect = new Rectangle(0, 192, this.textureWidth, this.textureHeight);
            //this.CurrentFrame = 0;
            //this.timer = 0.0;

            //this.MovementSpeed = defaultMovementSpeed;
        }

        public Vector2 Position
        {
            get
            {
                return this.position;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.Position);
        }
    }
}
