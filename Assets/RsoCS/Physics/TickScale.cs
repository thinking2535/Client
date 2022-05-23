using System;

namespace rso.gameutil
{
    public class CTickScale : CTick
    {
        double _Scale = 1.0f;
        Int64 _AccTick = 0; // C++ 코드 통일 위함.
        public CTickScale()
        {
        }
        public CTickScale(Int64 Tick_) :
            base(Tick_)
        {
        }
        public CTickScale(double Scale_)
        {
            _Scale = Scale_;
        }
        public CTickScale(Int64 Tick_, double Scale_) :
            base(Tick_)
        {
            _Scale = Scale_;
        }
        public void SetScale(double Scale_)
        {
            _AccTick = Get();
            Set(0);
            _Scale = Scale_;
        }
        public new Int64 Get()
        {
            return _AccTick + (Int64)(base.Get() * _Scale);
        }
    }
}