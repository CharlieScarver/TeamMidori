//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Graphics;
//using Midori.Core;
//using Midori.Core.TextureLoading;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Midori.GameObjects.Units
//{
//    public class TempHero : PlayableCharacter
//    {
//        private const int textureWidth = 32;
//        private const int textureHeight = 64;
//        private const int delay = 200;
//        private const int frameCount = 6;
//        private const float defaultMovementSpeed = 10;
//        private const float defaultJumpSpeed = 21;

//        public TempHero(Vector2 position)
//            : base(position, TempHero.textureWidth, TempHero.textureHeight, TempHero.delay, TempHero.frameCount, TempHero.defaultMovementSpeed, TempHero.defaultJumpSpeed)
//        {
//            this.SpriteSheet = TextureLoader.TempHeroSheet;
//        }

//        public override void Update(GameTime gameTime)
//        {
//            InputHandler.HandleInput(gameTime, this);
//            ManageMovement();
//        }

//        public override void Draw(SpriteBatch spriteBatch)
//        {
//            spriteBatch.Draw(this.SpriteSheet, this.Position, this.SourceRect, Color.White);
//        }

//        public override void AnimateRight(GameTime gameTime)
//        {
//            this.Timer += gameTime.ElapsedGameTime.TotalMilliseconds;

//            if (this.Timer >= this.Delay)
//            {
//                this.CurrentFrame++;

//                if (this.CurrentFrame == this.FrameCount)
//                {
//                    this.CurrentFrame = 0;
//                }


//                this.Timer = 0.0;
//            }

//            this.SourceRect = new Rectangle(this.CurrentFrame * this.TextureWidth, this.TextureHeight * 3, this.TextureWidth, this.TextureHeight);
//        }

//        public override void AnimateLeft(GameTime gameTime)
//        {
//            this.Timer += gameTime.ElapsedGameTime.TotalMilliseconds;

//            if (this.Timer >= this.Delay)
//            {
//                this.CurrentFrame++;

//                if (this.CurrentFrame == this.FrameCount)
//                {
//                    this.CurrentFrame = 0;
//                }


//                this.Timer = 0.0;
//            }

//            this.SourceRect = new Rectangle(this.CurrentFrame * this.TextureWidth, this.TextureHeight * 2, this.TextureWidth, this.TextureHeight);
//        }

//        public override void AnimateIdle(GameTime gameTime)
//        {
//            this.SourceRect = new Rectangle(0, 0, this.TextureWidth, this.TextureHeight);
//        }

//        public override void AnimateJump(GameTime gameTime)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
