using System;

namespace rso.core
{
    public struct Seconds : IComparable<Seconds>
    {
        const Int64 _ratio = 10000000;
        public static Seconds zero = new Seconds();

        public Int64 ticks;

        public Seconds(Int64 ticks)
        {
            this.ticks = ticks;
        }
        public Seconds(TimeSpan duration)
        {
            ticks = duration.Ticks;
        }
        public static Seconds fromValue(Int64 value)
        {
            return new Seconds(value * _ratio);
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
            var p = (Seconds)Obj_;
            return (this == p);
        }
        public Int32 CompareTo(Seconds value)
        {
            if (ticks > value.ticks) return 1;
            else if (ticks < value.ticks) return -1;
            else return 0;
        }
        public Int64 value => ticks / _ratio;

        public static bool operator ==(Seconds self, TimeSpan value)
        {
            return (self.ticks == value.Ticks);
        }
        public static bool operator !=(Seconds self, TimeSpan value)
        {
            return (self.ticks != value.Ticks);
        }
        public static bool operator ==(Seconds self, Microseconds value)
        {
            return (self.ticks == value.ticks);
        }
        public static bool operator !=(Seconds self, Microseconds value)
        {
            return (self.ticks != value.ticks);
        }
        public static bool operator ==(Seconds self, Milliseconds value)
        {
            return (self.ticks == value.ticks);
        }
        public static bool operator !=(Seconds self, Milliseconds value)
        {
            return (self.ticks != value.ticks);
        }
        public static bool operator ==(Seconds self, Seconds value)
        {
            return (self.ticks == value.ticks);
        }
        public static bool operator !=(Seconds self, Seconds value)
        {
            return (self.ticks != value.ticks);
        }
        public static bool operator ==(Seconds self, Minutes value)
        {
            return (self.ticks == value.ticks);
        }
        public static bool operator !=(Seconds self, Minutes value)
        {
            return (self.ticks != value.ticks);
        }
        public static bool operator ==(Seconds self, Hours value)
        {
            return (self.ticks == value.ticks);
        }
        public static bool operator !=(Seconds self, Hours value)
        {
            return (self.ticks != value.ticks);
        }

        public static bool operator <(Seconds self, TimeSpan value)
        {
            return (self.ticks < value.Ticks);
        }
        public static bool operator >(Seconds self, TimeSpan value)
        {
            return (self.ticks > value.Ticks);
        }
        public static bool operator <(Seconds self, Microseconds value)
        {
            return (self.ticks < value.ticks);
        }
        public static bool operator >(Seconds self, Microseconds value)
        {
            return (self.ticks > value.ticks);
        }
        public static bool operator <(Seconds self, Milliseconds value)
        {
            return (self.ticks < value.ticks);
        }
        public static bool operator >(Seconds self, Milliseconds value)
        {
            return (self.ticks > value.ticks);
        }
        public static bool operator <(Seconds self, Seconds value)
        {
            return (self.ticks < value.ticks);
        }
        public static bool operator >(Seconds self, Seconds value)
        {
            return (self.ticks > value.ticks);
        }
        public static bool operator <(Seconds self, Minutes value)
        {
            return (self.ticks < value.ticks);
        }
        public static bool operator >(Seconds self, Minutes value)
        {
            return (self.ticks > value.ticks);
        }
        public static bool operator <(Seconds self, Hours value)
        {
            return (self.ticks < value.ticks);
        }
        public static bool operator >(Seconds self, Hours value)
        {
            return (self.ticks > value.ticks);
        }

        public static bool operator <=(Seconds self, TimeSpan value)
        {
            return (self.ticks <= value.Ticks);
        }
        public static bool operator >=(Seconds self, TimeSpan value)
        {
            return (self.ticks >= value.Ticks);
        }
        public static bool operator <=(Seconds self, Microseconds value)
        {
            return (self.ticks <= value.ticks);
        }
        public static bool operator >=(Seconds self, Microseconds value)
        {
            return (self.ticks >= value.ticks);
        }
        public static bool operator <=(Seconds self, Milliseconds value)
        {
            return (self.ticks <= value.ticks);
        }
        public static bool operator >=(Seconds self, Milliseconds value)
        {
            return (self.ticks >= value.ticks);
        }
        public static bool operator <=(Seconds self, Seconds value)
        {
            return (self.ticks <= value.ticks);
        }
        public static bool operator >=(Seconds self, Seconds value)
        {
            return (self.ticks >= value.ticks);
        }
        public static bool operator <=(Seconds self, Minutes value)
        {
            return (self.ticks <= value.ticks);
        }
        public static bool operator >=(Seconds self, Minutes value)
        {
            return (self.ticks >= value.ticks);
        }
        public static bool operator <=(Seconds self, Hours value)
        {
            return (self.ticks <= value.ticks);
        }
        public static bool operator >=(Seconds self, Hours value)
        {
            return (self.ticks >= value.ticks);
        }

        public static TimeSpan operator +(Seconds self, TimeSpan value)
        {
            return new TimeSpan(self.ticks + value.Ticks);
        }
        public static TimeSpan operator -(Seconds self, TimeSpan value)
        {
            return new TimeSpan(self.ticks - value.Ticks);
        }
        public static Microseconds operator +(Seconds self, Microseconds value)
        {
            return new Microseconds(self.ticks + value.ticks);
        }
        public static Microseconds operator -(Seconds self, Microseconds value)
        {
            return new Microseconds(self.ticks - value.ticks);
        }
        public static Milliseconds operator +(Seconds self, Milliseconds value)
        {
            return new Milliseconds(self.ticks + value.ticks);
        }
        public static Milliseconds operator -(Seconds self, Milliseconds value)
        {
            return new Milliseconds(self.ticks - value.ticks);
        }
        public static Seconds operator +(Seconds self, Seconds value)
        {
            return new Seconds(self.ticks + value.ticks);
        }
        public static Seconds operator -(Seconds self, Seconds value)
        {
            return new Seconds(self.ticks - value.ticks);
        }
        public static Seconds operator +(Seconds self, Minutes value)
        {
            return new Seconds(self.ticks + value.ticks);
        }
        public static Seconds operator -(Seconds self, Minutes value)
        {
            return new Seconds(self.ticks - value.ticks);
        }
        public static Seconds operator +(Seconds self, Hours value)
        {
            return new Seconds(self.ticks + value.ticks);
        }
        public static Seconds operator -(Seconds self, Hours value)
        {
            return new Seconds(self.ticks - value.ticks);
        }
    }
}