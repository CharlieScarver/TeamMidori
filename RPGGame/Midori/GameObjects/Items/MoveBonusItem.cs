using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Midori.TextureLoading;
using Midori.GameObjects.Units.PlayableCharacters;
using Midori.Interfaces;

namespace Midori.GameObjects.Items
{
    public class MoveBonusItem : TimedBonusItem
    {
        public MoveBonusItem(Vector2 position) : base(TextureLoader.SpeedBox, position, ItemTypes.MoveBonus)
        {
            this.Duration = 10;
            this.IsTimedOut = false;
            this.Color = Color.White;
        }

    }
}
