using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Midori.GameObjects.Units.PlayableCharacters;
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
                // Idle
            }

            if (currentKeyboardState != previousKeyboardState)
            {
                if (!unit.IsAttackingRanged)
                {
                    unit.CurrentFrame = 0;
                }
                unit.IsMovingRight = false;
                unit.IsMovingLeft = false;
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
                && previousKeyboardState.IsKeyUp(Keys.Up))
            {
                Jump(gameTime, unit);                
            }

            // RangedAttack
            if (currentKeyboardState.IsKeyDown(Keys.A)
                && previousKeyboardState.IsKeyUp(Keys.A))                
            {
                AttackRanged(gameTime, unit);
            }
            
        }

        private static void MoveRight(GameTime gameTime, PlayableCharacter unit)
        {
            if (!unit.IsAttackingRanged)
            {
                if (unit.HasFreePathing || World.CheckForCollisionWithTiles(unit.BoundingBox))
                {
                    unit.IsMovingRight = true;
                    unit.IsFacingRight = true;
                }
                else
                {
                    // compensating because origin is in the left top corner
                    futurePosition = new Rectangle(
                                (int)(unit.BoundingBox.X + unit.BoundingBox.Width + unit.MovementSpeed),
                                (int)unit.BoundingBox.Y,
                                unit.BoundingBox.Width,
                                unit.BoundingBox.Height);
                    if (!World.CheckForCollisionWithTiles(futurePosition))
                    {
                        unit.IsMovingRight = true;
                        unit.IsFacingRight = true;
                    }
                }
            }
        }

        private static void MoveLeft(GameTime gameTime, PlayableCharacter unit)
        {
            if (!unit.IsAttackingRanged)
            {
                if (unit.HasFreePathing || World.CheckForCollisionWithTiles(unit.BoundingBox))
                {
                    unit.IsMovingLeft = true;
                    unit.IsFacingLeft = true;
                }
                else
                {
                    futurePosition = new Rectangle(
                            (int)(unit.BoundingBox.X - unit.MovementSpeed),
                            (int)unit.BoundingBox.Y,
                            unit.BoundingBox.Width,
                            unit.BoundingBox.Height);
                    if (!World.CheckForCollisionWithTiles(futurePosition))
                    {
                        unit.IsMovingLeft = true;
                        unit.IsFacingLeft = true;
                    }
                }
            }
        }

        private static void Jump(GameTime gameTime, PlayableCharacter unit)
        {
            if (!unit.IsAttackingRanged 
                && unit.JumpCounter < 2)
            {
                unit.IsFalling = false;

                unit.IsJumping = true;
                unit.JumpSpeed = unit.DefaultJumpSpeed;
                unit.JumpCounter++;
            }
        }

        private static void AttackRanged(GameTime gameTime, PlayableCharacter unit)
        {
            if (!unit.IsAttackingRanged
                && !unit.IsJumping
                && !unit.IsFalling)
            {
                unit.IsAttackingRanged = true;
            }
        }

    }
}
