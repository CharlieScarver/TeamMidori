using Microsoft.Xna.Framework;
using Midori.GameObjects.Units;
using Midori.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.GameObjects.Projectiles
{
    public abstract class Projectile : GameObject, IAnimatable, Interfaces.IUpdatable, IMoveable
    {
        public Projectile()
            : base()
        {
            this.Timer = 0.0;
            this.CurrentFrame = 0;
            this.SourceRect = new Rectangle();
        }

        # region Properties

        public bool IsMovingRight { get; set; }

        public int CurrentFrame { get; set; } //protected

        public int BasicAnimationFrameCount { get; protected set; }

        public double Timer { get; protected set; }

        public int Delay { get; protected set; }

        public Rectangle SourceRect { get; protected set; }

        public float MovementSpeed { get; set; }

        public Unit Owner { get; protected set; }

        #endregion 

        # region Methods

        // Non-abstract Methods
        protected void ManageMovement(GameTime gameTime)
        {
            // Left & Right Movement
            if (this.IsMovingRight)
            {
                this.X += this.MovementSpeed;
            }
            else
            {
                this.X -= this.MovementSpeed;
            }
        }

        // Abstract Methods
        public abstract void Update(GameTime gameTime);

        # endregion
    }
}
