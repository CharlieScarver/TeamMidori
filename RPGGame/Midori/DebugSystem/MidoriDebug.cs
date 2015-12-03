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
            this.AnchoredObjects = new List<GameObject>();
        }

        private List<GameObject> AnchoredObjects { get; set; }

        private SpriteBatch SpriteBatch { get; set; }

        private ContentManager Content { get; set; }

        private Point MousePosition { get { return new Point(Mouse.GetState().Position.X, Mouse.GetState().Position.Y + 20); } }


        public void UnitPosition(GameObject unit)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var prop in unit.GetType().GetProperties())
            {
                sb.Append(string.Format("{0}={1}", prop.Name, prop.GetValue(unit, null)));
                sb.Append("\n");
            }

            SpriteBatch.DrawString(TextureLoader.Font, sb.ToString(), new Vector2(unit.Position.X, unit.Position.Y - sb.Length/2), Color.White);
        }

        //TODO: Figure a way to get mouse position relative to the current position because
        //if in window Mouse.GetState() returns point relative to the border of the window
        public void StatsOnHover()
        {
            foreach (var item in Engine.Objects)
            {
                if (item.BoundingBox.Contains(this.MousePosition))
                {
                    this.UnitPosition(item);
                    this.Anchor(item);
                }
            }

            if (Engine.Player.BoundingBox.Contains(this.MousePosition))
            {
                this.UnitPosition(Engine.Player);
                this.Anchor(Engine.Player);
            }

            foreach (var item in this.AnchoredObjects)
            {
                this.UnitPosition(item);
            }
        }

        private void Anchor(GameObject item)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (!this.AnchoredObjects.Contains(item))
                {
                    this.AnchoredObjects.Add(item);
                }
                else
                {
                    this.AnchoredObjects.Remove(item);
                }
            }
        }
    }
}
