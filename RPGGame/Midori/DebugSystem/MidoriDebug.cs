using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Midori.Core;
using Midori.Core.TextureLoading;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Midori.GameObjects;
using System;
using Midori.GameObjects.Units;

namespace Midori.DebugSystem
{
    public class MidoriDebug
    {
        private int indent;
        private MouseState currentMouseState;
        private MouseState previousMouseState;

        public MidoriDebug(ContentManager content, SpriteBatch spriteBatch)
        {
            this.Content = content;
            this.SpriteBatch = spriteBatch;
            List<GameObject> gameObjects = new List<GameObject>(Engine.Objects);
            this.AnchoredObjects = new LinkedList<DebugObject>();
            this.indent = 10;
        }

        private LinkedList<DebugObject> AnchoredObjects { get; set; }

        private SpriteBatch SpriteBatch { get; set; }

        private ContentManager Content { get; set; }

        private Point MousePosition { get { return new Point(Mouse.GetState().Position.X, Mouse.GetState().Position.Y); } }


        public void HoverDrawing(GameObject obj)
        {
            SpriteBatch.DrawString(TextureLoader.Font, DebugObject.toString(obj), obj.Position, Color.White);
        }

        public void AnchorDrawing(DebugObject obj)
        {
            SpriteBatch.Draw(
                texture: TextureLoader.TheOnePixel, 
                destinationRectangle: new Rectangle((int)obj.Position.X, 
                                        (int)obj.Position.Y,
                                        (int)TextureLoader.Font.MeasureString(obj.ToString()).X, 
                                        (int)TextureLoader.Font.MeasureString(obj.ToString()).Y),
                color: Color.Black * 0.4f);

            SpriteBatch.DrawString(TextureLoader.Font, obj.ToString(), obj.Position, Color.White);
            SpriteBatch.DrawString(TextureLoader.Font, "Anchored", obj.item.Position, Color.Crimson);

            SpriteBatch.Draw(
                texture: TextureLoader.TheOnePixel,
                destinationRectangle: new Rectangle((int)obj.item.Position.X,
                                        (int)obj.item.Position.Y,
                                        obj.item.TextureWidth,
                                        obj.item.TextureHeight),
                color: Color.Black * 0.4f);

            SpriteBatch.Draw(
                texture: TextureLoader.TheOnePixel,
                destinationRectangle: new Rectangle((int)obj.item.BoundingBoxX,
                                        (int)obj.item.BoundingBoxY,
                                        obj.item.BoundingBox.Width,
                                        obj.item.BoundingBox.Height),
                color: Color.Black * 0.4f);

        }

        //TODO: Figure a way to get mouse position relative to the current position because
        //if in window Mouse.GetState() returns point relative to the border of the window
        public void StatsOnHover()
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            foreach (var item in Engine.Objects)
            {
                if (item.BoundingBox.Contains(this.MousePosition))
                {
                    HoverDrawing(item);
                    if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
                    {
                        var debugObj = new DebugObject(item);
                        Anchor(debugObj);
                    }
                }
            }

            foreach (var item in AnchoredObjects)
            {
                AnchorDrawing(item);
            }
            MouseStats();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        private void Anchor(DebugObject item)
        {
            bool contains = this.AnchoredObjects.Any(p => p.Id == item.Id);
            if (!contains)
            {
                item.Position = new Vector2(this.indent, 0);
                this.AnchoredObjects.AddLast(new LinkedListNode<DebugObject>(item));
                this.indent += (int)TextureLoader.Font.MeasureString(item.LongestLine).X;

            }
            else
            {
                var toRemove = this.AnchoredObjects.First(e => e.Id == item.Id);
                this.indent -= (int)TextureLoader.Font.MeasureString(item.LongestLine).X;
                for (var i = AnchoredObjects.Last.Value; i != AnchoredObjects.Find(toRemove).Value; i = AnchoredObjects.Find(i).Previous.Value)
                {
                    i.Position = AnchoredObjects.Find(i).Previous.Value.Position;
                }
                this.AnchoredObjects.Remove(toRemove);
            }
            System.Diagnostics.Debug.WriteLine(indent);
        }

        private void MouseStats()
        {
            this.SpriteBatch.DrawString(TextureLoader.Font, this.MousePosition.ToString(), this.MousePosition.ToVector2(), Color.Black);
        }
    }
}