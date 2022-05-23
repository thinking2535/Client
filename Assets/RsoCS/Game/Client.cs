using System;
using System.IO;
using rso.core;
using rso.Base;
using rso.net;

namespace rso
{
    namespace game
    {
        using TPeerCnt = UInt32;
        using TUID = Int64;
        using TSessionCode = Int64;
        using TState = System.Byte;
        using TFriends = System.Collections.Generic.Dictionary<System.Int64, SFriend>;
        using TNick = System.String;
        using TMessage = System.String;

        public class CClient : INet
        {
            enum _ENode
            {
                Auth,
                Master,
                Server,
                Max,
                Null
            }
            class _SClient
            {
                public EProto CaProto = EProto.Null;
                public _ENode CurNode = _ENode.Null;
                public COptionStream<CNamePort> MasterNamePort = null;
                public COptionStream<SLoginInfo> LastLoginInfo = null;
                public CNamePort AuthNamePort = null;
                public TUID UID = 0;
                public string ID = "";
                public string Nick = "";
                public TUID SubUID = 0;
                public TState State = 0;
                public CStream Stream = null;
                public TSessionCode SessionCode;
                public bool Logon = false;
                public CClientConnectHistory ConnectHistory = new CClientConnectHistory();

                public _SClient(TPeerCnt PeerNum_, String DataPath_, CNamePort AuthNamePort_, string ID_, string Nick_, TUID SubUID_, TState State_, CStream Stream_) // For Create
                {
                    CaProto = EProto.CaCreate;
                    MasterNamePort = new COptionStream<CNamePort>(DataPath_ + "Master_" + PeerNum_.ToString() + ".dat", true);
                    LastLoginInfo = new COptionStream<SLoginInfo>(DataPath_ + "LastLoginInfo_" + PeerNum_.ToString() + ".dat", true);
                    AuthNamePort = AuthNamePort_;
                    ID = ID_;
                    Nick = Nick_;
                    SubUID = SubUID_;
                    State = State_;
                    Stream = Stream_;
                    Directory.CreateDirectory(DataPath_);
                }
                public _SClient(TPeerCnt PeerNum_, String DataPath_, CNamePort AuthNamePort_, TUID SubUID_, CStream Stream_) // For Login
                {
                    CaProto = EProto.CaLogin;
                    MasterNamePort = new COptionStream<CNamePort>(DataPath_ + "Master_" + PeerNum_.ToString() + ".dat", true);
                    LastLoginInfo = new COptionStream<SLoginInfo>(DataPath_ + "LastLoginInfo_" + PeerNum_.ToString() + ".dat", true);
                    AuthNamePort = AuthNamePort_;
                    UID = LastLoginInfo.Data.UID;
                    ID = LastLoginInfo.Data.ID;
                    SubUID = SubUID_;
                    Stream = Stream_;
                    Directory.CreateDirectory(DataPath_);
                }
                public _SClient(String DataPath_, CNamePort AuthNamePort_, string ID_, TUID SubUID_) // For Check
                {
                    CaProto = EProto.CaCheck;
                    AuthNamePort = AuthNamePort_;
                    ID = ID_;
                    SubUID = SubUID_;
                    Directory.CreateDirectory(DataPath_);
                }
			    public SCaCreate GetCaCreate()
			    {
				    return new SCaCreate(ID, Nick, State);
			    }
                public SCaLogin GetCaLogin()
			    {
				    return new SCaLogin(UID, ID);
			    }
                public SCaCheck GetCaCheck()
			    {
				    return new SCaCheck(ID);
			    }
                public _ENode GetNodeToConnect(string ID_)
                {
                    //파일읽기성공여부    외부ID유효  ID일치여부  처리
                    //x                   x           .           로그인 실패
                    //x                   o           .           Auth 로 로그인
                    //o                   x           .           Master로 로그인
                    //o                   o           x           Auth 로 로그인
                    //o                   o           o           Master로 로그인

                    if (LastLoginInfo.Data.UID == 0)
                    {
                        if (ID_.Length == 0)
                        {
                            return _ENode.Null;
                        }
                        else
                        {
                            ID = ID_;
                            return _ENode.Auth;
                        }
                    }
                    else
                    {
                        if (ID_.Length == 0 || LastLoginInfo.Data.ID == ID_)
                        {
                            return _ENode.Master;
                        }
                        else
                        {
                            UID = 0;
                            ID = ID_;
                            return _ENode.Auth;
                        }
                    }
                }
                public bool IsCreate()
                {
                    return (CaProto == EProto.CaCreate);
                }
                public bool IsCheck()
                {
                    return (CaProto == EProto.CaCheck);
                }
                public bool IsValidAccount()
                {
                    return (UID > 0 || ID.Length > 0);
                }
                public void SetNullAccount()
                {
                    UID = 0;
                    ID = "";
                }
                public void Clear()
                {
                    MasterNamePort.Clear();
                    LastLoginInfo.Clear();
                }
                public void Login(string Nick_)
                {
                    Nick = Nick_;
                    Logon = true;
                    LastLoginInfo.Data = new SLoginInfo(UID, ID);
                    LastLoginInfo.Save();
                }
                public void ReleaseAccount()
                {
                    LastLoginInfo.Clear();
                }
            }

