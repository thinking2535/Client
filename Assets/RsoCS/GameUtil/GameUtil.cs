using System;

namespace rso.gameutil
{
    public class CGameUtil
    {
        public static string TickToMinuteSecondString(Int64 Tick_)
        {
            var Duration = TimeSpan.FromTicks(Tick_);
            var TotalSeconds = (Int32)Math.Ceiling(Duration.TotalSeconds);
            return (TotalSeconds / 60).ToString("00") + ":" + (TotalSeconds % 60).ToString("00");
        }
        public static string TickToHourMinuteSecondString(Int64 Tick_)
        {
            var Duration = TimeSpan.FromTicks(Tick_);
            var TotalSeconds = (Int32)Math.Ceiling(Duration.TotalSeconds);
            var TotalMinutes = TotalSeconds / 60;
            return (TotalMinutes / 60).ToString("00") + ":" + (TotalMinutes % 60).ToString("00") + ":" + (TotalSeconds % 60).ToString("00");
        }
    }
}
