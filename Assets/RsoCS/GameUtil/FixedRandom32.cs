using System;

namespace rso
{
    namespace gameutil
    {
        public class CFixedRandom32
        {
            UInt32 _Seed;
            public CFixedRandom32(UInt32 Seed_)
            {
                _Seed = Seed_;
            }
            public UInt32 GetSeed()
            {
                return _Seed;
            }
            public UInt32 Get()
            {
                UInt32 Result;

                _Seed *= 1103515245;
                _Seed += 12345;
                Result = (UInt32)(_Seed / 65536) % 2048;

                _Seed *= 1103515245;
                _Seed += 12345;
                Result <<= 10;
                Result ^= (UInt32)(_Seed / 65536) % 1024;

                _Seed *= 1103515245;
                _Seed += 12345;
                Result <<= 10;
                Result ^= (UInt32)(_Seed / 65536) % 1024;

                return Result;
            }
        }
    }
}