            public delegate void TLinkFunc(CKey Key_, TUID UID_, string Nick_, TFriends Friends_);
            public delegate void TLinkFailFunc(TPeerCnt PeerNum_, EGameRet GameRet_);
            public delegate void TUnLinkFunc(CKey Key_, EGameRet GameRet_);
            public delegate void TErrorFunc(TPeerCnt PeerNum_, EGameRet GameRet_);
            public delegate void TCheckFunc(TUID UID_, CStream Stream_);
            public delegate void TFriendAddedFunc(TPeerCnt PeerNum_, TUID UID_, SFriend Friend_);
            public delegate void TFriendRequestedFunc(TPeerCnt PeerNum_, TUID UID_, TNick Nick_);
            public delegate void TFriendAllowedFunc(TPeerCnt PeerNum_, TUID FriendUID_);
            public delegate void TFriendDenyedFunc(TPeerCnt PeerNum_, TUID FriendUID_);
            public delegate void TFriendBlockedFunc(TPeerCnt PeerNum_, TUID FriendUID_);
            public delegate void TFriendUnBlockedFunc(TPeerCnt PeerNum_, TUID FriendUID_);
            public delegate void TStateChangedFunc(TPeerCnt PeerNum_, TState State_);
            public delegate void TFriendStateChangedFunc(TPeerCnt PeerNum_, TUID FriendUID_, TState State_);
            public delegate void TMessageReceivedFunc(TPeerCnt PeerNum_, TUID FriendUID_, TMessage Message_);

            public TLinkFunc LinkFunc;
            public TLinkFailFunc LinkFailFunc;
            public TUnLinkFunc UnLinkFunc;
            public TRecvFunc RecvFunc;
            public TErrorFunc ErrorFunc;
            public TCheckFunc CheckFunc;
            public TFriendAddedFunc FriendAddedFunc;
            public TFriendRequestedFunc FriendRequestedFunc;
            public TFriendAllowedFunc FriendAllowedFunc;
            public TFriendDenyedFunc FriendDenyedFunc;
            public TFriendBlockedFunc FriendBlockedFunc;
            public TFriendUnBlockedFunc FriendUnBlockedFunc;
            public TStateChangedFunc StateChangedFunc;
            public TFriendStateChangedFunc FriendStateChangedFunc;
            public TMessageReceivedFunc MessageReceivedFunc;

            SVersion _Version;
            CList<_SClient> _Clients = new CList<_SClient>();
            net.CClient _NetA;
            net.CClient _NetM;
            net.CClient _NetS;

            EGameRet _NetRetToGameRet(ENetRet NetRet_)
            {
                switch (NetRet_)
                {
                    case ENetRet.Ok: return EGameRet.Ok;
                    case ENetRet.UserClose: return EGameRet.UserClose;
                    case ENetRet.HeartBeatFail: return EGameRet.HeartBeatFail;
                    case ENetRet.KeepConnectTimeOut: return EGameRet.KeepConnectTimeOut;
                    case ENetRet.ConnectFail: return EGameRet.ConnectFail;
                    case ENetRet.CertifyFail: return EGameRet.CertifyFail;
                    case ENetRet.SystemError: return EGameRet.SystemError;
                    case ENetRet.SocketError: return EGameRet.SocketError;
                    case ENetRet.InvalidPacket: return EGameRet.InvalidPacket;
                    default: return EGameRet.NetError;
                }
            }

