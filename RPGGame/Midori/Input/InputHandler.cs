using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Midori.GameObjects.Units.PlayableCharacters;
using Midori.Interfaces;

namespace Midori.Input
{
    public static class InputHandler
    {
        private static KeyboardState previousKeyboardState;
        private static KeyboardState currentKeyboardState;

        public static void HandleInput(GameTime gameTime, PlayableCharacter unit)
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            //if (currentKeyboardState.GetPressedKeys().Length == 0)
            //{
            //    // Idle
            //}

            //Reset any currently ongoing animation
            if (currentKeyboardState.IsKeyDown(Keys.R) && previousKeyboardState.IsKeyUp(Keys.R))
            {
                unit.ResetAnimation();
            }

            if (currentKeyboardState != previousKeyboardState)
            {
                if (!unit.IsAttackingRanged)
                {
                    //unit.ResetAnimationCounter();
                }
                unit.MakeUnitIdle();
            }

            // Move Right
            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {                 
                unit.ValidateMovementRight();             
            }

            // Move Left
            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {                
                unit.ValidateMovementLeft();
            }

            // Jumping
            if (currentKeyboardState.IsKeyDown(Keys.Up) 
                && previousKeyboardState.IsKeyUp(Keys.Up))
            { 
                unit.ValidateJump();
            }

            // RangedAttack          

            if (currentKeyboardState.IsKeyDown(Keys.A)
                && currentKeyboardState.IsKeyDown(Keys.S)
                && previousKeyboardState.IsKeyUp(Keys.S))
            {
                unit.ComboStageCounter += 2;
                unit.ValidateRangedAttack();
            }
            else if (currentKeyboardState.IsKeyDown(Keys.A)
               && previousKeyboardState.IsKeyUp(Keys.A))
            {
                unit.ValidateRangedAttack();
            }

        }

    }
}
