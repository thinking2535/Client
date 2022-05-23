using System;

namespace rso
{
    namespace gameutil
    {
        public class CSimpleRand
        {
            UInt64 _Seed = 0;

            public CSimpleRand(UInt64 Seed_)
            {
                _Seed = (0x1f3a49b72c8d5ef6 ^ Seed_);
            }
            public UInt64 Get(UInt64 Value_)
            {
                Int32 ShiftCntSeed = (((Int32)Value_ % 128) >> 1);
                Int32 ShiftCntValue = ((Int32)Value_ % 64);
                return ((_Seed << ShiftCntSeed) | (_Seed >> (64 - ShiftCntSeed)) ^
                        (Value_ << ShiftCntValue) | (Value_ >> (64 - ShiftCntValue)));
            }
        }
    }
}