using System;
using System.Collections.Generic;
using rso.core;
using rso.net;

namespace rso
{
    namespace monitor
    {
        using TPeerCnt = UInt32;
        using TLongIP = UInt32;

        public class CClient
        {
            public delegate void TCallbackFunc(CStream Stream_);
            public delegate void TRecvFunc(CKey Key_, EScProto ScProto_, CStream Stream_);

            CNamePort _ServerNamePort;
            CKey _ServerKey;
            net.CClient _Net = null;
            TLinkFunc _LinkFunc;
            TLinkFailFunc _LinkFailFunc;
            TUnLinkFunc _UnLinkFunc;
            SCsLogin _LoginInfo;
            TRecvFunc _RecvFunc;

			void _LinkS(CKey Key_)
            {
                _ServerKey = Key_;
                _LinkFunc(Key_);
                _Net.Send(Key_.PeerNum, new SCsHeader(ECsProto.Login, new List<SKey>()), _LoginInfo);
            }
            void _LinkFailS(TPeerCnt PeerNum_, ENetRet NetRet_)
            {
                _LinkFailFunc(PeerNum_, NetRet_);
            }
            void _UnLinkS(CKey Key_, ENetRet NetRet_)
            {
                _UnLinkFunc(Key_, NetRet_);
            }
			void _RecvS(CKey Key_, CStream Stream_)
            {
                SScHeader Header = new SScHeader();
                Header.Push(Stream_);
                _RecvFunc(Key_, Header.Proto, Stream_);
            }
            public CClient(CNamePort ServerNamePort_, TLinkFunc LinkFunc_, TLinkFailFunc LinkFailFunc_, TUnLinkFunc UnLinkFunc_, TRecvFunc RecvFunc_)
            {
                _ServerNamePort = ServerNamePort_;
                _LinkFunc = LinkFunc_;
                _LinkFailFunc = LinkFailFunc_;
                _UnLinkFunc = UnLinkFunc_;
                _RecvFunc = RecvFunc_;

                _Net = new net.CClient(
                    _LinkS, _LinkFailS, _UnLinkS, _RecvS,
                    false, 1024000, 1024000,
                    new TimeSpan(200000000), new TimeSpan(90000000), 60);
            }
   			public bool Login(SCsLogin LoginInfo_)
            {
                _LoginInfo = LoginInfo_;

                if (_Net.Connect(_ServerNamePort) == null)
                    return false;

                return true;
            }
			public void Logout()
            {
                _Net.CloseAll();
            }
		    // To Agent
		    public void AgentFileSend(List<SKey> AgentKeys_, SSaFileSend Proto_)
		    {
			    _Net.Send(_ServerKey, new SCsHeader(ECsProto.ToAgent, AgentKeys_), new CStream().Push(new SSaHeader(ESaProto.FileSend)).Push(Proto_));
		    }
            public void AgentKeepAlive(List<SKey> AgentKeys_, SSaKeepAlive Proto_)
		    {
			    _Net.Send(_ServerKey, new SCsHeader(ECsProto.ToAgent, AgentKeys_), new CStream().Push(new SSaHeader(ESaProto.KeepAlive)).Push(Proto_));
		    }
            public void AgentRunProcess(List<SKey> AgentKeys_, SSaRunProcess Proto_)
		    {
			    _Net.Send(_ServerKey, new SCsHeader(ECsProto.ToAgent, AgentKeys_), new CStream().Push(new SSaHeader(ESaProto.RunProcess)).Push(Proto_));
		    }
            public void AgentKillProcess(List<SKey> AgentKeys_, SSaKillProcess Proto_)
		    {
			    _Net.Send(_ServerKey, new SCsHeader(ECsProto.ToAgent, AgentKeys_), new CStream().Push(new SSaHeader(ESaProto.KillProcess)).Push(Proto_));
		    }
            public void AgentShellCommand(List<SKey> AgentKeys_, SSaShellCommand Proto_)
		    {
			    _Net.Send(_ServerKey, new SCsHeader(ECsProto.ToAgent, AgentKeys_), new CStream().Push(new SSaHeader(ESaProto.ShellCommand)).Push(Proto_));
		    }
            // To Proc
            public void ProcStop(List<SKey> AgentKeys_, SApStop Proto_)
		    {
			    _Net.Send(_ServerKey, new SCsHeader(ECsProto.ToAgent, AgentKeys_), new CStream().Push(new SSaHeader(ESaProto.ToApp)).Push(new CStream().Push(new SApHeader(EApProto.Stop)).Push(Proto_)));
		    }
            public void ProcMessage(List<SKey> AgentKeys_, SApMessage Proto_)
		    {
			    _Net.Send(_ServerKey, new SCsHeader(ECsProto.ToAgent, AgentKeys_), new CStream().Push(new SSaHeader(ESaProto.ToApp)).Push(new CStream().Push(new SApHeader(EApProto.Message)).Push(Proto_)));
		    }
			public void ProcUserProto(List<SKey> AgentKeys_, CStream Stream_)
			{
                _Net.Send(_ServerKey, new SCsHeader(ECsProto.ToAgent, AgentKeys_), new CStream().Push(new SSaHeader(ESaProto.ToApp)).Push(new CStream().Push(new SApHeader(EApProto.UserProto)).Push(Stream_)));
			}

            //public void Send<_TProtoType>(_TProtoType Proto_) where _TProtoType : IProto
            //{
            //    _Net.Send(_ServerKey, Proto_);
            //}
            //public void Send<_TProtoType0, _TProtoType1>(_TProtoType0 Proto0_, _TProtoType1 Proto1_)
            //    where _TProtoType0 : IProto
            //    where _TProtoType1 : IProto
            //{
            //    _Net.Send(_ServerKey, Proto0_, Proto1_);
            //}
            //public void Send<_TProtoType0, _TProtoType1, _TProtoType2>(_TProtoType0 Proto0_, _TProtoType1 Proto1_, _TProtoType2 Proto2_)
            //    where _TProtoType0 : IProto
            //    where _TProtoType1 : IProto
            //    where _TProtoType2 : IProto
            //{
            //    _Net.Send(_ServerKey, Proto0_, Proto1_, Proto2_);
            //}
            public void Proc()
            {
                _Net.Proc();
            }
        }
    }
}