using System;

namespace rso.core
{
    public struct Milliseconds : IComparable<Milliseconds>
    {
        const Int64 _ratio = 10000;
        public static Milliseconds zero = new Milliseconds();

        public Int64 ticks;

        public Milliseconds(Int64 ticks)
        {
            this.ticks = ticks;
        }
        public Milliseconds(TimeSpan duration)
        {
            ticks = duration.Ticks;
        }
        public static Milliseconds fromValue(Int64 value)
        {
            return new Milliseconds(value * _ratio);
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
            var p = (Milliseconds)Obj_;
            return (this == p);
        }
        public Int32 CompareTo(Milliseconds value)
        {
            if (ticks > value.ticks) return 1;
            else if (ticks < value.ticks) return -1;
            else return 0;
        }
        public Int64 value => ticks / _ratio;

        public static bool operator ==(Milliseconds self, TimeSpan value)
        {
            return (self.ticks == value.Ticks);
        }
        public static bool operator !=(Milliseconds self, TimeSpan value)
        {
            return (self.ticks != value.Ticks);
        }
        public static bool operator ==(Milliseconds self, Microseconds value)
        {
            return (self.ticks == value.ticks);
        }
        public static bool operator !=(Milliseconds self, Microseconds value)
        {
            return (self.ticks != value.ticks);
        }
        public static bool operator ==(Milliseconds self, Milliseconds value)
        {
            return (self.ticks == value.ticks);
        }
        public static bool operator !=(Milliseconds self, Milliseconds value)
        {
            return (self.ticks != value.ticks);
        }
        public static bool operator ==(Milliseconds self, Seconds value)
        {
            return (self.ticks == value.ticks);
        }
        public static bool operator !=(Milliseconds self, Seconds value)
        {
            return (self.ticks != value.ticks);
        }
        public static bool operator ==(Milliseconds self, Minutes value)
        {
            return (self.ticks == value.ticks);
        }
        public static bool operator !=(Milliseconds self, Minutes value)
        {
            return (self.ticks != value.ticks);
        }
        public static bool operator ==(Milliseconds self, Hours value)
        {
            return (self.ticks == value.ticks);
        }
        public static bool operator !=(Milliseconds self, Hours value)
        {
            return (self.ticks != value.ticks);
        }

        public static bool operator <(Milliseconds self, TimeSpan value)
        {
            return (self.ticks < value.Ticks);
        }
        public static bool operator >(Milliseconds self, TimeSpan value)
        {
            return (self.ticks > value.Ticks);
        }
        public static bool operator <(Milliseconds self, Microseconds value)
        {
            return (self.ticks < value.ticks);
        }
        public static bool operator >(Milliseconds self, Microseconds value)
        {
            return (self.ticks > value.ticks);
        }
        public static bool operator <(Milliseconds self, Milliseconds value)
        {
            return (self.ticks < value.ticks);
        }
        public static bool operator >(Milliseconds self, Milliseconds value)
        {
            return (self.ticks > value.ticks);
        }
        public static bool operator <(Milliseconds self, Seconds value)
        {
            return (self.ticks < value.ticks);
        }
        public static bool operator >(Milliseconds self, Seconds value)
        {
            return (self.ticks > value.ticks);
        }
        public static bool operator <(Milliseconds self, Minutes value)
        {
            return (self.ticks < value.ticks);
        }
        public static bool operator >(Milliseconds self, Minutes value)
        {
            return (self.ticks > value.ticks);
        }
        public static bool operator <(Milliseconds self, Hours value)
        {
            return (self.ticks < value.ticks);
        }
        public static bool operator >(Milliseconds self, Hours value)
        {
            return (self.ticks > value.ticks);
        }

        public static bool operator <=(Milliseconds self, TimeSpan value)
        {
            return (self.ticks <= value.Ticks);
        }
        public static bool operator >=(Milliseconds self, TimeSpan value)
        {
            return (self.ticks >= value.Ticks);
        }
        public static bool operator <=(Milliseconds self, Microseconds value)
        {
            return (self.ticks <= value.ticks);
        }
        public static bool operator >=(Milliseconds self, Microseconds value)
        {
            return (self.ticks >= value.ticks);
        }
        public static bool operator <=(Milliseconds self, Milliseconds value)
        {
            return (self.ticks <= value.ticks);
        }
        public static bool operator >=(Milliseconds self, Milliseconds value)
        {
            return (self.ticks >= value.ticks);
        }
        public static bool operator <=(Milliseconds self, Seconds value)
        {
            return (self.ticks <= value.ticks);
        }
        public static bool operator >=(Milliseconds self, Seconds value)
        {
            return (self.ticks >= value.ticks);
        }
        public static bool operator <=(Milliseconds self, Minutes value)
        {
            return (self.ticks <= value.ticks);
        }
        public static bool operator >=(Milliseconds self, Minutes value)
        {
            return (self.ticks >= value.ticks);
        }
        public static bool operator <=(Milliseconds self, Hours value)
        {
            return (self.ticks <= value.ticks);
        }
        public static bool operator >=(Milliseconds self, Hours value)
        {
            return (self.ticks >= value.ticks);
        }

        public static TimeSpan operator +(Milliseconds self, TimeSpan value)
        {
            return new TimeSpan(self.ticks + value.Ticks);
        }
        public static TimeSpan operator -(Milliseconds self, TimeSpan value)
        {
            return new TimeSpan(self.ticks - value.Ticks);
        }
        public static Microseconds operator +(Milliseconds self, Microseconds value)
        {
            return new Microseconds(self.ticks + value.ticks);
        }
        public static Microseconds operator -(Milliseconds self, Microseconds value)
        {
            return new Microseconds(self.ticks - value.ticks);
        }
        public static Milliseconds operator +(Milliseconds self, Milliseconds value)
        {
            return new Milliseconds(self.ticks + value.ticks);
        }
        public static Milliseconds operator -(Milliseconds self, Milliseconds value)
        {
            return new Milliseconds(self.ticks - value.ticks);
        }
        public static Milliseconds operator +(Milliseconds self, Seconds value)
        {
            return new Milliseconds(self.ticks + value.ticks);
        }
        public static Milliseconds operator -(Milliseconds self, Seconds value)
        {
            return new Milliseconds(self.ticks - value.ticks);
        }
        public static Milliseconds operator +(Milliseconds self, Minutes value)
        {
            return new Milliseconds(self.ticks + value.ticks);
        }
        public static Milliseconds operator -(Milliseconds self, Minutes value)
        {
            return new Milliseconds(self.ticks - value.ticks);
        }
        public static Milliseconds operator +(Milliseconds self, Hours value)
        {
            return new Milliseconds(self.ticks + value.ticks);
        }
        public static Milliseconds operator -(Milliseconds self, Hours value)
        {
            return new Milliseconds(self.ticks - value.ticks);
        }
    }
}