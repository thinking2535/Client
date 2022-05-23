using System;

namespace rso
{
    namespace core
    {
        public struct TimePoint
        {
            public Int64 Ticks;

            public TimePoint(Int64 Ticks_)
            {
                Ticks = Ticks_;
            }
            public TimePoint(DateTime DateTime_)
            {
                Ticks = DateTime_.ToTimePointTicks();
            }
            public TimePoint(string Time_, DateTimeKind DateTimeKind_ = DateTimeKind.Utc) :
                this(DateTime.SpecifyKind(Convert.ToDateTime(Time_), DateTimeKind_))
            {
            }
            public DateTime ToDateTime()
            {
                return (Extension.BaseDateTime() + new TimeSpan(Ticks)).ToLocalTime();
            }
            public static TimePoint Now
            {
                get
                {
                    return new TimePoint(DateTime.UtcNow);
                }
            }
            public override string ToString()
            {
                return ToDateTime().ToString();
            }
            public override bool Equals(object Obj_)
            {
                var p = (TimePoint)Obj_;

                return (this == p);
            }
            public override Int32 GetHashCode()
            {
                return (Int32)Ticks;
            }
            public static bool operator ==(TimePoint lhs_, TimePoint rhs_)
            {
                return (lhs_.Ticks == rhs_.Ticks);
            }
            public static bool operator !=(TimePoint lhs_, TimePoint rhs_)
            {
                return (lhs_.Ticks != rhs_.Ticks);
            }
            public static bool operator <(TimePoint lhs_, TimePoint rhs_)
            {
                return (lhs_.Ticks < rhs_.Ticks);
            }
            public static bool operator >(TimePoint lhs_, TimePoint rhs_)
            {
                return (lhs_.Ticks > rhs_.Ticks);
            }
            public static bool operator <=(TimePoint lhs_, TimePoint rhs_)
            {
                return (lhs_.Ticks <= rhs_.Ticks);
            }
            public static bool operator >=(TimePoint lhs_, TimePoint rhs_)
            {
                return (lhs_.Ticks >= rhs_.Ticks);
            }

            public static TimePoint operator +(TimePoint lhs_, TimeSpan rhs_)
            {
                return new TimePoint(lhs_.Ticks + rhs_.Ticks);
            }
            public static TimePoint operator -(TimePoint lhs_, TimeSpan rhs_)
            {
                return new TimePoint(lhs_.Ticks - rhs_.Ticks);
            }
            public static TimeSpan operator -(TimePoint lhs_, TimePoint rhs_)
            {
                return TimeSpan.FromTicks(lhs_.Ticks - rhs_.Ticks);
            }
            public static TimePoint FromTicks(Int64 Ticks_)
            {
                return new TimePoint(Ticks_);
            }
            public static TimePoint FromDateTime(DateTime DateTime_)
            {
                return new TimePoint(DateTime_);
            }
        }
    }
}