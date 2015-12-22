using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Midori.Timer;
using Midori.Interfaces;
using Midori.Enumerations;

namespace Midori.GameObjects.Items
{
    public class TimedBonusItem : Item, ITimeOutable
    {
        public TimedBonusItem(Texture2D sprite, Vector2 position, ItemType type) : base(sprite, position, type)
        {
            this.CountDownTimer = new CountDownTimer();
        }

        public bool IsTimedOut { get; set; }
        public int Duration { get; set; }
        public CountDownTimer CountDownTimer { get; set; }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
        }
    }
}
