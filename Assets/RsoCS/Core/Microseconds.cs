using System;

namespace rso.core
{
    public struct Microseconds : IComparable<Microseconds>
    {
        const Int64 _ratio = 10;
        public static Microseconds zero = new Microseconds();

        public Int64 ticks;

        public Microseconds(Int64 ticks)
        {
            this.ticks = ticks;
        }
        public Microseconds(TimeSpan duration)
        {
            ticks = duration.Ticks;
        }
        public static Microseconds fromValue(Int64 value)
        {
            return new Microseconds(value * _ratio);
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
            var p = (Microseconds)Obj_;
            return (this == p);
        }
        public Int32 CompareTo(Microseconds value)
        {
            if (ticks > value.ticks) return 1;
            else if (ticks < value.ticks) return -1;
            else return 0;
        }
        public Int64 value => ticks / _ratio;

        public static bool operator ==(Microseconds self, TimeSpan value)
        {
            return (self.ticks == value.Ticks);
        }
        public static bool operator !=(Microseconds self, TimeSpan value)
        {
            return (self.ticks != value.Ticks);
        }
        public static bool operator ==(Microseconds self, Microseconds value)
        {
            return (self.ticks == value.ticks);
        }
        public static bool operator !=(Microseconds self, Microseconds value)
        {
            return (self.ticks != value.ticks);
        }
        public static bool operator ==(Microseconds self, Milliseconds value)
        {
            return (self.ticks == value.ticks);
        }
        public static bool operator !=(Microseconds self, Milliseconds value)
        {
            return (self.ticks != value.ticks);
        }
        public static bool operator ==(Microseconds self, Seconds value)
        {
            return (self.ticks == value.ticks);
        }
        public static bool operator !=(Microseconds self, Seconds value)
        {
            return (self.ticks != value.ticks);
        }
        public static bool operator ==(Microseconds self, Minutes value)
        {
            return (self.ticks == value.ticks);
        }
        public static bool operator !=(Microseconds self, Minutes value)
        {
            return (self.ticks != value.ticks);
        }
        public static bool operator ==(Microseconds self, Hours value)
        {
            return (self.ticks == value.ticks);
        }
        public static bool operator !=(Microseconds self, Hours value)
        {
            return (self.ticks != value.ticks);
        }

        public static bool operator <(Microseconds self, TimeSpan value)
        {
            return (self.ticks < value.Ticks);
        }
        public static bool operator >(Microseconds self, TimeSpan value)
        {
            return (self.ticks > value.Ticks);
        }
        public static bool operator <(Microseconds self, Microseconds value)
        {
            return (self.ticks < value.ticks);
        }
        public static bool operator >(Microseconds self, Microseconds value)
        {
            return (self.ticks > value.ticks);
        }
        public static bool operator <(Microseconds self, Milliseconds value)
        {
            return (self.ticks < value.ticks);
        }
        public static bool operator >(Microseconds self, Milliseconds value)
        {
            return (self.ticks > value.ticks);
        }
        public static bool operator <(Microseconds self, Seconds value)
        {
            return (self.ticks < value.ticks);
        }
        public static bool operator >(Microseconds self, Seconds value)
        {
            return (self.ticks > value.ticks);
        }
        public static bool operator <(Microseconds self, Minutes value)
        {
            return (self.ticks < value.ticks);
        }
        public static bool operator >(Microseconds self, Minutes value)
        {
            return (self.ticks > value.ticks);
        }
        public static bool operator <(Microseconds self, Hours value)
        {
            return (self.ticks < value.ticks);
        }
        public static bool operator >(Microseconds self, Hours value)
        {
            return (self.ticks > value.ticks);
        }

        public static bool operator <=(Microseconds self, TimeSpan value)
        {
            return (self.ticks <= value.Ticks);
        }
        public static bool operator >=(Microseconds self, TimeSpan value)
        {
            return (self.ticks >= value.Ticks);
        }
        public static bool operator <=(Microseconds self, Microseconds value)
        {
            return (self.ticks <= value.ticks);
        }
        public static bool operator >=(Microseconds self, Microseconds value)
        {
            return (self.ticks >= value.ticks);
        }
        public static bool operator <=(Microseconds self, Milliseconds value)
        {
            return (self.ticks <= value.ticks);
        }
        public static bool operator >=(Microseconds self, Milliseconds value)
        {
            return (self.ticks >= value.ticks);
        }
        public static bool operator <=(Microseconds self, Seconds value)
        {
            return (self.ticks <= value.ticks);
        }
        public static bool operator >=(Microseconds self, Seconds value)
        {
            return (self.ticks >= value.ticks);
        }
        public static bool operator <=(Microseconds self, Minutes value)
        {
            return (self.ticks <= value.ticks);
        }
        public static bool operator >=(Microseconds self, Minutes value)
        {
            return (self.ticks >= value.ticks);
        }
        public static bool operator <=(Microseconds self, Hours value)
        {
            return (self.ticks <= value.ticks);
        }
        public static bool operator >=(Microseconds self, Hours value)
        {
            return (self.ticks >= value.ticks);
        }

        public static TimeSpan operator +(Microseconds self, TimeSpan value)
        {
            return new TimeSpan(self.ticks + value.Ticks);
        }
        public static TimeSpan operator -(Microseconds self, TimeSpan value)
        {
            return new TimeSpan(self.ticks - value.Ticks);
        }
        public static Microseconds operator +(Microseconds self, Microseconds value)
        {
            return new Microseconds(self.ticks + value.ticks);
        }
        public static Microseconds operator -(Microseconds self, Microseconds value)
        {
            return new Microseconds(self.ticks - value.ticks);
        }
        public static Microseconds operator +(Microseconds self, Milliseconds value)
        {
            return new Microseconds(self.ticks + value.ticks);
        }
        public static Microseconds operator -(Microseconds self, Milliseconds value)
        {
            return new Microseconds(self.ticks - value.ticks);
        }
        public static Microseconds operator +(Microseconds self, Seconds value)
        {
            return new Microseconds(self.ticks + value.ticks);
        }
        public static Microseconds operator -(Microseconds self, Seconds value)
        {
            return new Microseconds(self.ticks - value.ticks);
        }
        public static Microseconds operator +(Microseconds self, Minutes value)
        {
            return new Microseconds(self.ticks + value.ticks);
        }
        public static Microseconds operator -(Microseconds self, Minutes value)
        {
            return new Microseconds(self.ticks - value.ticks);
        }
        public static Microseconds operator +(Microseconds self, Hours value)
        {
            return new Microseconds(self.ticks + value.ticks);
        }
        public static Microseconds operator -(Microseconds self, Hours value)
        {
            return new Microseconds(self.ticks - value.ticks);
        }
    }
}