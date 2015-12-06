using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Midori.Core.TextureLoading;

namespace Midori.Core
{
    public class Camera
    {
        private Vector2 position;
        private Matrix viewMatrix;
        public Camera()
        {
            this.Zoom = 0.5f;
            this.Position = Vector2.Zero;
            this.Rotation = 0;
            this.Origin = Vector2.Zero;
            this.Position = Vector2.Zero;
        }
        public Matrix ViewMatrix
        {
            get
            {
                return viewMatrix;
            }

            set
            {
                viewMatrix = value;
            }
        }

        public int ViewportWidth { get; set; }
        public int ViewportHeight { get; set; }

        public Vector2 ViewportCenter
        {
            get
            {
                return new Vector2(ViewportWidth * 0.5f, ViewportHeight * 0.5f);
            }
        }

        public float Zoom { get; set; }
        public Vector2 Position { get { return this.position; } set { this.position = value; } }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }

        public void LookAt(GameObjects.GameObject item)
        {
            position.X = (item.Position.X + item.TextureWidth / 2) - (ViewportWidth / 2) / this.Zoom;
            position.Y = (item.Position.Y + item.TextureHeight /2) - (ViewportHeight / 2) / this.Zoom;

            if (Position.X < 0)
            {
                position.X = 0;
            }
            if (Position.Y < 0)
            {
                position.Y = 0;
            }

            ViewMatrix = Matrix.CreateTranslation(new Vector3(-position, 0)) *
                Matrix.CreateScale(this.Zoom);
        }
    }
}
