﻿using Microsoft.Xna.Framework;
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
        public event EventHandler PlayerIsDead;

        public PlayableCharacter()
            : base()
        {
  
        }

        #region Properties 

        protected CountDownTimer ComboTimer { get; set; }

        #endregion

        #region Methods

        public void OnPlayerDeath()
        {
            if (this.PlayerIsDead != null)
            {
                this.PlayerIsDead(this, EventArgs.Empty);
            }
        }

        public void ValidateMovementLeft()
        {
            if (!this.IsAttackingRanged)
            {                
                this.IsMovingLeft = true;
                this.IsFacingLeft = true;                
            }
        }

        public void ValidateMovementRight()
        {
            if (!this.IsAttackingRanged)
            {                
                this.IsMovingRight = true;
                this.IsFacingLeft = false;                
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

        protected void PickUpItem(GameTime gameTime, Item item)
        {
            if (item != null)
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

                if (itemType == ItemType.Heal)
                {
                    this.Health += 10;
                }
                else if (itemType == ItemType.MoveBonus)
                {
                    this.MovementSpeed += 10;
                    this.Delay -= 25;
                }
                else if (itemType == ItemType.AttackBonus)
                {
                    this.DamageRanged += 10;
                }
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
                        case ItemType.MoveBonus:
                            this.MovementSpeed -= 10;
                            this.Delay += 25;
                            break;
                        case ItemType.AttackBonus:
                            this.DamageRanged -= 10;
                            break;
                    }
                }
            }
            Engine.PlayerTimedBonuses.RemoveAll(bonus => bonus.IsTimedOut);
        }

        #endregion

        public override void Update(GameTime gameTime)
        {
            if (this.Health <= 0)
            {
                this.OnPlayerDeath();
                this.Nullify();
            }
        }

    }
}
