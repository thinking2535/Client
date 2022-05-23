using System;
using rso.core;
using rso.Base;
using rso.net;

namespace rso
{
    namespace mobile
    {
        using TPeerCnt = UInt32;
        using TProtoSeq = UInt64;
        using _TPeersWillExpire = CList<CClient._SPeerWillExpire>;
        using _TPeersWillExpireIt = CList<CClient._SPeerWillExpire>.SIterator;
        using _TPeersExt = CList<CClient._SPeerExt>;
        using _TPeersExtIt = CList<CClient._SPeerExt>.SIterator;
        using _TPeersNet = CList<CClient._SPeerNet>;
        using _TPeersNetIt = CList<CClient._SPeerNet>.SIterator;

        public class CClient : CNet, INet, IClient
        {
            public class _SPeerWillExpire
            {
                public DateTime ExpireTime;
                public TPeerCnt PeerExtNum;
                public bool NeedToConnect = false; // _LinkFailCallback _UnLink 에서 바로 Connect하지 않고, NeedToConnect 변수를 사용하요 Proc() 에서 Connect하는 이유는
                                                   // _LinkFailCallback 의 경우 메모리 부족등의 이유로 호출된 경우 거기서 재 Connect하면 또다시 _LinkFailCallback 가 호출되어 재귀호출 될 가능성 있음.
                public _SPeerWillExpire(DateTime ExpireTime_, TPeerCnt PeerExtNum_)
                {
                    ExpireTime = ExpireTime_;
                    PeerExtNum = PeerExtNum_;
                }
            }
            public class _SPeerExt : SPeerExt
            {
                public _TPeersWillExpireIt itPeerWillExpire; // end() 이면 서버와 Mobile 연결됨.(IsLinked == true)
                public CNamePort NamePort;
                public CKey ServerExtKey;
                public bool HaveBeenLinked = false; // 한번이라도 IsLinked 가 true 인 적이 있었는가? (한번 IsLinked 가 true가 되면 _SPeerExt가 제거되기 전까지 연결된 상태이므로 외부에서의 Linked)

                public _SPeerExt(CKey Key_, CNamePort NamePort_) :
                    base(Key_)
                {
                    NamePort = NamePort_;
                }
            };
            public class _SPeerNet
            {
                public CKey ExtKey;

                public _SPeerNet(CKey ExtKey_)
                {
                    ExtKey = ExtKey_;
                }
            };

            net.CClient _Net;
            _TPeersNet _PeersNet = new _TPeersNet();
            _TPeersExt _PeersExt = new _TPeersExt();
            _TPeersWillExpire _PeersWillExpire = new _TPeersWillExpire();
            TLinkFailFunc _LinkFailFunc;
            TLinkFunc _LinkFuncSoft;
            TUnLinkFunc _UnLinkFuncSoft;

