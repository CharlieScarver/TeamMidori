
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Midori.GameObjects.Units.PlayableCharacters;

namespace Midori.Camera
{ 
    public class Camera2D
    {
        #region Fields

        private PlayableCharacter chaseTarget;
        private Matrix transform;
        private Vector2 position;
        private Vector2 velocity;
        private Viewport viewport;
        private Rectangle sceneBounds;

        private float zoom;
        private float rotation;

        private float mass = 150;
        private float stiffness = 1500;
        private float damping = 600;

        #endregion

        #region Constructor

        public Camera2D(GraphicsDevice device)
        {
            transform = Matrix.Identity;
            position = Vector2.Zero;
            chaseTarget = null;
            zoom = 0.99f;
            rotation = 0;

            viewport = device.Viewport;
        }

        #endregion

        #region Properties

        public Rectangle SceneBounds
        {
            get { return this.sceneBounds; }
        }

        public Matrix Transform
        {
            get
            {
                transform =
                Matrix.Identity *
                Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
                Matrix.CreateRotationZ(rotation) *
                Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
                Matrix.CreateTranslation(new Vector3(viewport.Width / 2, viewport.Height / 2, 0));

                return transform;
            }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Viewport Viewport
        {
            get { return viewport; }
        }
        public float Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
                if (zoom < 0.5f)
                    zoom = 0.5f;
                if (zoom > 0.99f)
                    zoom = 0.99f;
            }
        }
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        #endregion



        #region Initialization

        public void SetCameraPhysics(float mass, float damping, float stiffness)
        {
            this.mass = mass;
            this.damping = damping;
            this.stiffness = stiffness;
        }
        public void SetSceneBounds(Rectangle bounds)
        {
            this.sceneBounds = bounds;
        }
        public void SetChaseTarget(PlayableCharacter chaseTarget)
        {
            this.chaseTarget = chaseTarget;
        }

        #endregion

        #region Methods

        public void Clamp()
        {
            position.X = MathHelper.Clamp(position.X, sceneBounds.X + (viewport.Width / 2), sceneBounds.X + sceneBounds.Width - (viewport.Width / 2));
            position.Y = MathHelper.Clamp(position.Y, sceneBounds.Y + (viewport.Height / 2), sceneBounds.Y + sceneBounds.Height - (viewport.Height / 2));
        
        }
        public void Chase(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 stretch = position - chaseTarget.Position;
            Vector2 force = (stretch * -stiffness) - (velocity * damping);
            Vector2 acceleration = force / mass;

            velocity += acceleration * elapsed;
            position += velocity * elapsed;
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            if (sceneBounds != Rectangle.Empty)
            {
                Clamp();
                if (position.X < 0)
                {
                    position.X = 0;
                }
                if (position.Y < 0)
                {
                    position.Y = 0;
                }
            }

            if (chaseTarget != null)
            {
                Chase(gameTime);
            }

        
        }

        #endregion
    }

}
