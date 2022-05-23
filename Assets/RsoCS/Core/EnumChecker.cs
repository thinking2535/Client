using System;
using System.Collections.Generic;

namespace rso
{
    namespace core
    {
        public static class SEnumChecker
        {
            // for GetStdName //////////////////////////////////
            public static string GetStdName(bool Value_) { return "bool"; }
            public static string GetStdName(SByte Value_) { return "int8"; }
            public static string GetStdName(Byte Value_) { return "uint8"; }
            public static string GetStdName(Int16 Value_) { return "int16"; }
            public static string GetStdName(UInt16 Value_) { return "uint16"; }
            public static string GetStdName(Int32 Value_) { return "int32"; }
            public static string GetStdName(UInt32 Value_) { return "uint32"; }
            public static string GetStdName(Int64 Value_) { return "int64"; }
            public static string GetStdName(UInt64 Value_) { return "uint64"; }
            public static string GetStdName(Single Value_) { return "float"; }
            public static string GetStdName(Double Value_) { return "double"; }
            public static string GetStdName(String Value_) { return "wstring"; }
            public static string GetStdName(TimePoint Value_) { return "time_point"; }
            public static string GetStdName(DateTime Value_) { return "datetime"; }
            public static string GetStdName<TValue>(TValue[] Value_)
            {
                string Name = string.Empty;
                Name += GetStdName(default(TValue));

                for (Int32 i = 1; i < Value_.Length; ++i)
                    Name += ("," + GetStdName(default(TValue)));
                return Name;
            }
            public static string GetStdName<TValue>(List<TValue> Value_) { return '{' + GetStdName(default(TValue)) + '}'; }
            public static string GetStdName<TValue>(HashSet<TValue> Value_) { return '{' + GetStdName(default(TValue)) + '}'; }
            public static string GetStdName<TKey, TValue>(Dictionary<TKey, TValue> Value_) { return '{' + GetStdName(default(TKey)) + ',' + GetStdName(default(TValue)) + '}'; }
            public static string GetStdName<TKey, TValue>(SortedDictionary<TKey, TValue> Value_) { return '{' + GetStdName(default(TKey)) + ',' + GetStdName(default(TValue)) + '}'; }
            public static string GetStdName<TValue>(MultiSet<TValue> Value_) { return '{' + GetStdName(default(TValue)) + '}'; }
            public static string GetStdName<TKey, TValue>(MultiMap<TKey, TValue> Value_) { return '{' + GetStdName(default(TKey)) + ',' + GetStdName(default(TValue)) + '}'; }
            public static string GetStdName<TValue>(TValue Value_)
            {
                switch (Type.GetTypeCode(typeof(TValue)))
                {
                    case TypeCode.Boolean:
                        return GetStdName(new bool());
                    case TypeCode.SByte:
                        return GetStdName(new SByte());
                    case TypeCode.Byte:
                    case TypeCode.Char:
                        return GetStdName(new Byte());
                    case TypeCode.Int16:
                        return GetStdName(new Int16());
                    case TypeCode.UInt16:
                        return GetStdName(new UInt16());
                    case TypeCode.Int32:
                        return GetStdName(new Int32());
                    case TypeCode.UInt32:
                        return GetStdName(new UInt32());
                    case TypeCode.Int64:
                        return GetStdName(new Int64());
                    case TypeCode.UInt64:
                        return GetStdName(new UInt64());
                    case TypeCode.Single:
                        return GetStdName(new Single());
                    case TypeCode.Double:
                        return GetStdName(new Double());
                    case TypeCode.String:
                        return GetStdName(String.Empty);
                    case TypeCode.DateTime:
                        return GetStdName(new DateTime());
                    case TypeCode.Object:
                        {
                            switch (typeof(TValue).ToString())
                            {
                                case "rso.core.TimePoint":
                                    return "time_point";
                                case "rso.core.CStream":
                                    return "stream";
                                default:
                                    return ((SProto)(object)Value_).StdName();
                            }
                        }
                    // break;
                    default:
                        throw new Exception("Invalid TypeCode : " + Type.GetTypeCode(typeof(TValue)).ToString());
                }
            }


            // for GetStdName //////////////////////////////////
            public static string GetMemberName(bool Value_, string MemberName_) { return MemberName_; }
            public static string GetMemberName(SByte Value_, string MemberName_) { return MemberName_; }
            public static string GetMemberName(Byte Value_, string MemberName_) { return MemberName_; }
            public static string GetMemberName(Int16 Value_, string MemberName_) { return MemberName_; }
            public static string GetMemberName(UInt16 Value_, string MemberName_) { return MemberName_; }
            public static string GetMemberName(Int32 Value_, string MemberName_) { return MemberName_; }
            public static string GetMemberName(UInt32 Value_, string MemberName_) { return MemberName_; }
            public static string GetMemberName(Int64 Value_, string MemberName_) { return MemberName_; }
            public static string GetMemberName(UInt64 Value_, string MemberName_) { return MemberName_; }
            public static string GetMemberName(Single Value_, string MemberName_) { return MemberName_; }
            public static string GetMemberName(Double Value_, string MemberName_) { return MemberName_; }
            public static string GetMemberName(String Value_, string MemberName_) { return MemberName_; }
            public static string GetMemberName(TimePoint Value_, string MemberName_) { return MemberName_; }
            public static string GetMemberName(DateTime Value_, string MemberName_) { return MemberName_; }
            public static string GetMemberName<TValue>(TValue[] Value_, string MemberName_) { return MemberName_; }
            public static string GetMemberName<TValue>(List<TValue> Value_, string MemberName_) { return MemberName_; }
            public static string GetMemberName<TValue>(HashSet<TValue> Value_, string MemberName_) { return MemberName_; }
            public static string GetMemberName<TKey, TValue>(Dictionary<TKey, TValue> Value_, string MemberName_) { return MemberName_; }
            public static string GetMemberName<TKey, TValue>(SortedDictionary<TKey, TValue> Value_, string MemberName_) { return MemberName_; }
            public static string GetMemberName<TValue>(MultiSet<TValue> Value_, string MemberName_) { return MemberName_; }
            public static string GetMemberName<TKey, TValue>(MultiMap<TKey, TValue> Value_, string MemberName_) { return MemberName_; }
            public static string GetMemberName<TValue>(TValue Value_, string MemberName_)
            {
                switch (Type.GetTypeCode(typeof(TValue)))
                {
                    case TypeCode.Boolean:
                    case TypeCode.SByte:
                    case TypeCode.Byte:
                    case TypeCode.Char:
                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                    case TypeCode.Int32:
                    case TypeCode.UInt32:
                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                    case TypeCode.Single:
                    case TypeCode.Double:
                    case TypeCode.String:
                    case TypeCode.DateTime:
                        return MemberName_;
                    case TypeCode.Object:
                        {
                            switch (typeof(TValue).ToString())
                            {
                                case "rso.core.TimePoint":
                                case "rso.core.CStream":
                                    return MemberName_;
                                default:
                                    return ((SProto)(object)Value_).MemberName();
                            }
                        }
                    // break;
                    default:
                        throw new Exception("GetMemberName() Invalid TypeCode : " + Type.GetTypeCode(typeof(TValue)).ToString());
                }
            }
            ////////////////////////////////////////////////////


            public static TType GetNewValue<TType>()
            {
                var constructorInfo = typeof(TType).GetConstructor(Type.EmptyTypes);
                if (constructorInfo == null)
                    return default(TType);
                else
                    return (TType)constructorInfo.Invoke(null);
            }
        }
    }
}