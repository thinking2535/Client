using System;
using System.Diagnostics;

namespace rso.gameutil
{
    public class CTick
    {
        protected Stopwatch _Stopwatch = new Stopwatch();
        protected Int64 _StartTick = 0;
        public CTick()
        {
        }
        public CTick(Int64 Tick_)
        {
            Set(Tick_);
        }
        public void Set(Int64 Tick_)
        {
            _StartTick = _Stopwatch.ElapsedTicks - Tick_;
        }
        public void Stop()
        {
            _Stopwatch.Stop();
        }
        public void Start()
        {
            _Stopwatch.Start();
        }
        public Int64 Get()
        {
            return _Stopwatch.ElapsedTicks - _StartTick;
        }
    }
}