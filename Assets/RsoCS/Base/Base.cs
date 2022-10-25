using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace rso
{
    namespace Base
    {
        using TPeerCnt = UInt32;

        public static class CBase
        {
            public const TPeerCnt c_PeerCnt_Null = TPeerCnt.MaxValue;

            public static String MakeRelativePath(String FromPath_, String ToPath_)
            {
                if (String.IsNullOrEmpty(FromPath_)) throw new ArgumentNullException("FromPath_");
                if (String.IsNullOrEmpty(ToPath_)) throw new ArgumentNullException("ToPath_");

                Uri FromUri = new Uri(FromPath_);
                Uri ToUri = new Uri(ToPath_);

                if (FromUri.Scheme != ToUri.Scheme) { return ToPath_; } // path can't be made relative.

                Uri RelativeUri = FromUri.MakeRelativeUri(ToUri);
                String RelativePath = Uri.UnescapeDataString(RelativeUri.ToString());

                if (ToUri.Scheme.Equals("file", StringComparison.InvariantCultureIgnoreCase))
                {
                    RelativePath = RelativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
                }

                return RelativePath;
            }

            // Array Operations //////////////////////////////////////////////
            public static bool IsSame<T>(this T[] Self_, T[] Value_) where T : IComparable<T>
            {
                if (Self_.Length != Value_.Length) return false;

                for (Int32 i = 0; i < Self_.Length; ++i)
                    if (Self_[i].CompareTo(Value_[i]) != 0) return false;

                return true;
            }
            public static bool IsGreaterThanEqual<T>(this T[] Self_, T[] Value_) where T : IComparable<T>
            {
                for (Int32 i = 0; i < Self_.Length; ++i)
                {
                    if (Self_[i].CompareTo(Value_[i]) < 0)
                        return false;
                }

                return true;
            }
            public static T[] GetCopy<T>(this T[] Self_)
            {
                T[] Ret = new T[Self_.Length];

                for (Int32 i = 0; i < Self_.Length; ++i)
                    Ret[i] = Self_[i];

                return Ret;
            }
            public static void Clear<T>(this T[] Self_)
            {
                for (Int32 i = 0; i < Self_.Length; ++i)
                    Self_[i] = default;
            }
            public static void Set<T>(this T[] Self_, T[] With_)
            {
                for (Int32 i = 0; i < Self_.Length; ++i)
                    Self_[i] = With_[i];
            }
            public static void LimitMin<T>(this T[] Self_, T[] Min_) where T : IComparable<T>
            {
                for (Int32 i = 0; i < Self_.Length; ++i)
                {
                    if (Self_[i].CompareTo(Min_[i]) < 0)
                        Self_[i] = Min_[i];
                }
            }
            public static void LimitMax<T>(this T[] Self_, T[] Max_) where T : IComparable<T>
            {
                for (Int32 i = 0; i < Self_.Length; ++i)
                {
                    if (Self_[i].CompareTo(Max_[i]) > 0)
                        Self_[i] = Max_[i];
                }
            }
            public static void LimitMinMax<T>(this T[] Self_, T[] Min_, T[] Max_) where T : IComparable<T>
            {
                Self_.LimitMin(Min_);
                Self_.LimitMax(Max_);
            }
            public static string ToArrayString<T>(this T[] Self_)
            {
                string Ret = "[";

                if (Self_.Length > 0)
                    Ret += Self_[0].ToString();

                for (Int32 i = 1; i < Self_.Length; ++i)
                {
                    Ret += ", ";
                    Ret += Self_[i].ToString();
                }

                Ret += ']';

                return Ret;
            }
            ////////////////////////////////////////////////
        }

        static class CUri
        {
            static Int32 Hex2Dec(Char Char_)
            {
                if (Char_ >= '0' && Char_ <= '9')
                    return (Char_ - '0');

                if (Char_ >= 'A' && Char_ <= 'Z')
                    return (Char_ - 'A' + 10);

                if (Char_ >= 'a' && Char_ <= 'z')
                    return (Char_ - 'a' + 10);

                return -1;
            }

            public static String Decode(String Str_)
            {
                bool IsDecoding = false;
                var Result = new List<Byte>();

                for (Int32 i = 0; i < Str_.Length; ++i)
                {
                    if (IsDecoding)
                    {
                        if ((Str_.Length - i) < 2)
                            break;

                        Int32 dec1, dec2;
                        if (-1 != (dec1 = Hex2Dec(Str_[i])) &&
                            -1 != (dec2 = Hex2Dec(Str_[++i])))
                        {
                            Result.Add(Convert.ToByte((dec1 << 4) + dec2));
                        }

                        IsDecoding = false;
                    }
                    else
                    {
                        if (Str_[i] == '%')
                        {
                            IsDecoding = true;
                            continue;
                        }
                        try
                        {
                            Result.Add(Convert.ToByte(Str_[i]));
                        }
                        catch
                        {
                        }
                    }
                }

                return Encoding.Default.GetString(Result.ToArray());
            }
        }
    }
}