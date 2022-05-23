using System;

namespace rso
{
    namespace crypto
    {
        public partial class CCrypto
        {
            Byte[] _aKey = new Byte[8];
            Byte _Index = 0;
            Byte _Bit = 0;

            Byte _GetBit()
            {
                _Bit += _aKey[_Index];
                ++_Index;
                _Index %= 8;

                return _Bit;
            }
            void _SetKey(UInt64 Key_)
            {
                _Bit = 0;

                _aKey[0] = (Byte)(((Key_) & 0xFF) == 0 ? 0xAA : ((Key_) & 0xFF) == 0xFF ? 0x55 : ((Key_) & 0xFF));
                _aKey[1] = (Byte)(((Key_ >> 8) & 0xFF) == 0 ? 0xAA : ((Key_ >> 8) & 0xFF) == 0xFF ? 0x55 : ((Key_ >> 8) & 0xFF));
                _aKey[2] = (Byte)(((Key_ >> 16) & 0xFF) == 0 ? 0xAA : ((Key_ >> 16) & 0xFF) == 0xFF ? 0x55 : ((Key_ >> 16) & 0xFF));
                _aKey[3] = (Byte)(((Key_ >> 24) & 0xFF) == 0 ? 0xAA : ((Key_ >> 24) & 0xFF) == 0xFF ? 0x55 : ((Key_ >> 24) & 0xFF));
                _aKey[4] = (Byte)(((Key_ >> 32) & 0xFF) == 0 ? 0xAA : ((Key_ >> 32) & 0xFF) == 0xFF ? 0x55 : ((Key_ >> 32) & 0xFF));
                _aKey[5] = (Byte)(((Key_ >> 40) & 0xFF) == 0 ? 0xAA : ((Key_ >> 40) & 0xFF) == 0xFF ? 0x55 : ((Key_ >> 40) & 0xFF));
                _aKey[6] = (Byte)(((Key_ >> 48) & 0xFF) == 0 ? 0xAA : ((Key_ >> 48) & 0xFF) == 0xFF ? 0x55 : ((Key_ >> 48) & 0xFF));
                _aKey[7] = (Byte)(((Key_ >> 56) & 0xFF) == 0 ? 0xAA : ((Key_ >> 56) & 0xFF) == 0xFF ? 0x55 : ((Key_ >> 56) & 0xFF));

                _Index = 0;

                for (Int32 i = 0; i < 8; ++i)
                    _Bit ^= _aKey[i];
            }

            public void Encode(Byte[] aData_io, Int32 Index_, Int32 Length_, UInt64 Key_)
            {
                _SetKey(Key_);

                for (Int32 i = 0; i < Length_; ++i)
                    aData_io[Index_ + i] ^= _GetBit();
            }

            public void Decode(Byte[] aData_io, Int32 Index_, Int32 Length_, UInt64 Key_)
            {
                _SetKey(Key_);

                for (Int32 i = 0; i < Length_; ++i)
                    aData_io[Index_ + i] ^= _GetBit();
            }
        }
    }
}