using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using Microsoft.Xna.Framework;

namespace Midori.Timer
{
    public class CountDownTimer
    {
        private int startCount;
        private int endCount;

        public CountDownTimer()
        {
            this.IsActive = false;
            this.IsComplete = true;
            this.startCount = 0;
            this.endCount = 0;
        }

        public void SetTimer(GameTime gameTime, int seconds)
        {
            this.startCount = gameTime.TotalGameTime.Seconds;
            this.endCount = this.startCount + seconds;
            this.IsActive = true;
            this.IsComplete = false;
        }

        public bool CheckTimer(GameTime gameTime)
        {
            if (!this.IsComplete)
            {
                if (gameTime.TotalGameTime.Seconds > this.startCount)
                {
                    if (gameTime.TotalGameTime.Seconds >= this.endCount)
                    {
                        this.endCount = 0;
                        this.IsComplete = true;
                    }
                }
            }
            return this.IsComplete;
        }

        public bool IsActive { get; private set; }
        public bool IsComplete { get; private set; }
    }
}
