using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Midori.Core;
using Midori.TextureLoading;
using Midori.Interfaces;

namespace Midori.GameObjects.Items
{
    public class AttackBonusItem : TimedBonusItem
    {
        public AttackBonusItem( Vector2 position) : base(TextureLoader.AttackBox, position, ItemType.AttackBonus)
        {
            this.Duration = 10;
            this.IsTimedOut = false;
            this.Color = Color.White;
        }
    }
}
