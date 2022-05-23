using TSize = System.Int32;
using TCheckSum = System.UInt64;
using TUID = System.Int64;
using TPeerCnt = System.UInt32;
using TLongIP = System.UInt32;
using TPort = System.UInt16;
using TPacketSeq = System.UInt64;
using TSessionCode = System.Int64;
using SRangeUID = rso.net.SRangeKey<System.Int64>;
using System;
using System.Collections.Generic;
using rso.core;


namespace rso
{
	namespace balance
	{
		using rso.net;
		public enum EProto
		{
			UdParentOn,
			DuChildOn,
			UdNewChild,
			DuNewChild,
			DuNewChildFail,
			UdNewParent,
			UdCapacity,
			DuCapacity,
			CsConnect,
			ScNewParent,
			ScAllocated,
			UdBroadCast,
			UdUserProto,
			DuUserProto,
			ScUserProto,
			CsUserProto,
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
					"rso.balance.EProto";
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(Proto, "Proto");
			}
		}
		public class SCapacity : SProto
		{
			public TPeerCnt ClientCnt = default(TPeerCnt);
			public TPeerCnt ServerCnt = default(TPeerCnt);
			public SCapacity()
			{
			}
			public SCapacity(SCapacity Obj_)
			{
				ClientCnt = Obj_.ClientCnt;
				ServerCnt = Obj_.ServerCnt;
			}
			public SCapacity(TPeerCnt ClientCnt_, TPeerCnt ServerCnt_)
			{
				ClientCnt = ClientCnt_;
				ServerCnt = ServerCnt_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ClientCnt);
				Stream_.Pop(ref ServerCnt);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ClientCnt", ref ClientCnt);
				Value_.Pop("ServerCnt", ref ServerCnt);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ClientCnt);
				Stream_.Push(ServerCnt);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ClientCnt", ClientCnt);
				Value_.Push("ServerCnt", ServerCnt);
			}
			public void Set(SCapacity Obj_)
			{
				ClientCnt = Obj_.ClientCnt;
				ServerCnt = Obj_.ServerCnt;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ClientCnt) + "," + 
					SEnumChecker.GetStdName(ServerCnt);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ClientCnt, "ClientCnt") + "," + 
					SEnumChecker.GetMemberName(ServerCnt, "ServerCnt");
			}
		}
		public class SServer : SProto
		{
			public SNamePort ClientBindNamePortPub = new SNamePort();
			public SCapacity Capacity = new SCapacity();
			public SServer()
			{
			}
			public SServer(SServer Obj_)
			{
				ClientBindNamePortPub = Obj_.ClientBindNamePortPub;
				Capacity = Obj_.Capacity;
			}
			public SServer(SNamePort ClientBindNamePortPub_, SCapacity Capacity_)
			{
				ClientBindNamePortPub = ClientBindNamePortPub_;
				Capacity = Capacity_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ClientBindNamePortPub);
				Stream_.Pop(ref Capacity);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ClientBindNamePortPub", ref ClientBindNamePortPub);
				Value_.Pop("Capacity", ref Capacity);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ClientBindNamePortPub);
				Stream_.Push(Capacity);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ClientBindNamePortPub", ClientBindNamePortPub);
				Value_.Push("Capacity", Capacity);
			}
			public void Set(SServer Obj_)
			{
				ClientBindNamePortPub.Set(Obj_.ClientBindNamePortPub);
				Capacity.Set(Obj_.Capacity);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ClientBindNamePortPub) + "," + 
					SEnumChecker.GetStdName(Capacity);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ClientBindNamePortPub, "ClientBindNamePortPub") + "," + 
					SEnumChecker.GetMemberName(Capacity, "Capacity");
			}
		}
		public class SUdParentOn : SServer
		{
			public TPeerCnt ChildCntMax = default(TPeerCnt);
			public TPeerCnt ErrorCnt = default(TPeerCnt);
			public SUdParentOn()
			{
			}
			public SUdParentOn(SUdParentOn Obj_) : base(Obj_)
			{
				ChildCntMax = Obj_.ChildCntMax;
				ErrorCnt = Obj_.ErrorCnt;
			}
			public SUdParentOn(SServer Super_, TPeerCnt ChildCntMax_, TPeerCnt ErrorCnt_) : base(Super_)
			{
				ChildCntMax = ChildCntMax_;
				ErrorCnt = ErrorCnt_;
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
				Stream_.Pop(ref ChildCntMax);
				Stream_.Pop(ref ErrorCnt);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
				Value_.Pop("ChildCntMax", ref ChildCntMax);
				Value_.Pop("ErrorCnt", ref ErrorCnt);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
				Stream_.Push(ChildCntMax);
				Stream_.Push(ErrorCnt);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
				Value_.Push("ChildCntMax", ChildCntMax);
				Value_.Push("ErrorCnt", ErrorCnt);
			}
			public void Set(SUdParentOn Obj_)
			{
				base.Set(Obj_);
				ChildCntMax = Obj_.ChildCntMax;
				ErrorCnt = Obj_.ErrorCnt;
			}
			public override string StdName()
			{
				return 
					base.StdName() + "," + 
					SEnumChecker.GetStdName(ChildCntMax) + "," + 
					SEnumChecker.GetStdName(ErrorCnt);
			}
			public override string MemberName()
			{
				return 
					base.MemberName() + "," + 
					SEnumChecker.GetMemberName(ChildCntMax, "ChildCntMax") + "," + 
					SEnumChecker.GetMemberName(ErrorCnt, "ErrorCnt");
			}
		}
		public class SDuChildOn : SServer
		{
			public TSessionCode SessionCode = default(TSessionCode);
			public SNamePort ChildBindNamePort = new SNamePort();
			public SDuChildOn()
			{
			}
			public SDuChildOn(SDuChildOn Obj_) : base(Obj_)
			{
				SessionCode = Obj_.SessionCode;
				ChildBindNamePort = Obj_.ChildBindNamePort;
			}
			public SDuChildOn(SServer Super_, TSessionCode SessionCode_, SNamePort ChildBindNamePort_) : base(Super_)
			{
				SessionCode = SessionCode_;
				ChildBindNamePort = ChildBindNamePort_;
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
				Stream_.Pop(ref SessionCode);
				Stream_.Pop(ref ChildBindNamePort);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
				Value_.Pop("SessionCode", ref SessionCode);
				Value_.Pop("ChildBindNamePort", ref ChildBindNamePort);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
				Stream_.Push(SessionCode);
				Stream_.Push(ChildBindNamePort);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
				Value_.Push("SessionCode", SessionCode);
				Value_.Push("ChildBindNamePort", ChildBindNamePort);
			}
			public void Set(SDuChildOn Obj_)
			{
				base.Set(Obj_);
				SessionCode = Obj_.SessionCode;
				ChildBindNamePort.Set(Obj_.ChildBindNamePort);
			}
			public override string StdName()
			{
				return 
					base.StdName() + "," + 
					SEnumChecker.GetStdName(SessionCode) + "," + 
					SEnumChecker.GetStdName(ChildBindNamePort);
			}
			public override string MemberName()
			{
				return 
					base.MemberName() + "," + 
					SEnumChecker.GetMemberName(SessionCode, "SessionCode") + "," + 
					SEnumChecker.GetMemberName(ChildBindNamePort, "ChildBindNamePort");
			}
		}
		public class SUdNewChild : SProto
		{
			public SNamePort ClientBindNamePortPub = new SNamePort();
			public TSessionCode SessionCode = default(TSessionCode);
			public SKey NewChildKey = new SKey();
			public SUdNewChild()
			{
			}
			public SUdNewChild(SUdNewChild Obj_)
			{
				ClientBindNamePortPub = Obj_.ClientBindNamePortPub;
				SessionCode = Obj_.SessionCode;
				NewChildKey = Obj_.NewChildKey;
			}
			public SUdNewChild(SNamePort ClientBindNamePortPub_, TSessionCode SessionCode_, SKey NewChildKey_)
			{
				ClientBindNamePortPub = ClientBindNamePortPub_;
				SessionCode = SessionCode_;
				NewChildKey = NewChildKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ClientBindNamePortPub);
				Stream_.Pop(ref SessionCode);
				Stream_.Pop(ref NewChildKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ClientBindNamePortPub", ref ClientBindNamePortPub);
				Value_.Pop("SessionCode", ref SessionCode);
				Value_.Pop("NewChildKey", ref NewChildKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ClientBindNamePortPub);
				Stream_.Push(SessionCode);
				Stream_.Push(NewChildKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ClientBindNamePortPub", ClientBindNamePortPub);
				Value_.Push("SessionCode", SessionCode);
				Value_.Push("NewChildKey", NewChildKey);
			}
			public void Set(SUdNewChild Obj_)
			{
				ClientBindNamePortPub.Set(Obj_.ClientBindNamePortPub);
				SessionCode = Obj_.SessionCode;
				NewChildKey.Set(Obj_.NewChildKey);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ClientBindNamePortPub) + "," + 
					SEnumChecker.GetStdName(SessionCode) + "," + 
					SEnumChecker.GetStdName(NewChildKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ClientBindNamePortPub, "ClientBindNamePortPub") + "," + 
					SEnumChecker.GetMemberName(SessionCode, "SessionCode") + "," + 
					SEnumChecker.GetMemberName(NewChildKey, "NewChildKey");
			}
		}
		public class SDuNewChild : SProto
		{
			public SKey NewChildKey = new SKey();
			public TSessionCode SessionCode = default(TSessionCode);
			public SDuNewChild()
			{
			}
			public SDuNewChild(SDuNewChild Obj_)
			{
				NewChildKey = Obj_.NewChildKey;
				SessionCode = Obj_.SessionCode;
			}
			public SDuNewChild(SKey NewChildKey_, TSessionCode SessionCode_)
			{
				NewChildKey = NewChildKey_;
				SessionCode = SessionCode_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref NewChildKey);
				Stream_.Pop(ref SessionCode);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("NewChildKey", ref NewChildKey);
				Value_.Pop("SessionCode", ref SessionCode);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(NewChildKey);
				Stream_.Push(SessionCode);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("NewChildKey", NewChildKey);
				Value_.Push("SessionCode", SessionCode);
			}
			public void Set(SDuNewChild Obj_)
			{
				NewChildKey.Set(Obj_.NewChildKey);
				SessionCode = Obj_.SessionCode;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(NewChildKey) + "," + 
					SEnumChecker.GetStdName(SessionCode);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(NewChildKey, "NewChildKey") + "," + 
					SEnumChecker.GetMemberName(SessionCode, "SessionCode");
			}
		}
		public class SDuNewChildFail : SProto
		{
			public SKey NewChildKey = new SKey();
			public SDuNewChildFail()
			{
			}
			public SDuNewChildFail(SDuNewChildFail Obj_)
			{
				NewChildKey = Obj_.NewChildKey;
			}
			public SDuNewChildFail(SKey NewChildKey_)
			{
				NewChildKey = NewChildKey_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref NewChildKey);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("NewChildKey", ref NewChildKey);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(NewChildKey);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("NewChildKey", NewChildKey);
			}
			public void Set(SDuNewChildFail Obj_)
			{
				NewChildKey.Set(Obj_.NewChildKey);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(NewChildKey);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(NewChildKey, "NewChildKey");
			}
		}
		public class SUdNewParent : SProto
		{
			public SNamePort ChildBindNamePort = new SNamePort();
			public TSessionCode SessionCode = default(TSessionCode);
			public SUdNewParent()
			{
			}
			public SUdNewParent(SUdNewParent Obj_)
			{
				ChildBindNamePort = Obj_.ChildBindNamePort;
				SessionCode = Obj_.SessionCode;
			}
			public SUdNewParent(SNamePort ChildBindNamePort_, TSessionCode SessionCode_)
			{
				ChildBindNamePort = ChildBindNamePort_;
				SessionCode = SessionCode_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ChildBindNamePort);
				Stream_.Pop(ref SessionCode);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ChildBindNamePort", ref ChildBindNamePort);
				Value_.Pop("SessionCode", ref SessionCode);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ChildBindNamePort);
				Stream_.Push(SessionCode);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ChildBindNamePort", ChildBindNamePort);
				Value_.Push("SessionCode", SessionCode);
			}
			public void Set(SUdNewParent Obj_)
			{
				ChildBindNamePort.Set(Obj_.ChildBindNamePort);
				SessionCode = Obj_.SessionCode;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ChildBindNamePort) + "," + 
					SEnumChecker.GetStdName(SessionCode);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ChildBindNamePort, "ChildBindNamePort") + "," + 
					SEnumChecker.GetMemberName(SessionCode, "SessionCode");
			}
		}
		public class SCsConnect : SProto
		{
			public SNamePort PrevClientBindNamePortPub = new SNamePort();
			public SCsConnect()
			{
			}
			public SCsConnect(SCsConnect Obj_)
			{
				PrevClientBindNamePortPub = Obj_.PrevClientBindNamePortPub;
			}
			public SCsConnect(SNamePort PrevClientBindNamePortPub_)
			{
				PrevClientBindNamePortPub = PrevClientBindNamePortPub_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref PrevClientBindNamePortPub);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("PrevClientBindNamePortPub", ref PrevClientBindNamePortPub);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(PrevClientBindNamePortPub);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("PrevClientBindNamePortPub", PrevClientBindNamePortPub);
			}
			public void Set(SCsConnect Obj_)
			{
				PrevClientBindNamePortPub.Set(Obj_.PrevClientBindNamePortPub);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(PrevClientBindNamePortPub);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(PrevClientBindNamePortPub, "PrevClientBindNamePortPub");
			}
		}
		public class SScNewParent : SProto
		{
			public SNamePort ClientBindNamePortPub = new SNamePort();
			public SScNewParent()
			{
			}
			public SScNewParent(SScNewParent Obj_)
			{
				ClientBindNamePortPub = Obj_.ClientBindNamePortPub;
			}
			public SScNewParent(SNamePort ClientBindNamePortPub_)
			{
				ClientBindNamePortPub = ClientBindNamePortPub_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ClientBindNamePortPub);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ClientBindNamePortPub", ref ClientBindNamePortPub);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ClientBindNamePortPub);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ClientBindNamePortPub", ClientBindNamePortPub);
			}
			public void Set(SScNewParent Obj_)
			{
				ClientBindNamePortPub.Set(Obj_.ClientBindNamePortPub);
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ClientBindNamePortPub);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ClientBindNamePortPub, "ClientBindNamePortPub");
			}
		}
		public class SScAllocated : SProto
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
	}
}
