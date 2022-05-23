using System;
using System.Runtime.InteropServices;

namespace rso
{
    namespace core
    {
        public class JsonDataNumber : JsonData
        {
            [StructLayout(LayoutKind.Explicit)]
            struct _SData
            {
                [FieldOffset(0)]
                public char ValueChar;
                [FieldOffset(0)]
                public SByte ValueSByte;
                [FieldOffset(0)]
                public Byte ValueByte;
                [FieldOffset(0)]
                public Int16 ValueShort;
                [FieldOffset(0)]
                public UInt16 ValueUShort;
                [FieldOffset(0)]
                public Int32 ValueInt;
                [FieldOffset(0)]
                public UInt32 ValueUInt;
                [FieldOffset(0)]
                public Int64 ValueLong;
                [FieldOffset(0)]
                public UInt64 ValueULong;
                [FieldOffset(0)]
                public float ValueFloat;
                [FieldOffset(0)]
                public double ValueDouble;
            }
            TypeCode _TypeCode;
            _SData _Data;
            public JsonDataNumber(char Data_)
            {
                SetData(Data_);
            }
            public JsonDataNumber(SByte Data_)
            {
                SetData(Data_);
            }
            public JsonDataNumber(Byte Data_)
            {
                SetData(Data_);
            }
            public JsonDataNumber(Int16 Data_)
            {
                SetData(Data_);
            }
            public JsonDataNumber(UInt16 Data_)
            {
                SetData(Data_);
            }
            public JsonDataNumber(Int32 Data_)
            {
                SetData(Data_);
            }
            public JsonDataNumber(UInt32 Data_)
            {
                SetData(Data_);
            }
            public JsonDataNumber(Int64 Data_)
            {
                SetData(Data_);
            }
            public JsonDataNumber(UInt64 Data_)
            {
                SetData(Data_);
            }
            public JsonDataNumber(float Data_)
            {
                SetData(Data_);
            }
            public JsonDataNumber(double Data_)
            {
                SetData(Data_);
            }
            public JsonDataNumber(string Text_, ref Int32 Index_)
            {
                var StartIndex = Index_;

                for (++Index_; Index_ < Text_.Length; ++Index_) // 외부에서 첫 문자 체크 한것으로 간주
                {
                    var c = Text_[Index_];
                    if (c != '-' && !(c >= '0' && c <= '9') && c != '.')
                        break;
                }

                try
                {
                    SetData(Int64.Parse(Text_.Substring(StartIndex, Index_ - StartIndex)));
                }
                catch
                {
                    try
                    {
                        SetData(double.Parse(Text_.Substring(StartIndex, Index_ - StartIndex)));
                    }
                    catch
                    {
                        throw new Exception("Invalid Json Number");
                    }
                }
            }
            string _ToString()
            {
                switch (_TypeCode)
                {
                    case TypeCode.Char:
                        return _Data.ValueChar.ToString();
                    case TypeCode.SByte:
                        return _Data.ValueSByte.ToString();
                    case TypeCode.Byte:
                        return _Data.ValueByte.ToString();
                    case TypeCode.Int16:
                        return _Data.ValueShort.ToString();
                    case TypeCode.UInt16:
                        return _Data.ValueUShort.ToString();
                    case TypeCode.Int32:
                        return _Data.ValueInt.ToString();
                    case TypeCode.UInt32:
                        return _Data.ValueUInt.ToString();
                    case TypeCode.Int64:
                        return _Data.ValueLong.ToString();
                    case TypeCode.UInt64:
                        return _Data.ValueULong.ToString();
                    case TypeCode.Single:
                        return _Data.ValueFloat.ToString();
                    case TypeCode.Double:
                        return _Data.ValueDouble.ToString();
                    default:
                        return "0";
                }
            }
            public override string ToString(string Name_, string Indentation_)
            {
                return Indentation_ + JsonGlobal.GetNameString(Name_) + _ToString();
            }
            public override char GetChar() { return _Data.ValueChar; }
            public override SByte GetSByte() { return _Data.ValueSByte; }
            public override Byte GetByte() { return _Data.ValueByte; }
            public override Int16 GetInt16() { return _Data.ValueShort; }
            public override UInt16 GetUInt16() { return _Data.ValueUShort; }
            public override Int32 GetInt32() { return _Data.ValueInt; }
            public override UInt32 GetUInt32() { return _Data.ValueUInt; }
            public override Int64 GetInt64() { return _Data.ValueLong; }
            public override UInt64 GetUInt64() { return _Data.ValueULong; }
            public override float GetFloat() { return _Data.ValueFloat; }
            public override double GetDouble() { return _Data.ValueDouble; }
            public override TimePoint GetTimePoint() { return new TimePoint(_Data.ValueLong); }
            public override DateTime GetDateTime() { return GetTimePoint().ToDateTime(); }
            public void SetData(char Data_)
            {
                _TypeCode = TypeCode.Char;
                _Data.ValueChar = Data_;
            }
            public void SetData(SByte Data_)
            {
                _TypeCode = TypeCode.SByte;
                _Data.ValueSByte = Data_;
            }
            public void SetData(Byte Data_)
            {
                _TypeCode = TypeCode.Byte;
                _Data.ValueByte = Data_;
            }
            public void SetData(Int16 Data_)
            {
                _TypeCode = TypeCode.Int16;
                _Data.ValueShort = Data_;
            }
            public void SetData(UInt16 Data_)
            {
                _TypeCode = TypeCode.UInt16;
                _Data.ValueUShort = Data_;
            }
            public void SetData(Int32 Data_)
            {
                _TypeCode = TypeCode.Int32;
                _Data.ValueInt = Data_;
            }
            public void SetData(UInt32 Data_)
            {
                _TypeCode = TypeCode.UInt32;
                _Data.ValueUInt = Data_;
            }
            public void SetData(Int64 Data_)
            {
                _TypeCode = TypeCode.Int64;
                _Data.ValueLong = Data_;
            }
            public void SetData(UInt64 Data_)
            {
                _TypeCode = TypeCode.UInt64;
                _Data.ValueULong = Data_;
            }
            public void SetData(float Data_)
            {
                _TypeCode = TypeCode.Single;
                _Data.ValueFloat = Data_;
            }
            public void SetData(double Data_)
            {
                _TypeCode = TypeCode.Double;
                _Data.ValueDouble = Data_;
            }
            public void SetData(TimePoint Data_)
            {
                SetData(Data_.Ticks);
            }
            public void SetData(DateTime Data_)
            {
                SetData(Data_.ToTimePoint().Ticks);
            }
        }
    }
}