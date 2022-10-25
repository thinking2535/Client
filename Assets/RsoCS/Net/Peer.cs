using System;
using System.Net.Sockets;

using rso.Base;
using rso.core;

namespace rso
{
    namespace net
    {
        using crypto;

        using TSize = Int32;
        using TPacketSeq = UInt64;

        public delegate void TEventHandler(object sender, SocketAsyncEventArgs e);

        public class CPeer
        {
            Int32 _HeaderSize = 0;
            Socket _Socket = null;
            CKey _Key;
            CNamePort _NamePort;
            DateTime? _CloseTime = null;
            CPeriod _HBRcvPeriod = null;
            CPeriod _HBSndPeriod = null;
            SHeader _RecvHeader = null;
            Byte[] _RecvBuf;
            public CStream StreamRcv = new CStream();
            CStream _StreamSnd = new CStream();
            CCrypto _Crypto = new CCrypto();
            TPacketSeq _SendPacketSeq = 0;
            TPacketSeq _RecvPacketSeq = 0;
            TPacketSeq _PingPacketSeq = 0;
            DateTime? _PingTime = null;
            ENetRet _WillCloseNetRet = ENetRet.Null;
            TimeSpan _Latency = TimeSpan.Zero;
            public TimeSpan Latency
            {
                get
                {
                    return _Latency;
                }
            }
            public CKey Key
            {
                get
                {
                    return _Key;
                }
            }
            public CNamePort GetNamePort()
            {
                return _NamePort;
            }
            public bool ValidKey(CKey Key_)
            {
                return (_Key != null && _Key.Equals(Key_));
            }
            void _SendBegin(EPacketType PacketType_ = EPacketType.User)  // 외부에서 Stream 에 한번에 담아 보낼 수 있으나, 복사를 한번 더 하게 되기 때문에 SProto 를 여러 파라미터로받아 내부에서 SendBegin, SendEnd 영역을 두어 보내는 방식으로 처리
            {
                new SHeader(0, 0, _SendPacketSeq + 1).Pop(_StreamSnd);
                new SHeader2(PacketType_, _RecvPacketSeq).Pop(_StreamSnd);
            }
            void _SendEnd()
            {
                ++_SendPacketSeq;
                var BodySize = _StreamSnd.Size - _HeaderSize;
                _StreamSnd.SetAt(0, BodySize);

                if (BodySize > 0)
                {
                    var CheckSum = CCore.GetCheckSum(_StreamSnd.Data, _HeaderSize, BodySize);
                    _StreamSnd.SetAt(sizeof(TSize), CheckSum);
                    _Crypto.Encode(_StreamSnd.Data, _HeaderSize, BodySize, (0x1f3a49b72c8d5ef6 ^ (UInt64)BodySize ^ CheckSum ^ _SendPacketSeq));
                }

                _Socket.Send(_StreamSnd.Data, 0, _StreamSnd.Size, SocketFlags.None);
                _StreamSnd.Clear();
            }
            bool _SendPing()
            {
                if (_PingTime != null)
                    return true;

                try
                {
                    _SendBegin(EPacketType.Ping);
                    _PingPacketSeq = _SendPacketSeq + 1;
                    _PingTime = DateTime.Now;
                    _SendEnd();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            bool _SendPong()
            {
                try
                {
                    _SendBegin(EPacketType.Pong);
                    _SendEnd();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            public void Dispose()
            {
                if (_Socket != null)
                    _Socket.Close();
            }
            public CPeer(Int32 HeaderSize_, TimeSpan HBRcvDelay_, TimeSpan HBSndDelay_, Socket Socket_, CKey Key_, CNamePort NamePort_)
            {
                _RecvBuf = new Byte[40960];
                _HeaderSize = HeaderSize_;

                if (HBRcvDelay_.TotalSeconds > 0.0)
                {
                    _HBRcvPeriod = new CPeriod(HBRcvDelay_ + TimeSpan.FromSeconds(3));
                    _HBRcvPeriod.NextLoose();
                }

                if (HBSndDelay_.TotalSeconds > 0.0)
                    _HBSndPeriod = new CPeriod(HBSndDelay_);

                _Socket = Socket_;
                _Key = Key_;
                _NamePort = NamePort_;
            }
            ~CPeer()
            {
                Dispose();
            }
            public ERecvState RecvedBegin()
            {
                // For Header /////////////////////
                if (_RecvHeader == null)
                {
                    if (StreamRcv.Size < _HeaderSize)
                        return ERecvState.NoData;

                    _RecvHeader = new SHeader();
                    _RecvHeader.Push(StreamRcv);
                }

                // For Body /////////////////////
                if (StreamRcv.Size < _RecvHeader.Size)
                    return ERecvState.NoData;

                if (_RecvHeader.SendPacketSeq != (_RecvPacketSeq + 1))
                    throw new Exception("PacketSeq Error " + _RecvHeader.SendPacketSeq.ToString() + ", " + (_RecvPacketSeq + 1).ToString());

                _RecvPacketSeq = _RecvHeader.SendPacketSeq;
                _Crypto.Decode(StreamRcv.Data, StreamRcv.Head, _RecvHeader.Size, (0x1f3a49b72c8d5ef6 ^ (UInt64)_RecvHeader.Size ^ _RecvHeader.CheckSum ^ _RecvHeader.SendPacketSeq));

                if (_RecvHeader.CheckSum != CCore.GetCheckSum(StreamRcv.Data, StreamRcv.Head, _RecvHeader.Size))
                    throw new Exception("CheckSum Error");

                var OldStreamSize = StreamRcv.Size;
                var Header2 = new SHeader2();
                Header2.Push(StreamRcv);
                _RecvHeader.Size -= (OldStreamSize - StreamRcv.Size);
                StreamRcv.SaveState();

                switch (Header2.PacketType)
                {
                    case EPacketType.Ping:
                        if (!_SendPong())
                            throw new Exception("_SendPong Fail");

                        return ERecvState.PingPong;

                    case EPacketType.Pong:
                        if (Header2.RecvPacketSeq == _PingPacketSeq)
                        {
                            _Latency = DateTime.Now - _PingTime.Value;
                            _PingTime = null;
                        }
                        return ERecvState.PingPong;

                    case EPacketType.User:
                        break;

                    default:
                        throw new Exception("Invalid Packet Type");
                }

                return ERecvState.UserData;
            }
            public void RecvedEnd()
            {
                StreamRcv.LoadState();
                StreamRcv.PopSize(_RecvHeader.Size);
                _RecvHeader = null;
            }
            public void WillClose(TimeSpan WaitDuration_, ENetRet NetRet_)
            {
                if (DoesWillClose())
                    return;

                _WillCloseNetRet = NetRet_;
                _CloseTime = (DateTime.Now + WaitDuration_);
            }
            public bool DoesWillClose()
            {
                return (_CloseTime != null);
            }
            public void Send(CStream Stream_) // TODO : 닷넷 버전 제한으로 dynamic 불가하여 이렇게 처리, 유니티 제한 풀리면 코드 수정 요
            {
                if (DoesWillClose())
                    return;

                try
                {
                    _SendBegin();
                    _StreamSnd.Push(Stream_);
                    _SendEnd();
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(SocketException))
                        WillClose(TimeSpan.Zero, ENetRet.SocketError);
                    else
                        WillClose(TimeSpan.Zero, ENetRet.SystemError);
                }
            }
            public void Send(CStream Stream_, Int64 Data_) // TODO : 닷넷 버전 제한으로 dynamic 불가하여 이렇게 처리, 유니티 제한 풀리면 코드 수정 요
            {
                if (DoesWillClose())
                    return;

                try
                {
                    _SendBegin();
                    _StreamSnd.Push(Stream_);
                    _StreamSnd.Push(Data_);
                    _SendEnd();
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(SocketException))
                        WillClose(TimeSpan.Zero, ENetRet.SocketError);
                    else
                        WillClose(TimeSpan.Zero, ENetRet.SystemError);
                }
            }
            public void Send(Int32 ProtoNum_, CStream Stream_) // TODO : 닷넷 버전 제한으로 dynamic 불가하여 이렇게 처리, 유니티 제한 풀리면 코드 수정 요
            {
                if (DoesWillClose())
                    return;

                try
                {
                    _SendBegin();
                    _StreamSnd.Push(ProtoNum_);
                    _StreamSnd.Push(Stream_);
                    _SendEnd();
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(SocketException))
                        WillClose(TimeSpan.Zero, ENetRet.SocketError);
                    else
                        WillClose(TimeSpan.Zero, ENetRet.SystemError);
                }
            }
            public void Send(Int32 ProtoNum_, SProto Proto_) // TODO : 닷넷 버전 제한으로 dynamic 불가하여 이렇게 처리, 유니티 제한 풀리면 코드 수정 요
            {
                if (DoesWillClose())
                    return;

                try
                {
                    _SendBegin();
                    _StreamSnd.Push(ProtoNum_);
                    _StreamSnd.Push(Proto_);
                    _SendEnd();
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(SocketException))
                        WillClose(TimeSpan.Zero, ENetRet.SocketError);
                    else
                        WillClose(TimeSpan.Zero, ENetRet.SystemError);
                }
            }
            public void Send(Int32 ProtoNum_, SProto Proto_, SProto Proto2_) // TODO : 닷넷 버전 제한으로 dynamic 불가하여 이렇게 처리, 유니티 제한 풀리면 코드 수정 요
            {
                if (DoesWillClose())
                    return;

                try
                {
                    _SendBegin();
                    _StreamSnd.Push(ProtoNum_);
                    _StreamSnd.Push(Proto_);
                    _StreamSnd.Push(Proto2_);
                    _SendEnd();
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(SocketException))
                        WillClose(TimeSpan.Zero, ENetRet.SocketError);
                    else
                        WillClose(TimeSpan.Zero, ENetRet.SystemError);
                }
            }
            public void Send(SProto Proto_) // TODO : 닷넷 버전 제한으로 dynamic 불가하여 이렇게 처리, 유니티 제한 풀리면 코드 수정 요
            {
                if (DoesWillClose())
                    return;

                try
                {
                    _SendBegin();
                    _StreamSnd.Push(Proto_);
                    _SendEnd();
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(SocketException))
                        WillClose(TimeSpan.Zero, ENetRet.SocketError);
                    else
                        WillClose(TimeSpan.Zero, ENetRet.SystemError);
                }
            }
            public void Send(SProto Proto_, Int64 Data_) // TODO : 닷넷 버전 제한으로 dynamic 불가하여 이렇게 처리, 유니티 제한 풀리면 코드 수정 요
            {
                if (DoesWillClose())
                    return;

                try
                {
                    _SendBegin();
                    _StreamSnd.Push(Proto_);
                    _StreamSnd.Push(Data_);
                    _SendEnd();
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(SocketException))
                        WillClose(TimeSpan.Zero, ENetRet.SocketError);
                    else
                        WillClose(TimeSpan.Zero, ENetRet.SystemError);
                }
            }
            public void Send(SProto Proto_, SProto Proto2_) // TODO : 닷넷 버전 제한으로 dynamic 불가하여 이렇게 처리, 유니티 제한 풀리면 코드 수정 요
            {
                if (DoesWillClose())
                    return;

                try
                {
                    _SendBegin();
                    _StreamSnd.Push(Proto_);
                    _StreamSnd.Push(Proto2_);
                    _SendEnd();
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(SocketException))
                        WillClose(TimeSpan.Zero, ENetRet.SocketError);
                    else
                        WillClose(TimeSpan.Zero, ENetRet.SystemError);
                }
            }
            public void Send(SProto Proto_, SProto Proto2_, SProto Proto3_) // TODO : 닷넷 버전 제한으로 dynamic 불가하여 이렇게 처리, 유니티 제한 풀리면 코드 수정 요
            {
                if (DoesWillClose())
                    return;

                try
                {
                    _SendBegin();
                    _StreamSnd.Push(Proto_);
                    _StreamSnd.Push(Proto2_);
                    _StreamSnd.Push(Proto3_);
                    _SendEnd();

                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(SocketException))
                        WillClose(TimeSpan.Zero, ENetRet.SocketError);
                    else
                        WillClose(TimeSpan.Zero, ENetRet.SystemError);
                }
            }
            public void Send(SProto Proto_, Int32 Proto2_, SProto Proto3_) // TODO : 닷넷 버전 제한으로 dynamic 불가하여 이렇게 처리, 유니티 제한 풀리면 코드 수정 요
            {
                if (DoesWillClose())
                    return;

                try
                {
                    _SendBegin();
                    _StreamSnd.Push(Proto_);
                    _StreamSnd.Push(Proto2_);
                    _StreamSnd.Push(Proto3_);
                    _SendEnd();
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(SocketException))
                        WillClose(TimeSpan.Zero, ENetRet.SocketError);
                    else
                        WillClose(TimeSpan.Zero, ENetRet.SystemError);
                }
            }
            public void Send(SProto Proto_, SProto Proto2_, Int32 Proto3_, SProto Proto4_) // TODO : 닷넷 버전 제한으로 dynamic 불가하여 이렇게 처리, 유니티 제한 풀리면 코드 수정 요
            {
                if (DoesWillClose())
                    return;

                try
                {
                    _SendBegin();
                    _StreamSnd.Push(Proto_);
                    _StreamSnd.Push(Proto2_);
                    _StreamSnd.Push(Proto3_);
                    _StreamSnd.Push(Proto4_);
                    _SendEnd();
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(SocketException))
                        WillClose(TimeSpan.Zero, ENetRet.SocketError);
                    else
                        WillClose(TimeSpan.Zero, ENetRet.SystemError);
                }
            }
            public void Send(SProto Proto_, CStream Stream_) // TODO : 닷넷 버전 제한으로 dynamic 불가하여 이렇게 처리, 유니티 제한 풀리면 코드 수정 요
            {
                if (DoesWillClose())
                    return;

                try
                {
                    _SendBegin();
                    _StreamSnd.Push(Proto_);
                    _StreamSnd.Push(Stream_);
                    _SendEnd();
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(SocketException))
                        WillClose(TimeSpan.Zero, ENetRet.SocketError);
                    else
                        WillClose(TimeSpan.Zero, ENetRet.SystemError);
                }
            }
            public void Send(bool Data_)
            {
                if (DoesWillClose())
                    return;

                try
                {
                    _SendBegin();
                    _StreamSnd.Push(Data_);
                    _SendEnd();
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(SocketException))
                        WillClose(TimeSpan.Zero, ENetRet.SocketError);
                    else
                        WillClose(TimeSpan.Zero, ENetRet.SystemError);
                }
            }
            public void Send(SByte Data_)
            {
                if (DoesWillClose())
                    return;

                try
                {
                    _SendBegin();
                    _StreamSnd.Push(Data_);
                    _SendEnd();
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(SocketException))
                        WillClose(TimeSpan.Zero, ENetRet.SocketError);
                    else
                        WillClose(TimeSpan.Zero, ENetRet.SystemError);
                }
            }
            public void Send(Byte Data_)
            {
                if (DoesWillClose())
                    return;

                try
                {
                    _SendBegin();
                    _StreamSnd.Push(Data_);
                    _SendEnd();
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(SocketException))
                        WillClose(TimeSpan.Zero, ENetRet.SocketError);
                    else
                        WillClose(TimeSpan.Zero, ENetRet.SystemError);
                }
            }
            public void Send(Int16 Data_)
            {
                if (DoesWillClose())
                    return;

                try
                {
                    _SendBegin();
                    _StreamSnd.Push(Data_);
                    _SendEnd();
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(SocketException))
                        WillClose(TimeSpan.Zero, ENetRet.SocketError);
                    else
                        WillClose(TimeSpan.Zero, ENetRet.SystemError);
                }
            }
            public void Send(UInt16 Data_)
            {
                if (DoesWillClose())
                    return;

                try
                {
                    _SendBegin();
                    _StreamSnd.Push(Data_);
                    _SendEnd();
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(SocketException))
                        WillClose(TimeSpan.Zero, ENetRet.SocketError);
                    else
                        WillClose(TimeSpan.Zero, ENetRet.SystemError);
                }
            }
            public void Send(Int32 Data_)
            {
                if (DoesWillClose())
                    return;

                try
                {
                    _SendBegin();
                    _StreamSnd.Push(Data_);
                    _SendEnd();
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(SocketException))
                        WillClose(TimeSpan.Zero, ENetRet.SocketError);
                    else
                        WillClose(TimeSpan.Zero, ENetRet.SystemError);
                }
            }
            public void Send(UInt32 Data_)
            {
                if (DoesWillClose())
                    return;

                try
                {
                    _SendBegin();
                    _StreamSnd.Push(Data_);
                    _SendEnd();
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(SocketException))
                        WillClose(TimeSpan.Zero, ENetRet.SocketError);
                    else
                        WillClose(TimeSpan.Zero, ENetRet.SystemError);
                }
            }
            public void Send(Int64 Data_)
            {
                if (DoesWillClose())
                    return;

                try
                {
                    _SendBegin();
                    _StreamSnd.Push(Data_);
                    _SendEnd();
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(SocketException))
                        WillClose(TimeSpan.Zero, ENetRet.SocketError);
                    else
                        WillClose(TimeSpan.Zero, ENetRet.SystemError);
                }
            }
            public void Send(UInt64 Data_)
            {
                if (DoesWillClose())
                    return;

                try
                {
                    _SendBegin();
                    _StreamSnd.Push(Data_);
                    _SendEnd();
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(SocketException))
                        WillClose(TimeSpan.Zero, ENetRet.SocketError);
                    else
                        WillClose(TimeSpan.Zero, ENetRet.SystemError);
                }
            }
            public void Send(float Data_)
            {
                if (DoesWillClose())
                    return;

                try
                {
                    _SendBegin();
                    _StreamSnd.Push(Data_);
                    _SendEnd();
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(SocketException))
                        WillClose(TimeSpan.Zero, ENetRet.SocketError);
                    else
                        WillClose(TimeSpan.Zero, ENetRet.SystemError);
                }
            }
            public void Send(double Data_)
            {
                if (DoesWillClose())
                    return;

                try
                {
                    _SendBegin();
                    _StreamSnd.Push(Data_);
                    _SendEnd();
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(SocketException))
                        WillClose(TimeSpan.Zero, ENetRet.SocketError);
                    else
                        WillClose(TimeSpan.Zero, ENetRet.SystemError);
                }
            }
            public void Send(String Data_)
            {
                if (DoesWillClose())
                    return;

                try
                {
                    _SendBegin();
                    _StreamSnd.Push(Data_);
                    _SendEnd();
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(SocketException))
                        WillClose(TimeSpan.Zero, ENetRet.SocketError);
                    else
                        WillClose(TimeSpan.Zero, ENetRet.SystemError);
                }
            }
            public void Send(DateTime Data_)
            {
                if (DoesWillClose())
                    return;

                try
                {
                    _SendBegin();
                    _StreamSnd.Push(Data_);
                    _SendEnd();
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(SocketException))
                        WillClose(TimeSpan.Zero, ENetRet.SocketError);
                    else
                        WillClose(TimeSpan.Zero, ENetRet.SystemError);
                }
            }
            public bool Poll()
            {
                if (!_Socket.Poll(0, SelectMode.SelectRead))
                    return false;

                var BytesReceived = _Socket.Receive(_RecvBuf);
                if (BytesReceived == 0)
                    throw new Exception("SocketError BytesReceived == 0");

                if (_HBRcvPeriod != null)
                    _HBRcvPeriod.NextLoose();

                StreamRcv.Push(_RecvBuf, 0, BytesReceived);

                return true;
            }
            public ENetRet Proc()
            {
                if (_HBRcvPeriod != null)
                {
                    if (_HBRcvPeriod.CheckAndNextLoose())
                        return ENetRet.HeartBeatFail;
                }

                if (_HBSndPeriod != null && _HBSndPeriod.CheckAndNextLoose())
                {
                    if (!_SendPing())
                        return ENetRet.SystemError;
                }

                if (DoesWillClose() && _CloseTime < DateTime.Now)
                    return _WillCloseNetRet;

                return ENetRet.Ok;
            }
        }
    }
}