using System;

namespace rso
{
    namespace core
    {
        public class CSafeInt64
        {
            Int64 _Data = 0;

            public CSafeInt64()
            {
            }
            public CSafeInt64(Int64 Data_)
            {
                _Data = Data_;
            }
            public static implicit operator Int64(CSafeInt64 Var_)
            {
                return Var_._Data;
            }
            public CSafeInt64 Add(Int64 Data_)
            {
                if (Data_ > 0)
                {
                    if (_Data + Data_ > _Data)
                        _Data += Data_;
                    else
                        _Data = Int64.MaxValue;
                }
                else if (Data_ < 0)
                {
                    if (_Data + Data_ < _Data)
                        _Data += Data_;
                    else
                        _Data = Int64.MinValue;
                }

                return this;
            }
            public CSafeInt64 Mul(Int64 Data_)
            {
                if (_Data == 0)
                    return this;

                if (Data_ == 0)
                {
                    _Data = 0;
                    return this;
                }

                Int64 NewData = _Data * Data_;

                if (NewData / Data_ == _Data)
                    _Data = NewData;
                else if (_Data < 0 && Data_ < 0 || _Data > 0 && Data_ > 0)
                    _Data = Int64.MaxValue;
                else
                    _Data = Int64.MinValue;

                return this;
            }
            public CSafeInt64 Div(Int64 Data_)
            {
                _Data /= Data_;
                return this;
            }
        }
    }
}