﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Midori.Core.TextureLoading;
using Midori.GameObjects.Units.PlayableCharacters;
using Midori.Interfaces;

namespace Midori.GameObjects.Items
{
    public class MoveBonusItem : TimedBonusItem
    {
        public MoveBonusItem(Texture2D sprite, Vector2 position) : base(sprite, position, ItemTypes.MoveBonus)
        {
            this.Duration = 10;
            this.Color = Color.DeepSkyBlue;
            this.IsTimedOut = false;
        }

    }
}
