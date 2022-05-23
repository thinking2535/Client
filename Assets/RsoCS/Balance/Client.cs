namespace rso
{
    namespace balance
    {
        using Base;
        using core;
        using net;
        using System;
        using System.IO;
        using TPeerCnt = System.UInt32;
        public class CClient : INet
        {
            class _SServer
            {
                public TPeerCnt PeerCounter = 0;
                public CNamePort PrevConnectNamePort = new CNamePort();
                public CList<CNamePort> ConnectNamePorts = new CList<CNamePort>();
                public COptionStream<CNamePort> ParentNamePort; // 접속 실패하더라도 Clear하지 않고 성공하면 갱신. (유저의 네트워크 상태, 유저 강종 등에 의해 접속 실패 할 수 있기 때문)
                public bool Logon = false;

                public _SServer(TPeerCnt PeerCounter_, CNamePort MasterNamePort_, string DataFile_)
                {
                    PeerCounter = PeerCounter_;
                    ParentNamePort = new COptionStream<CNamePort>(DataFile_, true);

                    ConnectNamePorts.Add(MasterNamePort_);

                    if (ParentNamePort.Data && !ParentNamePort.Data.Equals(MasterNamePort_))
                        ConnectNamePorts.Add(ParentNamePort.Data);
                }
                public CNamePort PopConnectNamePort()
                {
                    if (ConnectNamePorts.Count == 0)
                        return new CNamePort();

                    var ConnectNamePort = ConnectNamePorts.Last();
                    ConnectNamePorts.RemoveLast();

                    return ConnectNamePort;
                }
                public void Login()
                {
                    Logon = true;
                    ParentNamePort.Data = PrevConnectNamePort;
                }
            }

            TPeerCnt _PeerCounter = 0;
            CList<_SServer> _Servers = new CList<_SServer>();

            TLinkFunc _LinkFuncS;
            TLinkFailFunc _LinkFailFuncS;
            TUnLinkFunc _UnLinkFuncS;
            TRecvFunc _RecvFuncS;

            net.CClient _NetS;

            bool _Connect(CList<_SServer>.SIterator Server_)
            {
                var ConnectNamePort = Server_.Data.PopConnectNamePort();
                if (ConnectNamePort)
                {
                    if (_NetS.Connect(ConnectNamePort, (TPeerCnt)Server_.Index))
                        return true;
                }

                var PeerNum = (TPeerCnt)Server_.Index;
                _Servers.Remove(Server_);

               _LinkFailFuncS(PeerNum, ENetRet.ConnectFail);

                return false;
            }
            bool _Connect(TPeerCnt PeerNum_)
            {
                return _Connect(_Servers.Get((Int32)PeerNum_));
            }
            void _LinkS(CKey Key_)
            {
                _NetS.Send(Key_.PeerNum, new SHeader(EProto.CsConnect), new SCsConnect(_Servers[(Int32)Key_.PeerNum].PrevConnectNamePort));
                _Servers[(Int32)Key_.PeerNum].PrevConnectNamePort = _NetS.GetNamePort(Key_.PeerNum);
            }
            void _LinkFailS(TPeerCnt PeerNum_, ENetRet NetRet_)
            {
                _Connect(PeerNum_);
            }
            void _UnLinkS(CKey Key_, ENetRet NetRet_)
            {
                var It = _Servers.Get((Int32)Key_.PeerNum);
                if (It.Data.Logon)
                {
                    _Servers.Remove((Int32)Key_.PeerNum);
                    _UnLinkFuncS(Key_, NetRet_);
                }
                else
                {
                    _Connect(Key_.PeerNum);
                }
            }
            void _RecvS(CKey Key_, CStream Stream_)
            {
                var Header = new SHeader();
                Stream_.Pop(ref Header);

                if (!_Servers[(Int32)Key_.PeerNum].Logon)
                {
                    switch (Header.Proto)
                    {
                        case EProto.ScNewParent:
                            _RecvScNewParent(Key_, Stream_);
                            return;
                        case EProto.ScAllocated:
                            _RecvScAllocated(Key_, Stream_);
                            return;
                        default:
                            _NetS.Close(Key_.PeerNum);
                            return;
                    }
                }
                else
                {
                    switch (Header.Proto)
                    {
                        case EProto.ScUserProto:
                            _RecvFuncS(Key_, Stream_);
                            return;
                        default:
                            _NetS.Close(Key_.PeerNum);
                            return;
                    }
                }
            }
            void _RecvScNewParent(CKey Key_, CStream Stream_)
            {
                var Proto = new SScNewParent();
                Stream_.Pop(ref Proto);

                _Servers[(Int32)Key_.PeerNum].ConnectNamePorts.Add(new CNamePort(Proto.ClientBindNamePortPub));
                _NetS.Close(Key_.PeerNum);
            }
            void _RecvScAllocated(CKey Key_, CStream Stream_)
            {
                var Proto = new SScAllocated();
                Stream_.Pop(ref Proto);

                _Servers[(Int32)Key_.PeerNum].Login();
                _LinkFuncS(Key_);
            }

            public CClient(TLinkFunc LinkFuncS_, TLinkFailFunc LinkFailFuncS_, TUnLinkFunc UnLinkFuncS_, TRecvFunc RecvFuncS_)
            {
                _LinkFuncS = LinkFuncS_;
                _LinkFailFuncS = LinkFailFuncS_;
                _UnLinkFuncS = UnLinkFuncS_;
                _RecvFuncS = RecvFuncS_;

                _NetS = new net.CClient(
                    _LinkS, _LinkFailS, _UnLinkS, _RecvS,
                    false, 1024000, 1024000,
                    TimeSpan.FromMilliseconds(120000), TimeSpan.FromMilliseconds(60000), 60);
            }
            public void Dispose()
            {
                _NetS.CloseAll();
                _NetS.Dispose();
            }
            public bool IsLinked(TPeerCnt PeerNum_)
            {
                var Server = _Servers.Get((Int32)PeerNum_);
                if (!Server)
                    return false;

                return Server.Data.Logon;
            }
            public void Close(TPeerCnt PeerNum_)
            {
                _NetS.Close(PeerNum_);
            }
            public bool Close(CKey Key_)
            {
                return _NetS.Close(Key_);
            }
            public void CloseAll()
            {
                _NetS.CloseAll();
            }
            public void WillClose(TPeerCnt PeerNum_, TimeSpan WaitMilliseconds_)
            {
                _NetS.WillClose(PeerNum_, WaitMilliseconds_);
            }
            public bool WillClose(CKey Key_, TimeSpan WaitMilliseconds_)
            {
                return _NetS.WillClose(Key_, WaitMilliseconds_);
            }
            public TPeerCnt GetPeerCnt()
            {
                return _NetS.GetPeerCnt();
            }
            public TimeSpan Latency(TPeerCnt PeerNum_)
            {
                return _NetS.Latency(PeerNum_);
            }
            public bool IsConnected(TPeerCnt PeerNum_)
            {
                return _Servers.Get((Int32)PeerNum_);
            }
            public CKey Connect(TPeerCnt PeerNum_, string DataPath_, CNamePort MasterNamePort_)
            {
                var FullPath = Path.GetFullPath(DataPath_);

                var FileName = FullPath + "Data_" + PeerNum_.ToString() + ".bin";

                _Servers.AddAt((Int32)PeerNum_, new _SServer(_PeerCounter, MasterNamePort_, FileName));
                if (!_Connect(PeerNum_))
                    return new CKey();

                return new CKey(PeerNum_, _PeerCounter++);
            }
            public CKey Connect(string DataPath_, CNamePort MasterNamePort_)
            {
                return Connect((TPeerCnt)_Servers.NewIndex, DataPath_, MasterNamePort_);
            }
            public void Proc()
            {
                _NetS.Proc();
            }
            public void Send(TPeerCnt PeerNum_, SProto Proto_)
            {
                if (!_Servers[(Int32)PeerNum_].Logon)
                    return;

                _NetS.Send(PeerNum_, new SHeader(EProto.CsUserProto), Proto_);
            }
            public void Send(TPeerCnt PeerNum_, Int32 ProtoNum_)
            {
                if (!_Servers[(Int32)PeerNum_].Logon)
                    return;

                _NetS.Send(PeerNum_, new SHeader(EProto.CsUserProto), ProtoNum_);
            }
            public void Send(TPeerCnt PeerNum_, Int32 ProtoNum_, SProto Proto1_)
            {
                if (!_Servers[(Int32)PeerNum_].Logon)
                    return;

                _NetS.Send(PeerNum_, new SHeader(EProto.CsUserProto), ProtoNum_, Proto1_);
            }
            public void Send(TPeerCnt PeerNum_, SProto Proto0_, SProto Proto1_)
            {
                if (!_Servers[(Int32)PeerNum_].Logon)
                    return;

                _NetS.Send(PeerNum_, new SHeader(EProto.CsUserProto), Proto0_, Proto1_);
            }
            public void Send(CKey Key_, SProto Proto_)
            {
                if (_Servers[(Int32)Key_.PeerNum].PeerCounter != Key_.PeerCounter)
                    return;

                _NetS.Send(Key_.PeerNum, new SHeader(EProto.CsUserProto), Proto_);
            }
            public void SendAll(SProto Proto_)
            {
                for (var it = _Servers.Begin(); it != _Servers.End(); it.MoveNext())
                {
                    if (!it.Data.Logon)
                        continue;

                    _NetS.Send((TPeerCnt)it.Index, new SHeader(EProto.CsUserProto), Proto_);
                }
            }
        }
    }
}