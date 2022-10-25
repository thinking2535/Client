using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace rso
{
    namespace core
    {
        public class CStream
        {
            Int32 _Head = 0;
            Int32 _Tail = 0;
            Int32 _SavedHead = 0;
            Int32 _SavedTail = 0;
            CVector<Byte> _Bytes = new CVector<Byte>();
            public CStream()
            {
            }
            public CStream(SProto Proto_)
            {
                Proto_.Pop(this);
            }
            public CStream(string FileName_)
            {
                LoadFile(FileName_);
            }
            public CStream(Byte[] Data_)
            {
                Push(Data_);
            }
            ~CStream()
            {
                Dispose();
            }
            public void Set(CStream Stream_)
            {
                _Head = Stream_._Head;
                _Tail = Stream_._Tail;
                _SavedHead = Stream_._SavedHead;
                _SavedTail = Stream_._SavedTail;
                _Bytes.Set(Stream_._Bytes);
            }
            public void Dispose()
            {
                Clear();
            }
            public override string ToString()
            {
                return Encoding.Default.GetString(_Bytes.Data, _Head, Size);
            }
            public void PopSize(Int32 Size_)
            {
                if (Size_ < Size)
                    _Head += Size_;
                else
                    Clear();
            }
            public CStream Pop(ref bool Value_)
            {
                if (_Bytes.Size - _Head < 1)
                    throw new Exception("Pop Fail Invalid Parameter");

                Value_ = Convert.ToBoolean(_Bytes[_Head]);
                PopSize(1);

                return this;
            }
            public CStream Pop(ref SByte Value_)
            {
                if (Size < 1)
                    throw new Exception("Pop Fail Invalid Parameter");

                Value_ = (SByte)_Bytes[_Head];
                PopSize(1);

                return this;
            }
            public CStream Pop(ref Byte Value_)
            {
                if (Size < 1)
                    throw new Exception("Pop Fail Invalid Parameter");

                Value_ = _Bytes[_Head];
                PopSize(1);

                return this;
            }
            public CStream Pop(ref Int16 Value_)
            {
                if (Size < 2)
                    throw new Exception("Pop Fail Invalid Parameter");

                if (!BitConverter.IsLittleEndian)
                {
                    var Data = _Bytes.Data.SubArray(_Head, 2);
                    Array.Reverse(Data);
                    Value_ = BitConverter.ToInt16(Data, 0);
                }
                else
                {
                    Value_ = BitConverter.ToInt16(_Bytes.Data, _Head);
                }

                PopSize(2);

                return this;
            }
            public CStream Pop(ref UInt16 Value_)
            {
                if (Size < 2)
                    throw new Exception("Pop Fail Invalid Parameter");

                if (!BitConverter.IsLittleEndian)
                {
                    var Data = _Bytes.Data.SubArray(_Head, 2);
                    Array.Reverse(Data);
                    Value_ = BitConverter.ToUInt16(Data, 0);
                }
                else
                {
                    Value_ = BitConverter.ToUInt16(_Bytes.Data, _Head);
                }

                PopSize(2);

                return this;
            }
            public CStream Pop(ref Int32 Value_)
            {
                if (Size < 4)
                    throw new Exception("Pop Fail Invalid Parameter");

                if (!BitConverter.IsLittleEndian)
                {
                    var Data = _Bytes.Data.SubArray(_Head, 4);
                    Array.Reverse(Data);
                    Value_ = BitConverter.ToInt32(Data, 0);
                }
                else
                {
                    Value_ = BitConverter.ToInt32(_Bytes.Data, _Head);
                }

                PopSize(4);

                return this;
            }
            public CStream Pop(ref UInt32 Value_)
            {
                if (Size < 4)
                    throw new Exception("Pop Fail Invalid Parameter");

                if (!BitConverter.IsLittleEndian)
                {
                    var Data = _Bytes.Data.SubArray(_Head, 4);
                    Array.Reverse(Data);
                    Value_ = BitConverter.ToUInt32(Data, 0);
                }
                else
                {
                    Value_ = BitConverter.ToUInt32(_Bytes.Data, _Head);
                }

                PopSize(4);

                return this;
            }
            public CStream Pop(ref Int64 Value_)
            {
                if (Size < 8)
                    throw new Exception("Pop Fail Invalid Parameter");

                if (!BitConverter.IsLittleEndian)
                {
                    var Data = _Bytes.Data.SubArray(_Head, 8);
                    Array.Reverse(Data);
                    Value_ = BitConverter.ToInt64(Data, 0);
                }
                else
                {
                    Value_ = BitConverter.ToInt64(_Bytes.Data, _Head);
                }

                PopSize(8);

                return this;
            }
            public CStream Pop(ref UInt64 Value_)
            {
                if (Size < 8)
                    throw new Exception("Pop Fail Invalid Parameter");

                if (!BitConverter.IsLittleEndian)
                {
                    var Data = _Bytes.Data.SubArray(_Head, 8);
                    Array.Reverse(Data);
                    Value_ = BitConverter.ToUInt64(Data, 0);
                }
                else
                {
                    Value_ = BitConverter.ToUInt64(_Bytes.Data, _Head);
                }

                PopSize(8);

                return this;
            }
            public CStream Pop(ref float Value_)
            {
                if (Size < 4)
                    throw new Exception("Pop Fail Invalid Parameter");

                if (!BitConverter.IsLittleEndian)
                {
                    var Data = _Bytes.Data.SubArray(_Head, 4);
                    Array.Reverse(Data);
                    Value_ = BitConverter.ToSingle(Data, 0);
                }
                else
                {
                    Value_ = BitConverter.ToSingle(_Bytes.Data, _Head);
                }

                PopSize(4);

                return this;
            }
            public CStream Pop(ref double Value_)
            {
                if (Size < 8)
                    throw new Exception("Pop Fail Invalid Parameter");

                if (!BitConverter.IsLittleEndian)
                {
                    var Data = _Bytes.Data.SubArray(_Head, 8);
                    Array.Reverse(Data);
                    Value_ = BitConverter.ToDouble(Data, 0);
                }
                else
                {
                    Value_ = BitConverter.ToDouble(_Bytes.Data, _Head);
                }

                PopSize(8);

                return this;
            }
            public CStream Pop(ref string Value_)
            {
                Int32 Length = 0;

                if (Size < 4)
                    throw new Exception("Pop Fail Invalid Parameter");

                if (!BitConverter.IsLittleEndian)
                {
                    var Data = _Bytes.Data.SubArray(_Head, 4);
                    Array.Reverse(Data);
                    Length = BitConverter.ToInt32(Data, 0);
                }
                else
                {
                    Length = BitConverter.ToInt32(_Bytes.Data, _Head);
                }

                Value_ = Encoding.Unicode.GetString(_Bytes.Data, _Head + 4, Length * 2);
                PopSize(4 + Length * 2);

                return this;
            }
            public CStream Pop(ref TimePoint Value_)
            {
                Pop(ref Value_.Ticks);

                return this;
            }
            public CStream Pop(ref Microseconds value)
            {
                Int64 Ticks = 0;
                Pop(ref Ticks);

                value = new Microseconds(Ticks);

                return this;
            }
            public CStream Pop(ref Milliseconds value)
            {
                Int64 Ticks = 0;
                Pop(ref Ticks);

                value = new Milliseconds(Ticks);

                return this;
            }
            public CStream Pop(ref Seconds value)
            {
                Int64 Ticks = 0;
                Pop(ref Ticks);

                value = new Seconds(Ticks);

                return this;
            }
            public CStream Pop(ref Minutes value)
            {
                Int64 Ticks = 0;
                Pop(ref Ticks);

                value = new Minutes(Ticks);

                return this;
            }
            public CStream Pop(ref Hours value)
            {
                Int64 Ticks = 0;
                Pop(ref Ticks);

                value = new Hours(Ticks);

                return this;
            }
            public CStream Pop(ref DateTime Value_)
            {
                Int16 year = 0;
                UInt16 month = 0;
                UInt16 day = 0;
                UInt16 hour = 0;
                UInt16 min = 0;
                UInt16 sec = 0;
                UInt32 fraction = 0;

                if (!BitConverter.IsLittleEndian)
                {
                    var Data2 = _Bytes.Data.SubArray(_Head, 2);
                    Array.Reverse(Data2);
                    year = BitConverter.ToInt16(Data2, 0);

                    _Bytes.Data.SubArray(_Head + 2, Data2);
                    Array.Reverse(Data2);
                    month = BitConverter.ToUInt16(Data2, 0);

                    _Bytes.Data.SubArray(_Head + 4, Data2);
                    Array.Reverse(Data2);
                    day = BitConverter.ToUInt16(Data2, 0);

                    _Bytes.Data.SubArray(_Head + 6, Data2);
                    Array.Reverse(Data2);
                    hour = BitConverter.ToUInt16(Data2, 0);

                    _Bytes.Data.SubArray(_Head + 8, Data2);
                    Array.Reverse(Data2);
                    min = BitConverter.ToUInt16(Data2, 0);

                    _Bytes.Data.SubArray(_Head + 10, Data2);
                    Array.Reverse(Data2);
                    sec = BitConverter.ToUInt16(Data2, 0);

                    var Data4 = _Bytes.Data.SubArray(_Head + 12, 4);
                    Array.Reverse(Data4);
                    fraction = BitConverter.ToUInt32(Data4, 0);

                }
                else
                {
                    year = BitConverter.ToInt16(_Bytes.Data, _Head);
                    month = BitConverter.ToUInt16(_Bytes.Data, _Head + 2);
                    day = BitConverter.ToUInt16(_Bytes.Data, _Head + 4);
                    hour = BitConverter.ToUInt16(_Bytes.Data, _Head + 6);
                    min = BitConverter.ToUInt16(_Bytes.Data, _Head + 8);
                    sec = BitConverter.ToUInt16(_Bytes.Data, _Head + 10);
                    fraction = BitConverter.ToUInt32(_Bytes.Data, _Head + 12);
                }

                Value_ = new DateTime(year, month, day, hour, min, sec);
                PopSize(16);

                return this;
            }
            public CStream Pop(CStream Stream_)
            {
                Int32 StreamSize = 0;

                if (Size < 4)
                    throw new Exception("Pop Fail Invalid Parameter");

                if (!BitConverter.IsLittleEndian)
                {
                    var Data = _Bytes.Data.SubArray(_Head, 4);
                    Array.Reverse(Data);
                    StreamSize = BitConverter.ToInt32(Data, 0);
                }
                else
                {
                    StreamSize = BitConverter.ToInt32(_Bytes.Data, _Head);
                }

                Stream_.Push(_Bytes.Data, _Head + 4, StreamSize);
                PopSize(4 + StreamSize);

                return this;
            }
            public CStream Pop(ref SProto Proto_)
            {
                Proto_.Push(this);
                return this;
            }
            public CStream Push(Byte Value_)
            {
                if (_Tail + 1 > _Bytes.Size)
                    _Bytes.PushBack(Value_);
                else
                    _Bytes[_Tail] = Value_;

                ++_Tail;

                return this;
            }
            public CStream Push(bool Value_)
            {
                Push(Convert.ToByte(Value_));

                return this;
            }
            public CStream Push(SByte Value_)
            {
                Push((Byte)Value_);

                return this;
            }
            public CStream Push(Int16 Value_)
            {
                Byte[] Bytes = BitConverter.GetBytes(Value_);

                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(Bytes);

                Push(Bytes);

                return this;
            }
            public CStream Push(UInt16 Value_)
            {
                Byte[] Bytes = BitConverter.GetBytes(Value_);

                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(Bytes);

                Push(Bytes);

                return this;
            }
            public CStream Push(Int32 Value_)
            {
                Byte[] Bytes = BitConverter.GetBytes(Value_);

                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(Bytes);

                Push(Bytes);

                return this;
            }
            public CStream Push(UInt32 Value_)
            {
                Byte[] Bytes = BitConverter.GetBytes(Value_);

                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(Bytes);

                Push(Bytes);

                return this;
            }
            public CStream Push(Int64 Value_)
            {
                Byte[] Bytes = BitConverter.GetBytes(Value_);

                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(Bytes);

                Push(Bytes);

                return this;
            }
            public CStream Push(UInt64 Value_)
            {
                Byte[] Bytes = BitConverter.GetBytes(Value_);

                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(Bytes);

                Push(Bytes);

                return this;
            }
            public CStream Push(float Value_)
            {
                Byte[] Bytes = BitConverter.GetBytes(Value_);

                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(Bytes);

                Push(Bytes);

                return this;
            }
            public CStream Push(double Value_)
            {
                Byte[] Bytes = BitConverter.GetBytes(Value_);

                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(Bytes);

                Push(Bytes);

                return this;
            }
            public CStream Push(string Value_)
            {
                if (_Tail + 4 + Value_.Length * 2 > _Bytes.Size)
                    _Bytes.Resize(_Tail + 4 + Value_.Length * 2);

                Byte[] LengthBytes = BitConverter.GetBytes(Value_.Length);

                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(LengthBytes);

                Array.Copy(LengthBytes, 0, _Bytes.Data, _Tail, 4);

                var StrBytes = Encoding.Unicode.GetBytes(Value_);
                Array.Copy(StrBytes, 0, _Bytes.Data, _Tail + 4, StrBytes.Length);

                _Tail += (4 + StrBytes.Length);

                return this;
            }
            public CStream Push(TimePoint Value_)
            {
                Push(Value_.Ticks);
                return this;
            }
            public CStream Push(Microseconds value)
            {
                Push(value.ticks);
                return this;
            }
            public CStream Push(Milliseconds value)
            {
                Push(value.ticks);
                return this;
            }
            public CStream Push(Seconds value)
            {
                Push(value.ticks);
                return this;
            }
            public CStream Push(Minutes value)
            {
                Push(value.ticks);
                return this;
            }
            public CStream Push(Hours value)
            {
                Push(value.ticks);
                return this;
            }
            public CStream Push(DateTime Value_)
            {
                if (_Tail + 16 > _Bytes.Size)
                    _Bytes.Resize(_Tail + 16);

                if (!BitConverter.IsLittleEndian)
                {
                    Byte[] Bytes2 = BitConverter.GetBytes((Int16)Value_.Year);
                    Array.Reverse(Bytes2);
                    Array.Copy(Bytes2, 0, _Bytes.Data, _Tail, 2);

                    Bytes2 = BitConverter.GetBytes((UInt16)Value_.Month);
                    Array.Reverse(Bytes2);
                    Array.Copy(Bytes2, 0, _Bytes.Data, _Tail + 2, 2);

                    Bytes2 = BitConverter.GetBytes((UInt16)Value_.Day);
                    Array.Reverse(Bytes2);
                    Array.Copy(Bytes2, 0, _Bytes.Data, _Tail + 4, 2);

                    Bytes2 = BitConverter.GetBytes((UInt16)Value_.Hour);
                    Array.Reverse(Bytes2);
                    Array.Copy(Bytes2, 0, _Bytes.Data, _Tail + 6, 2);

                    Bytes2 = BitConverter.GetBytes((UInt16)Value_.Minute);
                    Array.Reverse(Bytes2);
                    Array.Copy(Bytes2, 0, _Bytes.Data, _Tail + 8, 2);

                    Bytes2 = BitConverter.GetBytes((UInt16)Value_.Second);
                    Array.Reverse(Bytes2);
                    Array.Copy(Bytes2, 0, _Bytes.Data, _Tail + 10, 2);

                    Byte[] Bytes4 = BitConverter.GetBytes((UInt32)0);
                    Array.Reverse(Bytes4);
                    Array.Copy(Bytes4, 0, _Bytes.Data, _Tail + 12, 4);
                }
                else
                {
                    Array.Copy(BitConverter.GetBytes((Int16)Value_.Year), 0, _Bytes.Data, _Tail, 2);
                    Array.Copy(BitConverter.GetBytes((UInt16)Value_.Month), 0, _Bytes.Data, _Tail + 2, 2);
                    Array.Copy(BitConverter.GetBytes((UInt16)Value_.Day), 0, _Bytes.Data, _Tail + 4, 2);
                    Array.Copy(BitConverter.GetBytes((UInt16)Value_.Hour), 0, _Bytes.Data, _Tail + 6, 2);
                    Array.Copy(BitConverter.GetBytes((UInt16)Value_.Minute), 0, _Bytes.Data, _Tail + 8, 2);
                    Array.Copy(BitConverter.GetBytes((UInt16)Value_.Second), 0, _Bytes.Data, _Tail + 10, 2);
                    Array.Copy(BitConverter.GetBytes((UInt32)0), 0, _Bytes.Data, _Tail + 12, 4);
                }

                _Tail += 16;

                return this;
            }
            public CStream Push(SProto Proto_)
            {
                Proto_.Pop(this);
                return this;
            }
            public void Push(Byte[] Data_, Int32 Index_, Int32 Length_)
            {
                if (Length_ <= 0)
                    return;

                if (_Tail + Length_ > _Bytes.Size)
                    _Bytes.Resize(_Tail + Length_);

                Array.Copy(Data_, Index_, _Bytes.Data, _Tail, Length_);

                _Tail += Length_;
            }
            public CStream Push(Byte[] Data_)
            {
                Push(Data_, 0, Data_.Length);

                return this;
            }
            public CStream Push(CStream Stream_)
            {
                if (_Tail + 4 + Stream_.Size > _Bytes.Size)
                    _Bytes.Resize(_Tail + 4 + Stream_.Size);

                Byte[] LengthBytes = BitConverter.GetBytes(Stream_.Size);

                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(LengthBytes);

                Array.Copy(LengthBytes, 0, _Bytes.Data, _Tail, 4);
                Array.Copy(Stream_._Bytes.Data, Stream_._Head, _Bytes.Data, _Tail + 4, Stream_.Size);
                _Tail += (4 + Stream_.Size);

                return this;
            }
            public Int32 Head
            {
                get
                {
                    return _Head;
                }
            }
            public Int32 Tail
            {
                get
                {
                    return _Tail;
                }
                set
                {
                    if (value > _Bytes.Size)
                        throw (new Exception("Invalid Stream Tail"));

                    _Tail = value;
                }
            }
            public Byte[] Data
            {
                get
                {
                    return _Bytes.Data;
                }
            }
            public Int32 Size
            {
                get
                {
                    return (_Tail - _Head);
                }
            }
            void _SetAtStream(Int32 Index_, Byte[] Data_)
            {
                for (Int32 i = 0; i < Data_.Length; ++i)
                    _Bytes[Index_ + i] = Data_[i];
            }
            public void SetAt(Int32 Index_, Byte Value_)
            {
                _Bytes[Index_] = Value_;
            }
            public void SetAt(Int32 Index_, Int32 Value_)
            {
                Byte[] Bytes = BitConverter.GetBytes(Value_);

                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(Bytes);

                _SetAtStream(Index_, Bytes);
            }
            public void SetAt(Int32 Index_, UInt64 Value_)
            {
                Byte[] Bytes = BitConverter.GetBytes(Value_);

                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(Bytes);

                _SetAtStream(Index_, Bytes);
            }
            public void LoadFile(string FileName_)
            {
                Clear();
                Push(File.ReadAllBytes(Path.GetFullPath(FileName_)));
            }
            public void SaveFile(string FileName_)
            {
                var FullPath = Path.GetFullPath(FileName_);
                Directory.CreateDirectory(Path.GetDirectoryName(FullPath));
                File.WriteAllBytes(FullPath, _Bytes.Data);
            }
            public void LoadState()
            {
                _Head = _SavedHead;
                _Tail = _SavedTail;
            }
            public void SaveState()
            {
                _SavedHead = _Head;
                _SavedTail = _Tail;
            }
            public void Clear()
            {
                _Head = _Tail = 0;
            }
            public UInt64 CheckSum()
            {
                return CCore.GetCheckSum(_Bytes.Data, 0, _Bytes.Size);
            }
            // 아래 함수들로 중첩 컨테이너는 처리 불가하지만 Excel Export 용으로 1단계의 컨테이너는 처리 가능하도록 남겨둠.
            public CStream Push<T>(T Data_)
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
                                case "rso.core.Microseconds":
                                    Push((Microseconds)(object)Data_);
                                    break;
                                case "rso.core.Milliseconds":
                                    Push((Milliseconds)(object)Data_);
                                    break;
                                case "rso.core.Seconds":
                                    Push((Seconds)(object)Data_);
                                    break;
                                case "rso.core.Minutes":
                                    Push((Minutes)(object)Data_);
                                    break;
                                case "rso.core.Hours":
                                    Push((Hours)(object)Data_);
                                    break;
                                case "rso.core.CStream":
                                    Push((CStream)(object)Data_);
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
            public CStream Pop<T>(ref T Data_)
            {
                // switch 말고 다른 방법은 없는가?
                switch (Type.GetTypeCode(typeof(T)))
                {
                    case TypeCode.Boolean:
                        {
                            bool Data = false;
                            Pop(ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.SByte:
                        {
                            SByte Data = 0;
                            Pop(ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.Byte:
                    case TypeCode.Char:
                        {
                            Byte Data = 0;
                            Pop(ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.Int16:
                        {
                            Int16 Data = 0;
                            Pop(ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.UInt16:
                        {
                            UInt16 Data = 0;
                            Pop(ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.Int32:
                        {
                            Int32 Data = 0;
                            Pop(ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.UInt32:
                        {
                            UInt32 Data = 0;
                            Pop(ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.Int64:
                        {
                            Int64 Data = 0;
                            Pop(ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.UInt64:
                        {
                            UInt64 Data = 0;
                            Pop(ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.Single:
                        {
                            float Data = 0.0f;
                            Pop(ref Data);
                            Data_ = (T)(object)Data;
                        }
                        break;
                    case TypeCode.Double:
                        {
                            double Data = 0.0;
                            Pop(ref Data);
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
                            Pop(ref Data);
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
                                        Pop(ref Data);
                                        Data_ = (T)(object)Data;
                                    }
                                    break;
                                case "rso.core.Microseconds":
                                    {
                                        var Data = (Microseconds)(object)Data_;
                                        Pop(ref Data);
                                        Data_ = (T)(object)Data;
                                    }
                                    break;
                                case "rso.core.Milliseconds":
                                    {
                                        var Data = (Milliseconds)(object)Data_;
                                        Pop(ref Data);
                                        Data_ = (T)(object)Data;
                                    }
                                    break;
                                case "rso.core.Seconds":
                                    {
                                        var Data = (Seconds)(object)Data_;
                                        Pop(ref Data);
                                        Data_ = (T)(object)Data;
                                    }
                                    break;
                                case "rso.core.Minutes":
                                    {
                                        var Data = (Minutes)(object)Data_;
                                        Pop(ref Data);
                                        Data_ = (T)(object)Data;
                                    }
                                    break;
                                case "rso.core.Hours":
                                    {
                                        var Data = (Hours)(object)Data_;
                                        Pop(ref Data);
                                        Data_ = (T)(object)Data;
                                    }
                                    break;
                                case "rso.core.CStream":
                                    Pop((CStream)(object)Data_);
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
            // 아래 함수들은 C++ 코드와 호환이 필요하지만 C#또는 C#버전의 한계로 인한 임시 코드
            public CStream Push<TValue>(TValue[] Data_)
            {
                foreach (var i in Data_)
                    Push(i);

                return this;
            }
            public CStream Pop(ref string[] Data_) // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                for (Int32 i = 0; i < Data_.Length; ++i)
                    Pop(ref Data_[i]);

                return this;
            }
            public CStream Pop<TValue>(ref TValue[] Data_) where TValue : new()
            {
                for (Int32 i = 0; i < Data_.Length; ++i)
                    Pop(ref Data_[i]);

                return this;
            }
            public CStream Push<TValue>(List<TValue> Data_)
            {
                Push(Data_.Count);
                foreach (var i in Data_)
                    Push(i);

                return this;
            }
            public CStream Pop(ref List<string> Data_) // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                Int32 Cnt = 0;
                Pop(ref Cnt);
                for (Int32 i = 0; i < Cnt; ++i)
                {
                    string Value = "";
                    Pop(ref Value);
                    Data_.Add(Value);
                }

                return this;
            }
            public CStream Pop<TValue>(ref List<TValue> Data_) where TValue : new()
            {
                Int32 Cnt = 0;
                Pop(ref Cnt);
                for (Int32 i = 0; i < Cnt; ++i)
                {
                    var Value = new TValue();
                    Pop(ref Value);
                    Data_.Add(Value);
                }

                return this;
            }
            public CStream Push<TValue>(HashSet<TValue> Data_)
            {
                Push(Data_.Count);
                foreach (var i in Data_)
                    Push(i);

                return this;
            }
            public CStream Pop(ref HashSet<string> Data_) // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                Int32 Cnt = 0;
                Pop(ref Cnt);
                for (Int32 i = 0; i < Cnt; ++i)
                {
                    string Value = "";
                    Pop(ref Value);
                    Data_.Add(Value);
                }

                return this;
            }
            public CStream Pop<TValue>(ref HashSet<TValue> Data_) where TValue : new()
            {
                Int32 Cnt = 0;
                Pop(ref Cnt);
                for (Int32 i = 0; i < Cnt; ++i)
                {
                    var Value = new TValue();
                    Pop(ref Value);
                    Data_.Add(Value);
                }

                return this;
            }
            public CStream Push<TKey, TValue>(KeyValuePair<TKey, TValue> Data_)
            {
                Push(Data_.Key);
                Push(Data_.Value);

                return this;
            }
            public CStream Push<TKey, TValue>(Dictionary<TKey, TValue> Data_)
            {
                Push(Data_.Count);
                foreach (var i in Data_)
                    Push(i);

                return this;
            }
            public CStream Pop(ref Dictionary<string, string> Data_) // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                Int32 Cnt = 0;
                Pop(ref Cnt);
                for (Int32 i = 0; i < Cnt; ++i)
                {
                    string Key = "";
                    string Value = "";
                    Pop(ref Key);
                    Pop(ref Value);
                    Data_.Add(Key, Value);
                }

                return this;
            }
            public CStream Pop<TValue>(ref Dictionary<string, TValue> Data_) where TValue : new() // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                Int32 Cnt = 0;
                Pop(ref Cnt);
                for (Int32 i = 0; i < Cnt; ++i)
                {
                    string Key = "";
                    var Value = new TValue();
                    Pop(ref Key);
                    Pop(ref Value);
                    Data_.Add(Key, Value);
                }

                return this;
            }
            public CStream Pop<TKey>(ref Dictionary<TKey, string> Data_) where TKey : new() // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                Int32 Cnt = 0;
                Pop(ref Cnt);
                for (Int32 i = 0; i < Cnt; ++i)
                {
                    var Key = new TKey();
                    string Value = "";
                    Pop(ref Key);
                    Pop(ref Value);
                    Data_.Add(Key, Value);
                }

                return this;
            }
            public CStream Pop<TKey, TValue>(ref Dictionary<TKey, TValue> Data_) where TKey : new() where TValue : new()
            {
                Int32 Cnt = 0;
                Pop(ref Cnt);
                for (Int32 i = 0; i < Cnt; ++i)
                {
                    var Key = new TKey();
                    var Value = new TValue();
                    Pop(ref Key);
                    Pop(ref Value);
                    Data_.Add(Key, Value);
                }

                return this;
            }
            public CStream Push<TKey, TValue>(SortedDictionary<TKey, TValue> Data_)
            {
                Push(Data_.Count);
                foreach (var i in Data_)
                    Push(i);

                return this;
            }
            public CStream Pop(ref SortedDictionary<string, string> Data_) // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                Int32 Cnt = 0;
                Pop(ref Cnt);
                for (Int32 i = 0; i < Cnt; ++i)
                {
                    string Key = "";
                    string Value = "";
                    Pop(ref Key);
                    Pop(ref Value);
                    Data_.Add(Key, Value);
                }

                return this;
            }
            public CStream Pop<TValue>(ref SortedDictionary<string, TValue> Data_) where TValue : new() // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                Int32 Cnt = 0;
                Pop(ref Cnt);
                for (Int32 i = 0; i < Cnt; ++i)
                {
                    string Key = "";
                    var Value = new TValue();
                    Pop(ref Key);
                    Pop(ref Value);
                    Data_.Add(Key, Value);
                }

                return this;
            }
            public CStream Pop<TKey>(ref SortedDictionary<TKey, string> Data_) where TKey : new() // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                Int32 Cnt = 0;
                Pop(ref Cnt);
                for (Int32 i = 0; i < Cnt; ++i)
                {
                    var Key = new TKey();
                    string Value = "";
                    Pop(ref Key);
                    Pop(ref Value);
                    Data_.Add(Key, Value);
                }

                return this;
            }
            public CStream Pop<TKey, TValue>(ref SortedDictionary<TKey, TValue> Data_) where TKey : new() where TValue : new()
            {
                Int32 Cnt = 0;
                Pop(ref Cnt);
                for (Int32 i = 0; i < Cnt; ++i)
                {
                    var Key = new TKey();
                    var Value = new TValue();
                    Pop(ref Key);
                    Pop(ref Value);
                    Data_.Add(Key, Value);
                }

                return this;
            }
            public CStream Push<TKey>(MultiSet<TKey> Data_)
            {
                Push(Data_.Count);
                foreach (var i in Data_)
                    Push(i);

                return this;
            }
            public CStream Pop(ref MultiSet<string> Data_) // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                Int32 Cnt = 0;
                Pop(ref Cnt);
                for (Int32 i = 0; i < Cnt; ++i)
                {
                    string Key = "";
                    Pop(ref Key);
                    Data_.Add(Key);
                }

                return this;
            }
            public CStream Pop<TKey>(ref MultiSet<TKey> Data_) where TKey : new() // Pop<TKey> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                Int32 Cnt = 0;
                Pop(ref Cnt);
                for (Int32 i = 0; i < Cnt; ++i)
                {
                    var Key = new TKey();
                    Pop(ref Key);
                    Data_.Add(Key);
                }

                return this;
            }
            public CStream Push<TKey, TValue>(MultiMap<TKey, TValue> Data_)
            {
                Push(Data_.Count);
                foreach (var i in Data_)
                    Push(i);

                return this;
            }
            public CStream Pop(ref MultiMap<string, string> Data_) // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                Int32 Cnt = 0;
                Pop(ref Cnt);
                for (Int32 i = 0; i < Cnt; ++i)
                {
                    string Key = "";
                    string Value = "";
                    Pop(ref Key);
                    Pop(ref Value);
                    Data_.Add(Key, Value);
                }

                return this;
            }
            public CStream Pop<TValue>(ref MultiMap<string, TValue> Data_) where TValue : new() // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                Int32 Cnt = 0;
                Pop(ref Cnt);
                for (Int32 i = 0; i < Cnt; ++i)
                {
                    string Key = "";
                    var Value = new TValue();
                    Pop(ref Key);
                    Pop(ref Value);
                    Data_.Add(Key, Value);
                }

                return this;
            }
            public CStream Pop<TKey>(ref MultiMap<TKey, string> Data_) where TKey : new() // Pop<TValue> 에서 where TValue : new() 조건이 필요하고, string 은 new 불가하므로 overloading 필요
            {
                Int32 Cnt = 0;
                Pop(ref Cnt);
                for (Int32 i = 0; i < Cnt; ++i)
                {
                    var Key = new TKey();
                    string Value = "";
                    Pop(ref Key);
                    Pop(ref Value);
                    Data_.Add(Key, Value);
                }

                return this;
            }
            public CStream Pop<TKey, TValue>(ref MultiMap<TKey, TValue> Data_) where TKey : new() where TValue : new()
            {
                Int32 Cnt = 0;
                Pop(ref Cnt);
                for (Int32 i = 0; i < Cnt; ++i)
                {
                    var Key = new TKey();
                    var Value = new TValue();
                    Pop(ref Key);
                    Pop(ref Value);
                    Data_.Add(Key, Value);
                }

                return this;
            }
            ///////////////////////////////////////////////////////////////////////////////////////////
        }
    }
}