            void _Close(_TPeersExtIt itPeerExt_, ENetRet NetRet_)
            {
                if (itPeerExt_.Data.itPeerWillExpire)
                {
                    _PeersWillExpire.Remove(itPeerExt_.Data.itPeerWillExpire);
                    itPeerExt_.Data.itPeerWillExpire = _PeersWillExpire.End();
                }

                var ExtKey = itPeerExt_.Data.Key;
                var HaveBeenLinked = itPeerExt_.Data.HaveBeenLinked;
                _PeersExt.Remove(itPeerExt_);

                try
                {
                    if (HaveBeenLinked)
                        _UnLinkFunc(ExtKey, NetRet_);
                    else
                        _LinkFailFunc(ExtKey.PeerNum, NetRet_);
                }
                catch
                {
                }
            }
            void _Close(TPeerCnt PeerNum_, ENetRet NetRet_)
            {
                _Close(_PeersExt.Get((Int32)PeerNum_), NetRet_);
            }
            void _Close(_TPeersExtIt itPeerExt_)
            {
                if (itPeerExt_.Data.NetKey)
                {
                    var itPeerNet = _PeersNet.Get((Int32)itPeerExt_.Data.NetKey.PeerNum);
                    if (itPeerNet)
                    {
                        itPeerNet.Data.ExtKey = null;

                        if (_Net.IsLinked(itPeerExt_.Data.NetKey.PeerNum))
                            _SendUnLink(itPeerExt_.Data.NetKey.PeerNum);
                        else
                            _Net.Close(itPeerExt_.Data.NetKey.PeerNum);
                    }
                }
                if (itPeerExt_.Data.itPeerWillExpire)
                {
                    _PeersWillExpire.Remove(itPeerExt_.Data.itPeerWillExpire);
                    itPeerExt_.Data.itPeerWillExpire = _PeersWillExpire.End();
                }

                _Close(itPeerExt_, ENetRet.UserClose);
            }
            void _Link(CKey Key_)
            {
                var itPeerNet = _PeersNet.Get((Int32)Key_.PeerNum); // _Net.Connect() 호출정상이면 무조건 _PeersNet 에 추가된 상태이므로 itPeerNet 체크 불필요

                if (!itPeerNet.Data.ExtKey)
                    return;

                var itPeerExt = _PeersExt.Get((Int32)itPeerNet.Data.ExtKey.PeerNum);
                if (!itPeerExt)
                    return;

                if (itPeerExt.Data.HaveBeenLinked)
                    _Net.Send(Key_.PeerNum, new SHeaderCs(EProtoCs.ReLink), new SReLinkCs(itPeerExt.Data.ServerExtKey, itPeerExt.Data.ProtoSeqMustRecv));
                else
                    _Net.Send(Key_.PeerNum, new SHeaderCs(EProtoCs.Link), new SLinkCs());
            }
            void _LinkFail(TPeerCnt PeerNum_, ENetRet NetRet_)
            {
                var itPeerNet = _PeersNet.Get((Int32)PeerNum_);
                if (!itPeerNet)
                    return;

                var ExtKey = itPeerNet.Data.ExtKey;
                _PeersNet.Remove(itPeerNet);

                if (ExtKey)
                {
                    var itPeerExt = _PeersExt.Get((Int32)ExtKey.PeerNum);
                    if (itPeerExt)
                    {
                        itPeerExt.Data.NetKey = null;
                        itPeerExt.Data.itPeerWillExpire.Data.NeedToConnect = true;
                    }
                }
            }
            void _UnLink(CKey Key_, ENetRet NetRet_)
            {
                var itPeerNet = _PeersNet.Get((Int32)Key_.PeerNum);
                if (!itPeerNet)
                    return;

                var ExtKey = itPeerNet.Data.ExtKey;
                _PeersNet.Remove(itPeerNet);

                if (ExtKey)
                {
                    var itPeerExt = _PeersExt.Get((Int32)ExtKey.PeerNum);
                    if (itPeerExt)
                    {
                        if (NetRet_ == ENetRet.CertifyFail ||
                            NetRet_ == ENetRet.InvalidPacket ||
                            NetRet_ == ENetRet.SystemError ||
                            NetRet_ == ENetRet.UserClose ||
                            NetRet_ == ENetRet.KeepConnectTimeOut)
                        {
                            _Close(itPeerExt, NetRet_);
                        }
                        else
                        {
                            try
                            {
                                if (!itPeerExt.Data.itPeerWillExpire)
                                {
                                    itPeerExt.Data.itPeerWillExpire = _PeersWillExpire.Add(new _SPeerWillExpire(DateTime.Now + _KeepConnectDuration, itPeerExt.Data.Key.PeerNum));
                                    _UnLinkFuncSoft(ExtKey, NetRet_);
                                }

                                itPeerExt.Data.itPeerWillExpire.Data.NeedToConnect = true;
                                itPeerExt.Data.NetKey = null;
                            }
                            catch
                            {
                                _Close(itPeerExt, NetRet_);
                            }
                        }
                    }
                }
            }
            void _Recv(CKey Key_, CStream Stream_)
            {
                var Proto = new SHeaderSc();
                Stream_.Pop(ref Proto);

                var itPeerNet = _PeersNet.Get((Int32)Key_.PeerNum);

                if (!itPeerNet.Data.ExtKey)
                    return;

                var itPeerExt = _PeersExt.Get((Int32)itPeerNet.Data.ExtKey.PeerNum);
                if (!itPeerExt)
                    return;

                if (itPeerExt.Data.itPeerWillExpire) // Not Linked Peer
                {
                    switch (Proto.Proto)
                    {
                        case EProtoSc.Link:
                            _RecvLink(itPeerNet, itPeerExt, Stream_);
                            break;

                        case EProtoSc.ReLink:
                            _RecvReLink(Key_, itPeerNet, itPeerExt, Stream_);
                            break;

                        case EProtoSc.UnLink:
                            _RecvUnLink(Key_, Stream_);
                            break;

                        default:
                            _Net.Close(Key_.PeerNum);
                            break;
                    }
                }
                else // Linked Peer
                {
                    switch (Proto.Proto)
                    {
                        case EProtoSc.UnLink:
                            _RecvUnLink(Key_, Stream_);
                            break;

                        case EProtoSc.Ack:
                            _RecvAck(itPeerExt);
                            break;

                        case EProtoSc.ReSend:
                            Stream_.PopSize(sizeof(Int32));
                            goto case EProtoSc.UserProto;
                        case EProtoSc.UserProto:
                            _RecvUserProto(itPeerExt, Stream_);
                            break;

                        default:
                            _Close(itPeerExt, ENetRet.InvalidPacket); // 인증된 클라이언트가 잘못된 프로토콜을 보내면 접속해제 절차 없이 삭제
                            _Net.Close(Key_.PeerNum);
                            break;
                    }
                }
            }
            void _RecvAck(_TPeersExtIt itPeerExt_)
            {
                itPeerExt_.Data.RecvAck();
            }
            void _RecvLink(_TPeersNetIt itPeerNet_, _TPeersExtIt itPeerExt_, CStream Stream_)
            {
                var Proto = new SLinkSc();
                Stream_.Pop(ref Proto);

                itPeerExt_.Data.ServerExtKey = new CKey(Proto.ServerExtKey);
                _PeersWillExpire.Remove(itPeerExt_.Data.itPeerWillExpire);
                itPeerExt_.Data.itPeerWillExpire = _PeersWillExpire.End();

                if (!itPeerExt_.Data.HaveBeenLinked)
                {
                    itPeerExt_.Data.HaveBeenLinked = true;
                    _LinkFunc(itPeerExt_.Data.Key);
                }
            }
            void _RecvReLink(CKey NetKey_, _TPeersNetIt itPeerNet_, _TPeersExtIt itPeerExt_, CStream Stream_)
            {
                var Proto = new SReLinkSc();
                Stream_.Pop(ref Proto);

                _PeersWillExpire.Remove(itPeerExt_.Data.itPeerWillExpire);
                itPeerExt_.Data.itPeerWillExpire = _PeersWillExpire.End();

                TProtoSeq MustDeleteCnt = (Proto.ServerProtoSeqMustRecv - itPeerExt_.Data.ProtoSeqFirstForSendProtos); // 0 이상의 값이 나와야 함.(조작에의해 음수(큰 양수)가 나오더라도 상관없음
                for (var it = itPeerExt_.Data.SendProtos.Begin(); it;)
                {
                    if (MustDeleteCnt == 0)
                        break;

                    var itCheck = it;
                    it.MoveNext();

                    itCheck.Data.Clear();
                    itPeerExt_.Data.SendProtos.Remove(itCheck);
                    --MustDeleteCnt;
                }
                itPeerExt_.Data.ProtoSeqFirstForSendProtos = Proto.ServerProtoSeqMustRecv;

                foreach (var i in itPeerExt_.Data.SendProtos)
                    _Net.Send(NetKey_, new SHeaderCs(EProtoCs.ReSend), i);

                _LinkFuncSoft(itPeerExt_.Data.Key);
            }
            void _RecvUnLink(CKey NetKey_, CStream Stream_)
            {
                var Proto = new SUnLinkSc();
                Stream_.Pop(ref Proto);

                _Net.Close(NetKey_.PeerNum);
            }
            void _RecvUserProto(_TPeersExtIt itPeerExt_, CStream Stream_)
            {
                ++itPeerExt_.Data.ProtoSeqMustRecv;

                var ExtKey = itPeerExt_.Data.Key;
                _RecvFunc(ExtKey, Stream_);

                var itPeerExt = _PeersExt.Get((Int32)ExtKey.PeerNum); // _RecvFunc 내부에서 throw or Close 할 수 있으므로 재확인
                if (itPeerExt && itPeerExt.Data.NetKey)
                    _Net.Send(itPeerExt.Data.NetKey, new SHeaderCs(EProtoCs.Ack));
            }
            void _SendUnLink(TPeerCnt NetPeerNum_)
            {
                _Net.Send(NetPeerNum_, new SHeaderCs(EProtoCs.UnLink), new SUnLinkCs());
                _Net.WillClose(NetPeerNum_, TimeSpan.FromMilliseconds(500));
            }

