using rso.net;
using System;
using TUID = System.Int64;

namespace rso
{
    namespace game
    {
        public class ExceptionGame : Exception
        {
            EGameRet _Ret;
            public ExceptionGame()
            {
            }
            public ExceptionGame(EGameRet Ret_) :
                base(string.Format("ExceptionGame[{0}]", Ret_.ToString()))
            {
                _Ret = Ret_;
            }
            public ExceptionGame(EGameRet Ret_, Exception innerException) :
                base(string.Format("ExceptionGame[{0}]", Ret_.ToString()), innerException)
            {
                _Ret = Ret_;
            }
            public EGameRet GetRet()
            {
                return _Ret;
            }
        }
        public class CUIDPair : SUIDPair
        {
            public CUIDPair()
            {
            }
            public CUIDPair(SUIDPair UIDPair_) :
                base(UIDPair_)
            {
            }
            public CUIDPair(TUID UID_, TUID SubUID_) :
                base(UID_, SubUID_)
            {
            }
            public static implicit operator bool(CUIDPair UIDPair_)
            {
                return (UIDPair_ != null && UIDPair_.UID > 0 && UIDPair_.SubUID > 0);
            }
            public override bool Equals(object Obj_)
            {
                var p = Obj_ as CUIDPair;
                if (p == null)
                    return false;

                return (UID == p.UID && SubUID == p.SubUID);
            }
            public override Int32 GetHashCode()
            {
                return base.GetHashCode();
            }
            public override string ToString()
            {
                return "[" + UID.ToString() + "," + SubUID.ToString() + "]";
            }
            public void Clear()
            {
                UID = 0;
                SubUID = 0;
            }
            public SUIDPairCompare KeyCompare
            {
                get
                {
                    return new SUIDPairCompare(this);
                }
            }
        }
        public struct SUIDPairCompare
        {
            public TUID UID;
            public TUID SubUID;
            public SUIDPairCompare(CUIDPair Value_)
            {
                UID = Value_.UID;
                SubUID = Value_.SubUID;
            }
            public CUIDPair Key
            {
                get
                {
                    return new CUIDPair(UID, SubUID);
                }
            }
        }
    }
}