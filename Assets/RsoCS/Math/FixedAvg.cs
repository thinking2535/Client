using System;

namespace rso
{
    namespace math
    {
        public class CFixedAvg
        {
            double _Cnt = 1.0;
            double _Avg = 0.0;

            public CFixedAvg(UInt32 Cnt_)
            {
                _Cnt = Cnt_;
            }
            public CFixedAvg(UInt32 Cnt_, double Avg_)
            {
                _Cnt = (double)Cnt_;
                _Avg = Avg_;
            }
            public static CFixedAvg operator +(CFixedAvg lhs_, double Value_)
            {
                lhs_._Avg = (lhs_._Avg * lhs_._Cnt + Value_) / (lhs_._Cnt + 1.0);
                return lhs_;
            }
            public static CFixedAvg operator -(CFixedAvg lhs_, double Value_)
            {
                lhs_._Avg = (lhs_._Avg * lhs_._Cnt - Value_) / (lhs_._Cnt + 1.0);
                return lhs_;
            }
            public static explicit operator double(CFixedAvg Value_)
            {
                return Value_._Avg;
            }
        }
    }
}