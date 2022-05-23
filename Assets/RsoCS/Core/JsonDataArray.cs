using System;
using System.Collections;
using System.Collections.Generic;

namespace rso
{
    namespace core
    {
        public class JsonDataArray : JsonData, IEnumerable
        {
            List<JsonData> _Array = new List<JsonData>();
            List<JsonData>.Enumerator _Current;
            public Int32 Count
            {
                get { return _Array.Count; }
            }
            public void Reset()
            {
                _Current = _Array.GetEnumerator();
            }
            public IEnumerator GetEnumerator()
            {
                return _Array.GetEnumerator();
            }
            bool Parse(ref string Text_, ref Int32 Index_)
            {
                JsonParser.PopBlank(ref Text_, ref Index_);

                if (Text_[Index_] == ']')
                {
                    ++Index_;
                    return false;
                }

                _Array.Add(JsonParser.Parse(ref Text_, ref Index_));

                JsonParser.PopBlank(ref Text_, ref Index_);

                switch (Text_[Index_++])
                {
                    case ',':
                        return true;

                    case ']':
                        return false;

                    default:
                        throw new Exception("Invalid Json Object");
                }
            }
            public JsonDataArray()
            {
            }
            public JsonDataArray(string Text_, ref Int32 Index_)
            {
                if (Text_[Index_] != '[')
                    throw new Exception("Invalid Json Object (Expected '[')");

                ++Index_;
                if (Index_ >= Text_.Length)
                    throw new Exception("Invalid Json Object (Expected ']')");

                if (Text_[Index_] == ']')
                {
                    ++Index_;
                    return;
                }

                if (!Parse(ref Text_, ref Index_))
                    return;

                while (Index_ < Text_.Length)
                {
                    JsonParser.PopBlank(ref Text_, ref Index_);

                    if (!Parse(ref Text_, ref Index_))
                        return;
                }
            }
            public JsonData this[Int32 Index_]
            {
                get
                {
                    return _Array[Index_];
                }
                set
                {
                    if (Index_ < _Array.Count)
                        _Array[Index_] = value;
                    else
                        throw new Exception("Array Index Over Count[" + _Array.Count.ToString() + "] + Index[" + Index_.ToString() + "]");
                }
            }
            public override string ToString(string Name_, string Indentation_)
            {
                string Str = Indentation_ + JsonGlobal.GetNameString(Name_) + '[';
                Indentation_ = Indentation_.PushIndentation();

                if (_Array.Count > 0)
                {
                    Str += '\n';
                    Str += _Array[0].ToString(null, Indentation_);
                }

                for (Int32 i = 1; i < _Array.Count; ++i)
                {
                    Str += ",\n";
                    Str += _Array[i].ToString(null, Indentation_);
                }
                Str += '\n';

                Indentation_ = Indentation_.PopIndentation();
                Str += Indentation_;
                Str += ']';

                return Str;
            }
            public Int32 Add(JsonData Data_)
            {
                _Array.Add(Data_);
                return _Array.Count - 1;
            }
            public JsonDataArray Push(bool Data_)
            {
                _Array.Add(new JsonDataBool(Data_));
                return this;
            }
            public JsonDataArray Push(char Data_)
            {
                _Array.Add(new JsonDataNumber(Data_));
                return this;
            }
            public JsonDataArray Push(SByte Data_)
            {
                _Array.Add(new JsonDataNumber(Data_));
                return this;
            }
            public JsonDataArray Push(Byte Data_)
            {
                _Array.Add(new JsonDataNumber(Data_));
                return this;
            }
            public JsonDataArray Push(Int16 Data_)
            {
                _Array.Add(new JsonDataNumber(Data_));
                return this;
            }
            public JsonDataArray Push(UInt16 Data_)
            {
                _Array.Add(new JsonDataNumber(Data_));
                return this;
            }
            public JsonDataArray Push(Int32 Data_)
            {
                _Array.Add(new JsonDataNumber(Data_));
                return this;
            }
            public JsonDataArray Push(UInt32 Data_)
            {
                _Array.Add(new JsonDataNumber(Data_));
                return this;
            }
            public JsonDataArray Push(Int64 Data_)
            {
                _Array.Add(new JsonDataNumber(Data_));
                return this;
            }
            public JsonDataArray Push(UInt64 Data_)
            {
                _Array.Add(new JsonDataNumber(Data_));
                return this;
            }
            public JsonDataArray Push(float Data_)
            {
                _Array.Add(new JsonDataNumber(Data_));
                return this;
            }
            public JsonDataArray Push(double Data_)
            {
                _Array.Add(new JsonDataNumber(Data_));
                return this;
            }
            public JsonDataArray Push(string Data_)
            {
                _Array.Add(new JsonDataString(Data_));
                return this;
            }
            public JsonDataArray Push(TimePoint Data_)
            {
                _Array.Add(new JsonDataNumber(Data_.Ticks));
                return this;
            }
            public JsonDataArray Push(DateTime Data_)
            {
                _Array.Add(new JsonDataNumber(Data_.ToTimePoint().Ticks));
                return this;
            }
            public JsonDataArray Push(CStream Data_)
            {
                _Array.Add(new JsonDataString(""));
                return this;
            }
            public JsonDataArray Push(SProto Data_)
            {
                var Collection = new JsonDataObject();
                _Array.Add(Collection);
                Data_.Pop(Collection);
                return this;
            }
            public JsonDataArray Pop(Int32 Index_, ref bool Data_)
            {
                Data_ = _Array[Index_].GetBool();
                return this;
            }
            public JsonDataArray Pop(Int32 Index_, ref char Data_)
            {
                Data_ = _Array[Index_].GetChar();
                return this;
            }
            public JsonDataArray Pop(Int32 Index_, ref SByte Data_)
            {
                Data_ = _Array[Index_].GetSByte();
                return this;
            }
            public JsonDataArray Pop(Int32 Index_, ref Byte Data_)
            {
                Data_ = _Array[Index_].GetByte();
                return this;
            }
            public JsonDataArray Pop(Int32 Index_, ref Int16 Data_)
            {
                Data_ = _Array[Index_].GetInt16();
                return this;
            }
            public JsonDataArray Pop(Int32 Index_, ref UInt16 Data_)
            {
                Data_ = _Array[Index_].GetUInt16();
                return this;
            }
            public JsonDataArray Pop(Int32 Index_, ref Int32 Data_)
            {
                Data_ = _Array[Index_].GetInt32();
                return this;
            }
            public JsonDataArray Pop(Int32 Index_, ref UInt32 Data_)
            {
                Data_ = _Array[Index_].GetUInt32();
                return this;
            }
            public JsonDataArray Pop(Int32 Index_, ref Int64 Data_)
            {
                Data_ = _Array[Index_].GetInt64();
                return this;
            }
            public JsonDataArray Pop(Int32 Index_, ref UInt64 Data_)
            {
                Data_ = _Array[Index_].GetUInt64();
                return this;
            }
            public JsonDataArray Pop(Int32 Index_, ref float Data_)
            {
                Data_ = _Array[Index_].GetFloat();
                return this;
            }
            public JsonDataArray Pop(Int32 Index_, ref double Data_)
            {
                Data_ = _Array[Index_].GetDouble();
                return this;
            }
            public JsonDataArray Pop(Int32 Index_, ref string Data_)
            {
                Data_ = _Array[Index_].GetString();
                return this;
            }
            public JsonDataArray Pop(Int32 Index_, ref TimePoint Data_)
            {
                Data_ = _Array[Index_].GetTimePoint();
                return this;
            }
            public JsonDataArray Pop(Int32 Index_, ref DateTime Data_)
            {
                Data_ = _Array[Index_].GetDateTime();
                return this;
            }
            public JsonDataArray Pop(Int32 Index_, CStream Data_)
            {
                Data_ = _Array[Index_].GetStream();
                return this;
            }
            public JsonDataArray Pop(Int32 Index_, SProto Data_)
            {
                Data_.Push((JsonDataObject)_Array[Index_]);
                return this;
            }
            // 아래 함수들은 C++ 코드와 호환이 필요하지만 C#또는 C#버전의 한계로 인한 임시 코드
            public JsonDataArray Push<T>(T Data_)
            {
                // switch 말고 다른 방법은 없는가?
                switch (Type.GetTypeCode(typeof(T)))
                {
                    case TypeCode.Boolean:
                        Push((bool)(object)Data_);
                        break;
                    case TypeCode.SByte:
                        Push((SByte)(object)Data_);
                        break;
                    case TypeCode.Byte:
                    case TypeCode.Char:
                        Push((Byte)(object)Data_);
                        break;
                    case TypeCode.Int16:
                        Push((Int16)(object)Data_);
                        break;
                    case TypeCode.UInt16:
                        Push((UInt16)(object)Data_);
                        break;
                    case TypeCode.Int32:
                        Push((Int32)(object)Data_);
                        break;
                    case TypeCode.UInt32:
                        Push((UInt32)(object)Data_);
                        break;
                    case TypeCode.Int64:
                        Push((Int64)(object)Data_);
                        break;
                    case TypeCode.UInt64:
                        Push((UInt64)(object)Data_);
                        break;
                    case TypeCode.Single:
                        Push((float)(object)Data_);
                        break;
                    case TypeCode.Double:
                        Push((double)(object)Data_);
                        break;
                    case TypeCode.String:
                        Push((string)(object)Data_);
                        break;
                    case TypeCode.DateTime:
                        Push((DateTime)(object)Data_);
                        break;
                    case TypeCode.Object:
                        {
                            switch (typeof(T).ToString())
                            {
                                case "rso.core.TimePoint":
                                    Push((TimePoint)(object)Data_);
                                    break;
                                case "rso.core.CStream":
                                    Push((CStream)(object)Data_);
                                    break;
                                default:
                                    Push((SProto)(object)Data_);
                                    break;
                            }
                        }
                        break;
                    default:
                        throw new Exception("Invalid TypeCode : " + Type.GetTypeCode(typeof(T)).ToString());
                }

                return this;
            }
            public JsonDataArray Pop<T>(Int32 Index_, ref T Data_)
            {
                // switch 말고 다른 방법은 없는가?
                switch (Type.GetTypeCode(typeof(T)))
                {
                    case TypeCode.Boolean:
                        {
                            bool Data = false;
                            Pop(Index_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.SByte:
                        {
                            SByte Data = 0;
                            Pop(Index_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.Byte:
                    case TypeCode.Char:
                        {
                            Byte Data = 0;
                            Pop(Index_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.Int16:
                        {
                            Int16 Data = 0;
                            Pop(Index_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.UInt16:
                        {
                            UInt16 Data = 0;
                            Pop(Index_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.Int32:
                        {
                            Int32 Data = 0;
                            Pop(Index_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.UInt32:
                        {
                            UInt32 Data = 0;
                            Pop(Index_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.Int64:
                        {
                            Int64 Data = 0;
                            Pop(Index_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.UInt64:
                        {
                            UInt64 Data = 0;
                            Pop(Index_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.Single:
                        {
                            float Data = 0.0f;
                            Pop(Index_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.Double:
                        {
                            double Data = 0.0;
                            Pop(Index_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    // string 은 new 불가로 제네릭 함수에서 처리 불가
                    //case TypeCode.String:
                    //{
                    //    string Data = "";
                    //    Pop(ref Data);
                    //    Data_ = (T)(object)Data;
                    //}
                    //break;
                    case TypeCode.DateTime:
                        {
                            var Data = (DateTime)(object)Data_;
                            Pop(Index_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.Object:
                        {
                            switch (typeof(T).ToString())
                            {
                                case "rso.core.TimePoint":
                                    {
                                        var Data = (TimePoint)(object)Data_;
                                        Pop(Index_, ref Data);
                                        Data_ = (T)(object)Data;
                                    }
                                    break;
                                case "rso.core.CStream":
                                    Pop(Index_, (CStream)(object)Data_);
                                    break;
                                default:
                                    {
                                        var Data = (SProto)(object)Data_;
                                        Pop(Index_, Data);
                                    }
                                    break;
                            }
                        }
                        break;
                    default:
                        throw new Exception("Invalid TypeCode : " + Type.GetTypeCode(typeof(T)).ToString());
                }

                return this;
            }
            public void Clear()
            {
                _Array.Clear();
            }
        }
    }
}