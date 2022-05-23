using System;
using System.Net;
using System.Net.Sockets;
using rso.core;
using rso.Base;

namespace rso
{
    namespace net
    {
        using TPeerCnt = UInt32;
        using TPeers = CList<CPeer>;
        using TIterator = CList<CPeer>.SIterator;

        public delegate void TLinkFunc(CKey Key_);
        public delegate void TLinkFailFunc(TPeerCnt PeerNum_, ENetRet NetRet_);
        public delegate void TUnLinkFunc(CKey Key_, ENetRet NetRet_);
        public delegate void TRecvFunc(CKey Key_, CStream Stream_);

        public abstract class CNet : INet
        {
            Int32 _HeaderSize = 0;
            protected TLinkFunc _LinkFunc;
            protected TUnLinkFunc _UnLinkFunc;
            protected TRecvFunc _RecvFunc;
            TimeSpan _HBRcvDelay;
            TimeSpan _HBSndDelay;
            protected TPeerCnt _Counter = 0;
            protected TPeers _Peers = new TPeers();
            protected bool _NoDelay = false;

            protected CKey _NewKey(TPeerCnt PeerNum_)
            {
                return new CKey(PeerNum_, _Counter++);
            }
            protected void _Link(CKey Key_, Socket Socket_, SocketAsyncEventArgs Event_)
            {
                Event_.UserToken = Key_;
                var NamePort = new CNamePort((IPEndPoint)Event_.RemoteEndPoint);
                _Peers.AddAt((Int32)Key_.PeerNum, new CPeer(_HeaderSize, _HBRcvDelay, _HBSndDelay, Socket_, Key_, NamePort));

                try
                {
                    _LinkFunc(Key_);
                }
                catch (Exception Exception_)
                {
                    throw new ExceptionExtern(Exception_.Message);
                }
            }
            void _Link(Socket Socket_, SocketAsyncEventArgs Event_)
            {
                _Link(_NewKey((TPeerCnt)_Peers.NewIndex), Socket_, Event_);
            }
            protected void _Close(TIterator itPeer_, ENetRet NetRet_)
            {
                var Key = itPeer_.Data.Key;

                itPeer_.Data.Dispose();
                _Peers.Remove(itPeer_);
                _UnLinkFunc(Key, NetRet_);
            }
            public void Dispose()
            {
                CloseAll();

                foreach (var Peer in _Peers)
                    Peer.Dispose();
            }
            public CNet(
                TLinkFunc LinkFunc_, TUnLinkFunc UnLinkFunc_, TRecvFunc RecvFunc_,
                bool NoDelay_,
                TimeSpan HBRcvDelay_, TimeSpan HBSndDelay_)
            {
                var StreamForHeaderSizeChecking = new CStream();
                new SHeader().Pop(StreamForHeaderSizeChecking);

                _HeaderSize = StreamForHeaderSizeChecking.Size;
                _HBRcvDelay = HBRcvDelay_;
                _HBSndDelay = HBSndDelay_;
                _LinkFunc = LinkFunc_;
                _UnLinkFunc = UnLinkFunc_;
                _RecvFunc = RecvFunc_;
                _NoDelay = NoDelay_;
            }
            ~CNet()
            {
                Dispose();
            }
            // 아래 같은 방법밖에 없나? 제네릭을 쓸 수는 있나?
            public bool IsLinked(TPeerCnt PeerNum_)
            {
                return _Peers.Get((Int32)PeerNum_);
            }
            public CNamePort GetNamePort(TPeerCnt PeerNum_)
            {
                return _Peers[(Int32)PeerNum_].GetNamePort();
            }
            public void Close(TPeerCnt PeerNum_)
            {
                _Close(_Peers.Get((Int32)PeerNum_), ENetRet.UserClose);
            }
            public bool Close(CKey Key_)
            {
                var itPeer = _Peers.Get((Int32)Key_.PeerNum);
                if (!itPeer)
                    return false;

                if (itPeer.Data.Key.PeerCounter != Key_.PeerCounter)
                    return false;

                _Close(itPeer, ENetRet.UserClose);

                return true;
            }
            public void CloseAll()
            {
                for (var itPeer = _Peers.Begin(); itPeer; itPeer = _Peers.Begin())
                    _Close(itPeer, ENetRet.UserClose);
            }
            public void WillClose(TPeerCnt PeerNum_, TimeSpan WaitDuration_)
            {
                _Peers[(Int32)PeerNum_].WillClose(WaitDuration_, ENetRet.UserClose);
            }
            public bool WillClose(CKey Key_, TimeSpan WaitDuration_)
            {
                var itPeer = _Peers.Get((Int32)Key_.PeerNum);
                if (!itPeer)
                    return false;

                if (itPeer.Data.Key.PeerCounter != Key_.PeerCounter)
                    return false;

                itPeer.Data.WillClose(WaitDuration_, ENetRet.UserClose);

                return true;
            }
            public TPeerCnt GetPeerCnt()
            {
                return (TPeerCnt)_Peers.Count;
            }
            public void Send(TPeerCnt PeerNum_, CStream Stream_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                _Peers[(Int32)PeerNum_].Send(Stream_);
            }
            public void Send(TPeerCnt PeerNum_, CStream Stream_, Int64 Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                _Peers[(Int32)PeerNum_].Send(Stream_, Data_);
            }
            public void Send(TPeerCnt PeerNum_, Int32 ProtoNum_, CStream Stream_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                _Peers[(Int32)PeerNum_].Send(ProtoNum_, Stream_);
            }
            public void Send(TPeerCnt PeerNum_, Int32 ProtoNum_, SProto Proto_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                _Peers[(Int32)PeerNum_].Send(ProtoNum_, Proto_);
            }
            public void Send(TPeerCnt PeerNum_, Int32 ProtoNum_, SProto Proto_, SProto Proto2_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                _Peers[(Int32)PeerNum_].Send(ProtoNum_, Proto_, Proto2_);
            }
            public void Send(TPeerCnt PeerNum_, SProto Proto_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                _Peers[(Int32)PeerNum_].Send(Proto_);
            }
            public void Send(TPeerCnt PeerNum_, SProto Proto_, Int64 Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                _Peers[(Int32)PeerNum_].Send(Proto_, Data_);
            }
            public void Send(TPeerCnt PeerNum_, SProto Proto_, SProto Proto2_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                _Peers[(Int32)PeerNum_].Send(Proto_, Proto2_);
            }
            public void Send(TPeerCnt PeerNum_, SProto Proto_, SProto Proto2_, SProto Proto3_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                _Peers[(Int32)PeerNum_].Send(Proto_, Proto2_, Proto3_);
            }
            public void Send(TPeerCnt PeerNum_, SProto Proto_, Int32 Proto2_, SProto Proto3_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                _Peers[(Int32)PeerNum_].Send(Proto_, Proto2_, Proto3_);
            }
            public void Send(TPeerCnt PeerNum_, SProto Proto_, CStream Stream_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                _Peers[(Int32)PeerNum_].Send(Proto_, Stream_);
            }
            public void Send(TPeerCnt PeerNum_, SProto Proto_, SProto Proto2_, Int32 Proto3_, SProto Proto4_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                _Peers[(Int32)PeerNum_].Send(Proto_, Proto2_, Proto3_, Proto4_);
            }
            public void Send(TPeerCnt PeerNum_, bool Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                _Peers[(Int32)PeerNum_].Send(Data_);
            }
            public void Send(TPeerCnt PeerNum_, SByte Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                _Peers[(Int32)PeerNum_].Send(Data_);
            }
            public void Send(TPeerCnt PeerNum_, Byte Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                _Peers[(Int32)PeerNum_].Send(Data_);
            }
            public void Send(TPeerCnt PeerNum_, Int16 Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                _Peers[(Int32)PeerNum_].Send(Data_);
            }
            public void Send(TPeerCnt PeerNum_, UInt16 Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                _Peers[(Int32)PeerNum_].Send(Data_);
            }
            public void Send(TPeerCnt PeerNum_, Int32 Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                _Peers[(Int32)PeerNum_].Send(Data_);
            }
            public void Send(TPeerCnt PeerNum_, UInt32 Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                _Peers[(Int32)PeerNum_].Send(Data_);
            }
            public void Send(TPeerCnt PeerNum_, Int64 Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                _Peers[(Int32)PeerNum_].Send(Data_);
            }
            public void Send(TPeerCnt PeerNum_, UInt64 Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                _Peers[(Int32)PeerNum_].Send(Data_);
            }
            public void Send(TPeerCnt PeerNum_, float Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                _Peers[(Int32)PeerNum_].Send(Data_);
            }
            public void Send(TPeerCnt PeerNum_, double Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                _Peers[(Int32)PeerNum_].Send(Data_);
            }
            public void Send(TPeerCnt PeerNum_, String Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                _Peers[(Int32)PeerNum_].Send(Data_);
            }
            public void Send(TPeerCnt PeerNum_, DateTime Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                _Peers[(Int32)PeerNum_].Send(Data_);
            }

