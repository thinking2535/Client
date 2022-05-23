using TSize = System.Int32;
using TCheckSum = System.UInt64;
using TUID = System.Int64;
using TPeerCnt = System.UInt32;
using TLongIP = System.UInt32;
using TPort = System.UInt16;
using TPacketSeq = System.UInt64;
using TSessionCode = System.Int64;
using SRangeUID = rso.net.SRangeKey<System.Int64>;
using TVer = System.SByte;
using TID = System.String;
using TNick = System.String;
using TMessage = System.String;
using TState = System.Byte;
using TServerNets = System.Collections.Generic.HashSet<rso.game.SServerNet>;
using TMasterNets = System.Collections.Generic.List<rso.game.SMasterNet>;
using TFriendDBs = System.Collections.Generic.Dictionary<System.Int64,rso.game.SFriendDB>;
using TFriends = System.Collections.Generic.Dictionary<System.Int64,rso.game.SFriend>;
using TUIDFriendInfos = System.Collections.Generic.List<rso.game.SUIDFriendInfo>;
using TFriendInfos = System.Collections.Generic.Dictionary<System.Int64,rso.game.SFriendInfo>;
using System;
using System.Collections.Generic;
using rso.core;


namespace rso
{
	namespace game
	{
		using rso.net;
		public enum EProto
		{
			MaMasterOn,
			AmMasterOn,
			MsMasterOn,
			AmOtherMasterOn,
			MsOtherMasterOn,
			AmOtherMasterOff,
			MsOtherMasterOff,
			SmServerOn,
			MsServerOn,
			MsOtherServerOn,
			MaServerOn,
			AmOtherMasterServerOn,
			MsOtherMasterServerOn,
			MsOtherServerOff,
			MaServerOff,
			AmOtherMasterServerOff,
			MsOtherMasterServerOff,
			CaCreate,
			AmCreate,
			MaCreate,
			MaCreateFail,
			CaLogin,
			AcLogin,
			AcLoginFail,
			SmChangeNick,
			MaChangeNick,
			AmChangeNick,
			AmChangeNickFail,
			MsChangeNick,
			MsChangeNickFail,
			CaCheck,
			AmCheck,
			MsCheck,
			SmCheck,
			MaCheck,
			MaCheckFail,
			AcCheck,
			AcCheckFail,
			CmLogin,
			MsLogin,
			SmLogin,
			SmSessionEnd,
			SmSetOpened,
			McLogin,
			McLoginFail,
			CsLogin,
			ScLogin,
			ScLoginFail,
			ScError,
			CsAddFriend,
			SmAddFriend,
			MaAddFriendGetUID,
			AmAddFriendGetUID,
			AmAddFriendGetUIDFail,
			MaAddFriend,
			AmAddFriendRequest,
			MsAddFriendRequest,
			ScAddFriendRequest,
			MaAddFriendRequest,
			MaAddFriendRequestFail,
			AmAddFriend,
			AmAddFriendFail,
			MsAddFriend,
			MsAddFriendFail,
			ScAddFriend,
			CsAllowFriend,
			SmAllowFriend,
			MsAllowFriend,
			MsAllowFriendFail,
			ScAllowFriend,
			CsDenyFriend,
			SmDenyFriend,
			MsDenyFriend,
			MsDenyFriendFail,
			ScDenyFriend,
			CsBlockFriend,
			SmBlockFriend,
			MsBlockFriend,
			MsBlockFriendFail,
			ScBlockFriend,
			CsUnBlockFriend,
			SmUnBlockFriend,
			MsUnBlockFriend,
			MsUnBlockFriendFail,
			ScUnBlockFriend,
			CsMessageSend,
			SsMessageSend,
			ScMessageReceived,
			CsChangeState,
			SmChangeState,
			MsChangeState,
			MsChangeStateFail,
			ScChangeState,
			MmFriendStateChanged,
			MsFriendStateChanged,
			ScFriendStateChanged,
			MmFriendStateChangedRenew,
			MsFriendStateChangedOffline,
			ScFriendStateChangedOffline,
			SmToServer,
			MaToServer,
			AmToServer,
			MsToServer,
			MsSessionHold,
			AmPunish,
			MsPunish,
			AmUserProto,
			MaUserProto,
			MsUserProto,
			SmUserProto,
			ScUserProto,
			CsUserProto,
			Max,
			Null,
		}
		public enum EGameRet
		{
			Ok,
			UserClose,
			HeartBeatFail,
			KeepConnectTimeOut,
			ConnectFail,
			CertifyFail,
			SystemError,
			SocketError,
			InvalidPacket,
			NetError,
			InvalidAccess,
			InvalidVersion,
			InvalidUID,
			InvalidID,
			InvalidIDLength,
			InvalidNick,
			InvalidNickLength,
			InvalidSession,
			AlreadyExist,
			DataBaseError,
			DataBasePushError,
			NoAuthToConnect,
			NoMasterToConnect,
			NoServerToConnect,
			ConnectAuthFail,
			ConnectMasterFail,
			ConnectServerFail,
			SessionNotFound,
			AddSessionFail,
			Punished,
			AuthDisconnected,
			Max,
			Null,
		}
		public enum EFriendState
		{
			Adding,
			Request,
			Normal,
			Blocked,
			Max,
			Null,
		}
		public class SHeader : SProto
		{
			public EProto Proto = default(EProto);
			public SHeader()
			{
			}
			public SHeader(SHeader Obj_)
			{
				Proto = Obj_.Proto;
			}
			public SHeader(EProto Proto_)
			{
				Proto = Proto_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Proto);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Proto", ref Proto);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Proto);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Proto", Proto);
			}
			public void Set(SHeader Obj_)
			{
				Proto = Obj_.Proto;
			}
			public override string StdName()
			{
				return 
					"rso.game.EProto";
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Proto, "Proto");
			}
		}
		public class SUIDPair : SProto
		{
			public TUID UID = default(TUID);
			public TUID SubUID = default(TUID);
			public SUIDPair()
			{
			}
			public SUIDPair(SUIDPair Obj_)
			{
				UID = Obj_.UID;
				SubUID = Obj_.SubUID;
			}
			public SUIDPair(TUID UID_, TUID SubUID_)
			{
				UID = UID_;
				SubUID = SubUID_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref SubUID);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("SubUID", ref SubUID);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(SubUID);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("SubUID", SubUID);
			}
			public void Set(SUIDPair Obj_)
			{
				UID = Obj_.UID;
				SubUID = Obj_.SubUID;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(SubUID);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(SubUID, "SubUID");
			}
		}
		public class SServerNet : SProto
		{
			public SNamePort ServerBindNamePort = new SNamePort();
			public SServerNet()
			{
			}
			public SServerNet(SServerNet Obj_)
			{
				ServerBindNamePort = Obj_.ServerBindNamePort;
			}
			public SServerNet(SNamePort ServerBindNamePort_)
			{
				ServerBindNamePort = ServerBindNamePort_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ServerBindNamePort);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ServerBindNamePort", ref ServerBindNamePort);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ServerBindNamePort);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ServerBindNamePort", ServerBindNamePort);
			}
			public void Set(SServerNet Obj_)
			{
				ServerBindNamePort.Set(Obj_.ServerBindNamePort);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ServerBindNamePort);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ServerBindNamePort, "ServerBindNamePort");
			}
		}
		public class SMasterNet : SProto
		{
			public SRangeUID RangeUID = new SRangeUID();
			public TServerNets Servers = new TServerNets();
			public SMasterNet()
			{
			}
			public SMasterNet(SMasterNet Obj_)
			{
				RangeUID = Obj_.RangeUID;
				Servers = Obj_.Servers;
			}
			public SMasterNet(SRangeUID RangeUID_, TServerNets Servers_)
			{
				RangeUID = RangeUID_;
				Servers = Servers_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref RangeUID);
				Stream_.Pop(ref Servers);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("RangeUID", ref RangeUID);
				Value_.Pop("Servers", ref Servers);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(RangeUID);
				Stream_.Push(Servers);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("RangeUID", RangeUID);
				Value_.Push("Servers", Servers);
			}
			public void Set(SMasterNet Obj_)
			{
				RangeUID.Set(Obj_.RangeUID);
				Servers = Obj_.Servers;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(RangeUID) + "," + 
					SEnumChecker.GetStdName(Servers);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(RangeUID, "RangeUID") + "," + 
					SEnumChecker.GetMemberName(Servers, "Servers");
			}
		}
		public class SMaMasterOn : SMasterNet
		{
			public SNamePort ClientBindNamePortPub = new SNamePort();
			public TPort MasterBindPort = default(TPort);
			public SMaMasterOn()
			{
			}
			public SMaMasterOn(SMaMasterOn Obj_) : base(Obj_)
			{
				ClientBindNamePortPub = Obj_.ClientBindNamePortPub;
				MasterBindPort = Obj_.MasterBindPort;
			}
			public SMaMasterOn(SMasterNet Super_, SNamePort ClientBindNamePortPub_, TPort MasterBindPort_) : base(Super_)
			{
				ClientBindNamePortPub = ClientBindNamePortPub_;
				MasterBindPort = MasterBindPort_;
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
				Stream_.Pop(ref ClientBindNamePortPub);
				Stream_.Pop(ref MasterBindPort);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
				Value_.Pop("ClientBindNamePortPub", ref ClientBindNamePortPub);
				Value_.Pop("MasterBindPort", ref MasterBindPort);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
				Stream_.Push(ClientBindNamePortPub);
				Stream_.Push(MasterBindPort);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
				Value_.Push("ClientBindNamePortPub", ClientBindNamePortPub);
				Value_.Push("MasterBindPort", MasterBindPort);
			}
			public void Set(SMaMasterOn Obj_)
			{
				base.Set(Obj_);
				ClientBindNamePortPub.Set(Obj_.ClientBindNamePortPub);
				MasterBindPort = Obj_.MasterBindPort;
			}
			public override string StdName()
			{
				return 
					base.StdName() + "," + 
					SEnumChecker.GetStdName(ClientBindNamePortPub) + "," + 
					SEnumChecker.GetStdName(MasterBindPort);
			}
			public override string MemberName()
			{
				return 
					base.MemberName() + "," + 
					SEnumChecker.GetMemberName(ClientBindNamePortPub, "ClientBindNamePortPub") + "," + 
					SEnumChecker.GetMemberName(MasterBindPort, "MasterBindPort");
			}
		}
		public class SMaster : SMasterNet
		{
			public SNamePort MasterBindNamePort = new SNamePort();
			public SMaster()
			{
			}
			public SMaster(SMaster Obj_) : base(Obj_)
			{
				MasterBindNamePort = Obj_.MasterBindNamePort;
			}
			public SMaster(SMasterNet Super_, SNamePort MasterBindNamePort_) : base(Super_)
			{
				MasterBindNamePort = MasterBindNamePort_;
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
				Stream_.Pop(ref MasterBindNamePort);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
				Value_.Pop("MasterBindNamePort", ref MasterBindNamePort);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
				Stream_.Push(MasterBindNamePort);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
				Value_.Push("MasterBindNamePort", MasterBindNamePort);
			}
			public void Set(SMaster Obj_)
			{
				base.Set(Obj_);
				MasterBindNamePort.Set(Obj_.MasterBindNamePort);
			}
			public override string StdName()
			{
				return 
					base.StdName() + "," + 
					SEnumChecker.GetStdName(MasterBindNamePort);
			}
			public override string MemberName()
			{
				return 
					base.MemberName() + "," + 
					SEnumChecker.GetMemberName(MasterBindNamePort, "MasterBindNamePort");
			}
		}
		public class SAmMasterOn : SProto
		{
			public List<SMaster> OtherMasters = new List<SMaster>();
			public SAmMasterOn()
			{
			}
			public SAmMasterOn(SAmMasterOn Obj_)
			{
				OtherMasters = Obj_.OtherMasters;
			}
			public SAmMasterOn(List<SMaster> OtherMasters_)
			{
				OtherMasters = OtherMasters_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref OtherMasters);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("OtherMasters", ref OtherMasters);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(OtherMasters);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("OtherMasters", OtherMasters);
			}
			public void Set(SAmMasterOn Obj_)
			{
				OtherMasters = Obj_.OtherMasters;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(OtherMasters);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(OtherMasters, "OtherMasters");
			}
		}
		public class SMsMasterOn : SProto
		{
			public List<SMaster> OtherMasters = new List<SMaster>();
			public SMsMasterOn()
			{
			}
			public SMsMasterOn(SMsMasterOn Obj_)
			{
				OtherMasters = Obj_.OtherMasters;
			}
			public SMsMasterOn(List<SMaster> OtherMasters_)
			{
				OtherMasters = OtherMasters_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref OtherMasters);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("OtherMasters", ref OtherMasters);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(OtherMasters);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("OtherMasters", OtherMasters);
			}
			public void Set(SMsMasterOn Obj_)
			{
				OtherMasters = Obj_.OtherMasters;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(OtherMasters);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(OtherMasters, "OtherMasters");
			}
		}
		public class SAmOtherMasterOn : SProto
		{
			public SMaster Master = new SMaster();
			public SAmOtherMasterOn()
			{
			}
			public SAmOtherMasterOn(SAmOtherMasterOn Obj_)
			{
				Master = Obj_.Master;
			}
			public SAmOtherMasterOn(SMaster Master_)
			{
				Master = Master_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Master);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Master", ref Master);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Master);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Master", Master);
			}
			public void Set(SAmOtherMasterOn Obj_)
			{
				Master.Set(Obj_.Master);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Master);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Master, "Master");
			}
		}
		public class SMsOtherMasterOn : SMasterNet
		{
			public SMsOtherMasterOn()
			{
			}
			public SMsOtherMasterOn(SMsOtherMasterOn Obj_) : base(Obj_)
			{
			}
			public SMsOtherMasterOn(SMasterNet Super_) : base(Super_)
			{
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
			}
			public void Set(SMsOtherMasterOn Obj_)
			{
				base.Set(Obj_);
			}
			public override string StdName()
			{
				return 
					base.StdName();
			}
			public override string MemberName()
			{
				return 
					base.MemberName();
			}
		}
		public class SAmOtherMasterOff : SProto
		{
			public SRangeUID MasterRangeUID = new SRangeUID();
			public SAmOtherMasterOff()
			{
			}
			public SAmOtherMasterOff(SAmOtherMasterOff Obj_)
			{
				MasterRangeUID = Obj_.MasterRangeUID;
			}
			public SAmOtherMasterOff(SRangeUID MasterRangeUID_)
			{
				MasterRangeUID = MasterRangeUID_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref MasterRangeUID);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("MasterRangeUID", ref MasterRangeUID);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(MasterRangeUID);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("MasterRangeUID", MasterRangeUID);
			}
			public void Set(SAmOtherMasterOff Obj_)
			{
				MasterRangeUID.Set(Obj_.MasterRangeUID);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(MasterRangeUID);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(MasterRangeUID, "MasterRangeUID");
			}
		}
		public class SMsOtherMasterOff : SProto
		{
			public SRangeUID MasterRangeUID = new SRangeUID();
			public SMsOtherMasterOff()
			{
			}
			public SMsOtherMasterOff(SMsOtherMasterOff Obj_)
			{
				MasterRangeUID = Obj_.MasterRangeUID;
			}
			public SMsOtherMasterOff(SRangeUID MasterRangeUID_)
			{
				MasterRangeUID = MasterRangeUID_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref MasterRangeUID);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("MasterRangeUID", ref MasterRangeUID);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(MasterRangeUID);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("MasterRangeUID", MasterRangeUID);
			}
			public void Set(SMsOtherMasterOff Obj_)
			{
				MasterRangeUID.Set(Obj_.MasterRangeUID);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(MasterRangeUID);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(MasterRangeUID, "MasterRangeUID");
			}
		}
		public class SSmServerOn : SProto
		{
			public SNamePort ClientBindNamePortPub = new SNamePort();
			public TPort ServerBindPort = default(TPort);
			public SSmServerOn()
			{
			}
			public SSmServerOn(SSmServerOn Obj_)
			{
				ClientBindNamePortPub = Obj_.ClientBindNamePortPub;
				ServerBindPort = Obj_.ServerBindPort;
			}
			public SSmServerOn(SNamePort ClientBindNamePortPub_, TPort ServerBindPort_)
			{
				ClientBindNamePortPub = ClientBindNamePortPub_;
				ServerBindPort = ServerBindPort_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ClientBindNamePortPub);
				Stream_.Pop(ref ServerBindPort);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ClientBindNamePortPub", ref ClientBindNamePortPub);
				Value_.Pop("ServerBindPort", ref ServerBindPort);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ClientBindNamePortPub);
				Stream_.Push(ServerBindPort);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ClientBindNamePortPub", ClientBindNamePortPub);
				Value_.Push("ServerBindPort", ServerBindPort);
			}
			public void Set(SSmServerOn Obj_)
			{
				ClientBindNamePortPub.Set(Obj_.ClientBindNamePortPub);
				ServerBindPort = Obj_.ServerBindPort;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ClientBindNamePortPub) + "," + 
					SEnumChecker.GetStdName(ServerBindPort);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ClientBindNamePortPub, "ClientBindNamePortPub") + "," + 
					SEnumChecker.GetMemberName(ServerBindPort, "ServerBindPort");
			}
		}
		public class SMsServerOn : SProto
		{
			public SNamePort ServerBindNamePort = new SNamePort();
			public TServerNets OtherServers = new TServerNets();
			public TMasterNets OtherMasters = new TMasterNets();
			public SMsServerOn()
			{
			}
			public SMsServerOn(SMsServerOn Obj_)
			{
				ServerBindNamePort = Obj_.ServerBindNamePort;
				OtherServers = Obj_.OtherServers;
				OtherMasters = Obj_.OtherMasters;
			}
			public SMsServerOn(SNamePort ServerBindNamePort_, TServerNets OtherServers_, TMasterNets OtherMasters_)
			{
				ServerBindNamePort = ServerBindNamePort_;
				OtherServers = OtherServers_;
				OtherMasters = OtherMasters_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ServerBindNamePort);
				Stream_.Pop(ref OtherServers);
				Stream_.Pop(ref OtherMasters);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ServerBindNamePort", ref ServerBindNamePort);
				Value_.Pop("OtherServers", ref OtherServers);
				Value_.Pop("OtherMasters", ref OtherMasters);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ServerBindNamePort);
				Stream_.Push(OtherServers);
				Stream_.Push(OtherMasters);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ServerBindNamePort", ServerBindNamePort);
				Value_.Push("OtherServers", OtherServers);
				Value_.Push("OtherMasters", OtherMasters);
			}
			public void Set(SMsServerOn Obj_)
			{
				ServerBindNamePort.Set(Obj_.ServerBindNamePort);
				OtherServers = Obj_.OtherServers;
				OtherMasters = Obj_.OtherMasters;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ServerBindNamePort) + "," + 
					SEnumChecker.GetStdName(OtherServers) + "," + 
					SEnumChecker.GetStdName(OtherMasters);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ServerBindNamePort, "ServerBindNamePort") + "," + 
					SEnumChecker.GetMemberName(OtherServers, "OtherServers") + "," + 
					SEnumChecker.GetMemberName(OtherMasters, "OtherMasters");
			}
		}
		public class SMsOtherServerOn : SProto
		{
			public SServerNet Server = new SServerNet();
			public SMsOtherServerOn()
			{
			}
			public SMsOtherServerOn(SMsOtherServerOn Obj_)
			{
				Server = Obj_.Server;
			}
			public SMsOtherServerOn(SServerNet Server_)
			{
				Server = Server_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Server);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Server", ref Server);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Server);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Server", Server);
			}
			public void Set(SMsOtherServerOn Obj_)
			{
				Server.Set(Obj_.Server);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Server);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Server, "Server");
			}
		}
		public class SMaServerOn : SProto
		{
			public SNamePort ServerBindNamePort = new SNamePort();
			public SMaServerOn()
			{
			}
			public SMaServerOn(SMaServerOn Obj_)
			{
				ServerBindNamePort = Obj_.ServerBindNamePort;
			}
			public SMaServerOn(SNamePort ServerBindNamePort_)
			{
				ServerBindNamePort = ServerBindNamePort_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ServerBindNamePort);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ServerBindNamePort", ref ServerBindNamePort);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ServerBindNamePort);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ServerBindNamePort", ServerBindNamePort);
			}
			public void Set(SMaServerOn Obj_)
			{
				ServerBindNamePort.Set(Obj_.ServerBindNamePort);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ServerBindNamePort);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ServerBindNamePort, "ServerBindNamePort");
			}
		}
		public class SAmOtherMasterServerOn : SProto
		{
			public SRangeUID MasterRangeUID = new SRangeUID();
			public SServerNet Server = new SServerNet();
			public SAmOtherMasterServerOn()
			{
			}
			public SAmOtherMasterServerOn(SAmOtherMasterServerOn Obj_)
			{
				MasterRangeUID = Obj_.MasterRangeUID;
				Server = Obj_.Server;
			}
			public SAmOtherMasterServerOn(SRangeUID MasterRangeUID_, SServerNet Server_)
			{
				MasterRangeUID = MasterRangeUID_;
				Server = Server_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref MasterRangeUID);
				Stream_.Pop(ref Server);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("MasterRangeUID", ref MasterRangeUID);
				Value_.Pop("Server", ref Server);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(MasterRangeUID);
				Stream_.Push(Server);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("MasterRangeUID", MasterRangeUID);
				Value_.Push("Server", Server);
			}
			public void Set(SAmOtherMasterServerOn Obj_)
			{
				MasterRangeUID.Set(Obj_.MasterRangeUID);
				Server.Set(Obj_.Server);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(MasterRangeUID) + "," + 
					SEnumChecker.GetStdName(Server);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(MasterRangeUID, "MasterRangeUID") + "," + 
					SEnumChecker.GetMemberName(Server, "Server");
			}
		}
		public class SMsOtherMasterServerOn : SProto
		{
			public SRangeUID MasterRangeUID = new SRangeUID();
			public SServerNet Server = new SServerNet();
			public SMsOtherMasterServerOn()
			{
			}
			public SMsOtherMasterServerOn(SMsOtherMasterServerOn Obj_)
			{
				MasterRangeUID = Obj_.MasterRangeUID;
				Server = Obj_.Server;
			}
			public SMsOtherMasterServerOn(SRangeUID MasterRangeUID_, SServerNet Server_)
			{
				MasterRangeUID = MasterRangeUID_;
				Server = Server_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref MasterRangeUID);
				Stream_.Pop(ref Server);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("MasterRangeUID", ref MasterRangeUID);
				Value_.Pop("Server", ref Server);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(MasterRangeUID);
				Stream_.Push(Server);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("MasterRangeUID", MasterRangeUID);
				Value_.Push("Server", Server);
			}
			public void Set(SMsOtherMasterServerOn Obj_)
			{
				MasterRangeUID.Set(Obj_.MasterRangeUID);
				Server.Set(Obj_.Server);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(MasterRangeUID) + "," + 
					SEnumChecker.GetStdName(Server);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(MasterRangeUID, "MasterRangeUID") + "," + 
					SEnumChecker.GetMemberName(Server, "Server");
			}
		}
		public class SMsOtherServerOff : SProto
		{
			public SServerNet Server = new SServerNet();
			public SMsOtherServerOff()
			{
			}
			public SMsOtherServerOff(SMsOtherServerOff Obj_)
			{
				Server = Obj_.Server;
			}
			public SMsOtherServerOff(SServerNet Server_)
			{
				Server = Server_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Server);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Server", ref Server);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Server);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Server", Server);
			}
			public void Set(SMsOtherServerOff Obj_)
			{
				Server.Set(Obj_.Server);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Server);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Server, "Server");
			}
		}
		public class SMaServerOff : SProto
		{
			public SServerNet Server = new SServerNet();
			public SMaServerOff()
			{
			}
			public SMaServerOff(SMaServerOff Obj_)
			{
				Server = Obj_.Server;
			}
			public SMaServerOff(SServerNet Server_)
			{
				Server = Server_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Server);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Server", ref Server);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Server);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Server", Server);
			}
			public void Set(SMaServerOff Obj_)
			{
				Server.Set(Obj_.Server);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Server);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Server, "Server");
			}
		}
		public class SAmOtherMasterServerOff : SProto
		{
			public SRangeUID MasterRangeUID = new SRangeUID();
			public SServerNet Server = new SServerNet();
			public SAmOtherMasterServerOff()
			{
			}
			public SAmOtherMasterServerOff(SAmOtherMasterServerOff Obj_)
			{
				MasterRangeUID = Obj_.MasterRangeUID;
				Server = Obj_.Server;
			}
			public SAmOtherMasterServerOff(SRangeUID MasterRangeUID_, SServerNet Server_)
			{
				MasterRangeUID = MasterRangeUID_;
				Server = Server_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref MasterRangeUID);
				Stream_.Pop(ref Server);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("MasterRangeUID", ref MasterRangeUID);
				Value_.Pop("Server", ref Server);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(MasterRangeUID);
				Stream_.Push(Server);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("MasterRangeUID", MasterRangeUID);
				Value_.Push("Server", Server);
			}
			public void Set(SAmOtherMasterServerOff Obj_)
			{
				MasterRangeUID.Set(Obj_.MasterRangeUID);
				Server.Set(Obj_.Server);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(MasterRangeUID) + "," + 
					SEnumChecker.GetStdName(Server);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(MasterRangeUID, "MasterRangeUID") + "," + 
					SEnumChecker.GetMemberName(Server, "Server");
			}
		}
		public class SMsOtherMasterServerOff : SProto
		{
			public SRangeUID MasterRangeUID = new SRangeUID();
			public SServerNet Server = new SServerNet();
			public SMsOtherMasterServerOff()
			{
			}
			public SMsOtherMasterServerOff(SMsOtherMasterServerOff Obj_)
			{
				MasterRangeUID = Obj_.MasterRangeUID;
				Server = Obj_.Server;
			}
			public SMsOtherMasterServerOff(SRangeUID MasterRangeUID_, SServerNet Server_)
			{
				MasterRangeUID = MasterRangeUID_;
				Server = Server_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref MasterRangeUID);
				Stream_.Pop(ref Server);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("MasterRangeUID", ref MasterRangeUID);
				Value_.Pop("Server", ref Server);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(MasterRangeUID);
				Stream_.Push(Server);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("MasterRangeUID", MasterRangeUID);
				Value_.Push("Server", Server);
			}
			public void Set(SMsOtherMasterServerOff Obj_)
			{
				MasterRangeUID.Set(Obj_.MasterRangeUID);
				Server.Set(Obj_.Server);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(MasterRangeUID) + "," + 
					SEnumChecker.GetStdName(Server);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(MasterRangeUID, "MasterRangeUID") + "," + 
					SEnumChecker.GetMemberName(Server, "Server");
			}
		}
		public class SCaCreate : SProto
		{
			public TID ID = string.Empty;
			public TNick Nick = string.Empty;
			public TState State = default(TState);
			public SCaCreate()
			{
			}
			public SCaCreate(SCaCreate Obj_)
			{
				ID = Obj_.ID;
				Nick = Obj_.Nick;
				State = Obj_.State;
			}
			public SCaCreate(TID ID_, TNick Nick_, TState State_)
			{
				ID = ID_;
				Nick = Nick_;
				State = State_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ID);
				Stream_.Pop(ref Nick);
				Stream_.Pop(ref State);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ID", ref ID);
				Value_.Pop("Nick", ref Nick);
				Value_.Pop("State", ref State);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ID);
				Stream_.Push(Nick);
				Stream_.Push(State);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ID", ID);
				Value_.Push("Nick", Nick);
				Value_.Push("State", State);
			}
			public void Set(SCaCreate Obj_)
			{
				ID = Obj_.ID;
				Nick = Obj_.Nick;
				State = Obj_.State;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ID) + "," + 
					SEnumChecker.GetStdName(Nick) + "," + 
					SEnumChecker.GetStdName(State);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ID, "ID") + "," + 
					SEnumChecker.GetMemberName(Nick, "Nick") + "," + 
					SEnumChecker.GetMemberName(State, "State");
			}
		}
		public class SAccount : SProto
		{
			public TID ID = string.Empty;
			public TNick Nick = string.Empty;
			public SAccount()
			{
			}
			public SAccount(SAccount Obj_)
			{
				ID = Obj_.ID;
				Nick = Obj_.Nick;
			}
			public SAccount(TID ID_, TNick Nick_)
			{
				ID = ID_;
				Nick = Nick_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ID);
				Stream_.Pop(ref Nick);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ID", ref ID);
				Value_.Pop("Nick", ref Nick);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ID);
				Stream_.Push(Nick);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ID", ID);
				Value_.Push("Nick", Nick);
			}
			public void Set(SAccount Obj_)
			{
				ID = Obj_.ID;
				Nick = Obj_.Nick;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ID) + "," + 
					SEnumChecker.GetStdName(Nick);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ID, "ID") + "," + 
					SEnumChecker.GetMemberName(Nick, "Nick");
			}
		}
		public class SAmCreate : SProto
		{
			public TUID UID = default(TUID);
			public SAccount Account = new SAccount();
			public TState State = default(TState);
			public SKey UserKey = new SKey();
			public SAmCreate()
			{
			}
			public SAmCreate(SAmCreate Obj_)
			{
				UID = Obj_.UID;
				Account = Obj_.Account;
				State = Obj_.State;
				UserKey = Obj_.UserKey;
			}
			public SAmCreate(TUID UID_, SAccount Account_, TState State_, SKey UserKey_)
			{
				UID = UID_;
				Account = Account_;
				State = State_;
				UserKey = UserKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref Account);
				Stream_.Pop(ref State);
				Stream_.Pop(ref UserKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("Account", ref Account);
				Value_.Pop("State", ref State);
				Value_.Pop("UserKey", ref UserKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(Account);
				Stream_.Push(State);
				Stream_.Push(UserKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("Account", Account);
				Value_.Push("State", State);
				Value_.Push("UserKey", UserKey);
			}
			public void Set(SAmCreate Obj_)
			{
				UID = Obj_.UID;
				Account.Set(Obj_.Account);
				State = Obj_.State;
				UserKey.Set(Obj_.UserKey);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(Account) + "," + 
					SEnumChecker.GetStdName(State) + "," + 
					SEnumChecker.GetStdName(UserKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(Account, "Account") + "," + 
					SEnumChecker.GetMemberName(State, "State") + "," + 
					SEnumChecker.GetMemberName(UserKey, "UserKey");
			}
		}
		public class SMaCreate : SProto
		{
			public SKey UserKey = new SKey();
			public TUID UID = default(TUID);
			public SMaCreate()
			{
			}
			public SMaCreate(SMaCreate Obj_)
			{
				UserKey = Obj_.UserKey;
				UID = Obj_.UID;
			}
			public SMaCreate(SKey UserKey_, TUID UID_)
			{
				UserKey = UserKey_;
				UID = UID_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UserKey);
				Stream_.Pop(ref UID);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UserKey", ref UserKey);
				Value_.Pop("UID", ref UID);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UserKey);
				Stream_.Push(UID);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UserKey", UserKey);
				Value_.Push("UID", UID);
			}
			public void Set(SMaCreate Obj_)
			{
				UserKey.Set(Obj_.UserKey);
				UID = Obj_.UID;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UserKey) + "," + 
					SEnumChecker.GetStdName(UID);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UserKey, "UserKey") + "," + 
					SEnumChecker.GetMemberName(UID, "UID");
			}
		}
		public class SMaCreateFail : SProto
		{
			public SKey UserKey = new SKey();
			public EGameRet GameRet = default(EGameRet);
			public SMaCreateFail()
			{
			}
			public SMaCreateFail(SMaCreateFail Obj_)
			{
				UserKey = Obj_.UserKey;
				GameRet = Obj_.GameRet;
			}
			public SMaCreateFail(SKey UserKey_, EGameRet GameRet_)
			{
				UserKey = UserKey_;
				GameRet = GameRet_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UserKey);
				Stream_.Pop(ref GameRet);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UserKey", ref UserKey);
				Value_.Pop("GameRet", ref GameRet);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UserKey);
				Stream_.Push(GameRet);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UserKey", UserKey);
				Value_.Push("GameRet", GameRet);
			}
			public void Set(SMaCreateFail Obj_)
			{
				UserKey.Set(Obj_.UserKey);
				GameRet = Obj_.GameRet;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UserKey) + "," + 
					"rso.game.EGameRet";
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UserKey, "UserKey") + "," + 
					SEnumChecker.GetMemberName(GameRet, "GameRet");
			}
		}
		public class SCaLogin : SProto
		{
			public TUID UID = default(TUID);
			public TID ID = string.Empty;
			public SCaLogin()
			{
			}
			public SCaLogin(SCaLogin Obj_)
			{
				UID = Obj_.UID;
				ID = Obj_.ID;
			}
			public SCaLogin(TUID UID_, TID ID_)
			{
				UID = UID_;
				ID = ID_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref ID);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("ID", ref ID);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(ID);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("ID", ID);
			}
			public void Set(SCaLogin Obj_)
			{
				UID = Obj_.UID;
				ID = Obj_.ID;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(ID);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(ID, "ID");
			}
		}
		public class SAcLogin : SProto
		{
			public TUID UID = default(TUID);
			public SNamePort ClientBindNamePortPubToMaster = new SNamePort();
			public SAcLogin()
			{
			}
			public SAcLogin(SAcLogin Obj_)
			{
				UID = Obj_.UID;
				ClientBindNamePortPubToMaster = Obj_.ClientBindNamePortPubToMaster;
			}
			public SAcLogin(TUID UID_, SNamePort ClientBindNamePortPubToMaster_)
			{
				UID = UID_;
				ClientBindNamePortPubToMaster = ClientBindNamePortPubToMaster_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref ClientBindNamePortPubToMaster);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("ClientBindNamePortPubToMaster", ref ClientBindNamePortPubToMaster);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(ClientBindNamePortPubToMaster);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("ClientBindNamePortPubToMaster", ClientBindNamePortPubToMaster);
			}
			public void Set(SAcLogin Obj_)
			{
				UID = Obj_.UID;
				ClientBindNamePortPubToMaster.Set(Obj_.ClientBindNamePortPubToMaster);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(ClientBindNamePortPubToMaster);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(ClientBindNamePortPubToMaster, "ClientBindNamePortPubToMaster");
			}
		}
		public class SAcLoginFail : SProto
		{
			public EGameRet GameRet = default(EGameRet);
			public SAcLoginFail()
			{
			}
			public SAcLoginFail(SAcLoginFail Obj_)
			{
				GameRet = Obj_.GameRet;
			}
			public SAcLoginFail(EGameRet GameRet_)
			{
				GameRet = GameRet_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref GameRet);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("GameRet", ref GameRet);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(GameRet);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("GameRet", GameRet);
			}
			public void Set(SAcLoginFail Obj_)
			{
				GameRet = Obj_.GameRet;
			}
			public override string StdName()
			{
				return 
					"rso.game.EGameRet";
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(GameRet, "GameRet");
			}
		}
		public class SSmChangeNick : SProto
		{
			public TUID UID = default(TUID);
			public TNick Nick = string.Empty;
			public SKey ClientKey = new SKey();
			public SSmChangeNick()
			{
			}
			public SSmChangeNick(SSmChangeNick Obj_)
			{
				UID = Obj_.UID;
				Nick = Obj_.Nick;
				ClientKey = Obj_.ClientKey;
			}
			public SSmChangeNick(TUID UID_, TNick Nick_, SKey ClientKey_)
			{
				UID = UID_;
				Nick = Nick_;
				ClientKey = ClientKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref Nick);
				Stream_.Pop(ref ClientKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("Nick", ref Nick);
				Value_.Pop("ClientKey", ref ClientKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(Nick);
				Stream_.Push(ClientKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("Nick", Nick);
				Value_.Push("ClientKey", ClientKey);
			}
			public void Set(SSmChangeNick Obj_)
			{
				UID = Obj_.UID;
				Nick = Obj_.Nick;
				ClientKey.Set(Obj_.ClientKey);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(Nick) + "," + 
					SEnumChecker.GetStdName(ClientKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(Nick, "Nick") + "," + 
					SEnumChecker.GetMemberName(ClientKey, "ClientKey");
			}
		}
		public class SMaChangeNick : SSmChangeNick
		{
			public SKey ServerKey = new SKey();
			public SMaChangeNick()
			{
			}
			public SMaChangeNick(SMaChangeNick Obj_) : base(Obj_)
			{
				ServerKey = Obj_.ServerKey;
			}
			public SMaChangeNick(SSmChangeNick Super_, SKey ServerKey_) : base(Super_)
			{
				ServerKey = ServerKey_;
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
				Stream_.Pop(ref ServerKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
				Value_.Pop("ServerKey", ref ServerKey);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
				Stream_.Push(ServerKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
				Value_.Push("ServerKey", ServerKey);
			}
			public void Set(SMaChangeNick Obj_)
			{
				base.Set(Obj_);
				ServerKey.Set(Obj_.ServerKey);
			}
			public override string StdName()
			{
				return 
					base.StdName() + "," + 
					SEnumChecker.GetStdName(ServerKey);
			}
			public override string MemberName()
			{
				return 
					base.MemberName() + "," + 
					SEnumChecker.GetMemberName(ServerKey, "ServerKey");
			}
		}
		public class SMsChangeNick : SProto
		{
			public TNick Nick = string.Empty;
			public SKey ClientKey = new SKey();
			public TUID UID = default(TUID);
			public SMsChangeNick()
			{
			}
			public SMsChangeNick(SMsChangeNick Obj_)
			{
				Nick = Obj_.Nick;
				ClientKey = Obj_.ClientKey;
				UID = Obj_.UID;
			}
			public SMsChangeNick(TNick Nick_, SKey ClientKey_, TUID UID_)
			{
				Nick = Nick_;
				ClientKey = ClientKey_;
				UID = UID_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Nick);
				Stream_.Pop(ref ClientKey);
				Stream_.Pop(ref UID);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Nick", ref Nick);
				Value_.Pop("ClientKey", ref ClientKey);
				Value_.Pop("UID", ref UID);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Nick);
				Stream_.Push(ClientKey);
				Stream_.Push(UID);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Nick", Nick);
				Value_.Push("ClientKey", ClientKey);
				Value_.Push("UID", UID);
			}
			public void Set(SMsChangeNick Obj_)
			{
				Nick = Obj_.Nick;
				ClientKey.Set(Obj_.ClientKey);
				UID = Obj_.UID;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Nick) + "," + 
					SEnumChecker.GetStdName(ClientKey) + "," + 
					SEnumChecker.GetStdName(UID);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Nick, "Nick") + "," + 
					SEnumChecker.GetMemberName(ClientKey, "ClientKey") + "," + 
					SEnumChecker.GetMemberName(UID, "UID");
			}
		}
		public class SMsChangeNickFail : SProto
		{
			public EGameRet GameRet = default(EGameRet);
			public SKey ClientKey = new SKey();
			public TUID UID = default(TUID);
			public SMsChangeNickFail()
			{
			}
			public SMsChangeNickFail(SMsChangeNickFail Obj_)
			{
				GameRet = Obj_.GameRet;
				ClientKey = Obj_.ClientKey;
				UID = Obj_.UID;
			}
			public SMsChangeNickFail(EGameRet GameRet_, SKey ClientKey_, TUID UID_)
			{
				GameRet = GameRet_;
				ClientKey = ClientKey_;
				UID = UID_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref GameRet);
				Stream_.Pop(ref ClientKey);
				Stream_.Pop(ref UID);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("GameRet", ref GameRet);
				Value_.Pop("ClientKey", ref ClientKey);
				Value_.Pop("UID", ref UID);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(GameRet);
				Stream_.Push(ClientKey);
				Stream_.Push(UID);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("GameRet", GameRet);
				Value_.Push("ClientKey", ClientKey);
				Value_.Push("UID", UID);
			}
			public void Set(SMsChangeNickFail Obj_)
			{
				GameRet = Obj_.GameRet;
				ClientKey.Set(Obj_.ClientKey);
				UID = Obj_.UID;
			}
			public override string StdName()
			{
				return 
					"rso.game.EGameRet" + "," + 
					SEnumChecker.GetStdName(ClientKey) + "," + 
					SEnumChecker.GetStdName(UID);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(GameRet, "GameRet") + "," + 
					SEnumChecker.GetMemberName(ClientKey, "ClientKey") + "," + 
					SEnumChecker.GetMemberName(UID, "UID");
			}
		}
		public class SAmChangeNick : SMsChangeNick
		{
			public SKey ServerKey = new SKey();
			public SAmChangeNick()
			{
			}
			public SAmChangeNick(SAmChangeNick Obj_) : base(Obj_)
			{
				ServerKey = Obj_.ServerKey;
			}
			public SAmChangeNick(SMsChangeNick Super_, SKey ServerKey_) : base(Super_)
			{
				ServerKey = ServerKey_;
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
				Stream_.Pop(ref ServerKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
				Value_.Pop("ServerKey", ref ServerKey);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
				Stream_.Push(ServerKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
				Value_.Push("ServerKey", ServerKey);
			}
			public void Set(SAmChangeNick Obj_)
			{
				base.Set(Obj_);
				ServerKey.Set(Obj_.ServerKey);
			}
			public override string StdName()
			{
				return 
					base.StdName() + "," + 
					SEnumChecker.GetStdName(ServerKey);
			}
			public override string MemberName()
			{
				return 
					base.MemberName() + "," + 
					SEnumChecker.GetMemberName(ServerKey, "ServerKey");
			}
		}
		public class SAmChangeNickFail : SMsChangeNickFail
		{
			public SKey ServerKey = new SKey();
			public SAmChangeNickFail()
			{
			}
			public SAmChangeNickFail(SAmChangeNickFail Obj_) : base(Obj_)
			{
				ServerKey = Obj_.ServerKey;
			}
			public SAmChangeNickFail(SMsChangeNickFail Super_, SKey ServerKey_) : base(Super_)
			{
				ServerKey = ServerKey_;
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
				Stream_.Pop(ref ServerKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
				Value_.Pop("ServerKey", ref ServerKey);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
				Stream_.Push(ServerKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
				Value_.Push("ServerKey", ServerKey);
			}
			public void Set(SAmChangeNickFail Obj_)
			{
				base.Set(Obj_);
				ServerKey.Set(Obj_.ServerKey);
			}
			public override string StdName()
			{
				return 
					base.StdName() + "," + 
					SEnumChecker.GetStdName(ServerKey);
			}
			public override string MemberName()
			{
				return 
					base.MemberName() + "," + 
					SEnumChecker.GetMemberName(ServerKey, "ServerKey");
			}
		}
		public class SCaCheck : SProto
		{
			public TID ID = string.Empty;
			public SCaCheck()
			{
			}
			public SCaCheck(SCaCheck Obj_)
			{
				ID = Obj_.ID;
			}
			public SCaCheck(TID ID_)
			{
				ID = ID_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ID);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ID", ref ID);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ID);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ID", ID);
			}
			public void Set(SCaCheck Obj_)
			{
				ID = Obj_.ID;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ID);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ID, "ID");
			}
		}
		public class SAmCheck : SProto
		{
			public TUID UID = default(TUID);
			public SKey UserKey = new SKey();
			public SAmCheck()
			{
			}
			public SAmCheck(SAmCheck Obj_)
			{
				UID = Obj_.UID;
				UserKey = Obj_.UserKey;
			}
			public SAmCheck(TUID UID_, SKey UserKey_)
			{
				UID = UID_;
				UserKey = UserKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref UserKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("UserKey", ref UserKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(UserKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("UserKey", UserKey);
			}
			public void Set(SAmCheck Obj_)
			{
				UID = Obj_.UID;
				UserKey.Set(Obj_.UserKey);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(UserKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(UserKey, "UserKey");
			}
		}
		public class SMsCheck : SProto
		{
			public TUID UID = default(TUID);
			public SKey UserKey = new SKey();
			public SKey AuthKey = new SKey();
			public TSessionCode SessionCode = default(TSessionCode);
			public SMsCheck()
			{
			}
			public SMsCheck(SMsCheck Obj_)
			{
				UID = Obj_.UID;
				UserKey = Obj_.UserKey;
				AuthKey = Obj_.AuthKey;
				SessionCode = Obj_.SessionCode;
			}
			public SMsCheck(TUID UID_, SKey UserKey_, SKey AuthKey_, TSessionCode SessionCode_)
			{
				UID = UID_;
				UserKey = UserKey_;
				AuthKey = AuthKey_;
				SessionCode = SessionCode_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref UserKey);
				Stream_.Pop(ref AuthKey);
				Stream_.Pop(ref SessionCode);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("UserKey", ref UserKey);
				Value_.Pop("AuthKey", ref AuthKey);
				Value_.Pop("SessionCode", ref SessionCode);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(UserKey);
				Stream_.Push(AuthKey);
				Stream_.Push(SessionCode);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("UserKey", UserKey);
				Value_.Push("AuthKey", AuthKey);
				Value_.Push("SessionCode", SessionCode);
			}
			public void Set(SMsCheck Obj_)
			{
				UID = Obj_.UID;
				UserKey.Set(Obj_.UserKey);
				AuthKey.Set(Obj_.AuthKey);
				SessionCode = Obj_.SessionCode;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(UserKey) + "," + 
					SEnumChecker.GetStdName(AuthKey) + "," + 
					SEnumChecker.GetStdName(SessionCode);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(UserKey, "UserKey") + "," + 
					SEnumChecker.GetMemberName(AuthKey, "AuthKey") + "," + 
					SEnumChecker.GetMemberName(SessionCode, "SessionCode");
			}
		}
		public class SSmCheck : SProto
		{
			public TUID UID = default(TUID);
			public SKey UserKey = new SKey();
			public SKey AuthKey = new SKey();
			public CStream Stream = new CStream();
			public SSmCheck()
			{
			}
			public SSmCheck(SSmCheck Obj_)
			{
				UID = Obj_.UID;
				UserKey = Obj_.UserKey;
				AuthKey = Obj_.AuthKey;
				Stream = Obj_.Stream;
			}
			public SSmCheck(TUID UID_, SKey UserKey_, SKey AuthKey_, CStream Stream_)
			{
				UID = UID_;
				UserKey = UserKey_;
				AuthKey = AuthKey_;
				Stream = Stream_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref UserKey);
				Stream_.Pop(ref AuthKey);
				Stream_.Pop(ref Stream);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("UserKey", ref UserKey);
				Value_.Pop("AuthKey", ref AuthKey);
				Value_.Pop("Stream", ref Stream);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(UserKey);
				Stream_.Push(AuthKey);
				Stream_.Push(Stream);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("UserKey", UserKey);
				Value_.Push("AuthKey", AuthKey);
				Value_.Push("Stream", Stream);
			}
			public void Set(SSmCheck Obj_)
			{
				UID = Obj_.UID;
				UserKey.Set(Obj_.UserKey);
				AuthKey.Set(Obj_.AuthKey);
				Stream.Set(Obj_.Stream);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(UserKey) + "," + 
					SEnumChecker.GetStdName(AuthKey) + "," + 
					SEnumChecker.GetStdName(Stream);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(UserKey, "UserKey") + "," + 
					SEnumChecker.GetMemberName(AuthKey, "AuthKey") + "," + 
					SEnumChecker.GetMemberName(Stream, "Stream");
			}
		}
		public class SMaCheck : SProto
		{
			public TUID UID = default(TUID);
			public SKey UserKey = new SKey();
			public CStream Stream = new CStream();
			public SMaCheck()
			{
			}
			public SMaCheck(SMaCheck Obj_)
			{
				UID = Obj_.UID;
				UserKey = Obj_.UserKey;
				Stream = Obj_.Stream;
			}
			public SMaCheck(TUID UID_, SKey UserKey_, CStream Stream_)
			{
				UID = UID_;
				UserKey = UserKey_;
				Stream = Stream_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref UserKey);
				Stream_.Pop(ref Stream);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("UserKey", ref UserKey);
				Value_.Pop("Stream", ref Stream);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(UserKey);
				Stream_.Push(Stream);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("UserKey", UserKey);
				Value_.Push("Stream", Stream);
			}
			public void Set(SMaCheck Obj_)
			{
				UID = Obj_.UID;
				UserKey.Set(Obj_.UserKey);
				Stream.Set(Obj_.Stream);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(UserKey) + "," + 
					SEnumChecker.GetStdName(Stream);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(UserKey, "UserKey") + "," + 
					SEnumChecker.GetMemberName(Stream, "Stream");
			}
		}
		public class SMaCheckFail : SProto
		{
			public SKey UserKey = new SKey();
			public EGameRet GameRet = default(EGameRet);
			public SMaCheckFail()
			{
			}
			public SMaCheckFail(SMaCheckFail Obj_)
			{
				UserKey = Obj_.UserKey;
				GameRet = Obj_.GameRet;
			}
			public SMaCheckFail(SKey UserKey_, EGameRet GameRet_)
			{
				UserKey = UserKey_;
				GameRet = GameRet_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UserKey);
				Stream_.Pop(ref GameRet);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UserKey", ref UserKey);
				Value_.Pop("GameRet", ref GameRet);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UserKey);
				Stream_.Push(GameRet);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UserKey", UserKey);
				Value_.Push("GameRet", GameRet);
			}
			public void Set(SMaCheckFail Obj_)
			{
				UserKey.Set(Obj_.UserKey);
				GameRet = Obj_.GameRet;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UserKey) + "," + 
					"rso.game.EGameRet";
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UserKey, "UserKey") + "," + 
					SEnumChecker.GetMemberName(GameRet, "GameRet");
			}
		}
		public class SAcCheck : SProto
		{
			public TUID UID = default(TUID);
			public CStream Stream = new CStream();
			public SAcCheck()
			{
			}
			public SAcCheck(SAcCheck Obj_)
			{
				UID = Obj_.UID;
				Stream = Obj_.Stream;
			}
			public SAcCheck(TUID UID_, CStream Stream_)
			{
				UID = UID_;
				Stream = Stream_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref Stream);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("Stream", ref Stream);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(Stream);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("Stream", Stream);
			}
			public void Set(SAcCheck Obj_)
			{
				UID = Obj_.UID;
				Stream.Set(Obj_.Stream);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(Stream);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(Stream, "Stream");
			}
		}
		public class SAcCheckFail : SProto
		{
			public EGameRet GameRet = default(EGameRet);
			public SAcCheckFail()
			{
			}
			public SAcCheckFail(SAcCheckFail Obj_)
			{
				GameRet = Obj_.GameRet;
			}
			public SAcCheckFail(EGameRet GameRet_)
			{
				GameRet = GameRet_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref GameRet);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("GameRet", ref GameRet);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(GameRet);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("GameRet", GameRet);
			}
			public void Set(SAcCheckFail Obj_)
			{
				GameRet = Obj_.GameRet;
			}
			public override string StdName()
			{
				return 
					"rso.game.EGameRet";
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(GameRet, "GameRet");
			}
		}
		public class SCmLogin : SProto
		{
			public TUID UID = default(TUID);
			public TID ID = string.Empty;
			public TUID SubUID = default(TUID);
			public SCmLogin()
			{
			}
			public SCmLogin(SCmLogin Obj_)
			{
				UID = Obj_.UID;
				ID = Obj_.ID;
				SubUID = Obj_.SubUID;
			}
			public SCmLogin(TUID UID_, TID ID_, TUID SubUID_)
			{
				UID = UID_;
				ID = ID_;
				SubUID = SubUID_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref ID);
				Stream_.Pop(ref SubUID);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("ID", ref ID);
				Value_.Pop("SubUID", ref SubUID);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(ID);
				Stream_.Push(SubUID);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("ID", ID);
				Value_.Push("SubUID", SubUID);
			}
			public void Set(SCmLogin Obj_)
			{
				UID = Obj_.UID;
				ID = Obj_.ID;
				SubUID = Obj_.SubUID;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(ID) + "," + 
					SEnumChecker.GetStdName(SubUID);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(ID, "ID") + "," + 
					SEnumChecker.GetMemberName(SubUID, "SubUID");
			}
		}
		public class SFriendDB : SProto
		{
			public TNick Nick = string.Empty;
			public EFriendState FriendState = default(EFriendState);
			public SFriendDB()
			{
			}
			public SFriendDB(SFriendDB Obj_)
			{
				Nick = Obj_.Nick;
				FriendState = Obj_.FriendState;
			}
			public SFriendDB(TNick Nick_, EFriendState FriendState_)
			{
				Nick = Nick_;
				FriendState = FriendState_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Nick);
				Stream_.Pop(ref FriendState);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Nick", ref Nick);
				Value_.Pop("FriendState", ref FriendState);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Nick);
				Stream_.Push(FriendState);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Nick", Nick);
				Value_.Push("FriendState", FriendState);
			}
			public void Set(SFriendDB Obj_)
			{
				Nick = Obj_.Nick;
				FriendState = Obj_.FriendState;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Nick) + "," + 
					"rso.game.EFriendState";
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Nick, "Nick") + "," + 
					SEnumChecker.GetMemberName(FriendState, "FriendState");
			}
		}
		public class SFriendInfo : SProto
		{
			public SNamePort ServerBindNamePort = new SNamePort();
			public TState State = default(TState);
			public SFriendInfo()
			{
			}
			public SFriendInfo(SFriendInfo Obj_)
			{
				ServerBindNamePort = Obj_.ServerBindNamePort;
				State = Obj_.State;
			}
			public SFriendInfo(SNamePort ServerBindNamePort_, TState State_)
			{
				ServerBindNamePort = ServerBindNamePort_;
				State = State_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ServerBindNamePort);
				Stream_.Pop(ref State);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ServerBindNamePort", ref ServerBindNamePort);
				Value_.Pop("State", ref State);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ServerBindNamePort);
				Stream_.Push(State);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ServerBindNamePort", ServerBindNamePort);
				Value_.Push("State", State);
			}
			public void Set(SFriendInfo Obj_)
			{
				ServerBindNamePort.Set(Obj_.ServerBindNamePort);
				State = Obj_.State;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ServerBindNamePort) + "," + 
					SEnumChecker.GetStdName(State);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ServerBindNamePort, "ServerBindNamePort") + "," + 
					SEnumChecker.GetMemberName(State, "State");
			}
		}
		public class SFriend : SFriendDB
		{
			public SFriendInfo Info = new SFriendInfo();
			public SFriend()
			{
			}
			public SFriend(SFriend Obj_) : base(Obj_)
			{
				Info = Obj_.Info;
			}
			public SFriend(SFriendDB Super_, SFriendInfo Info_) : base(Super_)
			{
				Info = Info_;
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
				Stream_.Pop(ref Info);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
				Value_.Pop("Info", ref Info);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
				Stream_.Push(Info);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
				Value_.Push("Info", Info);
			}
			public void Set(SFriend Obj_)
			{
				base.Set(Obj_);
				Info.Set(Obj_.Info);
			}
			public override string StdName()
			{
				return 
					base.StdName() + "," + 
					SEnumChecker.GetStdName(Info);
			}
			public override string MemberName()
			{
				return 
					base.MemberName() + "," + 
					SEnumChecker.GetMemberName(Info, "Info");
			}
		}
		public class SMsLogin : SProto
		{
			public TUID UID = default(TUID);
			public SAccount Account = new SAccount();
			public TUID SubUID = default(TUID);
			public TFriends Friends = new TFriends();
			public SKey UserKey = new SKey();
			public TSessionCode SessionCode = default(TSessionCode);
			public SMsLogin()
			{
			}
			public SMsLogin(SMsLogin Obj_)
			{
				UID = Obj_.UID;
				Account = Obj_.Account;
				SubUID = Obj_.SubUID;
				Friends = Obj_.Friends;
				UserKey = Obj_.UserKey;
				SessionCode = Obj_.SessionCode;
			}
			public SMsLogin(TUID UID_, SAccount Account_, TUID SubUID_, TFriends Friends_, SKey UserKey_, TSessionCode SessionCode_)
			{
				UID = UID_;
				Account = Account_;
				SubUID = SubUID_;
				Friends = Friends_;
				UserKey = UserKey_;
				SessionCode = SessionCode_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref Account);
				Stream_.Pop(ref SubUID);
				Stream_.Pop(ref Friends);
				Stream_.Pop(ref UserKey);
				Stream_.Pop(ref SessionCode);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("Account", ref Account);
				Value_.Pop("SubUID", ref SubUID);
				Value_.Pop("Friends", ref Friends);
				Value_.Pop("UserKey", ref UserKey);
				Value_.Pop("SessionCode", ref SessionCode);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(Account);
				Stream_.Push(SubUID);
				Stream_.Push(Friends);
				Stream_.Push(UserKey);
				Stream_.Push(SessionCode);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("Account", Account);
				Value_.Push("SubUID", SubUID);
				Value_.Push("Friends", Friends);
				Value_.Push("UserKey", UserKey);
				Value_.Push("SessionCode", SessionCode);
			}
			public void Set(SMsLogin Obj_)
			{
				UID = Obj_.UID;
				Account.Set(Obj_.Account);
				SubUID = Obj_.SubUID;
				Friends = Obj_.Friends;
				UserKey.Set(Obj_.UserKey);
				SessionCode = Obj_.SessionCode;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(Account) + "," + 
					SEnumChecker.GetStdName(SubUID) + "," + 
					SEnumChecker.GetStdName(Friends) + "," + 
					SEnumChecker.GetStdName(UserKey) + "," + 
					SEnumChecker.GetStdName(SessionCode);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(Account, "Account") + "," + 
					SEnumChecker.GetMemberName(SubUID, "SubUID") + "," + 
					SEnumChecker.GetMemberName(Friends, "Friends") + "," + 
					SEnumChecker.GetMemberName(UserKey, "UserKey") + "," + 
					SEnumChecker.GetMemberName(SessionCode, "SessionCode");
			}
		}
		public class SSmLogin : SProto
		{
			public SKey UserKey = new SKey();
			public TSessionCode SessionCode = default(TSessionCode);
			public SSmLogin()
			{
			}
			public SSmLogin(SSmLogin Obj_)
			{
				UserKey = Obj_.UserKey;
				SessionCode = Obj_.SessionCode;
			}
			public SSmLogin(SKey UserKey_, TSessionCode SessionCode_)
			{
				UserKey = UserKey_;
				SessionCode = SessionCode_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UserKey);
				Stream_.Pop(ref SessionCode);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UserKey", ref UserKey);
				Value_.Pop("SessionCode", ref SessionCode);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UserKey);
				Stream_.Push(SessionCode);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UserKey", UserKey);
				Value_.Push("SessionCode", SessionCode);
			}
			public void Set(SSmLogin Obj_)
			{
				UserKey.Set(Obj_.UserKey);
				SessionCode = Obj_.SessionCode;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UserKey) + "," + 
					SEnumChecker.GetStdName(SessionCode);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UserKey, "UserKey") + "," + 
					SEnumChecker.GetMemberName(SessionCode, "SessionCode");
			}
		}
		public class SSmSessionEnd : SProto
		{
			public TUID UID = default(TUID);
			public TSessionCode SessionCode = default(TSessionCode);
			public SSmSessionEnd()
			{
			}
			public SSmSessionEnd(SSmSessionEnd Obj_)
			{
				UID = Obj_.UID;
				SessionCode = Obj_.SessionCode;
			}
			public SSmSessionEnd(TUID UID_, TSessionCode SessionCode_)
			{
				UID = UID_;
				SessionCode = SessionCode_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref SessionCode);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("SessionCode", ref SessionCode);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(SessionCode);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("SessionCode", SessionCode);
			}
			public void Set(SSmSessionEnd Obj_)
			{
				UID = Obj_.UID;
				SessionCode = Obj_.SessionCode;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(SessionCode);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(SessionCode, "SessionCode");
			}
		}
		public class SSmSetOpened : SProto
		{
			public Boolean Opened = default(Boolean);
			public SSmSetOpened()
			{
			}
			public SSmSetOpened(SSmSetOpened Obj_)
			{
				Opened = Obj_.Opened;
			}
			public SSmSetOpened(Boolean Opened_)
			{
				Opened = Opened_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Opened);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Opened", ref Opened);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Opened);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Opened", Opened);
			}
			public void Set(SSmSetOpened Obj_)
			{
				Opened = Obj_.Opened;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Opened);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Opened, "Opened");
			}
		}
		public class SMcLogin : SProto
		{
			public SNamePort ClientBindNamePortPubToServer = new SNamePort();
			public TSessionCode SessionCode = default(TSessionCode);
			public SMcLogin()
			{
			}
			public SMcLogin(SMcLogin Obj_)
			{
				ClientBindNamePortPubToServer = Obj_.ClientBindNamePortPubToServer;
				SessionCode = Obj_.SessionCode;
			}
			public SMcLogin(SNamePort ClientBindNamePortPubToServer_, TSessionCode SessionCode_)
			{
				ClientBindNamePortPubToServer = ClientBindNamePortPubToServer_;
				SessionCode = SessionCode_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ClientBindNamePortPubToServer);
				Stream_.Pop(ref SessionCode);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ClientBindNamePortPubToServer", ref ClientBindNamePortPubToServer);
				Value_.Pop("SessionCode", ref SessionCode);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ClientBindNamePortPubToServer);
				Stream_.Push(SessionCode);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ClientBindNamePortPubToServer", ClientBindNamePortPubToServer);
				Value_.Push("SessionCode", SessionCode);
			}
			public void Set(SMcLogin Obj_)
			{
				ClientBindNamePortPubToServer.Set(Obj_.ClientBindNamePortPubToServer);
				SessionCode = Obj_.SessionCode;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ClientBindNamePortPubToServer) + "," + 
					SEnumChecker.GetStdName(SessionCode);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ClientBindNamePortPubToServer, "ClientBindNamePortPubToServer") + "," + 
					SEnumChecker.GetMemberName(SessionCode, "SessionCode");
			}
		}
		public class SMcLoginFail : SProto
		{
			public EGameRet GameRet = default(EGameRet);
			public SMcLoginFail()
			{
			}
			public SMcLoginFail(SMcLoginFail Obj_)
			{
				GameRet = Obj_.GameRet;
			}
			public SMcLoginFail(EGameRet GameRet_)
			{
				GameRet = GameRet_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref GameRet);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("GameRet", ref GameRet);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(GameRet);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("GameRet", GameRet);
			}
			public void Set(SMcLoginFail Obj_)
			{
				GameRet = Obj_.GameRet;
			}
			public override string StdName()
			{
				return 
					"rso.game.EGameRet";
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(GameRet, "GameRet");
			}
		}
		public class SVersion : SProto
		{
			public TVer Main = default(TVer);
			public UInt64 Data = default(UInt64);
			public SVersion()
			{
			}
			public SVersion(SVersion Obj_)
			{
				Main = Obj_.Main;
				Data = Obj_.Data;
			}
			public SVersion(TVer Main_, UInt64 Data_)
			{
				Main = Main_;
				Data = Data_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Main);
				Stream_.Pop(ref Data);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Main", ref Main);
				Value_.Pop("Data", ref Data);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Main);
				Stream_.Push(Data);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Main", Main);
				Value_.Push("Data", Data);
			}
			public void Set(SVersion Obj_)
			{
				Main = Obj_.Main;
				Data = Obj_.Data;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Main) + "," + 
					SEnumChecker.GetStdName(Data);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Main, "Main") + "," + 
					SEnumChecker.GetMemberName(Data, "Data");
			}
		}
		public class SCsLogin : SProto
		{
			public TUID UID = default(TUID);
			public TID ID = string.Empty;
			public TUID SubUID = default(TUID);
			public TSessionCode SessionCode = default(TSessionCode);
			public SVersion Version = new SVersion();
			public Boolean Create = default(Boolean);
			public CStream Stream = new CStream();
			public SCsLogin()
			{
			}
			public SCsLogin(SCsLogin Obj_)
			{
				UID = Obj_.UID;
				ID = Obj_.ID;
				SubUID = Obj_.SubUID;
				SessionCode = Obj_.SessionCode;
				Version = Obj_.Version;
				Create = Obj_.Create;
				Stream = Obj_.Stream;
			}
			public SCsLogin(TUID UID_, TID ID_, TUID SubUID_, TSessionCode SessionCode_, SVersion Version_, Boolean Create_, CStream Stream_)
			{
				UID = UID_;
				ID = ID_;
				SubUID = SubUID_;
				SessionCode = SessionCode_;
				Version = Version_;
				Create = Create_;
				Stream = Stream_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref ID);
				Stream_.Pop(ref SubUID);
				Stream_.Pop(ref SessionCode);
				Stream_.Pop(ref Version);
				Stream_.Pop(ref Create);
				Stream_.Pop(ref Stream);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("ID", ref ID);
				Value_.Pop("SubUID", ref SubUID);
				Value_.Pop("SessionCode", ref SessionCode);
				Value_.Pop("Version", ref Version);
				Value_.Pop("Create", ref Create);
				Value_.Pop("Stream", ref Stream);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(ID);
				Stream_.Push(SubUID);
				Stream_.Push(SessionCode);
				Stream_.Push(Version);
				Stream_.Push(Create);
				Stream_.Push(Stream);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("ID", ID);
				Value_.Push("SubUID", SubUID);
				Value_.Push("SessionCode", SessionCode);
				Value_.Push("Version", Version);
				Value_.Push("Create", Create);
				Value_.Push("Stream", Stream);
			}
			public void Set(SCsLogin Obj_)
			{
				UID = Obj_.UID;
				ID = Obj_.ID;
				SubUID = Obj_.SubUID;
				SessionCode = Obj_.SessionCode;
				Version.Set(Obj_.Version);
				Create = Obj_.Create;
				Stream.Set(Obj_.Stream);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(ID) + "," + 
					SEnumChecker.GetStdName(SubUID) + "," + 
					SEnumChecker.GetStdName(SessionCode) + "," + 
					SEnumChecker.GetStdName(Version) + "," + 
					SEnumChecker.GetStdName(Create) + "," + 
					SEnumChecker.GetStdName(Stream);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(ID, "ID") + "," + 
					SEnumChecker.GetMemberName(SubUID, "SubUID") + "," + 
					SEnumChecker.GetMemberName(SessionCode, "SessionCode") + "," + 
					SEnumChecker.GetMemberName(Version, "Version") + "," + 
					SEnumChecker.GetMemberName(Create, "Create") + "," + 
					SEnumChecker.GetMemberName(Stream, "Stream");
			}
		}
		public class SScLogin : SProto
		{
			public TNick Nick = string.Empty;
			public TFriends Friends = new TFriends();
			public SScLogin()
			{
			}
			public SScLogin(SScLogin Obj_)
			{
				Nick = Obj_.Nick;
				Friends = Obj_.Friends;
			}
			public SScLogin(TNick Nick_, TFriends Friends_)
			{
				Nick = Nick_;
				Friends = Friends_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Nick);
				Stream_.Pop(ref Friends);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Nick", ref Nick);
				Value_.Pop("Friends", ref Friends);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Nick);
				Stream_.Push(Friends);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Nick", Nick);
				Value_.Push("Friends", Friends);
			}
			public void Set(SScLogin Obj_)
			{
				Nick = Obj_.Nick;
				Friends = Obj_.Friends;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Nick) + "," + 
					SEnumChecker.GetStdName(Friends);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Nick, "Nick") + "," + 
					SEnumChecker.GetMemberName(Friends, "Friends");
			}
		}
		public class SScLoginFail : SProto
		{
			public EGameRet GameRet = default(EGameRet);
			public SScLoginFail()
			{
			}
			public SScLoginFail(SScLoginFail Obj_)
			{
				GameRet = Obj_.GameRet;
			}
			public SScLoginFail(EGameRet GameRet_)
			{
				GameRet = GameRet_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref GameRet);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("GameRet", ref GameRet);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(GameRet);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("GameRet", GameRet);
			}
			public void Set(SScLoginFail Obj_)
			{
				GameRet = Obj_.GameRet;
			}
			public override string StdName()
			{
				return 
					"rso.game.EGameRet";
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(GameRet, "GameRet");
			}
		}
		public class SScError : SProto
		{
			public EGameRet GameRet = default(EGameRet);
			public SScError()
			{
			}
			public SScError(SScError Obj_)
			{
				GameRet = Obj_.GameRet;
			}
			public SScError(EGameRet GameRet_)
			{
				GameRet = GameRet_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref GameRet);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("GameRet", ref GameRet);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(GameRet);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("GameRet", GameRet);
			}
			public void Set(SScError Obj_)
			{
				GameRet = Obj_.GameRet;
			}
			public override string StdName()
			{
				return 
					"rso.game.EGameRet";
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(GameRet, "GameRet");
			}
		}
		public class SCsAddFriend : SProto
		{
			public TNick Nick = string.Empty;
			public SCsAddFriend()
			{
			}
			public SCsAddFriend(SCsAddFriend Obj_)
			{
				Nick = Obj_.Nick;
			}
			public SCsAddFriend(TNick Nick_)
			{
				Nick = Nick_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Nick);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Nick", ref Nick);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Nick);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Nick", Nick);
			}
			public void Set(SCsAddFriend Obj_)
			{
				Nick = Obj_.Nick;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Nick);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Nick, "Nick");
			}
		}
		public class SSmAddFriend : SProto
		{
			public TNick ToNick = string.Empty;
			public TUID FromUID = default(TUID);
			public TNick FromNick = string.Empty;
			public SSmAddFriend()
			{
			}
			public SSmAddFriend(SSmAddFriend Obj_)
			{
				ToNick = Obj_.ToNick;
				FromUID = Obj_.FromUID;
				FromNick = Obj_.FromNick;
			}
			public SSmAddFriend(TNick ToNick_, TUID FromUID_, TNick FromNick_)
			{
				ToNick = ToNick_;
				FromUID = FromUID_;
				FromNick = FromNick_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ToNick);
				Stream_.Pop(ref FromUID);
				Stream_.Pop(ref FromNick);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ToNick", ref ToNick);
				Value_.Pop("FromUID", ref FromUID);
				Value_.Pop("FromNick", ref FromNick);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ToNick);
				Stream_.Push(FromUID);
				Stream_.Push(FromNick);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ToNick", ToNick);
				Value_.Push("FromUID", FromUID);
				Value_.Push("FromNick", FromNick);
			}
			public void Set(SSmAddFriend Obj_)
			{
				ToNick = Obj_.ToNick;
				FromUID = Obj_.FromUID;
				FromNick = Obj_.FromNick;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ToNick) + "," + 
					SEnumChecker.GetStdName(FromUID) + "," + 
					SEnumChecker.GetStdName(FromNick);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ToNick, "ToNick") + "," + 
					SEnumChecker.GetMemberName(FromUID, "FromUID") + "," + 
					SEnumChecker.GetMemberName(FromNick, "FromNick");
			}
		}
		public class SMaAddFriendGetUID : SProto
		{
			public TNick ToNick = string.Empty;
			public TUID FromUID = default(TUID);
			public TNick FromNick = string.Empty;
			public SKey FromServerKey = new SKey();
			public SMaAddFriendGetUID()
			{
			}
			public SMaAddFriendGetUID(SMaAddFriendGetUID Obj_)
			{
				ToNick = Obj_.ToNick;
				FromUID = Obj_.FromUID;
				FromNick = Obj_.FromNick;
				FromServerKey = Obj_.FromServerKey;
			}
			public SMaAddFriendGetUID(TNick ToNick_, TUID FromUID_, TNick FromNick_, SKey FromServerKey_)
			{
				ToNick = ToNick_;
				FromUID = FromUID_;
				FromNick = FromNick_;
				FromServerKey = FromServerKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ToNick);
				Stream_.Pop(ref FromUID);
				Stream_.Pop(ref FromNick);
				Stream_.Pop(ref FromServerKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ToNick", ref ToNick);
				Value_.Pop("FromUID", ref FromUID);
				Value_.Pop("FromNick", ref FromNick);
				Value_.Pop("FromServerKey", ref FromServerKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ToNick);
				Stream_.Push(FromUID);
				Stream_.Push(FromNick);
				Stream_.Push(FromServerKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ToNick", ToNick);
				Value_.Push("FromUID", FromUID);
				Value_.Push("FromNick", FromNick);
				Value_.Push("FromServerKey", FromServerKey);
			}
			public void Set(SMaAddFriendGetUID Obj_)
			{
				ToNick = Obj_.ToNick;
				FromUID = Obj_.FromUID;
				FromNick = Obj_.FromNick;
				FromServerKey.Set(Obj_.FromServerKey);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ToNick) + "," + 
					SEnumChecker.GetStdName(FromUID) + "," + 
					SEnumChecker.GetStdName(FromNick) + "," + 
					SEnumChecker.GetStdName(FromServerKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ToNick, "ToNick") + "," + 
					SEnumChecker.GetMemberName(FromUID, "FromUID") + "," + 
					SEnumChecker.GetMemberName(FromNick, "FromNick") + "," + 
					SEnumChecker.GetMemberName(FromServerKey, "FromServerKey");
			}
		}
		public class SAmAddFriendGetUID : SProto
		{
			public TUID ToUID = default(TUID);
			public TNick ToNick = string.Empty;
			public TUID FromUID = default(TUID);
			public TNick FromNick = string.Empty;
			public SKey FromServerKey = new SKey();
			public SAmAddFriendGetUID()
			{
			}
			public SAmAddFriendGetUID(SAmAddFriendGetUID Obj_)
			{
				ToUID = Obj_.ToUID;
				ToNick = Obj_.ToNick;
				FromUID = Obj_.FromUID;
				FromNick = Obj_.FromNick;
				FromServerKey = Obj_.FromServerKey;
			}
			public SAmAddFriendGetUID(TUID ToUID_, TNick ToNick_, TUID FromUID_, TNick FromNick_, SKey FromServerKey_)
			{
				ToUID = ToUID_;
				ToNick = ToNick_;
				FromUID = FromUID_;
				FromNick = FromNick_;
				FromServerKey = FromServerKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ToUID);
				Stream_.Pop(ref ToNick);
				Stream_.Pop(ref FromUID);
				Stream_.Pop(ref FromNick);
				Stream_.Pop(ref FromServerKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ToUID", ref ToUID);
				Value_.Pop("ToNick", ref ToNick);
				Value_.Pop("FromUID", ref FromUID);
				Value_.Pop("FromNick", ref FromNick);
				Value_.Pop("FromServerKey", ref FromServerKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ToUID);
				Stream_.Push(ToNick);
				Stream_.Push(FromUID);
				Stream_.Push(FromNick);
				Stream_.Push(FromServerKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ToUID", ToUID);
				Value_.Push("ToNick", ToNick);
				Value_.Push("FromUID", FromUID);
				Value_.Push("FromNick", FromNick);
				Value_.Push("FromServerKey", FromServerKey);
			}
			public void Set(SAmAddFriendGetUID Obj_)
			{
				ToUID = Obj_.ToUID;
				ToNick = Obj_.ToNick;
				FromUID = Obj_.FromUID;
				FromNick = Obj_.FromNick;
				FromServerKey.Set(Obj_.FromServerKey);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ToUID) + "," + 
					SEnumChecker.GetStdName(ToNick) + "," + 
					SEnumChecker.GetStdName(FromUID) + "," + 
					SEnumChecker.GetStdName(FromNick) + "," + 
					SEnumChecker.GetStdName(FromServerKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ToUID, "ToUID") + "," + 
					SEnumChecker.GetMemberName(ToNick, "ToNick") + "," + 
					SEnumChecker.GetMemberName(FromUID, "FromUID") + "," + 
					SEnumChecker.GetMemberName(FromNick, "FromNick") + "," + 
					SEnumChecker.GetMemberName(FromServerKey, "FromServerKey");
			}
		}
		public class SAmAddFriendGetUIDFail : SProto
		{
			public EGameRet GameRet = default(EGameRet);
			public TUID FromUID = default(TUID);
			public SKey FromServerKey = new SKey();
			public SAmAddFriendGetUIDFail()
			{
			}
			public SAmAddFriendGetUIDFail(SAmAddFriendGetUIDFail Obj_)
			{
				GameRet = Obj_.GameRet;
				FromUID = Obj_.FromUID;
				FromServerKey = Obj_.FromServerKey;
			}
			public SAmAddFriendGetUIDFail(EGameRet GameRet_, TUID FromUID_, SKey FromServerKey_)
			{
				GameRet = GameRet_;
				FromUID = FromUID_;
				FromServerKey = FromServerKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref GameRet);
				Stream_.Pop(ref FromUID);
				Stream_.Pop(ref FromServerKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("GameRet", ref GameRet);
				Value_.Pop("FromUID", ref FromUID);
				Value_.Pop("FromServerKey", ref FromServerKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(GameRet);
				Stream_.Push(FromUID);
				Stream_.Push(FromServerKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("GameRet", GameRet);
				Value_.Push("FromUID", FromUID);
				Value_.Push("FromServerKey", FromServerKey);
			}
			public void Set(SAmAddFriendGetUIDFail Obj_)
			{
				GameRet = Obj_.GameRet;
				FromUID = Obj_.FromUID;
				FromServerKey.Set(Obj_.FromServerKey);
			}
			public override string StdName()
			{
				return 
					"rso.game.EGameRet" + "," + 
					SEnumChecker.GetStdName(FromUID) + "," + 
					SEnumChecker.GetStdName(FromServerKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(GameRet, "GameRet") + "," + 
					SEnumChecker.GetMemberName(FromUID, "FromUID") + "," + 
					SEnumChecker.GetMemberName(FromServerKey, "FromServerKey");
			}
		}
		public class SMaAddFriend : SProto
		{
			public TUID ToUID = default(TUID);
			public TNick ToNick = string.Empty;
			public TUID FromUID = default(TUID);
			public TNick FromNick = string.Empty;
			public SKey FromServerKey = new SKey();
			public SMaAddFriend()
			{
			}
			public SMaAddFriend(SMaAddFriend Obj_)
			{
				ToUID = Obj_.ToUID;
				ToNick = Obj_.ToNick;
				FromUID = Obj_.FromUID;
				FromNick = Obj_.FromNick;
				FromServerKey = Obj_.FromServerKey;
			}
			public SMaAddFriend(TUID ToUID_, TNick ToNick_, TUID FromUID_, TNick FromNick_, SKey FromServerKey_)
			{
				ToUID = ToUID_;
				ToNick = ToNick_;
				FromUID = FromUID_;
				FromNick = FromNick_;
				FromServerKey = FromServerKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ToUID);
				Stream_.Pop(ref ToNick);
				Stream_.Pop(ref FromUID);
				Stream_.Pop(ref FromNick);
				Stream_.Pop(ref FromServerKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ToUID", ref ToUID);
				Value_.Pop("ToNick", ref ToNick);
				Value_.Pop("FromUID", ref FromUID);
				Value_.Pop("FromNick", ref FromNick);
				Value_.Pop("FromServerKey", ref FromServerKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ToUID);
				Stream_.Push(ToNick);
				Stream_.Push(FromUID);
				Stream_.Push(FromNick);
				Stream_.Push(FromServerKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ToUID", ToUID);
				Value_.Push("ToNick", ToNick);
				Value_.Push("FromUID", FromUID);
				Value_.Push("FromNick", FromNick);
				Value_.Push("FromServerKey", FromServerKey);
			}
			public void Set(SMaAddFriend Obj_)
			{
				ToUID = Obj_.ToUID;
				ToNick = Obj_.ToNick;
				FromUID = Obj_.FromUID;
				FromNick = Obj_.FromNick;
				FromServerKey.Set(Obj_.FromServerKey);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ToUID) + "," + 
					SEnumChecker.GetStdName(ToNick) + "," + 
					SEnumChecker.GetStdName(FromUID) + "," + 
					SEnumChecker.GetStdName(FromNick) + "," + 
					SEnumChecker.GetStdName(FromServerKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ToUID, "ToUID") + "," + 
					SEnumChecker.GetMemberName(ToNick, "ToNick") + "," + 
					SEnumChecker.GetMemberName(FromUID, "FromUID") + "," + 
					SEnumChecker.GetMemberName(FromNick, "FromNick") + "," + 
					SEnumChecker.GetMemberName(FromServerKey, "FromServerKey");
			}
		}
		public class SAmAddFriendRequest : SProto
		{
			public TUID ToUID = default(TUID);
			public TNick ToNick = string.Empty;
			public TUID FromUID = default(TUID);
			public TNick FromNick = string.Empty;
			public SKey FromServerKey = new SKey();
			public SKey FromMasterKey = new SKey();
			public SAmAddFriendRequest()
			{
			}
			public SAmAddFriendRequest(SAmAddFriendRequest Obj_)
			{
				ToUID = Obj_.ToUID;
				ToNick = Obj_.ToNick;
				FromUID = Obj_.FromUID;
				FromNick = Obj_.FromNick;
				FromServerKey = Obj_.FromServerKey;
				FromMasterKey = Obj_.FromMasterKey;
			}
			public SAmAddFriendRequest(TUID ToUID_, TNick ToNick_, TUID FromUID_, TNick FromNick_, SKey FromServerKey_, SKey FromMasterKey_)
			{
				ToUID = ToUID_;
				ToNick = ToNick_;
				FromUID = FromUID_;
				FromNick = FromNick_;
				FromServerKey = FromServerKey_;
				FromMasterKey = FromMasterKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ToUID);
				Stream_.Pop(ref ToNick);
				Stream_.Pop(ref FromUID);
				Stream_.Pop(ref FromNick);
				Stream_.Pop(ref FromServerKey);
				Stream_.Pop(ref FromMasterKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ToUID", ref ToUID);
				Value_.Pop("ToNick", ref ToNick);
				Value_.Pop("FromUID", ref FromUID);
				Value_.Pop("FromNick", ref FromNick);
				Value_.Pop("FromServerKey", ref FromServerKey);
				Value_.Pop("FromMasterKey", ref FromMasterKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ToUID);
				Stream_.Push(ToNick);
				Stream_.Push(FromUID);
				Stream_.Push(FromNick);
				Stream_.Push(FromServerKey);
				Stream_.Push(FromMasterKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ToUID", ToUID);
				Value_.Push("ToNick", ToNick);
				Value_.Push("FromUID", FromUID);
				Value_.Push("FromNick", FromNick);
				Value_.Push("FromServerKey", FromServerKey);
				Value_.Push("FromMasterKey", FromMasterKey);
			}
			public void Set(SAmAddFriendRequest Obj_)
			{
				ToUID = Obj_.ToUID;
				ToNick = Obj_.ToNick;
				FromUID = Obj_.FromUID;
				FromNick = Obj_.FromNick;
				FromServerKey.Set(Obj_.FromServerKey);
				FromMasterKey.Set(Obj_.FromMasterKey);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ToUID) + "," + 
					SEnumChecker.GetStdName(ToNick) + "," + 
					SEnumChecker.GetStdName(FromUID) + "," + 
					SEnumChecker.GetStdName(FromNick) + "," + 
					SEnumChecker.GetStdName(FromServerKey) + "," + 
					SEnumChecker.GetStdName(FromMasterKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ToUID, "ToUID") + "," + 
					SEnumChecker.GetMemberName(ToNick, "ToNick") + "," + 
					SEnumChecker.GetMemberName(FromUID, "FromUID") + "," + 
					SEnumChecker.GetMemberName(FromNick, "FromNick") + "," + 
					SEnumChecker.GetMemberName(FromServerKey, "FromServerKey") + "," + 
					SEnumChecker.GetMemberName(FromMasterKey, "FromMasterKey");
			}
		}
		public class SMsAddFriendRequest : SProto
		{
			public TUID ToUID = default(TUID);
			public TUID FromUID = default(TUID);
			public TNick FromNick = string.Empty;
			public SMsAddFriendRequest()
			{
			}
			public SMsAddFriendRequest(SMsAddFriendRequest Obj_)
			{
				ToUID = Obj_.ToUID;
				FromUID = Obj_.FromUID;
				FromNick = Obj_.FromNick;
			}
			public SMsAddFriendRequest(TUID ToUID_, TUID FromUID_, TNick FromNick_)
			{
				ToUID = ToUID_;
				FromUID = FromUID_;
				FromNick = FromNick_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ToUID);
				Stream_.Pop(ref FromUID);
				Stream_.Pop(ref FromNick);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ToUID", ref ToUID);
				Value_.Pop("FromUID", ref FromUID);
				Value_.Pop("FromNick", ref FromNick);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ToUID);
				Stream_.Push(FromUID);
				Stream_.Push(FromNick);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ToUID", ToUID);
				Value_.Push("FromUID", FromUID);
				Value_.Push("FromNick", FromNick);
			}
			public void Set(SMsAddFriendRequest Obj_)
			{
				ToUID = Obj_.ToUID;
				FromUID = Obj_.FromUID;
				FromNick = Obj_.FromNick;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ToUID) + "," + 
					SEnumChecker.GetStdName(FromUID) + "," + 
					SEnumChecker.GetStdName(FromNick);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ToUID, "ToUID") + "," + 
					SEnumChecker.GetMemberName(FromUID, "FromUID") + "," + 
					SEnumChecker.GetMemberName(FromNick, "FromNick");
			}
		}
		public class SScAddFriendRequest : SProto
		{
			public TUID FromUID = default(TUID);
			public TNick FromNick = string.Empty;
			public SScAddFriendRequest()
			{
			}
			public SScAddFriendRequest(SScAddFriendRequest Obj_)
			{
				FromUID = Obj_.FromUID;
				FromNick = Obj_.FromNick;
			}
			public SScAddFriendRequest(TUID FromUID_, TNick FromNick_)
			{
				FromUID = FromUID_;
				FromNick = FromNick_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref FromUID);
				Stream_.Pop(ref FromNick);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("FromUID", ref FromUID);
				Value_.Pop("FromNick", ref FromNick);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(FromUID);
				Stream_.Push(FromNick);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("FromUID", FromUID);
				Value_.Push("FromNick", FromNick);
			}
			public void Set(SScAddFriendRequest Obj_)
			{
				FromUID = Obj_.FromUID;
				FromNick = Obj_.FromNick;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(FromUID) + "," + 
					SEnumChecker.GetStdName(FromNick);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(FromUID, "FromUID") + "," + 
					SEnumChecker.GetMemberName(FromNick, "FromNick");
			}
		}
		public class SMaAddFriendRequest : SProto
		{
			public TUID ToUID = default(TUID);
			public TNick ToNick = string.Empty;
			public TUID FromUID = default(TUID);
			public SKey FromServerKey = new SKey();
			public SKey FromMasterKey = new SKey();
			public SMaAddFriendRequest()
			{
			}
			public SMaAddFriendRequest(SMaAddFriendRequest Obj_)
			{
				ToUID = Obj_.ToUID;
				ToNick = Obj_.ToNick;
				FromUID = Obj_.FromUID;
				FromServerKey = Obj_.FromServerKey;
				FromMasterKey = Obj_.FromMasterKey;
			}
			public SMaAddFriendRequest(TUID ToUID_, TNick ToNick_, TUID FromUID_, SKey FromServerKey_, SKey FromMasterKey_)
			{
				ToUID = ToUID_;
				ToNick = ToNick_;
				FromUID = FromUID_;
				FromServerKey = FromServerKey_;
				FromMasterKey = FromMasterKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ToUID);
				Stream_.Pop(ref ToNick);
				Stream_.Pop(ref FromUID);
				Stream_.Pop(ref FromServerKey);
				Stream_.Pop(ref FromMasterKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ToUID", ref ToUID);
				Value_.Pop("ToNick", ref ToNick);
				Value_.Pop("FromUID", ref FromUID);
				Value_.Pop("FromServerKey", ref FromServerKey);
				Value_.Pop("FromMasterKey", ref FromMasterKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ToUID);
				Stream_.Push(ToNick);
				Stream_.Push(FromUID);
				Stream_.Push(FromServerKey);
				Stream_.Push(FromMasterKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ToUID", ToUID);
				Value_.Push("ToNick", ToNick);
				Value_.Push("FromUID", FromUID);
				Value_.Push("FromServerKey", FromServerKey);
				Value_.Push("FromMasterKey", FromMasterKey);
			}
			public void Set(SMaAddFriendRequest Obj_)
			{
				ToUID = Obj_.ToUID;
				ToNick = Obj_.ToNick;
				FromUID = Obj_.FromUID;
				FromServerKey.Set(Obj_.FromServerKey);
				FromMasterKey.Set(Obj_.FromMasterKey);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ToUID) + "," + 
					SEnumChecker.GetStdName(ToNick) + "," + 
					SEnumChecker.GetStdName(FromUID) + "," + 
					SEnumChecker.GetStdName(FromServerKey) + "," + 
					SEnumChecker.GetStdName(FromMasterKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ToUID, "ToUID") + "," + 
					SEnumChecker.GetMemberName(ToNick, "ToNick") + "," + 
					SEnumChecker.GetMemberName(FromUID, "FromUID") + "," + 
					SEnumChecker.GetMemberName(FromServerKey, "FromServerKey") + "," + 
					SEnumChecker.GetMemberName(FromMasterKey, "FromMasterKey");
			}
		}
		public class SMaAddFriendRequestFail : SProto
		{
			public EGameRet GameRet = default(EGameRet);
			public TUID ToUID = default(TUID);
			public TUID FromUID = default(TUID);
			public SKey FromServerKey = new SKey();
			public SKey FromMasterKey = new SKey();
			public SMaAddFriendRequestFail()
			{
			}
			public SMaAddFriendRequestFail(SMaAddFriendRequestFail Obj_)
			{
				GameRet = Obj_.GameRet;
				ToUID = Obj_.ToUID;
				FromUID = Obj_.FromUID;
				FromServerKey = Obj_.FromServerKey;
				FromMasterKey = Obj_.FromMasterKey;
			}
			public SMaAddFriendRequestFail(EGameRet GameRet_, TUID ToUID_, TUID FromUID_, SKey FromServerKey_, SKey FromMasterKey_)
			{
				GameRet = GameRet_;
				ToUID = ToUID_;
				FromUID = FromUID_;
				FromServerKey = FromServerKey_;
				FromMasterKey = FromMasterKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref GameRet);
				Stream_.Pop(ref ToUID);
				Stream_.Pop(ref FromUID);
				Stream_.Pop(ref FromServerKey);
				Stream_.Pop(ref FromMasterKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("GameRet", ref GameRet);
				Value_.Pop("ToUID", ref ToUID);
				Value_.Pop("FromUID", ref FromUID);
				Value_.Pop("FromServerKey", ref FromServerKey);
				Value_.Pop("FromMasterKey", ref FromMasterKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(GameRet);
				Stream_.Push(ToUID);
				Stream_.Push(FromUID);
				Stream_.Push(FromServerKey);
				Stream_.Push(FromMasterKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("GameRet", GameRet);
				Value_.Push("ToUID", ToUID);
				Value_.Push("FromUID", FromUID);
				Value_.Push("FromServerKey", FromServerKey);
				Value_.Push("FromMasterKey", FromMasterKey);
			}
			public void Set(SMaAddFriendRequestFail Obj_)
			{
				GameRet = Obj_.GameRet;
				ToUID = Obj_.ToUID;
				FromUID = Obj_.FromUID;
				FromServerKey.Set(Obj_.FromServerKey);
				FromMasterKey.Set(Obj_.FromMasterKey);
			}
			public override string StdName()
			{
				return 
					"rso.game.EGameRet" + "," + 
					SEnumChecker.GetStdName(ToUID) + "," + 
					SEnumChecker.GetStdName(FromUID) + "," + 
					SEnumChecker.GetStdName(FromServerKey) + "," + 
					SEnumChecker.GetStdName(FromMasterKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(GameRet, "GameRet") + "," + 
					SEnumChecker.GetMemberName(ToUID, "ToUID") + "," + 
					SEnumChecker.GetMemberName(FromUID, "FromUID") + "," + 
					SEnumChecker.GetMemberName(FromServerKey, "FromServerKey") + "," + 
					SEnumChecker.GetMemberName(FromMasterKey, "FromMasterKey");
			}
		}
		public class SAmAddFriend : SProto
		{
			public TUID ToUID = default(TUID);
			public TNick ToNick = string.Empty;
			public TUID FromUID = default(TUID);
			public SKey FromServerKey = new SKey();
			public SAmAddFriend()
			{
			}
			public SAmAddFriend(SAmAddFriend Obj_)
			{
				ToUID = Obj_.ToUID;
				ToNick = Obj_.ToNick;
				FromUID = Obj_.FromUID;
				FromServerKey = Obj_.FromServerKey;
			}
			public SAmAddFriend(TUID ToUID_, TNick ToNick_, TUID FromUID_, SKey FromServerKey_)
			{
				ToUID = ToUID_;
				ToNick = ToNick_;
				FromUID = FromUID_;
				FromServerKey = FromServerKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ToUID);
				Stream_.Pop(ref ToNick);
				Stream_.Pop(ref FromUID);
				Stream_.Pop(ref FromServerKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ToUID", ref ToUID);
				Value_.Pop("ToNick", ref ToNick);
				Value_.Pop("FromUID", ref FromUID);
				Value_.Pop("FromServerKey", ref FromServerKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ToUID);
				Stream_.Push(ToNick);
				Stream_.Push(FromUID);
				Stream_.Push(FromServerKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ToUID", ToUID);
				Value_.Push("ToNick", ToNick);
				Value_.Push("FromUID", FromUID);
				Value_.Push("FromServerKey", FromServerKey);
			}
			public void Set(SAmAddFriend Obj_)
			{
				ToUID = Obj_.ToUID;
				ToNick = Obj_.ToNick;
				FromUID = Obj_.FromUID;
				FromServerKey.Set(Obj_.FromServerKey);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ToUID) + "," + 
					SEnumChecker.GetStdName(ToNick) + "," + 
					SEnumChecker.GetStdName(FromUID) + "," + 
					SEnumChecker.GetStdName(FromServerKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ToUID, "ToUID") + "," + 
					SEnumChecker.GetMemberName(ToNick, "ToNick") + "," + 
					SEnumChecker.GetMemberName(FromUID, "FromUID") + "," + 
					SEnumChecker.GetMemberName(FromServerKey, "FromServerKey");
			}
		}
		public class SAmAddFriendFail : SProto
		{
			public EGameRet GameRet = default(EGameRet);
			public TUID ToUID = default(TUID);
			public TUID FromUID = default(TUID);
			public SKey FromServerKey = new SKey();
			public SAmAddFriendFail()
			{
			}
			public SAmAddFriendFail(SAmAddFriendFail Obj_)
			{
				GameRet = Obj_.GameRet;
				ToUID = Obj_.ToUID;
				FromUID = Obj_.FromUID;
				FromServerKey = Obj_.FromServerKey;
			}
			public SAmAddFriendFail(EGameRet GameRet_, TUID ToUID_, TUID FromUID_, SKey FromServerKey_)
			{
				GameRet = GameRet_;
				ToUID = ToUID_;
				FromUID = FromUID_;
				FromServerKey = FromServerKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref GameRet);
				Stream_.Pop(ref ToUID);
				Stream_.Pop(ref FromUID);
				Stream_.Pop(ref FromServerKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("GameRet", ref GameRet);
				Value_.Pop("ToUID", ref ToUID);
				Value_.Pop("FromUID", ref FromUID);
				Value_.Pop("FromServerKey", ref FromServerKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(GameRet);
				Stream_.Push(ToUID);
				Stream_.Push(FromUID);
				Stream_.Push(FromServerKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("GameRet", GameRet);
				Value_.Push("ToUID", ToUID);
				Value_.Push("FromUID", FromUID);
				Value_.Push("FromServerKey", FromServerKey);
			}
			public void Set(SAmAddFriendFail Obj_)
			{
				GameRet = Obj_.GameRet;
				ToUID = Obj_.ToUID;
				FromUID = Obj_.FromUID;
				FromServerKey.Set(Obj_.FromServerKey);
			}
			public override string StdName()
			{
				return 
					"rso.game.EGameRet" + "," + 
					SEnumChecker.GetStdName(ToUID) + "," + 
					SEnumChecker.GetStdName(FromUID) + "," + 
					SEnumChecker.GetStdName(FromServerKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(GameRet, "GameRet") + "," + 
					SEnumChecker.GetMemberName(ToUID, "ToUID") + "," + 
					SEnumChecker.GetMemberName(FromUID, "FromUID") + "," + 
					SEnumChecker.GetMemberName(FromServerKey, "FromServerKey");
			}
		}
		public class SMsAddFriend : SProto
		{
			public TUID ToUID = default(TUID);
			public TNick ToNick = string.Empty;
			public TUID FromUID = default(TUID);
			public SMsAddFriend()
			{
			}
			public SMsAddFriend(SMsAddFriend Obj_)
			{
				ToUID = Obj_.ToUID;
				ToNick = Obj_.ToNick;
				FromUID = Obj_.FromUID;
			}
			public SMsAddFriend(TUID ToUID_, TNick ToNick_, TUID FromUID_)
			{
				ToUID = ToUID_;
				ToNick = ToNick_;
				FromUID = FromUID_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ToUID);
				Stream_.Pop(ref ToNick);
				Stream_.Pop(ref FromUID);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ToUID", ref ToUID);
				Value_.Pop("ToNick", ref ToNick);
				Value_.Pop("FromUID", ref FromUID);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ToUID);
				Stream_.Push(ToNick);
				Stream_.Push(FromUID);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ToUID", ToUID);
				Value_.Push("ToNick", ToNick);
				Value_.Push("FromUID", FromUID);
			}
			public void Set(SMsAddFriend Obj_)
			{
				ToUID = Obj_.ToUID;
				ToNick = Obj_.ToNick;
				FromUID = Obj_.FromUID;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ToUID) + "," + 
					SEnumChecker.GetStdName(ToNick) + "," + 
					SEnumChecker.GetStdName(FromUID);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ToUID, "ToUID") + "," + 
					SEnumChecker.GetMemberName(ToNick, "ToNick") + "," + 
					SEnumChecker.GetMemberName(FromUID, "FromUID");
			}
		}
		public class SMsAddFriendFail : SProto
		{
			public EGameRet GameRet = default(EGameRet);
			public TUID FromUID = default(TUID);
			public SMsAddFriendFail()
			{
			}
			public SMsAddFriendFail(SMsAddFriendFail Obj_)
			{
				GameRet = Obj_.GameRet;
				FromUID = Obj_.FromUID;
			}
			public SMsAddFriendFail(EGameRet GameRet_, TUID FromUID_)
			{
				GameRet = GameRet_;
				FromUID = FromUID_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref GameRet);
				Stream_.Pop(ref FromUID);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("GameRet", ref GameRet);
				Value_.Pop("FromUID", ref FromUID);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(GameRet);
				Stream_.Push(FromUID);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("GameRet", GameRet);
				Value_.Push("FromUID", FromUID);
			}
			public void Set(SMsAddFriendFail Obj_)
			{
				GameRet = Obj_.GameRet;
				FromUID = Obj_.FromUID;
			}
			public override string StdName()
			{
				return 
					"rso.game.EGameRet" + "," + 
					SEnumChecker.GetStdName(FromUID);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(GameRet, "GameRet") + "," + 
					SEnumChecker.GetMemberName(FromUID, "FromUID");
			}
		}
		public class SScAddFriend : SProto
		{
			public TUID ToUID = default(TUID);
			public SFriend Friend = new SFriend();
			public SScAddFriend()
			{
			}
			public SScAddFriend(SScAddFriend Obj_)
			{
				ToUID = Obj_.ToUID;
				Friend = Obj_.Friend;
			}
			public SScAddFriend(TUID ToUID_, SFriend Friend_)
			{
				ToUID = ToUID_;
				Friend = Friend_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ToUID);
				Stream_.Pop(ref Friend);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ToUID", ref ToUID);
				Value_.Pop("Friend", ref Friend);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ToUID);
				Stream_.Push(Friend);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ToUID", ToUID);
				Value_.Push("Friend", Friend);
			}
			public void Set(SScAddFriend Obj_)
			{
				ToUID = Obj_.ToUID;
				Friend.Set(Obj_.Friend);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ToUID) + "," + 
					SEnumChecker.GetStdName(Friend);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ToUID, "ToUID") + "," + 
					SEnumChecker.GetMemberName(Friend, "Friend");
			}
		}
		public class SCsAllowFriend : SProto
		{
			public TUID FriendUID = default(TUID);
			public SCsAllowFriend()
			{
			}
			public SCsAllowFriend(SCsAllowFriend Obj_)
			{
				FriendUID = Obj_.FriendUID;
			}
			public SCsAllowFriend(TUID FriendUID_)
			{
				FriendUID = FriendUID_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref FriendUID);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("FriendUID", ref FriendUID);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(FriendUID);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("FriendUID", FriendUID);
			}
			public void Set(SCsAllowFriend Obj_)
			{
				FriendUID = Obj_.FriendUID;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(FriendUID);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(FriendUID, "FriendUID");
			}
		}
		public class SSmAllowFriend : SProto
		{
			public TUID UID = default(TUID);
			public TUID FriendUID = default(TUID);
			public SKey UserKey = new SKey();
			public SSmAllowFriend()
			{
			}
			public SSmAllowFriend(SSmAllowFriend Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				UserKey = Obj_.UserKey;
			}
			public SSmAllowFriend(TUID UID_, TUID FriendUID_, SKey UserKey_)
			{
				UID = UID_;
				FriendUID = FriendUID_;
				UserKey = UserKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref FriendUID);
				Stream_.Pop(ref UserKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("FriendUID", ref FriendUID);
				Value_.Pop("UserKey", ref UserKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(FriendUID);
				Stream_.Push(UserKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("FriendUID", FriendUID);
				Value_.Push("UserKey", UserKey);
			}
			public void Set(SSmAllowFriend Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				UserKey.Set(Obj_.UserKey);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(FriendUID) + "," + 
					SEnumChecker.GetStdName(UserKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(FriendUID, "FriendUID") + "," + 
					SEnumChecker.GetMemberName(UserKey, "UserKey");
			}
		}
		public class SMsAllowFriend : SProto
		{
			public TUID UID = default(TUID);
			public TUID FriendUID = default(TUID);
			public SKey UserKey = new SKey();
			public SMsAllowFriend()
			{
			}
			public SMsAllowFriend(SMsAllowFriend Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				UserKey = Obj_.UserKey;
			}
			public SMsAllowFriend(TUID UID_, TUID FriendUID_, SKey UserKey_)
			{
				UID = UID_;
				FriendUID = FriendUID_;
				UserKey = UserKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref FriendUID);
				Stream_.Pop(ref UserKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("FriendUID", ref FriendUID);
				Value_.Pop("UserKey", ref UserKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(FriendUID);
				Stream_.Push(UserKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("FriendUID", FriendUID);
				Value_.Push("UserKey", UserKey);
			}
			public void Set(SMsAllowFriend Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				UserKey.Set(Obj_.UserKey);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(FriendUID) + "," + 
					SEnumChecker.GetStdName(UserKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(FriendUID, "FriendUID") + "," + 
					SEnumChecker.GetMemberName(UserKey, "UserKey");
			}
		}
		public class SMsAllowFriendFail : SProto
		{
			public EGameRet GameRet = default(EGameRet);
			public SKey UserKey = new SKey();
			public SMsAllowFriendFail()
			{
			}
			public SMsAllowFriendFail(SMsAllowFriendFail Obj_)
			{
				GameRet = Obj_.GameRet;
				UserKey = Obj_.UserKey;
			}
			public SMsAllowFriendFail(EGameRet GameRet_, SKey UserKey_)
			{
				GameRet = GameRet_;
				UserKey = UserKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref GameRet);
				Stream_.Pop(ref UserKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("GameRet", ref GameRet);
				Value_.Pop("UserKey", ref UserKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(GameRet);
				Stream_.Push(UserKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("GameRet", GameRet);
				Value_.Push("UserKey", UserKey);
			}
			public void Set(SMsAllowFriendFail Obj_)
			{
				GameRet = Obj_.GameRet;
				UserKey.Set(Obj_.UserKey);
			}
			public override string StdName()
			{
				return 
					"rso.game.EGameRet" + "," + 
					SEnumChecker.GetStdName(UserKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(GameRet, "GameRet") + "," + 
					SEnumChecker.GetMemberName(UserKey, "UserKey");
			}
		}
		public class SScAllowFriend : SProto
		{
			public TUID FriendUID = default(TUID);
			public SScAllowFriend()
			{
			}
			public SScAllowFriend(SScAllowFriend Obj_)
			{
				FriendUID = Obj_.FriendUID;
			}
			public SScAllowFriend(TUID FriendUID_)
			{
				FriendUID = FriendUID_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref FriendUID);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("FriendUID", ref FriendUID);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(FriendUID);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("FriendUID", FriendUID);
			}
			public void Set(SScAllowFriend Obj_)
			{
				FriendUID = Obj_.FriendUID;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(FriendUID);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(FriendUID, "FriendUID");
			}
		}
		public class SCsDenyFriend : SProto
		{
			public TUID FriendUID = default(TUID);
			public SCsDenyFriend()
			{
			}
			public SCsDenyFriend(SCsDenyFriend Obj_)
			{
				FriendUID = Obj_.FriendUID;
			}
			public SCsDenyFriend(TUID FriendUID_)
			{
				FriendUID = FriendUID_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref FriendUID);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("FriendUID", ref FriendUID);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(FriendUID);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("FriendUID", FriendUID);
			}
			public void Set(SCsDenyFriend Obj_)
			{
				FriendUID = Obj_.FriendUID;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(FriendUID);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(FriendUID, "FriendUID");
			}
		}
		public class SSmDenyFriend : SProto
		{
			public TUID UID = default(TUID);
			public TUID FriendUID = default(TUID);
			public SKey UserKey = new SKey();
			public SSmDenyFriend()
			{
			}
			public SSmDenyFriend(SSmDenyFriend Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				UserKey = Obj_.UserKey;
			}
			public SSmDenyFriend(TUID UID_, TUID FriendUID_, SKey UserKey_)
			{
				UID = UID_;
				FriendUID = FriendUID_;
				UserKey = UserKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref FriendUID);
				Stream_.Pop(ref UserKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("FriendUID", ref FriendUID);
				Value_.Pop("UserKey", ref UserKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(FriendUID);
				Stream_.Push(UserKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("FriendUID", FriendUID);
				Value_.Push("UserKey", UserKey);
			}
			public void Set(SSmDenyFriend Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				UserKey.Set(Obj_.UserKey);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(FriendUID) + "," + 
					SEnumChecker.GetStdName(UserKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(FriendUID, "FriendUID") + "," + 
					SEnumChecker.GetMemberName(UserKey, "UserKey");
			}
		}
		public class SMsDenyFriend : SProto
		{
			public TUID UID = default(TUID);
			public TUID FriendUID = default(TUID);
			public SKey UserKey = new SKey();
			public SMsDenyFriend()
			{
			}
			public SMsDenyFriend(SMsDenyFriend Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				UserKey = Obj_.UserKey;
			}
			public SMsDenyFriend(TUID UID_, TUID FriendUID_, SKey UserKey_)
			{
				UID = UID_;
				FriendUID = FriendUID_;
				UserKey = UserKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref FriendUID);
				Stream_.Pop(ref UserKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("FriendUID", ref FriendUID);
				Value_.Pop("UserKey", ref UserKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(FriendUID);
				Stream_.Push(UserKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("FriendUID", FriendUID);
				Value_.Push("UserKey", UserKey);
			}
			public void Set(SMsDenyFriend Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				UserKey.Set(Obj_.UserKey);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(FriendUID) + "," + 
					SEnumChecker.GetStdName(UserKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(FriendUID, "FriendUID") + "," + 
					SEnumChecker.GetMemberName(UserKey, "UserKey");
			}
		}
		public class SMsDenyFriendFail : SProto
		{
			public EGameRet GameRet = default(EGameRet);
			public SKey UserKey = new SKey();
			public SMsDenyFriendFail()
			{
			}
			public SMsDenyFriendFail(SMsDenyFriendFail Obj_)
			{
				GameRet = Obj_.GameRet;
				UserKey = Obj_.UserKey;
			}
			public SMsDenyFriendFail(EGameRet GameRet_, SKey UserKey_)
			{
				GameRet = GameRet_;
				UserKey = UserKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref GameRet);
				Stream_.Pop(ref UserKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("GameRet", ref GameRet);
				Value_.Pop("UserKey", ref UserKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(GameRet);
				Stream_.Push(UserKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("GameRet", GameRet);
				Value_.Push("UserKey", UserKey);
			}
			public void Set(SMsDenyFriendFail Obj_)
			{
				GameRet = Obj_.GameRet;
				UserKey.Set(Obj_.UserKey);
			}
			public override string StdName()
			{
				return 
					"rso.game.EGameRet" + "," + 
					SEnumChecker.GetStdName(UserKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(GameRet, "GameRet") + "," + 
					SEnumChecker.GetMemberName(UserKey, "UserKey");
			}
		}
		public class SScDenyFriend : SProto
		{
			public TUID FriendUID = default(TUID);
			public SScDenyFriend()
			{
			}
			public SScDenyFriend(SScDenyFriend Obj_)
			{
				FriendUID = Obj_.FriendUID;
			}
			public SScDenyFriend(TUID FriendUID_)
			{
				FriendUID = FriendUID_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref FriendUID);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("FriendUID", ref FriendUID);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(FriendUID);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("FriendUID", FriendUID);
			}
			public void Set(SScDenyFriend Obj_)
			{
				FriendUID = Obj_.FriendUID;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(FriendUID);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(FriendUID, "FriendUID");
			}
		}
		public class SCsBlockFriend : SProto
		{
			public TUID FriendUID = default(TUID);
			public SCsBlockFriend()
			{
			}
			public SCsBlockFriend(SCsBlockFriend Obj_)
			{
				FriendUID = Obj_.FriendUID;
			}
			public SCsBlockFriend(TUID FriendUID_)
			{
				FriendUID = FriendUID_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref FriendUID);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("FriendUID", ref FriendUID);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(FriendUID);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("FriendUID", FriendUID);
			}
			public void Set(SCsBlockFriend Obj_)
			{
				FriendUID = Obj_.FriendUID;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(FriendUID);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(FriendUID, "FriendUID");
			}
		}
		public class SSmBlockFriend : SProto
		{
			public TUID UID = default(TUID);
			public TUID FriendUID = default(TUID);
			public SKey UserKey = new SKey();
			public SSmBlockFriend()
			{
			}
			public SSmBlockFriend(SSmBlockFriend Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				UserKey = Obj_.UserKey;
			}
			public SSmBlockFriend(TUID UID_, TUID FriendUID_, SKey UserKey_)
			{
				UID = UID_;
				FriendUID = FriendUID_;
				UserKey = UserKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref FriendUID);
				Stream_.Pop(ref UserKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("FriendUID", ref FriendUID);
				Value_.Pop("UserKey", ref UserKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(FriendUID);
				Stream_.Push(UserKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("FriendUID", FriendUID);
				Value_.Push("UserKey", UserKey);
			}
			public void Set(SSmBlockFriend Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				UserKey.Set(Obj_.UserKey);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(FriendUID) + "," + 
					SEnumChecker.GetStdName(UserKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(FriendUID, "FriendUID") + "," + 
					SEnumChecker.GetMemberName(UserKey, "UserKey");
			}
		}
		public class SMsBlockFriend : SProto
		{
			public TUID UID = default(TUID);
			public TUID FriendUID = default(TUID);
			public EFriendState FriendState = default(EFriendState);
			public SKey UserKey = new SKey();
			public SMsBlockFriend()
			{
			}
			public SMsBlockFriend(SMsBlockFriend Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				FriendState = Obj_.FriendState;
				UserKey = Obj_.UserKey;
			}
			public SMsBlockFriend(TUID UID_, TUID FriendUID_, EFriendState FriendState_, SKey UserKey_)
			{
				UID = UID_;
				FriendUID = FriendUID_;
				FriendState = FriendState_;
				UserKey = UserKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref FriendUID);
				Stream_.Pop(ref FriendState);
				Stream_.Pop(ref UserKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("FriendUID", ref FriendUID);
				Value_.Pop("FriendState", ref FriendState);
				Value_.Pop("UserKey", ref UserKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(FriendUID);
				Stream_.Push(FriendState);
				Stream_.Push(UserKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("FriendUID", FriendUID);
				Value_.Push("FriendState", FriendState);
				Value_.Push("UserKey", UserKey);
			}
			public void Set(SMsBlockFriend Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				FriendState = Obj_.FriendState;
				UserKey.Set(Obj_.UserKey);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(FriendUID) + "," + 
					"rso.game.EFriendState" + "," + 
					SEnumChecker.GetStdName(UserKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(FriendUID, "FriendUID") + "," + 
					SEnumChecker.GetMemberName(FriendState, "FriendState") + "," + 
					SEnumChecker.GetMemberName(UserKey, "UserKey");
			}
		}
		public class SMsBlockFriendFail : SProto
		{
			public EGameRet GameRet = default(EGameRet);
			public SKey UserKey = new SKey();
			public SMsBlockFriendFail()
			{
			}
			public SMsBlockFriendFail(SMsBlockFriendFail Obj_)
			{
				GameRet = Obj_.GameRet;
				UserKey = Obj_.UserKey;
			}
			public SMsBlockFriendFail(EGameRet GameRet_, SKey UserKey_)
			{
				GameRet = GameRet_;
				UserKey = UserKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref GameRet);
				Stream_.Pop(ref UserKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("GameRet", ref GameRet);
				Value_.Pop("UserKey", ref UserKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(GameRet);
				Stream_.Push(UserKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("GameRet", GameRet);
				Value_.Push("UserKey", UserKey);
			}
			public void Set(SMsBlockFriendFail Obj_)
			{
				GameRet = Obj_.GameRet;
				UserKey.Set(Obj_.UserKey);
			}
			public override string StdName()
			{
				return 
					"rso.game.EGameRet" + "," + 
					SEnumChecker.GetStdName(UserKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(GameRet, "GameRet") + "," + 
					SEnumChecker.GetMemberName(UserKey, "UserKey");
			}
		}
		public class SScBlockFriend : SProto
		{
			public TUID FriendUID = default(TUID);
			public SScBlockFriend()
			{
			}
			public SScBlockFriend(SScBlockFriend Obj_)
			{
				FriendUID = Obj_.FriendUID;
			}
			public SScBlockFriend(TUID FriendUID_)
			{
				FriendUID = FriendUID_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref FriendUID);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("FriendUID", ref FriendUID);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(FriendUID);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("FriendUID", FriendUID);
			}
			public void Set(SScBlockFriend Obj_)
			{
				FriendUID = Obj_.FriendUID;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(FriendUID);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(FriendUID, "FriendUID");
			}
		}
		public class SCsUnBlockFriend : SProto
		{
			public TUID FriendUID = default(TUID);
			public SCsUnBlockFriend()
			{
			}
			public SCsUnBlockFriend(SCsUnBlockFriend Obj_)
			{
				FriendUID = Obj_.FriendUID;
			}
			public SCsUnBlockFriend(TUID FriendUID_)
			{
				FriendUID = FriendUID_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref FriendUID);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("FriendUID", ref FriendUID);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(FriendUID);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("FriendUID", FriendUID);
			}
			public void Set(SCsUnBlockFriend Obj_)
			{
				FriendUID = Obj_.FriendUID;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(FriendUID);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(FriendUID, "FriendUID");
			}
		}
		public class SSmUnBlockFriend : SProto
		{
			public TUID UID = default(TUID);
			public TUID FriendUID = default(TUID);
			public SKey UserKey = new SKey();
			public SSmUnBlockFriend()
			{
			}
			public SSmUnBlockFriend(SSmUnBlockFriend Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				UserKey = Obj_.UserKey;
			}
			public SSmUnBlockFriend(TUID UID_, TUID FriendUID_, SKey UserKey_)
			{
				UID = UID_;
				FriendUID = FriendUID_;
				UserKey = UserKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref FriendUID);
				Stream_.Pop(ref UserKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("FriendUID", ref FriendUID);
				Value_.Pop("UserKey", ref UserKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(FriendUID);
				Stream_.Push(UserKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("FriendUID", FriendUID);
				Value_.Push("UserKey", UserKey);
			}
			public void Set(SSmUnBlockFriend Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				UserKey.Set(Obj_.UserKey);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(FriendUID) + "," + 
					SEnumChecker.GetStdName(UserKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(FriendUID, "FriendUID") + "," + 
					SEnumChecker.GetMemberName(UserKey, "UserKey");
			}
		}
		public class SMsUnBlockFriend : SProto
		{
			public TUID UID = default(TUID);
			public TUID FriendUID = default(TUID);
			public EFriendState FriendState = default(EFriendState);
			public SKey UserKey = new SKey();
			public SMsUnBlockFriend()
			{
			}
			public SMsUnBlockFriend(SMsUnBlockFriend Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				FriendState = Obj_.FriendState;
				UserKey = Obj_.UserKey;
			}
			public SMsUnBlockFriend(TUID UID_, TUID FriendUID_, EFriendState FriendState_, SKey UserKey_)
			{
				UID = UID_;
				FriendUID = FriendUID_;
				FriendState = FriendState_;
				UserKey = UserKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref FriendUID);
				Stream_.Pop(ref FriendState);
				Stream_.Pop(ref UserKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("FriendUID", ref FriendUID);
				Value_.Pop("FriendState", ref FriendState);
				Value_.Pop("UserKey", ref UserKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(FriendUID);
				Stream_.Push(FriendState);
				Stream_.Push(UserKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("FriendUID", FriendUID);
				Value_.Push("FriendState", FriendState);
				Value_.Push("UserKey", UserKey);
			}
			public void Set(SMsUnBlockFriend Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				FriendState = Obj_.FriendState;
				UserKey.Set(Obj_.UserKey);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(FriendUID) + "," + 
					"rso.game.EFriendState" + "," + 
					SEnumChecker.GetStdName(UserKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(FriendUID, "FriendUID") + "," + 
					SEnumChecker.GetMemberName(FriendState, "FriendState") + "," + 
					SEnumChecker.GetMemberName(UserKey, "UserKey");
			}
		}
		public class SMsUnBlockFriendFail : SProto
		{
			public EGameRet GameRet = default(EGameRet);
			public SKey UserKey = new SKey();
			public SMsUnBlockFriendFail()
			{
			}
			public SMsUnBlockFriendFail(SMsUnBlockFriendFail Obj_)
			{
				GameRet = Obj_.GameRet;
				UserKey = Obj_.UserKey;
			}
			public SMsUnBlockFriendFail(EGameRet GameRet_, SKey UserKey_)
			{
				GameRet = GameRet_;
				UserKey = UserKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref GameRet);
				Stream_.Pop(ref UserKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("GameRet", ref GameRet);
				Value_.Pop("UserKey", ref UserKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(GameRet);
				Stream_.Push(UserKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("GameRet", GameRet);
				Value_.Push("UserKey", UserKey);
			}
			public void Set(SMsUnBlockFriendFail Obj_)
			{
				GameRet = Obj_.GameRet;
				UserKey.Set(Obj_.UserKey);
			}
			public override string StdName()
			{
				return 
					"rso.game.EGameRet" + "," + 
					SEnumChecker.GetStdName(UserKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(GameRet, "GameRet") + "," + 
					SEnumChecker.GetMemberName(UserKey, "UserKey");
			}
		}
		public class SScUnBlockFriend : SProto
		{
			public TUID FriendUID = default(TUID);
			public SScUnBlockFriend()
			{
			}
			public SScUnBlockFriend(SScUnBlockFriend Obj_)
			{
				FriendUID = Obj_.FriendUID;
			}
			public SScUnBlockFriend(TUID FriendUID_)
			{
				FriendUID = FriendUID_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref FriendUID);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("FriendUID", ref FriendUID);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(FriendUID);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("FriendUID", FriendUID);
			}
			public void Set(SScUnBlockFriend Obj_)
			{
				FriendUID = Obj_.FriendUID;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(FriendUID);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(FriendUID, "FriendUID");
			}
		}
		public class SCsMessageSend : SProto
		{
			public TUID ToUID = default(TUID);
			public TMessage Message = string.Empty;
			public SCsMessageSend()
			{
			}
			public SCsMessageSend(SCsMessageSend Obj_)
			{
				ToUID = Obj_.ToUID;
				Message = Obj_.Message;
			}
			public SCsMessageSend(TUID ToUID_, TMessage Message_)
			{
				ToUID = ToUID_;
				Message = Message_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ToUID);
				Stream_.Pop(ref Message);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ToUID", ref ToUID);
				Value_.Pop("Message", ref Message);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ToUID);
				Stream_.Push(Message);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ToUID", ToUID);
				Value_.Push("Message", Message);
			}
			public void Set(SCsMessageSend Obj_)
			{
				ToUID = Obj_.ToUID;
				Message = Obj_.Message;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ToUID) + "," + 
					SEnumChecker.GetStdName(Message);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ToUID, "ToUID") + "," + 
					SEnumChecker.GetMemberName(Message, "Message");
			}
		}
		public class SSsMessageSend : SProto
		{
			public TUID FromUID = default(TUID);
			public TUID ToUID = default(TUID);
			public TMessage Message = string.Empty;
			public SSsMessageSend()
			{
			}
			public SSsMessageSend(SSsMessageSend Obj_)
			{
				FromUID = Obj_.FromUID;
				ToUID = Obj_.ToUID;
				Message = Obj_.Message;
			}
			public SSsMessageSend(TUID FromUID_, TUID ToUID_, TMessage Message_)
			{
				FromUID = FromUID_;
				ToUID = ToUID_;
				Message = Message_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref FromUID);
				Stream_.Pop(ref ToUID);
				Stream_.Pop(ref Message);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("FromUID", ref FromUID);
				Value_.Pop("ToUID", ref ToUID);
				Value_.Pop("Message", ref Message);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(FromUID);
				Stream_.Push(ToUID);
				Stream_.Push(Message);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("FromUID", FromUID);
				Value_.Push("ToUID", ToUID);
				Value_.Push("Message", Message);
			}
			public void Set(SSsMessageSend Obj_)
			{
				FromUID = Obj_.FromUID;
				ToUID = Obj_.ToUID;
				Message = Obj_.Message;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(FromUID) + "," + 
					SEnumChecker.GetStdName(ToUID) + "," + 
					SEnumChecker.GetStdName(Message);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(FromUID, "FromUID") + "," + 
					SEnumChecker.GetMemberName(ToUID, "ToUID") + "," + 
					SEnumChecker.GetMemberName(Message, "Message");
			}
		}
		public class SScMessageReceived : SProto
		{
			public TUID FromUID = default(TUID);
			public TMessage Message = string.Empty;
			public SScMessageReceived()
			{
			}
			public SScMessageReceived(SScMessageReceived Obj_)
			{
				FromUID = Obj_.FromUID;
				Message = Obj_.Message;
			}
			public SScMessageReceived(TUID FromUID_, TMessage Message_)
			{
				FromUID = FromUID_;
				Message = Message_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref FromUID);
				Stream_.Pop(ref Message);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("FromUID", ref FromUID);
				Value_.Pop("Message", ref Message);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(FromUID);
				Stream_.Push(Message);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("FromUID", FromUID);
				Value_.Push("Message", Message);
			}
			public void Set(SScMessageReceived Obj_)
			{
				FromUID = Obj_.FromUID;
				Message = Obj_.Message;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(FromUID) + "," + 
					SEnumChecker.GetStdName(Message);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(FromUID, "FromUID") + "," + 
					SEnumChecker.GetMemberName(Message, "Message");
			}
		}
		public class SCsChangeState : SProto
		{
			public TState State = default(TState);
			public SCsChangeState()
			{
			}
			public SCsChangeState(SCsChangeState Obj_)
			{
				State = Obj_.State;
			}
			public SCsChangeState(TState State_)
			{
				State = State_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref State);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("State", ref State);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(State);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("State", State);
			}
			public void Set(SCsChangeState Obj_)
			{
				State = Obj_.State;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(State);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(State, "State");
			}
		}
		public class SSmChangeState : SProto
		{
			public TUID UID = default(TUID);
			public TState State = default(TState);
			public SKey UserKey = new SKey();
			public SSmChangeState()
			{
			}
			public SSmChangeState(SSmChangeState Obj_)
			{
				UID = Obj_.UID;
				State = Obj_.State;
				UserKey = Obj_.UserKey;
			}
			public SSmChangeState(TUID UID_, TState State_, SKey UserKey_)
			{
				UID = UID_;
				State = State_;
				UserKey = UserKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref State);
				Stream_.Pop(ref UserKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("State", ref State);
				Value_.Pop("UserKey", ref UserKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(State);
				Stream_.Push(UserKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("State", State);
				Value_.Push("UserKey", UserKey);
			}
			public void Set(SSmChangeState Obj_)
			{
				UID = Obj_.UID;
				State = Obj_.State;
				UserKey.Set(Obj_.UserKey);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(State) + "," + 
					SEnumChecker.GetStdName(UserKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(State, "State") + "," + 
					SEnumChecker.GetMemberName(UserKey, "UserKey");
			}
		}
		public class SMsChangeState : SProto
		{
			public TState State = default(TState);
			public SKey UserKey = new SKey();
			public SMsChangeState()
			{
			}
			public SMsChangeState(SMsChangeState Obj_)
			{
				State = Obj_.State;
				UserKey = Obj_.UserKey;
			}
			public SMsChangeState(TState State_, SKey UserKey_)
			{
				State = State_;
				UserKey = UserKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref State);
				Stream_.Pop(ref UserKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("State", ref State);
				Value_.Pop("UserKey", ref UserKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(State);
				Stream_.Push(UserKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("State", State);
				Value_.Push("UserKey", UserKey);
			}
			public void Set(SMsChangeState Obj_)
			{
				State = Obj_.State;
				UserKey.Set(Obj_.UserKey);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(State) + "," + 
					SEnumChecker.GetStdName(UserKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(State, "State") + "," + 
					SEnumChecker.GetMemberName(UserKey, "UserKey");
			}
		}
		public class SMsChangeStateFail : SProto
		{
			public EGameRet GameRet = default(EGameRet);
			public SKey UserKey = new SKey();
			public SMsChangeStateFail()
			{
			}
			public SMsChangeStateFail(SMsChangeStateFail Obj_)
			{
				GameRet = Obj_.GameRet;
				UserKey = Obj_.UserKey;
			}
			public SMsChangeStateFail(EGameRet GameRet_, SKey UserKey_)
			{
				GameRet = GameRet_;
				UserKey = UserKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref GameRet);
				Stream_.Pop(ref UserKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("GameRet", ref GameRet);
				Value_.Pop("UserKey", ref UserKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(GameRet);
				Stream_.Push(UserKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("GameRet", GameRet);
				Value_.Push("UserKey", UserKey);
			}
			public void Set(SMsChangeStateFail Obj_)
			{
				GameRet = Obj_.GameRet;
				UserKey.Set(Obj_.UserKey);
			}
			public override string StdName()
			{
				return 
					"rso.game.EGameRet" + "," + 
					SEnumChecker.GetStdName(UserKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(GameRet, "GameRet") + "," + 
					SEnumChecker.GetMemberName(UserKey, "UserKey");
			}
		}
		public class SScChangeState : SProto
		{
			public TState State = default(TState);
			public SScChangeState()
			{
			}
			public SScChangeState(SScChangeState Obj_)
			{
				State = Obj_.State;
			}
			public SScChangeState(TState State_)
			{
				State = State_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref State);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("State", ref State);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(State);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("State", State);
			}
			public void Set(SScChangeState Obj_)
			{
				State = Obj_.State;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(State);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(State, "State");
			}
		}
		public class SUIDFriendInfo : SProto
		{
			public TUID UID = default(TUID);
			public SFriendInfo FriendInfo = new SFriendInfo();
			public SUIDFriendInfo()
			{
			}
			public SUIDFriendInfo(SUIDFriendInfo Obj_)
			{
				UID = Obj_.UID;
				FriendInfo = Obj_.FriendInfo;
			}
			public SUIDFriendInfo(TUID UID_, SFriendInfo FriendInfo_)
			{
				UID = UID_;
				FriendInfo = FriendInfo_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref FriendInfo);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("FriendInfo", ref FriendInfo);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(FriendInfo);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("FriendInfo", FriendInfo);
			}
			public void Set(SUIDFriendInfo Obj_)
			{
				UID = Obj_.UID;
				FriendInfo.Set(Obj_.FriendInfo);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(FriendInfo);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(FriendInfo, "FriendInfo");
			}
		}
		public class SMmFriendStateChanged : SProto
		{
			public TUIDFriendInfos UIDFriendInfos = new TUIDFriendInfos();
			public TUID FriendUID = default(TUID);
			public SMmFriendStateChanged()
			{
			}
			public SMmFriendStateChanged(SMmFriendStateChanged Obj_)
			{
				UIDFriendInfos = Obj_.UIDFriendInfos;
				FriendUID = Obj_.FriendUID;
			}
			public SMmFriendStateChanged(TUIDFriendInfos UIDFriendInfos_, TUID FriendUID_)
			{
				UIDFriendInfos = UIDFriendInfos_;
				FriendUID = FriendUID_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UIDFriendInfos);
				Stream_.Pop(ref FriendUID);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UIDFriendInfos", ref UIDFriendInfos);
				Value_.Pop("FriendUID", ref FriendUID);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UIDFriendInfos);
				Stream_.Push(FriendUID);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UIDFriendInfos", UIDFriendInfos);
				Value_.Push("FriendUID", FriendUID);
			}
			public void Set(SMmFriendStateChanged Obj_)
			{
				UIDFriendInfos = Obj_.UIDFriendInfos;
				FriendUID = Obj_.FriendUID;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UIDFriendInfos) + "," + 
					SEnumChecker.GetStdName(FriendUID);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UIDFriendInfos, "UIDFriendInfos") + "," + 
					SEnumChecker.GetMemberName(FriendUID, "FriendUID");
			}
		}
		public class SMsFriendStateChanged : SProto
		{
			public TUIDFriendInfos UIDFriendInfos = new TUIDFriendInfos();
			public TUID FriendUID = default(TUID);
			public SMsFriendStateChanged()
			{
			}
			public SMsFriendStateChanged(SMsFriendStateChanged Obj_)
			{
				UIDFriendInfos = Obj_.UIDFriendInfos;
				FriendUID = Obj_.FriendUID;
			}
			public SMsFriendStateChanged(TUIDFriendInfos UIDFriendInfos_, TUID FriendUID_)
			{
				UIDFriendInfos = UIDFriendInfos_;
				FriendUID = FriendUID_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UIDFriendInfos);
				Stream_.Pop(ref FriendUID);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UIDFriendInfos", ref UIDFriendInfos);
				Value_.Pop("FriendUID", ref FriendUID);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UIDFriendInfos);
				Stream_.Push(FriendUID);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UIDFriendInfos", UIDFriendInfos);
				Value_.Push("FriendUID", FriendUID);
			}
			public void Set(SMsFriendStateChanged Obj_)
			{
				UIDFriendInfos = Obj_.UIDFriendInfos;
				FriendUID = Obj_.FriendUID;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UIDFriendInfos) + "," + 
					SEnumChecker.GetStdName(FriendUID);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UIDFriendInfos, "UIDFriendInfos") + "," + 
					SEnumChecker.GetMemberName(FriendUID, "FriendUID");
			}
		}
		public class SScFriendStateChanged : SProto
		{
			public TUID FriendUID = default(TUID);
			public TState FriendState = default(TState);
			public SScFriendStateChanged()
			{
			}
			public SScFriendStateChanged(SScFriendStateChanged Obj_)
			{
				FriendUID = Obj_.FriendUID;
				FriendState = Obj_.FriendState;
			}
			public SScFriendStateChanged(TUID FriendUID_, TState FriendState_)
			{
				FriendUID = FriendUID_;
				FriendState = FriendState_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref FriendUID);
				Stream_.Pop(ref FriendState);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("FriendUID", ref FriendUID);
				Value_.Pop("FriendState", ref FriendState);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(FriendUID);
				Stream_.Push(FriendState);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("FriendUID", FriendUID);
				Value_.Push("FriendState", FriendState);
			}
			public void Set(SScFriendStateChanged Obj_)
			{
				FriendUID = Obj_.FriendUID;
				FriendState = Obj_.FriendState;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(FriendUID) + "," + 
					SEnumChecker.GetStdName(FriendState);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(FriendUID, "FriendUID") + "," + 
					SEnumChecker.GetMemberName(FriendState, "FriendState");
			}
		}
		public class SMmFriendStateChangedRenew : SProto
		{
			public List<SMmFriendStateChanged> MmFriendStateChangeds = new List<SMmFriendStateChanged>();
			public SMmFriendStateChangedRenew()
			{
			}
			public SMmFriendStateChangedRenew(SMmFriendStateChangedRenew Obj_)
			{
				MmFriendStateChangeds = Obj_.MmFriendStateChangeds;
			}
			public SMmFriendStateChangedRenew(List<SMmFriendStateChanged> MmFriendStateChangeds_)
			{
				MmFriendStateChangeds = MmFriendStateChangeds_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref MmFriendStateChangeds);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("MmFriendStateChangeds", ref MmFriendStateChangeds);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(MmFriendStateChangeds);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("MmFriendStateChangeds", MmFriendStateChangeds);
			}
			public void Set(SMmFriendStateChangedRenew Obj_)
			{
				MmFriendStateChangeds = Obj_.MmFriendStateChangeds;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(MmFriendStateChangeds);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(MmFriendStateChangeds, "MmFriendStateChangeds");
			}
		}
		public class SMsFriendStateChangedOffline : SProto
		{
			public Dictionary<TUID,List<TUID>> Friends = new Dictionary<TUID,List<TUID>>();
			public SMsFriendStateChangedOffline()
			{
			}
			public SMsFriendStateChangedOffline(SMsFriendStateChangedOffline Obj_)
			{
				Friends = Obj_.Friends;
			}
			public SMsFriendStateChangedOffline(Dictionary<TUID,List<TUID>> Friends_)
			{
				Friends = Friends_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Friends);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Friends", ref Friends);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Friends);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Friends", Friends);
			}
			public void Set(SMsFriendStateChangedOffline Obj_)
			{
				Friends = Obj_.Friends;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Friends);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Friends, "Friends");
			}
		}
		public class SScFriendStateChangedOffline : SProto
		{
			public List<TUID> Friends = new List<TUID>();
			public SScFriendStateChangedOffline()
			{
			}
			public SScFriendStateChangedOffline(SScFriendStateChangedOffline Obj_)
			{
				Friends = Obj_.Friends;
			}
			public SScFriendStateChangedOffline(List<TUID> Friends_)
			{
				Friends = Friends_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Friends);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Friends", ref Friends);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Friends);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Friends", Friends);
			}
			public void Set(SScFriendStateChangedOffline Obj_)
			{
				Friends = Obj_.Friends;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Friends);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Friends, "Friends");
			}
		}
		public class SAmPunish : SProto
		{
			public TUID UID = default(TUID);
			public TimePoint EndTime = default(TimePoint);
			public SAmPunish()
			{
			}
			public SAmPunish(SAmPunish Obj_)
			{
				UID = Obj_.UID;
				EndTime = Obj_.EndTime;
			}
			public SAmPunish(TUID UID_, TimePoint EndTime_)
			{
				UID = UID_;
				EndTime = EndTime_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref EndTime);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("EndTime", ref EndTime);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(EndTime);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("EndTime", EndTime);
			}
			public void Set(SAmPunish Obj_)
			{
				UID = Obj_.UID;
				EndTime = Obj_.EndTime;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(EndTime);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(EndTime, "EndTime");
			}
		}
		public class SMsPunish : SProto
		{
			public TUID UID = default(TUID);
			public TimePoint EndTime = default(TimePoint);
			public SMsPunish()
			{
			}
			public SMsPunish(SMsPunish Obj_)
			{
				UID = Obj_.UID;
				EndTime = Obj_.EndTime;
			}
			public SMsPunish(TUID UID_, TimePoint EndTime_)
			{
				UID = UID_;
				EndTime = EndTime_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref EndTime);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("EndTime", ref EndTime);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(EndTime);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("EndTime", EndTime);
			}
			public void Set(SMsPunish Obj_)
			{
				UID = Obj_.UID;
				EndTime = Obj_.EndTime;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(EndTime);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(EndTime, "EndTime");
			}
		}
		public enum EDataBaseError
		{
			InvalidID=50003,
		}
		public enum EAuthProtoDB
		{
			Create,
			Login,
			AddFriendGetUID,
			ChangeNick,
			Check,
		}
		public class SDummyOutDb : SProto
		{
			public override void Push(CStream Stream_)
			{
			}
			public override void Push(JsonDataObject Value_)
			{
			}
			public override void Pop(CStream Stream_)
			{
			}
			public override void Pop(JsonDataObject Value_)
			{
			}
			public override string StdName()
			{
				return "";
			}
			public override string MemberName()
			{
				return "";
			}
		}
		public class SAuthLoginInDb : SProto
		{
			public TID ID = string.Empty;
			public SAuthLoginInDb()
			{
			}
			public SAuthLoginInDb(SAuthLoginInDb Obj_)
			{
				ID = Obj_.ID;
			}
			public SAuthLoginInDb(TID ID_)
			{
				ID = ID_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ID);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ID", ref ID);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ID);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ID", ID);
			}
			public void Set(SAuthLoginInDb Obj_)
			{
				ID = Obj_.ID;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ID);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ID, "ID");
			}
		}
		public class SAuthCreateInDb : SAuthLoginInDb
		{
			public TNick Nick = string.Empty;
			public SAuthCreateInDb()
			{
			}
			public SAuthCreateInDb(SAuthCreateInDb Obj_) : base(Obj_)
			{
				Nick = Obj_.Nick;
			}
			public SAuthCreateInDb(SAuthLoginInDb Super_, TNick Nick_) : base(Super_)
			{
				Nick = Nick_;
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
				Stream_.Pop(ref Nick);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
				Value_.Pop("Nick", ref Nick);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
				Stream_.Push(Nick);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
				Value_.Push("Nick", Nick);
			}
			public void Set(SAuthCreateInDb Obj_)
			{
				base.Set(Obj_);
				Nick = Obj_.Nick;
			}
			public override string StdName()
			{
				return 
					base.StdName() + "," + 
					SEnumChecker.GetStdName(Nick);
			}
			public override string MemberName()
			{
				return 
					base.MemberName() + "," + 
					SEnumChecker.GetMemberName(Nick, "Nick");
			}
		}
		public class SAuthAddFriendGetUIDInDb : SProto
		{
			public TNick Nick = string.Empty;
			public SAuthAddFriendGetUIDInDb()
			{
			}
			public SAuthAddFriendGetUIDInDb(SAuthAddFriendGetUIDInDb Obj_)
			{
				Nick = Obj_.Nick;
			}
			public SAuthAddFriendGetUIDInDb(TNick Nick_)
			{
				Nick = Nick_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Nick);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Nick", ref Nick);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Nick);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Nick", Nick);
			}
			public void Set(SAuthAddFriendGetUIDInDb Obj_)
			{
				Nick = Obj_.Nick;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Nick);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Nick, "Nick");
			}
		}
		public class SAuthAddFriendGetUIDOutDb : SProto
		{
			public List<TUID> UIDs = new List<TUID>();
			public SAuthAddFriendGetUIDOutDb()
			{
			}
			public SAuthAddFriendGetUIDOutDb(SAuthAddFriendGetUIDOutDb Obj_)
			{
				UIDs = Obj_.UIDs;
			}
			public SAuthAddFriendGetUIDOutDb(List<TUID> UIDs_)
			{
				UIDs = UIDs_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UIDs);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UIDs", ref UIDs);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UIDs);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UIDs", UIDs);
			}
			public void Set(SAuthAddFriendGetUIDOutDb Obj_)
			{
				UIDs = Obj_.UIDs;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UIDs);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UIDs, "UIDs");
			}
		}
		public class SAuthChangeNickInDb : SProto
		{
			public TUID UID = default(TUID);
			public TNick Nick = string.Empty;
			public SAuthChangeNickInDb()
			{
			}
			public SAuthChangeNickInDb(SAuthChangeNickInDb Obj_)
			{
				UID = Obj_.UID;
				Nick = Obj_.Nick;
			}
			public SAuthChangeNickInDb(TUID UID_, TNick Nick_)
			{
				UID = UID_;
				Nick = Nick_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref Nick);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("Nick", ref Nick);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(Nick);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("Nick", Nick);
			}
			public void Set(SAuthChangeNickInDb Obj_)
			{
				UID = Obj_.UID;
				Nick = Obj_.Nick;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(Nick);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(Nick, "Nick");
			}
		}
		public class SAuthCheckInDb : SAuthLoginInDb
		{
			public SAuthCheckInDb()
			{
			}
			public SAuthCheckInDb(SAuthCheckInDb Obj_) : base(Obj_)
			{
			}
			public SAuthCheckInDb(SAuthLoginInDb Super_) : base(Super_)
			{
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
			}
			public void Set(SAuthCheckInDb Obj_)
			{
				base.Set(Obj_);
			}
			public override string StdName()
			{
				return 
					base.StdName();
			}
			public override string MemberName()
			{
				return 
					base.MemberName();
			}
		}
		public class SAuthUser : SProto
		{
			public TUID UID = default(TUID);
			public TNick Nick = string.Empty;
			public SAuthUser()
			{
			}
			public SAuthUser(SAuthUser Obj_)
			{
				UID = Obj_.UID;
				Nick = Obj_.Nick;
			}
			public SAuthUser(TUID UID_, TNick Nick_)
			{
				UID = UID_;
				Nick = Nick_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref Nick);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("Nick", ref Nick);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(Nick);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("Nick", Nick);
			}
			public void Set(SAuthUser Obj_)
			{
				UID = Obj_.UID;
				Nick = Obj_.Nick;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(Nick);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(Nick, "Nick");
			}
		}
		public class SAuthLoginOutDb : SProto
		{
			public List<SAuthUser> Users = new List<SAuthUser>();
			public SAuthLoginOutDb()
			{
			}
			public SAuthLoginOutDb(SAuthLoginOutDb Obj_)
			{
				Users = Obj_.Users;
			}
			public SAuthLoginOutDb(List<SAuthUser> Users_)
			{
				Users = Users_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Users);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Users", ref Users);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Users);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Users", Users);
			}
			public void Set(SAuthLoginOutDb Obj_)
			{
				Users = Obj_.Users;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Users);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Users, "Users");
			}
		}
		public enum EMasterProtoDB
		{
			Create,
			Login,
			ChangeNick,
			Check,
			Punish,
			AddFriendBegin,
			AddFriendRequest,
			AddFriendEnd,
			AddFriendFail,
			AllowFriend,
			DenyFriend,
			BlockFriend,
			UnBlockFriend,
			ChangeState,
		}
		public class SMasterCreateInDb : SProto
		{
			public TUID UID = default(TUID);
			public SAccount Account = new SAccount();
			public TState State = default(TState);
			public SMasterCreateInDb()
			{
			}
			public SMasterCreateInDb(SMasterCreateInDb Obj_)
			{
				UID = Obj_.UID;
				Account = Obj_.Account;
				State = Obj_.State;
			}
			public SMasterCreateInDb(TUID UID_, SAccount Account_, TState State_)
			{
				UID = UID_;
				Account = Account_;
				State = State_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref Account);
				Stream_.Pop(ref State);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("Account", ref Account);
				Value_.Pop("State", ref State);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(Account);
				Stream_.Push(State);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("Account", Account);
				Value_.Push("State", State);
			}
			public void Set(SMasterCreateInDb Obj_)
			{
				UID = Obj_.UID;
				Account.Set(Obj_.Account);
				State = Obj_.State;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(Account) + "," + 
					SEnumChecker.GetStdName(State);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(Account, "Account") + "," + 
					SEnumChecker.GetMemberName(State, "State");
			}
		}
		public class SMasterCreateOutDb : SProto
		{
			public override void Push(CStream Stream_)
			{
			}
			public override void Push(JsonDataObject Value_)
			{
			}
			public override void Pop(CStream Stream_)
			{
			}
			public override void Pop(JsonDataObject Value_)
			{
			}
			public override string StdName()
			{
				return "";
			}
			public override string MemberName()
			{
				return "";
			}
		}
		public class SMasterLoginInDb : SProto
		{
			public TUID UID = default(TUID);
			public TID ID = string.Empty;
			public SMasterLoginInDb()
			{
			}
			public SMasterLoginInDb(SMasterLoginInDb Obj_)
			{
				UID = Obj_.UID;
				ID = Obj_.ID;
			}
			public SMasterLoginInDb(TUID UID_, TID ID_)
			{
				UID = UID_;
				ID = ID_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref ID);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("ID", ref ID);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(ID);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("ID", ID);
			}
			public void Set(SMasterLoginInDb Obj_)
			{
				UID = Obj_.UID;
				ID = Obj_.ID;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(ID);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(ID, "ID");
			}
		}
		public class SMasterUser : SProto
		{
			public TNick Nick = string.Empty;
			public TState State = default(TState);
			public TimePoint PunishEndTime = default(TimePoint);
			public SMasterUser()
			{
			}
			public SMasterUser(SMasterUser Obj_)
			{
				Nick = Obj_.Nick;
				State = Obj_.State;
				PunishEndTime = Obj_.PunishEndTime;
			}
			public SMasterUser(TNick Nick_, TState State_, TimePoint PunishEndTime_)
			{
				Nick = Nick_;
				State = State_;
				PunishEndTime = PunishEndTime_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Nick);
				Stream_.Pop(ref State);
				Stream_.Pop(ref PunishEndTime);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Nick", ref Nick);
				Value_.Pop("State", ref State);
				Value_.Pop("PunishEndTime", ref PunishEndTime);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Nick);
				Stream_.Push(State);
				Stream_.Push(PunishEndTime);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Nick", Nick);
				Value_.Push("State", State);
				Value_.Push("PunishEndTime", PunishEndTime);
			}
			public void Set(SMasterUser Obj_)
			{
				Nick = Obj_.Nick;
				State = Obj_.State;
				PunishEndTime = Obj_.PunishEndTime;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Nick) + "," + 
					SEnumChecker.GetStdName(State) + "," + 
					SEnumChecker.GetStdName(PunishEndTime);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Nick, "Nick") + "," + 
					SEnumChecker.GetMemberName(State, "State") + "," + 
					SEnumChecker.GetMemberName(PunishEndTime, "PunishEndTime");
			}
		}
		public class SMasterLoginOutDb : SProto
		{
			public List<SMasterUser> Users = new List<SMasterUser>();
			public TFriendDBs Friends = new TFriendDBs();
			public SMasterLoginOutDb()
			{
			}
			public SMasterLoginOutDb(SMasterLoginOutDb Obj_)
			{
				Users = Obj_.Users;
				Friends = Obj_.Friends;
			}
			public SMasterLoginOutDb(List<SMasterUser> Users_, TFriendDBs Friends_)
			{
				Users = Users_;
				Friends = Friends_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref Users);
				Stream_.Pop(ref Friends);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("Users", ref Users);
				Value_.Pop("Friends", ref Friends);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(Users);
				Stream_.Push(Friends);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("Users", Users);
				Value_.Push("Friends", Friends);
			}
			public void Set(SMasterLoginOutDb Obj_)
			{
				Users = Obj_.Users;
				Friends = Obj_.Friends;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(Users) + "," + 
					SEnumChecker.GetStdName(Friends);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Users, "Users") + "," + 
					SEnumChecker.GetMemberName(Friends, "Friends");
			}
		}
		public class SMasterChangeNickInDb : SProto
		{
			public TUID UID = default(TUID);
			public TNick Nick = string.Empty;
			public SMasterChangeNickInDb()
			{
			}
			public SMasterChangeNickInDb(SMasterChangeNickInDb Obj_)
			{
				UID = Obj_.UID;
				Nick = Obj_.Nick;
			}
			public SMasterChangeNickInDb(TUID UID_, TNick Nick_)
			{
				UID = UID_;
				Nick = Nick_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref Nick);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("Nick", ref Nick);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(Nick);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("Nick", Nick);
			}
			public void Set(SMasterChangeNickInDb Obj_)
			{
				UID = Obj_.UID;
				Nick = Obj_.Nick;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(Nick);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(Nick, "Nick");
			}
		}
		public class SMasterAddFriendBeginInDb : SProto
		{
			public TUID UID = default(TUID);
			public TUID FriendUID = default(TUID);
			public TNick FriendNick = string.Empty;
			public EFriendState FriendState = default(EFriendState);
			public SMasterAddFriendBeginInDb()
			{
			}
			public SMasterAddFriendBeginInDb(SMasterAddFriendBeginInDb Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				FriendNick = Obj_.FriendNick;
				FriendState = Obj_.FriendState;
			}
			public SMasterAddFriendBeginInDb(TUID UID_, TUID FriendUID_, TNick FriendNick_, EFriendState FriendState_)
			{
				UID = UID_;
				FriendUID = FriendUID_;
				FriendNick = FriendNick_;
				FriendState = FriendState_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref FriendUID);
				Stream_.Pop(ref FriendNick);
				Stream_.Pop(ref FriendState);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("FriendUID", ref FriendUID);
				Value_.Pop("FriendNick", ref FriendNick);
				Value_.Pop("FriendState", ref FriendState);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(FriendUID);
				Stream_.Push(FriendNick);
				Stream_.Push(FriendState);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("FriendUID", FriendUID);
				Value_.Push("FriendNick", FriendNick);
				Value_.Push("FriendState", FriendState);
			}
			public void Set(SMasterAddFriendBeginInDb Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				FriendNick = Obj_.FriendNick;
				FriendState = Obj_.FriendState;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(FriendUID) + "," + 
					SEnumChecker.GetStdName(FriendNick) + "," + 
					"rso.game.EFriendState";
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(FriendUID, "FriendUID") + "," + 
					SEnumChecker.GetMemberName(FriendNick, "FriendNick") + "," + 
					SEnumChecker.GetMemberName(FriendState, "FriendState");
			}
		}
		public class SMasterAddFriendRequestInDb : SProto
		{
			public TUID UID = default(TUID);
			public TUID FriendUID = default(TUID);
			public TNick FriendNick = string.Empty;
			public EFriendState FriendState = default(EFriendState);
			public SMasterAddFriendRequestInDb()
			{
			}
			public SMasterAddFriendRequestInDb(SMasterAddFriendRequestInDb Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				FriendNick = Obj_.FriendNick;
				FriendState = Obj_.FriendState;
			}
			public SMasterAddFriendRequestInDb(TUID UID_, TUID FriendUID_, TNick FriendNick_, EFriendState FriendState_)
			{
				UID = UID_;
				FriendUID = FriendUID_;
				FriendNick = FriendNick_;
				FriendState = FriendState_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref FriendUID);
				Stream_.Pop(ref FriendNick);
				Stream_.Pop(ref FriendState);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("FriendUID", ref FriendUID);
				Value_.Pop("FriendNick", ref FriendNick);
				Value_.Pop("FriendState", ref FriendState);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(FriendUID);
				Stream_.Push(FriendNick);
				Stream_.Push(FriendState);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("FriendUID", FriendUID);
				Value_.Push("FriendNick", FriendNick);
				Value_.Push("FriendState", FriendState);
			}
			public void Set(SMasterAddFriendRequestInDb Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				FriendNick = Obj_.FriendNick;
				FriendState = Obj_.FriendState;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(FriendUID) + "," + 
					SEnumChecker.GetStdName(FriendNick) + "," + 
					"rso.game.EFriendState";
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(FriendUID, "FriendUID") + "," + 
					SEnumChecker.GetMemberName(FriendNick, "FriendNick") + "," + 
					SEnumChecker.GetMemberName(FriendState, "FriendState");
			}
		}
		public class SMasterAddFriendRequestOutDb : SProto
		{
			public List<Int32> RowCounts = new List<Int32>();
			public SMasterAddFriendRequestOutDb()
			{
			}
			public SMasterAddFriendRequestOutDb(SMasterAddFriendRequestOutDb Obj_)
			{
				RowCounts = Obj_.RowCounts;
			}
			public SMasterAddFriendRequestOutDb(List<Int32> RowCounts_)
			{
				RowCounts = RowCounts_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref RowCounts);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("RowCounts", ref RowCounts);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(RowCounts);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("RowCounts", RowCounts);
			}
			public void Set(SMasterAddFriendRequestOutDb Obj_)
			{
				RowCounts = Obj_.RowCounts;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(RowCounts);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(RowCounts, "RowCounts");
			}
		}
		public class SMasterAddFriendEndInDb : SProto
		{
			public TUID UID = default(TUID);
			public TUID FriendUID = default(TUID);
			public EFriendState FriendState = default(EFriendState);
			public SMasterAddFriendEndInDb()
			{
			}
			public SMasterAddFriendEndInDb(SMasterAddFriendEndInDb Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				FriendState = Obj_.FriendState;
			}
			public SMasterAddFriendEndInDb(TUID UID_, TUID FriendUID_, EFriendState FriendState_)
			{
				UID = UID_;
				FriendUID = FriendUID_;
				FriendState = FriendState_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref FriendUID);
				Stream_.Pop(ref FriendState);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("FriendUID", ref FriendUID);
				Value_.Pop("FriendState", ref FriendState);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(FriendUID);
				Stream_.Push(FriendState);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("FriendUID", FriendUID);
				Value_.Push("FriendState", FriendState);
			}
			public void Set(SMasterAddFriendEndInDb Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				FriendState = Obj_.FriendState;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(FriendUID) + "," + 
					"rso.game.EFriendState";
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(FriendUID, "FriendUID") + "," + 
					SEnumChecker.GetMemberName(FriendState, "FriendState");
			}
		}
		public class SMasterAddFriendFailInDb : SProto
		{
			public TUID UID = default(TUID);
			public TUID FriendUID = default(TUID);
			public SMasterAddFriendFailInDb()
			{
			}
			public SMasterAddFriendFailInDb(SMasterAddFriendFailInDb Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
			}
			public SMasterAddFriendFailInDb(TUID UID_, TUID FriendUID_)
			{
				UID = UID_;
				FriendUID = FriendUID_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref FriendUID);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("FriendUID", ref FriendUID);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(FriendUID);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("FriendUID", FriendUID);
			}
			public void Set(SMasterAddFriendFailInDb Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(FriendUID);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(FriendUID, "FriendUID");
			}
		}
		public class SMasterAllowFriendInDb : SProto
		{
			public TUID UID = default(TUID);
			public TUID FriendUID = default(TUID);
			public EFriendState FriendState = default(EFriendState);
			public SMasterAllowFriendInDb()
			{
			}
			public SMasterAllowFriendInDb(SMasterAllowFriendInDb Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				FriendState = Obj_.FriendState;
			}
			public SMasterAllowFriendInDb(TUID UID_, TUID FriendUID_, EFriendState FriendState_)
			{
				UID = UID_;
				FriendUID = FriendUID_;
				FriendState = FriendState_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref FriendUID);
				Stream_.Pop(ref FriendState);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("FriendUID", ref FriendUID);
				Value_.Pop("FriendState", ref FriendState);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(FriendUID);
				Stream_.Push(FriendState);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("FriendUID", FriendUID);
				Value_.Push("FriendState", FriendState);
			}
			public void Set(SMasterAllowFriendInDb Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				FriendState = Obj_.FriendState;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(FriendUID) + "," + 
					"rso.game.EFriendState";
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(FriendUID, "FriendUID") + "," + 
					SEnumChecker.GetMemberName(FriendState, "FriendState");
			}
		}
		public class SMasterDenyFriendInDb : SProto
		{
			public TUID UID = default(TUID);
			public TUID FriendUID = default(TUID);
			public EFriendState FriendState = default(EFriendState);
			public SMasterDenyFriendInDb()
			{
			}
			public SMasterDenyFriendInDb(SMasterDenyFriendInDb Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				FriendState = Obj_.FriendState;
			}
			public SMasterDenyFriendInDb(TUID UID_, TUID FriendUID_, EFriendState FriendState_)
			{
				UID = UID_;
				FriendUID = FriendUID_;
				FriendState = FriendState_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref FriendUID);
				Stream_.Pop(ref FriendState);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("FriendUID", ref FriendUID);
				Value_.Pop("FriendState", ref FriendState);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(FriendUID);
				Stream_.Push(FriendState);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("FriendUID", FriendUID);
				Value_.Push("FriendState", FriendState);
			}
			public void Set(SMasterDenyFriendInDb Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				FriendState = Obj_.FriendState;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(FriendUID) + "," + 
					"rso.game.EFriendState";
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(FriendUID, "FriendUID") + "," + 
					SEnumChecker.GetMemberName(FriendState, "FriendState");
			}
		}
		public class SMasterBlockFriendInDb : SProto
		{
			public TUID UID = default(TUID);
			public TUID FriendUID = default(TUID);
			public EFriendState FriendState = default(EFriendState);
			public SMasterBlockFriendInDb()
			{
			}
			public SMasterBlockFriendInDb(SMasterBlockFriendInDb Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				FriendState = Obj_.FriendState;
			}
			public SMasterBlockFriendInDb(TUID UID_, TUID FriendUID_, EFriendState FriendState_)
			{
				UID = UID_;
				FriendUID = FriendUID_;
				FriendState = FriendState_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref FriendUID);
				Stream_.Pop(ref FriendState);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("FriendUID", ref FriendUID);
				Value_.Pop("FriendState", ref FriendState);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(FriendUID);
				Stream_.Push(FriendState);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("FriendUID", FriendUID);
				Value_.Push("FriendState", FriendState);
			}
			public void Set(SMasterBlockFriendInDb Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				FriendState = Obj_.FriendState;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(FriendUID) + "," + 
					"rso.game.EFriendState";
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(FriendUID, "FriendUID") + "," + 
					SEnumChecker.GetMemberName(FriendState, "FriendState");
			}
		}
		public class SMasterUnBlockFriendInDb : SProto
		{
			public TUID UID = default(TUID);
			public TUID FriendUID = default(TUID);
			public EFriendState FriendState = default(EFriendState);
			public SMasterUnBlockFriendInDb()
			{
			}
			public SMasterUnBlockFriendInDb(SMasterUnBlockFriendInDb Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				FriendState = Obj_.FriendState;
			}
			public SMasterUnBlockFriendInDb(TUID UID_, TUID FriendUID_, EFriendState FriendState_)
			{
				UID = UID_;
				FriendUID = FriendUID_;
				FriendState = FriendState_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref FriendUID);
				Stream_.Pop(ref FriendState);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("FriendUID", ref FriendUID);
				Value_.Pop("FriendState", ref FriendState);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(FriendUID);
				Stream_.Push(FriendState);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("FriendUID", FriendUID);
				Value_.Push("FriendState", FriendState);
			}
			public void Set(SMasterUnBlockFriendInDb Obj_)
			{
				UID = Obj_.UID;
				FriendUID = Obj_.FriendUID;
				FriendState = Obj_.FriendState;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(FriendUID) + "," + 
					"rso.game.EFriendState";
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(FriendUID, "FriendUID") + "," + 
					SEnumChecker.GetMemberName(FriendState, "FriendState");
			}
		}
		public class SMasterChangeStateInDb : SProto
		{
			public TUID UID = default(TUID);
			public TState State = default(TState);
			public SMasterChangeStateInDb()
			{
			}
			public SMasterChangeStateInDb(SMasterChangeStateInDb Obj_)
			{
				UID = Obj_.UID;
				State = Obj_.State;
			}
			public SMasterChangeStateInDb(TUID UID_, TState State_)
			{
				UID = UID_;
				State = State_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref State);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("State", ref State);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(State);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("State", State);
			}
			public void Set(SMasterChangeStateInDb Obj_)
			{
				UID = Obj_.UID;
				State = Obj_.State;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(State);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(State, "State");
			}
		}
		public class SMasterCheckInDb : SProto
		{
			public TUID UID = default(TUID);
			public SMasterCheckInDb()
			{
			}
			public SMasterCheckInDb(SMasterCheckInDb Obj_)
			{
				UID = Obj_.UID;
			}
			public SMasterCheckInDb(TUID UID_)
			{
				UID = UID_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
			}
			public void Set(SMasterCheckInDb Obj_)
			{
				UID = Obj_.UID;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID");
			}
		}
		public class SMasterCheckOutDb : SProto
		{
			public List<TimePoint> PunishEndTimes = new List<TimePoint>();
			public SMasterCheckOutDb()
			{
			}
			public SMasterCheckOutDb(SMasterCheckOutDb Obj_)
			{
				PunishEndTimes = Obj_.PunishEndTimes;
			}
			public SMasterCheckOutDb(List<TimePoint> PunishEndTimes_)
			{
				PunishEndTimes = PunishEndTimes_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref PunishEndTimes);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("PunishEndTimes", ref PunishEndTimes);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(PunishEndTimes);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("PunishEndTimes", PunishEndTimes);
			}
			public void Set(SMasterCheckOutDb Obj_)
			{
				PunishEndTimes = Obj_.PunishEndTimes;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(PunishEndTimes);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(PunishEndTimes, "PunishEndTimes");
			}
		}
		public class SMasterPunishInDb : SProto
		{
			public TUID UID = default(TUID);
			public TimePoint EndTime = default(TimePoint);
			public SMasterPunishInDb()
			{
			}
			public SMasterPunishInDb(SMasterPunishInDb Obj_)
			{
				UID = Obj_.UID;
				EndTime = Obj_.EndTime;
			}
			public SMasterPunishInDb(TUID UID_, TimePoint EndTime_)
			{
				UID = UID_;
				EndTime = EndTime_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref EndTime);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("EndTime", ref EndTime);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(EndTime);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("EndTime", EndTime);
			}
			public void Set(SMasterPunishInDb Obj_)
			{
				UID = Obj_.UID;
				EndTime = Obj_.EndTime;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(EndTime);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(EndTime, "EndTime");
			}
		}
		public class SMasterPunishOutDb : SProto
		{
			public override void Push(CStream Stream_)
			{
			}
			public override void Push(JsonDataObject Value_)
			{
			}
			public override void Pop(CStream Stream_)
			{
			}
			public override void Pop(JsonDataObject Value_)
			{
			}
			public override string StdName()
			{
				return "";
			}
			public override string MemberName()
			{
				return "";
			}
		}
		public class SLoginInfo : SProto
		{
			public TUID UID = default(TUID);
			public TID ID = string.Empty;
			public SLoginInfo()
			{
			}
			public SLoginInfo(SLoginInfo Obj_)
			{
				UID = Obj_.UID;
				ID = Obj_.ID;
			}
			public SLoginInfo(TUID UID_, TID ID_)
			{
				UID = UID_;
				ID = ID_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref UID);
				Stream_.Pop(ref ID);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("UID", ref UID);
				Value_.Pop("ID", ref ID);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(UID);
				Stream_.Push(ID);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("UID", UID);
				Value_.Push("ID", ID);
			}
			public void Set(SLoginInfo Obj_)
			{
				UID = Obj_.UID;
				ID = Obj_.ID;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(UID) + "," + 
					SEnumChecker.GetStdName(ID);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(UID, "UID") + "," + 
					SEnumChecker.GetMemberName(ID, "ID");
			}
		}
		public partial class global
		{
			public const Int32 c_BinaryIDLengthMax = 32;
			public const TState c_Default_State = 0;
			public const Int32 c_IDLengthMax = 255;
			public const Int32 c_NickLengthMax = 32;
			public const Int32 c_NickLengthMin = 2;
		}
	}
}
