using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Midori.Core;
using Midori.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.GameObjects.Units
{
    public class Unit : GameObject, Interfaces.IDrawable
    {
        private const int textureWidth = 32;
        private const int textureHeight = 64;
        private const int delay = 200;
        private const float defaultMovementSpeed = 1;

        private Vector2 position;
        private Texture2D spriteSheet;
        private Rectangle sourceRect;
        private int currentFrame;
        private double timer;
        public float movementSpeed;

        public Unit(Vector2 position)
        {
            this.position = position;
            this.sourceRect = new Rectangle(0, 192, textureWidth, textureHeight);
            this.currentFrame = 0;
            this.timer = 0.0;
            this.movementSpeed = Unit.defaultMovementSpeed;
        }

        public Vector2 Position 
        {
            get { return this.position; }
        }

        public float X
        {
            get { return this.position.X; }
            set
            {
                this.position.X = value;
            }
        }

        public float Y
        {
            get { return this.position.Y; }
            set
            {
                this.position.Y = value;
            }
        }

        //protected Texture2D SpriteSheet { get; set; }

        //protected Rectangle SourceRect { get; set; }

        //protected int CurrentFrame { get; set; }

        public void Load(ContentManager content)
        {
            this.spriteSheet = content.Load<Texture2D>("Sprites/old_guy");
        }

        public void Update(GameTime gameTime)
        {
            InputHandler.HandleInput(gameTime, this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.spriteSheet, this.Position, this.sourceRect, Color.White);
        }

        public void Animate(GameTime gameTime)
        {
            this.timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.timer >= Unit.delay)
            {
                this.currentFrame++;

                if (this.currentFrame == 6)
                {
                    this.currentFrame = 0;
                }

                this.sourceRect = new Rectangle(this.currentFrame * Unit.textureWidth, 192, Unit.textureWidth, Unit.textureHeight);
                this.timer = 0.0;
            }
        }
    }
}