            public CClient(
                TLinkFunc LinkFunc_, TLinkFailFunc LinkFailFunc_, TUnLinkFunc UnLinkFunc_, TRecvFunc RecvFunc_,
                bool NoDelay_, Int32 RecvBuffSize_, Int32 SendBuffSize_,
                TimeSpan HBRcvDelay_, TimeSpan HBSndDelay_, Int32 ConnectTimeOutSec_,
                TLinkFunc LinkFuncSoft_, TUnLinkFunc UnLinkFuncSoft_, TimeSpan KeepConnectDuration_) :
                base(KeepConnectDuration_, LinkFunc_, UnLinkFunc_, RecvFunc_)
            {
                _LinkFailFunc = LinkFailFunc_;
                _LinkFuncSoft = LinkFuncSoft_;
                _UnLinkFuncSoft = UnLinkFuncSoft_;

                _Net = new net.CClient(
                    _Link, _LinkFail, _UnLink, _Recv,
                    NoDelay_, RecvBuffSize_, SendBuffSize_,
                    HBRcvDelay_, HBSndDelay_, ConnectTimeOutSec_);
            }
            public bool IsLinked(TPeerCnt PeerNum_)
            {
                var Peer = _PeersExt.Get((Int32)PeerNum_);
                if (!Peer)
                    return false;

                return Peer.Data.HaveBeenLinked;
            }
            public CNamePort GetNamePort(TPeerCnt PeerNum_)
            {
                return _PeersExt[(Int32)PeerNum_].NamePort;
            }
            public void Close(TPeerCnt PeerNum_)
            {
                _Close(_PeersExt.Get((Int32)PeerNum_));
            }
            public bool Close(CKey Key_)
            {
                var itPeerExt = _PeersExt.Get((Int32)Key_.PeerNum);
                if (!itPeerExt)
                    return false;

                if (!itPeerExt.Data.Key.Equals(Key_))
                    return false;

                _Close(itPeerExt);

                return true;
            }
            public void CloseAll()
            {
                for (var it = _PeersExt.Begin(); it;)
                {
                    var itCheck = it;
                    it.MoveNext();

                    _Close(itCheck);
                }
            }
            public void WillClose(TPeerCnt PeerNum_, TimeSpan WaitDuration_)
            {
                var itPeerExt = _PeersExt.Get((Int32)PeerNum_);

                if (itPeerExt.Data.DoesWillClose())
                    return;

                itPeerExt.Data.WillClose(WaitDuration_);
            }
            public bool WillClose(CKey Key_, TimeSpan WaitDuration_)
            {
                var itPeerExt = _PeersExt.Get((Int32)Key_.PeerNum);
                if (!itPeerExt)
                    return false;

                if (!itPeerExt.Data.Key.Equals(Key_))
                    return false;

                if (itPeerExt.Data.DoesWillClose())
                    return false;

                itPeerExt.Data.WillClose(WaitDuration_);

                return true;
            }
            public TPeerCnt GetPeerCnt()
            {
                return (TPeerCnt)_PeersExt.Count;
            }
            public TimeSpan Latency(TPeerCnt PeerNum_)
            {
                return _Net.Latency(_PeersExt[(Int32)PeerNum_].NetKey.PeerNum);
            }
            public void Proc()
            {
                _Net.Proc();

                if (_PeriodUnLinked.CheckAndNextLoose())
                {
                    var Now = DateTime.Now;

                    for (var it = _PeersExt.Begin(); it;)
                    {
                        var itCheck = it;
                        it.MoveNext();

                        if (itCheck.Data.DoesWillClose() && itCheck.Data.CloseTime < Now)
                            _Close(itCheck);
                    }

                    for (var it = _PeersWillExpire.Begin(); it;)
                    {
                        var itCheck = it;
                        it.MoveNext();

                        if (itCheck.Data.ExpireTime > Now)
                            break;

                        _Close(itCheck.Data.PeerExtNum, ENetRet.KeepConnectTimeOut);
                        _PeersWillExpire.Remove(itCheck);
                    }

                    foreach (var it in _PeersWillExpire)
                    {
                        if (!it.NeedToConnect)
                            continue;

                        var itPeerExt = _PeersExt.Get((Int32)it.PeerExtNum);
                        if (!itPeerExt)
                            continue;

                        _Connect(itPeerExt);
                    }
                }
            }
            public bool IsConnecting(TPeerCnt PeerNum_)
            {
                return _PeersExt.Get((Int32)PeerNum_);
            }
            bool _Connect(_TPeersExtIt itPeerExt_)
            {
                try
                {
                    var itPeerNet = _PeersNet.Add(new _SPeerNet(itPeerExt_.Data.Key));
                    
                    try
                    {
                        var NetKey = _Net.Connect(itPeerExt_.Data.NamePort, (TPeerCnt)itPeerNet.Index);
                        if (NetKey == null)
                            throw new Exception();

                        itPeerExt_.Data.NetKey = NetKey;
                        itPeerExt_.Data.itPeerWillExpire.Data.NeedToConnect = false;
                        return true;
                    }
                    catch
                    {
                        _PeersNet.Remove(itPeerNet);
                        throw;
                    }
                }
                catch
                {
                    return false;
                }
            }
            public CKey Connect(CNamePort NamePort_, TPeerCnt PeerNum_)
            {
                try
                {
                    var itPeerExt = _PeersExt.AddAt((Int32)PeerNum_, new _SPeerExt(new CKey(PeerNum_, _PeerCounter), NamePort_));

                    try
                    {
                        itPeerExt.Data.itPeerWillExpire = _PeersWillExpire.Add(new _SPeerWillExpire(DateTime.Now + _KeepConnectDuration, itPeerExt.Data.Key.PeerNum));

                        if (!_Connect(itPeerExt))
                        {
                            _PeersWillExpire.Remove(itPeerExt.Data.itPeerWillExpire);
                            throw new Exception();
                        }

                        ++_PeerCounter;

                        return itPeerExt.Data.Key;
                    }
                    catch
                    {
                        _PeersExt.Remove(itPeerExt);
                        throw;
                    }
                }
                catch
                {
                    return null;
                }
            }
            public CKey Connect(CNamePort NamePort_)
            {
                return Connect(NamePort_, (TPeerCnt)_PeersExt.NewIndex);
            }
            public void Send(TPeerCnt PeerNum_, CStream Stream_) // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeerExt = _PeersExt.Get((Int32)PeerNum_);

