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
	namespace balance_rr
	{
		using rso.net;
		public enum EProto
		{
			SaServerOn,
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
			public SSaServerOn()
			{
			}
			public SSaServerOn(SSaServerOn Obj_)
			{
				ClientBindNamePortPub = Obj_.ClientBindNamePortPub;
			}
			public SSaServerOn(SNamePort ClientBindNamePortPub_)
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
			public void Set(SSaServerOn Obj_)
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
			public SServer()
			{
			}
			public SServer(SServer Obj_) : base(Obj_)
			{
			}
			public SServer(SSaServerOn Super_) : base(Super_)
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
			public void Set(SServer Obj_)
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
	}
}
