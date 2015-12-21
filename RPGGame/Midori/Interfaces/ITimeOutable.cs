using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Midori.Core;

namespace Midori.Interfaces
{
    public interface ITimeOutable
    {
        bool IsTimedOut { get; set; }

        int Duration { get; set; }

        CountDownTimer CountDownTimer { get; set; }
    }
}
