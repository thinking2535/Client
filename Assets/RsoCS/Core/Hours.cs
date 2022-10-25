using System;

namespace rso.core
{
    public struct Hours : IComparable<Hours>
    {
        const Int64 _ratio = 36000000000;
        public static Hours zero = new Hours();

        public Int64 ticks;

        public Hours(Int64 ticks)
        {
            this.ticks = ticks;
        }
        public Hours(TimeSpan duration)
        {
            ticks = duration.Ticks;
        }
        public static Hours fromValue(Int64 value)
        {
            return new Hours(value * _ratio);
        }
        public TimeSpan toTimeSpan()
        {
            return TimeSpan.FromTicks(ticks);
        }
        public override string ToString()
        {
            return toTimeSpan().ToString();
        }
        public override Int32 GetHashCode()
        {
            return (Int32)ticks;
        }
        public override bool Equals(object Obj_)
        {
            var p = (Hours)Obj_;
            return (this == p);
        }
        public Int32 CompareTo(Hours value)
        {
            if (ticks > value.ticks) return 1;
            else if (ticks < value.ticks) return -1;
            else return 0;
        }
        public Int64 value => ticks / _ratio;

        public static bool operator ==(Hours self, TimeSpan value)
        {
            return (self.ticks == value.Ticks);
        }
        public static bool operator !=(Hours self, TimeSpan value)
        {
            return (self.ticks != value.Ticks);
        }
        public static bool operator ==(Hours self, Microseconds value)
        {
            return (self.ticks == value.ticks);
        }
        public static bool operator !=(Hours self, Microseconds value)
        {
            return (self.ticks != value.ticks);
        }
        public static bool operator ==(Hours self, Milliseconds value)
        {
            return (self.ticks == value.ticks);
        }
        public static bool operator !=(Hours self, Milliseconds value)
        {
            return (self.ticks != value.ticks);
        }
        public static bool operator ==(Hours self, Seconds value)
        {
            return (self.ticks == value.ticks);
        }
        public static bool operator !=(Hours self, Seconds value)
        {
            return (self.ticks != value.ticks);
        }
        public static bool operator ==(Hours self, Minutes value)
        {
            return (self.ticks == value.ticks);
        }
        public static bool operator !=(Hours self, Minutes value)
        {
            return (self.ticks != value.ticks);
        }
        public static bool operator ==(Hours self, Hours value)
        {
            return (self.ticks == value.ticks);
        }
        public static bool operator !=(Hours self, Hours value)
        {
            return (self.ticks != value.ticks);
        }

        public static bool operator <(Hours self, TimeSpan value)
        {
            return (self.ticks < value.Ticks);
        }
        public static bool operator >(Hours self, TimeSpan value)
        {
            return (self.ticks > value.Ticks);
        }
        public static bool operator <(Hours self, Microseconds value)
        {
            return (self.ticks < value.ticks);
        }
        public static bool operator >(Hours self, Microseconds value)
        {
            return (self.ticks > value.ticks);
        }
        public static bool operator <(Hours self, Milliseconds value)
        {
            return (self.ticks < value.ticks);
        }
        public static bool operator >(Hours self, Milliseconds value)
        {
            return (self.ticks > value.ticks);
        }
        public static bool operator <(Hours self, Seconds value)
        {
            return (self.ticks < value.ticks);
        }
        public static bool operator >(Hours self, Seconds value)
        {
            return (self.ticks > value.ticks);
        }
        public static bool operator <(Hours self, Minutes value)
        {
            return (self.ticks < value.ticks);
        }
        public static bool operator >(Hours self, Minutes value)
        {
            return (self.ticks > value.ticks);
        }
        public static bool operator <(Hours self, Hours value)
        {
            return (self.ticks < value.ticks);
        }
        public static bool operator >(Hours self, Hours value)
        {
            return (self.ticks > value.ticks);
        }

        public static bool operator <=(Hours self, TimeSpan value)
        {
            return (self.ticks <= value.Ticks);
        }
        public static bool operator >=(Hours self, TimeSpan value)
        {
            return (self.ticks >= value.Ticks);
        }
        public static bool operator <=(Hours self, Microseconds value)
        {
            return (self.ticks <= value.ticks);
        }
        public static bool operator >=(Hours self, Microseconds value)
        {
            return (self.ticks >= value.ticks);
        }
        public static bool operator <=(Hours self, Milliseconds value)
        {
            return (self.ticks <= value.ticks);
        }
        public static bool operator >=(Hours self, Milliseconds value)
        {
            return (self.ticks >= value.ticks);
        }
        public static bool operator <=(Hours self, Seconds value)
        {
            return (self.ticks <= value.ticks);
        }
        public static bool operator >=(Hours self, Seconds value)
        {
            return (self.ticks >= value.ticks);
        }
        public static bool operator <=(Hours self, Minutes value)
        {
            return (self.ticks <= value.ticks);
        }
        public static bool operator >=(Hours self, Minutes value)
        {
            return (self.ticks >= value.ticks);
        }
        public static bool operator <=(Hours self, Hours value)
        {
            return (self.ticks <= value.ticks);
        }
        public static bool operator >=(Hours self, Hours value)
        {
            return (self.ticks >= value.ticks);
        }

        public static TimeSpan operator +(Hours self, TimeSpan value)
        {
            return new TimeSpan(self.ticks + value.Ticks);
        }
        public static TimeSpan operator -(Hours self, TimeSpan value)
        {
            return new TimeSpan(self.ticks - value.Ticks);
        }
        public static Microseconds operator +(Hours self, Microseconds value)
        {
            return new Microseconds(self.ticks + value.ticks);
        }
        public static Microseconds operator -(Hours self, Microseconds value)
        {
            return new Microseconds(self.ticks - value.ticks);
        }
        public static Milliseconds operator +(Hours self, Milliseconds value)
        {
            return new Milliseconds(self.ticks + value.ticks);
        }
        public static Milliseconds operator -(Hours self, Milliseconds value)
        {
            return new Milliseconds(self.ticks - value.ticks);
        }
        public static Seconds operator +(Hours self, Seconds value)
        {
            return new Seconds(self.ticks + value.ticks);
        }
        public static Seconds operator -(Hours self, Seconds value)
        {
            return new Seconds(self.ticks - value.ticks);
        }
        public static Minutes operator +(Hours self, Minutes value)
        {
            return new Minutes(self.ticks + value.ticks);
        }
        public static Minutes operator -(Hours self, Minutes value)
        {
            return new Minutes(self.ticks - value.ticks);
        }
        public static Hours operator +(Hours self, Hours value)
        {
            return new Hours(self.ticks + value.ticks);
        }
        public static Hours operator -(Hours self, Hours value)
        {
            return new Hours(self.ticks - value.ticks);
        }
    }
}