            void _LoginClear(TPeerCnt PeerNum_, EGameRet GameRet_)
            {
                var itClient = _Clients.Get((Int32)PeerNum_);

                if (GameRet_ == EGameRet.InvalidID)
                    itClient.Data.Clear();

                _Clients.Remove(itClient);
            }
            void _LoginFail(TPeerCnt PeerNum_, EGameRet GameRet_)
            {
                _LoginClear(PeerNum_, GameRet_);
                LinkFailFunc?.Invoke(PeerNum_, GameRet_);
            }
            void _LoginFailAndCloseA(TPeerCnt PeerNum_, EGameRet GameRet_)
            {
                _LoginClear(PeerNum_, GameRet_);
                _NetA.Close(PeerNum_);
                LinkFailFunc?.Invoke(PeerNum_, GameRet_);
            }
            void _LoginFailAndCloseM(TPeerCnt PeerNum_, EGameRet GameRet_)
            {
                _LoginClear(PeerNum_, GameRet_);
                _NetM.Close(PeerNum_);
                LinkFailFunc?.Invoke(PeerNum_, GameRet_);
            }
            void _LoginFailAndCloseS(TPeerCnt PeerNum_, EGameRet GameRet_)
            {
                _LoginClear(PeerNum_, GameRet_);
                _NetS.Close(PeerNum_);
                LinkFailFunc?.Invoke(PeerNum_, GameRet_);
            }
            void _CheckFail(TPeerCnt PeerNum_, EGameRet GameRet_)
            {
                _Clients.Remove((Int32)PeerNum_);
                ErrorFunc?.Invoke(PeerNum_, GameRet_);
            }
            void _CheckFailAndClose(TPeerCnt PeerNum_, EGameRet GameRet_)
            {
                _Clients.Remove((Int32)PeerNum_);
                _NetA.Close(PeerNum_);
                ErrorFunc?.Invoke(PeerNum_, GameRet_); // 외부에서 다시 연결 할 수 있으므로 콜백 전에 연결전 상태로
            }
            void _Connect(TPeerCnt PeerNum_, _ENode Node_, CNamePort NamePort_)
            {
                _Clients[(Int32)PeerNum_].CurNode = Node_;

                switch (Node_)
                {
                    case _ENode.Auth:
                        if (!_NetA.Connect(_Clients[(Int32)PeerNum_].AuthNamePort, PeerNum_))
                            _LoginFail(PeerNum_, EGameRet.ConnectAuthFail);
                        break;
                    case _ENode.Master:
                        if (_NetM.Connect(NamePort_, PeerNum_) == null)
                            _LoginFail(PeerNum_, EGameRet.ConnectMasterFail);
                        break;
                    case _ENode.Server:
                        if (_NetS.Connect(NamePort_, PeerNum_) == null)
                            _LoginFail(PeerNum_, EGameRet.ConnectServerFail);
                        break;
                    default:
                        break;
                }
            }
            void _ConnectToUpper(TPeerCnt PeerNum_, _ENode Node_)
            {
                if (Node_ == _ENode.Master)
                {
                    if (_Clients[(Int32)PeerNum_].MasterNamePort.Data)
                    {
                        _Connect(PeerNum_, _ENode.Master, _Clients[(Int32)PeerNum_].MasterNamePort.Data);
                        return;
                    }
                    Node_ = _ENode.Auth;
                }

                if (Node_ == _ENode.Auth)
                {
                    _Connect(PeerNum_, _ENode.Auth, null);
                    return;
                }
            }
            bool _ConnectToLower(TPeerCnt PeerNum_, _ENode Node_, CNamePort NamePort_)
            {
                if (!_Clients[(Int32)PeerNum_].ConnectHistory.Connect(NamePort_))
                    return false;

                _Connect(PeerNum_, Node_, NamePort_);
                return true;
            }

