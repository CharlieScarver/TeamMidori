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
        private static Rectangle futurePosition;

        public static void HandleInput(GameTime gameTime, PlayableCharacter unit)
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.GetPressedKeys().Length == 0)
            {
                unit.AnimateIdle(gameTime);
            }

            if (currentKeyboardState != previousKeyboardState)
            {
                unit.CurrentFrame = 0;
                unit.isMovingRight = false;
                unit.isMovingLeft = false;
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
                Jump(gameTime, unit);                
            }
            
        }

        private static void MoveRight(GameTime gameTime, PlayableCharacter unit)
        {
            if (unit.HasFreePathing || World.CheckForCollisionWithTiles(unit.BoundingBox))
            {
                unit.isMovingRight = true;

                if (!unit.IsJumping && !unit.IsFalling)
                {
                    unit.AnimateRunningRight(gameTime);
                }
            }
            else
            {
                // compensating because origin is in the left top corner
                futurePosition = new Rectangle(
                            (int)(unit.Position.X + unit.BoundingBox.Width + unit.MovementSpeed),
                            (int)unit.Position.Y,
                            unit.BoundingBox.Width,
                            unit.BoundingBox.Height);
                if (!World.CheckForCollisionWithTiles(futurePosition))
                {
                    unit.isMovingRight = true;
                    
                    if (!unit.IsJumping && !unit.IsFalling)
                    {
                        unit.AnimateRunningRight(gameTime);
                    }
                }
            }
        }

        private static void MoveLeft(GameTime gameTime, PlayableCharacter unit)
        {
            if (unit.HasFreePathing || World.CheckForCollisionWithTiles(unit.BoundingBox))
            {
                unit.isMovingLeft = true;

                if (!unit.IsJumping && !unit.IsFalling)
                { 
                    unit.AnimateRunningLeft(gameTime);
                }
            }
            else
            {
                futurePosition = new Rectangle(
                        (int)(unit.Position.X - unit.MovementSpeed),
                        (int)unit.Position.Y,
                        unit.BoundingBox.Width,
                        unit.BoundingBox.Height);
                if (!World.CheckForCollisionWithTiles(futurePosition))
                {
                    unit.isMovingLeft = true;

                    if (!unit.IsJumping && !unit.IsFalling)
                    {
                        unit.AnimateRunningLeft(gameTime);
                    }
                }
            }
        }

        private static void Jump(GameTime gameTime, Unit unit)
        {
            unit.IsFalling = false;

            unit.IsJumping = true;
            unit.JumpSpeed = unit.DefaultJumpSpeed;
            unit.JumpCounter++;

        }

    }
}
