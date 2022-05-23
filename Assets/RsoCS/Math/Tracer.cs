using System;

namespace rso
{
    namespace math
    {
        public class CTracer
        {
            Int64 _Accel = 0;
            public Int64 Velocity = 0;
            public Int64 MinValue = 0;
            public Int64 MaxValue = 0;
            public Int64 Value = 0;

            public CTracer(Int64 Accel_, Int64 Velocity_, Int64 MinValue_, Int64 MaxValue_, Int64 Value_)
            {
                _Accel = Accel_;
                Velocity = Velocity_;
                MinValue = MinValue_;
                MaxValue = MaxValue_;
                Value = Value_;
            }
            public static CTracer operator ++(CTracer lhs_)
            {
                if (lhs_.Velocity < 0)
                    lhs_.Velocity = 0;

                lhs_.Velocity += lhs_._Accel;
                lhs_.Value += lhs_.Velocity;

                if (lhs_.Value > lhs_.MaxValue)
                    lhs_.Value = lhs_.MaxValue;

                return lhs_;
            }
            public static CTracer operator --(CTracer lhs_)
            {
                if (lhs_.Velocity > 0)
                    lhs_.Velocity = 0;

                lhs_.Velocity -= lhs_._Accel;
                lhs_.Value += lhs_.Velocity;

                if (lhs_.Value < lhs_.MinValue)
                    lhs_.Value = lhs_.MinValue;

                return lhs_;
            }
            public Int64 Accel
            {
                set
                {
                    _Accel = value;
                }
            }
            public static explicit operator Int64(CTracer Value_)
            {
                return Value_.Value;
            }
        }
    }
}