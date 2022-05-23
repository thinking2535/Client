using rso.core;
using System;

namespace rso.gameutil
{
    public class CLatency
    {
        readonly TimeSpan _MaxLatency;
        readonly TimeSpan _SubDuration = TimeSpan.FromTicks(100000);
        TimeSpan _Offset = TimeSpan.MinValue;
        TimeSpan _Latency = TimeSpan.Zero;

        public CLatency(TimeSpan MaxLatency_)
        {
            _MaxLatency = MaxLatency_;
        }
        public void Recv(TimePoint Time_, TimePoint RemoteTime_)
        {
            var Duration = RemoteTime_ - Time_;
            if (_Offset < Duration)
                _Offset = Duration;

            Duration = (Time_ + _Offset) - RemoteTime_;
            if (Duration > _MaxLatency)
                _Latency = _MaxLatency;
            else if (_Latency - Duration > _SubDuration)
                _Latency -= _SubDuration;
            else
                _Latency = Duration;
        }
        public bool Proc(TimePoint Time_, TimePoint RemoteTime_)
        {
            return Time_ + _Offset - _Latency >= RemoteTime_;
        }
        public TimeSpan GetOffset()
        {
            return _Offset;
        }
        public TimeSpan GetLatency()
        {
            return _Latency;
        }
    }
}