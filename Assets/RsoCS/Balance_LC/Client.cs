namespace rso
{
    using System;
    using core;
    using Base;
    using net;

    namespace balance_lc
    {
        using TPeerCnt = UInt32;
        public class CClient
        {
            enum _EState
            {
                WillConnectServer,
                Allocated,
                Max,
                Null
            }
            class _SServer
            {
                public COptionStream<CNamePort> NamePort;
                public CKey Key;
                public _EState State = _EState.Null;
                public _SServer(string FileName_)
                {
                    NamePort = new COptionStream<CNamePort>(FileName_, true);
                }
            }
            CNamePort _AllocatorNamePort;
            string _DataPath = string.Empty;
            CList<_SServer> _Servers = new CList<_SServer>();
            net.CClient _Net;
            TLinkFunc _LinkFunc;
            TLinkFailFunc _LinkFailFunc;
            TUnLinkFunc _UnLinkFunc;
            TRecvFunc _RecvFunc;

            void _Link(CKey Key_)
            {
                _Servers[(Int32)Key_.PeerNum].Key = Key_;
            }
            void _LinkFail(TPeerCnt PeerNum_, ENetRet NetRet_)
            {
                var itServer = _Servers.Get((Int32)PeerNum_);
                itServer.Data.NamePort.Clear();

                _Servers.Remove(itServer);
                _LinkFailFunc(PeerNum_, NetRet_);
            }
            void _UnLink(CKey Key_, ENetRet NetRet_)
            {
                var itServer = _Servers.Get((Int32)Key_.PeerNum);

                switch (itServer.Data.State)
                {
                    case _EState.WillConnectServer:
                        return;
                    case _EState.Allocated:
                        _UnLinkFunc(Key_, NetRet_);
                        break;
                    default:
                        _LinkFailFunc(Key_.PeerNum, NetRet_);
                        break;
                }

                _Servers.Remove(itServer);
            }
            void _Recv(CKey Key_, CStream Stream_)
            {
                Int32 Proto = 0;
                Stream_.Pop(ref Proto);

                switch ((EProto)Proto)
                {
                    case EProto.AcServerToConnect:
                        {
                            _RecvAcServerToConnect(Key_, Stream_);
                            return;
                        }
                    case EProto.ScAllocated:
                        {
                            _RecvScAllocated(Key_, Stream_);
                            return;
                        }
                    case EProto.ScUserProto:
                        {
                            _RecvFunc(Key_, Stream_);
                            return;
                        }
                    default:
                        {
                            _Net.Close(Key_.PeerNum);
                            return;
                        }
                }
            }
            void _RecvAcServerToConnect(CKey Key_, CStream Stream_)
            {
                var itServer = _Servers.Get((Int32)Key_.PeerNum);

                var Proto = new SAcServerToConnect();
                Stream_.Pop(ref Proto);

                itServer.Data.State = _EState.WillConnectServer;
                _Net.Close(Key_.PeerNum);

                itServer.Data.State = _EState.Null;
                itServer.Data.NamePort.Data = new CNamePort(Proto.ClientBindNamePortPub);
                if (_Net.Connect(new CNamePort(Proto.ClientBindNamePortPub), Key_.PeerNum) == null)
                {
                    _Servers.Remove(itServer);
                    _LinkFailFunc(Key_.PeerNum, ENetRet.SystemError);
                    return;
                }
            }
            void _RecvScAllocated(CKey Key_, CStream Stream_)
            {
                var itServer = _Servers.Get((Int32)Key_.PeerNum);

                //var Proto = new SScAllocated();
                //Stream_.Pop(Proto);

                itServer.Data.State = _EState.Allocated;
                _LinkFunc(Key_);
            }
            public CClient(CNamePort AllocatorNamePort_, string DataPath_, TLinkFunc LinkFunc_, TLinkFailFunc LinkFailFunc_, TUnLinkFunc UnLinkFunc_, TRecvFunc RecvFunc_)
            {
                _AllocatorNamePort = AllocatorNamePort_;
                _DataPath = DataPath_;
                _Net = new net.CClient(
                    _Link, _LinkFail, _UnLink, _Recv,
                    false, 1024000, 1024000,
                    TimeSpan.FromMilliseconds(120000), TimeSpan.FromMilliseconds(60000), 60);

                _LinkFunc = LinkFunc_;
                _LinkFailFunc = LinkFailFunc_;
                _UnLinkFunc = UnLinkFunc_;
                _RecvFunc = RecvFunc_;
            }
            public void Dispose()
            {
                _Net.CloseAll();
                _Net.Dispose();
            }
            public bool IsLinked(TPeerCnt PeerNum_)
            {
                var Server = _Servers.Get((Int32)PeerNum_);
                if (!Server)
                    return false;

                return (Server.Data.State == _EState.Allocated);
            }
            public bool IsConnecting(TPeerCnt PeerNum_)
            {
                return _Servers.Get((Int32)PeerNum_);
            }
            public bool Connect(TPeerCnt PeerNum_)
            {
                try
                {
                    CNamePort AuthNamePort;
                    var DataFile = _DataPath + "Allocator_" + PeerNum_.ToString() + ".dat";
                    var itServer = _Servers.AddAt((Int32)PeerNum_, new _SServer(DataFile));
                    if (itServer.Data.NamePort.Data)
                        AuthNamePort = itServer.Data.NamePort.Data;
                    else
                        AuthNamePort = _AllocatorNamePort;

                    if (_Net.Connect(AuthNamePort, PeerNum_) == null)
                    {
                        _Servers.Remove((Int32)PeerNum_);
                        return false;
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }
            public bool Connect()
            {
                return Connect((TPeerCnt)_Servers.NewIndex);
            }
            public void Send(TPeerCnt PeerNum_, CStream Stream_)
            {
                if (_Servers[(Int32)PeerNum_].State != _EState.Allocated)
                    return;

                _Net.Send(PeerNum_, (Int32)EProto.CsUserProto, Stream_);
            }
            public void Send(TPeerCnt PeerNum_, SProto Proto_)
            {
                var itServer = _Servers.Get((Int32)PeerNum_);
                if (!itServer ||
                    itServer.Data.State != _EState.Allocated)
                    return;

                _Net.Send(PeerNum_, (Int32)EProto.CsUserProto, Proto_);
            }
            public void Send(TPeerCnt PeerNum_, SProto Proto_, SProto Proto2_)
            {
                var itServer = _Servers.Get((Int32)PeerNum_);
                if (!itServer ||
                    itServer.Data.State != _EState.Allocated)
                    return;

                _Net.Send(PeerNum_, (Int32)EProto.CsUserProto, Proto_, Proto2_);
            }
            public void Send(CKey Key_, SProto Proto_)
            {
                var itServer = _Servers.Get((Int32)Key_.PeerNum);
                if (!itServer ||
                    itServer.Data.Key.PeerCounter != Key_.PeerCounter ||
                    itServer.Data.State != _EState.Allocated)
                    return;

                _Net.Send(Key_.PeerNum, (Int32)EProto.CsUserProto, Proto_);
            }
            public void Send(CKey Key_, SProto Proto_, SProto Proto2_)
            {
                var itServer = _Servers.Get((Int32)Key_.PeerNum);
                if (!itServer ||
                    itServer.Data.Key.PeerCounter != Key_.PeerCounter ||
                    itServer.Data.State != _EState.Allocated)
                    return;

                _Net.Send(Key_.PeerNum, (Int32)EProto.CsUserProto, Proto_, Proto2_);
            }
            public void SendAll(SProto Proto_)
            {
                foreach (var i in _Servers)
                {
                    if (i.State != _EState.Allocated)
                        continue;

                    _Net.Send(i.Key.PeerNum, (Int32)EProto.CsUserProto, Proto_);
                }
            }
            public void SendAllExcept(CKey Key_, SProto Proto_)
            {
                foreach (var i in _Servers)
                {
                    if (i.State != _EState.Allocated)
                        continue;

                    if (i.Key.Equals(Key_))
                        continue;

                    _Net.Send(i.Key.PeerNum, (Int32)EProto.CsUserProto, Proto_);
                }
            }
            public void Close(TPeerCnt PeerNum_)
            {
                _Net.Close(PeerNum_);
            }
            public void Close(CKey Key_)
            {
                _Net.Close(Key_);
            }
            public void CloseAll()
            {
                _Net.CloseAll();
            }
            public TimeSpan Latency(TPeerCnt PeerNum_)
            {
                return _Net.Latency(PeerNum_);
            }
            public void Proc()
            {
                _Net.Proc();
            }
            public void ClearServer(TPeerCnt PeerNum_)
            {
                _Servers[(Int32)PeerNum_].NamePort.Clear();
            }
        }
    }
}
