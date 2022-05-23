using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using rso.Base;

namespace rso
{
    namespace net
    {
        using TPeerCnt = UInt32;

        public class CClient : CNet, IClient
        {
            class _SConnect
            {
                public SocketAsyncEventArgs Event = null;
                public ENetRet NetRet = ENetRet.Null;
            }
            struct _SConnectingInfo
            {
                public CKey Key;
                public Socket socket;

                public _SConnectingInfo(CKey Key_, Socket socket_)
                {
                    Key = Key_;
                    socket = socket_;
                }
            }

            CList<CNamePort> _Connectings = new CList<CNamePort>(); // Connecting 
            CConnectTimeOut _ConnectTimeOut;
            new TLinkFunc _LinkFunc;
            new TUnLinkFunc _UnLinkFunc;
            TLinkFailFunc _LinkFailFunc;
            Int32 _SendBuffSize = 0;
            Int32 _RecvBuffSize = 0;
            CPeriod _ProcPeriod = new CPeriod(new TimeSpan(10000000));
            CList<CKey> _PeersAndConnectings = new CList<CKey>();
            CLFQueueB<_SConnect> _Connects = new CLFQueueB<_SConnect>();

            void _LinkFail(TPeerCnt PeerNum_, ENetRet NetRet_)
            {
                try
                {
                    _LinkFailFunc(PeerNum_, NetRet_);
                }
                catch
                {
                }
            }
            void _Link(CKey Key_)
            {
                _ConnectTimeOut.Remove((Int32)Key_.PeerNum);
                _LinkFunc(Key_);
            }
            void _UnLink(CKey Key_, ENetRet NetRet_)
            {
                _PeersAndConnectings.Remove((Int32)Key_.PeerNum);
                _ConnectTimeOut.Remove((Int32)Key_.PeerNum);
                _UnLinkFunc(Key_, NetRet_);
            }
            void _PushIOCP(SocketAsyncEventArgs Event_, ENetRet NetRet_)
            {
                lock (_Connects)
                {
                    _SConnect LFConnect = null;
                    while ((LFConnect = _Connects.GetPushBuf()) == null)
                        Thread.Sleep(3);

                    LFConnect.Event = Event_;
                    LFConnect.NetRet = NetRet_;
                    _Connects.Push();
                }
            }
            void _Worker(object Sender, SocketAsyncEventArgs Event_)
            {
                switch (Event_.LastOperation)
                {
                    case SocketAsyncOperation.Connect:
                        if (Event_.SocketError == SocketError.Success)
                            _PushIOCP(Event_, ENetRet.Ok);
                        else
                            _PushIOCP(Event_, ENetRet.SocketError);
                        break;

                    default:
                        throw new Exception("Invalid Operation");
                }
            }
            public CClient(
                TLinkFunc LinkFunc_, TLinkFailFunc LinkFailFunc_, TUnLinkFunc UnLinkFunc_, TRecvFunc RecvFunc_,
                bool NoDelay_, Int32 RecvBuffSize_, Int32 SendBuffSize_,
                TimeSpan HBRcvDelay_, TimeSpan HBSndDelay_, Int32 ConnectTimeOutSec_) :
                base(
                    null, null, RecvFunc_,
                    NoDelay_,
                    HBRcvDelay_, HBSndDelay_)
            {
                _ConnectTimeOut = new CConnectTimeOut(ConnectTimeOutSec_);
                base._LinkFunc = _Link;
                base._UnLinkFunc = _UnLink;
                _LinkFunc = LinkFunc_;
                _LinkFailFunc = LinkFailFunc_;
                _UnLinkFunc = UnLinkFunc_;
                _RecvFunc = RecvFunc_;
                _SendBuffSize = SendBuffSize_;
                _RecvBuffSize = RecvBuffSize_;
            }
            public bool IsConnecting(TPeerCnt PeerNum_)
            {
                return _PeersAndConnectings.Get((Int32)PeerNum_);
            }
            public CKey Connect(CNamePort NamePort_, TPeerCnt PeerNum_)
            {
                try
                {
                    var itPeerConnecting = _PeersAndConnectings.AddAt((Int32)PeerNum_, _NewKey(PeerNum_));
                    try
                    {
                        _Connectings.AddAt((Int32)PeerNum_, NamePort_);
                    }
                    catch
                    {
                        _PeersAndConnectings.Remove(itPeerConnecting);
                        throw;
                    }

                    return itPeerConnecting.Data;
                }
                catch
                {
                    return null;
                }
            }
            public CKey Connect(CNamePort NamePort_)
            {
                return Connect(NamePort_, (TPeerCnt)_PeersAndConnectings.NewIndex);
            }
            public new void Close(TPeerCnt PeerNum_)
            {
                var Peer = _Peers.Get((Int32)PeerNum_);
                if (Peer)
                {
                    base.Close(PeerNum_);
                }
                else
                {
                    if (_PeersAndConnectings.Remove((Int32)PeerNum_)) // 여기서 지워야 LockFree 큐에서 들어오는 Connect 정보가 처리되지 않음.
                    {
                        _Connectings.Remove((Int32)PeerNum_);
                        _LinkFail(PeerNum_, ENetRet.UserClose);
                    }
                }
            }
            public new bool Close(CKey Key_)
            {
                var Peer = _Peers.Get((Int32)Key_.PeerNum);
                if (Peer)
                {
                    if (Peer.Data.Key.PeerCounter != Key_.PeerCounter)
                        return false;

                    _Close(Peer, ENetRet.UserClose);

                    return true;
                }
                else
                {
                    var PeerConnecting = _PeersAndConnectings.Get((Int32)Key_.PeerNum);
                    if (!PeerConnecting)
                        return false;

                    if (PeerConnecting.Data.PeerCounter != Key_.PeerCounter)
                        return false;

                    _PeersAndConnectings.Remove(PeerConnecting); // 여기서 먼저 지워야 LockFree 큐에서 들어오는 Connect 정보가 처리되지 않음.
                    _Connectings.Remove((Int32)Key_.PeerNum);
                    _LinkFail(Key_.PeerNum, ENetRet.UserClose);

                    return true;
                }
            }
            public new void CloseAll()
            {
                for (var it = _PeersAndConnectings.Begin(); it != _PeersAndConnectings.End(); it = _PeersAndConnectings.Begin())
                {
                    var Peer = _Peers.Get(it.Index);
                    if (Peer)
                    {
                        base.Close(it.Data);
                    }
                    else
                    {
                        var PeerNum = it.Data.PeerNum;
                        _PeersAndConnectings.Remove(it);
                        _Connectings.Remove((Int32)PeerNum);
                        _LinkFail(PeerNum, ENetRet.UserClose);
                    }
                }
            }
            public new void Proc()
            {
                base.Proc();

                for (var it = _Connectings.Begin(); it != _Connectings.End();)
                {
                    var itCheck = it;
                    it.MoveNext();

                    var itPeer = _PeersAndConnectings.Get(itCheck.Index);
                    var PeerNum = itPeer.Data.PeerNum;
                    var NamePort = itCheck.Data;
                    _Connectings.Remove(itCheck);

                    try
                    {
                        Socket Sock = null;

                        AddressFamily[] AddressFamilies = { AddressFamily.InterNetwork, AddressFamily.InterNetworkV6 };

                        foreach (var i in NamePort.GetIPAddresses())
                        {
                            if (i.AddressFamily != AddressFamily.InterNetwork)
                                continue;

                            foreach (var a in AddressFamilies)
                            {
                                Sock = new Socket(a, SocketType.Stream, ProtocolType.Tcp)
                                {
                                    NoDelay = _NoDelay,
                                    SendBufferSize = _SendBuffSize,
                                    ReceiveBufferSize = _RecvBuffSize
                                };

                                var RecvEvent = new SocketAsyncEventArgs();
                                RecvEvent.Completed += new EventHandler<SocketAsyncEventArgs>(_Worker);
                                RecvEvent.UserToken = new _SConnectingInfo(itPeer.Data, Sock);
                                RecvEvent.RemoteEndPoint = new IPEndPoint(i, NamePort.Port);

                                bool ConnRet = false;

                                try
                                {
                                    ConnRet = Sock.ConnectAsync(RecvEvent);
                                }
                                catch
                                {
                                    Sock = null;
                                    continue;
                                }

                                if (!ConnRet)
                                {
                                    if (!RecvEvent.ConnectSocket.Connected) // Android(iOS는 미확인)에서 Wifi, Data 모두 끈 상태에서 여기에 들어오므로 여기서 연결여부 체크
                                    {
                                        Sock = null;
                                        continue;
                                    }

                                    _Link(itPeer.Data, Sock, RecvEvent);
                                }
                                else
                                {
                                    _ConnectTimeOut.Add((Int32)PeerNum, Sock);
                                }

                                break; // Success
                            }

                            if (Sock != null)
                                break; // Success
                        }

                        if (Sock == null)
                            throw new Exception("Can not new Socket");
                    }
                    catch (ExceptionExtern)
                    {
                        throw;
                    }
                    catch
                    {
                        _PeersAndConnectings.Remove(itPeer);
                        _LinkFail(PeerNum, ENetRet.SocketError);
                        continue;
                    }
                }

                for (var LFConnect = _Connects.GetPopBuf();
                    LFConnect != null;
                    LFConnect = _Connects.GetPopBuf())
                {
                    var ConnectInfo = (_SConnectingInfo)LFConnect.Event.UserToken;
                    var itPeer = _PeersAndConnectings.Get((Int32)ConnectInfo.Key.PeerNum);
                    if (itPeer && itPeer.Data.Equals(ConnectInfo.Key))
                    {
                        if (LFConnect.NetRet == ENetRet.Ok)
                        {
                            try
                            {
                                _Link(ConnectInfo.Key, ConnectInfo.socket, LFConnect.Event);
                            }
                            catch (ExceptionExtern)
                            {
                                throw;
                            }
                            catch
                            {
                                LFConnect.NetRet = ENetRet.SystemError;
                            }
                        }

                        if (LFConnect.NetRet != ENetRet.Ok)
                        {
                            _ConnectTimeOut.Remove(itPeer.Index);
                            _PeersAndConnectings.Remove(itPeer);
                            _LinkFail(ConnectInfo.Key.PeerNum, LFConnect.NetRet);
                        }
                    }

                    _Connects.Pop();
                }

                if (_ProcPeriod.CheckAndNextLoose())
                    _ConnectTimeOut.Proc();
            }
        }
    }
}