            public void Send(CKey Key_, CStream Stream_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeer = _Peers.Get((Int32)Key_.PeerNum);
                if (!itPeer)
                    return;

                if (itPeer.Data.Key.PeerCounter != Key_.PeerCounter)
                    return;

                itPeer.Data.Send(Stream_);
            }
            public void Send(CKey Key_, CStream Stream_, Int64 Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeer = _Peers.Get((Int32)Key_.PeerNum);
                if (!itPeer)
                    return;

                if (itPeer.Data.Key.PeerCounter != Key_.PeerCounter)
                    return;

                itPeer.Data.Send(Stream_, Data_);
            }
            public void Send(CKey Key_, Int32 ProtoNum_, CStream Stream_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeer = _Peers.Get((Int32)Key_.PeerNum);
                if (!itPeer)
                    return;

                if (itPeer.Data.Key.PeerCounter != Key_.PeerCounter)
                    return;

                itPeer.Data.Send(ProtoNum_, Stream_);
            }
            public void Send(CKey Key_, Int32 ProtoNum_, SProto Proto_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeer = _Peers.Get((Int32)Key_.PeerNum);
                if (!itPeer)
                    return;

                if (itPeer.Data.Key.PeerCounter != Key_.PeerCounter)
                    return;

                itPeer.Data.Send(ProtoNum_, Proto_);
            }
            public void Send(CKey Key_, Int32 ProtoNum_, SProto Proto_, SProto Proto2_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeer = _Peers.Get((Int32)Key_.PeerNum);
                if (!itPeer)
                    return;

                if (itPeer.Data.Key.PeerCounter != Key_.PeerCounter)
                    return;

                itPeer.Data.Send(ProtoNum_, Proto_, Proto2_);
            }
            public void Send(CKey Key_, SProto Proto_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeer = _Peers.Get((Int32)Key_.PeerNum);
                if (!itPeer)
                    return;

                if (itPeer.Data.Key.PeerCounter != Key_.PeerCounter)
                    return;

                itPeer.Data.Send(Proto_);
            }
            public void Send(CKey Key_, SProto Proto_, Int64 Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeer = _Peers.Get((Int32)Key_.PeerNum);
                if (!itPeer)
                    return;

                if (itPeer.Data.Key.PeerCounter != Key_.PeerCounter)
                    return;

                itPeer.Data.Send(Proto_, Data_);
            }
            public void Send(CKey Key_, SProto Proto_, SProto Proto2_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeer = _Peers.Get((Int32)Key_.PeerNum);
                if (!itPeer)
                    return;

                if (itPeer.Data.Key.PeerCounter != Key_.PeerCounter)
                    return;

                itPeer.Data.Send(Proto_, Proto2_);
            }
            public void Send(CKey Key_, SProto Proto_, SProto Proto2_, SProto Proto3_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeer = _Peers.Get((Int32)Key_.PeerNum);
                if (!itPeer)
                    return;

                if (itPeer.Data.Key.PeerCounter != Key_.PeerCounter)
                    return;

                itPeer.Data.Send(Proto_, Proto2_, Proto3_);
            }
            public void Send(CKey Key_, SProto Proto_, Int32 Proto2_, SProto Proto3_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeer = _Peers.Get((Int32)Key_.PeerNum);
                if (!itPeer)
                    return;

                if (itPeer.Data.Key.PeerCounter != Key_.PeerCounter)
                    return;

                itPeer.Data.Send(Proto_, Proto2_, Proto3_);
            }
            public void Send(CKey Key_, SProto Proto_, CStream Stream_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeer = _Peers.Get((Int32)Key_.PeerNum);
                if (!itPeer)
                    return;

                if (itPeer.Data.Key.PeerCounter != Key_.PeerCounter)
                    return;

                itPeer.Data.Send(Proto_, Stream_);
            }
            public void Send(CKey Key_, SProto Proto_, SProto Proto2_, Int32 Proto3_, SProto Proto4_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeer = _Peers.Get((Int32)Key_.PeerNum);
                if (!itPeer)
                    return;

                if (itPeer.Data.Key.PeerCounter != Key_.PeerCounter)
                    return;

                itPeer.Data.Send(Proto_, Proto2_, Proto3_, Proto4_);
            }
            public void Send(CKey Key_, bool Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeer = _Peers.Get((Int32)Key_.PeerNum);
                if (!itPeer)
                    return;

                if (itPeer.Data.Key.PeerCounter != Key_.PeerCounter)
                    return;

                itPeer.Data.Send(Data_);
            }
            public void Send(CKey Key_, SByte Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeer = _Peers.Get((Int32)Key_.PeerNum);
                if (!itPeer)
                    return;

                if (itPeer.Data.Key.PeerCounter != Key_.PeerCounter)
                    return;

                itPeer.Data.Send(Data_);
            }
            public void Send(CKey Key_, Byte Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeer = _Peers.Get((Int32)Key_.PeerNum);
                if (!itPeer)
                    return;

                if (itPeer.Data.Key.PeerCounter != Key_.PeerCounter)
                    return;

                itPeer.Data.Send(Data_);
            }
            public void Send(CKey Key_, Int16 Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeer = _Peers.Get((Int32)Key_.PeerNum);
                if (!itPeer)
                    return;

                if (itPeer.Data.Key.PeerCounter != Key_.PeerCounter)
                    return;

                itPeer.Data.Send(Data_);
            }
            public void Send(CKey Key_, UInt16 Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeer = _Peers.Get((Int32)Key_.PeerNum);
                if (!itPeer)
                    return;

                if (itPeer.Data.Key.PeerCounter != Key_.PeerCounter)
                    return;

                itPeer.Data.Send(Data_);
            }
            public void Send(CKey Key_, Int32 Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeer = _Peers.Get((Int32)Key_.PeerNum);
                if (!itPeer)
                    return;

                if (itPeer.Data.Key.PeerCounter != Key_.PeerCounter)
                    return;

                itPeer.Data.Send(Data_);
            }
            public void Send(CKey Key_, UInt32 Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeer = _Peers.Get((Int32)Key_.PeerNum);
                if (!itPeer)
                    return;

                if (itPeer.Data.Key.PeerCounter != Key_.PeerCounter)
                    return;

                itPeer.Data.Send(Data_);
            }
            public void Send(CKey Key_, Int64 Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeer = _Peers.Get((Int32)Key_.PeerNum);
                if (!itPeer)
                    return;

                if (itPeer.Data.Key.PeerCounter != Key_.PeerCounter)
                    return;

                itPeer.Data.Send(Data_);
            }
            public void Send(CKey Key_, UInt64 Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeer = _Peers.Get((Int32)Key_.PeerNum);
                if (!itPeer)
                    return;

                if (itPeer.Data.Key.PeerCounter != Key_.PeerCounter)
                    return;

                itPeer.Data.Send(Data_);
            }
            public void Send(CKey Key_, float Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeer = _Peers.Get((Int32)Key_.PeerNum);
                if (!itPeer)
                    return;

                if (itPeer.Data.Key.PeerCounter != Key_.PeerCounter)
                    return;

                itPeer.Data.Send(Data_);
            }
            public void Send(CKey Key_, double Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeer = _Peers.Get((Int32)Key_.PeerNum);
                if (!itPeer)
                    return;

                if (itPeer.Data.Key.PeerCounter != Key_.PeerCounter)
                    return;

                itPeer.Data.Send(Data_);
            }
            public void Send(CKey Key_, String Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeer = _Peers.Get((Int32)Key_.PeerNum);
                if (!itPeer)
                    return;

                if (itPeer.Data.Key.PeerCounter != Key_.PeerCounter)
                    return;

                itPeer.Data.Send(Data_);
            }
            public void Send(CKey Key_, DateTime Data_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                var itPeer = _Peers.Get((Int32)Key_.PeerNum);
                if (!itPeer)
                    return;

                if (itPeer.Data.Key.PeerCounter != Key_.PeerCounter)
                    return;

                itPeer.Data.Send(Data_);
            }
            public void SendAll(Int32 ProtoNum_, SProto Proto_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                foreach (var Peer in _Peers)
                    Peer.Send(ProtoNum_, Proto_);
            }
            public void SendAll(SProto Proto0_, SProto Proto1_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                foreach (var Peer in _Peers)
                    Peer.Send(Proto0_, Proto1_);
            }
            public void SendAll(SProto Proto_, Int32 Proto2_, SProto Proto3_)    // unity덕분으로 .net 버젼이 낮아 dynamic 으로 처리불가하여 이렇게 처리...
            {
                foreach (var Peer in _Peers)
                    Peer.Send(Proto_, Proto2_, Proto3_);
            }
            public void SendAllExcept(CKey Key_, CStream Stream_)
            {
                foreach (var Peer in _Peers)
                {
                    if (Peer.Key.Equals(Key_))
                        continue;

                    Peer.Send(Stream_);
                }
            }
            public void SendAllExcept(CKey Key_, Int32 ProtoNum_, SProto Proto_)
            {
                foreach (var Peer in _Peers)
                {
                    if (Peer.Key.Equals(Key_))
                        continue;

                    Peer.Send(ProtoNum_, Proto_);
                }
            }
            public TimeSpan Latency(TPeerCnt PeerNum_)
            {
                var itPeer = _Peers.Get((Int32)PeerNum_);
                if (!itPeer)
                    return TimeSpan.Zero;

                return itPeer.Data.Latency;
            }
            public void Proc()
            {
                for (var it = _Peers.Begin(); it;)
                {
                    var itCheck = it;
                    it.MoveNext();

                    var NetRet = itCheck.Data.Proc();
                    if (NetRet != ENetRet.Ok)
                    {
                        _Close(itCheck, NetRet);
                        continue;
                    }

                    try
                    {
                        if (!itCheck.Data.Poll())
                            continue;

                        while (true)
                        {
                            var RecvState = itCheck.Data.RecvedBegin();
                            if (RecvState == ERecvState.NoData)
                            {
                                break;
                            }
                            else if (RecvState == ERecvState.UserData)
                            {
                                if (!itCheck.Data.DoesWillClose())
                                {
                                    var Key = itCheck.Data.Key;
                                    try
                                    {
                                        _RecvFunc(Key, itCheck.Data.StreamRcv);
                                    }
                                    catch (Exception Exception_)
                                    {
                                        throw new ExceptionExtern(Exception_.Message);
                                    }
                                    if (!_Peers.Get((Int32)Key.PeerNum))
                                        break;
                                }
                            }

                            itCheck.Data.RecvedEnd();
                        }
                    }
                    catch (ExceptionExtern)
                    {
                        throw;
                    }
                    catch (Exception)
                    {
                        _Close(itCheck, ENetRet.SocketError);
                    }
                }
            }
        }
    }
}