            void _LinkA(CKey Key_)
            {
                switch (_Clients[(Int32)Key_.PeerNum].CaProto)
                {
                    case EProto.CaCreate:
                        _NetA.Send(Key_.PeerNum, new SHeader(_Clients[(Int32)Key_.PeerNum].CaProto), _Clients[(Int32)Key_.PeerNum].GetCaCreate());
                        break;
                    case EProto.CaLogin:
                        _NetA.Send(Key_.PeerNum, new SHeader(_Clients[(Int32)Key_.PeerNum].CaProto), _Clients[(Int32)Key_.PeerNum].GetCaLogin());
                        break;
                    case EProto.CaCheck:
                        _NetA.Send(Key_.PeerNum, new SHeader(_Clients[(Int32)Key_.PeerNum].CaProto), _Clients[(Int32)Key_.PeerNum].GetCaCheck());
                        break;
                }
            }
            void _LinkFailA(TPeerCnt PeerNum_, ENetRet NetRet_)
            {
                if (_Clients[(Int32)PeerNum_].IsCheck())
                    _CheckFail(PeerNum_, _NetRetToGameRet(NetRet_));
                else
                    _LoginFail(PeerNum_, _NetRetToGameRet(NetRet_));
            }
            void _UnLinkA(CKey Key_, ENetRet NetRet_)
            {
                var Client = _Clients.Get((Int32)Key_.PeerNum);
                if (!Client)
                    return;

                if (_Clients[(Int32)Key_.PeerNum].IsCheck())
                {
                    _CheckFail(Key_.PeerNum, _NetRetToGameRet(NetRet_));
                }
                else
                {
                    if (_Clients[(Int32)Key_.PeerNum].CurNode != _ENode.Auth)
                        return;

                    _LoginFail(Key_.PeerNum, _NetRetToGameRet(NetRet_));
                }
            }
            void _RecvA(CKey Key_, CStream Stream_)
            {
                var Header = new SHeader();
                Stream_.Pop(ref Header);

                if (_Clients[(Int32)Key_.PeerNum].IsCheck())
                {
                    switch (Header.Proto)
                    {
                        case EProto.AcCheck: _RecvAcCheck(Key_, Stream_); return;
                        case EProto.AcCheckFail: _RecvAcCheckFail(Key_, Stream_); return;
                        default: _CheckFailAndClose(Key_.PeerNum, EGameRet.InvalidPacket); return;
                    }
                }
                else
                {
                    switch (Header.Proto)
                    {
                        case EProto.AcLogin: _RecvAcLogin(Key_, Stream_); return;
                        case EProto.AcLoginFail: _RecvAcLoginFail(Key_, Stream_); return;
                        default: _LoginFailAndCloseA(Key_.PeerNum, EGameRet.InvalidPacket); return;
                    }
                }
            }
            void _RecvAcLogin(CKey Key_, CStream Stream_)
            {
                var Proto = new SAcLogin();
                Stream_.Pop(ref Proto);

                _Clients[(Int32)Key_.PeerNum].UID = Proto.UID;

                if (_ConnectToLower(Key_.PeerNum, _ENode.Master, new CNamePort(Proto.ClientBindNamePortPubToMaster)))
                    _NetA.Close(Key_.PeerNum);
                else
                    _LoginFailAndCloseA(Key_.PeerNum, EGameRet.ConnectMasterFail);
            }
            void _RecvAcLoginFail(CKey Key_, CStream Stream_)
            {
                var Proto = new SAcLoginFail();
                Stream_.Pop(ref Proto);

                _LoginFailAndCloseA(Key_.PeerNum, Proto.GameRet);
            }
            void _RecvAcCheck(CKey Key_, CStream Stream_)
            {
                var Proto = new SAcCheck();
                Stream_.Pop(ref Proto);

                _Clients.Remove((Int32)Key_.PeerNum);
                _NetA.Close(Key_.PeerNum);
                CheckFunc?.Invoke(Proto.UID, Proto.Stream); ;
            }
            void _RecvAcCheckFail(CKey Key_, CStream Stream_)
            {
                var Proto = new SAcCheckFail();
                Stream_.Pop(ref Proto);

                _CheckFailAndClose(Key_.PeerNum, Proto.GameRet);
            }
            void _LinkM(CKey Key_)
            {
                var Client = _Clients[(Int32)Key_.PeerNum];
                Client.MasterNamePort.Data = new CNamePort(_NetM.GetNamePort(Key_.PeerNum));
                _NetM.Send(Key_.PeerNum, new SHeader(EProto.CmLogin), new SCmLogin(Client.UID, Client.ID, Client.SubUID));
            }
            void _LinkFailM(TPeerCnt PeerNum_, ENetRet NetRet_)
            {
                var Client = _Clients.Get((Int32)PeerNum_);
                if (!Client)
                    return;

                if (_Clients[(Int32)PeerNum_].CurNode != _ENode.Master)
                    return;

                if (!_Clients[(Int32)PeerNum_].IsValidAccount())
                {
                    _LoginFail(PeerNum_, _NetRetToGameRet(NetRet_));
                    return;
                }

                _ConnectToUpper(PeerNum_, _ENode.Auth);
            }
            void _UnLinkM(CKey Key_, ENetRet NetRet_)
            {
                var Client = _Clients.Get((Int32)Key_.PeerNum);
                if (!Client)
                    return;

                if (_Clients[(Int32)Key_.PeerNum].CurNode != _ENode.Master)
                    return;

                if (!_Clients[(Int32)Key_.PeerNum].IsValidAccount())
                {
                    _LoginFail(Key_.PeerNum, _NetRetToGameRet(NetRet_));
                    return;
                }

                _ConnectToUpper(Key_.PeerNum, _ENode.Auth);
            }
            void _RecvM(CKey Key_, CStream Stream_)
            {
                var Header = new SHeader();
                Stream_.Pop(ref Header);

                switch (Header.Proto)
                {
                    case EProto.McLogin: _RecvMcLogin(Key_, Stream_); return;
                    case EProto.McLoginFail: _RecvMcLoginFail(Key_, Stream_); return;
                    default: _NetM.Close(Key_); return;
                }
            }
            void _RecvMcLogin(CKey Key_, CStream Stream_)
            {
                var Proto = new SMcLogin();
                Stream_.Pop(ref Proto);

                _Clients[(Int32)Key_.PeerNum].SessionCode = Proto.SessionCode;

                if (_ConnectToLower(Key_.PeerNum, _ENode.Server, new CNamePort(Proto.ClientBindNamePortPubToServer)))
                    _NetM.Close(Key_.PeerNum);
                else
                    _LoginFailAndCloseM(Key_.PeerNum, EGameRet.ConnectServerFail);
            }
            void _RecvMcLoginFail(CKey Key_, CStream Stream_)
            {
                var Proto = new SMcLoginFail();
                Stream_.Pop(ref Proto);

                _LoginFailAndCloseM(Key_.PeerNum, Proto.GameRet);
            }

