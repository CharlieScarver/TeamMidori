using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Midori.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.GameObjects.Units
{
    public class TempHero : PlayableCharacter
    {
        private const int textureWidth = 32;
        private const int textureHeight = 64;
        private const int delay = 100;
        private const float defaultMovementSpeed = 3;
        private const int frameCount = 6;

        public TempHero(Vector2 position)
            : base(position, TempHero.defaultMovementSpeed, TempHero.textureWidth, TempHero.textureHeight, TempHero.delay, TempHero.frameCount)
        {

        }

        public override void Update(GameTime gameTime)
        {
            InputHandler.HandleInput(gameTime, this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.SpriteSheet, this.Position, this.SourceRect, Color.White);

        }

    }
}
