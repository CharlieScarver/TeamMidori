using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Midori.Core;
using Midori.Interfaces;
using Midori.Core.TextureLoading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Midori.GameObjects.Units;
using Midori.GameObjects;

namespace Midori.DebugSystem
{
    public class MidoriDebug
    {
        public MidoriDebug(ContentManager content, SpriteBatch spriteBatch)
        {
            this.Content = content;
            this.SpriteBatch = spriteBatch;
            List<GameObject> gameObjects = Engine.Objects;
        }

        private SpriteBatch SpriteBatch { get; set; }

        private ContentManager Content { get; set; }

        private Point MousePosition { get { return new Point(Mouse.GetState().Position.X, Mouse.GetState().Position.Y + 20); } }


        public void UnitPosition(GameObject unit)
        {
            string text = string.Format("{0}/{1}", unit.X, unit.Y);
            SpriteBatch.DrawString(TextureLoader.Font, text, unit.Position, Color.White);
        }

        public void DrawMouseBB()
        {
            //SpriteBatch.Draw(TextureLoader.SingleWhitePixel, this.MouseBoundingBox, Color.White);
        }

        //TODO: Figure a way to get mouse position relative to the current position because
        //Mouse.GetState().Position returns point relative to the monitor
        public void StatsOnHover()
        {
            foreach (var item in Engine.Objects)
            {
                if (item.BoundingBox.Contains(this.MousePosition))
                {
                    this.UnitPosition(item);
                }
            }
            if (Engine.Player.BoundingBox.Contains(this.MousePosition))
            {
                this.UnitPosition(Engine.Player);
            }
        }
    }
}
