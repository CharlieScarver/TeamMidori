using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Midori.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Midori.GameObjects.Items;
using Midori.Interfaces;

namespace Midori.GameObjects.Units.PlayableCharacters
{
    public abstract class PlayableCharacter : Unit
    {
        public PlayableCharacter()
            : base()
        {
  
        }

        #region Properties 


        #endregion

        #region Methods

        public void ValidateMovementLeft()
        {
            if (!this.IsAttackingRanged)
            {
                if (this.HasFreePathing || Collision.CheckForCollisionWithTiles(this.BoundingBox))
                {
                    this.IsMovingLeft = true;
                    this.IsFacingLeft = true;
                }
                else
                {
                    this.FuturePosition = new Rectangle(
                            (int)(this.BoundingBox.X - this.MovementSpeed),
                            (int)this.BoundingBox.Y,
                            this.BoundingBox.Width,
                            this.BoundingBox.Height);
                    if (!Collision.CheckForCollisionWithTiles(this.FuturePosition))
                    {
                        this.IsMovingLeft = true;
                        this.IsFacingLeft = true;
                    }
                }
            }
        }

        public void ValidateMovementRight()
        {
            if (!this.IsAttackingRanged)
            {
                if (this.HasFreePathing || Collision.CheckForCollisionWithTiles(this.BoundingBox))
                {
                    this.IsMovingRight = true;
                    this.IsFacingLeft = false;
                }
                else
                {
                    // compensating because origin is in the left top corner
                    this.FuturePosition = new Rectangle(
                                (int)(this.BoundingBox.X + this.BoundingBox.Width + this.MovementSpeed),
                                (int)this.BoundingBox.Y,
                                this.BoundingBox.Width,
                                this.BoundingBox.Height);
                    if (!Collision.CheckForCollisionWithTiles(this.FuturePosition))
                    {
                        this.IsMovingRight = true;
                        this.IsFacingLeft = false;
                    }
                }
            }
        }

        public void ValidateJump()
        {
            if (!this.IsAttackingRanged
                && this.JumpCounter < 2)
            {
                this.IsFalling = false;

                this.IsJumping = true;
                this.JumpSpeed = this.DefaultJumpSpeed;
                this.JumpCounter++;
            }
        }

        public void ValidateMeleeAttack()
        {
            // TODO
        }

        public void ValidateRangedAttack()
        {
            if (!this.IsAttackingRanged
                && !this.IsJumping
                && !this.IsFalling)
            {
                this.IsMovingRight = false;
                this.IsMovingLeft = false;
                this.IsAttackingRanged = true;
            }
        }

        protected void CheckCollisionWithItems(GameTime gameTime)
        {
            foreach (var item in Engine.Items)
            {
                if (this.BoundingBox.Intersects(item.BoundingBox))
                {
                    this.PickUpItem(item, gameTime);
                    item.Nullify();
                }
            }
        }

        private void PickUpItem(Item item, GameTime gameTime)
        {
            if (item is TimedBonusItem)
            {
                Engine.PlayerTimedBonuses.Add((TimedBonusItem)item);
                foreach (var bonus in Engine.PlayerTimedBonuses)
                {
                    if (bonus == item)
                    {
                        bonus.CountDownTimer.SetTimer(gameTime, bonus.Duration);
                    }
                }
            }

            var itemType = item.Type;
            if (itemType == ItemTypes.Heal)
            {
                this.Health += 10;
            }
            else if (itemType == ItemTypes.MoveBonus)
            {
                this.MovementSpeed += 10;
            }
        }

        protected void RemoveTimedOutBonuses()
        {
            foreach (var bonus in Engine.PlayerTimedBonuses)
            {
                if (bonus.IsTimedOut)
                {
                    switch (bonus.Type)
                    {
                        case ItemTypes.MoveBonus:
                            this.MovementSpeed -= 10;
                            break;
                        case ItemTypes.AttackBonus:
                            this.DamageRanged -= 10;
                            break;
                    }
                }
            }
            Engine.PlayerTimedBonuses.RemoveAll(bonus => bonus.IsTimedOut);
        }

        #endregion

    }
}
