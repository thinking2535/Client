using System;

namespace rso
{
    namespace gameutil
    {
        public class CFixedRandom64
        {
            CFixedRandom32 _Random32_0;
            CFixedRandom32 _Random32_1;
            public CFixedRandom64(UInt64 Seed_)
            {
                _Random32_0 = new CFixedRandom32((UInt32)(Seed_ & 0xFFFFFFFF));
                _Random32_1 = new CFixedRandom32((UInt32)(Seed_ >> 32));
            }
            public UInt64 GetSeed()
            {
                return (((UInt64)_Random32_1.GetSeed()) << 32) + (UInt64)_Random32_0.GetSeed();
            }
            public UInt64 Get()
            {
                return (((UInt64)_Random32_1.Get()) << 32) + (UInt64)_Random32_0.Get();
            }
        }
    }
}