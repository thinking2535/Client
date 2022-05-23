using rso.core;
using rso.Base;
using System;
using System.Collections.Generic;

namespace rso
{
    namespace gameutil
    {
		public class PunishLimiter
		{
			struct _SLimiter
			{
				public CLimiter Limiter;
				public TimeSpan PunishPeriod;

				public _SLimiter(CLimiter Limiter_, TimeSpan PunishPeriod_)
				{
                    Limiter = Limiter_;
                    PunishPeriod = PunishPeriod_;
                }
            }
			List<_SLimiter>	_Limiters = new List<_SLimiter>();
			DateTime _PunishTime = DateTime.Now;

			public void Add(TimeSpan Period_, Int32 LimitCount_, TimeSpan PunishPeriod_)
			{
				_Limiters.Add(new _SLimiter(new CLimiter(Period_, LimitCount_), PunishPeriod_));
			}
			public TimeSpan Set(Int32 Count_)
			{
				var Now = DateTime.Now;

                var LeftDuration = (_PunishTime - Now);

                if (LeftDuration.Ticks > 0)
                    return LeftDuration;

				foreach (var i in _Limiters)
				{
					if (!i.Limiter.Set(Count_))
					{
						_PunishTime = Now + i.PunishPeriod;
						return i.PunishPeriod;
					}
				}

				return TimeSpan.Zero;
			}
		};
    }
}