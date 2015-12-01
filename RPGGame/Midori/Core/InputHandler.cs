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

            if (currentKeyboardState.GetPressedKeys().Length == 0)
            {
                unit.AnimateIdle();
            }

            if (currentKeyboardState != previousKeyboardState)
            {
                unit.CurrentFrame = 0;
            }

            // Move Right
            if (currentKeyboardState.IsKeyDown(Keys.Right))
            { 
                
                MoveRight(gameTime, unit);
                              
            }

            // Move Left
            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                
                MoveLeft(gameTime, unit);
                
            }

            // Jumping
            if (currentKeyboardState.IsKeyDown(Keys.Up) 
                && previousKeyboardState.IsKeyUp(Keys.Up) 
                && (unit.JumpCounter < 2) )
            {
                
                unit.IsJumping = true;
                unit.IsFalling = false;
                unit.JumpSpeed = unit.DefaultJumpSpeed;
                unit.JumpCounter++;
                
            }

            
            
        }

        private static void MoveRight(GameTime gameTime, Unit unit)
        {
            // compensating because origin is in the left top corner
            var futurePosition = new Rectangle(
                        (int)(unit.Position.X + unit.BoundingBox.Width + unit.MovementSpeed),
                        (int)unit.Position.Y,
                        unit.BoundingBox.Width,
                        unit.BoundingBox.Height);
            if (World.ValidateFuturePosition(futurePosition))
            {
                //unit.IsMovingRight = true;
                unit.X += unit.MovementSpeed;
                unit.AnimateRight(gameTime);
            }
        }

        private static void MoveLeft(GameTime gameTime, Unit unit)
        {
            var futurePosition = new Rectangle(
                        (int)(unit.Position.X - unit.MovementSpeed),
                        (int)unit.Position.Y,
                        unit.BoundingBox.Width,
                        unit.BoundingBox.Height);
            if (World.ValidateFuturePosition(futurePosition))
            {
                unit.X -= unit.MovementSpeed;
                unit.AnimateLeft(gameTime);
            }
        }

    }
}
