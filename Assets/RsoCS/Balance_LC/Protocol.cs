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
	namespace balance_lc
	{
		using rso.net;
		public enum EProto
		{
			SaServerOn,
			SaClientCnt,
			AcServerToConnect,
			ScAllocated,
			AsUserProto,
			SaUserProto,
			ScUserProto,
			CsUserProto,
			Max,
			Null,
		}
		public class SSaServerOn : SProto
		{
			public SNamePort ClientBindNamePortPub = new SNamePort();
			public Int32 ClientCnt = default(Int32);
			public SSaServerOn()
			{
			}
			public SSaServerOn(SSaServerOn Obj_)
			{
				ClientBindNamePortPub = Obj_.ClientBindNamePortPub;
				ClientCnt = Obj_.ClientCnt;
			}
			public SSaServerOn(SNamePort ClientBindNamePortPub_, Int32 ClientCnt_)
			{
				ClientBindNamePortPub = ClientBindNamePortPub_;
				ClientCnt = ClientCnt_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ClientBindNamePortPub);
				Stream_.Pop(ref ClientCnt);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ClientBindNamePortPub", ref ClientBindNamePortPub);
				Value_.Pop("ClientCnt", ref ClientCnt);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ClientBindNamePortPub);
				Stream_.Push(ClientCnt);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ClientBindNamePortPub", ClientBindNamePortPub);
				Value_.Push("ClientCnt", ClientCnt);
			}
			public void Set(SSaServerOn Obj_)
			{
				ClientBindNamePortPub.Set(Obj_.ClientBindNamePortPub);
				ClientCnt = Obj_.ClientCnt;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ClientBindNamePortPub) + "," + 
					SEnumChecker.GetStdName(ClientCnt);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ClientBindNamePortPub, "ClientBindNamePortPub") + "," + 
					SEnumChecker.GetMemberName(ClientCnt, "ClientCnt");
			}
		}
		public class SSaClientCnt : SProto
		{
			public Int32 ClientCnt = default(Int32);
			public SSaClientCnt()
			{
			}
			public SSaClientCnt(SSaClientCnt Obj_)
			{
				ClientCnt = Obj_.ClientCnt;
			}
			public SSaClientCnt(Int32 ClientCnt_)
			{
				ClientCnt = ClientCnt_;
			}
			public override void Push(CStream Stream_)
			{
				Stream_.Pop(ref ClientCnt);
			}
			public override void Push(JsonDataObject Value_)
			{
				Value_.Pop("ClientCnt", ref ClientCnt);
			}
			public override void Pop(CStream Stream_)
			{
				Stream_.Push(ClientCnt);
			}
			public override void Pop(JsonDataObject Value_)
			{
				Value_.Push("ClientCnt", ClientCnt);
			}
			public void Set(SSaClientCnt Obj_)
			{
				ClientCnt = Obj_.ClientCnt;
			}
			public override string StdName()
			{
				return 
					SEnumChecker.GetStdName(ClientCnt);
			}
			public override string MemberName()
			{
				return 
					SEnumChecker.GetMemberName(ClientCnt, "ClientCnt");
			}
		}
		public class SAcServerToConnect : SProto
		{
			public SNamePort ClientBindNamePortPub = new SNamePort();
			public SAcServerToConnect()
			{
			}
			public SAcServerToConnect(SAcServerToConnect Obj_)
			{
				ClientBindNamePortPub = Obj_.ClientBindNamePortPub;
			}
			public SAcServerToConnect(SNamePort ClientBindNamePortPub_)
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
			public void Set(SAcServerToConnect Obj_)
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
		public class SServer : SSaServerOn
		{
			public List<TimePoint> EndTimes = new List<TimePoint>();
			public SServer()
			{
			}
			public SServer(SServer Obj_) : base(Obj_)
			{
				EndTimes = Obj_.EndTimes;
			}
			public SServer(SSaServerOn Super_, List<TimePoint> EndTimes_) : base(Super_)
			{
				EndTimes = EndTimes_;
			}
			public override void Push(CStream Stream_)
			{
				base.Push(Stream_);
				Stream_.Pop(ref EndTimes);
			}
			public override void Push(JsonDataObject Value_)
			{
				base.Push(Value_);
				Value_.Pop("EndTimes", ref EndTimes);
			}
			public override void Pop(CStream Stream_)
			{
				base.Pop(Stream_);
				Stream_.Push(EndTimes);
			}
			public override void Pop(JsonDataObject Value_)
			{
				base.Pop(Value_);
				Value_.Push("EndTimes", EndTimes);
			}
			public void Set(SServer Obj_)
			{
				base.Set(Obj_);
				EndTimes = Obj_.EndTimes;
			}
			public override string StdName()
			{
				return 
					base.StdName() + "," + 
					SEnumChecker.GetStdName(EndTimes);
			}
			public override string MemberName()
			{
				return 
					base.MemberName() + "," + 
					SEnumChecker.GetMemberName(EndTimes, "EndTimes");
			}
		}
	}
}
