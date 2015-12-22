using Midori.Timer;

namespace Midori.Interfaces
{
    public interface ITimeOutable
    {
        bool IsTimedOut { get; set; }

        int Duration { get; set; }

        CountDownTimer CountDownTimer { get; set; }
    }
}
