using System;
using System.Collections.Generic;

namespace rso
{
    namespace core
    {
        public class JsonDataObject : JsonData
        {
            JsonDataArray _JsonDataArray = new JsonDataArray();
            List<string> _Names = new List<string>();
            Dictionary<string, Int32> _NameIndices = new Dictionary<string, Int32>();

            bool Parse(ref string Text_, ref Int32 Index_)
            {
                JsonParser.PopBlank(ref Text_, ref Index_);

                Add(JsonParser.PopString(ref Text_, ref Index_));

                JsonParser.PopBlank(ref Text_, ref Index_);

                if (Text_[Index_++] != ':')
                    throw new Exception("Invalid Json String (Expected ':')");

                JsonParser.PopBlank(ref Text_, ref Index_);

                _JsonDataArray.Add(JsonParser.Parse(ref Text_, ref Index_));

                JsonParser.PopBlank(ref Text_, ref Index_);

                switch (Text_[Index_++])
                {
                    case ',':
                        return true;

                    case '}':
                        return false;

                    default:
                        throw new Exception("Invalid Json Object");
                }
            }
            public JsonDataObject()
            {
            }
            public JsonDataObject(SProto Proto_)
            {
                Proto_.Pop(this);
            }
            public JsonDataObject(string Text_, ref Int32 Index_)
            {
                if (Text_[Index_] != '{')
                    throw new Exception("Invalid Json Object (Expected '{')");

                ++Index_;
                if (Index_ >= Text_.Length)
                    throw new Exception("Invalid Json Object (Expected '}')");

                if (Text_[Index_] == '}')
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
            public JsonData this[string Name_]
            {
                get
                {
                    return _JsonDataArray[_NameIndices[Name_]];
                }
                set
                {
                    if (_NameIndices.ContainsKey(Name_))
                        _JsonDataArray[_NameIndices[Name_]] = value;
                    else
                        _NameIndices[Name_] = Add(Name_, value);
                }
            }
            public override string ToString(string Name_, string Indentation_)
            {
                string Str = Indentation_ + JsonGlobal.GetNameString(Name_) + '{';
                Indentation_ = Indentation_.PushIndentation();

                if (_JsonDataArray.Count > 0)
                {
                    Str += '\n';
                    Str += _JsonDataArray[0].ToString(_Names[0], Indentation_);
                }

                for (Int32 i = 1; i < _JsonDataArray.Count; ++i)
                {
                    Str += ",\n";
                    Str += _JsonDataArray[i].ToString(_Names[i], Indentation_);
                }
                Str += '\n';

                Indentation_ = Indentation_.PopIndentation();
                Str += Indentation_;
                Str += '}';

                return Str;
            }
            void Add(string Name_)
            {
                if (_NameIndices.ContainsKey(Name_))
                    throw new Exception("Duplicate Json Key : " + Name_);

                _NameIndices.Add(Name_, _NameIndices.Count);
                _Names.Add(Name_);
            }
            public Int32 Add(string Name_, JsonData Data_)
            {
                Add(Name_);
                return _JsonDataArray.Add(Data_);
            }
            public JsonDataObject Push(string Name_, bool Data_)
            {
                Add(Name_);
                _JsonDataArray.Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, char Data_)
            {
                Add(Name_);
                _JsonDataArray.Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, SByte Data_)
            {
                Add(Name_);
                _JsonDataArray.Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, Byte Data_)
            {
                Add(Name_);
                _JsonDataArray.Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, Int16 Data_)
            {
                Add(Name_);
                _JsonDataArray.Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, UInt16 Data_)
            {
                Add(Name_);
                _JsonDataArray.Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, Int32 Data_)
            {
                Add(Name_);
                _JsonDataArray.Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, UInt32 Data_)
            {
                Add(Name_);
                _JsonDataArray.Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, Int64 Data_)
            {
                Add(Name_);
                _JsonDataArray.Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, UInt64 Data_)
            {
                Add(Name_);
                _JsonDataArray.Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, float Data_)
            {
                Add(Name_);
                _JsonDataArray.Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, double Data_)
            {
                Add(Name_);
                _JsonDataArray.Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, string Data_)
            {
                Add(Name_);
                _JsonDataArray.Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, TimePoint Data_)
            {
                Add(Name_);
                _JsonDataArray.Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, DateTime Data_)
            {
                Add(Name_);
                _JsonDataArray.Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, CStream Data_)
            {
                Add(Name_);
                _JsonDataArray.Push(Data_);
                return this;
            }
            public JsonDataObject Push(string Name_, SProto Data_)
            {
                Add(Name_);
                _JsonDataArray.Push(Data_);
                return this;
            }

            public JsonDataObject Pop(string Name_, ref bool Data_)
            {
                _JsonDataArray.Pop(_NameIndices[Name_], ref Data_);
                return this;
            }
            public JsonDataObject Pop(string Name_, ref char Data_)
            {
                _JsonDataArray.Pop(_NameIndices[Name_], ref Data_);
                return this;
            }
            public JsonDataObject Pop(string Name_, ref SByte Data_)
            {
                _JsonDataArray.Pop(_NameIndices[Name_], ref Data_);
                return this;
            }
            public JsonDataObject Pop(string Name_, ref Byte Data_)
            {
                _JsonDataArray.Pop(_NameIndices[Name_], ref Data_);
                return this;
            }
            public JsonDataObject Pop(string Name_, ref Int16 Data_)
            {
                _JsonDataArray.Pop(_NameIndices[Name_], ref Data_);
                return this;
            }
            public JsonDataObject Pop(string Name_, ref UInt16 Data_)
            {
                _JsonDataArray.Pop(_NameIndices[Name_], ref Data_);
                return this;
            }
            public JsonDataObject Pop(string Name_, ref Int32 Data_)
            {
                _JsonDataArray.Pop(_NameIndices[Name_], ref Data_);
                return this;
            }
            public JsonDataObject Pop(string Name_, ref UInt32 Data_)
            {
                _JsonDataArray.Pop(_NameIndices[Name_], ref Data_);
                return this;
            }
            public JsonDataObject Pop(string Name_, ref Int64 Data_)
            {
                _JsonDataArray.Pop(_NameIndices[Name_], ref Data_);
                return this;
            }
            public JsonDataObject Pop(string Name_, ref UInt64 Data_)
            {
                _JsonDataArray.Pop(_NameIndices[Name_], ref Data_);
                return this;
            }
            public JsonDataObject Pop(string Name_, ref float Data_)
            {
                _JsonDataArray.Pop(_NameIndices[Name_], ref Data_);
                return this;
            }
            public JsonDataObject Pop(string Name_, ref double Data_)
            {
                _JsonDataArray.Pop(_NameIndices[Name_], ref Data_);
                return this;
            }
            public JsonDataObject Pop(string Name_, ref string Data_)
            {
                _JsonDataArray.Pop(_NameIndices[Name_], ref Data_);
                return this;
            }
            public JsonDataObject Pop(string Name_, ref TimePoint Data_)
            {
                _JsonDataArray.Pop(_NameIndices[Name_], ref Data_);
                return this;
            }
            public JsonDataObject Pop(string Name_, ref DateTime Data_)
            {
                _JsonDataArray.Pop(_NameIndices[Name_], ref Data_);
                return this;
            }
            public JsonDataObject Pop(string Name_, CStream Data_)
            {
                _JsonDataArray.Pop(_NameIndices[Name_], Data_);
                return this;
            }
            public JsonDataObject Pop(string Name_, SProto Data_)
            {
                _JsonDataArray.Pop(_NameIndices[Name_], Data_);
                return this;
            }
            public JsonDataObject Push<T>(string Name_, T Data_)
            {
                // switch 말고 다른 방법은 없는가?
                switch (Type.GetTypeCode(typeof(T)))
                {
                    case TypeCode.Boolean:
                        Push(Name_, (bool)(object)Data_);
                        break;
                    case TypeCode.SByte:
                        Push(Name_, (SByte)(object)Data_);
                        break;
                    case TypeCode.Byte:
                    case TypeCode.Char:
                        Push(Name_, (Byte)(object)Data_);
                        break;
                    case TypeCode.Int16:
                        Push(Name_, (Int16)(object)Data_);
                        break;
                    case TypeCode.UInt16:
                        Push(Name_, (UInt16)(object)Data_);
                        break;
                    case TypeCode.Int32:
                        Push(Name_, (Int32)(object)Data_);
                        break;
                    case TypeCode.UInt32:
                        Push(Name_, (UInt32)(object)Data_);
                        break;
                    case TypeCode.Int64:
                        Push(Name_, (Int64)(object)Data_);
                        break;
                    case TypeCode.UInt64:
                        Push(Name_, (UInt64)(object)Data_);
                        break;
                    case TypeCode.Single:
                        Push(Name_, (float)(object)Data_);
                        break;
                    case TypeCode.Double:
                        Push(Name_, (double)(object)Data_);
                        break;
                    case TypeCode.String:
                        Push(Name_, (string)(object)Data_);
                        break;
                    case TypeCode.DateTime:
                        Push(Name_, (DateTime)(object)Data_);
                        break;
                    case TypeCode.Object:
                        {
                            switch (typeof(T).ToString())
                            {
                                case "rso.core.TimePoint":
                                    Push(Name_, (TimePoint)(object)Data_);
                                    break;
                                default:
                                    ((SProto)(object)Data_).Pop(this);
                                    break;
                            }
                        }
                        break;
                    default:
                        throw new Exception("Invalid TypeCode : " + Type.GetTypeCode(typeof(T)).ToString());
                }

                return this;
            }
            // 아래 함수들은 C++ 코드와 호환이 필요하지만 C#또는 C#버전의 한계로 인한 임시 코드
            public JsonDataObject Push<TValue>(string Name_, TValue[] Data_)
            {
                var Collection = new JsonDataArray();
                foreach (var i in Data_)
                    Collection.Push(i);

                Add(Name_, Collection);
                return this;
            }
            public JsonDataObject Pop(string Name_, ref string[] Data_)
            {
                var Collection = (JsonDataArray)this[Name_];

                for (Int32 i = 0; i < Collection.Count; ++i)
                    Collection.Pop(i, ref Data_[i]);

                return this;
            }
            public JsonDataObject Pop<T>(string Name_, ref T Data_)
            {
                // switch 말고 다른 방법은 없는가?
                switch (Type.GetTypeCode(typeof(T)))
                {
                    case TypeCode.Boolean:
                        {
                            bool Data = false;
                            Pop(Name_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.SByte:
                        {
                            SByte Data = 0;
                            Pop(Name_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.Byte:
                    case TypeCode.Char:
                        {
                            Byte Data = 0;
                            Pop(Name_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.Int16:
                        {
                            Int16 Data = 0;
                            Pop(Name_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.UInt16:
                        {
                            UInt16 Data = 0;
                            Pop(Name_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.Int32:
                        {
                            Int32 Data = 0;
                            Pop(Name_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.UInt32:
                        {
                            UInt32 Data = 0;
                            Pop(Name_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.Int64:
                        {
                            Int64 Data = 0;
                            Pop(Name_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.UInt64:
                        {
                            UInt64 Data = 0;
                            Pop(Name_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.Single:
                        {
                            float Data = 0.0f;
                            Pop(Name_, ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.Double:
                        {
                            double Data = 0.0;
                            Pop(Name_, ref Data);
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
                            Pop(Name_, ref Data);
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
                                        Pop(Name_, ref Data);
                                        Data_ = (T)(object)Data;
                                    }
                                    break;
                                default:
                                    ((SProto)(object)Data_).Push(this);
                                    break;
                            }
                        }
                        break;
                    default:
                        throw new Exception("Invalid TypeCode : " + Type.GetTypeCode(typeof(T)).ToString());
                }

                return this;
            }
            public JsonDataObject Pop<TValue>(string Name_, ref TValue[] Data_)
            {
                var Collection = (JsonDataArray)this[Name_];

                for (Int32 i = 0; i < Collection.Count; ++i)
                    Collection.Pop(i, ref Data_[i]);

                return this;
            }
            public JsonDataObject Push<TKey, TValue>(string Name_, Dictionary<TKey, TValue>[] Data_)
            {
                var Collection = new JsonDataArray();
                foreach (var i in Data_)
                {
                    var InnerCollection = new JsonDataArray();

                    foreach (var ii in i)
                        InnerCollection.Push(ii.Key);

                    Collection.Add(InnerCollection);
                }

                Add(Name_, Collection);
                return this;
            }
            public JsonDataObject Pop(string Name_, ref Dictionary<string, string>[] Data_)
            {
                var Collection = (JsonDataArray)this[Name_];

                for (Int32 i = 0; i < Collection.Count; ++i)
                    Collection.Pop(i, ref Data_[i]);

                return this;
            }
            public JsonDataObject Pop<TValue>(string Name_, ref Dictionary<string, TValue>[] Data_) where TValue : new()
            {
                var Collection = (JsonDataArray)this[Name_];

                for (Int32 i = 0; i < Collection.Count; ++i)
                {
                    var InnerCollection = (JsonDataArray)Collection[i];

                    for (Int32 ii = 0; ii < InnerCollection.Count; ++ii)
                    {
                        string Key = "";
                        InnerCollection.Pop(ii, ref Key);
                        Data_[i].Add(Key, new TValue());
                    }

                }

                return this;
            }
            public JsonDataObject Pop<TKey>(string Name_, ref Dictionary<TKey, string>[] Data_) where TKey : new()
            {
                var Collection = (JsonDataArray)this[Name_];

                for (Int32 i = 0; i < Collection.Count; ++i)
                {
                    var InnerCollection = (JsonDataArray)Collection[i];

                    for (Int32 ii = 0; ii < InnerCollection.Count; ++ii)
                    {
                        var Key = new TKey();
                        InnerCollection.Pop(ii, ref Key);
                        Data_[i].Add(Key, "");
                    }
                }

                return this;
            }
            public JsonDataObject Pop<TKey, TValue>(string Name_, ref Dictionary<TKey, TValue>[] Data_) where TKey : new() where TValue : new()
            {
                var Collection = (JsonDataArray)this[Name_];

                for (Int32 i = 0; i < Collection.Count; ++i)
                {
                    var InnerCollection = (JsonDataArray)Collection[i];

                    for (Int32 ii = 0; ii < InnerCollection.Count; ++ii)
                    {
                        var Key = new TKey();
                        InnerCollection.Pop(ii, ref Key);
                        Data_[i].Add(Key, new TValue());
                    }
                }

                return this;
            }
            public JsonDataObject Push<TValue>(string Name_, List<TValue> Data_)
            {
                var Collection = new JsonDataArray();
                foreach (var i in Data_)
                    Collection.Push(i);

                Add(Name_, Collection);
                return this;
            }
            // 이 함수는 게임프로토콜 에 적용된 C++ 코드와 호환위해 임시 작성
            public JsonDataObject Push<TValue>(string Name_, List<TValue[]> Data_)
            {
                var Collection = new JsonDataArray();
                foreach (var i in Data_)
                {
                    var InnerCollection = new JsonDataArray();
                    foreach (var ii in i)
                        InnerCollection.Push(ii);

                    Collection.Add(InnerCollection);
                }

                Add(Name_, Collection);
                return this;
            }
            public JsonDataObject Pop(string Name_, ref List<string> Data_)
            {
                var Collection = (JsonDataArray)this[Name_];

                for (Int32 i = 0; i < Collection.Count; ++i)
                {
                    string Value = "";
                    Collection.Pop(i, ref Value);
                    Data_.Add(Value);
                }

                return this;
            }
            public JsonDataObject Pop<TValue>(string Name_, ref List<TValue> Data_) where TValue : new()
            {
                var Collection = (JsonDataArray)this[Name_];

                for (Int32 i = 0; i < Collection.Count; ++i)
                {
                    var Value = new TValue();
                    Collection.Pop(i, ref Value);
                    Data_.Add(Value);
                }

                return this;
            }
            // 이 함수는 게임프로토콜 에 적용된 C++ 코드와 호환위해 임시 작성
            public JsonDataObject Pop<TValue>(string Name_, ref List<TValue[]> Data_) where TValue : new()
            {
                var Collection = (JsonDataArray)this[Name_];

                for (Int32 i = 0; i < Collection.Count; ++i)
                {
                    var InnerCollection = (JsonDataArray)Collection[i];
                    var Value = new TValue[InnerCollection.Count];

                    for (Int32 ii = 0; ii < InnerCollection.Count; ++ii)
                    {
                        Value[ii] = new TValue();
                        InnerCollection.Pop(i, ref Value[ii]);
                    }

                    Data_.Add(Value);
                }

                return this;
            }
            public JsonDataObject Push<TValue>(string Name_, HashSet<TValue> Data_)
            {
                var Collection = new JsonDataArray();
                foreach (var i in Data_)
                    Collection.Push(i);

                Add(Name_, Collection);
                return this;
            }
            public JsonDataObject Pop(string Name_, ref HashSet<string> Data_)
            {
                var Collection = (JsonDataArray)this[Name_];

                for (Int32 i = 0; i < Collection.Count; ++i)
                {
                    string Value = "";
                    Collection.Pop(i, ref Value);
                    Data_.Add(Value);
                }

                return this;
            }
            public JsonDataObject Pop<TValue>(string Name_, ref HashSet<TValue> Data_) where TValue : new()
            {
                var Collection = (JsonDataArray)this[Name_];

                for (Int32 i = 0; i < Collection.Count; ++i)
                {
                    var Value = new TValue();
                    Collection.Pop(i, ref Value);
                    Data_.Add(Value);
                }

                return this;
            }
            public JsonDataObject Push<TKey, TValue>(string Name_, Dictionary<TKey, TValue> Data_)
            {
                var Collection = new JsonDataArray();
                foreach (var i in Data_)
                    Collection.Push(i.Key);

                Add(Name_, Collection);
                return this;
            }
            public JsonDataObject Pop(string Name_, ref Dictionary<string, string> Data_)
            {
                var Collection = (JsonDataArray)this[Name_];

                for (Int32 i = 0; i < Collection.Count; ++i)
                {
                    string Key = "";
                    Collection.Pop(i, ref Key);
                    Data_.Add(Key, "");
                }

                return this;
            }
            public JsonDataObject Pop<TValue>(string Name_, ref Dictionary<string, TValue> Data_) where TValue : new()
            {
                var Collection = (JsonDataArray)this[Name_];

                for (Int32 i = 0; i < Collection.Count; ++i)
                {
                    string Key = "";
                    Collection.Pop(i, ref Key);
                    Data_.Add(Key, new TValue());
                }

                return this;
            }
            public JsonDataObject Pop<TKey>(string Name_, ref Dictionary<TKey, string> Data_) where TKey : new()
            {
                var Collection = (JsonDataArray)this[Name_];

                for (Int32 i = 0; i < Collection.Count; ++i)
                {
                    var Key = new TKey();
                    Collection.Pop(i, ref Key);
                    Data_.Add(Key, "");
                }

                return this;
            }
            public JsonDataObject Pop<TKey, TValue>(string Name_, ref Dictionary<TKey, TValue> Data_) where TKey : new() where TValue : new()
            {
                var Collection = (JsonDataArray)this[Name_];

                for (Int32 i = 0; i < Collection.Count; ++i)
                {
                    var Key = new TKey();
                    Collection.Pop(i, ref Key);
                    Data_.Add(Key, new TValue());
                }

                return this;
            }
            public JsonDataObject Push<TKey, TValue>(string Name_, SortedDictionary<TKey, TValue> Data_)
            {
                var Collection = new JsonDataArray();
                foreach (var i in Data_)
                    Collection.Push(i.Key);

                Add(Name_, Collection);
                return this;
            }
            public JsonDataObject Pop(string Name_, ref SortedDictionary<string, string> Data_)
            {
                var Collection = (JsonDataArray)this[Name_];

                for (Int32 i = 0; i < Collection.Count; ++i)
                {
                    string Key = "";
                    Collection.Pop(i, ref Key);
                    Data_.Add(Key, "");
                }

                return this;
            }
            public JsonDataObject Pop<TValue>(string Name_, ref SortedDictionary<string, TValue> Data_) where TValue : new()
            {
                var Collection = (JsonDataArray)this[Name_];

                for (Int32 i = 0; i < Collection.Count; ++i)
                {
                    string Key = "";
                    Collection.Pop(i, ref Key);
                    Data_.Add(Key, new TValue());
                }

                return this;
            }
            public JsonDataObject Pop<TKey>(string Name_, ref SortedDictionary<TKey, string> Data_) where TKey : new()
            {
                var Collection = (JsonDataArray)this[Name_];

                for (Int32 i = 0; i < Collection.Count; ++i)
                {
                    var Key = new TKey();
                    Collection.Pop(i, ref Key);
                    Data_.Add(Key, "");
                }

                return this;
            }
            public JsonDataObject Pop<TKey, TValue>(string Name_, ref SortedDictionary<TKey, TValue> Data_) where TKey : new() where TValue : new()
            {
                var Collection = (JsonDataArray)this[Name_];

                for (Int32 i = 0; i < Collection.Count; ++i)
                {
                    var Key = new TKey();
                    Collection.Pop(i, ref Key);
                    Data_.Add(Key, new TValue());
                }

                return this;
            }
            public JsonDataObject Push<TKey>(string Name_, MultiSet<TKey> Data_)
            {
                var Collection = new JsonDataArray();
                foreach (var i in Data_)
                    Collection.Push(i);

                Add(Name_, Collection);
                return this;
            }
            public JsonDataObject Pop(string Name_, ref MultiSet<string> Data_)
            {
                var Collection = (JsonDataArray)this[Name_];

                for (Int32 i = 0; i < Collection.Count; ++i)
                {
                    string Key = "";
                    Collection.Pop(i, ref Key);
                    Data_.Add(Key);
                }

                return this;
            }
            public JsonDataObject Pop<TKey>(string Name_, ref MultiSet<TKey> Data_) where TKey : new()
            {
                var Collection = (JsonDataArray)this[Name_];

                for (Int32 i = 0; i < Collection.Count; ++i)
                {
                    var Key = new TKey();
                    Collection.Pop(i, ref Key);
                    Data_.Add(Key);
                }

                return this;
            }
            public JsonDataObject Push<TKey, TValue>(string Name_, MultiMap<TKey, TValue> Data_)
            {
                var Collection = new JsonDataArray();
                foreach (var i in Data_)
                    Collection.Push(i.Key);

                Add(Name_, Collection);
                return this;
            }
            public JsonDataObject Pop(string Name_, ref MultiMap<string, string> Data_)
            {
                var Collection = (JsonDataArray)this[Name_];

                for (Int32 i = 0; i < Collection.Count; ++i)
                {
                    string Key = "";
                    Collection.Pop(i, ref Key);
                    Data_.Add(Key, "");
                }

                return this;
            }
            public JsonDataObject Pop<TValue>(string Name_, ref MultiMap<string, TValue> Data_) where TValue : new()
            {
                var Collection = (JsonDataArray)this[Name_];

                for (Int32 i = 0; i < Collection.Count; ++i)
                {
                    string Key = "";
                    Collection.Pop(i, ref Key);
                    Data_.Add(Key, new TValue());
                }

                return this;
            }
            public JsonDataObject Pop<TKey>(string Name_, ref MultiMap<TKey, string> Data_) where TKey : new()
            {
                var Collection = (JsonDataArray)this[Name_];

                for (Int32 i = 0; i < Collection.Count; ++i)
                {
                    var Key = new TKey();
                    Collection.Pop(i, ref Key);
                    Data_.Add(Key, "");
                }

                return this;
            }
            public JsonDataObject Pop<TKey, TValue>(string Name_, ref MultiMap<TKey, TValue> Data_) where TKey : new() where TValue : new()
            {
                var Collection = (JsonDataArray)this[Name_];

                for (Int32 i = 0; i < Collection.Count; ++i)
                {
                    var Key = new TKey();
                    Collection.Pop(i, ref Key);
                    Data_.Add(Key, new TValue());
                }

                return this;
            }
            public void Clear()
            {
                _NameIndices.Clear();
                _Names.Clear();
                _JsonDataArray.Clear();
            }
        }
    }
}