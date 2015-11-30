using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Midori.GameObjects.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midori.Core
{
    public static class InputHandler
    {
        private static KeyboardState previousKeyboardState;
        private static KeyboardState currentKeyboardState;

        public static void HandleInput(GameTime gameTime, Unit unit)
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState != previousKeyboardState)
            {
                unit.CurrentFrame = 0;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                unit.X += unit.MovementSpeed;
                unit.AnimateRight(gameTime);
            }
            else if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                unit.X -= unit.MovementSpeed;
                unit.AnimateLeft(gameTime);
            }
            
            
        }
    }
}