                if (itPeerExt.Data.DoesWillClose())
                    return;

                try
                {
                    itPeerExt.Data.Send(Stream_);
                }
                catch
                {
                    itPeerExt.Data.WillClose(TimeSpan.Zero);
                    return;
                }

                if (!itPeerExt.Data.itPeerWillExpire)
                    _Net.Send(itPeerExt.Data.NetKey, new SHeaderCs(EProtoCs.UserProto), Stream_);
            }
            public void Send(CKey Key_, CStream Stream_) // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeerExt = _PeersExt.Get((Int32)Key_.PeerNum);
                if (!itPeerExt)
                    return;

                if (!itPeerExt.Data.Key.Equals(Key_))
                    return;

                if (itPeerExt.Data.DoesWillClose())
                    return;

                try
                {
                    itPeerExt.Data.Send(Stream_);
                }
                catch
                {
                    itPeerExt.Data.WillClose(TimeSpan.Zero);
                    return;
                }

                if (!itPeerExt.Data.itPeerWillExpire)
                    _Net.Send(itPeerExt.Data.NetKey, new SHeaderCs(EProtoCs.UserProto), Stream_);
            }
            public void Send(TPeerCnt PeerNum_, SProto Proto_, SProto Proto2_) // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeerExt = _PeersExt.Get((Int32)PeerNum_);

