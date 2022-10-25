using System;

namespace rso.gameutil
{
    public class Limiter
    {
        readonly Int64 _durationTicks;
        Int64 _lastTick = 0;
        public Limiter(Int64 durationTicks)
        {
            _durationTicks = durationTicks;
        }
        public bool push(Int64 tick)
        {
            if (tick - _lastTick < _durationTicks)
                return false;

            _lastTick = tick;

            return true;
        }
    }
}
