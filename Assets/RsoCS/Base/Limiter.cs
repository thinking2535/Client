using System;

namespace rso
{
    namespace Base
    {
        public class CLimiter
        {
            Int32 _LimitCount = 0;
            Int32 _AccCount = 0;
            TimeSpan _Period;
            DateTime _NextCheckTime;

            public CLimiter(TimeSpan Period_, Int32 LimitCount_)
            {
                _Period = Period_;
                _LimitCount = LimitCount_;
                _NextCheckTime = DateTime.Now + Period_;
            }
            public bool Set(Int32 Count_)
            {
                var Now = DateTime.Now;

                for (; _NextCheckTime < Now; _NextCheckTime = _NextCheckTime + _Period, _AccCount -= (_AccCount > _LimitCount ? _LimitCount : _AccCount)) ;

                if (_AccCount >= _LimitCount)
                    return false;

                _AccCount += Count_;

                return true;
            }
        }
    }
}