                if (itPeerExt.Data.DoesWillClose())
                    return;

                try
                {
                    itPeerExt.Data.Send(Proto_, Proto2_);
                }
                catch
                {
                    itPeerExt.Data.WillClose(TimeSpan.Zero);
                    return;
                }

                if (!itPeerExt.Data.itPeerWillExpire)
                    _Net.Send(itPeerExt.Data.NetKey, new SHeaderCs(EProtoCs.UserProto), Proto_, Proto2_);
            }
            public void Send(CKey Key_, SProto Proto_, SProto Proto2_) // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeerExt = _PeersExt.Get((Int32)Key_.PeerNum);
                if (!itPeerExt)
                    return;

                if (!itPeerExt.Data.Key.Equals(Key_))
                    return;

                if (itPeerExt.Data.DoesWillClose())
                    return;

                try
                {
                    itPeerExt.Data.Send(Proto_, Proto2_);
                }
                catch
                {
                    itPeerExt.Data.WillClose(TimeSpan.Zero);
                    return;
                }

                if (!itPeerExt.Data.itPeerWillExpire)
                    _Net.Send(itPeerExt.Data.NetKey, new SHeaderCs(EProtoCs.UserProto), Proto_, Proto2_);
            }
            public void Send(TPeerCnt PeerNum_, SProto Proto_, Int32 ProtoNum_, SProto Proto2_) // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeerExt = _PeersExt.Get((Int32)PeerNum_);

                if (itPeerExt.Data.DoesWillClose())
                    return;

                try
                {
                    itPeerExt.Data.Send(Proto_, ProtoNum_, Proto2_);
                }
                catch
                {
                    itPeerExt.Data.WillClose(TimeSpan.Zero);
                    return;
                }

                if (!itPeerExt.Data.itPeerWillExpire)
                    _Net.Send(itPeerExt.Data.NetKey, new SHeaderCs(EProtoCs.UserProto), Proto_, ProtoNum_, Proto2_);
            }
            public void Send(CKey Key_, SProto Proto_, Int32 ProtoNum_, SProto Proto2_) // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeerExt = _PeersExt.Get((Int32)Key_.PeerNum);
                if (!itPeerExt)
                    return;

                if (!itPeerExt.Data.Key.Equals(Key_))
                    return;

                if (itPeerExt.Data.DoesWillClose())
                    return;

                try
                {
                    itPeerExt.Data.Send(Proto_, ProtoNum_, Proto2_);
                }
                catch
                {
                    itPeerExt.Data.WillClose(TimeSpan.Zero);
                    return;
                }

                if (!itPeerExt.Data.itPeerWillExpire)
                    _Net.Send(itPeerExt.Data.NetKey, new SHeaderCs(EProtoCs.UserProto), Proto_, ProtoNum_, Proto2_);
            }
            public void Send(TPeerCnt PeerNum_, Int64 Data_) // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeerExt = _PeersExt.Get((Int32)PeerNum_);

                if (itPeerExt.Data.DoesWillClose())
                    return;

                try
                {
                    itPeerExt.Data.Send(Data_);
                }
                catch
                {
                    itPeerExt.Data.WillClose(TimeSpan.Zero);
                    return;
                }

                if (!itPeerExt.Data.itPeerWillExpire)
                    _Net.Send(itPeerExt.Data.NetKey, new SHeaderCs(EProtoCs.UserProto), Data_);
            }
            public void Send(CKey Key_, Int64 Data_) // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeerExt = _PeersExt.Get((Int32)Key_.PeerNum);
                if (!itPeerExt)
                    return;

                if (!itPeerExt.Data.Key.Equals(Key_))
                    return;

                if (itPeerExt.Data.DoesWillClose())
                    return;

                try
                {
                    itPeerExt.Data.Send(Data_);
                }
                catch
                {
                    itPeerExt.Data.WillClose(TimeSpan.Zero);
                    return;
                }

                if (!itPeerExt.Data.itPeerWillExpire)
                    _Net.Send(itPeerExt.Data.NetKey, new SHeaderCs(EProtoCs.UserProto), Data_);
            }
            public void SendAll(SProto Proto_, Int32 ProtoNum_, SProto Proto2_) // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                foreach (var i in _PeersExt)
                {
                    if (i.DoesWillClose())
                        continue;

                    try
                    {
                        i.Send(Proto_, ProtoNum_, Proto2_);
                    }
                    catch
                    {
                        i.WillClose(TimeSpan.Zero);
                        continue;
                    }

                    if (!i.itPeerWillExpire)
                        _Net.Send(i.NetKey, new SHeaderCs(EProtoCs.UserProto), Proto_, ProtoNum_, Proto2_);
                }
            }
            public void SendAll(CStream Stream_) // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                foreach (var i in _PeersExt)
                {
                    if (i.DoesWillClose())
                        continue;

                    try
                    {
                        i.Send(Stream_);
                    }
                    catch
                    {
                        i.WillClose(TimeSpan.Zero);
                        continue;
                    }

                    if (!i.itPeerWillExpire)
                        _Net.Send(i.NetKey, new SHeaderCs(EProtoCs.UserProto), Stream_);
                }
            }
            public void SendAllExcept(CKey Key_, CStream Stream_)
            {
                foreach (var i in _PeersExt)
                {
                    if (i.DoesWillClose())
                        continue;

                    if (i.Key.Equals(Key_))
                        continue;

                    try
                    {
                        i.Send(Stream_);
                    }
                    catch
                    {
                        i.WillClose(TimeSpan.Zero);
                        continue;
                    }

                    if (!i.itPeerWillExpire)
                        _Net.Send(i.NetKey, new SHeaderCs(EProtoCs.UserProto), Stream_);
                }
            }
            public void Dispose()
            {
                _Net.Dispose();
            }
        }
    }
}