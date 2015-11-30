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
        private static bool isJumping;

        public static void HandleInput(GameTime gameTime, Unit unit)
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.GetPressedKeys().Length == 0)
            {
                unit.Idle();
            }

            if (currentKeyboardState != previousKeyboardState)
            {
                unit.CurrentFrame = 0;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                MoveRight(gameTime, unit);                
            }

            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                MoveLeft(gameTime, unit);
            }

            if (isJumping)
            {
                unit.Y += unit.JumpSpeed;
                unit.JumpSpeed++;
                if (unit.Y >= 588)
                {
                    unit.Y = 588;
                    isJumping = false;
                }
            }
            else if (currentKeyboardState.IsKeyDown(Keys.Up) && previousKeyboardState.IsKeyUp(Keys.Up))
            {
                isJumping = true;
                unit.JumpSpeed = -10;
            }
            
        }

        private static void MoveRight(GameTime gameTime, Unit unit)
        {
            unit.X += unit.MovementSpeed;
            unit.AnimateRight(gameTime);
        }

        private static void MoveLeft(GameTime gameTime, Unit unit)
        {
            unit.X -= unit.MovementSpeed;
            unit.AnimateLeft(gameTime);
        }

    }
}
