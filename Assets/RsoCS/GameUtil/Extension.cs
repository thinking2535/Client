using System;
using rso.core;

namespace rso
{
    namespace gameutil
    {
        public static class Extension
		{
            public static bool IsInBoost(this STimeBoost Boost_, TimePoint Now_)
            {
	            return (Now_ < Boost_.EndTime);
            }
			public static TimeSpan GetRealDuration(TimeSpan BoostedDuration_, double Speed_)
			{
				return TimeSpan.FromTicks((Int64)(BoostedDuration_.Ticks / Speed_));
			}
			public static TimeSpan GetBoostedDuration(this STimeBoost Boost_, TimePoint Now_)
			{
                return TimeSpan.FromTicks((Int64)((Boost_.EndTime - Now_).Ticks * Boost_.Speed));
			}
            public static TimeSpan GetBoostedDuration(this STimeBoost Boost_, TimePoint BeginTime_, TimePoint EndTime_)
            {
                if (EndTime_ <= BeginTime_)
                    return TimeSpan.Zero;

                if (Boost_.EndTime < BeginTime_)
                {
                    return EndTime_ - BeginTime_;
                }
                else
                {
                    if (Boost_.EndTime < EndTime_)
                        return TimeSpan.FromTicks((Int64)((Boost_.EndTime - BeginTime_).Ticks * Boost_.Speed) + (EndTime_ - Boost_.EndTime).Ticks);
                    else
                        return TimeSpan.FromTicks((Int64)((EndTime_ - BeginTime_).Ticks * Boost_.Speed));
                }
            }
			public static TimePoint GetBoostedEndTime(this STimeBoost Boost_, TimePoint BeginTime_, TimeSpan BoostedDuration_)
			{
				if (BoostedDuration_.Ticks <= 0)
					return BeginTime_;

				if (Boost_.EndTime < BeginTime_)
				{
					return BeginTime_ + BoostedDuration_;
				}
				else
				{
					var BoostDuration = TimeSpan.FromTicks((Int64)((Boost_.EndTime - BeginTime_).Ticks * Boost_.Speed));
					if (BoostedDuration_ <= BoostDuration)
						return BeginTime_ + TimeSpan.FromTicks((Int64)(BoostedDuration_.Ticks / Boost_.Speed));
					else
						return Boost_.EndTime + (BoostedDuration_ - BoostDuration);
				}
			}
		    public static bool ChangeBeginTime(this STimeBoost Boost_, TimePoint Now_, double Speed_, ref TimePoint BeginTime_)
		    {
			    if (Boost_.IsInBoost(Now_))
				    return false;

			    if (BeginTime_ <= Boost_.EndTime)
				    BeginTime_ = Now_ - (Boost_.EndTime - BeginTime_) - GetRealDuration(Now_ - Boost_.EndTime, Speed_);
			    else
				    BeginTime_ = Now_ - GetRealDuration(Now_ - BeginTime_, Speed_);

			    return true;
		    }
            public static string ToString(STimeBoost Boost_)
            {
                return " EndTime:" + Boost_.EndTime.ToString() + " Speed:" + Boost_.Speed;
            }
        }
    }
}
