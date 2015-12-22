using Microsoft.Xna.Framework;
using Midori.Core;
using Midori.GameObjects.Units;
using Midori.GameObjects.Units.Enemies;
using Midori.GameObjects.Units.PlayableCharacters;
using Midori.Interfaces;

namespace Midori.GameObjects.Projectiles
{
    public abstract class Projectile : GameObject, IProjectile
    {
        protected Projectile()
            : base()
        {
            this.Timer = 0.0;
            this.CurrentFrame = 0;
            this.SourceRect = new Rectangle();
        }

        # region Properties
        // IMovable
        public float MovementSpeed { get; protected set; }

        public float DefaultMovementSpeed { get; protected set; }

        // INeedToKnowEhereImFacing
        public bool IsFacingLeft { get; protected set; }

        // IOwned
        public Unit Owner { get; protected set; }

        // IAnimatable
        public int CurrentFrame { get; protected set; }

        public int BasicAnimationFrameCount { get; protected set; }

        public double Timer { get; protected set; }

        public int Delay { get; protected set; }

        public Rectangle SourceRect { get; protected set; }                   

        #endregion 

        # region Methods

        // Non-abstract Methods
        protected void ManageMovement(GameTime gameTime)
        {
            // Left & Right Movement
            if (this.IsFacingLeft)
            {
                this.X -= this.MovementSpeed;
            }
            else
            {
                this.X += this.MovementSpeed;
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            if (Collision.CheckForCollisionWithWorldBounds(this) || Collision.CheckForCollisionWithAnyTiles(this.BoundingBox))
            {
                this.Nullify();
            }
            else
            {
                this.ManageMovement(gameTime);

                this.BoundingBoxX = (int)this.X;
                this.BoundingBoxY = (int)this.Y;
            }            
        }

        # endregion


        
    }
}
