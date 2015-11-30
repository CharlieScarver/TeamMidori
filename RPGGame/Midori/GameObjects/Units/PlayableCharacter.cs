using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.GameObjects.Units
{
    public abstract class PlayableCharacter : Unit
    {
       
        public PlayableCharacter(Vector2 position, float defaultMovementSpeed, int textureWidth, int textureHeight, int delay, int frameCount)
            : base(position, defaultMovementSpeed, textureWidth, textureHeight, delay, frameCount)
        {

        }

        
    }
}