            void _LinkS(CKey Key_)
            {
                var Client = _Clients[(Int32)Key_.PeerNum];
                _NetS.Send(Key_.PeerNum, new SHeader(EProto.CsLogin), new SCsLogin(Client.UID, Client.ID, Client.SubUID, Client.SessionCode, _Version, Client.IsCreate(), Client.Stream));
            }
            void _LinkFailS(TPeerCnt PeerNum_, ENetRet NetRet_)
            {
                var Client = _Clients.Get((Int32)PeerNum_);
                if (!Client)
                    return;

                if (_Clients[(Int32)PeerNum_].CurNode != _ENode.Server)
                    return;

                if (!_Clients[(Int32)PeerNum_].IsValidAccount())
                {
                    _LoginFail(PeerNum_, _NetRetToGameRet(NetRet_));
                    return;
                }

                _ConnectToUpper(PeerNum_, _ENode.Master);
            }
            void _UnLinkS(CKey Key_, ENetRet NetRet_)
            {
                var Client = _Clients.Get((Int32)Key_.PeerNum);
                if (!Client)
                    return;

                if (_Clients[(Int32)Key_.PeerNum].CurNode != _ENode.Server)
                    return;

                if (_Clients[(Int32)Key_.PeerNum].Logon)
                {
                    _Clients.Remove((Int32)Key_.PeerNum);
                    UnLinkFunc?.Invoke(Key_, _NetRetToGameRet(NetRet_));
                    return;
                }

                // 현재 클라가 로그인 시에 최초 GameServer로 접속하는 경우는 없고 최소 Master를 거쳐야 하기 때문에 최초 게임서버로 접속하여 호출된 _UnLinkS 
                _LoginFail(Key_.PeerNum, _NetRetToGameRet(NetRet_));
            }
            void _RecvS(CKey Key_, CStream Stream_)
            {
                var Header = new SHeader();
                Stream_.Pop(ref Header);

                switch (Header.Proto)
                {
                    case EProto.ScLogin: _RecvScLogin(Key_, Stream_); return;
                    case EProto.ScLoginFail: _RecvScLoginFail(Key_, Stream_); return;
                    case EProto.ScError: _RecvScError(Key_, Stream_); return;
                    case EProto.ScAddFriendRequest: _RecvScAddFriendRequest(Key_, Stream_); return;
                    case EProto.ScAddFriend: _RecvScAddFriend(Key_, Stream_); return;
                    case EProto.ScAllowFriend: _RecvScAllowFriend(Key_, Stream_); return;
                    case EProto.ScDenyFriend: _RecvScDenyFriend(Key_, Stream_); return;
                    case EProto.ScBlockFriend: _RecvScBlockFriend(Key_, Stream_); return;
                    case EProto.ScUnBlockFriend: _RecvScUnBlockFriend(Key_, Stream_); return;
                    case EProto.ScChangeState: _RecvScChangeState(Key_, Stream_); return;
                    case EProto.ScFriendStateChanged: _RecvScFriendStateChanged(Key_, Stream_); return;
                    case EProto.ScFriendStateChangedOffline: _RecvScFriendStateChangedOffline(Key_, Stream_); return;
                    case EProto.ScMessageReceived: _RecvScMessageReceived(Key_, Stream_); return;
                    case EProto.ScUserProto: RecvFunc?.Invoke(Key_, Stream_); return;
                    default: _NetS.Close(Key_); return;
                }
            }
            void _RecvScLogin(CKey Key_, CStream Stream_)
            {
                var Proto = new SScLogin();
                Stream_.Pop(ref Proto);

                _Clients[(Int32)Key_.PeerNum].Login(Proto.Nick);
                LinkFunc?.Invoke(Key_, _Clients[(Int32)Key_.PeerNum].UID, _Clients[(Int32)Key_.PeerNum].Nick, Proto.Friends);
            }
            void _RecvScLoginFail(CKey Key_, CStream Stream_)
            {
                var Proto = new SScLoginFail();
                Stream_.Pop(ref Proto);

                _LoginFailAndCloseS(Key_.PeerNum, Proto.GameRet);
            }
            void _RecvScError(CKey Key_, CStream Stream_)
            {
                var Proto = new SScError();
                Stream_.Pop(ref Proto);

                ErrorFunc?.Invoke(Key_.PeerNum, Proto.GameRet);
            }
            void _RecvScAddFriendRequest(CKey Key_, CStream Stream_)
	        {
                var Proto = new SScAddFriendRequest();
                Stream_.Pop(ref Proto);

                FriendRequestedFunc?.Invoke(Key_.PeerNum, Proto.FromUID, Proto.FromNick);
	        }
	        void _RecvScAddFriend(CKey Key_, CStream Stream_)
	        {
                var Proto = new SScAddFriend();
                Stream_.Pop(ref Proto);

                FriendAddedFunc?.Invoke(Key_.PeerNum, Proto.ToUID, Proto.Friend);
	        }
	        void _RecvScAllowFriend(CKey Key_, CStream Stream_)
	        {
                var Proto = new SScAllowFriend();
                Stream_.Pop(ref Proto);

                FriendAllowedFunc?.Invoke(Key_.PeerNum, Proto.FriendUID);
            }
            void _RecvScDenyFriend(CKey Key_, CStream Stream_)
	        {
                var Proto = new SScDenyFriend();
                Stream_.Pop(ref Proto);

                FriendDenyedFunc?.Invoke(Key_.PeerNum, Proto.FriendUID);
            }
            void _RecvScBlockFriend(CKey Key_, CStream Stream_)
	        {
                var Proto = new SScBlockFriend();
                Stream_.Pop(ref Proto);

                FriendBlockedFunc?.Invoke(Key_.PeerNum, Proto.FriendUID);
            }
            void _RecvScUnBlockFriend(CKey Key_, CStream Stream_)
            {
                var Proto = new SScUnBlockFriend();
                Stream_.Pop(ref Proto);

                FriendUnBlockedFunc?.Invoke(Key_.PeerNum, Proto.FriendUID);
            }
            void _RecvScChangeState(CKey Key_, CStream Stream_)
	        {
                var Proto = new SScChangeState();
                Stream_.Pop(ref Proto);

                StateChangedFunc?.Invoke(Key_.PeerNum, Proto.State);
            }
            void _RecvScFriendStateChanged(CKey Key_, CStream Stream_)
            {
                var Proto = new SScFriendStateChanged();
                Stream_.Pop(ref Proto);

                FriendStateChangedFunc?.Invoke(Key_.PeerNum, Proto.FriendUID, Proto.FriendState);
            }
            void _RecvScFriendStateChangedOffline(CKey Key_, CStream Stream_)
            {
                var Proto = new SScFriendStateChangedOffline();
                Stream_.Pop(ref Proto);

                if (FriendStateChangedFunc != null)
                {
                    foreach (var i in Proto.Friends)
                        FriendStateChangedFunc(Key_.PeerNum, i, global.c_Default_State);
                }
            }
            void _RecvScMessageReceived(CKey Key_, CStream Stream_)
            {
                var Proto = new SScMessageReceived();
                Stream_.Pop(ref Proto);

                MessageReceivedFunc?.Invoke(Key_.PeerNum, Proto.FromUID, Proto.Message);
            }
            public CClient(SVersion Version_)
            {
                _Version = Version_;

                _NetA = new net.CClient(
                    _LinkA, _LinkFailA, _UnLinkA, _RecvA,
                    false, 1024000, 1024000,
                    TimeSpan.Zero, TimeSpan.Zero, 60);

                _NetM = new net.CClient(
                    _LinkM, _LinkFailM, _UnLinkM, _RecvM,
                    false, 1024000, 1024000,
                    TimeSpan.Zero, TimeSpan.Zero, 60);

                _NetS = new net.CClient(
                    _LinkS, _LinkFailS, _UnLinkS, _RecvS,
                    true, 1024000, 1024000,
                    TimeSpan.FromSeconds(120), TimeSpan.FromSeconds(5), 60);
            }
            public void Dispose()
            {
                Logout();

                _NetA.Dispose();
                _NetM.Dispose();
                _NetS.Dispose();
            }
            public bool IsLinked(TPeerCnt PeerNum_)
            {
                var itClient = _Clients.Get((Int32)PeerNum_);
                if (!itClient)
                    return false;

                return itClient.Data.Logon;
            }
            public CNamePort GetNamePort(TPeerCnt PeerNum_)
            {
                return _NetS.GetNamePort(PeerNum_);
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
            public void WillClose(TPeerCnt PeerNum_, TimeSpan WaitDuration_)
            {
                _NetS.WillClose(PeerNum_, WaitDuration_);
            }
            public bool WillClose(CKey Key_, TimeSpan WaitDuration_)
            {
                return _NetS.WillClose(Key_, WaitDuration_);
            }
            public TPeerCnt GetPeerCnt()
            {
                return _NetS.GetPeerCnt();
            }
            public TimeSpan Latency(TPeerCnt PeerNum_)
            {
                return _NetS.Latency(PeerNum_);
            }
            public void Proc()
            {
                _NetA.Proc();
                _NetM.Proc();
                _NetS.Proc();
            }
            public bool IsConnecting(TPeerCnt PeerNum_)
            {
                return _Clients.Get((Int32)PeerNum_);
            }
            public void Create(TPeerCnt PeerNum_, string DataPath_, CNamePort AuthNamePort_, string ID_, string Nick_, TUID SubUID_, TState State_, CStream Stream_)
            {
                _Clients.AddAt((Int32)PeerNum_, new _SClient(PeerNum_, DataPath_, AuthNamePort_, ID_, Nick_, SubUID_, State_, Stream_));
                _ConnectToUpper(PeerNum_, _ENode.Auth);
            }
            public bool Login(TPeerCnt PeerNum_, string DataPath_, CNamePort HubNamePort_, string ID_, TUID SubUID_, CStream Stream_)
            {
                var itClient = _Clients.AddAt((Int32)PeerNum_, new _SClient(PeerNum_, DataPath_, HubNamePort_, SubUID_, Stream_));
                var NodeToConnect = itClient.Data.GetNodeToConnect(ID_);
                if (NodeToConnect == _ENode.Null)
                {
                    _Clients.Remove(itClient);
                    return false;
                }

                _ConnectToUpper(PeerNum_, NodeToConnect);

                return true;
            }
            void _Logout(CList<_SClient>.SIterator Client_)
            {
                Client_.Data.SetNullAccount();

                if (_NetA.IsConnecting((TPeerCnt)Client_.Index))
                    _NetA.Close((TPeerCnt)Client_.Index);

                if (_NetM.IsConnecting((TPeerCnt)Client_.Index))
                    _NetM.Close((TPeerCnt)Client_.Index);

                if (_NetS.IsConnecting((TPeerCnt)Client_.Index))
                    _NetS.Close((TPeerCnt)Client_.Index);
            }
            public void Logout(TPeerCnt PeerNum_)
            {
                _Logout(_Clients.Get((Int32)PeerNum_));
            }
            public void Logout()
            {
                for (var it = _Clients.Begin(); it;)
                {
                    var itCheck = it;
                    it.MoveNext();

                    _Logout(itCheck);
                }
            }
            void Check(TPeerCnt PeerNum_, string DataPath_, CNamePort AuthNamePort_, string ID_, TUID SubUID_)
            {
                _Clients.AddAt((Int32)PeerNum_, new _SClient(DataPath_, AuthNamePort_, ID_, SubUID_));

                if (!_NetA.Connect(AuthNamePort_, PeerNum_))
                    _CheckFail(PeerNum_, EGameRet.ConnectAuthFail);
            }
            public void ReleaseAccount(TPeerCnt PeerNum_) // 연동 해제
            {
                var itClient = _Clients.Get((Int32)PeerNum_);
                itClient.Data.ReleaseAccount();
                _Logout(itClient);
            }
            public void Send<_TCsProto>(TPeerCnt PeerNum_, Int32 ProtoNum_, _TCsProto Proto_) where _TCsProto : SProto
            {
                _NetS.Send(PeerNum_, new SHeader(EProto.CsUserProto), ProtoNum_, Proto_);
            }
            public void Send<_TCsProto>(CKey Key_, Int32 ProtoNum_, _TCsProto Proto_) where _TCsProto : SProto
            {
                _NetS.Send(Key_, new SHeader(EProto.CsUserProto), ProtoNum_, Proto_);
            }
            public void SendAll<_TCsProto>(Int32 ProtoNum_, _TCsProto Proto_) where _TCsProto : SProto
            {
                _NetS.SendAll(new SHeader(EProto.CsUserProto), ProtoNum_, Proto_);
            }
	        void AddFriend(TPeerCnt PeerNum_, TNick Nick_)
	        {
		        _NetS.Send(PeerNum_, new SHeader(EProto.CsAddFriend), new SCsAddFriend(Nick_));
	        }
            void AllowFriend(TPeerCnt PeerNum_, TUID UID_)
            {
                _NetS.Send(PeerNum_, new SHeader(EProto.CsAllowFriend), new SCsAllowFriend(UID_));
            }
            void DenyFriend(TPeerCnt PeerNum_, TUID UID_)
            {
                _NetS.Send(PeerNum_, new SHeader(EProto.CsDenyFriend), new SCsDenyFriend(UID_));
            }
            void BlockFriend(TPeerCnt PeerNum_, TUID UID_)
            {
                _NetS.Send(PeerNum_, new SHeader(EProto.CsBlockFriend), new SCsBlockFriend(UID_));
            }
            void UnBlockFriend(TPeerCnt PeerNum_, TUID UID_)
            {
                _NetS.Send(PeerNum_, new SHeader(EProto.CsUnBlockFriend), new SCsUnBlockFriend(UID_));
            }
            void ChangeState(TPeerCnt PeerNum_, TState State_)
            {
                _NetS.Send(PeerNum_, new SHeader(EProto.CsChangeState), new SCsChangeState(State_));
            }
            void MessageSend(TPeerCnt PeerNum_, TUID ToUID_, TMessage Message_)
            {
                _NetS.Send(PeerNum_, new SHeader(EProto.CsMessageSend), new SCsMessageSend(ToUID_, Message_));
            }
        }
    }
}
