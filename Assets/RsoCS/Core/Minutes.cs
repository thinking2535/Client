using System;

namespace rso.core
{
    public struct Minutes : IComparable<Minutes>
    {
        const Int64 _ratio = 600000000;
        public static Minutes zero = new Minutes();

        public Int64 ticks;

        public Minutes(Int64 ticks)
        {
            this.ticks = ticks;
        }
        public Minutes(TimeSpan duration)
        {
            ticks = duration.Ticks;
        }
        public static Minutes fromValue(Int64 value)
        {
            return new Minutes(value * _ratio);
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
            var p = (Minutes)Obj_;
            return (this == p);
        }
        public Int32 CompareTo(Minutes value)
        {
            if (ticks > value.ticks) return 1;
            else if (ticks < value.ticks) return -1;
            else return 0;
        }
        public Int64 value => ticks / _ratio;

        public static bool operator ==(Minutes self, TimeSpan value)
        {
            return (self.ticks == value.Ticks);
        }
        public static bool operator !=(Minutes self, TimeSpan value)
        {
            return (self.ticks != value.Ticks);
        }
        public static bool operator ==(Minutes self, Microseconds value)
        {
            return (self.ticks == value.ticks);
        }
        public static bool operator !=(Minutes self, Microseconds value)
        {
            return (self.ticks != value.ticks);
        }
        public static bool operator ==(Minutes self, Milliseconds value)
        {
            return (self.ticks == value.ticks);
        }
        public static bool operator !=(Minutes self, Milliseconds value)
        {
            return (self.ticks != value.ticks);
        }
        public static bool operator ==(Minutes self, Seconds value)
        {
            return (self.ticks == value.ticks);
        }
        public static bool operator !=(Minutes self, Seconds value)
        {
            return (self.ticks != value.ticks);
        }
        public static bool operator ==(Minutes self, Minutes value)
        {
            return (self.ticks == value.ticks);
        }
        public static bool operator !=(Minutes self, Minutes value)
        {
            return (self.ticks != value.ticks);
        }
        public static bool operator ==(Minutes self, Hours value)
        {
            return (self.ticks == value.ticks);
        }
        public static bool operator !=(Minutes self, Hours value)
        {
            return (self.ticks != value.ticks);
        }

        public static bool operator <(Minutes self, TimeSpan value)
        {
            return (self.ticks < value.Ticks);
        }
        public static bool operator >(Minutes self, TimeSpan value)
        {
            return (self.ticks > value.Ticks);
        }
        public static bool operator <(Minutes self, Microseconds value)
        {
            return (self.ticks < value.ticks);
        }
        public static bool operator >(Minutes self, Microseconds value)
        {
            return (self.ticks > value.ticks);
        }
        public static bool operator <(Minutes self, Milliseconds value)
        {
            return (self.ticks < value.ticks);
        }
        public static bool operator >(Minutes self, Milliseconds value)
        {
            return (self.ticks > value.ticks);
        }
        public static bool operator <(Minutes self, Seconds value)
        {
            return (self.ticks < value.ticks);
        }
        public static bool operator >(Minutes self, Seconds value)
        {
            return (self.ticks > value.ticks);
        }
        public static bool operator <(Minutes self, Minutes value)
        {
            return (self.ticks < value.ticks);
        }
        public static bool operator >(Minutes self, Minutes value)
        {
            return (self.ticks > value.ticks);
        }
        public static bool operator <(Minutes self, Hours value)
        {
            return (self.ticks < value.ticks);
        }
        public static bool operator >(Minutes self, Hours value)
        {
            return (self.ticks > value.ticks);
        }

        public static bool operator <=(Minutes self, TimeSpan value)
        {
            return (self.ticks <= value.Ticks);
        }
        public static bool operator >=(Minutes self, TimeSpan value)
        {
            return (self.ticks >= value.Ticks);
        }
        public static bool operator <=(Minutes self, Microseconds value)
        {
            return (self.ticks <= value.ticks);
        }
        public static bool operator >=(Minutes self, Microseconds value)
        {
            return (self.ticks >= value.ticks);
        }
        public static bool operator <=(Minutes self, Milliseconds value)
        {
            return (self.ticks <= value.ticks);
        }
        public static bool operator >=(Minutes self, Milliseconds value)
        {
            return (self.ticks >= value.ticks);
        }
        public static bool operator <=(Minutes self, Seconds value)
        {
            return (self.ticks <= value.ticks);
        }
        public static bool operator >=(Minutes self, Seconds value)
        {
            return (self.ticks >= value.ticks);
        }
        public static bool operator <=(Minutes self, Minutes value)
        {
            return (self.ticks <= value.ticks);
        }
        public static bool operator >=(Minutes self, Minutes value)
        {
            return (self.ticks >= value.ticks);
        }
        public static bool operator <=(Minutes self, Hours value)
        {
            return (self.ticks <= value.ticks);
        }
        public static bool operator >=(Minutes self, Hours value)
        {
            return (self.ticks >= value.ticks);
        }

        public static TimeSpan operator +(Minutes self, TimeSpan value)
        {
            return new TimeSpan(self.ticks + value.Ticks);
        }
        public static TimeSpan operator -(Minutes self, TimeSpan value)
        {
            return new TimeSpan(self.ticks - value.Ticks);
        }
        public static Microseconds operator +(Minutes self, Microseconds value)
        {
            return new Microseconds(self.ticks + value.ticks);
        }
        public static Microseconds operator -(Minutes self, Microseconds value)
        {
            return new Microseconds(self.ticks - value.ticks);
        }
        public static Milliseconds operator +(Minutes self, Milliseconds value)
        {
            return new Milliseconds(self.ticks + value.ticks);
        }
        public static Milliseconds operator -(Minutes self, Milliseconds value)
        {
            return new Milliseconds(self.ticks - value.ticks);
        }
        public static Seconds operator +(Minutes self, Seconds value)
        {
            return new Seconds(self.ticks + value.ticks);
        }
        public static Seconds operator -(Minutes self, Seconds value)
        {
            return new Seconds(self.ticks - value.ticks);
        }
        public static Minutes operator +(Minutes self, Minutes value)
        {
            return new Minutes(self.ticks + value.ticks);
        }
        public static Minutes operator -(Minutes self, Minutes value)
        {
            return new Minutes(self.ticks - value.ticks);
        }
        public static Minutes operator +(Minutes self, Hours value)
        {
            return new Minutes(self.ticks + value.ticks);
        }
        public static Minutes operator -(Minutes self, Hours value)
        {
            return new Minutes(self.ticks - value.ticks);
        }
    }
}