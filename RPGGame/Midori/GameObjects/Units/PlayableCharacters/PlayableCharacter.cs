using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.GameObjects.Units.PlayableCharacters
{
    public abstract class PlayableCharacter : Unit
    {
        private bool isFacingLeft;
        private bool isFacingRight;

        public PlayableCharacter()
            : base()
        {
            this.IsAttackingRanged = false;
            this.IsFacingLeft = false;
            this.IsFacingRight = true;
        }

        public bool IsAttackingRanged { get; set; }

        public bool IsFacingLeft 
        {
            get { return this.isFacingLeft; }
            set
            {
                if (value)
                {
                    this.isFacingRight = false;
                }
                else
                {
                    this.isFacingRight = true;
                }
                this.isFacingLeft = value;
            }
        }

        public bool IsFacingRight
        {
            get { return this.isFacingRight; }
            set
            {
                if (value)
                {
                    this.isFacingLeft = false;
                }
                else
                {
                    this.isFacingLeft = true;
                }
                this.isFacingRight = value;
            }
        }

    }
}
