using System;
using System.Collections.Generic;
using System.Linq;

namespace rso
{
    namespace core
    {
        using TCheckSum = UInt64;
        public abstract partial class SProto
        {
            public abstract string StdName();
            public abstract string MemberName();
            public abstract void Push(CStream Stream_);
            public abstract void Pop(CStream Stream_);
            public abstract void Push(JsonDataObject Value_);
            public abstract void Pop(JsonDataObject Value_);
            public string ToJsonString()
            {
                try
                {
                    return new JsonDataObject(this).ToString();
                }
                catch
                {
                    return ""; // 중첩 Container 또는 Array 라면 C#한계상 에러나므로 그냥 공백으로 처리
                }
            }
        }
        public static class CCore
        {
            public static UInt64 GetCheckSum(Byte[] Data_, Int32 Index_, Int32 Length_)
            {
                TCheckSum Sum = 0;
                Int32 CheckCnt = Length_ / 8;
                Int32 LeftByte = Length_ % 8;
                TCheckSum Num = 0;

                for (Int32 Cnt = 0; Cnt < CheckCnt; ++Cnt)
                {
                    Num = 0;

                    for (Int32 i = 0; i < 8; ++i)
                    {
                        Num |= ((TCheckSum)Data_[Index_ + (Cnt << 3) + i] << (i << 3));
                    }

                    Sum ^= Num;
                }


                Num = 0;
                for (Int32 i = 0; i < LeftByte; ++i)
                {
                    Num |= ((TCheckSum)Data_[Index_ + (CheckCnt << 3) + i] << (i << 3));
                }

                return Sum ^ Num;
            }
            public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> Container_, TKey Key_)
            {
                TValue Value;
                return Container_.TryGetValue(Key_, out Value) ? Value : default(TValue);
            }
            public static void Resize<TType>(this List<TType> List_, Int32 Size_) where TType : new()
            {
                Int32 CurSize = List_.Count;
                if (Size_ < CurSize)
                    List_.RemoveRange(Size_, CurSize - Size_);
                else if (Size_ > CurSize)
                {
                    if (Size_ > List_.Capacity)
                        List_.Capacity = Size_;

                    for (Int32 i = CurSize; i < Size_; ++i)
                        List_.Add(new TType());
                }
            }
            public static T[] SubArray<T>(this T[] Data_, Int32 Index_, Int32 Length_)
            {
                T[] Ret = new T[Length_];
                Array.Copy(Data_, Index_, Ret, 0, Length_);
                return Ret;
            }
            public static void SubArray<T>(this T[] Data_, Int32 Index_, T[] Target_)
            {
                Array.Copy(Data_, Index_, Target_, 0, Target_.Length);
            }
            public static string Combine(this string Path0_, string Path1_)
            {
                Path0_.Replace('/', '\\');
                Path1_.Replace('/', '\\');

                if (Path0_.Length == 0)
                {
                    return Path1_;
                }
                else if (Path1_.Length == 0)
                {
                    return Path0_;
                }
                else if (Path0_.Last() == '\\')
                {
                    if (Path1_.First() == '\\')
                        return Path0_.Remove(Path0_.Length - 1) + Path1_;
                    else
                        return Path0_ + Path1_;
                }
                else if (Path1_.First() == '\\')
                {
                    return Path0_ + Path1_;
                }
                else
                {
                    return Path0_ + '\\' + Path1_;
                }
            }
        }
    }
}
